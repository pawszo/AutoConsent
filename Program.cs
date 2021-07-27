using AutoConsent.Interface;
using AutoConsent.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConsent
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ServiceProvider.GetService<Controller>().Initialize();
        }

        private static readonly ServiceProvider ServiceProvider =
            new ServiceCollection()
            .AddTransient<Controller>()
            .AddSingleton<IInputService, OutlookService>()
            .AddSingleton<IFileService, ExcelService>()
            .AddSingleton<IRepository, MemoryDataService>()
            .BuildServiceProvider();
    }

}
