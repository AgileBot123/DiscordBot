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
        void CreateBannedWord(IBannedWord createBannedWord);
        void DeleteBannedWord(IBannedWord bannedWord);
        IChangelog UpdateBannedWord(IBannedWord updateBannedWord, int id);
    }
}
