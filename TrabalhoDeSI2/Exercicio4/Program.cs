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
            Boolean tranFlag = session.BeginTran();
            Boolean connFlag = session.OpenConnection();
            var cmd = session.CreateCommand();
            cmd.CommandText = "SELECT pontos from Cliente where nr_cliente=@nCliente";
            var p1 = new SqlParameter("@nCliente", clienteId);
            cmd.Parameters.Add(p1);
            var newPoints = (int)cmd.ExecuteScalar();

            newPoints += points;
            cmd.CommandText = "UPDATE cliente SET pontos=@newPoints where nr_cliente=@nCliente";
            p1 = new SqlParameter("@newPoints", newPoints);
            var p2 = new SqlParameter("@nCliente", clienteId);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.ExecuteNonQuery();
            session.CloseConnection(connFlag);
            session.EndTransaction(true,tranFlag);

        }

        static void Main(string[] args)
        {
            ponto2(2,0);
        }
    }
}
