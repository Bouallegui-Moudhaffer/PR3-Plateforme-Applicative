using Microsoft.AspNetCore.Mvc.ModelBinding;
using PA.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore.Models
{
    public class PostesModel
    {
        public string Ref { get; set; } 

        // Foreign Key
        public int SallesId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
    }
}
