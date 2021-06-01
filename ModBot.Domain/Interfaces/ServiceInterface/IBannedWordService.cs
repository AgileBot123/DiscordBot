
using ModBot.Domain.DTO.BannedWordDtos;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
    public interface IBannedWordService
    {
        Task<IBannedWord> GetBannedWord(ulong guildId, string word);
        Task<IEnumerable<IBannedWord>> GetAllBannedWords(ulong guildId);
        bool CreateBannedWord(BannedWordDto createBannedWord);
        Task<bool> DeleteBannedWord(ulong guildId, string word);
        Task<bool> UpdateBannedWordList(BannedWordListDto updatebannedWordList);
    }
}
