﻿using System;
using System.Collections;
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
        private static void Ponto1(int cliente, DateTime inicio, DateTime fim)
        {
            var session = new Session();
            Boolean tranFlag = session.BeginTran();
            Boolean connFlag = session.OpenConnection();
            var cmd = session.CreateCommand();
            cmd.CommandText = "SELECT * from Aluguer where where cliente= @cliente and datahora_recolha >= @inicio and datahora_entrega <= @fim";
            var p1 = new SqlParameter("@cliente", cliente);
            var p2 = new SqlParameter("@inicio", inicio);
            var p3 = new SqlParameter("@fim", fim);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);

            var r1 = cmd.ExecuteReader();

            Console.WriteLine("Lista de alugueres: ");
            var aux = new ArrayList();
            int index = 0;

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                aux.Add(rdr.GetValue(0));
                Console.WriteLine((index++) + "º - aluguer:" + rdr.GetValue(0).ToString());
            }
            if (index == 1)
            {
                Console.WriteLine("Nao existem Alugueres para o Cliente escolhido");
                return;
            }


            session.CloseConnection(connFlag);
            session.EndTransaction(true, tranFlag);
        }
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
