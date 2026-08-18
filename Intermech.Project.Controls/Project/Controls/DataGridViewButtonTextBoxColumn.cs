// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DataGridViewButtonTextBoxColumn
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

public class DataGridViewButtonTextBoxColumn : ProjectGridColumn, IComponent, IDisposable, ICloneable
{
  public DataGridViewButtonTextBoxColumn()
    : base((DataGridViewCell) new DataGridViewTextBoxCell())
  {
  }

  public DataGridViewButtonTextBoxColumn([NotNull] DataGridViewCell cellTemplate)
    : base(cellTemplate)
  {
  }
}
