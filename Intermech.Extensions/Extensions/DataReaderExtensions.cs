// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DataReaderExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Extensions;

public static class DataReaderExtensions
{
  [NotNull]
  [ItemNotNull]
  public static IEnumerable<IDataRecord> Enumerate([NotNull] this IDataReader dataReader)
  {
    while (dataReader.Read())
      yield return (IDataRecord) dataReader;
  }

  [NotNull]
  [ItemNotNull]
  public static IEnumerable<IEnumerable<IDataRecord>> BatchEnumerate([NotNull] this IDataReader dataReader)
  {
    do
    {
      yield return dataReader.Enumerate();
    }
    while (dataReader.NextResult());
  }
}
