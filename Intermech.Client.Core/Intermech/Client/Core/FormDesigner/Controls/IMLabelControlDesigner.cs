
// Type: Intermech.Client.Core.FormDesigner.Controls.IMLabelControlDesigner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[Serializable]
internal class IMLabelControlDesigner : ControlDesigner
{
  /// <summary>
  /// 
  /// </summary>
  public override SelectionRules SelectionRules
  {
    get
    {
      SelectionRules selectionRules = base.SelectionRules;
      object component = (object) this.Component;
      PropertyDescriptor property = TypeDescriptor.GetProperties(component)["AutoSize"];
      if (property != null && (bool) property.GetValue(component))
        selectionRules &= ~SelectionRules.AllSizeable;
      return selectionRules;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pe"></param>
  protected override void OnPaintAdornments(PaintEventArgs pe)
  {
    Color backColor = this.Control.BackColor;
    int int32_1 = Convert.ToInt32((double) backColor.R * 0.5);
    int int32_2 = Convert.ToInt32((double) backColor.G * 0.5);
    int int32_3 = Convert.ToInt32((double) backColor.B * 0.5);
    int green = int32_2;
    int blue = int32_3;
    using (Pen pen = new Pen(Color.FromArgb(int32_1, green, blue), 1f))
    {
      pen.DashStyle = DashStyle.Dash;
      pe.Graphics.DrawRectangle(pen, 0, 0, this.Control.Width - 1, this.Control.Height - 1);
    }
  }
}
