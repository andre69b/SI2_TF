using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio3
{
    class Program
    {

        private static void ponto2(int points,int clienteId)
        {
            using (var ctx = new SI2_1314i_TPEntities())
            {
                var q = (from a in ctx.Cliente 
                         where a.nr_cliente == clienteId 
                         select a
                        );
                foreach (var cliente in q)
                {
                    cliente.pontos += points;
                }
                ctx.SaveChanges();
            }
        }

        static void Main(string[] args)
        {
            ponto2(2,0);
        }
    }
}
