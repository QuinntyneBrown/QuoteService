using QuoteService.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static QuoteService.Features.Services.AddOrUpdateServiceCommand;
using static QuoteService.Features.Services.GetServicesQuery;
using static QuoteService.Features.Services.GetServiceByIdQuery;
using static QuoteService.Features.Services.RemoveServiceCommand;
using System.Net.Http;
using System;
using QuoteService.Features.Core;

namespace QuoteService.Features.Services
{
    [Authorize]
    [RoutePrefix("api/service")]
    public class ServiceController : ApiController
    {
        public ServiceController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateServiceResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateServiceResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetServicesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetServicesRequest();            
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetServiceByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetServiceByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveServiceResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
