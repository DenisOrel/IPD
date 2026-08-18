// Decompiled with JetBrains decompiler
// Type: Intermech.Diagnostics.EmptyDataTableException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Diagnostics;

[Serializable]
public class EmptyDataTableException : InvalidOperationException
{
  [CanBeNull]
  public DataTable DataTable { get; }

  public EmptyDataTableException()
  {
  }

  public EmptyDataTableException([CanBeNull, CanBeEmpty, InvokerParameterName] DataTable dataTable, [CanBeNull, CanBeEmpty] string message = null)
    : base(message)
  {
    this.DataTable = dataTable;
  }

  [CanBeNull]
  protected string OriginalMessage => base.Message;

  [NotNull]
  public override string Message => "Таблица данных пуста!";
}
