
// Type: Intermech.Redline.RedlineXmlRead
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Map;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Xml;


namespace Intermech.Redline;

internal class RedlineXmlRead
{
  internal IMapRelative Relative { get; set; }

  internal bool ConvertMMtoPixels { get; set; }

  internal float PixelsPerMM { get; set; } = 3.77952766f;

  public int Version { get; internal set; }

  /// <summary>прочитать данные для заливки</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>заливка</returns>
  private Brush LoadXml_Brush(XmlNode item)
  {
    XmlAttribute attribute = item.Attributes?["brush.Color"];
    return attribute == null ? (Brush) null : (Brush) new SolidBrush(Color.FromArgb(XmlConvert.ToInt32(attribute.Value)));
  }

  /// <summary>прочитать данные для пера</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>перо</returns>
  private Pen LoadXml_Pen(XmlNode item)
  {
    XmlAttribute attribute1 = item.Attributes?["pen.Color"];
    Color color = attribute1 != null ? Color.FromArgb(XmlConvert.ToInt32(attribute1.Value)) : Color.Red;
    XmlAttribute attribute2 = item.Attributes?["pen.Width"];
    float num = attribute2 != null ? XmlConvert.ToSingle(attribute2.Value) : 0.0f;
    if (!this.ConvertMMtoPixels)
      num /= this.PixelsPerMM;
    double width = (double) num;
    Pen pen = new Pen(color, (float) width);
    XmlAttribute attribute3 = item.Attributes?["pen.DashStyle"];
    if (attribute3 != null)
    {
      DashStyle dashStyle = (DashStyle) Enum.Parse(typeof (DashStyle), attribute3.Value, true);
      pen.DashStyle = dashStyle;
    }
    return pen;
  }

  /// <summary>прочитать данные для редактируемости примитива</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>true - только просмотр примитива</returns>
  private bool LoadXml_OnlyView(XmlNode item)
  {
    XmlAttribute attribute = item.Attributes?["pen.Color"];
    return attribute != null && (XmlConvert.ToInt32(attribute.Value) & 16777215 /*0xFFFFFF*/) == 4210752 /*0x404040*/;
  }

  /// <summary>прочитать данные для пера</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>дата создания примитива</returns>
  private DateTime LoadXml_CreateTime(XmlNode item)
  {
    DateTime now = DateTime.Now;
    XmlAttribute attribute = item.Attributes?["CreateTime"];
    if (attribute != null)
      now = DateTime.Parse(attribute.Value);
    return now;
  }

