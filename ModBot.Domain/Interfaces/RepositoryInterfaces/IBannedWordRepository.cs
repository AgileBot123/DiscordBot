using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IBannedWordRepository
    {
        Task<IBannedWord> GetBannedWord(int id);
        Task<IEnumerable<IBannedWord>> GetAllBannedWords();
        bool CreateBannedWord(IBannedWord createBannedWord);
        bool DeleteBannedWord(IBannedWord bannedWord);
        bool UpdateBannedWord(IBannedWord updateBannedWord, int id);
    }
}
