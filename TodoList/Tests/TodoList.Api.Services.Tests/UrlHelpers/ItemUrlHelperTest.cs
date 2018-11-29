using System;
using System.Web.Http.Routing;
using System.Web.Routing;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using TodoList.Contracts.Api.Services;
using TodoList.Contracts.Routes;
using TodoList.Api.Services.UrlHelpers;

namespace TodoList.Api.Services.Tests.UrlHelpers
{
    [TestFixture]
    public class ItemUrlHelperTest
    {
        private ITodoListUrlHelper _itemUrlHelper;
        private UrlHelper _urlHelper;
        private IRouteNames _routeNames;

        private static readonly Guid s_id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737");

        [SetUp]
        public void SetUp()
        {
            _urlHelper = Substitute.For<UrlHelper>();
            _routeNames = Substitute.For<IRouteNames>();
            _itemUrlHelper = new ItemUrlHelper(_urlHelper, _routeNames);
        }

        [Test]
        public void Link_CorrectRouteAndId_CorrectReturn()
        {
            var uri = new Uri($"http://local/Item/{s_id}");
            _urlHelper
                .Link("Item", Arg.Is<object>(item => CheckTypeOfGuid(item, s_id)))
                .Returns(uri.AbsoluteUri);
            _routeNames.GetItem.Returns("Item");

            var result = _itemUrlHelper.Link(s_id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(uri));
        }

        private static bool CheckTypeOfGuid(object item, Guid id)
        {
            var itemId = new RouteValueDictionary(item)["id"];
            return itemId is Guid && itemId.Equals(id);
        }
    }
}