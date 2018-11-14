using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NSubstitute;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Tests.Extensions;
using TodoList.Contracts.Api.Services;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;

namespace TodoList.Api.Tests.Controllers
{
    [TestFixture]
    public class ItemsControllerTest
    {
        private ItemsController _controller;
        private IItemRepository _repository;
        private ITodoListUrlHelper _urlHelper;

        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737"), Text = "Dog"},
            new Item {Id = new Guid("BFA20109-F15E-4F5C-B395-2879E02BC422"), Text = "Cat"},
            new Item {Id = new Guid("4BAF698C-AF41-4AA1-8465-85C00073BD13"), Text = "Elephant"}
        };

        private static readonly Item s_catDog = new Item
            {Id = new Guid("00000000-0000-0000-0000-000000000000"), Text = "CatDog"};

        [OneTimeSetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();
            _urlHelper = Substitute.For<ITodoListUrlHelper>();

            _controller = new ItemsController(_repository, _urlHelper)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            _controller.Configuration.Routes.MapHttpRoute(
                "GetItem",
                "objects/{id}/CreatedPath",
                new {id = RouteParameter.Optional});
        }

        [Test]
        public async Task GetAsync_WithoutParams_ReturnsAllItems()
        {
            _repository.GetAllItemsAsync().Returns(s_items);

            var contentResult = await _controller
                .ExecuteAction(controller => controller.GetItemsAsync());
            contentResult.TryGetContentValue<Item[]>(out var items);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(items, Is.EqualTo(s_items).UsingItemComparer());
        }

        [Test]
        public async Task GetAsync_WithValidId_ReturnsOneItem()
        {
            var id = s_items[0].Id;
            _repository.GetItemAsync(id).Returns(s_items[0]);

            var contentResult = await _controller
                .ExecuteAction(controller => controller.GetItemAsync(id));
            contentResult.TryGetContentValue<Item>(out var item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(s_items[0]).UsingItemComparer());
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task GetAsync_WithInvalidId_ReturnsNoItem()
        {
            var id = new Guid("5E73F108-97F6-4FFF-A15C-1A7AEDE686BA");

            var contentResult = await _controller
                .ExecuteAction(controller => controller.GetItemAsync(id));

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PostAsync_AddOneItem_ReturnsAddedItem()
        {
            _repository.AddItemAsync(s_catDog).Returns(s_items[0]);
            var uriString = $"http://location/objects/{s_items[0].Id}/CreatedPath";
            _urlHelper.Link(Arg.Any<Guid>()).Returns(new Uri(uriString));

            var contentResult = await _controller
                .ExecuteAction(controller => controller.PostItemAsync(s_catDog));
            contentResult.TryGetContentValue<Item>(out var item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(contentResult.Headers.Location.ToString(),
                Is.EqualTo(uriString));
        }

        [Test]
        public async Task PutAsync_EditExistingItem_ReturnsStatusCodeOk()
        {
            var id = s_items[0].Id;
            var inputItem = new Item {Id = id, Text = "DogDog"};
            _repository.EditItemAsync(inputItem).Returns(s_items[0]);

            var contentResult = await _controller
                .ExecuteAction(controller => controller.PutItemAsync(id, inputItem));
            contentResult.TryGetContentValue<Item>(out var item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(s_items[0]));
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task PutAsync_AddNewItem_ReturnsStatusCodeCreated()
        {
            var id = new Guid("5E73F108-97F6-4FFF-A15C-1A7AEDE686BA");
            var inputItem = new Item {Id = id, Text = "CatDogCat"};

            var contentResult = await _controller
                .ExecuteAction(controller => controller.PutItemAsync(id, inputItem));

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task DeleteAsync_OneExistingItem_ReturnsStatusCodeNoContent()
        {
            var id = s_items[0].Id;

            var contentResult = await _controller
                .ExecuteAction(controller => controller.DeleteItemAsync(id));

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            await _repository.Received(1).DeleteItemAsync(id);
        }

        [Test]
        [Ignore("This test will be used in task 3 (CS) - after adding a database.")]
        public async Task DeleteAsync_NoExistingItem_ReturnsStatusCodeNotFound()
        {
            var id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737");

            var contentResult = await _controller
                .ExecuteAction(controller => controller.DeleteItemAsync(id));

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}