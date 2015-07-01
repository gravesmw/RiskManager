using System;
namespace GravesConsultingLLC.RiskManager.Core.Infrastructure
{
    public interface IRepository
    {
        void BeginTransaction();
        void CommitTransaction();
        void Dispose();
        System.Collections.Generic.IEnumerable<T> Get<T>(string Procedure, System.Collections.Generic.Dictionary<string, object> Parameters, bool IsProcedure);
        T GetScalar<T>(string Procedure, System.Collections.Generic.Dictionary<string, object> Parameters, bool IsProcedure);
        T GetSingle<T>(string Procedure, System.Collections.Generic.Dictionary<string, object> Parameters, bool IsProcedure);
        void Put(string Procedure, System.Collections.Generic.Dictionary<string, object> Parameters);
        T Put<T>(string Procedure, System.Collections.Generic.Dictionary<string, object> Parameters, string OutputParameter);
        void RollbackTransaction();
    }
}
