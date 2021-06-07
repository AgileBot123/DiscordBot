using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.DTO.BannedWordDtos;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Testing.Controllers
{
    [TestClass]
    public class BannedWordControllerTest
    {

        private readonly Mock<IBannedWordService> _mockBannedWord;
        private BannedWordsController _bannedWordsController;
        private readonly Mock<ILoggerManager> loggerManager;
        public BannedWordControllerTest()
        {
            loggerManager = new Mock<ILoggerManager>();
            _mockBannedWord = new Mock<IBannedWordService>();
            _bannedWordsController = new BannedWordsController(_mockBannedWord.Object, loggerManager.Object);
        }

        private BannedWordDto bannedWordDto = new BannedWordDto()
        {
            Profanity = "Fuck",
            Punishment = "Timeout",
            BannedWordUsedCount = 0,
            Strikes = 2,
            GuildId = 838707761067982881
        };

        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnOk()
        {
            //Arrange
            List<IBannedWord> bannedWords = new List<IBannedWord>()
            {
                new BannedWord("Fuck", 1, "Kicked", 12312312312),
                new BannedWord("FuckFuck", 1, "Banned", 12312312312)
            };
            _mockBannedWord.Setup(x => x.GetAllBannedWords(It.IsAny<ulong>())).ReturnsAsync(bannedWords);
            //Act
            var response = await _bannedWordsController.GetAllBannedWords(bannedWordDto);
            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var vehicle = result.Value.Should().BeOfType<List<IBannedWord>>().Subject;
            vehicle.Count().Should().Be(2);
        }
   


        [TestMethod]
        public async Task GetBannedWord_ShouldReturnOk()
        {
            //Arrange
            _mockBannedWord.Setup(x => x.GetBannedWord(It.IsAny<ulong>(), It.IsAny<string>())).ReturnsAsync(new BannedWord("Fuck", 4, "Timeout", 12312312312));
            //Act
            var response = await _bannedWordsController.GetBannedWord(bannedWordDto);
            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var okValue = result.Value.Should().BeOfType<BannedWord>().Subject;
            okValue.Profanity.Should().Be("Fuck");

        }
        [TestMethod]
        public async Task GetBannedWord_ShouldReturnNotFound()
        {
            //Arrangep
            IBannedWord BannedWordIsNull = null;
            _mockBannedWord.Setup(x => x.GetBannedWord(It.IsAny<ulong>(), It.IsAny<string>())).ReturnsAsync(BannedWordIsNull);
            //Act
            var response = await _bannedWordsController.GetBannedWord(bannedWordDto);
            //Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public void CreateBannedWord_ShouldReturnNoContent()
        {
            //Arrange
            var createBannedWord = new BannedWordDto
            {
                Punishment = "Timeout",
                Strikes = 3,
                Profanity = "Fuck"
            };
            _mockBannedWord.Setup(x => x.CreateBannedWord(It.IsAny<BannedWordDto>())).Returns(true);
            //Act
            var response = _bannedWordsController.CreateBannedWord(createBannedWord);
            //Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task CreateBannedWord_ShouldReturnBadRequest()
        {
            //Arrange

            //Act
            var response = _bannedWordsController.CreateBannedWord(null);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Parameters cannot be null");
        }

        [TestMethod]
        public async Task CreateBannedWord_ShouldReturnBadRequestIfServiceReturnsFalse()
        {
            //Arrange
            var bannedWord = new BannedWordDto();
            _mockBannedWord.Setup(x => x.CreateBannedWord(It.IsAny<BannedWordDto>())).Returns(false);
            //Act
            var response = _bannedWordsController.CreateBannedWord(bannedWord);
            //Assert
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Banned word was not created");
        }


        [TestMethod]
        public async Task UpdateBannedWord_ShouldReturnNoContent()
        {
            //Arrange
            List<BannedWordDto> BannedWordList = new List<BannedWordDto>()
            {
                new BannedWordDto{ Punishment = "Timeout", Strikes = 1, Profanity = "Fuck" }
            };

            var bannedWord = new BannedWordListDto();
            bannedWord.BannedWordList = BannedWordList;
            _mockBannedWord.Setup(x => x.UpdateBannedWordList(It.IsAny<BannedWordListDto>())).ReturnsAsync(true);

            //Act
            var response = await _bannedWordsController.UpdateBannedWordList(bannedWord);
            //Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task UpdateBannedWord_ShouldReturnBadRequest()
        {
            //Arrange
            List<BannedWordDto> BannedWordList = new List<BannedWordDto>()
            {
                new BannedWordDto{ Punishment = "Timeout", Strikes = 1, Profanity = "Fuck" }
            };
            var bannedWord = new BannedWordListDto();
            bannedWord.BannedWordList = BannedWordList;
            _mockBannedWord.Setup(x => x.UpdateBannedWordList(It.IsAny<BannedWordListDto>())).ReturnsAsync(false);
            //Act
            var response = await _bannedWordsController.UpdateBannedWordList(bannedWord);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("banned word was not update");
        }

        [TestMethod]
        public async Task UpdateBannedWord_ShouldReturnBadRequestWhenDtoIsNull()
        {
            //Arrange
            List<BannedWordDto> BannedWordList = null;
            var bannedWord = new BannedWordListDto();
            bannedWord.BannedWordList = BannedWordList;
            //Act
            var response = await _bannedWordsController.UpdateBannedWordList(bannedWord);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Parameters cannot be null and/or id cannot be zero");
        }

  
        [TestMethod]
        public async Task DeleteBannedWord_ShouldReturnNoContent()
        {
            //Arrange
            string word = "Fuck";
            _mockBannedWord.Setup(x => x.DeleteBannedWord(It.IsAny<ulong>(), It.IsAny<string>())).ReturnsAsync(true);
            //Act
            var newBannedWord = new BannedWordDto()
            {
                Profanity = "Fuck",
                Punishment = "Timeout",
                BannedWordUsedCount = 0,
                Strikes = 2,
                GuildId = 838707761067982881
            };
            var response = await _bannedWordsController.DeleteBannedWord(bannedWordDto);
            //Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task DeleteBannedWord_ShouldReturnBadRequestIfWordWasNotDeleted()
        {
            //Arrange
            string word = "Fuck";
            _mockBannedWord.Setup(x => x.DeleteBannedWord(It.IsAny<ulong>(),It.IsAny<string>())).ReturnsAsync(false);
            //Act
            var response = await _bannedWordsController.DeleteBannedWord(bannedWordDto);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Banned word was not deleted");
        }

        [TestMethod]
        public async Task DeleteBannedWord_ShouldReturnBadRequestIfParametersIsNullOrEmpty()
        {
            //Arrange

            //Act
            var response = await _bannedWordsController.DeleteBannedWord(null);
            //Assert
            var result = response.Should().BeOfType<ObjectResult>().Subject;
            result.Value.Should().Be("internal server error");
        }
    }
}
