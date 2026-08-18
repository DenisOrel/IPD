// Decompiled with JetBrains decompiler
// Type: Intermech.StatusPopup
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech;

public class StatusPopup
{
  private static Dictionary<Control, Panel> _popups = new Dictionary<Control, Panel>();
  private static int _padding = 10;

  public static void Show(Image image, string text, Control c)
  {
    StatusPopup.Hide(c);
    Panel panel = new Panel();
    StatusPopup._popups[c] = panel;
    panel.Size = new Size(0, 0);
    PictureBox pictureBox = new PictureBox();
    pictureBox.Size = new Size();
    if (image != null)
    {
      pictureBox.Image = image;
      pictureBox.Parent = (Control) panel;
      pictureBox.Size = pictureBox.Image.Size;
      pictureBox.Left = StatusPopup._padding;
    }
    Label label = new Label();
    label.Text = text;
    label.MaximumSize = new Size(300, 0);
    label.Parent = (Control) panel;
    label.AutoSize = true;
    label.Left = pictureBox.Left + pictureBox.Width + StatusPopup._padding;
    label.TextAlign = ContentAlignment.MiddleLeft;
    int num = Math.Max(pictureBox.Height, label.Height);
    panel.Height = num + 2 * StatusPopup._padding;
    pictureBox.Top = panel.Height / 2 - pictureBox.Height / 2;
    label.Top = panel.Height / 2 - label.Height / 2;
    panel.Parent = c;
    panel.Width = pictureBox.Width + label.Width + 3 * StatusPopup._padding;
    panel.Left = c.Width / 2 - panel.Width / 2;
    panel.Top = c.Height / 2 - panel.Height / 2;
    panel.BringToFront();
  }

  public static void Hide(Control c)
  {
    Panel panel = (Panel) null;
    if (!StatusPopup._popups.TryGetValue(c, out panel))
      return;
    StatusPopup._popups.Remove(c);
    panel.Dispose();
  }
}
