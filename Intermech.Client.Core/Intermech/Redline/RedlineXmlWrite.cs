
// Type: Intermech.Redline.RedlineXmlWrite
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Map;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Xml;


namespace Intermech.Redline;

internal class RedlineXmlWrite
{
  internal bool ConvertMMtoPixels { get; set; }

  internal float PixelsPerMM { get; set; } = 3.77952766f;

  /// <summary>записать данные для заливки</summary>
  /// <param name="brush"></param>
  /// <param name="writer">поток с данными XML</param>
  /// <param name="pen">заливка</param>
  /// <returns>true - запись данных успешна</returns>
  private void StoreXml_Brush(Brush brush, XmlTextWriter writer)
  {
    if (brush == null || !(brush is SolidBrush solidBrush))
      return;
    writer.WriteAttributeString("brush.Color", XmlConvert.ToString(solidBrush.Color.ToArgb()));
  }

  /// <summary> записать данные для пера</summary>
  /// <param name="pen">перо</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true - запись данных успешна</returns>
  private void StoreXml_Pen(Pen pen, XmlTextWriter writer)
  {
    if (pen == null)
      return;
    writer.WriteAttributeString("pen.Color", XmlConvert.ToString(pen.Color.ToArgb()));
    double num = this.ConvertMMtoPixels ? (double) pen.Width : Math.Floor((double) pen.Width * (double) this.PixelsPerMM);
    writer.WriteAttributeString("pen.Width", XmlConvert.ToString(num));
    writer.WriteAttributeString("pen.DashStyle", pen.DashStyle.ToString());
  }

  /// <summary> записать данные для редактируемости примитива</summary>
  /// <param name="pen">перо</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true - запись данных успешна</returns>
  private void StoreXml_OnlyView(Pen pen, XmlTextWriter writer)
  {
  }

  /// <summary> записать данные для даты создания примитива</summary>
  /// <param name="time">дата создания примитива</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true - запись данных успешна</returns>
  private void StoreXml_CreateTime(DateTime time, XmlTextWriter writer)
  {
    writer.WriteAttributeString("CreateTime", XmlConvert.ToString(time, XmlDateTimeSerializationMode.Local));
  }

  private void StoreXml_RedlineLayer(RedlineLayer obj, XmlTextWriter writer)
  {
    writer.WriteAttributeString("UserID", obj.UserID);
    writer.WriteAttributeString("NameRemark", obj.NameRemark);
    writer.WriteAttributeString("Comment", obj.Comment);
    writer.WriteAttributeString("Time", XmlConvert.ToString(obj.Time, XmlDateTimeSerializationMode.Local));
    writer.WriteAttributeString("NameBusiness", obj.NameBusiness);
    writer.WriteAttributeString("StepBusiness", obj.StepBusiness);
    writer.WriteAttributeString("Signature", obj.Signature);
    writer.WriteAttributeString("StatusRemark", obj.StatusRemark.GetName<EStatusRemark>());
    writer.WriteAttributeString("LockRemark", obj.LockRemark.ToString());
    writer.WriteAttributeString("RedObjectID", obj.RedObjectID.ToString());
    writer.WriteAttributeString("ParentID", obj.ParentID.ToString());
  }

  /// <summary> записать данные для даты создания примитива</summary>
  /// <param name="time">дата создания примитива</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true - запись данных успешна</returns>
  private void StoreXml_ModificationTime(DateTime time, XmlTextWriter writer)
  {
    writer.WriteAttributeString("ModificationTime", XmlConvert.ToString(time, XmlDateTimeSerializationMode.Local));
  }

  /// <summary> </summary>
  /// <param name="varRelativeId"></param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true - запись данных успешна</returns>
  private void StoreXml_RelativeId(string varRelativeId, XmlTextWriter writer)
  {
    if (varRelativeId == null)
      return;
    writer.WriteAttributeString("Id", varRelativeId);
  }

