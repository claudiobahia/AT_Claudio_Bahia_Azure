using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public int Foto { get; set; }
        public string Nome { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
    }
}
