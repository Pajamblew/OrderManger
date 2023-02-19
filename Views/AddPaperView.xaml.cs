using OrderManger.Models;
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
using System.Windows.Shapes;

namespace OrderManger.Views
{
    /// <summary>
    /// Interaction logic for AddPaperView.xaml
    /// </summary>
    public partial class AddPaperView : Window
    {
        public Paper Paper { get; private set; }

        public AddPaperView(Paper paper)
        {
            InitializeComponent();
            Paper = paper;
            DataContext = Paper;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
