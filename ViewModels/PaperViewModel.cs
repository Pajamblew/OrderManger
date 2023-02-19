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
    public class PaperViewModel
    {
        OrderManagerDbContext db = OrderManagerDbContext.getInstance();
        public RelayCommand? addCommand;
        public RelayCommand? editCommand;
        public RelayCommand? deleteCommand;
        public ObservableCollection<Paper> Papers { get; set; }

        public PaperViewModel()
        {
            db.Database.EnsureCreated();
            db.Papers.Load();
            Papers = db.Papers.Local.ToObservableCollection();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand((o) =>
                    {
                        AddPaperView addPaperView = new AddPaperView(new Paper());
                        if (addPaperView.ShowDialog() == true)
                        {
                            Paper paper = addPaperView.Paper;
                            db.Papers.Add(paper);
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
                        Paper? paper = selectedItem as Paper;
                        if (paper == null) return;
                        if (paper.orderCount != 0)
                        {
                            MessageBox.Show("Этот тип бумаги используется в заказе");
                            return;
                        }


                        db.Papers.Remove(paper);
                        db.SaveChanges();
                    }));
            }
        }
    }
}
