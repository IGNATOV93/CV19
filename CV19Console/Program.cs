using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CV19Console
{
    public class Progtam
    { 
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        public static void Main()
        {
            //var web_client = new WebClient();
            
            var client = new HttpClient();
            // var items = new string[10];
            //var last_item = items[^1];
            var response= client.GetAsync(data_url).Result;
            var csv_str = response.Content.ReadAsStringAsync().Result;
            Console.ReadLine();

        }
    }
}
