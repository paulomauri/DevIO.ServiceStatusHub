﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
