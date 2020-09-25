using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAmigos.Model
{
    public class Pais
    {
        public int Id { set; get; }
        public string Nome { set; get; }
        public string Foto { set; get; }
        public List<Estado> Estado { set; get; }
    }
}
