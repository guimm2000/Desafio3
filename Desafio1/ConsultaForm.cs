using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class ConsultaForm
    {
        public string Data { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }

        public void lerDados()
        {
            lerDados(null);
        }
        internal void lerDados(ConsultaValidator? validador)
        {
            if (validador != null)
            {
                Console.WriteLine("\n---------------------------- ERROS ---------------------------");

                // Percorre cada item do Enumerável
                foreach (CampoConsulta campo in Enum.GetValues(typeof(CampoConsulta)))
                {
                    var msg = validador.erros.GetErrorMessage(campo);

                    if (msg.Length > 0)
                        Console.WriteLine("{0}: {1}", campo.ToString(), msg);
                }

                Console.WriteLine("--------------------------------------------------------------");
            }

            if (validador == null || validador.erros.HasErro(CampoConsulta.DATA))
            {
                Console.Write("Data: ");
                Data = Console.ReadLine();
            }

            if (validador == null || validador.erros.HasErro(CampoConsulta.HORA_INICIAL))
            {
                Console.Write("Hora inicial: ");
                HoraInicial = Console.ReadLine();
            }

            if (validador == null || validador.erros.HasErro(CampoConsulta.HORA_FINAL))
            {
                Console.Write("Hora final: ");
                HoraFinal = Console.ReadLine();
            }
        }
    }
}
