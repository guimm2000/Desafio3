using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class Controlador
    {
        public static List<Paciente>? Pacientes {get;  private set; }

        //Decidi por manter uma lista de consulta no controlador para facilitar a listagem
        public static List<Consulta>? Agenda { get; private set; }

        public static void Start()
        {
            bool fim = false;
            Pacientes = new List<Paciente>();
            Agenda = new List<Consulta>();
            InterfaceUsuario interfaceUsuario = new InterfaceUsuario();

            do
            {
                //Fiquei em dúvida se o controlador deveria fazer mais coisas, mas acabei colocando as funções na classe Interface de Usuário
                fim = interfaceUsuario.navegarMenus();

            } while (!fim);
        }

        public static Paciente? findPaciente(long cpf)
        {
            foreach(Paciente p in Pacientes) {
                if(p.Cpf== cpf) return p;
            }

            return null;
        }

        public static void excluiPaciente(Paciente p)
        {
            p.Consultas.Clear();
            Pacientes.Remove(p);
        }

        public static bool cancelarConsulta(long cpf, string data, string horaI)
        {
            data = data.Trim();
            DateTime dataTemp = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            horaI.Trim();
            horaI = horaI.Insert(2, ":");
            TimeSpan horaITemp = TimeSpan.Parse(horaI);

            Paciente p = findPaciente(cpf);

            foreach(Consulta c in p.Consultas)
            {
                if(c.Data.Equals(dataTemp) && c.HoraInicial.Equals(horaITemp))
                {
                    p.Consultas.Remove(c);
                    Agenda.Remove(c);
                    return true;
                }
            }
            return false;
        }

        public static bool Intersecao(Consulta c)
        {
            foreach(Consulta consulta in Agenda)
            {
                if(consulta.TemSobreposicao(c))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ListarPacientes(CampoPaciente tipo)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("{0,-11} {1,-33} {2,-8} {3}", "CPF", "Nome", "Dt.Nasc.", "Idade");
            Console.WriteLine("------------------------------------------------------------");

            if (tipo.Equals(CampoPaciente.NOME))
            {
                var pacientesOrdenados = Pacientes.OrderBy(paciente => paciente.Nome).ToList();

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

            else if (tipo.Equals(CampoPaciente.CPF))
            {
                var pacientesOrdenados = Pacientes.OrderBy(paciente => paciente.Cpf).ToList();

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
        }
        public static void ListarAgenda(ListaAgenda tipo, DateTime? dataI, DateTime? dataF)
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("{0, 7} {1, 8} {2} {3} {4, -21} {5, 8}", "Data", "H.Ini", "H.Fim", "Tempo", "Nome", "Dt.Nasc");
            Console.WriteLine("-------------------------------------------------------------");

            var consultasOrdenadas = Agenda.OrderBy(consulta => consulta.Data).ToList();

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
