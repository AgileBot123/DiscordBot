using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Extensions.Routes
{
    public static class Routes
    {
        public const string Root = "api";
        private const string Base = Root + "/";

        public class BannedWords
        {
            public const string GetBannedWords = Base + "";
        }

    }
}
