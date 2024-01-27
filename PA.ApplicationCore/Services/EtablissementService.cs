using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore.Services
{
    public class EtablissementService : Service<Etablissement>, IEtablissement
    {
        public EtablissementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
