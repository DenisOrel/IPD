
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.ShapeList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.ShowNew.ExternFile;
using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

/// <summary></summary>
public sealed class ShapeList : IDisposable
{
  private List<BaseShape> _shapes = new List<BaseShape>();

  private GraphicsUnit Units { get; }

  public void Dispose()
  {
    List<BaseShape> shapes = this._shapes;
    if (shapes != null)
      shapes.Dispose<BaseShape>();
    this._shapes = (List<BaseShape>) null;
  }

  /// <summary>Рисует изображение в указанных границах GDI+</summary>
  /// <param name="graphics">Graphics для рисования GDI+</param>
  /// <param name="clipBox">Границы для рисования, = RectangleD.Empty - безграниц</param>
  public void Draw(System.Drawing.Graphics graphics, RectangleD clipBox, double epsilon)
  {
    GraphicsState gstate = graphics.Save();
    bool flag = false;
    try
    {
      if (clipBox != RectangleD.Empty)
      {
        graphics.SetClip(RectangleD.ToRectangleF(clipBox));
        flag = true;
      }
      foreach (BaseShape shape in this._shapes)
      {
        if (!flag || !shape.CheckBlank(clipBox, epsilon))
          shape.Draw(graphics);
      }
    }
    catch (OverflowException ex)
    {
    }
    catch (OutOfMemoryException ex)
    {
    }
    finally
    {
      graphics.Restore(gstate);
    }
  }

  /// <summary>Рисует изображение в указанных границах PDF</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="clipBox">Границы для рисования, = RectangleD.Empty - безграниц</param>
  public void Draw(PdfGraphics graphics, RectangleD clipBox, double epsilon)
  {
    PdfGraphicsState state = graphics.Save();
    bool flag = false;
    RectangleF rectangleF = RectangleD.ToRectangleF(clipBox);
    try
    {
      if (clipBox != RectangleD.Empty)
      {
        graphics.SetClip(rectangleF);
        flag = true;
      }
      foreach (BaseShape shape in this._shapes)
      {
        if (!flag || !shape.CheckBlank(clipBox, epsilon))
          shape.Draw(graphics, (RectangleD) rectangleF);
      }
    }
    catch (OverflowException ex)
    {
    }
    catch (OutOfMemoryException ex)
    {
    }
    finally
    {
      graphics.Restore(state);
    }
  }

  /// <summary>Рисует изображение в указанных границах GDI+</summary>
  /// <param name="graphics">Graphics для рисования GDI+</param>
  public void Draw(System.Drawing.Graphics graphics)
  {
    GraphicsState gstate = graphics.Save();
    try
    {
      foreach (BaseShape shape in this._shapes)
        shape.Draw(graphics);
    }
    catch (OverflowException ex)
    {
    }
    catch (OutOfMemoryException ex)
    {
    }
    finally
    {
      graphics.Restore(gstate);
    }
  }

  /// <summary>Рисует изображение в указанных границах Pdf</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="clipBox">Границы для рисования</param>
  public void Draw(PdfGraphics graphics, RectangleD clipBox)
  {
    PdfGraphicsState state = graphics.Save();
    try
    {
      foreach (BaseShape shape in this._shapes)
        shape.Draw(graphics, clipBox);
    }
    catch (OverflowException ex)
    {
    }
    catch (OutOfMemoryException ex)
    {
    }
    finally
    {
      graphics.Restore(state);
    }
  }

  internal ShapeList() => this.Units = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetMeasurement();

  internal void Read(
    ILayerTable layers,
    StylusTable styluses,
    ImageTable images,
    IShowDwgWork work)
  {
    this._shapes.Dispose<BaseShape>();
    this._shapes.Add((BaseShape) null);
    try
    {
      IntPtr buffer;
      int arSize;
      while ((arSize = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.NextDrawDwgDouble(out buffer)) != 0)
        new Formatter(arSize, buffer).Format(this, layers, styluses, images, work);
    }
    finally
    {
      this._shapes.RemoveAt(0);
    }
  }

  internal void ReadShort(
    ILayerTable dwgLayerTable,
    StylusTable stylusTable,
    ImageTable imageTable,
    MatrixD matr,
    double scale,
    IShowDwgWork work)
  {
    this._shapes.Dispose<BaseShape>();
    this._shapes.Add((BaseShape) null);
    try
    {
      IntPtr buffer;
      int arSize;
      while ((arSize = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.NextDrawDwg(out buffer)) != 0)
        new FormatterShort(arSize, buffer, matr, scale).Format(this, dwgLayerTable, stylusTable, imageTable, work);
    }
    finally
    {
      this._shapes.RemoveAt(0);
    }
  }

  internal void Add(BaseShape item)
  {
    if (item is PolyLineShape pline)
      this.AddPolyLine(pline);
    else
      this._shapes.Add(item);
  }

  internal void AddPolyLine(PolyLineShape pline)
  {
    BaseShape box = pline.CheckCreateBox();
    if (box != null)
      this._shapes.Add(box);
    else if ((this._shapes.Count != 0 ? this._shapes[this._shapes.Count - 1] : (BaseShape) null) is PolyLineShape shape && pline.CheckChainAdd(shape))
    {
      this._shapes.RemoveAt(this._shapes.Count - 1);
      this.AddPolyLine(pline);
    }
    else
      this._shapes.Add((BaseShape) pline);
  }

  /// <summary>пересчёт размеров для слоёв</summary>
  internal void ReCalculationBounds()
  {
    foreach (BaseShape shape in this._shapes)
      shape.ReCalculationBound();
  }
}
