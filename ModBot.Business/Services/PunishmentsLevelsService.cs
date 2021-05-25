
﻿using ModBot.DAL.Repository;
using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces;
using ModBot.Domain.Interfaces.ServiceInterface;
using ModBot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBot.Business.Services
{
    public class PunishmentsLevelsService : IPunishmentsLevelsService
    {
        private readonly DatabaseRepository _databaseRepository;
        public PunishmentsLevelsService(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public bool CreatePunishmentLevel(PunishmentDto createPunished)
        {
            var createdPunishment = new PunishmentsLevels(
                        timeoutLevel: createPunished.TimeOutLevel,
                        kickLevel: createPunished.KickLevel,
                        banLevel: createPunished.BanLevel,
                        spamMuteLevel: createPunished.SpamMuteTime,
                        strikeMuteLevel: createPunished.StrikeMuteTime
                     );

            return _databaseRepository.CreateGetPunishment(createdPunishment);
        }

        public async Task<bool> DeletePunishemntLevel(int id)
        {
            var getPunishedLevel = await _databaseRepository.GetPunishment(id);

            if (getPunishedLevel != null)
            {
                _databaseRepository.DeleteGetPunishment(getPunishedLevel);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<IPunishmentsLevels>> GetAllPunishmentLevels()
        {
            var punishmentLevels = await _databaseRepository.GetAllPunishmentLevels();

            if (punishmentLevels.Count() == 0)
                    return null;
            
            return punishmentLevels;
        }


        public async Task<IPunishmentsLevels> GetPunishmentLevel(int id)
        {

            var punishment = await _databaseRepository.GetPunishment(id);

            if (punishment is null)
                return null;

            return punishment;          
        }

        public async Task<bool> UpdatePunishmentLevel(PunishmentDto updatePunishment, int id)
        {
            var selectPunishment = await _databaseRepository.GetPunishment(id);

            if(selectPunishment != null)
            {
                var punishment = new PunishmentsLevels(updatePunishment.TimeOutLevel,
                                                       updatePunishment.KickLevel,
                                                       updatePunishment.BanLevel,
                                                       updatePunishment.SpamMuteTime,
                                                       updatePunishment.StrikeMuteTime);

                _databaseRepository.UpdateGetPunishment(punishment,id);

                return true;                            
            }
            return false;
            
        }
    }
}
