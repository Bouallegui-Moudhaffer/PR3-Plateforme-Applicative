﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        Task<int> SaveAsync();
        IGenericRepository<T> Repository<T>() where T : class;
    }
}
