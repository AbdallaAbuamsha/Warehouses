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
        private IIndex<string, INavigationViewModel> _navigationViewModelCreator;
        private INavigationViewModel _navigationViewModel;

        public MainViewModel(IMainMenuViewModel mainMenuViewModel,
          IIndex<string, IDetailViewModel> detailViewModelCreator,
          IIndex<string, INavigationViewModel> navigationViewModelCreator,
          IEventAggregator eventAggregator,
          IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _detailViewModelCreator = detailViewModelCreator;
            _navigationViewModelCreator = navigationViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<SelecteNavigationType>().Subscribe(SelecteNavigationType);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            MainMenuViewModel = mainMenuViewModel;

        }

        public void Load()
        {
            //NavigationViewModel.Load();
        }

        public ICommand CreateNewDetailCommand { get; }

        public INavigationViewModel NavigationViewModel
        {
            get
            {
                return _navigationViewModel;
            }
            set
            {
                _navigationViewModel = value;
                OnPropertyChanged();
            }
        }

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
        private void SelecteNavigationType(SelecteNavigationTypeArgs args)
        {
            NavigationViewModel = _navigationViewModelCreator[args.NavigationTypeName];
            NavigationViewModel.Load();
        }
        private void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            //if (DetailViewModel != null)
            //{
            //    var result = _messageDialogService.ShowOkCancelDialog("If you've made changes will be discared. Navigate away?", "Question");
            //    if (result == MessageDialogResult.Cancel)
            //    {
            //        return;
            //    }
            //}
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
