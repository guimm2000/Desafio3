using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class PacienteForm
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; } 
        public string DataNascimento { get; private set; }  

        public void lerDados()
        {
            lerDados(null);
        }
        internal void lerDados(PacienteValidator? validador)
        {
            if (validador != null)
            {
                Console.WriteLine("\n---------------------------- ERROS ---------------------------");

                // Percorre cada item do Enumerável
                foreach (CampoPaciente campo in Enum.GetValues(typeof(CampoPaciente)))
                {
                    var msg = validador.erros.GetErrorMessage(campo);

                    if (msg.Length > 0)
                        Console.WriteLine("{0}: {1}", campo.ToString(), msg);
                }

                Console.WriteLine("--------------------------------------------------------------");
            }

            if (validador == null || validador.erros.HasErro(CampoPaciente.NOME))
            {
                Console.Write("Nome: ");
                Nome = Console.ReadLine();
            }

            if (validador == null || validador.erros.HasErro(CampoPaciente.CPF))
            {
                Console.Write("CPF: ");
                Cpf = Console.ReadLine();
            }

            if (validador == null || validador.erros.HasErro(CampoPaciente.DATA_NASCIMENTO))
            {
                Console.Write("Data de nascimento: ");
                DataNascimento = Console.ReadLine();
            }
        }
    }
}
