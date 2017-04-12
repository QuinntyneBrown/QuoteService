using MediatR;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace QuoteService.Features.Geolocation
{
    public class GetTimeZoneQuery
    {        
        public class GetTimeZoneRequest : IRequest<GetTimeZoneResponse>
        {
            public Guid TenantUniqueId { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class GetTimeZoneResponse {
            public string TimeZoneName { get; set;}
        }

        public class GetTimezoneHandler : IAsyncRequestHandler<GetTimeZoneRequest, GetTimeZoneResponse>
        {
            public GetTimezoneHandler(HttpClient client, Lazy<IGoogleApiConfiguration> lazyGoogleApiConfiguration)
            {
                _client = client;
                _configuration = lazyGoogleApiConfiguration.Value;
            }

            public async Task<GetTimeZoneResponse> Handle(GetTimeZoneRequest request)
            {
                var timestamp = GetTimestamp();
                var httpResponse = await _client.GetAsync($"https://maps.googleapis.com/maps/api/timezone/json?location={request.Latitude},{request.Longitude}&timestamp={timestamp}&key={_configuration.ApiKey}");
                return await httpResponse.Content.ReadAsAsync<GetTimeZoneResponse>(); ;             
            }

            public double GetTimestamp()
            {
                DateTime date = DateTime.UtcNow;
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan diff = date.ToUniversalTime() - origin;
                return Math.Floor(diff.TotalSeconds);
            }

            protected readonly HttpClient _client;
            protected readonly IGoogleApiConfiguration _configuration;
        }
    }
}