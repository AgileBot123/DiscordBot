using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static ModBot.Domain.Extensions.Routes.Routes;

namespace ModBot.Domain.DTO
{
    public class ListOfBannedWordsDTO
    {
        public IEnumerable<BannedWord> BWords { get; set; } = new List<BannedWord>();
     }
}
