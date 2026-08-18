
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.Formatter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.ShowNew.ExternFile;
using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

internal sealed class Formatter
{
  private ConvertStream _buf;
  private TextData _textData;
  /// <summary>цвет примитива</summary>
  private DwgColor _color = new DwgColor((byte) 0);
  /// <summary>слой примитива</summary>
  private ILayer _curLayer;
  /// <summary>толщина пера</summary>
  private double _curWeight;

  internal Formatter(int arSize, IntPtr ptr)
  {
    this._buf = new ConvertStream(arSize, ptr);
    this._textData = new TextData();
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
        case 12:
          this._textData.ReadSizeFontD(this._buf);
          continue;
        case 13:
          this._textData.ReadFontStyle(this._buf);
          continue;
        case 15:
          this._textData.ReadRotateFontD(this._buf);
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
          this._textData.ReadWidthFontD(this._buf);
          continue;
        case 28:
          this.Point(shapes, styluses);
          continue;
        case 29:
          this.ReadColorLine32();
          continue;
        case 31 /*0x1F*/:
          this.TextUnicode(shapes, styluses);
          continue;
        default:
          this.Unknown(curType);
          continue;
      }
    }
  }

  /// <summary>неизвестный примитив</summary>
  /// <param name="curType">тип читаемого примитива</param>
  private void Unknown(int curType) => throw new Exception($"ShowFormatter error code={curType}.");

  /// <summary>чтение Acad индекса цвета примитива</summary>
  private void ReadColorLineAcad() => this._color.AcadIndex = (uint) this._buf.ReadByte();

  /// <summary>чтение Rgb цвета примитива</summary>
  private void ReadColorLine32() => this._color.Rgb = (uint) this._buf.ReadInt32();

  /// <summary>чтение слоя примитива</summary>
  /// <param name="layers">таблица слоёв</param>
  private void ReadLayer(ILayerTable layers)
  {
    this._curLayer = layers[(int) this._buf.ReadInt16()];
  }

  /// <summary>чтение толщины пера</summary>
  private void ReadWeightLine() => this._curWeight = this._buf.ReadDouble();

  /// <summary>чтение полилинии</summary>
  /// <param name="shapes">список примитивов</param>
  /// <param name="styluses">стиль пера</param>
  /// <param name="len">длинна полилинии</param>
  private void PolyLine(ShapeList shapes, StylusTable styluses, int len)
  {
    IStylus stylus = styluses.Generate(this._color);
    PolyLineShape pline = new PolyLineShape(new List<PointD>((IEnumerable<PointD>) this._buf.ReadPointD(len)), this._curLayer, stylus, this._curWeight);
    shapes.AddPolyLine(pline);
  }

  /// <summary>чтение прлигона</summary>
  /// <param name="shapes">список примитивов</param>
  /// <param name="styluses">стиль пера</param>
  private void Polygon(ShapeList shapes, StylusTable styluses, IShowDwgWork work)
  {
    PointD[] pointDs = this._buf.ReadPointD((int) this._buf.ReadInt16());
    IStylus stylus = styluses.Generate(this._color);
    if (stylus.ColorDwg.IsEmpty)
      shapes.Add((BaseShape) new WipeOutShape(pointDs, this._curLayer, stylus, this._curWeight, work));
    else
      shapes.Add((BaseShape) new PolygonShape(pointDs, this._curLayer, stylus, this._curWeight));
  }

  /// <summary>чтение дуги</summary>
  /// <param name="shapes">список примитивов</param>
  /// <param name="styluses">стиль пера</param>
  private void Arc(ShapeList shapes, StylusTable styluses)
  {
    ArcShape arcShape = new ArcShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    arcShape.Init(this._buf);
    shapes.Add((BaseShape) arcShape);
  }

  /// <summary>чтение точки</summary>
  /// <param name="shapes">список примитивов</param>
  /// <param name="styluses">стиль пера</param>
  private void Point(ShapeList shapes, StylusTable styluses)
  {
    PointD pos = this._buf.ReadPointD();
    shapes.Add((BaseShape) new PointShape(pos, this._curLayer, styluses.Generate(this._color), this._curWeight));
  }

  /// <summary>чтение текста</summary>
  /// <param name="shapes">список примитивов</param>
  /// <param name="styluses">стиль пера</param>
  private void Text(ShapeList shapes, StylusTable styluses)
  {
    TextShape textShape = new TextShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    PointD insert = this._buf.ReadPointD();
    byte gdiCharSet = this._buf.ReadByte();
    this._textData.ReadCharSetFont(gdiCharSet);
    string text = this._buf.ReadStringCodePage(this._buf.ReadBytes((int) this._buf.ReadInt16()), this._textData.EncodingText);
    if (gdiCharSet == (byte) 163 && text.Length == 1 && text[0] == 'Ø')
      text = "∅";
    textShape.Init(insert, text, this._textData);
    shapes.Add((BaseShape) textShape);
  }

  /// <summary>чтение текста</summary>
  /// <param name="shapes">список примитивов</param>
  /// <param name="styluses">стиль пера</param>
  private void TextUnicode(ShapeList shapes, StylusTable styluses)
  {
    TextShape textShape = new TextShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    PointD insert = this._buf.ReadPointD();
    string text = new string(Array.ConvertAll<short, char>(this._buf.ReadInt16((int) this._buf.ReadInt16()), (Converter<short, char>) (x => (char) x)));
    textShape.Init(insert, text, this._textData);
    shapes.Add((BaseShape) textShape);
  }

  /// <summary>чтение ACIS модели(неиспользуется)</summary>
  private void ACIS() => this._buf.ReadBytes(this._buf.ReadInt32());

  /// <summary>чтение рисунка</summary>
  /// <param name="shapes">список примитивов</param>
  /// <param name="styluses">стиль пера</param>
  /// <param name="images">список рисунков</param>
  private void Image(ShapeList shapes, StylusTable styluses, ImageTable images)
  {
    ImageShape imageShape = new ImageShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    imageShape.Init(images, this._buf);
    shapes.Add((BaseShape) imageShape);
  }

  /// <summary>чтение OLE</summary>
  /// <param name="shapes">список примитивов</param>
  /// <param name="styluses">стиль пера</param>
  private void OLE_Object(ShapeList shapes, StylusTable styluses)
  {
    OleShape oleShape = new OleShape(this._curLayer, styluses.Generate(this._color), this._curWeight);
    oleShape.Init(this._buf);
    if (oleShape.IsMetafile)
    {
      PolyLineShape polyLine = oleShape.CreatePolyLine();
      shapes.AddPolyLine(polyLine);
    }
    shapes.Add((BaseShape) oleShape);
  }

  private enum TypeShape
  {
    n_ColorLineAcad = 1,
    n_Polygon = 2,
    n_Line = 3,
    n_PolyLine = 4,
    n_Arc = 5,
    n_Pen = 6,
    n_Text = 7,
    n_Layer = 8,
    n_NameFont = 10, // 0x0000000A
    n_SizeFont = 12, // 0x0000000C
    n_FontStyle = 13, // 0x0000000D
    n_RotateFont = 15, // 0x0000000F
    n_ACIS = 16, // 0x00000010
    n_Image = 17, // 0x00000011
    n_OLE_Object = 24, // 0x00000018
    n_WidthFont = 25, // 0x00000019
    n_Point = 28, // 0x0000001C
    n_ColorLineRGB = 29, // 0x0000001D
    n_TextUnicode = 31, // 0x0000001F
  }
}
