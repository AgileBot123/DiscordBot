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
    public class ChangeLogControllerTest
    {
        private readonly Mock<IChangelogService> _mockPunish;
        private ChangeLogController _punishedLevelsController;
        public ChangeLogControllerTest()
        {
            _mockPunish = new Mock<IChangelogService>();
            _punishedLevelsController = new ChangeLogController(_mockPunish.Object);
        }


        [TestMethod]
        public async Task GetAllChangelogs_ShouldReturnOk()
        {
            //Arrange

            //Act

            //Assert
        }


        [TestMethod]
        public async Task GetAllChangelogs_ShouldReturnNotFound()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task GetAllChangelogs_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task GetChangelog_ShouldReturnOk()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task GetChangelog_ShouldReturnNotFound()
        {
            //Arrange

            //Act

            //Assert
        }
        [TestMethod]
        public async Task GetChangelog_ShouldReturnBadRequest()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public async Task CreateChangelog_ShouldReturnNoContent()
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
