// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.IGridFilterFactory
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions;

public interface IGridFilterFactory
{
  event EventHandler Changed;

  event GridFilterEventHandler GridFilterCreated;

  void BeginGridFilterCreation();

  void EndGridFilterCreation();

  IGridFilter CreateGridFilter(DataGridViewColumn column);
}
