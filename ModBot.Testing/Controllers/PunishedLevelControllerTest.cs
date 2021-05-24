using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModBot.Testing.Controllers
{
    [TestClass]
    public class PunishedLevelControllerTest
    {
        private readonly Mock<IPunishmentsLevelsService> _mockPunish;
        private PunishedLevelsController punishedLevelsController;
        public PunishedLevelControllerTest()
        {
          _mockPunish = new Mock<IPunishmentsLevelsService>();
          punishedLevelsController = new PunishedLevelsController(_mockPunish.Object);
        }


        private List<IPunishmentsLevels> _punishmentsList = new List<IPunishmentsLevels>
        {
            new PunishmentsLevels(1, 1, 2, 3, default(DateTime), default(DateTime))
        };


        [TestMethod]
        public async Task GetAllPunishedLevels_ShouldReturnOk()
        {
            //Arrange
            _mockPunish.Setup(x => x.GetAllPunishmentLevels()).ReturnsAsync(_punishmentsList);
            //Act
            var response = await punishedLevelsController.GetPunishedLevels();
            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var punishmentsLevel = result.Value.Should().BeOfType<List<IPunishmentsLevels>>().Subject;
            punishmentsLevel.Count().Should().Be(1);
        }



        [TestMethod]
        public async Task GetAllPunishedLevels_ShouldReturnNotFound()
        {
            //Arrange
            IEnumerable<IPunishmentsLevels> punishmentLevelsListIsEmpty = new List<IPunishmentsLevels>();
            _mockPunish.Setup(x => x.GetAllPunishmentLevels()).ReturnsAsync(punishmentLevelsListIsEmpty);
            //Act
            var response = await punishedLevelsController.GetPunishedLevels();
            //Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public async Task GetAllPunishedLevels_ShouldReturnInternalServerError()
        {
            //Arrange
                IEnumerable<IPunishmentsLevels> punishmentLevelsIsNull = null;
             _mockPunish.Setup(x => x.GetAllPunishmentLevels()).ReturnsAsync(punishmentLevelsIsNull);
            //Act
            var response = await punishedLevelsController.GetPunishedLevels();
            //Assert
            var result = response.Should().BeOfType<ObjectResult>().Subject;
            result.Value.Should().Be("internal server error");
        }

        [TestMethod]
        public async Task GetPunishedLevel_ShouldReturnOk()
        {
            //Arrange
            var id = 1;
            _mockPunish.Setup(x => x.GetPunishmentLevel(It.IsAny<int>())).ReturnsAsync(new PunishmentsLevels(1, 2, 3, 1, default(DateTime), default(DateTime)));
            //Act
            var response = await punishedLevelsController.GetPunishedLevel(id);
            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var okValue = result.Value.Should().BeOfType<PunishmentsLevels>().Subject;
            okValue.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task GetPunishedLevel_ShouldReturnNotFound()
        {
            //Arrangep
            IPunishmentsLevels punishedLevelIsNull = null;
            _mockPunish.Setup(x => x.GetPunishmentLevel(It.IsAny<int>())).ReturnsAsync(punishedLevelIsNull);
            //Act
            var response = await punishedLevelsController.GetPunishedLevel(1);
            //Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public async Task GetPunishedLevel_ShouldReturnInternalServerError()
        {
            //Arrange
            IPunishmentsLevels punishmentLevelIsNull = null;
            _mockPunish.Setup(x => x.GetPunishmentLevel(It.IsAny<int>())).ReturnsAsync(punishmentLevelIsNull);
            //Act
            var response = await punishedLevelsController.GetPunishedLevel(1);
            //Assert
            var result = response.Should().BeOfType<ObjectResult>().Subject;
            result.Value.Should().Be("internal server error");
        }

        [TestMethod]
        public async Task GetPunishedLevel_ShouldReturnBadRequest()
        {
            //Arrange

            //Act
            var response = await punishedLevelsController.GetPunishedLevel(0);
            //Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [TestMethod]
        public async Task CreatePunishedLevel_ShouldReturnNoContent()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task CreatePunishedLevel_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task UpdatePunishedLevel_ShouldReturnNoContent()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task UpdatePunishedLevel_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
        }


    }
}
