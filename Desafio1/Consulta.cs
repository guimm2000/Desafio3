using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class Consulta
    {
        public int Id { get; private set; }
        public DateTime Data { get; private set; }
        public TimeSpan HoraInicial { get; private set; }
        public TimeSpan HoraFinal { get; private set; }

        //Decidi por adicionar um paciente na classe Consulta para usar na hora de imprimir a lista de consultas
        public Paciente Paciente { get; private set; }
        public int PacienteId { get; private set; }
        
        //Tive que criar esse construtor pois estava dando uma exception dizendo que era impossível fazer o bind paciente-consulta
        private Consulta()
        {

        }

        public Consulta(DateTime data, TimeSpan horaInicial, TimeSpan horaFinal, Paciente paciente)
        {
            this.Data = data;
            this.HoraInicial = horaInicial;
            this.HoraFinal = horaFinal;
            Paciente = paciente;
        }


        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Consulta c = (Consulta)obj;
                return c.Data.Equals(this.Data) && c.HoraInicial.Equals(this.HoraInicial);
            }
        }

        public bool TemSobreposicao(Consulta c)
        {
            if(c.Data.Equals(this.Data)) 
            {
                Intervalo outro, atual;
                atual = new Intervalo(this.HoraInicial, this.HoraFinal);
                outro = new Intervalo(c.HoraInicial, c.HoraFinal);

                return atual.TemIntersecao(outro);
            }

            return false;
        }
    }
}
