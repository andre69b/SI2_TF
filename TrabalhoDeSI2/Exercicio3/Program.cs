using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Exercicio3
{
    class Program
    {

        public static void Ponto1(int cliente, DateTime inicio, DateTime fim)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions {IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted}))
            {
                using (var ctx = new SI2_1314i_TPEntities())
                {
                    var q1 = (from a in ctx.Aluguer
                        where a.cliente == cliente && a.datahora_recolha >= inicio && a.datahora_entrega <= fim
                        select a
                        );
                    int index = 1;
                    Console.WriteLine("Lista de alugueres: ");
                    var aux = new ArrayList();
                    foreach (var c in q1)
                    {
                        aux.Add(c.@ref);
                        Console.WriteLine((index++) + "º - aluguer:" + c.@ref);
                    }
                    if (index == 1)
                    {
                        Console.WriteLine("Nao existem Alugueres para o Cliente escolhido");
                        return;
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
                    var q2 = (from b in ctx.LogEvento
                        where b.aluguer == num
                        select b);
                    bool nentrou = true;
                    foreach (var c in q2)
                    {
                        nentrou = false;
                        Console.WriteLine("Tipo:" + c.tipo + " Data:" + c.data + "\nMensagem: " + c.mensagem + "\n");
                    }
                    if (nentrou)
                        Console.WriteLine("Nao existem eventos para o aluguer escolhido");
                }
            }
        }

        private static void ponto2(int points, int clienteId)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions {IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead}))
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
        }

        private static void ponto3(string empresa) {
            using (var ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions {IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead}))
            {
                using (var ctx = new SI2_1314i_TPEntities())
                {
                    bool blankCheck = ctx.Aluguer.Any(a => (a.estado == "finalizado" || a.estado == "cancelado")
                                                            && a.rentacar == empresa);
                    if (!blankCheck)
                        Console.WriteLine(
                            "Ainda há alugueres em curso associados à empresa seleccionada para eliminação.");
                    else
                    {
                        var q = (from a in ctx.Veiculo
                            where a.rentacar == empresa
                            select a);

                        foreach (var veiculo in q) ctx.Veiculo.Remove(veiculo);

                        var f = (from a in ctx.Aluguer
                            where a.rentacar == empresa
                            select a);

                        foreach (var aluguer in f) ctx.Aluguer.Remove(aluguer);

                        var e = (from a in ctx.Precario
                            where a.rentacar == empresa
                            select a);

                        foreach (var precario in e) ctx.Precario.Remove(precario);

                        var res = ctx.Empresa_Aluguer.First(n => n.nome == empresa);
                        ctx.Empresa_Aluguer.Remove(res);
                        Console.WriteLine("A empresa foi removida com sucesso");
                        ctx.SaveChanges();
                    }
                }
            }

        }
        static void Main(string[] args)
        {
            /*const int pontos = 200, cliente = 4;
            Ponto1(cliente, new DateTime(2000, 1, 1), new DateTime(2019, 2, 13));
            Console.WriteLine("\n\n\tFoi atribuido " + pontos + " pontos ao cliente " + cliente);
            ponto2(pontos, cliente);
            Console.WriteLine("--------Clique numa tecla---------");
            Console.ReadKey();
            ponto2(-pontos, cliente);*/
            string empName = "Emp Vasco";
            ponto3(empName);

        }
    }
}
