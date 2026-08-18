// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.UserRowSelector
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Views;

public class UserRowSelector : ITableRowSelector
{
  internal static UserRowSelector Instance = new UserRowSelector();
  private bool _enabled;

  internal bool Enabled
  {
    get => this._enabled;
    set => this._enabled = value;
  }

  public event TableView.RowSelecting Selecting;

  internal bool OnSelectingRow(AttributeTypeProperties[] properties, DataRow row)
  {
    bool flag = true;
    if (this._enabled && this.Selecting != null)
    {
      foreach (TableView.RowSelecting invocation in this.Selecting.GetInvocationList())
      {
        flag = invocation(properties, row);
        if (!flag)
          break;
      }
    }
    return flag;
  }
}
