using Autofac.Features.Indexed;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Warehouses.UI.Events;
using Warehouses.UI.Views.Services;

namespace Warehouses.UI.ViewModels
{

    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private IEventAggregator _eventAggregator;
        private IDetailViewModel _detailViewModel;
        private IMessageDialogService _messageDialogService;
        private IIndex<string, IDetailViewModel> _detailViewModelCreator;

        public MainViewModel(IMainMenuViewModel mainMenuViewModel,
            INavigationViewModel navigationViewModel,
          IIndex<string, IDetailViewModel> detailViewModelCreator,
          IEventAggregator eventAggregator,
          IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _detailViewModelCreator = detailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
             .Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
              .Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            NavigationViewModel = navigationViewModel;
            MainMenuViewModel = mainMenuViewModel;
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }

        public ICommand CreateNewDetailCommand { get; }

        public INavigationViewModel NavigationViewModel { get; }
        public IMainMenuViewModel MainMenuViewModel { get; set; }

        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            private set
            {
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }

        private void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null)
            {
                var result = _messageDialogService.ShowOkCancelDialog("If you've made changes will be discared. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            DetailViewModel = _detailViewModelCreator[args.ViewModelName];
            DetailViewModel.Load(args.Id);
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
              new OpenDetailViewEventArgs { ViewModelName = viewModelType.Name });
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            DetailViewModel = null;
        }
    }
}


    //public MainWindowViewModel(IMainMenuViewModel mainMenuViewModel,
    //    IMainWIndowWarehouseSelectionViewModel mainWIndowWarehouseSelectionDropDownsViewModel)
    //{
    //    MainWIndowWarehouseSelectionDropDownsViewModel = mainWIndowWarehouseSelectionDropDownsViewModel;
    //    MainMenuViewModel = mainMenuViewModel;
    //}


    //public void Load()
    //{
    //    MainWIndowWarehouseSelectionDropDownsViewModel.Load();
    //}
    //public IMainWIndowWarehouseSelectionViewModel MainWIndowWarehouseSelectionDropDownsViewModel { get; set; }
    //public IMainMenuViewModel MainMenuViewModel { get; set; }
