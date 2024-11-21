using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demo1111.Models;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace demo1111;

public partial class AddEditProd : Window
{
    public string path;
    public string destPath;
    public Bitmap bitmap;
    public string filename;
    public Prod product = null;

    public List<string> edizmBind = Helper.mycontext.EdIzms.Select(x => x.EdIzm1).ToList();
    public List<string> categ = Helper.mycontext.Categories.Select(x => x.Categ).ToList();
    public List<string> manuf = Helper.mycontext.Manufacturers.Select(x => x.Manuf).ToList();
    public List<string> custs = Helper.mycontext.Customers.Select(x => x.Customer1).ToList();
    public AddEditProd()
    {
        InitializeComponent();
        edizmCmb.ItemsSource = edizmBind;
        manufCmb.ItemsSource = manuf;
        custCmb.ItemsSource = custs;
        categCmb.ItemsSource = categ;
    }
    public AddEditProd(Prod prod)
    {
        InitializeComponent();
        edizmCmb.ItemsSource = edizmBind;
        imgOut.Source = prod.bitmap;
        NameTxt.Text = prod.NameProd;
        descrTxt.Text = prod.Descr;
        manufCmb.ItemsSource = manuf;
        custCmb.ItemsSource = custs;
        categCmb.ItemsSource = categ;
        manufCmb.SelectedItem = prod.IdManufNavigation.Manuf;
        custCmb.SelectedItem = prod.IdCustNavigation.Customer1;
        categCmb.SelectedItem = prod.IdCategNavigation.Categ;
        articulTxt.Text = prod.Articul;
        edizmCmb.SelectedItem = prod.IdEdIzmNavigation.EdIzm1;
        costTxt.Text = prod.Cost.ToString();
        maxdiscTxt.Text = prod.MaxDisc.ToString();
        currdiscTxt.Text = prod.CurrDisc.ToString();
        quanTxt.Text = prod.QuanSkl.ToString();
            product = prod;
    }

    private async void img_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        var res = await openFileDialog.ShowAsync(this);
        if (res == null) return;
        path = string.Join("", res);
        if (res != null)
        {
            bitmap = new Bitmap(path);
        }
        imgOut.Source = bitmap;
        filename = System.IO.Path.GetFileName(path);
        destPath = (AppDomain.CurrentDomain.BaseDirectory + "Assets").ToString();
    }

    private void Ok_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        bool check = true;
        switch (product)
        {
            case null:
                product = new Prod();
                product.IdProd = Helper.mycontext.Prods.OrderBy(x => x.IdProd).LastOrDefault().IdProd;
                product.IdProd += 1;
                if (filename != null)
                {
                    product.Image = filename;
                    File.Move(path, destPath+ "\\" + filename);
                }
                else
                {
                    product.Image = "picture_zagl.png";
                }
                break;
            case object:
                
                if (filename != null)
                {
                    File.Move(path, destPath + "\\" + filename);
                    product.Image = filename;
                }
                else
                {
                    product.Image = "picture_zagl.png";
                }


                break;
        }
       
        if (NameTxt.Text.Length > 0)
        {
            product.NameProd = NameTxt.Text;
        }
        else
        {
            check = false;

            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле наименования обязательно для заполнения");
            msg.ShowAsync();
        }
        if (manufCmb.SelectedIndex != -1)
        {
            product.IdManufNavigation = Helper.mycontext.Manufacturers.Where(x => x.Manuf == manufCmb.SelectedValue.ToString()).FirstOrDefault();
            product.IdManuf = Helper.mycontext.Manufacturers.Where(x => x.Manuf == manufCmb.SelectedValue.ToString()).FirstOrDefault().IdManuf;


        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите производителя");
            msg.ShowAsync();
        }
            if (custCmb.SelectedIndex != -1)
            {
                product.IdCustNavigation = Helper.mycontext.Customers.Where(x => x.Customer1 == custCmb.SelectedValue.ToString()).FirstOrDefault();
                product.IdCust = Helper.mycontext.Customers.Where(x => x.Customer1 == custCmb.SelectedValue.ToString()).FirstOrDefault().IdCustomer;


            }
            else
            {
                check = false;
                var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите поставщика");
                msg.ShowAsync();
            }
            if (categCmb.SelectedIndex != -1)
            {
                product.IdCategNavigation = Helper.mycontext.Categories.Where(x => x.Categ == categCmb.SelectedValue.ToString()).FirstOrDefault();
                product.IdCateg = Helper.mycontext.Categories.Where(x => x.Categ == categCmb.SelectedValue.ToString()).FirstOrDefault().IdCateg;

            }
            else
            {
                check = false;
                var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите категорию");
                msg.ShowAsync();
            }
        int quan;
        if (quanTxt.Text != null && Int32.TryParse(quanTxt.Text, out quan))
        {
            product.QuanSkl = quan;
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Проверьте поле количества на складе");
            msg.ShowAsync();
        }
        float maxD;
        if (maxdiscTxt.Text != null && float.TryParse(maxdiscTxt.Text, out maxD))
        {
            product.MaxDisc = maxD;
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Проверьте поле максимальной скидки");
            msg.ShowAsync();
        }
        float currD;
        if (currdiscTxt.Text != null && float.TryParse(currdiscTxt.Text, out currD))
        {
            product.CurrDisc = currD;
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Проверьте поле текущей скидки");
            msg.ShowAsync();

        }
        int cost;
        if (costTxt.Text != null && Int32.TryParse(costTxt.Text, out cost))
        {
            product.Cost = cost;
        }
        else
        {
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Проверьте поле стоимости");
            msg.ShowAsync();
            check = false;
        }
        if (descrTxt.Text != null)
        {
            product.Descr = descrTxt.Text;

        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Проверьте поле описания");
            msg.ShowAsync();
        }
        if (articulTxt.Text != null)
        {
            product.Articul = articulTxt.Text;
        }
        else
        {
            check = false;
            var msg = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Проверьте поле описания");
            msg.ShowAsync();
        }


        if (check)
        {

            Helper.mycontext.Prods.Add(product);
            Helper.mycontext.SaveChanges();
            Catalog catalog = new Catalog();
            catalog.Show();
            this.Close();
        
        
        }
    }

    private void vyhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Catalog catalog = new Catalog();
        catalog.Show();
        this.Close();
    }
}


