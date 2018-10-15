using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Routes;
using TodoList.Contracts.Models;
using TodoList.Repository;

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
            => Ok(await Task.FromResult(_repository.GetAll()));

        [Route("{id}", Name = _getItemRouteName)]
        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await Task.FromResult(_repository.Get(id)));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
            => await Task.FromResult(CreatedAtRoute(_getItemRouteName, new { id = item.Id }, item));

        [Route("{id}")]
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => await Task.FromResult(Ok(_repository.Get(id)));


        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            _repository.Delete(id);
            return StatusCode(await Task.FromResult(HttpStatusCode.NoContent));
        }
    }
}