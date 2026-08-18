// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InformationRequest.ThumbnailViewerControl
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client.InformationRequest;

public class ThumbnailViewerControl : FlowLayoutPanel
{
  private List<byte[]> _imageList;
  private int _selectedScreenshotIndex;

  /// <summary>Список для созданных скриншотов</summary>
  public List<byte[]> ImageList
  {
    get => this._imageList;
    set => this._imageList = value;
  }

  public int SelectedScreenshotIndex => this._selectedScreenshotIndex;

  public ThumbnailViewerControl()
  {
    this.AutoScroll = true;
    this.DoubleBuffered = true;
    this._imageList = new List<byte[]>();
  }

  public Image BinaryToImage(byte[] binaryData)
  {
    if (binaryData == null)
      return (Image) null;
    byte[] array = ((IEnumerable<byte>) binaryData).ToArray<byte>();
    using (MemoryStream memoryStream = new MemoryStream())
    {
      memoryStream.Write(array, 0, array.Length);
      return Image.FromStream((Stream) memoryStream);
    }
  }

  public void AddImage(List<byte[]> imageBytes)
  {
    this.Cursor = Cursors.WaitCursor;
    for (int index = 0; index < imageBytes.Count; ++index)
    {
      this.ImageList.Add(imageBytes[index]);
      this.MakeThumbnail(imageBytes[index]);
    }
    this.Cursor = Cursors.Default;
  }

  public void MakeThumbnail(byte[] binary)
  {
    PictureBox pictureBox1 = new PictureBox();
    pictureBox1.MaximumSize = new Size(512 /*0x0200*/, 358);
    pictureBox1.MinimumSize = new Size(128 /*0x80*/, 90);
    pictureBox1.Size = new Size(256 /*0x0100*/, 179);
    pictureBox1.BorderStyle = BorderStyle.FixedSingle;
    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
    PictureBox pictureBox2 = pictureBox1;
    pictureBox2.Paint += new PaintEventHandler(this.thumb_Paint);
    pictureBox2.MouseEnter += new EventHandler(this.thumb_MouseEnter);
    pictureBox2.MouseLeave += new EventHandler(this.thumb_MouseLeave);
    pictureBox2.DoubleClick += new EventHandler(this.thumb_DoubleClick);
    pictureBox2.Click += new EventHandler(this.thumb_Click);
    using (MemoryStream memoryStream = new MemoryStream(binary))
      pictureBox2.Image = Image.FromStream((Stream) memoryStream).GetThumbnailImage(pictureBox2.Width - 2, pictureBox2.Height - 2, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
    this.Controls.Add((Control) pictureBox2);
  }

  private void thumb_Paint(object sender, PaintEventArgs e)
  {
    if (this.Controls.GetChildIndex((Control) sender) != this._selectedScreenshotIndex)
      return;
    Rectangle clientRectangle = ((Control) sender).ClientRectangle;
    ControlPaint.DrawBorder(e.Graphics, clientRectangle, Color.Blue, 2, ButtonBorderStyle.Solid, Color.Blue, 2, ButtonBorderStyle.Solid, Color.Blue, 2, ButtonBorderStyle.Solid, Color.Blue, 2, ButtonBorderStyle.Solid);
  }

  private void thumb_Click(object sender, EventArgs e)
  {
    int selectedScreenshotIndex = this._selectedScreenshotIndex;
    this._selectedScreenshotIndex = this.Controls.GetChildIndex((Control) sender);
    ((Control) sender).Invalidate();
    this.Controls[selectedScreenshotIndex].Invalidate();
  }

  private void thumb_DoubleClick(object sender, EventArgs e)
  {
    PreviewForm previewForm = new PreviewForm();
    previewForm.SetImage(this.BinaryToImage(this.ImageList[this.Controls.GetChildIndex((Control) sender)]));
    int num = (int) previewForm.ShowDialog();
  }

  private void thumb_MouseLeave(object sender, EventArgs e) => ((Control) sender).Invalidate();

  private void thumb_MouseEnter(object sender, EventArgs e)
  {
    ((Control) sender).ClientRectangle.Inflate(2, 2);
    ControlPaint.DrawBorder(((Control) sender).CreateGraphics(), ((Control) sender).ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
  }
}
