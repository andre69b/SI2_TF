﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio4
{
    class Program
    {
        private static void Ponto1(int cliente, DateTime inicio, DateTime fim)
        {
            Boolean flag = true;
            var session = new Session();
            Boolean connFlag = session.OpenConnection();
            Boolean tranFlag = session.BeginTran(IsolationLevel.ReadCommitted);
            try
            {
                var cmd = session.CreateCommand();
                cmd.CommandText = "SELECT * from Aluguer where cliente= @cliente and datahora_recolha >= @inicio" +
                                  " and datahora_entrega <= @fim";
                var p1 = new SqlParameter("@cliente", cliente);
                var p2 = new SqlParameter("@inicio", inicio);
                var p3 = new SqlParameter("@fim", fim);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);


                Console.WriteLine("Lista de alugueres: ");
                var aux = new ArrayList();
                int index = 1;

                var r1 = cmd.ExecuteReader();
                while (r1.Read())
                {
                    var reader = r1.GetValue(0);
                    aux.Add(reader);
                    Console.WriteLine((index++) + "º - aluguer:" + reader.ToString());
                }
                if (index == 1)
                {
                    Console.WriteLine("Nao existem Alugueres para o Cliente escolhido");

                }

                Console.WriteLine("Qual é o aluguer que deseja selecionar ?");
                int num = 0;
                while (true)
                {
                    string text = Console.ReadLine();
                    if (text != null)
                    {
                        num = int.Parse(text) - 1;
                        if (aux.Count >= num && num >= 0)
                        {
                            num = (int) aux[num];
                            break;
                        }

                    }
                }

                cmd.CommandText = "SELECT * from LogEvento where aluguer= @aluguer";
                p1 = new SqlParameter("@aluguer", num);
                cmd.Parameters.Add(p1);
                r1.Close();
                r1 = cmd.ExecuteReader();
                bool nentrou = true;
                while (r1.Read())
                {
                    nentrou = false;
                    Console.WriteLine("Tipo:" + r1.GetValue(4) + " Data:" + r1.GetValue(2) + "\nMensagem: " +
                                      r1.GetValue(3) + "\n");
                }
                if (nentrou)
                    Console.WriteLine("Nao existem eventos para o aluguer escolhido");

                r1.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro na transacao.");
                flag = false;
                throw;
            }
            finally
            {
                session.EndTransaction(flag, tranFlag);
                session.CloseConnection(connFlag);
            }
        }
        private static void Ponto2(int points, int clienteId)
        {
            Boolean flag = true;
            var session = new Session();
            Boolean connFlag = session.OpenConnection();
            Boolean tranFlag = session.BeginTran(IsolationLevel.RepeatableRead);
            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro na transacao.");
                flag = false;
                throw;
            }
            finally
            {
                session.EndTransaction(flag, tranFlag);
                session.CloseConnection(connFlag);
            }

        }

        private static void Ponto3(string empresa)
        {
            Boolean flag = true;
            var session = new Session();
            Boolean connFlag = session.OpenConnection();
            Boolean tranFlag = session.BeginTran(IsolationLevel.RepeatableRead);
            var cmd = session.CreateCommand();
            try
            {
                cmd.CommandText = "SELECT Aluguer.ref a from Aluguer where ((estado = 'finalizado' OR estado = 'cancelado') AND rentacar = @nomeEmp)";
                var p1 = new SqlParameter("@nomeEmp", empresa);
                cmd.Parameters.Add(p1);
                int a1 = (int)cmd.ExecuteScalar();

                if (a1 == 0)
                    Console.WriteLine("Ainda há alugueres em curso associados à empresa seleccionada para eliminação.");
                else
                {
                
                    cmd.CommandText = "DELETE from Aluguer where (rentacar = @nomeEmp)";
                    cmd.Parameters.Add(p1);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE from Veiculo where (rentacar = @nomeEmp)";
                    cmd.Parameters.Add(p1);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE from Precario where (rentacar = @nomeEmp)";
                    cmd.Parameters.Add(p1);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE from Empresa_Aluguer where (rentacar = @nomeEmp)";
                    cmd.Parameters.Add(p1);
                    cmd.ExecuteNonQuery();
                
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro a eliminar uma das tabelas.");
                flag = false;
                throw;
            }
            finally
            {
                session.EndTransaction(flag, tranFlag);
                session.CloseConnection(connFlag);
            }
            Console.WriteLine("A empresa foi eliminada com sucesso.");
        }

        static void Main(string[] args)
        {
            const int pontos = 200, cliente = 4;
            Ponto1(cliente, new DateTime(2000, 1, 1), new DateTime(2019, 2, 13));
//            Console.WriteLine("\n\n\tFoi atribuido " + pontos + " pontos ao cliente " + cliente);
//            Ponto2(pontos, cliente);
//            Console.WriteLine("--------Clique numa tecla---------");
//            Console.ReadKey();
//            Ponto2(-pontos, cliente);
//            Ponto3("Emp Vasco");
        }
    }
}
