using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoList.Data
{
    public class CachedDataItemsRepository : ICachedDataItemsRepository
    {
        private List<DataItem> _data = new List<DataItem>();

        public void AddDataToCache(List<DataItem> data)
        {
            _data.AddRange(data);
        }

        public async Task<bool> ContainsData(int index, int count)
        {
            int cachedDataCount = await GetAvaliableDataAmount();
            return index < cachedDataCount && index + count <= cachedDataCount;
        }

        public Task<int> GetAvaliableDataAmount()
        {
            return Task.Run(() => _data.Count);
        }

        public Task<List<DataItem>> GetData(int index, int count)
        {
            return Task.Run(() => _data.GetRange(index, count));
        }
    }
}
