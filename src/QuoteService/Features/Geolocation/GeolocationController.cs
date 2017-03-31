using MediatR;
using QuoteService.Security;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using static QuoteService.Features.Geolocation.GetDistanceQuery;

namespace QuoteService.Features.Geolocation
{
    [RoutePrefix("api/geolocation")]
    public class GeolocationController: ApiController
    {
        public GeolocationController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("getDistance")]
        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(GetDistanceResponse))]
        public async Task<IHttpActionResult> GetDistance([FromBody]GetDistanceRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        private IMediator _mediator;
        private IUserManager _userManager;
    }
}
