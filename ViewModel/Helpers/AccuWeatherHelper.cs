using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Weather_App_MVVM.Model;

namespace Weather_App_MVVM.ViewModel.Helpers
{
    class AccuWeatherHelper
    {
        public const string Base_Url = "http://dataservice.accuweather.com/";
        public const string AutoComplete_Endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string Current_Conditions_Endpoint = "currentconditions/v1/{0}?apikey={1}";
        public const string Api_Key = "enter your accuweather api key";



        public async static Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            string url = Base_Url + String.Format(AutoComplete_Endpoint, Api_Key, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                string json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }


            return cities;
        }


        public async static Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions conditions = new CurrentConditions();

            string url = Base_Url + String.Format(Current_Conditions_Endpoint, cityKey, Api_Key);


            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                string json = await response.Content.ReadAsStringAsync();

                conditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();
            }

            return conditions;

        }

    }


}
