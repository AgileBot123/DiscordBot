using ModBot.DAL.Repository;
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

        public async Task CreatePunishmentLevel(CreatePunishmentDto createPunished)
        {
            var createdPunishment = new PunishmentsLevels(
                        timeoutLevel: createPunished.TimeOutLevel,
                        kickLevel: createPunished.KickLevel,
                        banLevel: createPunished.BanLevel,
                        spamMuteLevel: createPunished.SpamMuteTime,
                        strikeMuteLevel: createPunished.StrikeMuteTime
                     );

              _databaseRepository.CreateGetPunishment(createdPunishment);
        }

        public async Task DeletePunishemntLevel(IPunishmentsLevels punishments)
        {
            var getPunishedLevel = await _databaseRepository.GetPunishment(punishments.Id);

            if (getPunishedLevel != null)
            {
                _databaseRepository.DeleteGetPunishment(getPunishedLevel);
            }
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

        public async Task UpdatePunishmentLevel(UpdatePunishmentLevelDto updatePunishment, int id)
        {
            throw new NotImplementedException();
        }

    }
}
