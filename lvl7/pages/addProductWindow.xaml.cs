using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using lvl7.Models;
using Microsoft.Win32;

namespace lvl7.pages
{

    public partial class addProductWindow : Window
    {
        Product product;
        MainWindow mainWindow;

        public addProductWindow(Product pr, MainWindow w)
        {
            this.product = pr;
            DataContext = product;
            InitializeComponent();
            AddProductProductTypes.ItemsSource = EfModel.Init().ProductTypes.ToList();
            mainWindow = w;
            w.IsEnabled = false;

            this.Title = "Продукт";
        }

        // Обработка кнопки сохранения
        // Если id продукта = 0, добавляем новый продукт в бд
        // иначе редактируем уже существующий
        private void ProductSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (product.IdProduct == 0)
                EfModel.Init().Products.Add(product);
            EfModel.Save();

            if (product.IdProduct != 0)
                EfModel.Init().Entry(product).Reload();
            this.Close();

        }


        // Обработка закрытия окна
        // Делаем основное  меню активным и обновляем список продуктов
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
            mainWindow.update();
        }

        private void ProductPhoto_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog { Filter="Jpeg files|*.jpg|All Files|*.*"};
            if(fileDialog.ShowDialog() == true)
            {
                product.Photo = File.ReadAllBytes(fileDialog.FileName);
            }
        }
    }
}
