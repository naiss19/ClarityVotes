using System;
using Newtonsoft.Json;
using System.Linq;
using SODA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClearMaps.assets.Map
{

    

    public class MapController
    {

        private readonly ILogger<MapController> _logger;

        private const string MyTomTomKey = "o2cGGVGMV71PKdfZalwG8X9gB6qttIp0";

        public MapController(ILogger<MapController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    ViewData["MyTomTomKey"] = MyTomTomKey;

        //    return 
        //}

        internal partial class candidate
        {
            string firstName = "";
            string lastName = "";
            string emailAddress = "";
            string businessPhone = "";

            candidate(string firstName, string lastName, string email, string businessPhone)
            {
                this.firstName = firstName;
                this.lastName = lastName;
                this.emailAddress = email;
                this.businessPhone = businessPhone;
            }

            public candidate()
            {
                this.firstName = "";
                this.lastName = "";
                this.emailAddress = "";
                this.businessPhone = "";
            }
        }
        private partial class boundary
        {
            string coordinates = "";
            candidate c = new();
        }

        private static readonly string TomtomKey = "o2cGGVGMV71PKdfZalwG8X9gB6qttIp0";
        private static readonly string RealityVotesAPIKey = "7faqpn54kz72tz93mh2k8i2iz";
        private static readonly string RealityVotesSecretAPIKey = "fadh10jbyeqdm768sxci9gg1w2o85jdb0lvj3spga2f7q9tlj";
        private static readonly string RealityVotesAppToken = "n1BZ66hEVkARcFXnLK1thJvjI";
        private static readonly string RealityVotesSecretAppToken = "sYoIblXuQdb7wyqlY8Sm1F8mXan3cyZJxZvT";
        public MapController()
        {

        }


        public static async Task getBoundaries()
        {

            var client = new SodaClient("https://data.nashville.gov", RealityVotesAppToken);

            // Get a reference to the resource itself
            // The result (a Resource object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<object>("iw7r-m8qr");

            // Resource objects read their own data
            var rows = dataset.GetRows(limit: 5000);

            var boundaries = new List<boundary>();

            Console.WriteLine("Got {0} results. Dumping first results:", rows.Count());

            foreach (var keyValue in rows)
            {

                
                //var boundaryData = new
                //{
                   
                //}
            }
        }

        public async Task showMap()
        {
        }

        public async Task<IEnumerable<PolygonCoordinates>> GetPolygonsAsync()
        {
            

            using (HttpClient client = new HttpClient())
            {
                string url = "https://data.nashville.gov/resource/i3fu-jbj3.json";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var polygons = JsonConvert.DeserializeObject<IEnumerable<PolygonCoordinates>>(json);
                    return polygons;
                }
                else
                {
                    throw new Exception("Failed to fetch polygon data.");
                }
            }
        }
    }

    public partial class PolygonCoordinates
    {
        public List<List<List<double>>> Boundary { get; set; }
    }
}

