﻿using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ServiceInterface;
using System.Collections.Generic;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPunishmentsLevelsIRepository
    {
        void Get(int id);
        IEnumerable<IPunishedLevelService> GetAll();
        void Create(CreatePunishedDto createPunished);
        void Delete(int id);
        void Update(UpdatePunishedLevelDto updatePunished, int id);
    }
}