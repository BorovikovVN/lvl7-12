using System.Configuration;
using System.Data;
using System.Windows;

namespace lvl7
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        // Отлавливаем фатальные ошибки
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            // Выводим сообщение об ошибке
            MessageBox.Show("Фатальная ошибка");
            e.Handled = true;
        }
    }

}
