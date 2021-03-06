﻿using BusinessManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Contracts
{
    public interface ISequenceService
    {
        int? GetNextSequenceNumber(IRepository<Sequence> sequenceContext, string sequenceName);
    }
}
