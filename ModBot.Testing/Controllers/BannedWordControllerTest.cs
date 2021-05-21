using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.Interfaces.ServiceInterface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Testing.Controllers
{
    [TestClass]
    public class BannedWordControllerTest
    {

        private readonly Mock<IBannedWordService> _mockPunish;
        private BannedWordsController _bannedWordsController;
        public BannedWordControllerTest()
        {
            _mockPunish = new Mock<IBannedWordService>();
            _bannedWordsController = new BannedWordsController(_mockPunish.Object);
        }


        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnOk()
        {
            //Arrange

            //Act

            //Assert
        }
        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnNotFound()
        {
            //Arrange

            //Act

            //Assert
        }
        [TestMethod]
        public async Task GetAllBannedWords_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task GetBannedWord_ShouldReturnOk()
        {
            //Arrange

            //Act

            //Assert
        }
        [TestMethod]
        public async Task GetBannedWord_ShouldReturnNotFound()
        {
            //Arrange

            //Act

            //Assert
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
