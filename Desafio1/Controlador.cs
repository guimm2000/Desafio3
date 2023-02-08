using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class Controlador
    {
        //Decidi por manter uma lista de consulta no controlador para facilitar a listagem
        private static InterfaceUsuario interfaceUsuario;

        public static void Start()
        {
            bool fim = false;
            interfaceUsuario = new InterfaceUsuario();

            do
            {
                //Fiquei em dúvida se o controlador deveria fazer mais coisas, mas acabei colocando as funções na classe Interface de Usuário
                fim = interfaceUsuario.navegarMenus();

            } while (!fim);
        }

        public static void ListarPacientes(List<Paciente> pacientesOrdenados)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("{0,-11} {1,-33} {2,-8} {3}", "CPF", "Nome", "Dt.Nasc.", "Idade");
            Console.WriteLine("------------------------------------------------------------");

            foreach (Paciente p in pacientesOrdenados)
            {
                DateTime hoje = DateTime.Now;
                int idade = hoje.Year - p.DataNascimento.Year;

                if (hoje.Month < p.DataNascimento.Month)
                    idade--;
                else if (hoje.Month == p.DataNascimento.Month)
                {
                    if (hoje.Day < p.DataNascimento.Day)
                        idade--;
                }

                Console.WriteLine("{0} {1, -32} {2}  {3, 3}", p.Cpf, p.Nome, p.DataNascimento.ToString("dd/MM/yyyy"), idade);

                if (p.TemConsultaFutura())
                {
                    Console.WriteLine("{0, 27}{1}", "Agendado para: ", p.consultaFutura().Data.ToString("dd/MM/yyyy"));
                    Console.WriteLine("{0, 17} {1} {2}", p.consultaFutura().HoraInicial.ToString(@"hh\:mm"), "às", p.consultaFutura().HoraFinal.ToString(@"hh\:mm"));
                }

            }
        }

        public static void ListarAgenda(ListaAgenda tipo, DateTime? dataI, DateTime? dataF, List<Consulta> consultasOrdenadas)
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("{0, 7} {1, 8} {2} {3} {4, -21} {5, 8}", "Data", "H.Ini", "H.Fim", "Tempo", "Nome", "Dt.Nasc");
            Console.WriteLine("-------------------------------------------------------------");

            foreach(Consulta c in consultasOrdenadas)
            {
                if (tipo.Equals(ListaAgenda.TODA))
                {
                    Console.WriteLine("{0} {1} {2} {3} {4, -21} {5}", c.Data.ToString("dd/MM/yyyy"), c.HoraInicial.ToString(@"hh\:mm"),
                        c.HoraFinal.ToString(@"hh\:mm"), new Intervalo(c.HoraInicial, c.HoraFinal).Duracao.ToString(@"hh\:mm"),
                        c.Paciente.Nome, c.Paciente.DataNascimento.ToString("dd/MM/yyyy"));

                }
                else
                {
                    if(c.Data >= dataI && c.Data <= dataF)
                    {
                        Console.WriteLine("{0} {1} {2} {3} {4, -21} {5}", c.Data.ToString("dd/MM/yyyy"), c.HoraInicial.ToString(@"hh\:mm"),
                        c.HoraFinal.ToString(@"hh\:mm"), new Intervalo(c.HoraInicial, c.HoraFinal).Duracao.ToString(@"hh\:mm"),
                        c.Paciente.Nome, c.Paciente.DataNascimento.ToString("dd/MM/yyyy"));
                    }

                }
            }
        } 
    }
}
