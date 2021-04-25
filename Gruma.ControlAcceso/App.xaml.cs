using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Denso.ControlAcceso
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex mutex = null;
        bool created_new;
        const string mutex_name = "Control de Acceso";

        public App()
        {
            mutex = new Mutex(true, mutex_name, out created_new); if (!created_new)
            {
                MessageBox.Show("El programa ya se encuentra funcionando", "Control de Acceso a Proveedores",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                Current.Shutdown();
                return;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
