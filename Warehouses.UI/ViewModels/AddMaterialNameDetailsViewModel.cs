using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warehouse.Model;
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
            MaterialsNames = new ObservableCollection<Warehouse.Model.MaterialName>();
            Add = new DelegateCommand(ExecuteAddCommand, ExecuteCanAddCommand);
            Delete = new DelegateCommand<MaterialName>(ExecuteDeleteCommand);
        }

        private void ExecuteDeleteCommand(MaterialName materialName)
        {
            MaterialsNames.Remove(materialName);
        }

        public void Load()
        {
            var languages = _languageDataService.GetAll();
            FillLists(Languages, languages);
        }

        private void ExecuteAddCommand()
        {
            //MessageBox.Show(MaterialName + " " + SelectedLanguage.Name);
            MaterialsNames.Add(new Warehouse.Model.MaterialName { Name = MaterialName, Language = SelectedLanguage.Name });
        }

        private bool ExecuteCanAddCommand()
        {
            return true;// SelectedLanguage != null && !string.IsNullOrEmpty(MaterialName);
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
            }
        }

        public Language SelectedLanguage
        {
            get { return _selecteLanguage; }
            set {
                _selecteLanguage = value;
                OnPropertyChanged();
            }
        }
        public ICommand Add { get; set; }
        public ICommand Delete { get; set; }
    }
}
