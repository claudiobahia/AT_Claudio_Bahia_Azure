using System;
using System.Collections.Generic;
using System.Text;

namespace Domain._Mundo
{
    public class Pais
    {
        public int id { get; set; }
        public int foto { get; set; }
        public string nome { get; set; }
        public List<Estado> estados { get; set; } = new List<Estado>();

    }
}
