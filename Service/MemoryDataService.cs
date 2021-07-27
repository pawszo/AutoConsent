using AutoConsent.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConsent.Service
{
    public class MemoryDataService : IRepository
    {
        public Dictionary<string, object> Items { get; } = new Dictionary<string, object>();

    }
}
