using ModBot.DAL.Repository;
using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class MemberService : IMemberService
    {
        private readonly DatabaseRepository _databaseRepository;

        public MemberService(DatabaseRepository databaseRepository)
        {
            this._databaseRepository = databaseRepository;
        }
        public async Task<IEnumerable<IMember>> GetAllMembers()
        {
            var members = await _databaseRepository.GetAllMembers();

            if (members.Count() == 0)
                return null;

            return members;
        }

        public async Task<IMember> GetMemberById(ulong id)
        {
            var member = await _databaseRepository.GetMember(id);

            if (member == null)
                return null;

            return member;
        }
    }
}
