using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVendasAplication.Models
{
    public class UserResponse
    {
        public User user { get; set; }
        public string Token { get; set; }
    }
}