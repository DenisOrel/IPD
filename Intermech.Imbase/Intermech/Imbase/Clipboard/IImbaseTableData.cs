// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Clipboard.IImbaseTableData
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Data;

#nullable disable
namespace Intermech.Imbase.Clipboard;

internal interface IImbaseTableData
{
  DataSet DataSet { get; }

  long TableId { get; }

  long LinkId { get; }
}
