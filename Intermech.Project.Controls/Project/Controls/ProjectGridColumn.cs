// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ProjectGridColumn
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

/// <summary>A project grid column.</summary>
public class ProjectGridColumn : DataGridViewColumn, IComponent, IDisposable, ICloneable
{
  public ProjectGridColumn()
    : this((DataGridViewCell) new DataGridViewTextBoxCell())
  {
  }

  public ProjectGridColumn([NotNull] DataGridViewCell cellTemplate)
    : base(cellTemplate)
  {
    this.HeaderCell = (DataGridViewColumnHeaderCell) new ProjectGridColumnHeaderCell();
  }
}
