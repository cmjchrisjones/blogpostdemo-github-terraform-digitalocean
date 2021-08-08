using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRazorApp.Model
{
    public class WeatherServiceResponse
    { 
            public Location Location { get; set; }

            public Current Current { get; set; } 
    }
}
