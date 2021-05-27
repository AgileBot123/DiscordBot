using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.Business.Services;
using ModBot.DAL.Repository;
using ModBot.Domain.DTO.ChangelogDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Testing.Services
{
    [TestClass]
    public class ChangelogServiceTest
    {
        private readonly Mock<DatabaseRepository> _mockRepo;
        private readonly ChangelogService _changeLogService;

        public ChangelogServiceTest()
        {
            _mockRepo = new Mock<DatabaseRepository>();
            _changeLogService = new ChangelogService(_mockRepo.Object);
        }

        private ChangeLogDto changelogDto = new ChangeLogDto()
        {
            ChangeDate = DateTime.Now,
            Changed = "Timeout"
        };
        private IChangelog changeLog = new Changelog(default(DateTime), "Timeout");

        private List<ChangeLogDto> PunishmentLevelsList = new List<ChangeLogDto>();

        [TestMethod]
        public void CreateChangeLog_ShouldReturnTrue()
        {
            //Arrange
            _mockRepo.Setup(x => x.CreateChangelog(It.IsAny<IChangelog>())).Returns(true);
            //Act
            var response = _changeLogService.CreateChangelog(changelogDto);

            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public void CreateChangeLog_ShouldReturnFalse()
        {
            //Arrange
            _mockRepo.Setup(x => x.CreateChangelog(It.IsAny<IChangelog>())).Returns(false);
            //Act
            var response = _changeLogService.CreateChangelog(changelogDto);

            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public async Task GetChangeLog_shouldReturnABannedWordWithCOrrectWord()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetChangelog(It.IsAny<int>())).ReturnsAsync(changeLog);
            //Act
            var response = await _changeLogService.GetChangeLog(1);

            //Assert
            response.Changed.Should().Be("Timeout");
        }

        [TestMethod]
        public async Task GetChangeLog_shouldReturnNullIfNoBannedWordExists()
        {
            //Arrange
            IChangelog changelog = null;
            _mockRepo.Setup(x => x.GetChangelog(It.IsAny<int>())).ReturnsAsync(changelog);
            //Act
            var response = await _changeLogService.GetChangeLog(1);

            //Assert
            response.Should().BeNull();
        }

        [TestMethod]
        public async Task DeleteChangeLog_ShouldReturnTrue()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetChangelog(It.IsAny<int>())).ReturnsAsync(changeLog);
            _mockRepo.Setup(x => x.DeleteChangelog(It.IsAny<IChangelog>())).Returns(true);
            //Act
            var response = await _changeLogService.DeleteChangelog(1);
            //Assert
            _mockRepo.Verify(x => x.GetChangelog(It.IsAny<int>()), Times.Once);
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task DeleteChangeLog_ShouldReturnfalseIfBannedWordNotExist()
        {
            //Arrange
            IChangelog changelog = null;
            _mockRepo.Setup(x => x.GetChangelog(It.IsAny<int>())).ReturnsAsync(changelog);
            //Act
            var response = await _changeLogService.DeleteChangelog(1);
            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public async Task GetAllChangeLog_ShouldReturnAlistWithBannedWords()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetAllChangelogs()).ReturnsAsync(new List<IChangelog>()
            {
                new Changelog(default(DateTime), "Timeout")
            });
            //Act
            var response = await _changeLogService.GetAllChangelogs();
            //Assert
            var result = response.Should().BeOfType<List<IChangelog>>().Subject;
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GetAllChangeLog_ShouldReturnNullIfNoBannedWordsExistInDatabase()
        {
            //Arrange
            List<IChangelog> changelogs = new List<IChangelog>();
            _mockRepo.Setup(x => x.GetAllChangelogs()).ReturnsAsync(changelogs);
            //Act
            var response = await _changeLogService.GetAllChangelogs();
            //Assert
            response.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateChangeLog_ShouldReturnTrue()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetChangelog(It.IsAny<int>())).ReturnsAsync(changeLog);
            _mockRepo.Setup(x => x.UpdateChangelog(It.IsAny<int>(), It.IsAny<IChangelog>())).Returns(true);
            //Act
            var response = await _changeLogService.UpdateChangelog(changelogDto, 1);

            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task UpdateChangeLog_ShouldReturnFalse()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetChangelog(It.IsAny<int>())).ReturnsAsync(changeLog);
            _mockRepo.Setup(x => x.UpdateChangelog(It.IsAny<int>(), It.IsAny<IChangelog>())).Returns(false);
            //Act
            var response = await _changeLogService.UpdateChangelog(changelogDto, 1);

            //Assert
            response.Should().BeFalse();
        }
    }
}

