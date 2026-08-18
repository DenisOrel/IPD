// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ComboBoxEx
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for ComboBoxEx.</summary>
public class ComboBoxEx : ComboBox
{
  private ImageList imageList;
  private const int _pendingPadding = -999;
  private int _itemPadding = -999;

  public ImageList ImageList
  {
    get => this.imageList;
    set
    {
      this.imageList = value;
      if (this.DesignMode || this.imageList == null || this.ItemHeight >= this.imageList.ImageSize.Height)
        return;
      this.ItemHeight = this.imageList.ImageSize.Height;
    }
  }

  public ComboBoxEx() => this.DrawMode = DrawMode.OwnerDrawFixed;

  protected override void OnDrawItem(DrawItemEventArgs ea)
  {
    if (this.imageList != null)
    {
      if (this._itemPadding == -999)
        this._itemPadding = (this.ItemHeight - this.imageList.ImageSize.Height) / 2;
      ea.DrawBackground();
      ea.DrawFocusRectangle();
      Size imageSize = this.imageList.ImageSize;
      Rectangle bounds = ea.Bounds;
      try
      {
        if (ea.Index == -1)
        {
          using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
            ea.Graphics.DrawString(this.Text, ea.Font, (Brush) solidBrush, (float) (bounds.Left + this.ItemPadding), (float) (bounds.Top + this.ItemPadding));
        }
        else
        {
          object obj = this.Items[ea.Index];
          if (obj != null)
          {
            ComboBoxExItem comboBoxExItem = obj as ComboBoxExItem;
            int itemPadding = this.ItemPadding;
            if (comboBoxExItem != null && comboBoxExItem.ImageIndex != -1)
            {
              this.imageList.Draw(ea.Graphics, bounds.Left + this.ItemPadding, bounds.Top + this.ItemPadding, comboBoxExItem.ImageIndex);
              itemPadding += imageSize.Width;
            }
            using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
              ea.Graphics.DrawString(obj.ToString(), ea.Font, (Brush) solidBrush, (float) (itemPadding + bounds.Left), (float) (bounds.Top + this.ItemPadding));
          }
        }
      }
      catch
      {
      }
    }
    base.OnDrawItem(ea);
  }

  protected int ItemPadding => this._itemPadding;

  public new int ItemHeight
  {
    get => base.ItemHeight;
    set
    {
      this._itemPadding = -999;
      base.ItemHeight = value;
    }
  }
}
