// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.ImagePanel
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class ImagePanel : Control
{
  private static IPicturesCache _picturesCache;
  private static StringFormat _sf = new StringFormat();
  private object _picture;
  private long _objectId;
  private Hashtable _objectIdCache = new Hashtable(32 /*0x20*/);
  private int _shadowSize = 4;

  static ImagePanel()
  {
    ImagePanel._sf.Alignment = StringAlignment.Center;
    ImagePanel._sf.LineAlignment = StringAlignment.Center;
  }

  public ImagePanel()
  {
    this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.Selectable, true);
    this.BackColor = Color.Transparent;
  }

  [DefaultValue("Transparent")]
  public override Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  [DefaultValue(4)]
  public int ShadowSize
  {
    get => this._shadowSize;
    set
    {
      this._shadowSize = value;
      this.Invalidate();
    }
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    Rectangle displayRectangle = this.DisplayRectangle;
    if (this._shadowSize > 0)
      this.DrawShadow(e.Graphics, ref displayRectangle);
    if (this._picture != null)
      ImageDrawer.DrawImageObject(e.Graphics, this._picture, displayRectangle, this.Font, ImagePanel._sf);
    else
      e.Graphics.DrawString(LocalizationHolder.rm.GetString("Imbase.Client_25"), this.Font, SystemBrushes.ControlText, (RectangleF) displayRectangle, ImagePanel._sf);
  }

  private GraphicsPath GetPath(Rectangle bounds, int size)
  {
    GraphicsPath path = new GraphicsPath();
    int num = size * 2;
    path.AddLine(bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
    path.AddLine(bounds.Right, bounds.Bottom, bounds.Right, bounds.Top + 1);
    path.AddArc(bounds.Right, bounds.Top + 1, size, size, 270f, 90f);
    path.AddLine(bounds.Right + size, bounds.Top + 1 + size, bounds.Right + size, bounds.Bottom);
    path.AddArc(bounds.Right, bounds.Bottom, size, size, 0.0f, 90f);
    path.AddLine(bounds.Right, bounds.Bottom + size, bounds.Left + size, bounds.Bottom + size);
    path.AddArc(bounds.Left, bounds.Bottom - size, num, num, 90f, 90f);
    return path;
  }

  private void DrawShadow(Graphics g, ref Rectangle bounds)
  {
    if (this._shadowSize > 1)
    {
      bounds.Inflate(-1, -1);
      int shadowSize = this._shadowSize;
      bounds.Width -= shadowSize;
      bounds.Height -= shadowSize;
      using (GraphicsPath path = this.GetPath(bounds, shadowSize))
      {
        float num = (float) (1.0 - (double) shadowSize * 3.0 / (double) Math.Max((float) bounds.Width, (float) bounds.Width));
        g.FillPath((Brush) new PathGradientBrush(path)
        {
          CenterPoint = (PointF) new Point(0, 0),
          CenterColor = SystemColors.ControlDarkDark,
          FocusScales = new PointF(num, num),
          SurroundColors = new Color[1]{ Color.Transparent }
        }, path);
      }
    }
    bounds.Inflate(-1, -1);
    g.DrawRectangle(SystemPens.ControlDark, bounds);
    bounds.Inflate(-1, -1);
  }

  public void ClearCache() => this._objectIdCache.Clear();

  private long TranslateObjectId(long objectId)
  {
    object obj = this._objectIdCache[(object) objectId];
    return obj != null ? (long) obj : objectId;
  }

  private void LoadData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._picture = (object) null;
      IPicturesCache picturesCache = ImagePanel.PicturesCache;
      if (picturesCache != null && this._objectId != 0L)
      {
        long newObjectId = 0;
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._objectId);
        this._picture = picturesCache.GetPicture(objectInfo.ObjectTypeID, this._objectId, out newObjectId);
        if (newObjectId != this._objectId && newObjectId > 0L)
          this._objectIdCache[(object) this._objectId] = (object) newObjectId;
      }
      this.Invalidate();
    }
  }

  [Browsable(false)]
  public long ObjectId
  {
    get => this._objectId;
    set
    {
      if (this._objectId == value)
        return;
      this._objectId = this.TranslateObjectId(value);
      if (this.DesignMode)
        return;
      this.LoadData();
    }
  }

  private static IPicturesCache PicturesCache
  {
    get
    {
      if (ImagePanel._picturesCache == null)
        ImagePanel._picturesCache = ServicesManager.GetService(typeof (IPicturesCache)) as IPicturesCache;
      return ImagePanel._picturesCache;
    }
  }
}
