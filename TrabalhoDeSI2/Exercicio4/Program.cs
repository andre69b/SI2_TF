using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercicio3;

namespace Exercicio4
{
    class Program
    {
        private static void ponto2(int points, int clienteId)
        {
            var session = new Session();
            Boolean connFlag = session.OpenConnection();
            Boolean tranFlag = session.BeginTran();
            var cmd = session.CreateCommand();
            cmd.CommandText = "SELECT pontos from Cliente where nr_cliente=@nCliente";
            var p1 = new SqlParameter("@nCliente", clienteId);
            cmd.Parameters.Add(p1);
            var newPoints = (int)cmd.ExecuteScalar();

            newPoints += points;
            cmd.CommandText = "UPDATE Cliente SET pontos=@newPoints where nr_cliente=@nCliente";
            p1 = new SqlParameter("@newPoints", newPoints);
            var p2 = new SqlParameter("@nr_cliente", clienteId);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.ExecuteNonQuery();
            session.EndTransaction(true,tranFlag);
            session.CloseConnection(connFlag);

        }

        static void Main(string[] args)
        {
            const int pontos = 200, cliente = 0;
            Console.WriteLine("\n\n\tFoi atribuido " + pontos + " pontos ao cliente " + cliente);
            ponto2(pontos, cliente);
            Console.WriteLine("--------Clique numa tecla---------");
            Console.ReadKey();
            ponto2(-pontos, cliente);
        }
    }
}
