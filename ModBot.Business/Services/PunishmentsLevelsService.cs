﻿using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class PunishmentsLevelsService : IPunishmentsLevelsService
    {

     
        public PunishmentsLevelsService()
        {
            
        }

        public void CreatePunishmentLevel(CreatePunishmentDto createPunished)
        {
            throw new NotImplementedException();
        }

        public void DeletePunishemntLevel(IPunishmentsLevels punishments)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels()
        {
            throw new NotImplementedException();
        }

        public Task<PunishmentsLevels> GetPunishmentLevel(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePunishmentLevel(UpdatePunishmentLevelDto updatePunishment, int id)
        {
            throw new NotImplementedException();
        }
    }
}
