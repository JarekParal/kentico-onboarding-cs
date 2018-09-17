using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Models;

namespace TodoList.Api.Tests.Controllers
{
    [TestFixture]
    public class ItemsControllerTest
    {
        [Test]
        public void Get__WithoutParams_ReturnsArrayOfItems()
        {
            var itemsController = new ItemsController();

            var result = itemsController.Get();

            Assert.That(result, Is.InstanceOf(typeof(IEnumerable<Item>)));
            Assert.That(result, Is.Not.Empty);
        }
    }
}