// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.EnhDataGridViewTextBoxEditingControl
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class EnhDataGridViewTextBoxEditingControl : 
  DataGridViewTextBoxEditingControl,
  IDataGridViewEditingControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  public override void PrepareEditingControlForEdit(bool selectAll)
  {
    base.PrepareEditingControlForEdit(selectAll);
    if (!(this.EditingControlDataGridView is ProjectDataGridView controlDataGridView) || !controlDataGridView._IsMouseDown)
      return;
    Point client = this.PointToClient(Control.MousePosition);
    this.SelectionStart = 0;
    int indexFromPosition = this.GetCharIndexFromPosition(client);
    if (indexFromPosition == this.TextLength - 1)
    {
      Size size = TextRenderer.MeasureText(this.Text[indexFromPosition].ToString(), this.Font);
      if (client.X > this.GetPositionFromCharIndex(this.TextLength - 1).X + size.Width / 2 - 5)
        ++indexFromPosition;
    }
    this.SelectionStart = indexFromPosition;
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (e.KeyCode != Keys.F4 || e.Shift || e.Control || e.Alt || !(this is IPopupFormEditingControl formEditingControl))
      return;
    formEditingControl.ShowForm();
  }
}
