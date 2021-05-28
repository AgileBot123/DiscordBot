using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class MemberPunishment : IMemberPunishments
    {

        #region Properties
        private readonly ulong _memberId;
        private readonly int _punishmentId;


        public ulong MemberId
        {
            get { return _memberId; }
            private set { }
        }
        public Member Member { get; private set; }

        public int PunishmentId
        {
            get { return _punishmentId; }
            private set { }
        }
        public Punishment Punishment { get; private set; }
        #endregion

        #region Constructors
        private MemberPunishment(){}

        public MemberPunishment(ulong memberId, int punishmentId)
        {
            _memberId = memberId;
            _punishmentId = punishmentId;
        }
        #endregion

    }
}
