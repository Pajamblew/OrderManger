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
using System.Windows;

namespace OrderManger.ViewModels
{
    public class UserViewModel
    {
        OrderManagerDbContext db = OrderManagerDbContext.getInstance();
        public RelayCommand? addCommand;
        public RelayCommand? editCommand;
        public RelayCommand? deleteCommand;
        public ObservableCollection<User> Users { get; set; }




        public UserViewModel()
        {
            db.Database.EnsureCreated();
            db.Users.Load();
            Users = db.Users.Local.ToObservableCollection();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand((o) =>
                    {
                        AddUserView addUserView = new AddUserView(new User());
                        if (addUserView.ShowDialog() == true)
                        {
                            User user = addUserView.User;
                            db.Users.Add(user);
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
                        User? user = selectedItem as User;
                        if (user == null) return;
                        if (user.orderCount != 0)
                        {
                            MessageBox.Show("Этот пользователь имеет заказы");
                            return;
                        }


                        db.Users.Remove(user);
                        db.SaveChanges();
                    }));
            }
        }

    }
}
