using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPaises.Model
{
    public class Pais
    {
        public int Id { set; get; }
        public string Nome { set; get; }
        public string Foto { set; get; }
        public List<Estado> Estados { set; get; }
    }
}
