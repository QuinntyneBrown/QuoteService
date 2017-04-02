using MediatR;
using System.Threading.Tasks;
using System.Net.Http;

namespace QuoteService.Features.Geolocation
{
    public class GetLongLatCoordinatesQuery
    {
        public class GetLongLatCoordinatesRequest : IRequest<GetLongLatCoordinatesResponse>
        {
            public int? TenantId { get; set; }
            public string Address { get; set; }
        }

        public class GetLongLatCoordinatesResponse
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class GetLongLatCoordinatesHandler : IAsyncRequestHandler<GetLongLatCoordinatesRequest, GetLongLatCoordinatesResponse>
        {
            public GetLongLatCoordinatesHandler(HttpClient client)
            {
                _client = client;
            }

            public async Task<GetLongLatCoordinatesResponse> Handle(GetLongLatCoordinatesRequest request)
            {                
                var httpResponse = await _client.GetAsync($"http://maps.googleapis.com/maps/api/geocode/json?address={request.Address}&sensor=false");
                var googleResponse = await httpResponse.Content.ReadAsAsync<GetLongLatGoogleResponse>();

                return new GetLongLatCoordinatesResponse()
                {
                    Latitude = googleResponse.Latitude,
                    Longitude = googleResponse.Longitude
                };
            }
            private HttpClient _client;
        }
    }
}