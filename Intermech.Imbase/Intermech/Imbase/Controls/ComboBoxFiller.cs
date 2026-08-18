// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.ComboBoxFiller
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions;
using GridViewExtensions.GridFilters;
using GridViewExtensions.GridFilters.EnumerationSources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class ComboBoxFiller : IComboBoxFiller
{
  internal IGridFilter _filter;
  internal DataColumn _column;
  internal bool _refill;
  private Dictionary<string, string> _dict;
  private Action _action;

  public bool Refill
  {
    set => this._refill = value;
  }

  internal ComboBoxFiller(IGridFilter filter, DataColumn column, Dictionary<string, string> dict)
  {
    this._filter = filter;
    this._column = column;
    if (this._filter != null && this._filter.ComboBox != null)
    {
      this._filter.ComboBox.DropDown += new EventHandler(this.OnComboBoxDropDown);
      this._action = new Action(this.SimpleComboAction);
    }
    this._refill = true;
    this._dict = dict;
  }

  public Action Action
  {
    set => this._action = value;
  }

  public void ReadData()
  {
    this._refill = true;
    this.OnComboBoxDropDown((object) null, EventArgs.Empty);
  }

  public void SimpleComboAction() => this._filter.ApplyAutoComplete(this._column);

  public void DictionaryAction()
  {
    if (!(this._filter is EnumerationGridFilter filter) || !(filter.Source is ObjectStringMapEnumerationSource source))
      return;
    source.Clear();
    List<string> distinctValues = GridFilterBase.GetDistinctValues(this._column, out System.Type _, out bool _, out bool _);
    if (this._dict != null)
    {
      if (distinctValues != null)
      {
        foreach (string key in distinctValues)
        {
          if (!string.IsNullOrEmpty(key) && this._dict.ContainsKey(key))
            source.AddMapping((object) key, this._dict[key]);
        }
      }
    }
    else
    {
      foreach (string name in distinctValues)
      {
        if (!string.IsNullOrEmpty(name))
          source.AddMapping((object) name, name);
      }
    }
    filter.SetValues();
  }

  private void OnComboBoxDropDown(object sender, EventArgs e)
  {
    if (!this._refill)
      return;
    this._filter.Lock();
    try
    {
      Action action = this._action;
      if (action != null)
        action();
      this._refill = false;
    }
    finally
    {
      this._filter.UnLock();
    }
  }

  internal static ComboBox FillComboBox(ComboBox comboBox, Dictionary<string, string> dict)
  {
    if (dict != null)
    {
      List<NameValuePair> nameValuePairList = new List<NameValuePair>(comboBox.Items.Count);
      foreach (string str1 in comboBox.Items)
      {
        string str2 = str1.Trim();
        if (!string.IsNullOrWhiteSpace(str2) && dict.ContainsKey(str2))
          nameValuePairList.Add(new NameValuePair(dict[str2], str2));
        else
          nameValuePairList.Add(new NameValuePair(str2, str2));
      }
      bool flag = false;
      foreach (NameValuePair nameValuePair in nameValuePairList)
      {
        if (nameValuePair.Name.Length == 0)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        nameValuePairList.Insert(0, new NameValuePair(string.Empty, string.Empty));
      comboBox.DataSource = (object) null;
      comboBox.Items.Clear();
      comboBox.DisplayMember = "Name";
      comboBox.ValueMember = "Value";
      comboBox.DataSource = (object) nameValuePairList;
      comboBox.SelectedItem = (object) null;
    }
    return comboBox;
  }
}
