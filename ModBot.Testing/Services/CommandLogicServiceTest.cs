using Discord.Commands;
using Discord.WebSocket;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.Bot.Helper;
using ModBot.Business.Services;
using ModBot.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using System.Threading.Tasks;

namespace ModBot.Testing.Services
{
    [TestClass]
    public class CommandLogicServiceTest : ModuleBase<SocketCommandContext>
    {
        private readonly Mock<ICommandLogicRepository> _mockCommand;
        private CommandLogicService _commandService;

        public CommandLogicServiceTest()
        {
            _mockCommand = new Mock<ICommandLogicRepository>();
            _commandService = new CommandLogicService(_mockCommand.Object);
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
