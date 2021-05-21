using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ServiceInterface;
using System.Collections.Generic;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPunishmentsLevelsIRepository
    {
        IPunishmentsLevels Get(int id);
        IEnumerable<IPunishmentsLevels> GetAll();
        void Create(IPunishmentsLevels createPunished);
        void Delete(int id);
        IPunishmentsLevels Update(IPunishmentsLevels updatePunished, int id);
    }
}
