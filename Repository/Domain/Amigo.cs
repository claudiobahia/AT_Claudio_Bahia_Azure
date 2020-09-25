using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Domain
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Foto { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Dataaniversario { get; set; }
        public List<Amizade> Amizades { get; set; }
        public Pais Pais { get; set; }
        public int PaisId { get; set; }
        public Estado Estado { get; set; }
        public int EstadoId { get; set; }
    }
}
