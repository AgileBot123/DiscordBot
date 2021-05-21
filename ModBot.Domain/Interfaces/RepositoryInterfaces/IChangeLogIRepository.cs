﻿using ModBot.Domain.DTO;
using ModBot.Domain.Interfaces.ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModBot.Domain.Interfaces.RepositoryInterfaces
{
    public interface IChangeLogIRepository
    {
        IChangelog Get(int id);
        IEnumerable<IChangelog> GetAll();
        void Create(IChangelog createLog);
        void Delete(int id);
    }
}
