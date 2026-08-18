// Decompiled with JetBrains decompiler
// Type: Intermech.Data.SQLite.SQLiteProviderServices
// Assembly: Intermech.StructuredStorages, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8A874F4F-FB0A-412D-88F5-D43E1009C2E5
// Assembly location: D:\IPS\Client\Intermech.StructuredStorages.dll
// XML documentation location: D:\IPS\Client\Intermech.StructuredStorages.xml

using Intermech.ControlFlow;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;


namespace Intermech.Data.SQLite
{
    public class SQLiteProviderServices : ISqlProviderServices
    {
      private readonly SQLiteLastInsertService lastInsertService;

      public SQLiteProviderServices() => this.lastInsertService = new SQLiteLastInsertService();

      public DbProviderFactory Factory => (DbProviderFactory) SQLiteFactory.Instance;

      public bool IsNewDatabase(string connectionString)
      {
        FileInfo fileInfo = connectionString != null ? new FileInfo(new SQLiteConnectionStringBuilder(connectionString).DataSource) : throw new ArgumentNullException(nameof (connectionString));
        return !fileInfo.Exists || fileInfo.Length == 0L;
      }

      public void CreateNewDatabase(string connectionString)
      {
        SQLiteConnectionStringBuilder connectionStringBuilder = connectionString != null ? new SQLiteConnectionStringBuilder(connectionString) : throw new ArgumentNullException(nameof (connectionString));
        connectionStringBuilder.FailIfMissing = false;
        connectionStringBuilder.Pooling = false;
        try
        {
          string connectionString1 = connectionStringBuilder.ToString();
          SingleConnectionPool pool = new SingleConnectionPool(this.Factory, connectionString1);
          try
          {
            using (new DynamicScope())
            {
              DataScope.RequireNew();
              DataScope.OpenConnection((IDbConnectionPool) pool);
              this.CreateDatabaseFile((IDbConnectionPool) pool);
            }
          }
          finally
          {
            pool.ClearPool();
            this.ClearConnectionPool(connectionString1);
          }
        }
        catch
        {
          File.Delete(connectionStringBuilder.DataSource);
          throw;
        }
      }

      private void CreateDatabaseFile(IDbConnectionPool pool)
      {
        using (new DynamicScope())
        {
          DataScope.OpenConnection(pool);
          using (IDbCommand command = DataScope.CreateCommand())
          {
            command.CommandText = "vacuum";
            command.ExecuteNonQuery();
            command.CommandText = "pragma temp_store=FILE";
            command.ExecuteNonQuery();
          }
        }
      }

      public void ClearConnectionPool(string connectionString)
      {
        if (connectionString == null)
          throw new ArgumentNullException(nameof (connectionString));
        if (this.IsNewDatabase(connectionString))
          return;
        using (SQLiteConnection connection = new SQLiteConnection())
        {
          connection.ConnectionString = connectionString;
          connection.Open();
          SQLiteConnection.ClearPool(connection);
        }
      }

      public ISqlLastInsertService TryGetLastInsertService()
      {
        return (ISqlLastInsertService) this.lastInsertService;
      }
    }
}
