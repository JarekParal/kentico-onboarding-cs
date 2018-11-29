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

        public async Task<IHttpActionResult> GetItemsAsync()
            => Ok(await _repository.GetAllItemsAsync());

        [Route("{id}", Name = _getItemRouteName)]
        public async Task<IHttpActionResult> GetItemAsync(Guid id)
            => Ok(await _repository.GetItemAsync(id));

        public async Task<IHttpActionResult> PostItemAsync([FromBody] Item item)
        {
            var result = await _repository.AddItemAsync(item);
            return CreatedAtRoute(_getItemRouteName, new { id = result.Id }, result);
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> PutItemAsync(Guid id, [FromBody] Item item)
            => Ok(await _repository.EditItemAsync(item));

        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteItemAsync(Guid id)
        {
            await _repository.DeleteItemAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}