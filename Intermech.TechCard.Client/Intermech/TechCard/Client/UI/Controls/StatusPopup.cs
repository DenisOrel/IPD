// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.StatusPopup
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>
/// 
/// </summary>
public static class StatusPopup
{
  /// <summary>
  /// 
  /// </summary>
  private static readonly Dictionary<Control, Form> Popups = new Dictionary<Control, Form>();
  /// <summary>
  /// 
  /// </summary>
  private static int _padding = 10;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="image"></param>
  /// <param name="text"></param>
  /// <param name="c"></param>
  public static void Show(Image image, string text, Control c)
  {
    if (c == null)
      throw new ArgumentNullException(nameof (c));
    StatusPopup.Hide(c);
    if (c.Disposing || !c.CanFocus || c.Height == 0 || c.Width == 0)
      return;
    Form form = new Form();
    StatusPopup.Popups[c] = form;
    Panel panel = new Panel();
    PictureBox pictureBox1 = new PictureBox();
    pictureBox1.Size = new Size();
    PictureBox pictureBox2 = pictureBox1;
    form.SuspendLayout();
    panel.SuspendLayout();
    ((ISupportInitialize) pictureBox2).BeginInit();
    try
    {
      form.StartPosition = FormStartPosition.Manual;
      form.FormBorderStyle = FormBorderStyle.None;
      form.TopMost = true;
      form.Size = new Size(0, 0);
      form.Controls.Add((Control) panel);
      panel.Controls.Add((Control) pictureBox2);
      panel.Dock = DockStyle.Fill;
      if (image != null)
      {
        pictureBox2.Image = image;
        pictureBox2.Size = pictureBox2.Image.Size;
        pictureBox2.Left = StatusPopup._padding;
      }
      Label label1 = new Label();
      label1.Text = text;
      label1.MaximumSize = new Size(300, 0);
      label1.AutoSize = true;
      label1.Left = pictureBox2.Left + pictureBox2.Width + StatusPopup._padding;
      label1.TextAlign = ContentAlignment.MiddleLeft;
      Label label2 = label1;
      panel.Controls.Add((Control) label2);
      int num = Math.Max(pictureBox2.Height, label2.Height);
      form.Height = num + 2 * StatusPopup._padding;
      pictureBox2.Top = form.Height / 2 - pictureBox2.Height / 2;
      label2.Top = form.Height / 2 - label2.Height / 2;
      form.Width = pictureBox2.Width + label2.Width + 3 * StatusPopup._padding;
      form.Location = c.PointToScreen(new Point(c.Width / 2 - form.Width / 2, c.Height / 2 - form.Height / 2));
    }
    finally
    {
      ((ISupportInitialize) pictureBox2).EndInit();
      panel.ResumeLayout(false);
      panel.PerformLayout();
      form.ResumeLayout(false);
      form.PerformLayout();
    }
    form.Show();
    form.BringToFront();
    form.Refresh();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="c"></param>
  public static void Hide(Control c)
  {
    Form form;
    if (!StatusPopup.Popups.TryGetValue(c, out form))
      return;
    form.Close();
    StatusPopup.Popups.Remove(c);
    form.Dispose();
  }
}
