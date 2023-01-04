using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    //Classe para facilitar na hora de listar a agenda
    public class Intervalo
    {
        public TimeSpan HrInicio { get; private set; }
        public TimeSpan HrFim { get; private set; }

        public TimeSpan Duracao
        {
            get { return HrFim - HrInicio; }
        }

        public Intervalo(TimeSpan hrInicio, TimeSpan hrFim)
        {
            if (hrInicio > hrFim)
                throw new ArgumentException("Data de início deve ser menor ou igual que a data fim");

            HrInicio = hrInicio;
            HrFim = hrFim;
        }

        public override bool Equals(object? obj)
        {
            return obj is Intervalo intervalo &&
                   HrInicio == intervalo.HrInicio &&
                   HrFim == intervalo.HrFim;
        }

        /*
         * Dois intervalos tem interseção se um dos limites de um pertence ao outro intervalo
         */
        public bool TemIntersecao(Intervalo outro)
        {
            return Pertence(outro.HrInicio) || Pertence(outro.HrFim) || outro.Pertence(HrInicio) || outro.Pertence(HrFim);
        }

        /*
         * Verifica se uma data/hora pertence ao intervalo
         */
        private bool Pertence(TimeSpan hr)
        {
            return hr >= HrInicio && hr <= HrFim;
        }

        public override string? ToString()
        {
            return $"{HrInicio} a {HrFim}";
        }
    }
}
