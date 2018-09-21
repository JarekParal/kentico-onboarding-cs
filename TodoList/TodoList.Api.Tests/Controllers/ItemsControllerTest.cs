using System.Collections;
using System.Collections.Generic;
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
        [Test]
        public void Get_WithoutParams_ReturnsAllItems()
        {
            var controller = new ItemsController();

            IHttpActionResult actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<Item[]>;

            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Empty);
        }

        [Test]
        public void Get_WithValidIdParams_ReturnsOneItem()
        {
            var controller = new ItemsController();

            IHttpActionResult actionResult = controller.GetItem(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Item>;

            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content.Id, Is.EqualTo(1));
            Assert.That(contentResult.Content.Text, Is.EqualTo("Dog"));
        }

        [Test]
        public void Get_WithInvalidIdParams_ReturnsNoItem()
        {
            var controller = new ItemsController();

            IHttpActionResult actionResult = controller.GetItem(5);

            Assert.That(actionResult, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void Post_AddOneItem_ReturnsTheItem()
        {
            var controller = new ItemsController();

            IHttpActionResult actionResult = controller.Post(new Item { Text = "CatDog" });
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<Item>;

            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content.Text, Is.EqualTo("CatDog"));
        }

        [Test]
        public void Delete_OneExistingItem_ReturnsStatusCodeNoContent()
        {
            var controller = new ItemsController();

            IHttpActionResult actionResult = controller.Delete(1);
            var contentResult = actionResult as StatusCodeResult;

            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public void Delete_NoExistingItem_ReturnsStatusCodeNotFound()
        {
            var controller = new ItemsController();

            IHttpActionResult actionResult = controller.Delete(5);

            Assert.That(actionResult, Is.InstanceOf(typeof(NotFoundResult)));

        }
    }
}