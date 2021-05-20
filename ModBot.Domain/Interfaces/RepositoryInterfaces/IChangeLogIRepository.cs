using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IChangeLogIRepository
    {
        void Get(int id);
        IEnumerable<IChangelogService> GetAll();
        void Create(CreateLogDto createLog);
        void Delete(int id);
    }
}
