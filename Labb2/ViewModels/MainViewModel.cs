using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb2.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb2.ViewModels;

public class MainViewModel : ObservableObject
{

    private ObservableCollection<Butiker> _butiker = new ();
    private ObservableCollection<Böcker> _böcker = new();
    private ObservableCollection<Böcker> _allaböcker = new();
    private Butiker _selectedButik;
    private Böcker _selectedBok;
    private LagerSaldo _lagersaldo;

    public ObservableCollection<Butiker> Butiker
        {
        get { return _butiker; }
        set { SetProperty(_butiker, value, (v) => _butiker = v); }
        }

    public ObservableCollection<Böcker> AllaBöcker
    {
        get { return _allaböcker; }
        set
        {
            SetProperty(ref _allaböcker, value);
        }
    }

    public ObservableCollection<Böcker> Böcker => _böcker;


    public Butiker SelectedButik
    {

        get { return _selectedButik; }
        set
        {
            SetProperty( ref _selectedButik, value);
            GetBöcker();
        }

        }

    public Böcker SelectedBok
    {
        get { return _selectedBok;}
set
{
    SetProperty(ref _selectedBok, value);
                              if(_selectedBok is null) return;
                              GetLagerSaldo();
}
    }

    public LagerSaldo Lagersaldo
        {
        get { return _lagersaldo; }
        set
            {
            SetProperty(ref _lagersaldo, value);
            }
        }

    public IRelayCommand RemoveBookCommand { get; }
    public IRelayCommand AddBookCommand { get; }


    public void RemoveBook()
    {
        using var context = new BokhandelContext();

        var selected = context.LagerSaldo.Find(SelectedButik.ButiksId, SelectedBok.Isbn13);

        var result = context.LagerSaldo.Remove(selected);

        context.SaveChanges(); 
GetBöcker();

    }

    public void AddBook()
    {

        using var context = new BokhandelContext();
        var butik = context.Butiker.Find(SelectedButik.ButiksId);

                var lagersaldo = new LagerSaldo { ButiksId = SelectedButik.ButiksId, Isbn = SelectedBok.Isbn13};

                butik.LagerSaldos.Add(lagersaldo);
                context.SaveChanges();

                GetBöcker();

            }

    public void GetBöcker()
    {
        using var context = new BokhandelContext();

        var böcker = context.LagerSaldo.Include(ls => ls.IsbnNavigation).Include(ls => ls.Butiks)
            .Where(ls => ls.ButiksId == SelectedButik.ButiksId).Select(ls => ls.IsbnNavigation).ToList();

        Böcker.Clear();
        foreach (var bok in böcker)
        {
            Böcker.Add(bok);
        }
    }


    public void GetButiker()
    {
        using var context = new BokhandelContext();
        var butiker = context.Butiker;
        Butiker = new ObservableCollection<Butiker>(butiker);

        }

    public void GetAllaBöcker()
    {
        using var context = new BokhandelContext();
        var allaböcker = context.Böcker;
        AllaBöcker = new ObservableCollection<Böcker>(allaböcker);
    }


    public void GetLagerSaldo()
    {
        using var context = new BokhandelContext();
        var antal = context.LagerSaldo.Include(x => x.IsbnNavigation).FirstOrDefault(x =>
            x.ButiksId.Equals(SelectedButik.ButiksId) && x.Isbn.Equals(SelectedBok.Isbn13));

        Lagersaldo = antal;

        }

    public MainViewModel()
    {
        GetButiker();
        SelectedButik = Butiker.FirstOrDefault();
        GetBöcker();
        GetAllaBöcker();

        RemoveBookCommand = new RelayCommand(() => RemoveBook());
        AddBookCommand = new RelayCommand(() => AddBook());


        }
    }

