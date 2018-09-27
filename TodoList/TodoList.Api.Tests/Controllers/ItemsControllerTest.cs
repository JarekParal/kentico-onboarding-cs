using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
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
            var id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737");

            var actionResult = await _controller.GetAsync(id);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;
            contentResult.TryGetContentValue<Item>(out var item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item.Id, Is.EqualTo(id));
            Assert.That(item.Text, Is.EqualTo("Dog"));
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task GetAsync_WithInvalidId_ReturnsNoItem()
        {
            var id = new Guid("5E73F108-97F6-4FFF-A15C-1A7AEDE686BA");

            var actionResult = await _controller.GetAsync(id);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PostAsync_AddOneItem_ReturnsAddedItem()
        {
            var actionResult = await _controller.PostAsync(new Item {Text = "CatDog"});
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;
            contentResult.TryGetContentValue<Item>(out var item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item.Text, Is.EqualTo("CatDog"));
        }

        [Test]
        public async Task PutAsync_EditExistingItem_ReturnsStatusCodeOk()
        {
            var id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737");

            var actionResult = await _controller.PutAsync(id, new Item {Id = id, Text = "DogDog"});
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task PutAsync_AddNewItem_ReturnsStatusCodeCreated()
        {
            var id = new Guid("5E73F108-97F6-4FFF-A15C-1A7AEDE686BA");

            var actionResult = await _controller.PutAsync(id, new Item {Id = id, Text = "CatDogCat"});
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task DeleteAsync_OneExistingItem_ReturnsStatusCodeNoContent()
        {
            var id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737");

            var actionResult = await _controller.DeleteAsync(id);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task DeleteAsync_NoExistingItem_ReturnsStatusCodeNotFound()
        {
            var id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737");

            var actionResult = await _controller.DeleteAsync(id);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}