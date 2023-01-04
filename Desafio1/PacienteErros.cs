using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class PacienteErros
    {
        private readonly Dictionary<CampoPaciente, string> erros;

        public PacienteErros()
        {
            erros = new Dictionary<CampoPaciente, string>();
        }

        public void AddErro(CampoPaciente campo, string message)
        {
            erros.Add(campo, message);
        }
        public void Clear()
        {
            erros.Clear();
        }

        public bool HasErro(CampoPaciente campo)
        {
            return erros.TryGetValue(campo, out var _);
        }

        public bool isEmpty()
        {
            return erros.Count() == 0;
        }

        public string GetErrorMessage(CampoPaciente campo)
        {
            return HasErro(campo) ? erros[campo] : string.Empty;
        }
    }
}
