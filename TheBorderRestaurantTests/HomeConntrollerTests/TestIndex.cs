﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBorderRestaurant.Controllers;
using Xunit;

namespace TheBorderRestaurantTests.HomeConntrollerTests
{
    public class TestIndex
    {
        [Fact]
        public void TestReturnsView()
        {
            var uow = TestLibrary.TestLibrary.SetUpIUnitOfWork();
            var controller = new HomeController(uow.Object);
            var result = controller.Index() as ViewResult;
            Assert.IsType<ViewResult>(result);

        }
    }
}
