using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Domain
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Foto { get; set; }
        public List<Estado> Estados { get; set; }
    }
}
