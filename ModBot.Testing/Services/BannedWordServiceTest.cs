using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.Business.Services;
using ModBot.DAL.Repository;
using ModBot.Domain.DTO.BannedWordDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Testing.Services
{
    [TestClass]
    public class BannedWordServiceTest
    {
        private readonly Mock<DatabaseRepository> _mockRepo;
        private readonly BannedWordService _bannedWordService;

        public BannedWordServiceTest()
        {
            _mockRepo = new Mock<DatabaseRepository>();
            _bannedWordService = new BannedWordService(_mockRepo.Object);
        }

        private BannedWordDto bannedWords = new BannedWordDto()
        {
            Punishment = "Timeout",
            Strikes = 1,
            Word = "Fuck"
        };
        private IBannedWord bannedWord = new BannedWord("Fuck", 1, "Timeout");

        private List<IBannedWord> BannedWordList = new List<IBannedWord>()
        {
            new BannedWord("Fuck", 1, "Timeout")
        };

        [TestMethod]
        public void CreateBannedWord_ShouldReturnTrue()
        {
            //Arrange
            _mockRepo.Setup(x => x.CreateBannedWord(It.IsAny<IBannedWord>())).Returns(true);
            //Act
            var response = _bannedWordService.CreateBannedWord(bannedWords);

            //Assert
           response.Should().BeTrue();
        }

        [TestMethod]
        public void CreateBannedWord_ShouldReturnFalse()
        {
            //Arrange
            _mockRepo.Setup(x => x.CreateBannedWord(It.IsAny<IBannedWord>())).Returns(false);
            //Act
            var response = _bannedWordService.CreateBannedWord(bannedWords);

            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public async Task GetBannedWord_shouldReturnABannedWordWithCOrrectWord()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetBannedWord(It.IsAny<string>())).ReturnsAsync(bannedWord);
            //Act
            var response = await _bannedWordService.GetBannedWord("Fuck");

            //Assert
            response.Word.Should().BeEquivalentTo(bannedWord.Word);
        }

        [TestMethod]
        public async Task GetBannedWord_shouldReturnNullIfNoBannedWordExists()
        {
            //Arrange
            IBannedWord bannedWord = null;
            _mockRepo.Setup(x => x.GetBannedWord(It.IsAny<string>())).ReturnsAsync(bannedWord);
            //Act
            var response = await _bannedWordService.GetBannedWord("Fuck");

            //Assert
            response.Should().BeNull();
        }

        [TestMethod]
        public async Task DeleteBannedWord_ShouldReturnTrue()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetBannedWord(It.IsAny<string>())).ReturnsAsync(bannedWord);
            _mockRepo.Setup(x => x.DeleteBannedWord(It.IsAny<IBannedWord>())).Returns(true);
            //Act
            var response = await _bannedWordService.DeleteBannedWord("Fuck");
            //Assert
            _mockRepo.Verify(x => x.GetBannedWord(It.IsAny<string>()), Times.Once);
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task DeleteBannedWord_ShouldReturnfalseIfBannedWordNotExist()
        {
            //Arrange
            IBannedWord bannedWord = null;
            _mockRepo.Setup(x => x.GetBannedWord(It.IsAny<string>())).ReturnsAsync(bannedWord);
            //Act
            var response = await _bannedWordService.DeleteBannedWord("Fuck");
            //Assert
            response.Should().BeFalse();
        }

        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnAlistWithBannedWords()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetAllBannedWords()).ReturnsAsync(new List<IBannedWord>()
            {
                new BannedWord(1, "Timeout", 1)
            });
            //Act
            var response = await _bannedWordService.GetAllBannedWords();
            //Assert
            var result = response.Should().BeOfType<List<IBannedWord>>().Subject;
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnNullIfNoBannedWordsExistInDatabase()
        {
            //Arrange
            List<IBannedWord> bannedWord = new List<IBannedWord>();
            _mockRepo.Setup(x => x.GetAllBannedWords()).ReturnsAsync(bannedWord);
            //Act
            var response = await _bannedWordService.GetAllBannedWords();
            //Assert
            response.Should().BeNull();
        }

        [TestMethod]
        public async Task UpdateBannedWord_ShouldReturnTrue()
        {
            //Arrange
            BannedWordListDto BannedWordDtoList = new BannedWordListDto();
            BannedWordDtoList.BannedWordList.Add(new BannedWordDto { Punishment = "Timeout", Strikes = 2, Word = "Fuck" });
            _mockRepo.Setup(x => x.GetAllBannedWords()).ReturnsAsync(BannedWordList);
            _mockRepo.Setup(x => x.UpdateBannedWord(It.IsAny<BannedWord>())).Returns(true);
            _mockRepo.Setup(x => x.DeleteBannedWord(It.IsAny<IBannedWord>())).Returns(true);
            //Act

            var response = await _bannedWordService.UpdateBannedWordList(BannedWordDtoList);

            //Assert
            response.Should().BeTrue();
        }

        
        public async Task UpdateBannedWord_ShouldReturnFalse()
        {
            //Arrange
            BannedWordListDto BannedWordDtoList = new BannedWordListDto();
            BannedWordDtoList.BannedWordList.Add(new BannedWordDto { Punishment = "Timeout", Strikes = 2, Word = "FuckFuck" });
            _mockRepo.Setup(x => x.GetAllBannedWords()).ReturnsAsync(BannedWordList);
            _mockRepo.Setup(x => x.CreateBannedWord(It.IsAny<IBannedWord>())).Returns(true);
            _mockRepo.Setup(x => x.UpdateBannedWord(It.IsAny<BannedWord>())).Returns(false);
            _mockRepo.Setup(x => x.DeleteBannedWord(It.IsAny<IBannedWord>())).Returns(false);
            //Act

            var response = await _bannedWordService.UpdateBannedWordList(BannedWordDtoList);

            //Assert
           // _mockRepo.Verify(x => x.DeleteBannedWord(It.IsAny<IBannedWord>(), Times.Once));
            response.Should().BeFalse();
        }

    }
}
