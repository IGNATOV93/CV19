using System;
using System.Collections.Generic;

using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CV19.Models;

namespace CV19.Services
{
    internal class DataService
    {
        private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
       
        private static async Task<Stream> GetDataStream() //формирует поток данных в байтах
        {
            var client = new HttpClient();
            var response = await client.GetAsync(
                _DataSourceAddress,
                HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

        }


        private static IEnumerable<string> GetDataLines()//разбиывает поток на последовательность строк
        {
            var data_stream = GetDataStream().Result; 
            var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                yield return line.Replace("Korea,", "Korea -"). Replace("Bonaire,","Bonaire -");
            }
        }


        private static DateTime[] GetDates() => GetDataLines() //метод извлекает все даты
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Province,string Country,(double Lat,double Lon) Place, int[] Counts)> GetCOuntriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));
            foreach (var row in lines)
            {

                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var latitude = double.Parse(row[2]);
                var longitude = double.Parse(row[3]);
                var counts = row.Skip(4).Select(int.Parse).ToArray();
                yield return (province, country_name, (latitude, longitude), counts);
            }

        }

        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDates();
            var data = GetCOuntriesData().GroupBy(d => d.Country);
            foreach (var country_info in data)
            {
                
                var country = new CountryInfo
                {
                    Name = country_info.Key,
                    ProvinceCounts = country_info.Select((c=> new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat,c.Place.Lon),
                        Counts = dates.Zip(c.Counts,(date,count)=>new ConfirmedCount{Data= date,Count = count})
                    }))
                };
                yield return country;
            }
        }
    }
}
