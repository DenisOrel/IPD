// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.EnhDataGridViewTextBoxColumn
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class EnhDataGridViewTextBoxColumn : ProjectGridColumn, IComponent, IDisposable, ICloneable
{
  public EnhDataGridViewTextBoxColumn()
    : base((DataGridViewCell) new EnhDataGridViewTextBoxCell())
  {
  }

  /// <summary>
  /// Максимальная длина текста, разрешенная для ввода в поле ввода при редактировании
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int MaxWidth
  {
    get
    {
      return !(this.CellTemplate is DataGridViewTextBoxCell cellTemplate) ? 0 : cellTemplate.MaxInputLength;
    }
    set
    {
      if (!(this.CellTemplate is DataGridViewTextBoxCell cellTemplate) || value == 0)
        return;
      cellTemplate.MaxInputLength = value;
    }
  }
}
