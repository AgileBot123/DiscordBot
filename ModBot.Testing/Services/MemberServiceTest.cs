using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModBot.Business.Services;
using ModBot.DAL.Repository;
using ModBot.Domain.DTO;
using ModBot.Domain.interfaces;
using ModBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Testing.Services
{
    [TestClass]
    public class MemberServiceTest
    {
        private readonly Mock<DatabaseRepository> _mockRepo;
        private readonly MemberService _membersService;

        public MemberServiceTest()
        {
            _mockRepo = new Mock<DatabaseRepository>();
            _membersService = new MemberService(_mockRepo.Object);
        }


        private IMember member = new Member(1, "Nahd", "avatar", false);

        private List<GetMemberDto> memberList = new List<GetMemberDto>();


        [TestMethod]
        public async Task GetMember_shouldReturnABannedWordWithCOrrectWord()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetMember(It.IsAny<ulong>())).ReturnsAsync(member);
            //Act
            var response = await _membersService.GetMemberById(1);

            //Assert
            response.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task GetMember_shouldReturnNullIfNoBannedWordExists()
        {
            //Arrange
            IMember member = null;
            _mockRepo.Setup(x => x.GetMember(It.IsAny<ulong>())).ReturnsAsync(member);
            //Act
            var response = await _membersService.GetMemberById(1);

            //Assert
            response.Should().BeNull();
        }

        [TestMethod]
        public async Task GetAllMembers_ShouldReturnAlistWithBannedWords()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetAllMembers()).ReturnsAsync(new List<IMember>()
            {
               new Member(1, "Nahd", "avatar", false)
             });
            //Act
            var response = await _membersService.GetAllMembers();
            //Assert
            var result = response.Should().BeOfType<List<IMember>>().Subject;
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GetAllMembers_ShouldReturnNullIfNoBannedWordsExistInDatabase()
        {
            //Arrange
            List<IMember> members = new List<IMember>();
            _mockRepo.Setup(x => x.GetAllMembers()).ReturnsAsync(members);
            //Act
            var response = await _membersService.GetAllMembers();
            //Assert
            response.Should().BeNull();
        }
    }
}
