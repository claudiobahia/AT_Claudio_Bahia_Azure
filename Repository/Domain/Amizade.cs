using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Domain

{
    public class Amizade
    {
        public int PessoaId { get; set; }
        public Amigo PessoaEamigo { get; set; }
        public int AmigoId { get; set; }
    }
}
