using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Models;

namespace TodoList.Api.Tests.Controllers
{
    [TestFixture]
    public class ItemsControllerTest
    {
        private ItemsController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new ItemsController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async Task GetAsync_WithoutParams_ReturnsAllItems()
        {
            var actionResult = await _controller.GetAsync();
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;
            contentResult.TryGetContentValue<Item[]>(out var items);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(items, Is.Not.Empty);
        }

        [Test]
        public async Task GetAsync_WithValidId_ReturnsOneItem()
        {
            var actionResult = await _controller.GetAsync(1);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;
            contentResult.TryGetContentValue<Item>(out var item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item.Id, Is.EqualTo(1));
            Assert.That(item.Text, Is.EqualTo("Dog"));
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task GetAsync_WithInvalidId_ReturnsNoItem()
        {
            var actionResult = await _controller.GetAsync(1);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PostAsync_AddOneItem_ReturnsAddedItem()
        {
            var actionResult = await _controller.PostAsync(new Item { Text = "CatDog" });
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;
            contentResult.TryGetContentValue<Item>(out var item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item.Text, Is.EqualTo("CatDog"));
        }

        [Test]
        public async Task PutAsync_EditExistingItem_ReturnsStatusCodeOk()
        {
            var actionResult = await _controller.PutAsync(1, new Item { Id = 1, Text = "DogDog" });
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task PutAsync_AddNewItem_ReturnsStatusCodeCreated()
        {
            var actionResult = await _controller.PutAsync(5, new Item { Id = 5, Text = "CatDogCat" });
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task DeleteAsync_OneExistingItem_ReturnsStatusCodeNoContent()
        {
            var actionResult = await _controller.DeleteAsync(1);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task DeleteAsync_NoExistingItem_ReturnsStatusCodeNotFound()
        {
            var actionResult = await _controller.DeleteAsync(1);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}