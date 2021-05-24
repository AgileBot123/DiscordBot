using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.Business.Services;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Testing.Services
{
    [TestClass]
   public class CommandLogicServiceTest
    {
        private readonly Mock<ICommandLogicRepository> _mockCommand;
        private CommandLogicService _commandService;

        /// <summary>
        /// Behöver att ICommandLogicRepository skickas in i konstruktor i CommandLogicService
        /// Kan inte jobba i denna del annars.
        /// </summary>
        public CommandLogicServiceTest()
        {
            _mockCommand = new Mock<ICommandLogicRepository>();
            _commandService = new CommandLogicService();
        }


        [TestMethod]
        public async Task BotResponseCooldown_ShouldReturnCooldownResponse()
        {
            //Arrange

            //Act

            //Assert
        }

        
    }
}
