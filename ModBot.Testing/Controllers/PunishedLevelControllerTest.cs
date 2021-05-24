using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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


        [TestMethod]
        public async Task GetAllPunishedLevels_ShouldReturnOk()
        {
            //Arrange

            //Act

            //Assert
        }


        [TestMethod]
        public async Task GetAllPunishedLevels_ShouldReturnNotFound()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task GetAllPunishedLevels_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
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
            //Arrange

            //Act

            //Assert
        }
        [TestMethod]
        public async Task GetPunishedLevel_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
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
