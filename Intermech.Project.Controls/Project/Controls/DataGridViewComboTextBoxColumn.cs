// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DataGridViewComboTextBoxColumn
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal class DataGridViewComboTextBoxColumn : 
  DataGridViewColumn,
  IComponent,
  IDisposable,
  ICloneable
{
  public DataGridViewComboTextBoxColumn()
    : base((DataGridViewCell) new DataGridViewComboTextBoxCell())
  {
  }

  [CanBeNull]
  public override DataGridViewCell CellTemplate
  {
    get => base.CellTemplate;
    set
    {
      base.CellTemplate = value == null || value.GetType().IsAssignableFrom(typeof (DataGridViewComboTextBoxCell)) ? value : throw new InvalidCastException("Cell type must be based upon DataGridViewComboTextBoxCell.");
    }
  }
}
