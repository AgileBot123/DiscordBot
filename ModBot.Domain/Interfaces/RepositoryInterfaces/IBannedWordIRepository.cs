using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IBannedWordIRepository
    {
        void Get(int id);
        IEnumerable<IBannedWordService> GetAll();
        void Create(CreateBannedWordDto createBannedWord);
        void Delete(int id);
        void Update(UpdateBannedWordDto updateBannedWord, int id);
    }
}
