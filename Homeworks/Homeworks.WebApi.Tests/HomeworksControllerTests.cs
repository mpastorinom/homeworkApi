using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        [TestMethod]
        public void CreateNewHomeworkTest()
        {
            Homework fakeHomework = GetFakeHomework();
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.Add(It.IsAny<Entities.Homework>()))
                .Returns(Homework.ToEntity(fakeHomework));
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Post(fakeHomework);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<Homework>;

            mockHomeworksLogic.VerifyAll();
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(fakeHomework.Id, createdResult.RouteValues["id"]);
            Assert.AreEqual(fakeHomework, createdResult.Content);
        }

        [TestMethod]
        public void CreateNullHomeworkTest()
        {
            Homework fakeHomework = null;
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.Add(It.IsAny<Entities.Homework>()))
                .Throws(new ArgumentNullException());
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Post(fakeHomework);
            var createdResult = obtainedResult as CreatedAtRouteNegotiatedContentResult<Homework>;

            mockHomeworksLogic.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void UpdateExistingHomeworkOkTest()
        {
            Homework fakeHomework = GetFakeHomework();
            var expectedResult = true;
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.Update(It.IsAny<Guid>(), It.IsAny<Entities.Homework>()))
                .Returns(expectedResult);
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Put(Guid.NewGuid(), fakeHomework);

            mockHomeworksLogic.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateHomeworkErrorUnexistingTest()
        {
            Homework fakeHomework = GetFakeHomework();
            var expectedResult = false;
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.Update(It.IsAny<Guid>(), It.IsAny<Entities.Homework>()))
                .Returns(expectedResult);
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Put(Guid.NewGuid(), fakeHomework);

            mockHomeworksLogic.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteHomeworkOkTest()
        {
            Guid fakeGuid = Guid.NewGuid();
            var expectedResult = true;
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.DeleteById(It.IsAny<Guid>()))
                .Returns(expectedResult);
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Delete(fakeGuid);

            mockHomeworksLogic.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteHomeworkErrorNotFoundTest()
        {
            Guid fakeGuid = Guid.NewGuid();
            var expectedResult = false;
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.DeleteById(It.IsAny<Guid>()))
                .Returns(expectedResult);
            var controller = new HomeworksController(mockHomeworksLogic.Object);

            IHttpActionResult obtainedResult = controller.Delete(fakeGuid);

            mockHomeworksLogic.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllRestrictedHomeworksOkTest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "DefaultUri");
            request.Headers.Add("Authorization", "admin");

            var expectedHomeworks = GetFakeHomeworks();
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            mockHomeworksLogic
                .Setup(wl => wl.GetAll())
                .Returns(Homework.ToEntity(expectedHomeworks));
            var controller = new HomeworksController(mockHomeworksLogic.Object);
            controller.Request = request;

            IHttpActionResult obtainedResult = controller.GetAllRestrictedHomeworks();
            var contentResult = obtainedResult as OkNegotiatedContentResult<IEnumerable<Homework>>;

            mockHomeworksLogic.VerifyAll();
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(expectedHomeworks.SequenceEqual(contentResult.Content));
        }

        [TestMethod]
        public void GetAllRestrictedHomeworksErrorUnauthorizedTest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "DefaultUri");

            var expectedHomeworks = GetFakeHomeworks();
            var mockHomeworksLogic = new Mock<IHomeworksLogic>();
            var controller = new HomeworksController(mockHomeworksLogic.Object);
            controller.Request = request;

            IHttpActionResult obtainedResult = controller.GetAllRestrictedHomeworks();
            var contentResult = obtainedResult as OkNegotiatedContentResult<IEnumerable<Homework>>;

            mockHomeworksLogic.VerifyAll();
            Assert.IsInstanceOfType(obtainedResult, typeof(UnauthorizedResult));
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
