using InfoList.Widgets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace InfoList.Presentation
{
    public class InfoListPage : MonoBehaviour
    {
        [SerializeField] private UIDocument _page;
        [SerializeField] private InfoListVisualizer _infoListVisualizer;

        [Inject] private IInfoListViewModel _viewModel;
        private ProgressIndicator _progressIndicator = new LoadingCircle();

        private VisualElement _rootElement;
        private ListView _infoList;
        private Button _previousPageButton;
        private Button _nextPageButton;
        private VisualElement _loadingCircle;

        private void Start()
        {
            DefineViewElements();
            BindButtonEvents();

            _progressIndicator.Initialize(_loadingCircle);

            _infoListVisualizer.Initialize(_infoList);

            _viewModel.OnDataItemsUpdated += UpdateView;
            _viewModel.OnDataItemsLoadingStart += OnDataItemsLoadingStart;
            _viewModel.Initialize();
        }

        private void DefineViewElements()
        {
            _rootElement = _page.rootVisualElement;
            _previousPageButton = _rootElement.Q<Button>("previous_button");
            _nextPageButton = _rootElement.Q<Button>("next_button");
            _infoList = _rootElement.Q<ListView>("info_list");
            _loadingCircle = _rootElement.Q<VisualElement>("loading_circle");
        }

        private void BindButtonEvents()
        {
            _previousPageButton.RegisterCallback<ClickEvent>(ce => _viewModel.OnPreviosPageButtonClick());
            _nextPageButton.RegisterCallback<ClickEvent>(ce => _viewModel.OnNextPageButtonClick());
        }

        private void OnDestroy()
        {
            _viewModel.OnDataItemsUpdated -= UpdateView;
            _viewModel.OnDataItemsLoadingStart += OnDataItemsLoadingStart;
        }

        private void OnDataItemsLoadingStart()
        {
            _progressIndicator.StartProgress(); 
        }

        private void UpdateView(List<DataItem> dataItems, int startIndex)
        {
            _progressIndicator.StopProgress();

            _infoListVisualizer.UpdateList(dataItems, startIndex);
        }

    }
}
