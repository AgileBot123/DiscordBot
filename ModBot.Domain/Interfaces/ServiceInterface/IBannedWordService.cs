using ModBot.Domain.DTO.BannedWordDto;
using ModBot.Domain.DTO.ChangelogDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
    public interface IBannedWordService
    {
        Task<IBannedWord> GetBannedWord(int id);
        Task<IEnumerable<IBannedWord>> GetAllBannedWords();
        void CreateBannedWord(CreateBannedWordtDto createBannedWord);
        void DeleteBannedWord(IBannedWord bannedWord);
        void UpdateBannedWord(UpdateBannedWordDto updatePunishment, int id);
    }
}
