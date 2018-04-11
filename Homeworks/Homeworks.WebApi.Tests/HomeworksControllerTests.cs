using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Homeworks.Logic;
using Homeworks.WebApi.Controllers;
using Homeworks.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Homeworks.WebApi.Tests
{
    [TestClass]
    public class HomeworksControllerTests
    {
        [TestMethod]
        public void GetAllHomeworksOkTest()
        {
            var expectedHomeworks = GetFakeHomeworks();
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.GetAll())
                .Returns(Homework.ToEntity(expectedHomeworks));
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Get();
            var contentResult = obtainedResult as OkNegotiatedContentResult<IEnumerable<Homework>>;

            mockHomeworksLogic.VerifyAll();
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(expectedHomeworks.SequenceEqual(contentResult.Content));
            //CollectionAssert.AreEqual(expectedHomeworks.ToList(), contentResult.Content.ToList());
        }

        [TestMethod]
        public void GetHomeworkOkTest()
        {
            var expectedHomework = GetFakeHomework();
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.GetById(It.IsAny<Guid>()))
                .Returns(Homework.ToEntity(expectedHomework));
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Get(expectedHomework.Id);
            var contentResult = obtainedResult as OkNegotiatedContentResult<Homework>;

            mockHomeworksLogic.VerifyAll();
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(expectedHomework, contentResult.Content);
        }

        [TestMethod]
        public void GetHomeworkErrorNotFoundTest()
        {
            Homework expectedHomework = null;
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.GetById(It.IsAny<Guid>()))
                .Returns(Homework.ToEntity(expectedHomework));
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Get(Guid.NewGuid());
            var contentResult = obtainedResult as OkNegotiatedContentResult<Homework>;

            mockHomeworksLogic.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(NotFoundResult));
        }

        private Homework GetFakeHomework()
        {
            return new Homework
            {
                Id = Guid.NewGuid(),
                Description = "Homework",
                DueDate = DateTime.Now.AddDays(1)
            };
        }

        private IEnumerable<Homework> GetFakeHomeworks()
        {
            return new List<Homework>
            {
                new Homework
                {
                    Id = Guid.NewGuid(),
                    Description = "Homework 1",
                    DueDate = DateTime.Now.AddDays(1)
                },
                new Homework
                {
                    Id = Guid.NewGuid(),
                    Description = "Homework 2",
                    DueDate = DateTime.Now.AddDays(3)
                }
            };
        }
    }
}
