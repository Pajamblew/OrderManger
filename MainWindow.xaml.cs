using OrderManger.DbContexts;
using OrderManger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrderManger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OrderManagerDbContext db = OrderManagerDbContext.getInstance();
        public MainWindow()
        {
            InitializeComponent();

            db.Database.EnsureCreated();
            DataContext = new OrderViewModel();
        }
    }
}
