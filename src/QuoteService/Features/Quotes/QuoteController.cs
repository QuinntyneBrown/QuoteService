using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static QuoteService.Features.Quotes.AddOrUpdateQuoteCommand;
using static QuoteService.Features.Quotes.GetQuotesQuery;
using static QuoteService.Features.Quotes.GetQuoteByIdQuery;
using static QuoteService.Features.Quotes.RemoveQuoteCommand;
using static QuoteService.Features.Quotes.CalculateQuoteCommand;
using QuoteService.Features.Core;

namespace QuoteService.Features.Quotes
{
    [Authorize]
    [RoutePrefix("api/quote")]
    public class QuoteController : ApiController
    {
        public QuoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("calculate")]
        [HttpPost]
        [ResponseType(typeof(CalculateQuoteResponse))]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Calculate(CalculateQuoteRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateQuoteResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateQuoteRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateQuoteResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateQuoteRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetQuotesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetQuotesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetQuoteByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetQuoteByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveQuoteResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveQuoteRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
