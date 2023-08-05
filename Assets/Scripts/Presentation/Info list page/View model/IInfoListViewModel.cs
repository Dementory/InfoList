using System.Collections.Generic;
using System;

namespace InfoList.Presentation
{
    public interface IInfoListViewModel
    {
        event Action<List<DataItem>, int> OnDataItemsUpdated;
        event Action OnDataItemsLoadingStart;

        public void Initialize();

        public void OnNextPageButtonClick();

        public void OnPreviosPageButtonClick();
    }
}
