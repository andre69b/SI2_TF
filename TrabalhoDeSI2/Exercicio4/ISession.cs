using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio3
{
    public interface ISession : IDisposable
    {

        bool BeginTran();
        bool OpenConnection();
        void EndTransaction(bool MyVote, bool isMyTransaction);
        void CloseConnection(bool isMyTransaction);

        SqlCommand CreateCommand();
    }
}
