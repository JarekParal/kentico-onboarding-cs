using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Routes;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;

namespace TodoList.Api.Controllers
{
    [RoutePrefixController(ApiVersion.V1, nameof(ItemsController))]
    [Route("")]
    public class ItemsController : ApiController
    {
        private const string _getItemRouteName = "GetItem";

        private readonly IItemRepository _repository;

        public ItemsController(IItemRepository itemRepository)
        {
            _repository = itemRepository;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAllAsync());

        [Route("{id}", Name = _getItemRouteName)]
        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await _repository.GetAsync(id));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            var result = await _repository.AddAsync(item);
            return CreatedAtRoute(_getItemRouteName, new {id = result.Id}, result);
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