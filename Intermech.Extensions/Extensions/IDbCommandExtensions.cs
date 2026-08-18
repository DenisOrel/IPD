// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDbCommandExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Extensions;

public static class IDbCommandExtensions
{
  [NotNull]
  [ItemNotNull]
  public static IEnumerable<IDataRecord> Query(
    [NotNull] this IDbCommand dbCommand,
    [NotNull] params object[] paramValues)
  {
    int num = 0;
    foreach (object paramValue in paramValues)
      ((IDataParameter) dbCommand.Parameters[num++]).Value = paramValue;
    using (IDataReader dataReader = dbCommand.ExecuteReader())
    {
      while (dataReader.Read())
        yield return (IDataRecord) dataReader;
      dataReader.Close();
    }
  }

  public static int ExecSql([NotNull] this IDbCommand dbCommand, [NotNull] params object[] paramValues)
  {
    int num = 0;
    foreach (object paramValue in paramValues)
      ((IDataParameter) dbCommand.Parameters[num++]).Value = paramValue;
    return dbCommand.ExecuteNonQuery();
  }
}
