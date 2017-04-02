using MediatR;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using static QuoteService.Features.Locations.AddOrUpdateLocationCommand;
using static QuoteService.Features.Locations.GetLocationsQuery;
using static QuoteService.Features.Locations.GetLocationByIdQuery;
using static QuoteService.Features.Locations.RemoveLocationCommand;

namespace QuoteService.Features.Locations
{
    [Authorize]
    [RoutePrefix("api/location")]
    public class LocationController : ApiController
    {
        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateLocationResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateLocationRequest request)
        {
            request.TenantUniqueId = new Guid($"{Request.GetOwinContext().Environment["Tenant"]}");
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateLocationResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateLocationRequest request)
        {
            request.TenantUniqueId = new Guid($"{Request.GetOwinContext().Environment["Tenant"]}");
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetLocationsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetLocationsRequest();
            request.TenantUniqueId = new Guid($"{Request.GetOwinContext().Environment["Tenant"]}");
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetLocationByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetLocationByIdRequest request)
        {
            request.TenantUniqueId = new Guid($"{Request.GetOwinContext().Environment["Tenant"]}");
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveLocationResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveLocationRequest request)
        {
            request.TenantUniqueId = new Guid($"{Request.GetOwinContext().Environment["Tenant"]}");
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
