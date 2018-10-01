using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Models;
using TodoList.Api.Tests.Utils;

namespace TodoList.Api.Tests.Controllers
{
    [TestFixture]
    public class ItemsControllerTest
    {
        private ItemsController _controller;
        private ItemCompare _compare;

        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737"), Text = "Dog"},
            new Item {Id = new Guid("BFA20109-F15E-4F5C-B395-2879E02BC422"), Text = "Cat"},
            new Item {Id = new Guid("4BAF698C-AF41-4AA1-8465-85C00073BD13"), Text = "Elephant"}
        };

        private static readonly Item s_catdog = new Item { Id = new Guid("00000000-0000-0000-0000-000000000000"), Text = "CatDog" };

        [SetUp]
        public void SetUp()
        {
            _controller = new ItemsController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            _compare = new ItemCompare();
        }

        [Test]
        public async Task GetAsync_WithoutParams_ReturnsAllItems()
        {
            var actionResult = await _controller.GetAsync();
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;
            contentResult.TryGetContentValue<Item[]>(out var items);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(items, Is.Not.Empty);
            Assert.That(items, Is.EqualTo(s_items).Using(_compare));
        }

        [Test]
        public async Task GetAsync_WithValidId_ReturnsOneItem()
        {
            var id = s_items[0].Id;

            var actionResult = await _controller.GetAsync(id);
            var contentResult = actionResult.ExecuteAsync(CancellationToken.None).Result;
            contentResult.TryGetContentValue<Item>(out var item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(s_items[0]).Using(_compare));
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
            Assert.That(item, Is.EqualTo(s_catdog).Using(_compare)); 
        }

        [Test]
        public async Task PutAsync_EditExistingItem_ReturnsStatusCodeOk()
        {
            var id = s_items[0].Id;

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
            var id = s_items[0].Id;

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