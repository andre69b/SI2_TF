using System;
using System.Data;
using System.Data.SqlClient;

namespace Exercicio4
{
    public interface ISession : IDisposable
    {

        bool BeginTran(IsolationLevel ilevel);
        bool OpenConnection();
        void EndTransaction(bool MyVote, bool isMyTransaction);
        void CloseConnection(bool isMyTransaction);

        SqlCommand CreateCommand();
    }
}
