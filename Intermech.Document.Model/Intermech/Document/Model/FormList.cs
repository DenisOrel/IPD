// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.FormList
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.RtfEditor;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.Document.Model;

public class FormList : IEnumerable, IEnumerator
{
  private readonly object syncRoot = new object();
  protected System.Collections.Generic.List<Formula> fList = new System.Collections.Generic.List<Formula>();
  public SizeF totalSize;
  private int current = -1;

  /// <summary>Буферный экземпляр ImRtfEditor для печати</summary>
  public ImRtfEditor TernPrintBuffer { get; set; }

  public ImRtfEditor TernPaintBuffer { get; set; }

  public Formula this[int index]
  {
    get => this.fList[index];
    set => this.fList[index] = value;
  }

  public System.Collections.Generic.List<Formula> List => this.fList;

  public int Count => this.fList.Count;

  public FormList()
  {
  }

  public FormList(string s)
  {
    s = s.Trim();
    int startIndex1 = s.IndexOf(Formula.SpecStart);
    int startIndex2 = s.IndexOf(Formula.SpecEnd);
    int length1 = Formula.SpecStart.Length;
    if (startIndex1 == -1 || startIndex2 == -1)
    {
      startIndex1 = s.IndexOf(Formula.SpecStartOld);
      if (startIndex1 != -1)
      {
        startIndex2 = s.IndexOf(Formula.SpecEndOld);
        length1 = Formula.SpecStartOld.Length;
      }
      if (startIndex1 == -1 || startIndex2 == -1)
        return;
    }
    if (startIndex2 < startIndex1 || startIndex1 != 0 && startIndex2 == 0)
      return;
    if (startIndex2 >= 0)
      s = s.Remove(startIndex2);
    if (startIndex1 >= 0)
      s = s.Remove(startIndex1, length1);
    while (true)
    {
      int length2 = s.IndexOf(Formula.FormDivider);
      string s1 = length2 < 0 ? s : s.Substring(0, length2);
      if (!string.IsNullOrEmpty(s1))
        this.fList.Add(new Formula(s1));
      if (length2 >= 0)
        s = s.Remove(0, length2 + 1);
      else
        break;
    }
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder(Formula.SpecStart);
    for (int index = 0; index < this.fList.Count; ++index)
    {
      stringBuilder.Append(this.fList[index].ToString());
      if (index < this.fList.Count - 1)
        stringBuilder.Append(Formula.FormDivider);
    }
    stringBuilder.Append(Formula.SpecEnd);
    return stringBuilder.ToString();
  }

  /// <summary>Нет формул для которой найдена страница</summary>
  /// <returns></returns>
  public bool IsEmptyPages()
  {
    for (int index = 0; index < this.fList.Count; ++index)
    {
      if (this.fList[index].page != null)
        return false;
    }
    return true;
  }

  /// <summary>Обновить координаты</summary>
  /// <returns>Возвращает true, если в списке повторяются формулы и нужно каждый раз пересчитывать размеры</returns>
  public bool PerformCoords()
  {
    bool flag = false;
    float num1 = 0.0f;
    float num2 = 0.0f;
    for (int index1 = 0; index1 < this.fList.Count; ++index1)
    {
      for (int index2 = index1 + 1; !flag && index2 < this.fList.Count; ++index2)
      {
        if (this.fList[index1].page == this.fList[index2].page)
          flag = true;
      }
      this.fList[index1].SetFormulaParms();
      this.fList[index1].PerformHorzAligns((DocumentTreeNode) this.fList[index1].page);
      this.fList[index1].CalcCoords();
      float width = this.fList[index1].Width;
      float height = this.fList[index1].Height;
      num1 += width;
      if ((double) height > (double) num2)
        num2 = height;
    }
    this.totalSize.Width = num1;
    this.totalSize.Height = num2;
    return flag;
  }

  public void UpdatePages(Hashtable templates)
  {
    foreach (Formula f in this.fList)
      f.UpdatePage(templates);
  }

  public IEnumerator GetEnumerator() => (IEnumerator) this;

  public object Current => (object) this.fList[this.current];

  /// <summary>
  /// Смещение формулы вверх относительно базовой линии символов в шрифте
  /// При нулевом значении выравнивание согласно свойству AlignInText
  /// </summary>
  public float Offset
  {
    get
    {
      System.Collections.Generic.List<Formula> fList = this.fList;
      Page page = fList != null ? fList.FirstOrDefault<Formula>()?.page : (Page) null;
      return page != null ? page.Offset : 0.0f;
    }
  }

  /// <summary>Выравнивание формулы внутри строки текста</summary>
  public PictAlignmentInText AlignInText
  {
    get
    {
      System.Collections.Generic.List<Formula> fList = this.fList;
      Page page = fList != null ? fList.FirstOrDefault<Formula>()?.page : (Page) null;
      return page != null ? page.AlignInText : PictAlignmentInText.Center;
    }
  }

  public void Reset() => this.current = -1;

  public bool MoveNext()
  {
    if (this.current >= this.fList.Count - 1)
      return false;
    ++this.current;
    return true;
  }

