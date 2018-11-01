using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Routes;
using TodoList.Contracts.Api.Services;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;

namespace TodoList.Api.Controllers
{
    [RoutePrefixController(ApiVersion.V1, nameof(ItemsController))]
    [Route("")]
    public class ItemsController : ApiController
    {
        private readonly IItemRepository _repository;
        private readonly ITodoListUrlHelper _urlHelper;

        public ItemsController(IItemRepository itemRepository, ITodoListUrlHelper urlHelper)
        {
            _repository = itemRepository;
            _urlHelper = urlHelper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAllAsync());

        [Route("{id}", Name = RouteNames.GetItemName)]
        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await _repository.GetAsync(id));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            var result = await _repository.AddAsync(item);
            var link = _urlHelper.Link(result.Id);

            return Created(link, result);
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => Ok(await _repository.EditAsync(item));

        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}