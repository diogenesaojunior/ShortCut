using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisGerenciador.src.Models
{
    public class Usuario : IdentityUser
    {
        public bool isAdmin { get; set; }
    }
}