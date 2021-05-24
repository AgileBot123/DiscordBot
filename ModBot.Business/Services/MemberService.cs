using ModBot.DAL.Repository;
using ModBot.Domain.interfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
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
        public Task<IEnumerable<IMember>> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        public Task<IMember> GetMemberById(ulong id)
        {
            throw new NotImplementedException();
        }
    }
}
