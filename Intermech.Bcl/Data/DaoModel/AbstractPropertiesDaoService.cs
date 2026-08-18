
// Type: Intermech.Data.DaoModel.AbstractPropertiesDaoService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Data.DaoModel
{
    public abstract class AbstractPropertiesDaoService : DaoService, IPropertiesService
    {
      private readonly string tableName;
      private readonly string readCommand;
      private readonly string insertCommand;
      private readonly string updateCommand;

      protected AbstractPropertiesDaoService(string tableName)
      {
        this.tableName = tableName != null ? tableName : throw new ArgumentNullException(nameof (tableName));
        this.readCommand = $"select PROP_VALUE from {tableName} where PROP_NAME = @name";
        this.insertCommand = $"insert into {tableName} (PROP_NAME, PROP_VALUE) values (@name, @value)";
        this.updateCommand = $"update {tableName} set PROP_VALUE = @value where PROP_NAME = @name";
      }

      protected string TableName => this.tableName;

      public string ReadProperty(string name, string defaultValue)
      {
        AbstractPropertiesDaoService.CheckName(name);
        this.RequireStarted();
        return this.ReadPropertyCore(name, defaultValue);
      }

      private string ReadPropertyCore(string name, string defaultValue)
      {
        using (new DynamicScope())
        {
          DataScope.OpenConnection(this.ConnectionPool);
          using (IDbCommand command = DataScope.CreateCommand())
          {
            command.CommandText = this.readCommand;
            SqlUtils.MakeParameter(command, nameof (name), DbType.String).Value = (object) name;
            object obj = command.ExecuteScalar();
            return obj == null || Convert.IsDBNull(obj) ? defaultValue : Convert.ToString(obj);
          }
        }
      }

      public void WriteProperty(string name, string value)
      {
        AbstractPropertiesDaoService.CheckName(name);
        this.RequireStarted();
        this.WritePropertyCore(name, value);
      }

      private void WritePropertyCore(string name, string value)
      {
        using (new DynamicScope())
        {
          DataScope.OpenConnection(this.ConnectionPool);
          IDbCommand command1;
          int num;
          using (command1 = DataScope.CreateCommand())
          {
            command1.CommandText = this.updateCommand;
            SqlUtils.MakeParameter(command1, nameof (name), DbType.String).Value = (object) name;
            SqlUtils.MakeParameter(command1, nameof (value), DbType.String).Value = (object) value;
            num = command1.ExecuteNonQuery();
          }
          if (num != 0)
            return;
          IDbCommand command2;
          using (command2 = DataScope.CreateCommand())
          {
            command2.CommandText = this.insertCommand;
            SqlUtils.MakeParameter(command2, nameof (name), DbType.String).Value = (object) name;
            SqlUtils.MakeParameter(command2, nameof (value), DbType.String).Value = (object) value;
            command2.ExecuteNonQuery();
          }
        }
      }

      private static void CheckName(string name)
      {
        if (string.IsNullOrEmpty(name))
          throw new ArgumentException(LocalizationHolder.rm.GetString("SR_1676"), nameof (name));
      }
    }
}
