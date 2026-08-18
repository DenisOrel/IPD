using Intermech.ControlFlow;
using Intermech.Data.DaoModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Data.SQLite
{
    public sealed class ExplainQueryPlanDaoService : DaoService, IExplainQueryPlanService
    {
      private static readonly LinkedList<string> records = new LinkedList<string>();

      public void ExplainToLog(IDbCommand cmd)
      {
        if (cmd == null)
          throw new ArgumentNullException(nameof (cmd));
        this.RequireStarted();
        using (new DynamicScope())
        {
          DataScope.OpenConnection(this.ConnectionPool);
          using (IDbCommand command = DataScope.CreateCommand())
          {
            command.CommandText = $"explain query plan {cmd.CommandText}";
            foreach (IDbDataParameter parameter in (IEnumerable) cmd.Parameters)
              SqlUtils.CopyParameter(command, parameter);
            using (IDataReader dataReader = command.ExecuteReader())
            {
              lock (ExplainQueryPlanDaoService.records)
              {
                while (dataReader.Read())
                {
                  int int32_1 = dataReader.GetInt32(0);
                  int int32_2 = dataReader.GetInt32(1);
                  int int32_3 = dataReader.GetInt32(2);
                  string str = Convert.ToString(dataReader.GetValue(3));
                  ExplainQueryPlanDaoService.records.AddLast($"query: {cmd.CommandText}");
                  ExplainQueryPlanDaoService.records.AddLast($"{int32_1:00} | {int32_2:00} | {int32_3:00} | {str}");
                }
              }
            }
          }
        }
      }

      public List<string> GetLog()
      {
        this.RequireStarted();
        List<string> log;
        lock (ExplainQueryPlanDaoService.records)
        {
          log = new List<string>(ExplainQueryPlanDaoService.records.Count);
          foreach (string record in ExplainQueryPlanDaoService.records)
            log.Add(record);
        }
        return log;
      }
    }
}
