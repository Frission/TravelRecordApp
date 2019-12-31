using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TravelRecordApp.Managers
{
    public class VenueManager
    {
        public async static Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            List<Venue> venues = new List<Venue>();

            string url = VenueInfos.GenerateURL(latitude, longitude);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueInfo = JsonConvert.DeserializeObject<VenueInfos>(json);

                venues = venueInfo.response.venues as List<Venue>;
            }

            return venues;
        }
    }
}
