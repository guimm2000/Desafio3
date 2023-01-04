using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Desafio1
{
    public class Paciente
    {
        public string Nome { get; private set; }
        public long Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public List<Consulta> Consultas { get; private set; }

        public Paciente(string nome, long cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Consultas = new List<Consulta>();
        }

        public override string? ToString()
        {
            return string.Format("\nNome: {0}\nCPF: {1}\nData de nascimento: {2:dd/MM/yyyy}\n",
                                Nome, formatCPF(Cpf), DataNascimento);
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Paciente p = (Paciente) obj;
                return p.Cpf == this.Cpf;
            }
        }

        public bool TemConsultaFutura()
        {
            foreach(Consulta c in Consultas)
            {
                if(c.Data > DateTime.Now || (c.Data == DateTime.Now && c.HoraInicial > DateTime.Now.TimeOfDay))
                {
                    return true;
                }
            }

            return false;
        }

        //Foi necessário um método para dizer se existia consulta futura, porém, em outro caso, para o a listagem, ter a consulta futura ajuda
        public Consulta consultaFutura()
        {
            foreach (Consulta c in Consultas)
            {
                if (c.Data > DateTime.Now || (c.Data == DateTime.Now && c.HoraInicial > DateTime.Now.TimeOfDay))
                {
                    return c;
                }
            }
            return null;
        }

        public string formatCPF(long n)
        {
            var pattern = @"^(\d{3})(\d{3})(\d{3})(\d{2})$";
            var regExp = new Regex(pattern);
            return regExp.Replace(n.ToString(), "$1.$2.$3-$4");
        }
    }
}
