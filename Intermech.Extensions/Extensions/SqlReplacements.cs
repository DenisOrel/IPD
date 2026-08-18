// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SqlReplacements
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Text;

#nullable disable
namespace Intermech.Extensions;

public class SqlReplacements : ISqlReplacements, IDisposable
{
  [NotNull]
  private IDbCommandEx _dbCommand;

  public SqlReplacements([NotNull] IDbCommandEx dbCommand) => this._dbCommand = dbCommand;

  public string this[[NotNull] string replaceWhat]
  {
    set
    {
      this._dbCommand.CommandText = new StringBuilder(this._dbCommand.CommandText).Replace(replaceWhat[0] == '{' ? replaceWhat : $"{{{replaceWhat}}}", value).ToString();
    }
  }

  public void Dispose() => this._dbCommand = (IDbCommandEx) null;
}
