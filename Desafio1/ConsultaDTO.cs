using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class ConsultaDTO
    {
        public DateTime Data { get; set; } = DateTime.MinValue;
        public TimeSpan HoraInicial { get; set; } = TimeSpan.MinValue;
        public TimeSpan HoraFinal { get; set; } = TimeSpan.MinValue; 
    }
}
