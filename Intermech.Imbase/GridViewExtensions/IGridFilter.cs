// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.IGridFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions;

public interface IGridFilter : IDisposable
{
  event EventHandler Changed;

  Control FilterControl { get; }

  bool ApplyAutoComplete(DataColumn column);

  bool HasFilter { get; }

  bool UseCustomFilterPlacement { get; set; }

  ComboBox ComboBox { get; }

  string GetFilterText(string columnName);

  ConditionItem GetFilter(string columnName);

  void SetFilter(ConditionItem filter);

  void Clear();

  void Lock();

  void UnLock();
}
