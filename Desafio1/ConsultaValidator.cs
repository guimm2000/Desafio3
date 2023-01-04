using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public class ConsultaValidator
    {
        public ConsultaErros erros = new ConsultaErros();
        public ConsultaDTO Consulta { get; private set; }

        public ConsultaValidator()
        {
            Consulta = new ConsultaDTO();
        }

        public bool IsValid(string data, string horaI, string horaF)
        {
            erros.Clear();

            data = data.Trim();
            try
            {
                Consulta.Data = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (Consulta.Data < DateTime.Now)
                    erros.AddErro(CampoConsulta.DATA, "A data da consulta deve ser maior ou igual à data atual!");
            }
            catch (Exception)
            {
                erros.AddErro(CampoConsulta.DATA, "Data deve estar no formato DD/MM/AAAA!");
            }

            int horaInicial;
            horaI.Trim();
            horaInicial = Int32.Parse(horaI);
            horaI = horaI.Insert(2, ":");

            try
            {
                Consulta.HoraInicial = TimeSpan.Parse(horaI);

                int horaTemp = horaInicial%10 + (((horaInicial/10)%10)*10);

                if (Consulta.Data == DateTime.Now && Consulta.HoraInicial < DateTime.Now.TimeOfDay)
                    erros.AddErro(CampoConsulta.HORA_INICIAL, "Hora inicial deve ser maior que a hora atual!");

                else if (horaTemp % 15 != 0)
                    erros.AddErro(CampoConsulta.HORA_INICIAL, "Hora inicial deve ser múltipla de 15!");
                else if(Consulta.HoraInicial.Hours < 8)
                    erros.AddErro(CampoConsulta.HORA_INICIAL, "Hora inicial não pode ser antes das 8 horas!");
                else if ((Consulta.HoraInicial.Hours == 18 && Consulta.HoraInicial.Minutes > 45) || Consulta.HoraInicial.Hours >= 19)
                    erros.AddErro(CampoConsulta.HORA_INICIAL, "Último horário para consulta é às 18:45 horas!");


            }
            catch(Exception)
            {
                erros.AddErro(CampoConsulta.HORA_INICIAL, "Data deve estar no formato HH:MM!");
            }


            int horaFinal;
            horaF.Trim();
            horaFinal = Int32.Parse(horaF);
            horaF = horaF.Insert(2, ":");

            try
            {
                Consulta.HoraFinal = TimeSpan.Parse(horaF);

                int horaTemp = horaFinal % 10 + (((horaFinal / 10) % 10) * 10);

                if (Consulta.Data == DateTime.Now && Consulta.HoraFinal <= Consulta.HoraInicial)
                    erros.AddErro(CampoConsulta.HORA_FINAL, "Hora final deve ser maior que a hora atual!");

                else if (horaTemp % 15 != 0)
                    erros.AddErro(CampoConsulta.HORA_FINAL, "Hora final deve ser múltipla de 15!");

                else if(Consulta.HoraFinal < Consulta.HoraInicial)
                    erros.AddErro(CampoConsulta.HORA_FINAL, "Hora final deve ser maior do que a hora inicial!");
                else if(Consulta.HoraFinal.Hours > 19)
                    erros.AddErro(CampoConsulta.HORA_FINAL, "Hora final não pode passar das 19 horas!");

            }
            catch (Exception)
            {
                erros.AddErro(CampoConsulta.HORA_FINAL, "Data deve estar no formato HH:MM!");
            }

            return erros.isEmpty();
        }
    }
}
