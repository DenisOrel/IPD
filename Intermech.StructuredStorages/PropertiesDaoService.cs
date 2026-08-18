// Decompiled with JetBrains decompiler
// Type: Intermech.Data.SQLite.PropertiesDaoService
// Assembly: Intermech.StructuredStorages, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8A874F4F-FB0A-412D-88F5-D43E1009C2E5
// Assembly location: D:\IPS\Client\Intermech.StructuredStorages.dll
// XML documentation location: D:\IPS\Client\Intermech.StructuredStorages.xml

using Intermech.ControlFlow;
using Intermech.Data.DaoModel;
using System.Data;


namespace Intermech.Data.SQLite
{
    public sealed class PropertiesDaoService(string tableName) : AbstractPropertiesDaoService(tableName)
    {
      protected override void RunMaintenance(DbMaintenanceInfo info)
      {
        base.RunMaintenance(info);
        this.MigrateMetadata(info);
      }

      private void MigrateMetadata(DbMaintenanceInfo info)
      {
        using (new DynamicScope())
        {
          DataScope.OpenConnection(this.ConnectionPool);
          using (IDbCommand command = DataScope.CreateCommand())
          {
            command.CommandText = $"create table if not exists {this.TableName} (PROP_NAME text primary key collate LOCALIZED_CI, PROP_VALUE text)";
            command.ExecuteNonQuery();
          }
        }
      }
    }
}
