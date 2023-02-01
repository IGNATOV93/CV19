using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CV19.Services;

namespace CV19
{
   public partial class App : Application
    {
        public static bool IsDesingMode { get; private set; }=true;
        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesingMode = false;
            base.OnStartup(e);
           
        }
    }
}
