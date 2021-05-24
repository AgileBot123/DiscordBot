
using ModBot.Domain.DTO.BannedWordDto;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
    public interface IBannedWordService
    {
        Task<IBannedWord> GetBannedWord(int id);
        Task<IEnumerable<IBannedWord>> GetAllBannedWords();
        Task CreateBannedWord(CreateBannedWordDto createBannedWord);
        Task DeleteBannedWord(IBannedWord bannedWord);
        Task UpdateBannedWord(UpdateBannedWordDto updatePunishment, int id);
    }
}
