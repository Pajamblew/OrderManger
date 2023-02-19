using Microsoft.EntityFrameworkCore;
using OrderManger.Command;
using OrderManger.DbContexts;
using OrderManger.Models;
using OrderManger.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManger.ViewModels
{
    public class OrderViewModel
    {
        private readonly string[] PAPER_SIZES = { "A0", "A1", "A2", "A3", "A4", "A5" };

        public OrderManagerDbContext db = OrderManagerDbContext.getInstance();
        public RelayCommand? userCommand;
        public RelayCommand? paperCommand;
        public RelayCommand? addCommand;
        public RelayCommand? editCommand;
        public RelayCommand? deleteCommand;
        public ObservableCollection<Order> Orders { get; set; }

        public OrderViewModel()
        {
            db.Database.EnsureCreated();
            db.Users.Load();
            db.Papers.Load();
            db.Orders.Load();
            Orders = db.Orders.Local.ToObservableCollection();

        }

        public RelayCommand UserCommand
        {
            get
            {
                return userCommand ??
                    (userCommand = new RelayCommand((o) =>
                    {
                        UserView userView = new UserView();
                        if (userView.ShowDialog() == true)
                        {

                        }
                    }));
            }
        }
        public RelayCommand PaperCommand
        {
            get
            {
                return paperCommand ??
                    (paperCommand = new RelayCommand((o) =>
                    {
                        PaperView paperView = new PaperView();


                        if (paperView.ShowDialog() == true)
                        {

                        }
                    }));
            }
        }
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand((o) =>
                    {
                        AddOrderView addOrderView = new AddOrderView(new Order());
                        addOrderView.usersComboBox.ItemsSource = db.Users.ToList();
                        addOrderView.papersComboBox.ItemsSource = db.Papers.ToList();

                        addOrderView.paperSizesComboBox.ItemsSource = PAPER_SIZES;

                        if (addOrderView.ShowDialog() == true)
                        {
                            Order? order = addOrderView.Order;

                            order.User = addOrderView.usersComboBox.SelectedItem as User;
                            order.Paper = addOrderView.papersComboBox.SelectedItem as Paper;

                            order.User.orderCount++;
                            order.Paper.orderCount++;

                            db.Orders.Add(order);
                            db.SaveChanges();
                        }
                    }));
            }
        }
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand((selectedItem) =>
                    {
                        Order? order = selectedItem as Order;
                        if (order == null) return;

                        Order orderViewModel = new Order()
                        {
                            Id = order.Id,
                            User = order.User,
                            Paper = order.Paper,
                            PaperAmount = order.PaperAmount,
                            PaperSize = order.PaperSize,
                            OrderDate = order.OrderDate
                        };

                        EditOrderView editOrderView = new EditOrderView(orderViewModel);
  
                        editOrderView.paperSizesComboBox.ItemsSource = PAPER_SIZES;

                        if (editOrderView.ShowDialog() == true)
                        {
                            order.Id = editOrderView.Order.Id;
                            order.PaperAmount = editOrderView.Order.PaperAmount;
                            order.PaperSize = editOrderView.Order.PaperSize;
                            order.OrderDate = editOrderView.Order.OrderDate;

                            db.Entry(order).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand((selectedItem) =>
                    {
                        Order? order = selectedItem as Order;

                        order.User.orderCount--;
                        order.Paper.orderCount--;

                        if (order == null) return;

                        db.Orders.Remove(order);
                        db.SaveChanges();
                    }));
            }
        }
    }
}
