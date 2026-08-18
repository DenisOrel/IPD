// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.FilterItem
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal class FilterItem : ListViewItem, IComparable
{
  [NotNull]
  public readonly TaskFilter Filter;

  public FilterItem([NotNull] TaskFilter filter)
  {
    this.Filter = filter;
    this.Text = this.Filter.ToString();
  }

  public override int GetHashCode()
  {
    return this.Filter == null ? base.GetHashCode() : this.Filter.GetHashCode();
  }

  public override bool Equals(object obj)
  {
    TaskFilter filter = obj is FilterItem filterItem ? filterItem.Filter : (TaskFilter) null;
    return filter == null ? base.Equals(obj) : filter.Equals((object) this.Filter);
  }

  public override string ToString() => this.Filter.ToString();

  public int CompareTo([CanBeNull] object obj)
  {
    return this.Filter.CompareTo(obj is FilterItem filterItem ? (object) filterItem.Filter : (object) (TaskFilter) null);
  }
}
