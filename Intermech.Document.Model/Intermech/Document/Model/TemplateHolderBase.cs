// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TemplateHolderBase
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Document.Model;

public class TemplateHolderBase
{
  public static TemplateHolderBase Instance;
  public static readonly string guidSpecTemplates = "cad00251-306c-11d8-b4e9-00304f19f545";
  /// <summary>List of loaded templates from all docs of the specified type</summary>
  public List<DocumentSection> groups;
  /// <summary>Keys are names, values are FormSearch pairs for uniqueness checks and fast search</summary>
  public Hashtable templates;
  public List<Page> docTemplates;
  public static readonly float mmPerInch = 25.4f;

  static TemplateHolderBase() => TemplateHolderBase.Instance = new TemplateHolderBase();

  public TemplateHolderBase()
  {
    this.groups = new List<DocumentSection>();
    this.templates = new Hashtable();
    this.docTemplates = new List<Page>();
  }

  /// <summary>Загрузить все формулы</summary>
  public virtual void LoadTemplates()
  {
  }

  /// <summary>Перезагрузить все формулы</summary>
  public virtual void ReloadTemplates()
  {
  }

  /// <summary>Загрузить формулы из документа</summary>
  /// <param name="doc"></param>
  public virtual void SetTemplatesForDoc(ImDocument doc)
  {
  }

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  internal static extern IntPtr GetDC(IntPtr hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

  [DllImport("Gdi32.dll")]
  public static extern uint GetEnhMetaFileBits(IntPtr hemf, uint cbBuffer, byte[] lpbBuffer);

  [DllImport("Gdi32.dll")]
  public static extern bool DeleteEnhMetaFile(IntPtr hemf);

  public Bitmap GetBitmap(Formula f)
  {
    SizeF size1 = f.page.Size;
    Size size2 = Size.Empty;
    IntPtr dc = TemplateHolderBase.GetDC(IntPtr.Zero);
    try
    {
      Graphics graphics = Graphics.FromHdcInternal(dc);
      size2 = new Size(Convert.ToInt32(Math.Round((double) graphics.DpiX * (double) size1.Width / (double) TemplateHolderBase.mmPerInch)) + 1, Convert.ToInt32(Math.Round((double) graphics.DpiY * (double) size1.Height / (double) TemplateHolderBase.mmPerInch)) + 1);
    }
    finally
    {
      TemplateHolderBase.ReleaseDC(IntPtr.Zero, dc);
    }
    Bitmap bitmap = new Bitmap(size2.Width, size2.Height);
    using (Graphics g = Graphics.FromImage((Image) bitmap))
    {
      g.PageUnit = GraphicsUnit.Pixel;
      DrawContext context = new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, size1), 0, false, false, new MatrixWrapper(g.Transform));
      f.page.Draw(context);
    }
    return bitmap;
  }

  public Bitmap GetBitmap(FormList forms)
  {
    SizeF totalSize = forms.totalSize;
    Size size = Size.Empty;
    IntPtr dc = TemplateHolderBase.GetDC(IntPtr.Zero);
    try
    {
      Graphics graphics = Graphics.FromHdcInternal(dc);
      size = new Size(Convert.ToInt32(Math.Round((double) graphics.DpiX * (double) totalSize.Width / (double) TemplateHolderBase.mmPerInch)) + 1, Convert.ToInt32(Math.Round((double) graphics.DpiY * (double) totalSize.Height / (double) TemplateHolderBase.mmPerInch)) + 1);
    }
    finally
    {
      TemplateHolderBase.ReleaseDC(IntPtr.Zero, dc);
    }
    Bitmap bitmap = new Bitmap(size.Width, size.Height);
    using (Graphics g = Graphics.FromImage((Image) bitmap))
    {
      g.PageUnit = GraphicsUnit.Millimeter;
      RectangleF rect = MatrixWrapper.TransformPoints(g.Transform.Elements, new RectangleF(PointF.Empty, totalSize));
      ++rect.Width;
      ++rect.Height;
      g.SetClip(rect);
      DrawContext context = new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, totalSize), 0, false, false, new MatrixWrapper(g.Transform));
      float x = 0.0f;
      foreach (Formula formula in forms.List)
      {
        if (formula.page != null)
        {
          formula.SetFormulaParms();
          formula.PerformHorzAligns((DocumentTreeNode) formula.page);
          formula.CalcCoords();
          formula.AdjustCoordsTo(new PointF(x, 0.0f));
          x += formula.Width;
          formula.page.Draw(context);
          formula.AdjustCoordsTo(new PointF(0.0f, 0.0f));
        }
      }
    }
    return bitmap;
  }

  public Metafile GetMetafile(Formula f)
  {
    Metafile metafile = (Metafile) null;
    IntPtr dc = TemplateHolderBase.GetDC(IntPtr.Zero);
    SizeF size = f.page.Size;
    RectangleF rectangleF = new RectangleF((PointF) Point.Empty, size);
    Rectangle empty = Rectangle.Empty;
    try
    {
      metafile = new Metafile(dc, EmfType.EmfOnly);
      using (Graphics g = Graphics.FromImage((Image) metafile))
      {
        g.PageUnit = GraphicsUnit.Millimeter;
        RectangleF rect = MatrixWrapper.TransformPoints(g.Transform.Elements, new RectangleF(PointF.Empty, size));
        ++rect.Width;
        ++rect.Height;
        g.SetClip(rect);
        g.DrawRectangle(new Pen(Color.White, PageElementNode.DefaultLineWidth), 0.0f, 0.0f, size.Width, size.Height);
        DrawContext context = new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, size), 0, false, false, new MatrixWrapper(g.Transform));
        f.page.Draw(context);
      }
    }
    finally
    {
      TemplateHolderBase.ReleaseDC(IntPtr.Zero, dc);
    }
    return metafile;
  }

  public Metafile GetMetafile(FormList forms)
  {
    Metafile metafile = (Metafile) null;
    IntPtr dc = TemplateHolderBase.GetDC(IntPtr.Zero);
    SizeF totalSize = forms.totalSize;
    RectangleF rectangleF = new RectangleF((PointF) Point.Empty, totalSize);
    Rectangle empty = Rectangle.Empty;
    try
    {
      metafile = new Metafile(dc, EmfType.EmfOnly);
      using (Graphics g = Graphics.FromImage((Image) metafile))
      {
        g.PageUnit = GraphicsUnit.Millimeter;
        RectangleF rect = MatrixWrapper.TransformPoints(g.Transform.Elements, new RectangleF(PointF.Empty, totalSize));
        ++rect.Width;
        ++rect.Height;
        g.SetClip(rect);
        g.DrawRectangle(new Pen(Color.White, PageElementNode.DefaultLineWidth), 0.0f, 0.0f, totalSize.Width, totalSize.Height);
        DrawContext context = new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, totalSize), 0, false, false, new MatrixWrapper(g.Transform));
        float x = 0.0f;
        foreach (Formula formula in forms.List)
        {
          if (formula.page != null)
          {
            formula.SetFormulaParms();
            formula.PerformHorzAligns((DocumentTreeNode) formula.page);
            formula.CalcCoords();
            formula.AdjustCoordsTo(new PointF(x, 0.0f));
            x += formula.Width;
            formula.page.Draw(context);
            formula.AdjustCoordsTo(new PointF(0.0f, 0.0f));
          }
        }
      }
    }
    finally
    {
      TemplateHolderBase.ReleaseDC(IntPtr.Zero, dc);
    }
    return metafile;
  }

  public Metafile GetMetafileRedNote(FormList forms, out SizeF realSize)
  {
    Metafile metafileRedNote = (Metafile) null;
    SizeF totalSize = forms.totalSize;
    realSize = new SizeF(totalSize.Width + 1f, totalSize.Height + 1f);
    IntPtr dc = TemplateHolderBase.GetDC(IntPtr.Zero);
    RectangleF frameRect = new RectangleF((PointF) Point.Empty, realSize);
    try
    {
      metafileRedNote = new Metafile(dc, frameRect, MetafileFrameUnit.Millimeter, EmfType.EmfOnly);
      using (Graphics g = Graphics.FromImage((Image) metafileRedNote))
      {
        g.PageUnit = GraphicsUnit.Millimeter;
        RectangleF rect = MatrixWrapper.TransformPoints(g.Transform.Elements, new RectangleF(PointF.Empty, totalSize));
        ++rect.Width;
        ++rect.Height;
        g.SetClip(rect);
        g.DrawRectangle(new Pen(Color.Empty, 0.0f), 0.0f, 0.0f, totalSize.Width, totalSize.Height);
        DrawContext context = new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, totalSize), 0, false, false, new MatrixWrapper(g.Transform));
        float x = 0.0f;
        foreach (Formula formula in forms.List)
        {
          if (formula.page != null)
          {
            formula.SetFormulaParms();
            formula.PerformHorzAligns((DocumentTreeNode) formula.page);
            formula.CalcCoords();
            formula.AdjustCoordsTo(new PointF(x, 0.0f));
            x += formula.Width;
            formula.page.Draw(context);
            formula.AdjustCoordsTo(new PointF(0.0f, 0.0f));
          }
        }
      }
    }
    finally
    {
      TemplateHolderBase.ReleaseDC(IntPtr.Zero, dc);
    }
    return metafileRedNote;
  }

  public Bitmap GenerateBitmap(ImDocument doc, string specText)
  {
    this.SetTemplatesForDoc(doc);
    FormList forms = new FormList(specText);
    forms.UpdatePages(this.templates);
    forms.PerformCoords();
    return this.GetBitmap(forms);
  }

  public Metafile GenerateMetafile(ImDocument doc, string specText)
  {
    this.SetTemplatesForDoc(doc);
    FormList forms = new FormList(specText);
    forms.UpdatePages(this.templates);
    forms.PerformCoords();
    return this.GetMetafile(forms);
  }
}
