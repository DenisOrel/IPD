// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbDataProviderKey
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using System;

#nullable disable
namespace Intermech.Server.Data;

internal sealed class DbDataProviderKey : IEquatable<DbDataProviderKey>
{
  private static readonly StringComparer ordStringComparer = StringComparer.OrdinalIgnoreCase;

  public DbDataProviderKey(string dataProviderName, string connectionString)
  {
    if (dataProviderName == null)
      throw new ArgumentNullException(nameof (dataProviderName));
    if (connectionString == null)
      throw new ArgumentNullException(nameof (connectionString));
    this.DataProviderName = dataProviderName;
    this.ConnectionString = connectionString;
  }

  public string DataProviderName { get; }

  public string ConnectionString { get; }

  public bool Equals(DbDataProviderKey other)
  {
    return other != null && DbDataProviderKey.ordStringComparer.Equals(this.DataProviderName, other.DataProviderName) && DbDataProviderKey.ordStringComparer.Equals(this.ConnectionString, other.ConnectionString);
  }

  public override bool Equals(object obj)
  {
    return !(obj is DbDataProviderKey other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode()
  {
    return (932214115 * -1521134295 + DbDataProviderKey.ordStringComparer.GetHashCode(this.DataProviderName)) * -1521134295 + DbDataProviderKey.ordStringComparer.GetHashCode(this.ConnectionString);
  }

  public override string ToString() => $"[{this.DataProviderName},{this.ConnectionString}]";
}
