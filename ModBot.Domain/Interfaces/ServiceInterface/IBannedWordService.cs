
using ModBot.Domain.DTO.BannedWordDtos;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBot.Domain.Interfaces.ServiceInterface
{
    public interface IBannedWordService
    {
        Task<IBannedWord> GetBannedWord(string word);
        Task<IEnumerable<IBannedWord>> GetAllBannedWords();
        bool CreateBannedWord(BannedWordDto createBannedWord);
        Task<bool> DeleteBannedWord(string word);
        Task<bool> UpdateBannedWordList(BannedWordListDto updatebannedWordList);
    }
}
