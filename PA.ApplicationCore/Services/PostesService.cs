﻿using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore.Services
{
    public class PostesService : Service<Postes>, IPostes
    {
        public PostesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
