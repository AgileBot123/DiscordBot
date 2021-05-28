using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Models
{
    public class MemberPunishment : IMemberPunishments
    {

        #region Properties
        private readonly ulong _memberId;
        private readonly ulong _punishmentId;


        public ulong MemberId
        {
            get { return _memberId; }
            private set { }
        }
        public Member Member { get; private set; }

        public ulong PunishmentId
        {
            get { return _punishmentId; }
            private set { }
        }
        public Punishment Punishment { get; private set; }
        #endregion

        #region Constructors
        private MemberPunishment(){}

        public MemberPunishment(ulong memberId, ulong punishmentId)
        {
            _memberId = memberId;
            _punishmentId = punishmentId;
        }
        #endregion

    }
}
