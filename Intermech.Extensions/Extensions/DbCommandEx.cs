// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DbCommandEx
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Data;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class DbCommandEx : IDbCommandEx, IDbCommand, IDisposable
{
  [NotNull]
  private readonly IDbCommand _dbCommand;
  [CanBeNull]
  private string _baseCommandText;
  [CanBeNull]
  private ISqlReplacements _dbReplaces;
  private int _lockReplacementsCounter;

  public DbCommandEx([NotNull] IDbCommand dbCommand)
  {
    this._dbCommand = dbCommand;
    this._dbCommand.CommandTimeout = 360;
  }

  [NotNull]
  public string BaseCommandText
  {
    get
    {
      return this._baseCommandText ?? (this._baseCommandText = this._dbCommand.CommandText) ?? string.Empty;
    }
  }

  [NotNull]
  public ISqlReplacements SqlReplacements
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._dbReplaces == null)
      {
        this._baseCommandText = this._dbCommand.CommandText;
        this._dbReplaces = (ISqlReplacements) new Intermech.Extensions.SqlReplacements((IDbCommandEx) this);
      }
      return this._dbReplaces;
    }
  }

  public void Dispose()
  {
    if (this._dbReplaces != null)
      this._dbReplaces.Dispose();
    this._dbCommand.Dispose();
    this._baseCommandText = (string) null;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Prepare() => this._dbCommand.Prepare();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Cancel()
  {
    try
    {
      this._dbCommand.Cancel();
    }
    finally
    {
      if (this._baseCommandText != null && this._lockReplacementsCounter == 0)
        this._dbCommand.CommandText = this._baseCommandText;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IDbDataParameter CreateParameter() => this._dbCommand.CreateParameter();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void LockSqlReplacements() => ++this._lockReplacementsCounter;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void UnlockSqlReplacements()
  {
    if (this._lockReplacementsCounter <= 0 || --this._lockReplacementsCounter != 0 || this._baseCommandText == null)
      return;
    this._dbCommand.CommandText = this._baseCommandText;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int ExecuteNonQuery()
  {
    try
    {
      return this._dbCommand.ExecuteNonQuery();
    }
    finally
    {
      if (this._baseCommandText != null && this._lockReplacementsCounter == 0)
        this._dbCommand.CommandText = this._baseCommandText;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IDataReader ExecuteReader()
  {
    try
    {
      return this._dbCommand.ExecuteReader();
    }
    finally
    {
      if (this._baseCommandText != null && this._lockReplacementsCounter == 0)
        this._dbCommand.CommandText = this._baseCommandText;
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IDataReader ExecuteReader(CommandBehavior behavior)
  {
    try
    {
      return this._dbCommand.ExecuteReader(behavior);
    }
    finally
    {
      if (this._baseCommandText != null && this._lockReplacementsCounter == 0)
        this._dbCommand.CommandText = this._baseCommandText;
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public object ExecuteScalar()
  {
    try
    {
      return this._dbCommand.ExecuteScalar();
    }
    finally
    {
      if (this._baseCommandText != null && this._lockReplacementsCounter == 0)
        this._dbCommand.CommandText = this._baseCommandText;
    }
  }

  [CanBeNull]
  public IDbConnection Connection
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dbCommand.Connection;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._dbCommand.Connection = value;
  }

  [CanBeNull]
  public IDbTransaction Transaction
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dbCommand.Transaction;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._dbCommand.Transaction = value;
  }

  [CanBeNull]
  public string CommandText
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dbCommand.CommandText;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._dbCommand.CommandText = value;
  }

  public int CommandTimeout
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dbCommand.CommandTimeout;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._dbCommand.CommandTimeout = value;
    }
  }

  public CommandType CommandType
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dbCommand.CommandType;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._dbCommand.CommandType = value;
  }

  [NotNull]
  public IDataParameterCollection Parameters
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Intermech.Diagnostics.Check.Result.NotNull<IDataParameterCollection>(this._dbCommand.Parameters);
    }
  }

  public UpdateRowSource UpdatedRowSource
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dbCommand.UpdatedRowSource;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._dbCommand.UpdatedRowSource = value;
    }
  }
}
