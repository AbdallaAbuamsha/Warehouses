﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.UI.Events
{
    public class AfterDetailDeletedEvent : PubSubEvent<AfterDetailDeletedEventArgs>
    {
    }
    public class AfterDetailDeletedEventArgs
    {
        public long Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
