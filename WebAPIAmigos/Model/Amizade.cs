using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAmigos.Model
{
    public class Amizade
    {
        public int PessoaId { set; get; }
        public Amigo PessoaEamigo { set; get; }
        public int AmigoId { set; get; }
    }
}