  internal bool WriteMapLayer(MapLayer layer, XmlTextWriter writer)
  {
    if (!(layer.Identifier is RedlineLayer identifier) || layer.IsEmpty)
      return false;
    writer.WriteStartElement("MapLayer");
    this.StoreXml_RedlineLayer(identifier, writer);
    bool flag = false;
    foreach (MapObject mapObject in layer.GetEnumerator())
    {
      switch (mapObject.GetType().Name)
      {
        case "MapRedEllipse":
          flag &= this.StoreXmlMapRedEllipse(mapObject, writer);
          continue;
        case "MapRedRectangle":
          flag &= this.StoreXmlMapRectangle(mapObject, writer);
          continue;
        case "MapRedCircle":
          flag &= this.StoreXmlMapRedCircle(mapObject, writer);
          continue;
        case "MapRedStroke":
          flag &= this.StoreXmlMapRedStroke(mapObject, writer);
          continue;
        case "MapRedPencil":
          flag &= this.StoreXmlMapRedPencil(mapObject, writer);
          continue;
        case "MapRedNote":
          flag &= this.StoreXmlMapRedNote(mapObject, writer);
          continue;
        default:
          continue;
      }
    }
    writer.WriteEndElement();
    return flag;
  }

  /// <summary>создать запись в формате  Xml</summary>
  /// <param name="obj">объект</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true если объект был MapRedStroke; иначе false</returns>
  private bool StoreXmlMapRedStroke(MapObject obj, XmlTextWriter writer)
  {
    if (!(obj is MapRedStroke mapRedStroke) || this.IsEmpty(mapRedStroke.CopyPointsArray(), 0.01f))
      return false;
    writer.WriteStartElement("RedStroke");
    this.StoreXml_Pen(mapRedStroke.Pen, writer);
    this.StoreXml_OnlyView(mapRedStroke.Pen, writer);
    string str = "";
    PointFConverter pointFconverter = new PointFConverter();
    PointF basePoint = mapRedStroke.BasePoint;
    for (int i = 0; i < mapRedStroke.PointsCount; ++i)
    {
      if (i > 0)
        str += "|";
      PointF point = mapRedStroke.GetPoint(i);
      point.X -= basePoint.X;
      point.Y -= basePoint.Y;
      point.X /= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
      point.Y /= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
      str += pointFconverter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) point);
    }
    writer.WriteAttributeString("points", str);
    this.StoreXml_RelativeId(mapRedStroke.RelativeId, writer);
    this.StoreXml_ModificationTime(mapRedStroke.ModificationTime, writer);
    this.StoreXml_CreateTime(mapRedStroke.CreateTime, writer);
    writer.WriteEndElement();
    return true;
  }

  private bool IsEmpty(PointF[] points, float fizz)
  {
    if (points == null || points.Length < 2)
      return true;
    foreach (PointF point in points)
    {
      if ((double) Math.Abs(point.X - points[0].X) > (double) fizz || (double) Math.Abs(point.Y - points[0].Y) > (double) fizz)
        return false;
    }
    return true;
  }

  /// <summary>создать запись в формате  Xml</summary>
  /// <param name="obj">объект</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true если объект был MapRedPencil; иначе false</returns>
  private bool StoreXmlMapRedPencil(MapObject obj, XmlTextWriter writer)
  {
    if (!(obj is MapRedPencil mapRedPencil) || this.IsEmpty(mapRedPencil.CopyPointsArray(), 0.01f))
      return false;
    writer.WriteStartElement("RedPencil");
    this.StoreXml_Pen(mapRedPencil.Pen, writer);
    this.StoreXml_OnlyView(mapRedPencil.Pen, writer);
    string str = "";
    PointFConverter pointFconverter = new PointFConverter();
    PointF basePoint = mapRedPencil.BasePoint;
    for (int i = 0; i < mapRedPencil.PointsCount; ++i)
    {
      if (i > 0)
        str += "|";
      PointF point = mapRedPencil.GetPoint(i);
      point.X -= basePoint.X;
      point.Y -= basePoint.Y;
      point.X /= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
      point.Y /= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
      str += pointFconverter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) point);
    }
    writer.WriteAttributeString("points", str);
    this.StoreXml_RelativeId(mapRedPencil.RelativeId, writer);
    this.StoreXml_ModificationTime(mapRedPencil.ModificationTime, writer);
    this.StoreXml_CreateTime(mapRedPencil.CreateTime, writer);
    writer.WriteEndElement();
    return true;
  }

  /// <summary>создать запись в формате  Xml</summary>
  /// <param name="obj">объект</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true если объект был MapRedRectangle;иначе false</returns>
  private bool StoreXmlMapRectangle(MapObject obj, XmlTextWriter writer)
  {
    if (!(obj is MapRedRectangle mapRedRectangle) || (double) mapRedRectangle.Bounds.Width < 0.01 || (double) mapRedRectangle.Bounds.Height < 0.01)
      return false;
    writer.WriteStartElement("RedRectangle");
    this.StoreXml_Pen(mapRedRectangle.Pen, writer);
    this.StoreXml_Brush(mapRedRectangle.Brush, writer);
    this.StoreXml_OnlyView(mapRedRectangle.Pen, writer);
    PointF basePoint = mapRedRectangle.BasePoint;
    RectangleF bounds = mapRedRectangle.Bounds;
    float num1 = (float) (((double) bounds.X - (double) basePoint.X) / (this.ConvertMMtoPixels ? (double) this.PixelsPerMM : 1.0));
    bounds = mapRedRectangle.Bounds;
    float num2 = (float) (((double) bounds.Y - (double) basePoint.Y) / (this.ConvertMMtoPixels ? (double) this.PixelsPerMM : 1.0));
    bounds = mapRedRectangle.Bounds;
    float num3 = bounds.Width / (this.ConvertMMtoPixels ? this.PixelsPerMM : 1f);
    bounds = mapRedRectangle.Bounds;
    float num4 = bounds.Height / (this.ConvertMMtoPixels ? this.PixelsPerMM : 1f);
    writer.WriteAttributeString("x", XmlConvert.ToString(num1));
    writer.WriteAttributeString("y", XmlConvert.ToString(num2));
    writer.WriteAttributeString("width", XmlConvert.ToString(num3));
    writer.WriteAttributeString("height", XmlConvert.ToString(num4));
    this.StoreXml_RelativeId(mapRedRectangle.RelativeId, writer);
    this.StoreXml_ModificationTime(mapRedRectangle.ModificationTime, writer);
    this.StoreXml_CreateTime(mapRedRectangle.CreateTime, writer);
    writer.WriteEndElement();
    return true;
  }

  /// <summary>создать запись в формате  Xml</summary>
  /// <param name="obj">объект</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true если объект был MapRedCircle;иначе false</returns>
  private bool StoreXmlMapRedCircle(MapObject obj, XmlTextWriter writer)
  {
    if (!(obj is MapRedCircle mapRedCircle) || (double) mapRedCircle.Bounds.Width < 0.01 || (double) mapRedCircle.Bounds.Height < 0.01)
      return false;
    writer.WriteStartElement("RedCircle");
    this.StoreXml_Pen(mapRedCircle.Pen, writer);
    this.StoreXml_Brush(mapRedCircle.Brush, writer);
    this.StoreXml_OnlyView(mapRedCircle.Pen, writer);
    PointF basePoint = mapRedCircle.BasePoint;
    RectangleF bounds = mapRedCircle.Bounds;
    float num1 = (float) (((double) bounds.X - (double) basePoint.X) / (this.ConvertMMtoPixels ? (double) this.PixelsPerMM : 1.0));
    bounds = mapRedCircle.Bounds;
    float num2 = (float) (((double) bounds.Y - (double) basePoint.Y) / (this.ConvertMMtoPixels ? (double) this.PixelsPerMM : 1.0));
    bounds = mapRedCircle.Bounds;
    float num3 = bounds.Width / (this.ConvertMMtoPixels ? this.PixelsPerMM : 1f);
    bounds = mapRedCircle.Bounds;
    float num4 = bounds.Height / (this.ConvertMMtoPixels ? this.PixelsPerMM : 1f);
    writer.WriteAttributeString("x", XmlConvert.ToString(num1));
    writer.WriteAttributeString("y", XmlConvert.ToString(num2));
    writer.WriteAttributeString("width", XmlConvert.ToString(num3));
    writer.WriteAttributeString("height", XmlConvert.ToString(num4));
    this.StoreXml_RelativeId(mapRedCircle.RelativeId, writer);
    this.StoreXml_ModificationTime(mapRedCircle.ModificationTime, writer);
    this.StoreXml_CreateTime(mapRedCircle.CreateTime, writer);
    writer.WriteEndElement();
    return true;
  }

  /// <summary>создать запись в формате  Xml</summary>
  /// <param name="obj">объект</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true если объект был MapRedEllipse;иначе false</returns>
  private bool StoreXmlMapRedEllipse(MapObject obj, XmlTextWriter writer)
  {
    if (!(obj is MapRedEllipse mapRedEllipse) || (double) mapRedEllipse.Bounds.Width < 0.01 || (double) mapRedEllipse.Bounds.Height < 0.01)
      return false;
    writer.WriteStartElement("RedEllipse");
    this.StoreXml_Pen(mapRedEllipse.Pen, writer);
    this.StoreXml_OnlyView(mapRedEllipse.Pen, writer);
    this.StoreXml_Brush(mapRedEllipse.Brush, writer);
    PointF basePoint = mapRedEllipse.BasePoint;
    RectangleF bounds = mapRedEllipse.Bounds;
    float num1 = (float) (((double) bounds.X - (double) basePoint.X) / (this.ConvertMMtoPixels ? (double) this.PixelsPerMM : 1.0));
    bounds = mapRedEllipse.Bounds;
    float num2 = (float) (((double) bounds.Y - (double) basePoint.Y) / (this.ConvertMMtoPixels ? (double) this.PixelsPerMM : 1.0));
    bounds = mapRedEllipse.Bounds;
    float num3 = bounds.Width / (this.ConvertMMtoPixels ? this.PixelsPerMM : 1f);
    bounds = mapRedEllipse.Bounds;
    float num4 = bounds.Height / (this.ConvertMMtoPixels ? this.PixelsPerMM : 1f);
    writer.WriteAttributeString("x", XmlConvert.ToString(num1));
    writer.WriteAttributeString("y", XmlConvert.ToString(num2));
    writer.WriteAttributeString("width", XmlConvert.ToString(num3));
    writer.WriteAttributeString("height", XmlConvert.ToString(num4));
    this.StoreXml_RelativeId(mapRedEllipse.RelativeId, writer);
    this.StoreXml_ModificationTime(mapRedEllipse.ModificationTime, writer);
    this.StoreXml_CreateTime(mapRedEllipse.CreateTime, writer);
    writer.WriteEndElement();
    return true;
  }

  /// <summary>создать запись в формате  Xml</summary>
  /// <param name="obj">объект</param>
  /// <param name="writer">поток с данными XML</param>
  /// <returns>true если объект был MapRedNote;иначе false</returns>
  private bool StoreXmlMapRedNote(MapObject obj, XmlTextWriter writer)
  {
    if (!(obj is MapRedNote mapRedNote) || mapRedNote.Text == null || mapRedNote.Text.Length < 1)
      return false;
    writer.WriteStartElement("MapRedNote");
    PointFConverter pointFconverter = new PointFConverter();
    this.StoreXml_Pen(mapRedNote.Pen, writer);
    this.StoreXml_Brush(mapRedNote.Brush, writer);
    this.StoreXml_OnlyView(mapRedNote.Pen, writer);
    writer.WriteAttributeString("fontName", mapRedNote.FontName);
    string str = mapRedNote.FontSize.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    if (this.ConvertMMtoPixels)
      str += "px";
    writer.WriteAttributeString("fontSize", str);
    writer.WriteAttributeString("text.Color", XmlConvert.ToString(mapRedNote.TextColor.ToArgb()));
    writer.WriteAttributeString("noteStyle", mapRedNote.NoteStyle.GetName<IRedNoteStyle>());
    float num1 = this.ConvertMMtoPixels ? mapRedNote.Facet / this.PixelsPerMM : mapRedNote.Facet;
    writer.WriteAttributeString("facet", num1.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    writer.WriteAttributeString("noteArrow", mapRedNote.NoteArrow.GetName<IRedArrowStyle>());
    float num2 = this.ConvertMMtoPixels ? mapRedNote.ArrowSize / this.PixelsPerMM : mapRedNote.ArrowSize;
    writer.WriteAttributeString("arrowSize", num2.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    SizeF sizeF1 = new SizeF(mapRedNote.BasePoint);
    PointF pointF1 = mapRedNote.PlaceNote - sizeF1;
    pointF1.X /= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
    pointF1.Y /= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
    PointF pointF2 = mapRedNote.NoteLocation - sizeF1;
    pointF2.X /= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
    pointF2.Y /= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
    writer.WriteAttributeString("place", pointFconverter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) pointF1));
    writer.WriteAttributeString("location", pointFconverter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) pointF2));
    writer.WriteAttributeString("text", mapRedNote.Text);
    SizeF sizeF2 = mapRedNote.NoteSize;
    if (this.ConvertMMtoPixels)
      sizeF2 = new SizeF(sizeF2.Width / this.PixelsPerMM, sizeF2.Height / this.PixelsPerMM);
    writer.WriteAttributeString("size", new MapSizeFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) sizeF2));
    this.StoreXml_RelativeId(mapRedNote.RelativeId, writer);
    this.StoreXml_ModificationTime(mapRedNote.ModificationTime, writer);
    this.StoreXml_CreateTime(mapRedNote.CreateTime, writer);
    writer.WriteEndElement();
    return true;
  }
}
