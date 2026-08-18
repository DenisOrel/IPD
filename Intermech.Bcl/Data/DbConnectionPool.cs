
// Type: Intermech.Data.DbConnectionPool
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace Intermech.Data
{
    public class DbConnectionPool : IDbConnectionPool
    {
      private readonly DbProviderFactory factory;
      private readonly string connectionString;
      private readonly int maxPoolSize;
      private readonly LinkedList<IDbConnection> connectionPool;

      public DbConnectionPool(DbProviderFactory factory, string connectionString, int maxPoolSize)
      {
        if (factory == null)
          throw new ArgumentNullException(nameof (factory));
        if (connectionString == null)
          throw new ArgumentNullException("cstr");
        if (maxPoolSize <= 0)
          throw new ArgumentOutOfRangeException(nameof (maxPoolSize));
        this.factory = factory;
        this.connectionString = connectionString;
        this.maxPoolSize = maxPoolSize;
        this.connectionPool = new LinkedList<IDbConnection>();
      }

      public void ClearPool()
      {
        lock (this.connectionPool)
        {
          foreach (IDisposable disposable in this.connectionPool)
            DisposeUtils.SafelyDispose(disposable);
          this.connectionPool.Clear();
        }
      }

      public IDbConnection AllocateConnection()
      {
        lock (this.connectionPool)
        {
          if (this.connectionPool.Count > 0)
          {
            IDbConnection dbConnection = this.connectionPool.First.Value;
            this.connectionPool.RemoveFirst();
            return dbConnection;
          }
          DbConnection connection = this.factory.CreateConnection();
          connection.ConnectionString = this.connectionString;
          connection.Open();
          return (IDbConnection) connection;
        }
      }

      public void ReleaseConnection(IDbConnection connection)
      {
        if (connection == null)
          throw new ArgumentNullException(nameof (connection));
        if (connection.State != ConnectionState.Open)
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1672"));
        lock (this.connectionPool)
        {
          if (this.connectionPool.Count < this.maxPoolSize)
          {
            this.connectionPool.AddFirst(connection);
            return;
          }
        }
        connection.Close();
      }
    }
}
