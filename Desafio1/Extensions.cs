using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    public static class Extensions
    {
        public static bool IsValidCPF(this long cpf)
        {
            if (cpf > 99999999999 || cpf < 11111111111)
                return false;
            else if (cpf == long.Parse(new string((char)((cpf % 10) + 48), 11)))
                return false;
            else
            {
                long cpf_sem_dv = cpf / 100;

                long soma = 0;
                for (int valor = 2; valor <= 11; valor++)
                {
                    soma += cpf_sem_dv % 10 * valor;
                    cpf_sem_dv /= 10;
                }

                int dv1 = (int)(11 - (soma % 11));

                if (dv1 > 9)
                    dv1 = 0;

                cpf_sem_dv = cpf / 10;

                soma = 0;
                for (int valor = 2; valor <= 12; valor++)
                {
                    soma += cpf_sem_dv % 10 * valor;
                    cpf_sem_dv /= 10;
                }

                int dv2 = (int)(11 - (soma % 11));

                if (dv2 > 9)
                    dv2 = 0;

                return dv1 == cpf % 100 / 10 && dv2 == cpf % 10;
            }
        }
    }
}
