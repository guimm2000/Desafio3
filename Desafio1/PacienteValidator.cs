using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class PacienteValidator
    {
        public PacienteErros erros = new PacienteErros();
        public PacienteDTO Paciente { get; private set; }

        public PacienteValidator()
        {
            Paciente = new PacienteDTO();
        }

        public bool IsValid(string nome, string strCPF, string strDataNascimento)
        {
            erros.Clear();

            nome = nome.Trim();
            if (nome.Length < 5)
                erros.AddErro(CampoPaciente.NOME, "Nome deve ter ao menos 5 letras");
            else
                Paciente.Nome = nome;

            // CPF
            strCPF = strCPF.Trim();

            try
            {
                Paciente.Cpf = long.Parse(strCPF);

                if (!Paciente.Cpf.IsValidCPF())
                    erros.AddErro(CampoPaciente.CPF, "CPF inválido");
            }
            catch (Exception)
            {
                erros.AddErro(CampoPaciente.CPF, "CPF deve ter 11 dígitos");
            }

            // Data de nascimento
            try
            {
                Paciente.DataNascimento = DateTime.ParseExact(strDataNascimento, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (Paciente.DataNascimento > DateTime.Now.AddYears(-13))
                    erros.AddErro(CampoPaciente.DATA_NASCIMENTO, "paciente deve ter pelo menos 13 anos.");
            }
            catch (Exception)
            {
                erros.AddErro(CampoPaciente.DATA_NASCIMENTO, "Data de nascimento deve estar no formato DD/MM/AAAA");
            }

            return erros.isEmpty();
        }
    }
}
