using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.Business.Services;
using ModBot.DAL.Repository;
using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Testing.Services
{
    [TestClass]
    public class PunishedLevelServiceTest
    {
        private readonly Mock<DatabaseRepository> _mockRepo;
        private readonly PunishmentsLevelsService _punishedLevelService;

        public PunishedLevelServiceTest()
        {
            _mockRepo = new Mock<DatabaseRepository>();
            _punishedLevelService = new PunishmentsLevelsService(_mockRepo.Object);
        }

        private readonly PunishmentDto punishmentDto = new PunishmentDto()
        {
            BanLevel = 1,
            KickLevel = 2,
            TimeOutLevel = 3,
            SpamMuteTime = 1,
            StrikeMuteTime = 2
        };
        private readonly IPunishmentsLevels punishmentLevls = new PunishmentSettings(1,2, 3, default, default);

        private List<PunishmentDto> PunishmentLevelsList = new List<PunishmentDto>();

        [TestMethod]
        public void CreatePunishMent_ShouldReturnTrue()
        {
            //Arrange
            _mockRepo.Setup(x => x.CreatePunishment(It.IsAny<IPunishmentsLevels>())).Returns(true);
            //Act
            var response = _punishedLevelService.CreatePunishmentLevel(punishmentDto);

            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public void CreatePunishmentLevel_ShouldReturnFalse()
        {
            //Arrange
            _mockRepo.Setup(x => x.CreatePunishment(It.IsAny<IPunishmentsLevels>())).Returns(false);
            //Act
            var response = _punishedLevelService.CreatePunishmentLevel(punishmentDto);

            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public async Task GetPunishmentLevel_shouldReturnABannedWordWithCOrrectWord()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetPunishment(It.IsAny<int>())).ReturnsAsync(punishmentLevls);
            //Act
            var response = await _punishedLevelService.GetPunishmentLevel(1);

            //Assert
            response.BanLevel.Should().Be(3);
        }

        [TestMethod]
        public async Task GetPunishmentLevel_shouldReturnNullIfNoBannedWordExists()
        {
            //Arrange
            IPunishmentsLevels punishmentLevels = null;
            _mockRepo.Setup(x => x.GetPunishment(It.IsAny<int>())).ReturnsAsync(punishmentLevels);
            //Act
            var response = await _punishedLevelService.GetPunishmentLevel(1);

            //Assert
            response.Should().BeNull();
        }

        [TestMethod]
        public async Task DeletePunishmentLevel_ShouldReturnTrue()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetPunishment(It.IsAny<int>())).ReturnsAsync(punishmentLevls);
            _mockRepo.Setup(x => x.DeletePunishment(It.IsAny<IPunishmentsLevels>())).Returns(true);
            //Act
            var response = await _punishedLevelService.DeletePunishemntLevel(1);
            //Assert
            _mockRepo.Verify(x => x.GetPunishment(It.IsAny<int>()), Times.Once);
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task DeleteBannedWord_ShouldReturnfalseIfBannedWordNotExist()
        {
            //Arrange
            IPunishmentsLevels punishments = null;
            _mockRepo.Setup(x => x.GetPunishment(It.IsAny<int>())).ReturnsAsync(punishments);
            //Act
            var response = await _punishedLevelService.DeletePunishemntLevel(1);
            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnAlistWithBannedWords()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetAllPunishmentLevels()).ReturnsAsync(new List<IPunishmentsLevels>()
            {
                new PunishmentSettings(1,2,3, default, default)
            });
            //Act
            var response = await _punishedLevelService.GetAllPunishmentLevels();
            //Assert
            var result = response.Should().BeOfType<List<IPunishmentsLevels>>().Subject;
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnNullIfNoBannedWordsExistInDatabase()
        {
            //Arrange
            List<IPunishmentsLevels> punishmentsLevels = new List<IPunishmentsLevels>();
            _mockRepo.Setup(x => x.GetAllPunishmentLevels()).ReturnsAsync(punishmentsLevels);
            //Act
            var response = await _punishedLevelService.GetAllPunishmentLevels();
            //Assert
            response.Should().BeNull();
        }


        [TestMethod]
        public async Task UpdatePunishedLevels_ShouldReturnTrue()
        {
            //Arrange
            var punishmentDto = new PunishmentDto() { BanLevel = 1 };      
            var punishments = new PunishmentSettings(1, 2, 3, default, default);
            _mockRepo.Setup(x => x.GetPunishment(It.IsAny<int>())).ReturnsAsync(punishments);
            _mockRepo.Setup(x => x.UpdatePunishment(It.IsAny<IPunishmentsLevels>(), It.IsAny<int>())).Returns(true);
            //Act
            var response = await _punishedLevelService.UpdatePunishmentLevel(punishmentDto, 1);

            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task UpdatePunishedLevels_ShouldReturnFalse()
        {
            //Arrange
            var punishmentDto = new PunishmentDto() { BanLevel = 1 };
            var punishments = new PunishmentSettings(1, 2, 3, default, default);
            _mockRepo.Setup(x => x.GetPunishment(It.IsAny<int>())).ReturnsAsync(punishments);
            _mockRepo.Setup(x => x.UpdatePunishment(It.IsAny<IPunishmentsLevels>(), It.IsAny<int>())).Returns(false);
            //Act
            var response = await _punishedLevelService.UpdatePunishmentLevel(punishmentDto, 1);

            //Assert
            response.Should().BeFalse();
        }
    }
}
