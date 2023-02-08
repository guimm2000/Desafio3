using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class PacienteDAO : IDisposable
    {
        private ConsultorioContexto contexto;

        public PacienteDAO()
        {
            this.contexto = new ConsultorioContexto();
        }

        public void Adicionar(Paciente paciente)
        {
            contexto.Pacientes.Add(paciente);
            contexto.SaveChanges();
        }

        public void Remover(Paciente paciente)
        {
            paciente.Consultas.Clear();
            contexto.Pacientes.Remove(paciente);
            contexto.SaveChanges();
        }

        public List<Paciente> PacientesNome()
        {
            return (from p in contexto.Pacientes orderby p.Nome select p).ToList();
        }

        public List<Paciente> PacientesCpf()
        {
            return (from p in contexto.Pacientes orderby p.Cpf select p).ToList();
        }

        public Paciente? findPaciente(long cpf)
        {
            var query = from p in contexto.Pacientes where p.Cpf.Equals(cpf) select p;
            return query.FirstOrDefault();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
