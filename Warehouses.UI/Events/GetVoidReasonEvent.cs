using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.UI.Events
{
    public class GetVoidReasonEvent : PubSubEvent<string>
    {
        internal void Subscripe(object onGetVoidReason)
        {
            throw new NotImplementedException();
        }
    }
}
