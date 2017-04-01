using QuoteService.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static QuoteService.Features.Quotes.AddOrUpdateQuoteCommand;
using static QuoteService.Features.Quotes.GetQuotesQuery;
using static QuoteService.Features.Quotes.GetQuoteByIdQuery;
using static QuoteService.Features.Quotes.RemoveQuoteCommand;
using static QuoteService.Features.Quotes.CalculateQuoteCommand;

namespace QuoteService.Features.Quotes
{
    [Authorize]
    [RoutePrefix("api/quote")]
    public class QuoteController : ApiController
    {
        public QuoteController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateQuoteResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateQuoteRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("calculate")]
        [HttpPost]
        [ResponseType(typeof(CalculateQuoteResponse))]
        public async Task<IHttpActionResult> Calculate(CalculateQuoteRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateQuoteResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateQuoteRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetQuotesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetQuotesRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetQuoteByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetQuoteByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveQuoteResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveQuoteRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
