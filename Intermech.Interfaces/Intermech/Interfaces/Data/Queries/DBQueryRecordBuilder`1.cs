
// Type: Intermech.Interfaces.Data.Queries.DBQueryRecordBuilder`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces.Data.Queries
{
    public abstract class DBQueryRecordBuilder<TResult>
    {
      private IDBQuery query;

      public void AttachQuery(IDBQuery query)
      {
        if (query == null)
          throw new ArgumentNullException(nameof (query));
        if (this.query != null)
          throw new InvalidOperationException($"Объект '{this.GetType()}' уже был подключен к запросу.");
        try
        {
          this.query = query;
          this.DoAttachQuery();
        }
        catch
        {
          this.DoDetachQuery();
          throw;
        }
      }

      protected virtual void DoAttachQuery()
      {
      }

      protected void CheckAttached()
      {
        if (this.query == null)
          throw new InvalidOperationException($"Объект '{this.GetType()}' не был подключен к запросу.");
      }

      public void DetachQuery()
      {
        this.CheckAttached();
        this.DoDetachQuery();
      }

      protected virtual void DoDetachQuery() => this.query = (IDBQuery) null;

      public TResult Build(DataRow row)
      {
        if (row == null)
          throw new ArgumentNullException(nameof (row));
        this.CheckAttached();
        return this.DoBuild(row);
      }

      protected abstract TResult DoBuild(DataRow row);

      protected object Read(DataRow row, DBQueryAttribute attribute)
      {
        if (row == null)
          throw new ArgumentNullException(nameof (row));
        if (attribute == null)
          throw new ArgumentNullException(nameof (attribute));
        this.CheckAttached();
        int index = this.Query.Attributes.GetIndex(attribute);
        return row[index];
      }

      protected IDBQuery Query => this.query;
    }
}
