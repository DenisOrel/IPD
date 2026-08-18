// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ITableRowSelector
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Controls;

#nullable disable
namespace Intermech.Imbase.Views;

internal interface ITableRowSelector
{
  event TableView.RowSelecting Selecting;
}
