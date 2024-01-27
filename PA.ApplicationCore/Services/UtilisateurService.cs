using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore.Services
{
    public class UtilisateurService : Service<Utilisateur>, IUtilisateur
    {
        public UtilisateurService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
