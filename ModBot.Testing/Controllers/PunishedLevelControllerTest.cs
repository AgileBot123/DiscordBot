using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
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
        private PunishedLevelsController _punishedLevelsController;
        //public PunishedLevelControllerTest()
        //{
        //    _mockPunish = new Mock<IPunishmentsLevelsService>();
        //    _punishedLevelsController = new PunishedLevelsController(_mockPunish.Object);
        //}


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

            //Act

            //Assert
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
    }
}
