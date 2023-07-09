using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SJIDON_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DealershipApp.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Dealership> Dealerships { get; set; }
        public RestCollection<Brand> Brands { get; set; }
        public RestCollection<Car> Cars { get; set; }

        public RestService GetCarWhereMoreThan18Employees_;
        public RestService GetCarWhereBrandOwnerIsBMWGroup_;
        public RestService GetDealershipWhereCar313hp_;
        public RestService GetDealershipWhereCarModelIsCharger_;
        public RestService GetDealershipWherePriceIs209700_;


        private Dealership selectedDealership;
        private Brand selectedBrand;
        private Car selectedCar;


        public Dealership SelectedDealership
        {
            get { return selectedDealership; }
            set
            {
                if (value != null)
                {
                    selectedDealership = new Dealership()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Employees = value.Employees

                    };
                    OnPropertyChanged();
                    (DeleteDealership as RelayCommand).NotifyCanExecuteChanged();
                    //(UpdateAirport as RelayCommand).NotifyCanExecuteChanged();
                }


            }
        }

        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (value != null)
                {
                    selectedBrand = new Brand()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Owner = value.Owner,
                        Dealership_Id = value.Dealership_Id

                    };
                    OnPropertyChanged();
                    (DeleteBrand as RelayCommand).NotifyCanExecuteChanged();
                    //(UpdateAirport as RelayCommand).NotifyCanExecuteChanged();
                }


            }
        }

        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                if (value != null)
                {
                    selectedCar = new Car()
                    {
                        Id = value.Id,
                        Model = value.Model,
                        Horsepower = value.Horsepower,
                        Price = value.Price,
                        Brand_Id = value.Brand_Id

                    };
                    OnPropertyChanged();
                    (DeleteCar as RelayCommand).NotifyCanExecuteChanged();
                    //(UpdatePassanger as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }



        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ICommand CreateDealership { get; set; }
        public ICommand UpdateDealership { get; set; }
        public ICommand DeleteDealership { get; set; }

        public ICommand CreateBrand { get; set; }
        public ICommand UpdateBrand { get; set; }
        public ICommand DeleteBrand { get; set; }

        public ICommand CreateCar { get; set; }
        public ICommand UpdateCar { get; set; }
        public ICommand DeleteCar { get; set; }


        public List<Car> GetCarWhereMoreThan18Employees
        {
            get { return GetCarWhereMoreThan18Employees_.Get<Car>("stat/GetCarWhereMoreThan18Employees"); }
        }
        public List<Car> GetCarWhereBrandOwnerIsBMWGroup
        {
            get { return GetCarWhereBrandOwnerIsBMWGroup_.Get<Car>("stat/GetCarWhereBrandOwnerIsBMWGroup"); }
        }
        public List<Dealership> GetDealershipWhereCar313hp
        {
            get { return GetDealershipWhereCar313hp_.Get<Dealership>("stat/GetDealershipWhereCar313hp"); }
        }
        public List<Dealership> GetDealershipWhereCarModelIsCharger
        {
            get { return GetDealershipWhereCarModelIsCharger_.Get<Dealership>("stat/GetDealershipWhereCarModelIsCharger"); }
        }
        public List<Dealership> GetDealershipWherePriceIs209700
        {
            get { return GetDealershipWherePriceIs209700_.Get<Dealership>("stat/GetDealershipWherePriceIs209700"); }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {

                GetCarWhereMoreThan18Employees_ = new RestService("http://localhost:12969/", "stat/GetCarWhereMoreThan18Employees");
                GetCarWhereBrandOwnerIsBMWGroup_ = new RestService("http://localhost:12969/", "stat/GetCarWhereBrandOwnerIsBMWGroup");
                GetDealershipWhereCar313hp_ = new RestService("http://localhost:12969/", "stat/GetDealershipWhereCar313hp");
                GetDealershipWhereCarModelIsCharger_ = new RestService("http://localhost:12969/", "stat/GetDealershipWhereCarModelIsCharger");
                GetDealershipWherePriceIs209700_ = new RestService("http://localhost:12969/", "stat/GetDealershipWherePriceIs209700");

                Dealerships = new RestCollection<Dealership>("http://localhost:12969/", "dealership", "hub");
                CreateDealership = new RelayCommand(() =>
                {
                    Dealerships.Add(new Dealership()
                    {

                        Name = SelectedDealership.Name,
                        Employees = SelectedDealership.Employees

                    });
                });
                UpdateDealership = new RelayCommand(() => Dealerships.Update(SelectedDealership));
                DeleteDealership = new RelayCommand(() => Dealerships.Delete(SelectedDealership.Id), () => SelectedDealership != null);
                SelectedDealership = new Dealership();

                //---------------------------------------------------------------
                Brands = new RestCollection<Brand>("http://localhost:12969/", "brand", "hub");
                CreateBrand = new RelayCommand(() =>
                {
                    Brands.Add(new Brand()
                    {


                        Name = SelectedBrand.Name,
                        Owner = SelectedBrand.Owner,
                        Dealership_Id = SelectedBrand.Dealership_Id

                    });
                });
                UpdateBrand = new RelayCommand(() => Brands.Update(SelectedBrand));
                DeleteBrand = new RelayCommand(() => Brands.Delete(SelectedBrand.Id), () => SelectedBrand != null);
                SelectedBrand = new Brand();

                //---------------------------------------------------------------
                Cars = new RestCollection<Car>("http://localhost:12969/", "car", "hub");
                CreateCar = new RelayCommand(() =>
                {
                    Cars.Add(new Car()
                    {

                        Model = SelectedCar.Model,
                        Horsepower = SelectedCar.Horsepower,
                        Price = SelectedCar.Price,
                        Brand_Id = SelectedCar.Brand_Id

                    });
                });
                UpdateCar = new RelayCommand(() => Cars.Update(SelectedCar));
                DeleteCar = new RelayCommand(() => Cars.Delete(SelectedCar.Id), () => SelectedCar != null);
                SelectedCar = new Car();


            }
        }
    }
}
