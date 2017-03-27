using PostNL.LocationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace PostNL.Provider
{
    public class PostNLServiceProvider
    {
        public GetLocationsResponse GetNearestLocations(string postCode)
        {
            var client = LocationWebServiceClient();

            //call web service method
            GetNearestLocationsRequest request = new GetNearestLocationsRequest();

            var location = new Location();
            location.AllowSundaySorting = "true";
            location.DeliveryDate = DateTime.Now.Date.AddDays(4).ToString("dd-MM-yyyy");    //"30-04-2017";
            location.DeliveryOptions = new string[] { "PGE", "PG" };
            location.Options = new string[] { "Daytime" };
            location.Postalcode = postCode; 

            var message = new Message();
            message.MessageID = "2";
            message.MessageTimeStamp = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");

            request.Countrycode = "NL";
            request.Location = location;
            request.Message = message;

            var respose = client.GetNearestLocations(request);

            return respose;
        }


        public GetLocationsResponse GetLocationByCode(string locationCode, string networkId)
        {
            var client = LocationWebServiceClient();


            //call web service method
            GetLocationRequest request = new GetLocationRequest();

            request.LocationCode = locationCode;
            request.RetailNetworkID = networkId;

            var message = new Message();
            message.MessageID = "2";
            message.MessageTimeStamp = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");

            request.Message = message;

            var respose = client.GetLocation(request);
            return respose;
        }

        protected LocationWebServiceClient LocationWebServiceClient()
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["PostNL_WebserviceURL"];

            var uri = new Uri(url);

            var remoteAddress = new System.ServiceModel.EndpointAddress(uri);

            var client = new LocationWebServiceClient(new System.ServiceModel.BasicHttpsBinding(), remoteAddress);

            //set timeout
            client.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 0, 20);

            //add the custom header with username and password
            client.Endpoint.EndpointBehaviors.Add(new PostNLLCCustomEndpointBehavior());

            return client;
        }
    }
}
