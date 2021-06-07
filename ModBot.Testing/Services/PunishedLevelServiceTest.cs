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

        private readonly PunishmentSettingsDto punishmentDto = new PunishmentSettingsDto()
        {
            BanLevel = 1,
            KickLevel = 2,
            TimeOutLevel = 3,
            SpamMuteTime = 1,
            StrikeMuteTime = 2
        };
        private readonly IPunishmentsLevels punishmentLevls = new PunishmentSettings(1,2, 3, default, default, 1232131231123);

        private List<PunishmentSettingsDto> PunishmentLevelsList = new List<PunishmentSettingsDto>();

        [TestMethod]
        public void CreatePunishMent_ShouldReturnTrue()
        {
            //Arrange
            _mockRepo.Setup(x => x.CreatePunishmentSetting(It.IsAny<IPunishmentsLevels>())).Returns(true);
            //Act
            var response = _punishedLevelService.CreatePunishmentLevel(punishmentDto);

            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public void CreatePunishmentLevel_ShouldReturnFalse()
        {
            //Arrange
            _mockRepo.Setup(x => x.CreatePunishmentSetting(It.IsAny<IPunishmentsLevels>())).Returns(false);
            //Act
            var response = _punishedLevelService.CreatePunishmentLevel(punishmentDto);

            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public async Task GetPunishmentLevel_shouldReturnABannedWordWithCOrrectWord()
        {
            //Arrange
            var punishments = new PunishmentSettings(1, 2, 3, default, default, 11231232131231);
            _mockRepo.Setup(x => x.GetPunishmentSetting(It.IsAny<ulong>())).ReturnsAsync(punishments);
            //Act
            var response = await _punishedLevelService.GetPunishmentLevel(1123123123123, 1);

            //Assert
            response.BanLevel.Should().Be(3);
        }

        [TestMethod]
        public async Task GetPunishmentLevel_shouldReturnNullIfNoBannedWordExists()
        {
            //Arrange
            IPunishmentsLevels punishmentLevels = null;
            _mockRepo.Setup(x => x.GetPunishmentSetting(It.IsAny<ulong>())).ReturnsAsync(punishmentLevels);
            //Act
            var response = await _punishedLevelService.GetPunishmentLevel(1123123123123, 1);

            //Assert
            response.Should().BeNull();
        }

        [TestMethod]
        public async Task DeletePunishmentLevel_ShouldReturnTrue()
        {
            //Arrange
            var punishments = new PunishmentSettings(1, 2, 3, default, default, 11231232131231);
            _mockRepo.Setup(x => x.GetPunishmentSetting(It.IsAny<ulong>())).ReturnsAsync(punishments);
            _mockRepo.Setup(x => x.DeletePunishmentSetting(It.IsAny<IPunishmentsLevels>())).Returns(true);
            //Act
            var response = await _punishedLevelService.DeletePunishemntLevel(punishmentDto);
            //Assert
            _mockRepo.Verify(x => x.GetPunishmentSetting(It.IsAny<ulong>()), Times.Once);
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task DeleteBannedWord_ShouldReturnfalseIfBannedWordNotExist()
        {
            //Arrange
            IPunishmentsLevels punishments = null;
            _mockRepo.Setup(x => x.GetPunishmentSetting(It.IsAny<ulong>())).ReturnsAsync(punishments);
            //Act
            var response = await _punishedLevelService.DeletePunishemntLevel(punishmentDto);
            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnAlistWithBannedWords()
        {
            //Arrange
            var punishmentsLevels = new PunishmentSettings(1, 2, 3, 4, 5, 6);
            _mockRepo.Setup(x => x.GetPunishmentLevels(It.IsAny<ulong>())).ReturnsAsync(punishmentsLevels);
            //Act
            var response = await _punishedLevelService.GetPunishmentLevels(838707761067982881);
            //Assert
            var result = response.Should().BeOfType<List<IPunishmentsLevels>>().Subject;
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnNullIfNoBannedWordsExistInDatabase()
        {
            //Arrange
            IPunishmentsLevels punishmentsLevels = new PunishmentSettings(1,2,3,4,5,6);
            _mockRepo.Setup(x => x.GetPunishmentLevels(It.IsAny<ulong>())).ReturnsAsync(punishmentsLevels);
            //Act
            var response = await _punishedLevelService.GetPunishmentLevels(838707761067982881);
            //Assert
            response.Should().BeNull();
        }


        [TestMethod]
        public async Task UpdatePunishedLevels_ShouldReturnTrue()
        {
            //Arrange
            var punishmentDto = new PunishmentSettingsDto() { BanLevel = 1 };      
            var punishments = new PunishmentSettings(1, 2, 3, default, default, 11231232131231);
            _mockRepo.Setup(x => x.GetPunishmentSetting(It.IsAny<ulong>())).ReturnsAsync(punishments);
            _mockRepo.Setup(x => x.UpdatePunishmentSetting(It.IsAny<IPunishmentsLevels>(), It.IsAny<int>())).Returns(true);
            //Act
            var response = await _punishedLevelService.UpdatePunishmentLevel(punishmentDto, 1);

            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task UpdatePunishedLevels_ShouldReturnFalse()
        {
            //Arrange
            var punishmentDto = new PunishmentSettingsDto() { BanLevel = 1 };
            var punishments = new PunishmentSettings(1, 2, 3, default, default, 12312321321);
            _mockRepo.Setup(x => x.GetPunishmentSetting(It.IsAny<ulong>())).ReturnsAsync(punishments);
            _mockRepo.Setup(x => x.UpdatePunishmentSetting(It.IsAny<IPunishmentsLevels>(), It.IsAny<int>())).Returns(false);
            //Act
            var response = await _punishedLevelService.UpdatePunishmentLevel(punishmentDto, 1);

            //Assert
            response.Should().BeFalse();
        }
    }
}
