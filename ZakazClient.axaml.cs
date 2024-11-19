using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo1111.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demo1111;

public partial class ZakazClient : Window
{
    public List<ZakazProd>zakazProds  = Helper.mycontext.ZakazProds.Where(x => x.IdZakaz == Helper.zakaz.IdZakaz)
               .Include(x => x.IdZakazNavigation)
               .Include(x => x.IdProdNavigation).ToList();
    public ZakazClient()
    {
        InitializeComponent();
        updateKorz();
    }

    private void updateKorz()
    {
        zakazProds = Helper.mycontext.ZakazProds.Where(x => x.IdZakaz == Helper.zakaz.IdZakaz)
               .Include(x => x.IdZakazNavigation)
               .Include(x => x.IdProdNavigation).ToList();
        var list = zakazProds;
        listBox.ItemsSource = list.ToList();
    }

    private void ListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        var prod = listBox.SelectedItem as ZakazProd;
        if (prod != null)
        {
            Helper.mycontext.ZakazProds.Remove(prod);
            Helper.mycontext.SaveChanges();
            Helper.zakaz.ZakazProds.Remove(prod);
            updateKorz();
        }
       
        //ZakazProd zakazPr = Helper.mycontext.ZakazProds.Where(x => x.IdProd == prod.IdProd).LastOrDefault();
        
        
    }

    private void min_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int ind = Convert.ToInt32(((sender as Button)!).Tag!);
        var prod = Helper.mycontext.Prods.FirstOrDefault(x => x.IdProd == ind);
        ZakazProd zakazPr = Helper.zakaz.ZakazProds.FirstOrDefault(x => x.IdProd == ind && x.IdZakaz == Helper.zakaz.IdZakaz);
        zakazPr.Amount -= 1;
        
        if (zakazPr.Amount == 0)
        {
            Helper.zakaz.ZakazProds.Remove(zakazPr);
            Helper.mycontext.ZakazProds.Remove(zakazPr);
            Helper.mycontext.SaveChanges();
        }
        else
        {
            Helper.mycontext.ZakazProds.Where(x => x.IdProd == prod.IdProd).FirstOrDefault().Amount = zakazPr.Amount;
            Helper.mycontext.SaveChanges();
        }
        /* if (Helper.zakaz.ZakazPrs.Count() == 0)
         {
             Helper.user11019Context.ZakazPrs.Remove(zakazPr);
             Helper.user11019Context.SaveChanges();
         }*/

        updateKorz();
    }

    private void plus_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int ind = Convert.ToInt32(((sender as Button)!).Tag!);
        var prod = Helper.mycontext.Prods.FirstOrDefault(x => x.IdProd == ind);
        ZakazProd zakazPr = Helper.zakaz.ZakazProds.FirstOrDefault(x => x.IdProd == ind && x.IdZakaz == Helper.zakaz.IdZakaz);
        if (zakazPr.Amount.Value < Helper.mycontext.Prods.Where(x => x.IdProd == zakazPr.IdProd).FirstOrDefault().QuanSkl)
        {
            zakazPr.Amount += 1;
        }
        else
        {
        }
        Helper.mycontext.ZakazProds.Where(x => x.IdProd == zakazPr.IdProd).FirstOrDefault().Amount = zakazPr.Amount;
        Helper.mycontext.SaveChanges();
        updateKorz();
    }

    private void vyhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Catalog catalog = new Catalog();
        catalog.Show();
        this.Close();
    }
}