using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_App_MVVM.Model;
using Weather_App_MVVM.ViewModel.Commands;
using Weather_App_MVVM.ViewModel.Helpers;

namespace Weather_App_MVVM.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string query;

        public string Query
        {
            get { return query; }
            set
            {

                query = value;
                OnPropertyChanged("Query");

            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {

                currentConditions = value;
                OnPropertyChanged("CurrentConditions");

            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;

                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }


        public SearchCommand SearchCommand { get; set; }


        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "New York"
                };

                CurrentConditions = new CurrentConditions()
                {
                    WeatherText = "Partly cloudly",
                    Temperature = new Temperature()
                    {
                        Metric = new Units()
                        {
                            Value = "21"
                        }
                    }
                };

            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();

        }

        public async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions =await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);

        }
        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();

            foreach (var city in cities)
            {
                Cities.Add(city);
            }

        }



        private void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

