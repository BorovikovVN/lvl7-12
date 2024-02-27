using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using lvl7.Models;
using lvl7.pages;
using Microsoft.EntityFrameworkCore;

namespace lvl7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Добавляем критерии сортировки 
            cbSort.Items.Add("По убыванию цены");
            cbSort.Items.Add("По возрастанию цены");
            cbSort.Items.Add("По алфавиту(А-Я)");
            cbSort.Items.Add("По алфавиту(Я-А)");
            // Устанавливаем первоначальный критерий "По алфавиту(А-Я)"
            cbSort.SelectedIndex = 2;

            // Добавляем фильтрацию по типу продукта
            List<ProductType> productTypes = EfModel.Init().ProductTypes.ToList();
            productTypes.Insert(0, new ProductType { TypeName = "Все" });
            cbFiltr.ItemsSource = productTypes;
            cbFiltr.SelectedIndex = 0;

            // Устанавливаем ограничения на размер и заголовок окна
            this.MaxWidth = 830;
            this.MinWidth = 830;
            this.MaxHeight = 800;
            this.MinHeight = 800;
            this.Title = "Меню";
        }

        public void update()
        {
            // Создаем коллекцию продуктов, удовлетворяющих условиям поиска
            IEnumerable<Product> products = EfModel.Init().Products
                .Include(p => p.TypeProductNavigation)
                .Include(p => p.MaterialHasProducts)
                .ThenInclude(p => p.MaterialIdMaterialNavigation)
                .Where(p => p.NameProduct.Contains(SearchTextBox.Text)
                || p.TypeProductNavigation.TypeName.Contains(SearchTextBox.Text)
                || p.Article.Contains(SearchTextBox.Text));


            // Сортируем продукты по выбранному критерию
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case 1:
                    products = products.OrderBy(p => p.Price);
                    break;
                case 2:
                    products = products.OrderBy(p => p.NameProduct);
                    break;
                case 3:
                    products = products.OrderByDescending(p => p.NameProduct);
                    break;

            }

            // Фильтруем список продуктов. Если выбранный индекс > 0, то выводим только соответствующие критерию фильтрации продукты,
            // иначе выводим все
            if (cbFiltr.SelectedIndex > 0)
            {
                products = products.Where(p => p.TypeProduct == (cbFiltr.SelectedItem as ProductType).IdProductTypes);
            }


            MainListView.ItemsSource = products.ToList();

        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void cbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void DataTemplate_Selected(object sender, RoutedEventArgs e)
        {

        }

        // Обработка нажатия на кнопку добавления продукта
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            addProductWindow AddProductWindow = new addProductWindow(new Product(), this);
            AddProductWindow.Show();
        }

        // Обработка нажатия на кнопку редактирования продукта
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if(MainListView.SelectedItems.Count == 1) {
                Product product = MainListView.SelectedItem as Product;
                addProductWindow addP = new addProductWindow(product, this);
                addP.Show();
            }
        }
    }


}