// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.TransactionEventArgs
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class TransactionEventArgs : CancelEventArgs
{
  protected DataTableTransactionRecord _record;

  public DataTableTransactionRecord Record => this._record;

  public TransactionEventArgs(DataTableTransactionRecord record)
    : base(false)
  {
    this._record = record;
  }
}
