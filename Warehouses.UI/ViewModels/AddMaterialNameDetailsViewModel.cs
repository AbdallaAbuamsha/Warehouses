using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;

namespace Warehouses.UI.ViewModels
{
    class AddMaterialNameDetailsViewModel : ViewModelBase, IAddMaterialNameDetailsViewModel
    {
        ILanguagesDataService _languageDataService;
        private string _materialName;
        private Language _selecteLanguage;

        public AddMaterialNameDetailsViewModel(ILanguagesDataService languageDataService)
        {
            this._languageDataService = languageDataService;
            Languages = new ObservableCollection<Language>();
            MaterialsNames = new ObservableCollection<MaterialName>();
            Add = new DelegateCommand(ExecuteAddCommand, ExecuteCanAddCommand);
            Delete = new DelegateCommand<MaterialName>(ExecuteDeleteCommand);
        }

        private void ExecuteDeleteCommand(MaterialName materialName)
        {
            MaterialsNames.Remove(materialName);
            Languages.Insert(0, materialName.Language);
        }

        public void Load()
        {
            var languages = _languageDataService.GetAll();
            FillLists(Languages, languages);
        }

        public ObservableCollection<Language> Languages { get; set; }

        private ObservableCollection<MaterialName> _materialsNames;

        public ObservableCollection<MaterialName> MaterialsNames {
            get
            {
                return _materialsNames;
            }
            set
            {
                _materialsNames = value;
                OnPropertyChanged();
            }
        }

        public string MaterialName
        {
            get { return _materialName; }
            set { _materialName = value;
                OnPropertyChanged();
                ((DelegateCommand)Add).RaiseCanExecuteChanged();
            }
        }

        public Language SelectedLanguage
        {
            get { return _selecteLanguage; }
            set {
                _selecteLanguage = value;
                OnPropertyChanged();
                ((DelegateCommand)Add).RaiseCanExecuteChanged();
            }
        }

        public ICommand Add { get; set; }

        public ICommand Delete { get; set; }

        private void ExecuteAddCommand()
        {
            //MessageBox.Show(MaterialName + " " + SelectedLanguage.Name);
            MaterialsNames.Add(new MaterialName { Name = MaterialName, Language = SelectedLanguage });
            Languages.Remove(SelectedLanguage);
            MaterialName = string.Empty;
        }

        private bool ExecuteCanAddCommand()
        {
            return !string.IsNullOrEmpty(MaterialName) 
                && !string.IsNullOrWhiteSpace(MaterialName)
                && SelectedLanguage != null ;
        }

    }
}
