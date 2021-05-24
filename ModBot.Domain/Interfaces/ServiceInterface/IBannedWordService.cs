
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
        bool CreateBannedWord(CreateBannedWordDto createBannedWord);
        Task<bool> DeleteBannedWord(int id);
        Task<bool> UpdateBannedWord(UpdateBannedWordDto updatePunishment, int id);
    }
}
