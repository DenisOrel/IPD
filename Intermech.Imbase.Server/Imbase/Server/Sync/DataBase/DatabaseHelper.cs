// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.DataBase.DatabaseHelper
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces.Imbase.Params.CommonParams;

#nullable disable
namespace Intermech.Imbase.Server.Sync.DataBase;

public class DatabaseHelper
{
  public static IDataBase GetDataBase(
    BaseType type,
    string server,
    string database,
    string user,
    string password)
  {
    switch (type)
    {
      case BaseType.Interbase:
        return (IDataBase) new InterbaseBase(server, database, user, password);
      case BaseType.MSSQL:
        return (IDataBase) new MssqlBase(server, database, user, password);
      case BaseType.Oracle:
        return (IDataBase) new OracleBase(server, user, password);
      default:
        return (IDataBase) null;
    }
  }

  public static string GetDataBaseCaption(BaseType type, string server, string database)
  {
    switch (type)
    {
      case BaseType.Interbase:
        return new InterbaseBase(server, database, string.Empty, string.Empty).Caption;
      case BaseType.MSSQL:
        return new MssqlBase(server, database, string.Empty, string.Empty).Caption;
      case BaseType.Oracle:
        return new OracleBase(server, string.Empty, string.Empty).Caption;
      default:
        return string.Empty;
    }
  }
}
