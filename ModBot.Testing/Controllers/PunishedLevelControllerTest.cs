using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.DTO;
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
        private readonly Mock<ILoggerManager> logger;
        public PunishedLevelControllerTest()
        {
            logger = new Mock<ILoggerManager>();
          _mockPunish = new Mock<IPunishmentsLevelsService>();
          punishedLevelsController = new PunishedLevelsController(_mockPunish.Object, logger.Object);
        }


        private List<IPunishmentsLevels> _punishmentsList = new List<IPunishmentsLevels>
        {
            new PunishmentSettings(1, 1, 2, 3, default, default)
        };

        private PunishmentSettingsDto punishmentDto = new PunishmentSettingsDto()
        {
            Id = 1,
            TimeOutLevel = 1,
            BanLevel = 2,
            KickLevel = 1,
            SpamMuteTime = 1,
            StrikeMuteTime = 1,
            GuildId = 12312312321
        };

        private PunishmentSettings punishment = new PunishmentSettings(1,2,3,4,5, 12312312321);
       








        [TestMethod]
        public async Task GetPunishedLevel_ShouldReturnNotFound()
        {
            //Arrangep
            IPunishmentsLevels punishedLevelIsNull = null;
            _mockPunish.Setup(x => x.GetPunishmentLevels(It.IsAny<ulong>())).ReturnsAsync(punishedLevelIsNull);
            //Act
            var response = await punishedLevelsController.GetPunishmentLevel(12123123123, 1);
            //Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        //[TestMethod]
        //public async Task GetPunishedLevel_ShouldReturnBadRequest()
        //{
        //    //Arrange

        //    //Act
        //    var response = await punishedLevelsController.GetPunishmentLevels(123123213);
        //    //Assert
        //    response.Should().BeOfType<BadRequestObjectResult>();
        //}

        [TestMethod]
        public void CreatePunishedLevel_ShouldReturnNoContent()
        {
            //Arrange
            var createPunishment = new PunishmentSettingsDto()
            {
                TimeOutLevel = 1,
                KickLevel = 1,
                BanLevel = 1, 
                SpamMuteTime = 1,
                StrikeMuteTime = 2
            };

            _mockPunish.Setup(x => x.CreatePunishmentLevel(It.IsAny<PunishmentSettingsDto>())).Returns(true);

            //Act
            var response =  punishedLevelsController.CreatePunishmentLevel(createPunishment);

            //Assert
            var result = response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public void CreatePunishedLevel_ShouldReturnBadRequestIfLevelIsNotCreated()
        {
            //Arrange
            var createdPunishment = new PunishmentSettingsDto()
            {
                TimeOutLevel = 1
            };
            _mockPunish.Setup(x => x.CreatePunishmentLevel(It.IsAny<PunishmentSettingsDto>())).Returns(false);

            //Act
            var response = punishedLevelsController.CreatePunishmentLevel(createdPunishment);

            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("PunishedLevel was not created");
        }

        [TestMethod]
        public void CreatePunishedLevel_ShouldReturnBadRequestWhenParameterIsNull()
        {
            //Arrange
            _mockPunish.Setup(x => x.CreatePunishmentLevel(It.IsAny<PunishmentSettingsDto>()));

            //Act
            var response =  punishedLevelsController.CreatePunishmentLevel(null);

            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Parameters is null");
        }

        [TestMethod]
        public async Task UpdatePunishedLevel_ShouldReturnNoContent()
        {
            //Arrange
            var updatePunishment = new PunishmentSettingsDto()
            {
                TimeOutLevel = 1
            };
            _mockPunish.Setup(x => x.UpdatePunishmentLevel(It.IsAny<PunishmentSettingsDto>(), It.IsAny<int>())).ReturnsAsync(true);
            //Act
            var response = await punishedLevelsController.UpdatePunishmentLevel(1, updatePunishment);
            //Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task UpdatePunishedLevel_ShouldReturnBadRequestIfDtoIsNull()
        {
            //Arrange
            //Act
            var response = await punishedLevelsController.UpdatePunishmentLevel(1, null);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("object is null");
        }


        [TestMethod]
        public async Task UpdatePunishedLevel_ShouldReturnBadRequest()
        {
            //Arrange
            var updatePunishment = new PunishmentSettingsDto()
            {
                TimeOutLevel = 1
            };
            _mockPunish.Setup(x => x.UpdatePunishmentLevel(It.IsAny<PunishmentSettingsDto>(), It.IsAny<int>())).ReturnsAsync(false);
            //Act
            var response = await punishedLevelsController.UpdatePunishmentLevel(1, updatePunishment);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Punishmentlevels was not updated");
        }

        [TestMethod]
        public async Task DeletePunishedLevel_ShouldReturnNoContentWhenSuccessfull()
        {
            //Arrange
            int id = 1;
            _mockPunish.Setup(x => x.DeletePunishemntLevel(It.IsAny<PunishmentSettingsDto>())).ReturnsAsync(true); 
            //Act
            var response = await punishedLevelsController.DeletePunishmentLevel(punishmentDto);
            //Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task DeletePunishedLevel_ShouldReturnBadRequest()
        {
            //Arrange 
            //Act
            var response = await punishedLevelsController.DeletePunishmentLevel(punishmentDto);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("PunishedLevel was not created");
        }

        [TestMethod]
        public async Task DeletePunishedLevel_ShouldReturnBadRequestIfParameterIsZero()
        {
            //Arrange
            _mockPunish.Setup(x => x.DeletePunishemntLevel(It.IsAny<PunishmentSettingsDto>())).ReturnsAsync(false);
            //Act
            var response = await punishedLevelsController.DeletePunishmentLevel(punishmentDto);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("PunishedLevel was not created");
        }

    }
}
