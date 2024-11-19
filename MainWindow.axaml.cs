using Avalonia.Controls;
using demo1111.Models;
using Metsys.Bson;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo1111
{
    
    public partial class MainWindow : Window
    {
        public List<User> clients = Helper.mycontext.Users.Include(x => x.IdRoleNavigation).ToList();
        public bool access = false;

       
        public MainWindow()
        {
            InitializeComponent();
            /*  var a = Helper.user11019Context.ZakazPrs
               .Include(x=>x.IdZakazNavigation)
               .Include(x=>x.IdProdNavigation);*/
        }

        private void Guest_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Helper.isGuest = true;
            Helper.role = 2; //гость
            Catalog catalog = new Catalog();
            catalog.Show();
            this.Close();
        }

        private void vhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string loginStr = loginTb.Text;
            string passwdStr = paswdTb.Text;
            foreach (User client in clients)
            {
                if (client.Login == loginStr && client.Password == passwdStr)
                {
                    access = true;
                    Helper.currUser = client;
                    Helper.role = Convert.ToInt32(client.IdRole);
                    break;
                }
            }
            if (access)
            {
                Catalog catalog = new Catalog();
                catalog.Show();
                this.Close();
            }
            else
            {
                var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пользователь не найден");
                msg.ShowAsync();
            }

        }
    }
}