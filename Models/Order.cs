using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManger.Models
{
    public class Order : INotifyPropertyChanged
    {
        private User _user;
        private Paper _paper;
        private string _paperSize;
        private int _paperAmount;
        private DateTime _orderDate = DateTime.Now;

        public int Id { get; set; }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public Paper Paper
        {
            get { return _paper; }
            set
            {
                _paper = value;
                OnPropertyChanged(nameof(Paper));
            }
        }
        public int PaperAmount
        {
            get { return _paperAmount; }
            set
            {
                _paperAmount = value;
                OnPropertyChanged(nameof(PaperAmount));
            }
        }
        public string PaperSize
        {
            get { return _paperSize; }
            set
            {
                _paperSize = value;
                OnPropertyChanged(nameof(PaperSize));
            }
        }
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set
            {
                _orderDate = value;
                OnPropertyChanged(nameof(OrderDate));
            }
        }

        public Order() {  }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
