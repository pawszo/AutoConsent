using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConsent.Interface
{
    public interface IRepository
    {
        Dictionary<string, object> Items { get; }
    }
}
