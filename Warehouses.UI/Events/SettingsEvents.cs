namespace Warehouses.UI.Events
{
    public class SettingsEvents
    {
        public delegate void SettingsAction();
        public SettingsAction changeLanguage;

        public SettingsEvents()
        {

        }
    }
}
