
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.FormatterShort
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.ShowNew.ExternFile;
using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

internal sealed class FormatterShort
{
  private ConvertStream _buf;
  private TextData _textData;
  internal readonly double _scale = 1.0;
  private readonly MatrixD _matr;
  private DwgColor _color = new DwgColor((byte) 0);
  private ILayer _curLayer;
  private double _curWeight;

  internal FormatterShort(int arSize, IntPtr ptr, MatrixD matr, double scale)
  {
    this._buf = new ConvertStream(arSize, ptr);
    this._textData = new TextData();
    this._scale = scale;
    this._matr = matr;
  }

  internal void Format(
    ShapeList shapes,
    ILayerTable layers,
    StylusTable styluses,
    ImageTable images,
    IShowDwgWork work)
  {
    while (!this._buf.Eof)
    {
      int curType = (int) this._buf.ReadByte();
      switch (curType)
      {
        case 1:
          this.ReadColorLineAcad();
          continue;
        case 2:
          this.Polygon(shapes, styluses, work);
          continue;
        case 3:
          this.PolyLine(shapes, styluses, 2);
          continue;
        case 4:
          this.PolyLine(shapes, styluses, (int) this._buf.ReadInt16());
          continue;
        case 5:
          this.Arc(shapes, styluses);
          continue;
        case 6:
          this.ReadWeightLine();
          continue;
        case 7:
          this.Text(shapes, styluses);
          continue;
        case 8:
          this.ReadLayer(layers);
          continue;
        case 10:
          this._textData.ReadNameFont(this._buf);
          continue;
        case 11:
          this._textData.ReadColorFont(this._buf);
          continue;
        case 12:
          this._textData.ReadSizeFontShort(this._buf, this._scale);
          continue;
        case 13:
          this._textData.ReadFontStyle(this._buf);
          continue;
        case 14:
          this._textData.ReadCharSetFont(this._buf.ReadByte());
          continue;
        case 15:
          this._textData.ReadRotateFontShort(this._buf);
          continue;
        case 16 /*0x10*/:
          this.ACIS();
          continue;
        case 17:
          this.Image(shapes, styluses, images);
          continue;
        case 24:
          this.OLE_Object(shapes, styluses);
          continue;
        case 25:
          this._textData.ReadWidthFontShort(this._buf, this._scale);
          continue;
        case 27:
          this.TextPointF(shapes, styluses);
          continue;
        case 28:
          this.Point(shapes, styluses);
          continue;
        case 29:
          this.ReadColorLine32();
          continue;
        case 30:
          this._textData.ReadColorFont32(this._buf);
          continue;
        default:
          this.Unknown(curType);
          continue;
      }
    }
  }

  private void Unknown(int curType) => throw new Exception($"ShowFormatter error code={curType}.");

  private void ReadColorLineAcad() => this._color.AcadIndex = (uint) this._buf.ReadByte();

  private void ReadColorLine32() => this._color.Rgb = (uint) this._buf.ReadInt32();

  private void ReadLayer(ILayerTable layers)
  {
    this._curLayer = layers[(int) this._buf.ReadInt16()];
  }

  private void ReadWeightLine() => this._curWeight = (double) this._buf.ReadInt16() * this._scale;

  internal PointD ReCover(PointF value)
  {
    PointD[] pts = new PointD[1]{ new PointD(value) };
    this._matr.TransformPoints(pts);
    return pts[0];
  }

  internal PointD[] ReCover(PointF[] value)
  {
    PointD[] pointDArray = new PointD[value.Length];
    for (int index = 0; index < pointDArray.Length; ++index)
      pointDArray[index] = this.ReCover(value[index]);
    return pointDArray;
  }

  private void PolyLine(ShapeList shapes, StylusTable styluses, int len)
  {
    IStylus stylus = styluses.Generate(this._color);
    PolyLineShape pline = new PolyLineShape(new List<PointD>((IEnumerable<PointD>) this.ReCover(this._buf.ReadPointF16(len))), this._curLayer, stylus, this._curWeight);
    shapes.AddPolyLine(pline);
  }

