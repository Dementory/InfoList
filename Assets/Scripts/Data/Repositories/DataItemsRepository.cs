using InfoList.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace InfoList.Data
{
    public class DataItemsRepository : IDataItemsRepository
    {
        private IDataServer _dataServer;
        private ICachedDataItemsRepository _cachedDataRepository;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken => _cancellationTokenSource.Token;

        [Inject]
        public DataItemsRepository(IDataServer dataServer, ICachedDataItemsRepository cachedDataItemsRepository)
        {
            _dataServer = dataServer;
            _cachedDataRepository = cachedDataItemsRepository;
        }

        public async Task<int> GetAvaliableDataAmount() => await _dataServer.DataAvailable(_cancellationToken);

        public async Task<List<DataItem>> GetData(int index, int count)
        {
            CancelPrevioiusDataRequest();

            bool cacheContainsdata = await _cachedDataRepository.ContainsData(index, count);
            if (cacheContainsdata)
            {
                List<DataItem> cachedData = await _cachedDataRepository.GetData(index, count);
                return cachedData;
            }

            int actualAmount = await GetActualDataAmount(index, count);
            List<DataItem> list = (List<DataItem>)await _dataServer.RequestData(index, actualAmount, _cancellationToken);

            _cachedDataRepository.AddDataToCache(list);

            return list;
        }

        private async Task<int> GetActualDataAmount(int index, int count)
        {
            int availableDataAmount = await GetAvaliableDataAmount();
            int actualAmount = Mathf.Min(count, availableDataAmount - (index + 1));

            return actualAmount;
        }

        private void CancelPrevioiusDataRequest()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Cancel();

            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}
