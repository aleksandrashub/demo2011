using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo1111.Models;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace demo1111;

public partial class Catalog : Window
{
    public List<Prod> prods = Helper.mycontext.Prods.Include(x => x.IdManufNavigation).ToList();
    public Catalog()
    {
        InitializeComponent();
        updateList();
        if (Helper.role == 1)
        {
            showZakazs.IsVisible = true;
            addBtn.IsVisible = true;
        }
        else if (Helper.role==2)
        {
            showZakazs.IsVisible = false;
            addBtn.IsVisible = false;
        }
        else
        {
            showZakazs.IsVisible = false;
            addBtn.IsVisible = false;
        }
        if (Helper.isGuest == false)
        {
            fioClient.Text = Helper.currUser.Surname + " " + Helper.currUser.Name + " " + Helper.currUser.Lastname;
        }
        if (Helper.zakaz != null)
        {
            if (Helper.zakaz.ZakazProds.Count() > 0)
            {
                toZakazBtn.IsVisible = true;
            }
            else
            {
                toZakazBtn.IsVisible = false;
            }
        }
        
       
    }

    public void updateList()
    {
        var currProds = prods;

        switch (sort.SelectedIndex)
        {
            case 0:
                currProds = prods.OrderBy(x => x.ItogCost).ToList();
                break;
            case 1:
                currProds = prods.OrderByDescending(x => x.ItogCost).ToList();
                break;
            case 2:
            default:
                break;
        }

        switch (filter.SelectedIndex)
        {
            case 0:
                currProds = prods.Where(x => x.CurrDisc < 10).ToList();
                break;
            case 1:
                currProds = prods.Where(x => x.CurrDisc < 15 && x.CurrDisc >= 10).ToList();
                break;

            case 2:
                currProds = prods.Where(x => x.CurrDisc >= 15).ToList();
                break;
            case 3:
            default:
                break;

        }

        string searchText = poisk.Text ?? "";
        int count = searchText.Split(' ').Length;
        searchText = searchText.ToLower();
        string[] values = new string[count];
        values = searchText.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

       if (!string.IsNullOrEmpty(searchText))
            {
                currProds = prods.Where(x => x.NameProd.ToLower().Contains(searchText)).ToList();

            }
            
        
        listbox.ItemsSource = currProds;
    }

    private void Sort_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        updateList();
    }
    private void Filter_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        updateList();
    }

    private void Poisk_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        updateList();
    }

    private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var product = listbox.SelectedItem as Prod;
        ZakazProd zakazPr = new ZakazProd();
        if (Helper.zakaz.ZakazProds.Count == 0)
        {
            if (Helper.mycontext.Zakazs.Count() == 0)
            {
                Helper.zakaz.IdZakaz = 1;
            }
            else
            {
                Helper.zakaz.IdZakaz = Helper.mycontext.Zakazs.OrderBy(x => x.IdZakaz).LastOrDefault().IdZakaz + 1;
            }

            Helper.mycontext.Zakazs.Add(Helper.zakaz);
            Helper.mycontext.SaveChanges();
        }
        
        zakazPr.IdZakazNavigation = Helper.zakaz;
        zakazPr.IdZakaz = Helper.zakaz.IdZakaz;
        zakazPr.IdProdNavigation = product;
        zakazPr.IdProd = product.IdProd;
        zakazPr.Amount = 1;
        Helper.zakaz.ZakazProds.Add(zakazPr);
        Helper.mycontext.ZakazProds.Add(zakazPr);
        // Helper.user11019Context.ZakazPrs.Add(zakazPr);
        Helper.mycontext.SaveChanges();
        //Helper.user11019Context.SaveChanges();
        Helper.prodsZakaz.Add(product);
        if (Helper.zakaz.ZakazProds.Count() > 0)
        {
            toZakazBtn.IsVisible = true;
        }
        else
        {
            toZakazBtn.IsVisible = false;
        }
    }

    private void ZakazClient_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ZakazClient zakazClient = new ZakazClient();
        zakazClient.Show();
        this.Close();
    
    }
        private void vyhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
        Helper.prodsZakaz.Clear();
        if (Helper.zakaz != null)
        {
            Helper.zakaz.ZakazProds.Clear();
            if (Helper.mycontext.Zakazs.Contains(Helper.zakaz))
            {
                Helper.mycontext.ZakazProds.RemoveRange(Helper.zakaz.ZakazProds);
                Helper.mycontext.Zakazs.Remove(Helper.zakaz);
            }

            Helper.mycontext.SaveChanges();

        }
        
        Helper.zakaz=new Zakaz();
        Helper.currUser = null;
        Helper.role = -1;
    }

    private void AddNew_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        AddEditProd addEditProd = new AddEditProd();
        addEditProd.Show();
        this.Close();

    }

 
    private void showZakazs_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ShowZakazs showZakazs = new ShowZakazs();
        showZakazs.Show();
        this.Close();
    }
}
