using System;
using System.Collections.Generic;
using System.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GravesConsultingLLC.RiskManager.Core.Infrastructure
{
    public class Repository : IDisposable, GravesConsultingLLC.RiskManager.Core.Infrastructure.IRepository
    {
        private IDbConnection _DBConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["RMConnectionString"].ConnectionString);

        private IDbTransaction _DBTransaction;

        public Repository()
        {
            _DBConnection.Open();
        }

        public IEnumerable<T> Get<T>(string Procedure, Dictionary<string, object> Parameters, bool IsProcedure)
        {
            return
                _DBConnection.Query<T>(
                    Procedure, 
                    Parameters,
                    commandType: IsProcedure ? CommandType.StoredProcedure : CommandType.Text
            );
        }

        public T GetSingle<T>(string Procedure, Dictionary<string, object> Parameters, bool IsProcedure)
        {
            return
                _DBConnection.Query<T>(
                    Procedure, 
                    Parameters, 
                    commandType: IsProcedure ? CommandType.StoredProcedure : CommandType.Text
            ).FirstOrDefault<T>();
        }

        public T GetScalar<T>(string Procedure, Dictionary<string, object> Parameters, bool IsProcedure)
        {
            return
                _DBConnection.ExecuteScalar<T>(
                    Procedure,
                    Parameters,
                    commandType: IsProcedure ? CommandType.StoredProcedure : CommandType.Text
            );
        }

        public IDataReader GetReader(string Procedure, Dictionary<string, object> Parameters, bool IsProcedure)
        {
            return
                _DBConnection.ExecuteReader(
                    new CommandDefinition(
                        Procedure,
                        Parameters,
                        commandType: IsProcedure ? CommandType.StoredProcedure : CommandType.Text
                        ),
                        CommandBehavior.CloseConnection
                );
        }

        public void Put(string Procedure, Dictionary<string, object> Parameters)
        {
            _DBConnection.Execute(
                Procedure, 
                Parameters,
                transaction: _DBTransaction != null ? _DBTransaction : null,
                commandType: CommandType.StoredProcedure
            );
        }

        public T Put<T>(string Procedure, Dictionary<string, object> Parameters, string OutputParameter)
        {

            //Convert to DynamicParameters to define output parameter 
            DynamicParameters Params = new DynamicParameters(Parameters);
            
            //Add output parameter
            Params.Add(
                OutputParameter,
                dbType: GetSqlDBTypeFromString(typeof(T).Name), 
                direction: ParameterDirection.Output
            );

            _DBConnection.Execute(
                Procedure,
                Params, 
                transaction: _DBTransaction != null ? _DBTransaction : null,
                commandType: CommandType.StoredProcedure
            );

            return 
                Params.Get<T>(OutputParameter);
        }

        private DbType GetSqlDBTypeFromString(string OutputParameterType)
        {
            return (DbType)Enum.Parse(typeof(DbType), OutputParameterType, true);
        }

        public void BeginTransaction()
        {
            _DBTransaction = _DBConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_DBTransaction != null)
            {
                _DBTransaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (_DBTransaction != null)
            {
                _DBTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            if (_DBConnection != null && _DBConnection.State != ConnectionState.Closed)
            {
                _DBConnection.Close();
            }
        }
    }
}
