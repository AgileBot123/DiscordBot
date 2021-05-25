using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
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
        public BannedWordControllerTest()
        {
            _mockBannedWord = new Mock<IBannedWordService>();
            _bannedWordsController = new BannedWordsController(_mockBannedWord.Object);
        }


        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnOk()
        {
            //Arrange
            List<IBannedWord> bannedWords = new List<IBannedWord>()
            {
                new BannedWord("Fuck", 1, "Kicked"),
                new BannedWord("FuckFuck", 1, "Banned")
            };
            _mockBannedWord.Setup(x => x.GetAllBannedWords()).ReturnsAsync(bannedWords);
            //Act
            var response = await _bannedWordsController.GetAllBannedWords();
            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var vehicle = result.Value.Should().BeOfType<List<IBannedWord>>().Subject;
            vehicle.Count().Should().Be(2);
        }
        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnNotFoundWhenNoBannedWordsExists()
        {
            //Arrange
            List<IBannedWord> noBannedWordsInList = new List<IBannedWord>();
            _mockBannedWord.Setup(x => x.GetAllBannedWords()).ReturnsAsync(noBannedWordsInList);
            //Act
            var response = await _bannedWordsController.GetAllBannedWords();
            //Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnStatusCode500IfListIsNull()
        {
            //Arrange
            List<IBannedWord> listIsNull = null;
            _mockBannedWord.Setup(x => x.GetAllBannedWords()).ReturnsAsync(listIsNull);
            //Act
            var response = await _bannedWordsController.GetAllBannedWords();
            //Assert
            var result = response.Should().BeOfType<ObjectResult>().Subject;
            result.Value.Should().Be("internal server error");
        }


        [TestMethod]
        public async Task GetBannedWord_ShouldReturnOk()
        {
            //Arrange
            _mockBannedWord.Setup(x => x.GetBannedWord(It.IsAny<string>())).ReturnsAsync(new BannedWord("Fuck", 4, "Timeout"));
            //Act
            var response = await _bannedWordsController.GetBannedWord("Fuck");
            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var okValue = result.Value.Should().BeOfType<BannedWord>().Subject;
            okValue.Word.Should().Be("Fuck");

        }
        [TestMethod]
        public async Task GetBannedWord_ShouldReturnNotFound()
        {
            //Arrangep
            IBannedWord BannedWordIsNull = null;
            _mockBannedWord.Setup(x => x.GetBannedWord(It.IsAny<string>())).ReturnsAsync(BannedWordIsNull);
            //Act
            var response = await _bannedWordsController.GetBannedWord("Fuck");
            //Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }
        [TestMethod]
        public async Task GetBannedWord_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
        }
        [TestMethod]
        public async Task CreateBannedWord_ShouldReturnNoContent()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task CreateBannedWord_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task UpdateBannedWord_ShouldReturnNoContent()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task UpdateBannedWord_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
        }

    }
}
