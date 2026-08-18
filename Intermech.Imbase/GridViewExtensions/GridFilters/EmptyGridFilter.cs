// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.EmptyGridFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class EmptyGridFilter : GridFilterBase
{
  private Control _control;

  public EmptyGridFilter()
    : base(false)
  {
    this._control = new Control();
  }

  public override Control FilterControl => this._control;

  public override ComboBox ComboBox => (ComboBox) null;

  public override bool ApplyAutoComplete(DataColumn column) => true;

  public override bool HasFilter => false;

  public override string GetFilterText(string columnName) => "";

  public override ConditionItem GetFilter(string columnName) => (ConditionItem) null;

  public override void SetFilter(ConditionItem filter)
  {
  }

  public override void Clear()
  {
  }
}
