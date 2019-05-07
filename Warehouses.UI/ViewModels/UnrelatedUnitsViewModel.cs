namespace Warehouses.UI.ViewModels
{
    public class UnrelatedUnitsViewModel : ViewModelBase
    {
        private string _name;

        public UnrelatedUnitsViewModel()
        {

        }

        public long Id { get; set; }


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private float _quanitity;

        public float Quanitity
        {
            get { return _quanitity; }
            set
            {
                _quanitity = value;
                OnPropertyChanged();
            }
        }

    }
}
