using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.API.Controllers;
using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Testing.Controllers
{
    [TestClass]
    public class MemberControllerTest
    {

        private readonly Mock<IMemberService> _mockMember;
        private MemberController _memberController;
        private readonly Mock<ILoggerManager> _Logger;
        public MemberControllerTest()
        {
            _Logger = new Mock<ILoggerManager>();
            _mockMember = new Mock<IMemberService>();
            _memberController = new MemberController(_mockMember.Object, _Logger.Object);
        }

        private List<IMember> listOfMember = new List<IMember>() 
        { 
            new Member(1,"Nahd","avatar",false) 
        };

        private List<IMember> emptyList = new List<IMember>();
        private IMember member = new Member(1, "Nahd", "avatar", false);

        [TestMethod]
        public async Task GetAllMembers_ShouldReturnOkWithAlistOfMembers()
        {
            //Arrange
            _mockMember.Setup(x => x.GetAllMembers()).ReturnsAsync(listOfMember);
            //Act
            var response = await _memberController.GetAllMembers();
            //Assert
            response.Should().BeOfType<OkObjectResult>();
        }


        [TestMethod]
        public async Task GetAllMembers_ShouldReturnNotFound()
        {
            //Arrange
            _mockMember.Setup(x => x.GetAllMembers()).ReturnsAsync(emptyList);
            //Act
            var response = await _memberController.GetAllMembers();
            //Assert
            var result = response.Should().BeOfType<NotFoundObjectResult>().Subject;
            result.Value.Should().Be("Member is empty");
        }

        [TestMethod]
        public async Task GetAllMembers_ShouldReturnBadRequest()
        {
            //Arrange
            IEnumerable<IMember> member = null;
            _mockMember.Setup(x => x.GetAllMembers()).ReturnsAsync(member);
            //Act
            var response = await _memberController.GetAllMembers();
            //Assert
            var result = response.Should().BeOfType<ObjectResult>().Subject;
            result.Value.Should().Be("internal server error");
        }

        [TestMethod]
        public async Task GetAllMember_ShouldReturnOk()
        {
            //Arrange
            ulong id = 1;
            _mockMember.Setup(x => x.GetMemberById(It.IsAny<ulong>())).ReturnsAsync(member);
            //Act
            var response = await _memberController.GetMember(id);
            //Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [TestMethod]
        public async Task GetAllMember_ShouldReturnNotFound()
        {
            //Arrange
            List<IMember> emptyList = new List<IMember>();
            _mockMember.Setup(x => x.GetAllMembers()).ReturnsAsync(emptyList);
            //Act
            var response = await _memberController.GetAllMembers();
            //Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public async Task GetAllMember_ShouldReturnBadRequest()
        {
            //Arrange
            List<IMember> emptyList = null;
            _mockMember.Setup(x => x.GetAllMembers()).ReturnsAsync(emptyList);
            //Act
            var response = await _memberController.GetAllMembers();
            //Assert
            var result = response.Should().BeOfType<ObjectResult>().Subject;
            result.Value.Should().Be("internal server error");
        }
    }
}
