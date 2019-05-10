using Prism.Events;

namespace Warehouses.UI.Events
{
    class ExpandTreeItemEvent : PubSubEvent<ExpandTreeItemEventArgs>
    {
    }

    public class ExpandTreeItemEventArgs
    {
        public long Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
