using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Testing.Controllers
{
    [TestClass]
    public class PunishedLevelControllerTest
    {
        private readonly Mock<IPunishmentsLevels> _mockPunish;
        private PunishedLevelsController _punishedLevelsController;
        public PunishedLevelControllerTest()
        {
            _mockPunish = new Mock<IPunishmentsLevels>();
            _punishedLevelsController = new PunishedLevelsController();
        }
    }
}
