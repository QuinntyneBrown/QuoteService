using Google.Maps.DistanceMatrix;
using Google.Maps;
using MediatR;
using System.Threading.Tasks;

using static QuoteService.Features.Geolocation.GetLongLatCoordinatesQuery;

namespace QuoteService.Features.Geolocation
{
    public class GetDistanceQuery
    {
        public class GetDistanceRequest : IRequest<GetDistanceResponse>
        {
            public int? TenantId;
            public string Address1 { get; set; }
            public string Address2 { get; set; }
        }

        public class GetDistanceResponse
        {
            public string Distance { get; set; }
        }

        public class GetDistanceHandler : IAsyncRequestHandler<GetDistanceRequest, GetDistanceResponse>
        {
            public GetDistanceHandler(IMediator meditator)
            {
                _meditator = meditator;
            }

            public async Task<GetDistanceResponse> Handle(GetDistanceRequest request)
            {
                DistanceMatrixRequest distanceMatrixRequest = new DistanceMatrixRequest();

                var coordinate1 = await _meditator.Send(new GetLongLatCoordinatesRequest() { Address = request.Address1 });
                var coordinate2 = await _meditator.Send(new GetLongLatCoordinatesRequest() { Address = request.Address2 });
                
                distanceMatrixRequest.AddOrigin(new LatLng(coordinate1.Latitude, coordinate1.Latitude));
                distanceMatrixRequest.AddDestination(new LatLng(coordinate2.Latitude, coordinate2.Latitude));
                distanceMatrixRequest.Sensor = true;
                distanceMatrixRequest.Mode = TravelMode.driving;

                var response = new DistanceMatrixService().GetResponse(distanceMatrixRequest);

                return new GetDistanceResponse()
                {
                    Distance = response.Rows[0].Elements[0].distance.Value
                };
            }

            private IMediator _meditator;
        }
    }
}