using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class ConsultaDAO
    {
        private ConsultorioContexto contexto;

        public ConsultaDAO()
        {
            this.contexto = new ConsultorioContexto();
        }

        public void Adicionar(Consulta consulta)
        {
            //Com o método Add dava exception pois o banco tentava inserir o paciente de novo pois eles estão relacionados,
            //então usei o método update pois ele também vai adicionar a consulta ao banco.
            contexto.Consultas.Update(consulta);
            contexto.SaveChanges();
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("===================");
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }

        public void Remover(Consulta consulta)
        {
            contexto.Consultas.Remove(consulta);
            contexto.SaveChanges();
        }

        public  bool cancelarConsulta(long cpf, string data, string horaI, Paciente p)
        {
            data = data.Trim();
            DateTime dataTemp = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            horaI.Trim();
            horaI = horaI.Insert(2, ":");
            TimeSpan horaITemp = TimeSpan.Parse(horaI);

            foreach (Consulta c in p.Consultas)
            {
                if (c.Data.Equals(dataTemp) && c.HoraInicial.Equals(horaITemp))
                {
                    p.Consultas.Remove(c);
                    Remover(c);
                    return true;
                }
            }
            return false;
        }

        public List<Consulta> Consultas()
        {
            return (from c in contexto.Consultas orderby c.Data select c).ToList();
        }

        public bool Intersecao(Consulta c)
        {
            foreach (Consulta consulta in contexto.Consultas.ToList())
            {
                if (consulta.TemSobreposicao(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
