using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IBannedWordIRepository
    {
        IBannedWord Get(int id);
        IEnumerable<IBannedWord> GetAll();
        void Create(IBannedWord createBannedWord);
        void Delete(int id);
        IChangelog Update(IBannedWord updateBannedWord, int id);
    }
}
