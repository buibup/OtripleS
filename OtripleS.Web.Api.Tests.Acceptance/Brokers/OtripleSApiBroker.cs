using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;
using System.Net.Http;

namespace OtripleS.Web.Api.Tests.Acceptance.Brokers
{
    public class OtripleSApiBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiFactoryClient;

        public OtripleSApiBroker()
        {
            webApplicationFactory = new WebApplicationFactory<Startup>();
            httpClient = webApplicationFactory.CreateClient();
            apiFactoryClient = new RESTFulApiFactoryClient(httpClient);
        }
    }
}
