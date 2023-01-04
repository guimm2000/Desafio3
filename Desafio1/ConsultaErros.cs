using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class ConsultaErros
    {
        private readonly Dictionary<CampoConsulta, string> erros;

        public ConsultaErros()
        {
            erros = new Dictionary<CampoConsulta, string>();
        }

        public void AddErro(CampoConsulta campo, string message)
        {
            erros.Add(campo, message);
        }
        public void Clear()
        {
            erros.Clear();
        }

        public bool HasErro(CampoConsulta campo)
        {
            return erros.TryGetValue(campo, out var _);
        }

        public bool isEmpty()
        {
            return erros.Count() == 0;
        }

        public string GetErrorMessage(CampoConsulta campo)
        {
            return HasErro(campo) ? erros[campo] : string.Empty;
        }
    }
}
