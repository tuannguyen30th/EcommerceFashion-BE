﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IClaimService
    {
        public Guid? GetCurrentUserId { get; }
    }
}
