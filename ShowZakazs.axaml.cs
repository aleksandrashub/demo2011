using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo1111.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace demo1111;

public partial class ShowZakazs : Window
{
    public List<Zakaz> zakazs = Helper.mycontext.Zakazs.Include(x => x.IdClientNavigation).Include(x => x.ZakazProds).ToList();
    public ShowZakazs()
    {
        InitializeComponent();
        updateList();
    }
    private void updateList()
    {
        zakazs = Helper.mycontext.Zakazs.Include(x => x.ZakazProds).Include(x => x.IdClientNavigation).ToList();
        var list = zakazs;
        listZ.ItemsSource = list.ToList();
    }

    private void vyhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Catalog catalog = new Catalog();
        catalog.Show();
        this.Close();
    }
}