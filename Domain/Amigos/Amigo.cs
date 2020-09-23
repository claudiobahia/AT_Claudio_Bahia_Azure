using Domain._Mundo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Amigos
{
    public class Amigo
    {
        public int id { get; set; }
        public string foto { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string dataaniversario { get; set; }
        public List<Amigo> amigos { get; set; } = new List<Amigo>();
        public Pais pais { get; set; }
        public Estado estado { get; set; }

    }
}
