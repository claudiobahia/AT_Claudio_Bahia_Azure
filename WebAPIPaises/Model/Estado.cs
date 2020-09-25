using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPaises.Model
{
    public class Estado
    {
        public int Id { set; get; }
        public string Nome { set; get; }
        public string Foto { set; get; }
        public Guid PaisId { set; get; }
        public Pais Pais { set; get; }
    }
}
