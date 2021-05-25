using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.Interfaces.ServiceInterface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Testing.Controllers
{
    [TestClass]
    public class MemberControllerTest
    {

        private readonly Mock<IMemberService> _mockMamber;
        private MemberController _bannedWordsController;
        public MemberControllerTest()
        {
            _mockMamber = new Mock<IMemberService>();
            _bannedWordsController = new MemberController(_mockMamber.Object);
        }
    }
}
