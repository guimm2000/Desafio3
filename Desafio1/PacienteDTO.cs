using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class PacienteDTO
    {
        public string Nome { get; set; } = string.Empty;
        public long Cpf { get; set; } = 0;
        public DateTime DataNascimento { get; set; } = DateTime.MinValue;
    }
}
