using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces;
using WebApi.Controllers;
using WebApi.Services;
using WebApi.Repositories;

namespace UserPermissions.Test.WebApi
{
    [TestFixture]
    public class RequestPermissionTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IProducerService> _producerServiceMock;
        private Mock<ISearchRepository<Permission>> _searchRepositoryMock;
        private PermissionController _controller;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _producerServiceMock = new Mock<IProducerService>();
            _searchRepositoryMock = new Mock<ISearchRepository<Permission>>();
            _controller = new PermissionController(_unitOfWorkMock.Object, _producerServiceMock.Object, _searchRepositoryMock.Object);
        }

        [Test]
        public async Task GetPermissionsByEmployeeId_CallsExpectedMethods()
        {
            // Arrange
            var permissions = new List<Permission> { new Permission() };
            _unitOfWorkMock.Setup(u => u.Permissions.GetPermissions()).Returns(permissions);

            // Act
            var result = await _controller.GetPermissionsByEmployeeId();

            // Assert
            _unitOfWorkMock.Verify(u => u.Permissions.GetPermissions(), Times.Once);
            _producerServiceMock.Verify(p => p.Produce(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _searchRepositoryMock.Verify(s => s.Add(It.IsAny<IEnumerable<Permission>>()), Times.Once);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(permissions, okResult.Value);
        }

    }
}