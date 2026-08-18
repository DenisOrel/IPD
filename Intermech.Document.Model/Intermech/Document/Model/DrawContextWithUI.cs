// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.DrawContextWithUI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Контекст отрисовки с данными о элементах управления</summary>
public class DrawContextWithUI : DrawContext
{
  /// <summary>Элемент управленя на котором рисовать</summary>
  public PageControl PageControl;
  /// <summary>Буферный экземпляр ImRtfEditor для рисования</summary>
  public ImRtfEditor TernPaintBuffer;
  /// <summary>Буферный экземпляр ImRtfEditor для печати</summary>
  public ImRtfEditor TernPrintBuffer;
  /// <summary>Документ</summary>
  public ImDocument Document;

  /// <summary>
  /// Обеспечить двойное перечеркивание при рисовании содержимого
  /// </summary>
  public bool? IsDoubleStriked { get; set; }

  /// <summary>Конструктор копии контекста</summary>
  /// <param name="src">Оригинальный контекст</param>
  public DrawContextWithUI(DrawContextWithUI src)
    : base((DrawContext) src)
  {
    this.TernPaintBuffer = src.TernPaintBuffer;
    this.TernPrintBuffer = src.TernPrintBuffer;
    this.Document = src.Document;
  }

  /// <summary>Конструктор копии контекста</summary>
  /// <param name="src">Оригинальный контекст</param>
  public DrawContextWithUI(DrawContext src)
    : base(src)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="pageControl">Элемент управленя на котором рисовать</param>
  /// <param name="g">Объект на котором нужно отобразить</param>
  /// <param name="isPaint">Отрисовка вызвана событием Paint</param>
  /// <param name="clipRectangle">Область в которой нужно отрисовать. Все что вне области не рисуется</param>
  /// <param name="layer">Номер слоя. -1 невидимые линии (только для isPaint==true), 0 основной слой</param>
  /// <param name="withoutData">Не отображать данные, только границы и фон</param>
  /// <param name="showInvisibleLines">Отображать невидимые линии</param>
  /// <param name="transformMatrix">Матрица трансформации</param>
  public DrawContextWithUI(
    ImDocument document,
    PageControl pageControl,
    ImGraphics g,
    bool isPaint,
    RectangleF clipRectangle,
    int layer,
    bool withoutData,
    bool showInvisibleLines,
    MatrixWrapper transformMatrix)
    : base(g, isPaint, clipRectangle, layer, withoutData, showInvisibleLines, transformMatrix)
  {
    this.PageControl = pageControl;
    if (pageControl != null)
      this.displayDPI = pageControl.DisplayDpi;
    if (document == null)
      return;
    this.TernPaintBuffer = document.TernPaintBuffer;
    this.TernPrintBuffer = document.TernPrintBuffer;
    this.Document = document;
  }

  public DrawContextWithUI(ImDocument document, PageControl pageControl, DrawContext baseContext)
    : base(baseContext)
  {
    this.PageControl = pageControl;
    if (pageControl != null)
      this.displayDPI = pageControl.DisplayDpi;
    this.Document = document;
    if (document == null)
      return;
    this.TernPaintBuffer = document.TernPaintBuffer;
    this.TernPrintBuffer = document.TernPrintBuffer;
    this.Document = document;
  }

  /// <summary>Значение DisplayDPI по умолчанию</summary>
  protected override PointF DefaultDisplayDPI
  {
    get
    {
      if (DrawContext.defaultDisplayDPI.IsEmpty)
      {
        IntPtr dc = Page.GetDC(IntPtr.Zero);
        try
        {
          using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHdc(dc))
            DrawContext.defaultDisplayDPI = new PointF(graphics.DpiX, graphics.DpiY);
        }
        finally
        {
          Page.ReleaseDC(IntPtr.Zero, dc);
        }
      }
      return DrawContext.defaultDisplayDPI;
    }
  }

  /// <summary>Масштаб на экране</summary>
  public override float Scale => this.PageControl != null ? this.PageControl.PageScale : 1f;
}
