// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ProjectGridTextBoxColumn
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>A project grid text box column.</summary>
public class ProjectGridTextBoxColumn : 
  DataGridViewTextBoxColumn,
  IComponent,
  IDisposable,
  ICloneable
{
  public ProjectGridTextBoxColumn()
  {
    this.HeaderCell = (DataGridViewColumnHeaderCell) new ProjectGridColumnHeaderCell();
  }
}