  private void Polygon(ShapeList shapes, StylusTable styluses, IShowDwgWork work)
  {
    PointD[] pointDs = this.ReCover(this._buf.ReadPointF16((int) this._buf.ReadInt16()));
    IStylus stylus = styluses.Generate(this._color);
    if (stylus.ColorDwg.IsEmpty)
      shapes.Add((BaseShape) new WipeOutShape(pointDs, this._curLayer, stylus, this._curWeight, work));
    else
      shapes.Add((BaseShape) new PolygonShape(pointDs, this._curLayer, stylus, this._curWeight));
  }

  private void Arc(ShapeList shapes, StylusTable styluses)
  {
    ArcShape arcShape = new ArcShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    PointD pnt1 = this.ReCover(this._buf.ReadPointF16());
    PointD pnt2 = this.ReCover(this._buf.ReadPointF16());
    PointD pntS = this.ReCover(this._buf.ReadPointF16());
    PointD pntE = this.ReCover(this._buf.ReadPointF16());
    arcShape.InitShort(pnt1, pnt2, pntS, pntE);
    shapes.Add((BaseShape) arcShape);
  }

  private void Point(ShapeList shapes, StylusTable styluses)
  {
    PointD pos = this.ReCover(this._buf.ReadPointF16());
    shapes.Add((BaseShape) new PointShape(pos, this._curLayer, styluses.Generate(this._color), this._curWeight));
  }

  private void Text(ShapeList shapes, StylusTable styluses)
  {
    TextShape textShape = new TextShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    textShape.InitShort(this._buf, this._textData, this.ReCover(this._buf.ReadPointF16()));
    shapes.Add((BaseShape) textShape);
  }

  private void TextPointF(ShapeList shapes, StylusTable styluses)
  {
    TextShape textShape = new TextShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    textShape.InitShort(this._buf, this._textData, this.ReCover(this._buf.ReadPointF32()));
    shapes.Add((BaseShape) textShape);
  }

  private void ACIS() => this._buf.ReadBytes(this._buf.ReadInt32());

  private void Image(ShapeList shapes, StylusTable styluses, ImageTable images)
  {
    ImageShape imageShape = new ImageShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    imageShape.InitShort(images, this._buf, this);
    shapes.Add((BaseShape) imageShape);
  }

  private void OLE_Object(ShapeList shapes, StylusTable styluses)
  {
    OleShape oleShape = new OleShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    oleShape.InitShort(this._buf, this);
    if (oleShape.IsMetafile)
    {
      PolyLineShape polyLine = oleShape.CreatePolyLine();
      shapes.AddPolyLine(polyLine);
    }
    shapes.Add((BaseShape) oleShape);
  }

  private enum TypeShort
  {
    Old_ColorLineAcad = 1,
    Old_Polygon = 2,
    Old_Line = 3,
    Old_PolyLine = 4,
    Old_Arc = 5,
    Old_Pen = 6,
    Old_Text = 7,
    Old_Layer = 8,
    Old_NameFont = 10, // 0x0000000A
    Old_ColorFont = 11, // 0x0000000B
    Old_SizeFont = 12, // 0x0000000C
    Old_FontStyle = 13, // 0x0000000D
    Old_CharSetFont = 14, // 0x0000000E
    Old_RotateFont = 15, // 0x0000000F
    Old_ACIS = 16, // 0x00000010
    Old_Image = 17, // 0x00000011
    Old_OLE_Object = 24, // 0x00000018
    Old_WidthFont = 25, // 0x00000019
    Old_TextPointF = 27, // 0x0000001B
    Old_Point = 28, // 0x0000001C
    Old_ColorLine32 = 29, // 0x0000001D
    Old_ColorFont32 = 30, // 0x0000001E
  }
}
