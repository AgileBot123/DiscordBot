using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IBannedWordRepository
    {
        Task<IBannedWord> GetBannedWord(ulong guildId, string banned);
        Task<IEnumerable<IBannedWord>> GetAllBannedWords(ulong guildId);
        bool CreateBannedWord(IBannedWord createBannedWord);
        bool DeleteBannedWord(IBannedWord bannedWord);
        bool UpdateBannedWord(IBannedWord updateBannedWord);
    }
}