  [DllImport("gdi32.dll")]
  private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, string lpszFile);

  [DllImport("gdi32.dll")]
  private static extern int DeleteEnhMetaFile(IntPtr hemf);

  internal void SaveMetafile(Metafile mf, string fileName)
  {
    IntPtr henhmetafile = mf.GetHenhmetafile();
    FormList.CopyEnhMetaFile(henhmetafile, fileName);
    FormList.DeleteEnhMetaFile(henhmetafile);
  }

  public Metafile GetMetafile(bool isStrkedOut = false, bool isDoubleStriked = false)
  {
    lock (this.syncRoot)
    {
      bool flag1 = this.PerformCoords();
      SizeF totalSize = this.totalSize;
      Metafile metafile = (Metafile) null;
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 1f;
      float num4 = 1f;
      IntPtr dc1 = TemplateHolderBase.GetDC(IntPtr.Zero);
      try
      {
        metafile = new Metafile(dc1, EmfType.EmfOnly);
        using (Graphics graphics = Graphics.FromImage((Image) metafile))
        {
          graphics.DrawRectangle(new Pen(Color.White, 0.0f), 0.0f, 0.0f, totalSize.Width, totalSize.Height);
          num1 = graphics.DpiX;
          num2 = graphics.DpiY;
        }
        num3 = metafile.HorizontalResolution / num1;
        num4 = metafile.HorizontalResolution / num2;
        if (metafile.Height > 0 && metafile.Width > 0)
          metafile = ContainerElement.SetMetafileHeader(metafile, Rectangle.Empty, RectangleF.Empty);
        metafile.GetMetafileHeader();
        float num5 = (float) (1.0 + 1.0 / (double) totalSize.Width / 3.2000000476837158);
        float num6 = (float) (1.0 + 1.0 / (double) totalSize.Height / 3.2000000476837158);
        if ((double) totalSize.Height < 5.0)
          num6 = (float) (1.0 + 1.0 / (double) totalSize.Height / 1.5);
        num3 /= num5;
        num4 /= num6;
      }
      finally
      {
        TemplateHolderBase.ReleaseDC(IntPtr.Zero, dc1);
      }
      IntPtr dc2 = TemplateHolderBase.GetDC(IntPtr.Zero);
      RectangleF frameRect = new RectangleF(0.0f, 0.0f, totalSize.Width / num3, totalSize.Height / num4);
      try
      {
        metafile = new Metafile(dc2, frameRect, MetafileFrameUnit.Millimeter, EmfType.EmfOnly);
        using (Graphics g = Graphics.FromImage((Image) metafile))
        {
          g.PageUnit = GraphicsUnit.Millimeter;
          MatrixWrapper transformMatrix = new MatrixWrapper(g.Transform);
          DrawContextWithUI drawContextWithUi = new DrawContextWithUI(new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, totalSize), 0, false, false, transformMatrix));
          drawContextWithUi.TernPrintBuffer = ImDocument.TernDistributeBufferForPrint;
          drawContextWithUi.TernPaintBuffer = this.TernPaintBuffer;
          drawContextWithUi.IsMetafile = true;
          drawContextWithUi.IsDoubleStriked = new bool?(isDoubleStriked);
          DrawContextWithUI context = drawContextWithUi;
          ImRtfEditor ternPaintBuffer = context.TernPaintBuffer;
          bool flag2 = ternPaintBuffer != null && ternPaintBuffer.blk.IsDoubleStrikedOut;
          ImRtfEditor ternPrintBuffer = context.TernPrintBuffer;
          bool flag3 = ternPrintBuffer != null && ternPrintBuffer.blk.IsDoubleStrikedOut;
          float x = 0.0f;
          foreach (Formula f in this.fList)
          {
            if (f.page != null)
            {
              if (flag1)
              {
                f.SetFormulaParms();
                f.PerformHorzAligns((DocumentTreeNode) f.page);
                f.CalcCoords();
              }
              f.AdjustCoordsTo(new PointF(x, 0.0f));
              x += f.Width;
              if (context.TernPaintBuffer != null)
                context.TernPaintBuffer.blk.IsDoubleStrikedOut = isDoubleStriked;
              if (context.TernPrintBuffer != null)
                context.TernPrintBuffer.blk.IsDoubleStrikedOut = isDoubleStriked;
              f.page.Draw((DrawContext) context);
              f.AdjustCoordsTo(new PointF(0.0f, 0.0f));
            }
          }
          if (context.TernPaintBuffer != null)
            context.TernPaintBuffer.blk.IsDoubleStrikedOut = flag2;
          if (context.TernPrintBuffer != null)
            context.TernPrintBuffer.blk.IsDoubleStrikedOut = flag3;
        }
      }
      finally
      {
        TemplateHolderBase.ReleaseDC(IntPtr.Zero, dc2);
      }
      return metafile;
    }
  }

  internal void ApplyFormulaFieldsFormat(string formulaCharsFormat)
  {
    foreach (Formula f in this.fList)
      formulaCharsFormat = f.ApplyFormulaFieldsFormat(formulaCharsFormat);
  }

  internal string GetFormulaFieldsFormat()
  {
    System.Collections.Generic.List<string> stringList = new System.Collections.Generic.List<string>(this.fList.Count);
    foreach (Formula f in this.fList)
      stringList.Add(f.GetFormulaFieldsFormat());
    return stringList.Any<string>((Func<string, bool>) (s => s.Any<char>((Func<char, bool>) (ch => ch != ';')))) ? string.Join(";", (IEnumerable<string>) stringList) : "";
  }
}
