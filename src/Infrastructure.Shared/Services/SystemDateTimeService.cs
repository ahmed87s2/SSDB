﻿using SSDB.Application.Interfaces.Services;
using System;

namespace SSDB.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}