  private void LoadXml_RedlineLayer(RedlineLayer obj, XmlNode item)
  {
    obj.UserID = "";
    XmlAttribute attribute1;
    if ((attribute1 = item.Attributes?["UserID"]) != null)
      obj.UserID = attribute1.Value;
    obj.NameRemark = "";
    XmlAttribute attribute2;
    if ((attribute2 = item.Attributes?["NameRemark"]) != null)
      obj.NameRemark = attribute2.Value;
    obj.Comment = "";
    XmlAttribute attribute3;
    if ((attribute3 = item.Attributes?["Comment"]) != null)
      obj.Comment = attribute3.Value;
    obj.NameBusiness = "";
    XmlAttribute attribute4;
    if ((attribute4 = item.Attributes?["NameBusiness"]) != null)
      obj.NameBusiness = attribute4.Value;
    obj.StepBusiness = "";
    XmlAttribute attribute5;
    if ((attribute5 = item.Attributes?["StepBusiness"]) != null)
      obj.StepBusiness = attribute5.Value;
    obj.Time = DateTime.Now;
    XmlAttribute attribute6;
    if ((attribute6 = item.Attributes?["Time"]) != null)
      obj.Time = DateTime.Parse(attribute6.Value);
    obj.Signature = "";
    XmlAttribute attribute7;
    if ((attribute7 = item.Attributes?["Signature"]) != null)
      obj.Signature = attribute7.Value.Split('|')[0];
    obj.StatusRemark = EStatusRemark.eInconsistent;
    XmlAttribute attribute8;
    if ((attribute8 = item.Attributes?["StatusRemark"]) != null)
      obj.StatusRemark = attribute8.Value.ToEnum<EStatusRemark>();
    obj.LockRemark = false;
    XmlAttribute attribute9;
    if ((attribute9 = item.Attributes?["LockRemark"]) != null)
      obj.LockRemark = bool.Parse(attribute9.Value);
    obj.RedObjectID = 0UL;
    XmlAttribute attribute10;
    if ((attribute10 = item.Attributes?["RedObjectID"]) != null)
      obj.RedObjectID = ulong.Parse(attribute10.Value);
    obj.ParentID = 0UL;
    XmlAttribute attribute11;
    if ((attribute11 = item.Attributes?["ParentID"]) != null)
      obj.ParentID = ulong.Parse(attribute11.Value);
    XmlAttribute attribute12;
    if ((attribute12 = item.Attributes?["Identifier"]) == null)
      return;
    QuickObjectInfo objectInfo;
    objectInfo.Caption = attribute12.Value;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        objectInfo = sessionKeeper.Session.GetObjectInfo(new Guid(attribute12.Value));
        objectInfo.Caption = $"{objectInfo.Caption}|{attribute12.Value}";
      }
    }
    catch (Exception ex)
    {
    }
    obj.UserID = objectInfo.Caption;
    obj.NameRemark = "Замечание";
  }

  /// <summary>прочитать данные для пера</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>дата создания примитива</returns>
  private DateTime LoadXml_ModificationTime(XmlNode item)
  {
    DateTime now = DateTime.Now;
    XmlAttribute attribute = item.Attributes?["ModificationTime"];
    if (attribute != null)
      now = DateTime.Parse(attribute.Value);
    return now;
  }

  /// <summary></summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns></returns>
  private string LoadXml_RelativeId(XmlNode item)
  {
    XmlAttribute attribute = item.Attributes?["Id"];
    return attribute != null ? attribute.Value : this.Relative.GetCurrentPageId();
  }

  internal MapLayer LoadXmlMapLayer(XmlNode root, MapLayerCollection Layers)
  {
    if (root.Name != "MapLayer")
      return (MapLayer) null;
    RedlineLayer redlineLayer = new RedlineLayer();
    this.LoadXml_RedlineLayer(redlineLayer, root);
    MapLayer mapLayer = (MapLayer) null;
    if (Layers != null)
    {
      mapLayer = Layers.CreateNewLayerAfter(Layers.Default);
      mapLayer.Identifier = (object) redlineLayer;
      mapLayer.Add((MapObject) redlineLayer.CreateCommentText());
      mapLayer.Add((MapObject) redlineLayer.CreateSignatureText());
    }
    foreach (XmlNode xmlNode in root)
    {
      if (xmlNode.NodeType == XmlNodeType.Element)
      {
        MapObject mapObject;
        switch (xmlNode.Name)
        {
          case "RedCircle":
            mapObject = (MapObject) this.LoadXmlMapRedCircle(xmlNode);
            break;
          case "RedRectangle":
            mapObject = (MapObject) this.LoadXmlMapRedRectangle(xmlNode);
            break;
          case "RedEllipse":
            mapObject = (MapObject) this.LoadXmlMapRedEllipse(xmlNode);
            break;
          case "RedStroke":
            mapObject = (MapObject) this.LoadXmlMapRedStroke(xmlNode);
            break;
          case "RedPencil":
            mapObject = (MapObject) this.LoadXmlMapRedPencil(xmlNode);
            break;
          case "MapRedNote":
            mapObject = (MapObject) this.LoadXmlMapRedNote(xmlNode);
            break;
          default:
            continue;
        }
        if (mapObject != null && mapLayer != null)
        {
          mapLayer.Add(mapObject);
          ((IMapToolTipText) mapObject).GenerateToolTipText();
        }
      }
    }
    redlineLayer.UndoManager.Clear();
    return mapLayer;
  }

  /// <summary>проверить : описывает узел MapStroke</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>возвращает если item описывал MapRedStroke; иначе null</returns>
  private MapRedStroke LoadXmlMapRedStroke(XmlNode item)
  {
    if (item.Name != "RedStroke")
      return (MapRedStroke) null;
    PointF[] points = (PointF[]) null;
    XmlAttribute attribute = item.Attributes?["points"];
    if (attribute != null)
    {
      string[] strArray = attribute.Value.Split('|');
      points = new PointF[strArray.Length];
      PointFConverter pointFconverter = new PointFConverter();
      for (int index = 0; index < strArray.Length; ++index)
        points[index] = (PointF) pointFconverter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, strArray[index]);
    }
    if (this.IsEmpty(points, 0.01f))
      return (MapRedStroke) null;
    MapRedStroke mapRedStroke1 = new MapRedStroke();
    mapRedStroke1.Pen = this.LoadXml_Pen(item);
    MapRedStroke mapRedStroke2 = mapRedStroke1;
    bool viewOlny = this.LoadXml_OnlyView(item);
    Redliner.SetOnlyViewObject((MapObject) mapRedStroke2, viewOlny);
    string id = this.LoadXml_RelativeId(item);
    PointF pointF = id == null || this.Relative == null ? PointF.Empty : this.Relative.GetBasePoint(id);
    if (points != null)
    {
      for (int index = 0; index < points.Length; ++index)
      {
        points[index].X *= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
        points[index].Y *= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
        points[index].X += pointF.X;
        points[index].Y += pointF.Y;
      }
      mapRedStroke2.SetPoints(points);
    }
    mapRedStroke2.Relative = this.Relative;
    mapRedStroke2.RelativeId = id;
    mapRedStroke2.ModificationTime = this.LoadXml_ModificationTime(item);
    mapRedStroke2.CreateTime = this.LoadXml_CreateTime(item);
    return mapRedStroke2;
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

  /// <summary>проверить : описывает узел MapStroke</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>возвращает если item описывал MapRedPencil; иначе null</returns>
  private MapRedPencil LoadXmlMapRedPencil(XmlNode item)
  {
    if (item.Name != "RedPencil")
      return (MapRedPencil) null;
    PointF[] points = (PointF[]) null;
    XmlAttribute attribute = item.Attributes?["points"];
    if (attribute != null)
    {
      string[] strArray = attribute.Value.Split('|');
      points = new PointF[strArray.Length];
      PointFConverter pointFconverter = new PointFConverter();
      for (int index = 0; index < strArray.Length; ++index)
        points[index] = (PointF) pointFconverter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, strArray[index]);
    }
    if (this.IsEmpty(points, 0.01f))
      return (MapRedPencil) null;
    MapRedPencil mapRedPencil1 = new MapRedPencil();
    mapRedPencil1.Pen = this.LoadXml_Pen(item);
    MapRedPencil mapRedPencil2 = mapRedPencil1;
    bool viewOlny = this.LoadXml_OnlyView(item);
    Redliner.SetOnlyViewObject((MapObject) mapRedPencil2, viewOlny);
    string id = this.LoadXml_RelativeId(item);
    PointF pointF = id == null || this.Relative == null ? PointF.Empty : this.Relative.GetBasePoint(id);
    if (points != null)
    {
      for (int index = 0; index < points.Length; ++index)
      {
        points[index].X *= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
        points[index].Y *= this.ConvertMMtoPixels ? this.PixelsPerMM : 1f;
        points[index].X += pointF.X;
        points[index].Y += pointF.Y;
      }
      mapRedPencil2.SetPoints(points);
    }
    mapRedPencil2.Relative = this.Relative;
    mapRedPencil2.RelativeId = id;
    mapRedPencil2.ModificationTime = this.LoadXml_ModificationTime(item);
    mapRedPencil2.CreateTime = this.LoadXml_CreateTime(item);
    return mapRedPencil2;
  }

  /// <summary>проверить : описывает узел MapRedRectangle</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>возвращает если item описывал MapRedRectangle;иначе null</returns>
  private MapRedRectangle LoadXmlMapRedRectangle(XmlNode item)
  {
    if (item.Name != "RedRectangle")
      return (MapRedRectangle) null;
    XmlAttribute attribute1 = item.Attributes?["width"];
    float width = attribute1 != null ? XmlConvert.ToSingle(attribute1.Value) : 0.0f;
    XmlAttribute attribute2 = item.Attributes?["height"];
    float height = attribute2 != null ? XmlConvert.ToSingle(attribute2.Value) : 0.0f;
    if ((double) width < 0.01 || (double) height < 0.01)
      return (MapRedRectangle) null;
    MapRedRectangle mapRedRectangle = new MapRedRectangle();
    mapRedRectangle.Pen = this.LoadXml_Pen(item);
    mapRedRectangle.Brush = this.LoadXml_Brush(item);
    Redliner.SetOnlyViewObject((MapObject) mapRedRectangle, this.LoadXml_OnlyView(item));
    string id = this.LoadXml_RelativeId(item);
    PointF pointF = id == null || this.Relative == null ? PointF.Empty : this.Relative.GetBasePoint(id);
    XmlAttribute attribute3 = item.Attributes?["x"];
    float num1 = attribute3 != null ? XmlConvert.ToSingle(attribute3.Value) : 0.0f;
    XmlAttribute attribute4 = item.Attributes?["y"];
    float num2 = attribute4 != null ? XmlConvert.ToSingle(attribute4.Value) : 0.0f;
    if (this.ConvertMMtoPixels)
    {
      num1 *= this.PixelsPerMM;
      num2 *= this.PixelsPerMM;
      width *= this.PixelsPerMM;
      height *= this.PixelsPerMM;
    }
    mapRedRectangle.Bounds = new RectangleF(num1 + pointF.X, num2 + pointF.Y, width, height);
    mapRedRectangle.Relative = this.Relative;
    mapRedRectangle.RelativeId = id;
    mapRedRectangle.ModificationTime = this.LoadXml_ModificationTime(item);
    mapRedRectangle.CreateTime = this.LoadXml_CreateTime(item);
    return mapRedRectangle;
  }

  /// <summary>проверить : описывает узел MapRedCircle</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>возвращает если item описывал MapRedCircle;иначе null</returns>
  private MapRedCircle LoadXmlMapRedCircle(XmlNode item)
  {
    if (item.Name != "RedCircle")
      return (MapRedCircle) null;
    XmlAttribute attribute1 = item.Attributes?["width"];
    float width = attribute1 != null ? XmlConvert.ToSingle(attribute1.Value) : 0.0f;
    XmlAttribute attribute2 = item.Attributes?["height"];
    float height = attribute2 != null ? XmlConvert.ToSingle(attribute2.Value) : 0.0f;
    if ((double) width < 0.01 || (double) height < 0.01)
      return (MapRedCircle) null;
    MapRedCircle mapRedCircle = new MapRedCircle();
    mapRedCircle.Pen = this.LoadXml_Pen(item);
    mapRedCircle.Brush = this.LoadXml_Brush(item);
    Redliner.SetOnlyViewObject((MapObject) mapRedCircle, this.LoadXml_OnlyView(item));
    string id = this.LoadXml_RelativeId(item);
    PointF pointF = id == null || this.Relative == null ? PointF.Empty : this.Relative.GetBasePoint(id);
    XmlAttribute attribute3 = item.Attributes?["x"];
    float num1 = attribute3 != null ? XmlConvert.ToSingle(attribute3.Value) : 0.0f;
    XmlAttribute attribute4 = item.Attributes?["y"];
    float num2 = attribute4 != null ? XmlConvert.ToSingle(attribute4.Value) : 0.0f;
    if (this.ConvertMMtoPixels)
    {
      num1 *= this.PixelsPerMM;
      num2 *= this.PixelsPerMM;
      width *= this.PixelsPerMM;
      height *= this.PixelsPerMM;
    }
    mapRedCircle.Bounds = new RectangleF(num1 + pointF.X, num2 + pointF.Y, width, height);
    mapRedCircle.Relative = this.Relative;
    mapRedCircle.RelativeId = id;
    mapRedCircle.ModificationTime = this.LoadXml_ModificationTime(item);
    mapRedCircle.CreateTime = this.LoadXml_CreateTime(item);
    return mapRedCircle;
  }

  /// <summary>проверить : описывает узел MapRedEllipse</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>возвращает если item описывал MapRedEllipse;иначе null</returns>
  private MapRedEllipse LoadXmlMapRedEllipse(XmlNode item)
  {
    if (item.Name != "RedEllipse")
      return (MapRedEllipse) null;
    XmlAttribute attribute1 = item.Attributes?["width"];
    float width = attribute1 != null ? XmlConvert.ToSingle(attribute1.Value) : 0.0f;
    XmlAttribute attribute2 = item.Attributes?["height"];
    float height = attribute2 != null ? XmlConvert.ToSingle(attribute2.Value) : 0.0f;
    if ((double) width < 0.01 || (double) height < 0.01)
      return (MapRedEllipse) null;
    MapRedEllipse mapRedEllipse = new MapRedEllipse();
    mapRedEllipse.Pen = this.LoadXml_Pen(item);
    mapRedEllipse.Brush = this.LoadXml_Brush(item);
    Redliner.SetOnlyViewObject((MapObject) mapRedEllipse, this.LoadXml_OnlyView(item));
    string id = this.LoadXml_RelativeId(item);
    PointF pointF = id == null || this.Relative == null ? PointF.Empty : this.Relative.GetBasePoint(id);
    XmlAttribute attribute3 = item.Attributes?["x"];
    float num1 = attribute3 != null ? XmlConvert.ToSingle(attribute3.Value) : 0.0f;
    XmlAttribute attribute4 = item.Attributes?["y"];
    float num2 = attribute4 != null ? XmlConvert.ToSingle(attribute4.Value) : 0.0f;
    if (this.ConvertMMtoPixels)
    {
      num1 *= this.PixelsPerMM;
      num2 *= this.PixelsPerMM;
      width *= this.PixelsPerMM;
      height *= this.PixelsPerMM;
    }
    mapRedEllipse.Bounds = new RectangleF(num1 + pointF.X, num2 + pointF.Y, width, height);
    mapRedEllipse.Relative = this.Relative;
    mapRedEllipse.RelativeId = id;
    mapRedEllipse.ModificationTime = this.LoadXml_ModificationTime(item);
    mapRedEllipse.CreateTime = this.LoadXml_CreateTime(item);
    return mapRedEllipse;
  }

  /// <summary>проверить : описывает узел MapComment</summary>
  /// <param name="item">отдельный узел в XML-документе</param>
  /// <returns>возвращает если item описывал MapRedNote;иначе null</returns>
  private MapRedNote LoadXmlMapRedNote(XmlNode item)
  {
    if (item.Name != "MapRedNote")
      return (MapRedNote) null;
    string id = this.LoadXml_RelativeId(item);
    PointF pointF1 = id == null || this.Relative == null ? PointF.Empty : this.Relative.GetBasePoint(id);
    MapRedNote mapRedNote = new MapRedNote()
    {
      UseMillimeters = !this.ConvertMMtoPixels
    };
    mapRedNote.FontAutoScale = false;
    PointFConverter pointFconverter = new PointFConverter();
    if (item.Attributes?["pen.Color"] == null)
    {
      mapRedNote.Pen = new Pen(Color.LightGray, 0.5f);
      mapRedNote.Brush = Brushes.LemonChiffon;
    }
    else
    {
      mapRedNote.Pen = this.LoadXml_Pen(item);
      mapRedNote.Brush = this.LoadXml_Brush(item);
    }
    bool viewOlny = this.LoadXml_OnlyView(item);
    Redliner.SetOnlyViewObject((MapObject) mapRedNote, viewOlny);
    XmlAttribute attribute1 = item.Attributes?["fontName"];
    if (attribute1 != null)
      mapRedNote.FontName = attribute1.Value;
    mapRedNote.FontSize = this.ReadFontSize(item);
    XmlAttribute attribute2 = item.Attributes?["text.Color"];
    if (attribute2 == null)
    {
      Color black = Color.Black;
    }
    else
      Color.FromArgb(XmlConvert.ToInt32(attribute2.Value));
    XmlAttribute attribute3 = item.Attributes?["noteStyle"];
    mapRedNote.NoteStyle = attribute3 != null ? attribute3.Value.ToEnum<Intermech.Map.IRedNoteStyle>() : Intermech.Map.IRedNoteStyle.OldStyle;
    XmlAttribute attribute4 = item.Attributes?["facet"];
    float num1 = attribute4 != null ? XmlConvert.ToSingle(attribute4.Value.Replace(',', '.')) : 5f;
    if (this.ConvertMMtoPixels)
      num1 *= this.PixelsPerMM;
    mapRedNote.Facet = num1;
    XmlAttribute attribute5 = item.Attributes?["noteArrow"];
    mapRedNote.NoteArrow = attribute5 != null ? attribute5.Value.ToEnum<Intermech.Map.IRedArrowStyle>() : Intermech.Map.IRedArrowStyle.None;
    XmlAttribute attribute6 = item.Attributes?["arrowSize"];
    float num2 = attribute6 != null ? XmlConvert.ToSingle(attribute6.Value.Replace(',', '.')) : 4f;
    if (this.ConvertMMtoPixels)
      num2 *= this.PixelsPerMM;
    mapRedNote.ArrowSize = num2;
    XmlAttribute attribute7 = item.Attributes?["place"];
    if (attribute7 != null)
    {
      PointF pointF2 = (PointF) pointFconverter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, attribute7.Value);
      if (this.ConvertMMtoPixels)
      {
        pointF2.X *= this.PixelsPerMM;
        pointF2.Y *= this.PixelsPerMM;
      }
      pointF2.X += pointF1.X;
      pointF2.Y += pointF1.Y;
      mapRedNote.PlaceNote = pointF2;
    }
    XmlAttribute attribute8 = item.Attributes?["location"];
    if (attribute8 != null)
    {
      PointF pointF3 = (PointF) pointFconverter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, attribute8.Value);
      if (this.ConvertMMtoPixels)
      {
        pointF3.X *= this.PixelsPerMM;
        pointF3.Y *= this.PixelsPerMM;
      }
      pointF3.X += pointF1.X;
      pointF3.Y += pointF1.Y;
      mapRedNote.NoteLocation = pointF3;
    }
    if (mapRedNote.FontAutoScale)
    {
      SizeF sizeF = SizeF.Empty;
      XmlAttribute attribute9 = item.Attributes?["size"];
      if (attribute9 != null)
      {
        sizeF = (SizeF) new MapSizeFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, attribute9.Value);
        int num3 = (this.Version < 7 ? 1 : 0) + (this.ConvertMMtoPixels ? 1 : 0);
        for (int index = 0; index < num3; ++index)
          sizeF = new SizeF(sizeF.Width * this.PixelsPerMM, sizeF.Height * this.PixelsPerMM);
      }
      mapRedNote.NoteSize = sizeF;
      XmlAttribute attribute10 = item.Attributes?["text"];
      mapRedNote.Text = attribute10 != null ? attribute10.Value : "";
      mapRedNote.UpdateFontScale();
    }
    else
    {
      XmlAttribute attribute11 = item.Attributes?["text"];
      mapRedNote.Text = attribute11 != null ? attribute11.Value : "";
      SizeF sizeF = SizeF.Empty;
      XmlAttribute attribute12 = item.Attributes?["size"];
      if (attribute12 != null)
      {
        sizeF = (SizeF) new MapSizeFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, attribute12.Value);
        if (this.ConvertMMtoPixels)
          sizeF = new SizeF(sizeF.Width * this.PixelsPerMM, sizeF.Height * this.PixelsPerMM);
      }
      mapRedNote.NoteSize = sizeF;
    }
    mapRedNote.Relative = this.Relative;
    mapRedNote.RelativeId = id;
    mapRedNote.ModificationTime = this.LoadXml_ModificationTime(item);
    mapRedNote.CreateTime = this.LoadXml_CreateTime(item);
    return mapRedNote;
  }

  private float ReadFontSize(XmlNode item)
  {
    float num = this.ConvertMMtoPixels ? 15f : 5f;
    XmlAttribute attribute = item.Attributes?["fontSize"];
    bool flag = ((int) attribute?.Value?.Contains("px") ?? 0) != 0;
    if (string.IsNullOrWhiteSpace(attribute?.Value))
      return num;
    string s = attribute.Value.Replace(',', '.').Replace("px", "");
    float single;
    try
    {
      single = XmlConvert.ToSingle(s);
    }
    catch (Exception ex)
    {
      return num;
    }
    if (this.ConvertMMtoPixels && !flag)
      single *= this.PixelsPerMM;
    if (!this.ConvertMMtoPixels & flag)
      single /= this.PixelsPerMM;
    return single;
  }
}
