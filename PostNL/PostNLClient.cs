using PostNL.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostNL
{
    class PostNLClient
    {
        static void Main(string[] args)
        {
            var postNLProvider  = new PostNLServiceProvider();
            var result = postNLProvider.GetNearestLocations("2132WT");

            Console.WriteLine("Available locations");
            Console.WriteLine("-------------------");

            foreach(var location in result.GetLocationsResult)
            {
                Console.WriteLine("{0} \t - Phone Mumber : {1}", location.Name, location.PhoneNumber);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
