using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1 
{ 
    //Interface que recebe comandos e exibe as telas para o usuário
    public class InterfaceUsuario
    {
        private Menus estado;

        internal void MenuPrincipal()
        {
            Console.WriteLine("\nMenu Principal\r\n1-Cadastro de pacientes\r\n2-Agenda\r\n3-Fim\n");
            estado = Menus.MENU_PRICIPAL;
        }

        internal void MenuCadastro()
        {
            Console.WriteLine("\nMenu do Cadastro de Pacientes\r\n1-Cadastrar novo paciente\r\n2-Excluir paciente\r\n3-Listar pacientes (ordenado por CPF)\r\n4-Listar pacientes (ordenado por nome)\r\n5-Voltar p/ menu principal\n");
            estado = Menus.MENU_CADASTRO;
        }

        internal void Agenda()
        {
            Console.WriteLine("\nAgenda\r\n1-Agendar consulta\r\n2-Cancelar agendamento\r\n3-Listar agenda\r\n4-Voltar p/ menu principal\n");
            estado = Menus.AGENDA;
        }

        internal bool navegarMenus()
        {
            switch(estado) 
            {
                case Menus.MENU_PRICIPAL:
                    return this.navegarMenuPrincipal();

                case Menus.MENU_CADASTRO:
                    return this.navegarMenuCadastro();

                case Menus.AGENDA:
                    return this.navegarAgenda();
            
            }
            return false;
        }

        internal bool navegarMenuPrincipal()
        {
            this.MenuPrincipal();
            int opcao = Int32.Parse(Console.ReadLine());

            switch (opcao)
            {
                case (int)Menus.MENU_CADASTRO:
                    estado = Menus.MENU_CADASTRO;
                    this.navegarMenus();
                    break;

                case (int)Menus.AGENDA:
                    estado = Menus.AGENDA;
                    this.navegarMenus();
                    break;

                case (int)Menus.FIM:
                    return true;

                default:
                    Console.WriteLine("\nEssa opção não existe!");
                    break;
            }
            return false;
        }

        internal bool navegarMenuCadastro()
        {
            this.MenuCadastro();
            int opcao = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (opcao)
            {
                case (int)EnumMenuCadastro.CADASTRAR:
                    bool isValid;
                    PacienteForm form = new PacienteForm();
                    PacienteValidator validador = new PacienteValidator();

                    form.lerDados();

                    do
                    {
                        isValid = validador.IsValid(form.Nome, form.Cpf, form.DataNascimento);

                        if (isValid)
                        {
                            var paciente = new Paciente(validador.Paciente.Nome, validador.Paciente.Cpf, validador.Paciente.DataNascimento);

                            if (Controlador.Pacientes.Contains(paciente))
                            {
                                Console.WriteLine("\nCPF: {0}\r\nErro: CPF já cadastrado\n", paciente.formatCPF(paciente.Cpf));
                            }

                            else 
                            {
                                Console.WriteLine("\nPaciente cadastrado com sucesso!");
                                Controlador.Pacientes.Add(paciente);
                            }
                        }
                        else
                        {
                            form.lerDados(validador);
                        }

                    } while (!isValid);
                    break;

                case (int)EnumMenuCadastro.EXCLUIR:
                    Console.Write("CPF: ");
                    string cpf = Console.ReadLine();

                    Paciente? p = null;

                    if ((p = Controlador.findPaciente(long.Parse(cpf))) != null)
                    {
                        if (!p.TemConsultaFutura())
                        {
                            Controlador.excluiPaciente(p);
                            Console.WriteLine("\nPaciente excluído com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("\nErro: paciente está agendado.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nErro: paciente não cadastrado\n");
                    }
                    break;

                case (int)EnumMenuCadastro.LISTAR_CPF:
                    Controlador.ListarPacientes(CampoPaciente.CPF);
                    break;

                case (int)EnumMenuCadastro.LISTAR_NOME:
                    Controlador.ListarPacientes(CampoPaciente.NOME);
                    break;

                case (int)EnumMenuCadastro.VOLTAR:
                    estado = Menus.MENU_PRICIPAL;
                    break;

                default:
                    Console.WriteLine("\nEssa opção não existe!");
                    break;
            }
            return false;
        }

        internal bool navegarAgenda()
        {
            this.Agenda();
            int opcao = Int32.Parse(Console.ReadLine());
            Paciente? p;

            switch (opcao)
            {
                case (int)EnumAgenda.AGENDAR:
                    bool isValid;
                    ConsultaForm form = new ConsultaForm();
                    ConsultaValidator validador = new ConsultaValidator();

                    Console.Write("CPF: ");
                    string cpf = Console.ReadLine();

                    form.lerDados();

                    do
                    {
                        isValid = validador.IsValid(form.Data, form.HoraInicial, form.HoraFinal);

                        if (isValid)
                        { 
                            p = null;

                            p = Controlador.findPaciente(long.Parse(cpf));
                            var consulta = new Consulta(validador.Consulta.Data, validador.Consulta.HoraInicial, validador.Consulta.HoraFinal, p);
                            if (p != null)
                            {
                                if (!Controlador.Intersecao(consulta))
                                {
                                    if (!p.TemConsultaFutura())
                                    {
                                        Console.WriteLine("\nAgendamento realizado com sucesso!");
                                        p.Consultas.Add(consulta);
                                        Controlador.Agenda.Add(consulta);
                                    }
                                    else
                                        Console.WriteLine("Erro: paciente está agendado.");
                                }
                                else
                                {
                                    Console.WriteLine("\nErro: já existe uma consulta agendada nesse horário");
                                }
                            }

                            else
                            {
                                Console.WriteLine("\nErro: paciente não cadastrado");
                            }
                        }
                        else
                        {
                            form.lerDados(validador);
                        }

                    } while (!isValid);
                    break;

                case (int)EnumAgenda.CANCELAR:
                    Console.Write("\nCPF: ");
                    cpf = Console.ReadLine();

                    Console.Write("\nData da consulta: ");
                    string data = Console.ReadLine();

                    Console.Write("\nHora inicial: ");
                    string horaI = Console.ReadLine();

                    p = Controlador.findPaciente(long.Parse(cpf));

                    if (p == null)
                    {
                        Console.WriteLine("\nErro: paciente não cadastrado");
                    }

                    else if (Controlador.cancelarConsulta(long.Parse(cpf), data, horaI))
                    {
                        Console.WriteLine("\nAgendamento cancelado com sucesso!");
                    }
                    
                    else
                    {
                        Console.WriteLine("\nErro: agendamento não encontrado");
                    }

                    break;

                case (int)EnumAgenda.LISTAR:
                    Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
                    string tipo = Console.ReadLine().ToUpper();
                    if(tipo.Equals("P"))
                    {
                        Console.Write("Data Inicio: ");
                        string dataI = Console.ReadLine();
                        Console.Write("Data Final: ");
                        string dataF = Console.ReadLine();


                        Controlador.ListarAgenda(ListaAgenda.PERIODO, DateTime.ParseExact(dataI, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                            DateTime.ParseExact(dataF, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                    }
                    else if (tipo.Equals("T"))
                    {
                        Controlador.ListarAgenda(ListaAgenda.TODA, null, null);
                    }
                    else
                    {
                        Console.WriteLine("Essa opção não existe!");
                    }
                    break;

                case (int)EnumAgenda.VOLTAR:
                    estado = Menus.MENU_PRICIPAL;
                    break;

                default:
                    Console.WriteLine("\nEssa opção não existe!");
                    break;
            }
            return false;
        }
    }
}