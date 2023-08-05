using InfoList.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace InfoList.Presentation
{
    public class InfoListViewModel : IInfoListViewModel
    {
        private int _displayedItemsAmount = 5;
        private int _currentPageIndex;

        private IDataItemsRepository _repository;

        public event Action<List<DataItem>, int> OnDataItemsUpdated;
        public event Action OnDataItemsLoadingStart;

        [Inject]
        public InfoListViewModel(IDataItemsRepository repository)
        {
            _repository = repository;
        }

        public void Initialize()
        {
            UpdateInfoListItems();
        }

        public void OnNextPageButtonClick()
        {
            SwitchToAnotherPage(_currentPageIndex + 1);
        }

        public void OnPreviosPageButtonClick()
        {
            SwitchToAnotherPage(_currentPageIndex - 1);
        }

        private async void SwitchToAnotherPage(int pageIndex)
        {
            OnDataItemsLoadingStart?.Invoke();

            await ChangePageIndex(pageIndex);
            UpdateInfoListItems();
        }

        private async Task<bool> ChangePageIndex(int pageIndex)
        {
            int avaliableDataAmount = await _repository.GetAvaliableDataAmount();
            int maxIndex = (int)MathF.Ceiling((float)avaliableDataAmount / (float)_displayedItemsAmount) - 1;
            int minIndex = 0;

            _currentPageIndex = Mathf.Clamp(pageIndex, minIndex, maxIndex);

            return true;
        }

        private async void UpdateInfoListItems()
        {
            int startIndex = _currentPageIndex * _displayedItemsAmount;
            List<DataItem> dataItems = await _repository.GetData(startIndex, _displayedItemsAmount);

            OnDataItemsUpdated?.Invoke(dataItems, startIndex);
        }
    }
}
