using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Pais
    {
        public int Id { get; set; }
        public string Foto { get; set; }
        public string Nome { get; set; }
        public List<Estado> Estados { get; set; }
    }
}
