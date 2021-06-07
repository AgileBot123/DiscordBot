
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

        public bool CreatePunishmentLevel(PunishmentSettingsDto createPunished)
        {
            var createdPunishment = new PunishmentSettings(
                        timeoutLevel: createPunished.TimeOutLevel,
                        kickLevel: createPunished.KickLevel,
                        banLevel: createPunished.BanLevel,
                        spamMuteLevel: createPunished.SpamMuteTime,
                        strikeMuteLevel: createPunished.StrikeMuteTime,
                        guildId: createPunished.GuildId
                     );

            return _databaseRepository.CreatePunishmentSetting(createdPunishment);
        }

        public async Task<bool> DeletePunishemntLevel(PunishmentSettingsDto punishment)
        {
            var getPunishedLevel = await _databaseRepository.GetPunishmentSetting(punishment.GuildId);

            if (getPunishedLevel != null)
            {
                return _databaseRepository.DeletePunishmentSetting(getPunishedLevel);
            }

            return false;
        }

        public async Task<IPunishmentsLevels> GetPunishmentLevels(ulong guilId)
        {
            var punishmentLevels = await _databaseRepository.GetPunishmentLevels(guilId);

            return punishmentLevels;
        }


        public async Task<IPunishmentsLevels> GetPunishmentLevel(ulong guilId,int id)
        {

            var punishment = await _databaseRepository.GetPunishmentSetting(guilId);

            if (punishment is null)
                return null;

            return punishment;          
        }

        public async Task<bool> UpdatePunishmentLevel(PunishmentSettingsDto updatePunishment, int id)
        {
            var selectPunishment = await _databaseRepository.GetPunishmentSetting(updatePunishment.GuildId);

            if(selectPunishment != null)
            {
                var punishment = new PunishmentSettings(updatePunishment.TimeOutLevel,
                                                       updatePunishment.KickLevel,
                                                       updatePunishment.BanLevel,
                                                       updatePunishment.SpamMuteTime,
                                                       updatePunishment.StrikeMuteTime,
                                                       updatePunishment.GuildId);

                var result = _databaseRepository.UpdatePunishmentSetting(punishment,id);

                if (result)
                    return true;
            }
            return false;     
        }
    }
}
