// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PolylineData
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Полилиния</summary>
[Serializable]
public class PolylineData : PageElementNode
{
  /// <summary>Имя типа элемента</summary>
  public static string ElementTypeName = LocalizationHolder.rm.GetString("Interfaces.Document_79");
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  private Color foreColor = Color.Black;
  private DashStyle lineStyle;
  private float lineWidth;
  private PointF[] pathPoints = new PointF[0];
  private byte[] pathTypes = new byte[0];

  /// <summary>Получить точку на прямой ближайшую к заданной точке</summary>
  /// <param name="point">Точка</param>
  /// <param name="linePoint1">Точка прямой 1</param>
  /// <param name="linePoint2">Точка прямой 2</param>
  /// <param name="snapSize">Размер области привязки</param>
  /// <returns></returns>
  public static SnapPoint GetSnapLinePoint(
    PointF point,
    PointF linePoint1,
    PointF linePoint2,
    float snapSize)
  {
    PointF pointF1 = new PointF(linePoint1.X - linePoint2.X, linePoint1.Y - linePoint2.Y);
    if ((double) pointF1.X == 0.0 && (double) pointF1.Y == 0.0)
      return new SnapPoint(linePoint1, SnapPointType.Node);
    if ((double) pointF1.X == 0.0)
      return (double) point.X - (double) linePoint1.X <= (double) snapSize ? new SnapPoint(new PointF(linePoint1.X, point.Y), SnapPointType.LineX) : (SnapPoint) null;
    if ((double) pointF1.Y == 0.0)
      return (double) point.Y - (double) linePoint1.Y <= (double) snapSize ? new SnapPoint(new PointF(point.X, linePoint1.Y), SnapPointType.LineY) : (SnapPoint) null;
    float num1 = UnitsConverter.DistanceFromLine(point, linePoint1, linePoint2);
    if ((double) num1 > (double) snapSize)
      return (SnapPoint) null;
    pointF1 = new PointF(-pointF1.Y, pointF1.X);
    float num2 = (float) Math.Sqrt((double) pointF1.X * (double) pointF1.X + (double) pointF1.Y * (double) pointF1.Y);
    if ((double) num2 == 0.0)
      return new SnapPoint(linePoint1, SnapPointType.Node);
    pointF1 = new PointF(pointF1.X / num2, pointF1.Y / num2);
    PointF pointF2 = new PointF(point.X + pointF1.X * num1, point.Y + pointF1.Y * num1);
    float num3 = -num1;
    PointF pointF3 = new PointF(point.X + pointF1.X * num3, point.Y + pointF1.Y * num3);
    return (double) UnitsConverter.LineLength(pointF2, linePoint1) < (double) UnitsConverter.LineLength(pointF3, linePoint1) ? new SnapPoint(pointF2, SnapPointType.LineXY) : new SnapPoint(pointF3, SnapPointType.LineXY);
  }

  /// <summary>Получить список узлов привязки</summary>
  /// <param name="originalPoint">Оригинальная точка</param>
  /// <param name="snapSize">Размер области привязки</param>
  /// <param name="snapPointList">Список полученных точек</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  public override void GetSnapPoints(
    PointF originalPoint,
    float snapSize,
    List<SnapPoint> snapPointList,
    VisualNode excludeNode)
  {
    int length = this.pathPoints.Length;
    SnapPoint snapPoint = (SnapPoint) null;
    float num1 = 0.0f;
    for (int index = 0; index < length && (index <= 0 || index != length - 1 || !(this.pathPoints[index] == this.pathPoints[0])); ++index)
    {
      float num2 = UnitsConverter.LineLength(this.pathPoints[index], originalPoint);
      if ((double) num2 <= (double) snapSize && (snapPoint == null || (double) num2 < (double) num1))
      {
        snapPoint = new SnapPoint(this.pathPoints[index], SnapPointType.Node);
        num1 = num2;
      }
    }
    if (snapPoint != null)
      return;
    for (int index = 0; index < length - 1; ++index)
    {
      SnapPoint snapLinePoint = PolylineData.GetSnapLinePoint(originalPoint, this.pathPoints[index], this.pathPoints[index + 1], snapSize);
      if (snapLinePoint != null)
      {
        float num3 = UnitsConverter.LineLength(snapLinePoint.Point, originalPoint);
        if ((double) num3 <= (double) snapSize && (snapPoint == null || (double) num3 < (double) num1))
        {
          snapPoint = snapLinePoint;
          num1 = num3;
        }
      }
    }
  }

  /// <summary>Наименование типа</summary>
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => PolylineData.ElementTypeName;
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    this.RemoveProperty(properties, "IsReadOnly");
    this.RemoveProperty(properties, "Transparent");
    if (!this.IsTemplate && this.Template != null)
      return;
    this.RemoveProperty(properties, "TemplateGeometryOverrided");
  }

  /// <summary>Массив точек полилинии</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_262")]
  [CustomDescription("Attribute.Interfaces.Document_263")]
  [CustomCategory("Attribute.Interfaces.Document_264")]
  [TypeConverter(typeof (PolylinePointArrayConverter))]
  public PointF[] PathPoints
  {
    [DebuggerStepThrough] get => this.pathPoints;
    set
    {
      if (value == null || this.pathPoints == value)
        return;
      PointF[] pts = value;
      this.SetOverrideFlags(OverrideFlags.Geometry);
      if (pts.Length != this.pathTypes.Length)
      {
        byte[] types = this.ReDimPathTypesArray(pts.Length);
        this.Path = new GraphicsPath(pts, types);
      }
      else
        this.Path = new GraphicsPath(pts, this.pathTypes);
    }
  }

  private byte[] ReDimPathTypesArray(int newLength)
  {
    byte[] numArray = new byte[newLength];
    int num = Math.Min(numArray.Length, this.pathTypes.Length);
    for (int index = 0; index < num; ++index)
      numArray[index] = this.pathTypes[index];
    if (numArray.Length > this.pathTypes.Length)
    {
      byte pathType = this.pathTypes[this.pathTypes.Length - 1];
      int length1 = numArray.Length;
      for (int length2 = this.pathTypes.Length; length2 < length1; ++length2)
        numArray[length2] = pathType;
    }
    return numArray;
  }

  /// <summary>Массив типов элементов полилинии</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_265")]
  [CustomDescription("Attribute.Interfaces.Document_266")]
  [CustomCategory("Attribute.Interfaces.Document_267")]
  [Browsable(false)]
  public byte[] PathTypes
  {
    [DebuggerStepThrough] get => this.pathTypes;
  }

  /// <summary>Данные пути: точки и типы</summary>
  [Browsable(false)]
  public PathData PathData
  {
    [DebuggerStepThrough] get
    {
      return new PathData()
      {
        Points = (PointF[]) this.pathPoints.Clone(),
        Types = (byte[]) this.pathTypes.Clone()
      };
    }
    set
    {
      bool updateUiGeometryFlag = this.SuspendedUpdateUIGeometryFlag;
      if (!updateUiGeometryFlag)
        this.SuspendUpdateUIGeometry();
      if (value != null)
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (PathData), (object) this.PathData, (object) value);
        this.pathPoints = value.Points;
        this.pathTypes = value.Types;
        this.SetOverrideFlags(OverrideFlags.Geometry);
      }
      else
      {
        this.pathPoints = new PointF[0];
        this.pathTypes = new byte[0];
        this.SetOverrideFlags(OverrideFlags.Geometry);
      }
      this.TemplateGeometryOverrided = true;
      this.SetNeedUpdateUIGeometryRecursive(true, false);
      if (!updateUiGeometryFlag)
        this.ResumeUpdateUIGeometry(true, true);
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Объект для отображения полилинии</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_268")]
  [CustomDescription("Attribute.Interfaces.Document_269")]
  [CustomCategory("Attribute.Interfaces.Document_270")]
  [Browsable(false)]
  public GraphicsPath Path
  {
    [DebuggerStepThrough] get
    {
      return this.pathPoints == null || this.pathPoints.Length == 0 ? new GraphicsPath() : new GraphicsPath(this.pathPoints, this.pathTypes);
    }
    set => this.SetPath(value, true, true);
  }

  /// <summary>Установить новое значение свойству Path</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="setOverrideFlag">Установить флаг переопределения (отключение наследования)</param>
  public void SetPath(GraphicsPath value, bool updateUI, bool setOverrideFlag)
  {
    bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag;
    if (!flag)
      this.SuspendUpdateUIGeometry();
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Path", (object) this.Path, (object) value);
    if (value != null)
    {
      this.pathPoints = value.PathPoints;
      this.pathTypes = value.PathTypes;
    }
    else
    {
      this.pathPoints = new PointF[0];
      this.pathTypes = new byte[0];
    }
    if (setOverrideFlag)
      this.SetOverrideFlags(OverrideFlags.Geometry);
    this.needUpdateUIGeometry = true;
    this.InvalidateUI(true);
    if (!flag)
      this.ResumeUpdateUIGeometry(true, true);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Стиль линии</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_271")]
  [CustomDescription("Attribute.Interfaces.Document_272")]
  [CustomCategory("Attribute.Interfaces.Document_273")]
  [Browsable(false)]
  public DashStyle LineStyle
  {
    [DebuggerStepThrough] get => this.lineStyle;
    set
    {
      if (this.lineStyle == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (LineStyle), (object) this.LineStyle, (object) value);
      this.lineStyle = value;
      this.overrideFlags |= OverrideFlags.TopBorder;
      this.RefreshUI();
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Стиль линии</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_271")]
  [CustomDescription("Attribute.Interfaces.Document_272")]
  [CustomCategory("Attribute.Interfaces.Document_273")]
  public LineDashStyle LineStyleVisual
  {
    [DebuggerStepThrough] get => (LineDashStyle) this.lineStyle;
    set => this.LineStyle = (DashStyle) value;
  }

  /// <summary>Толщина линии в миллиметрах. 0 означает толщину по умолчанию</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_274")]
  [CustomDescription("Attribute.Interfaces.Document_275")]
  [CustomCategory("Attribute.Interfaces.Document_276")]
  [TypeConverter(typeof (FloatConverter))]
  public float LineWidth
  {
    [DebuggerStepThrough] get => this.lineWidth;
    set
    {
      if ((double) this.lineWidth == (double) value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (LineWidth), (object) this.LineWidth, (object) value);
      this.lineWidth = value;
      this.SetOverrideFlags(OverrideFlags.Geometry);
      this.SetNeedUpdateUIGeometryRecursive(true, true);
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Цвет линии</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_277")]
  [CustomDescription("Attribute.Interfaces.Document_278")]
  [CustomCategory("Attribute.Interfaces.Document_279")]
  public Color ForeColor
  {
    [DebuggerStepThrough] get => this.foreColor;
    set
    {
      if (!(this.foreColor != value))
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (ForeColor), (object) this.ForeColor, (object) value);
      this.foreColor = value;
      this.TemplateGeometryOverrided = true;
      this.RefreshUI();
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Добавить линию, первая точка новой линии - последняя точка полилинии.
  /// Если в полилинии не было точек, то метод ничего не делает</summary>
  /// <param name="p1">Последняя точка линии</param>
  public void AddLine(PointF p1)
  {
    if (this.PathPoints.Length == 0)
      return;
    GraphicsPath graphicsPath = new GraphicsPath(this.PathPoints, this.PathTypes);
    PointF lastPoint = graphicsPath.GetLastPoint();
    GraphicsPath addingPath = new GraphicsPath();
    addingPath.AddLine(lastPoint, p1);
    graphicsPath.AddPath(addingPath, true);
    this.Path = graphicsPath;
  }

  /// <summary>Добавить линию</summary>
  /// <param name="p0">Первая точка линии</param>
  /// <param name="p1">Вторая точка линии</param>
  public void AddLine(PointF p0, PointF p1)
  {
    GraphicsPath path = this.Path;
    path.AddLine(p0, p1);
    this.Path = path;
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag || this.PathPoints.Length == 0 || context.Layer != 0)
      return;
    base.Draw(context);
    GraphicsPath path = new GraphicsPath(this.PathPoints, this.PathTypes);
    if (!path.GetBounds().IntersectsWith(context.ClipRectangle))
      return;
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    Matrix transform = context.Graphics.Transform;
    float num = this.LineWidth;
    if ((double) num == 0.0)
      num = PageElementNode.DefaultLineWidth;
    if (context.IsPaint)
    {
      context.Graphics.PageUnit = GraphicsUnit.Pixel;
      context.Graphics.Transform = new Matrix();
      PointF[] pts = (PointF[]) this.PathPoints.Clone();
      for (int index = 0; index < pts.Length; ++index)
        pts[index] = (PointF) this.Page.ConvertWorldToPixel(pts[index]);
      path = new GraphicsPath(pts, this.PathTypes);
      num = (float) UnitsConverter.MmToPixels(num, context.Graphics.DpiX, true);
    }
    else
      context.Graphics.PageUnit = GraphicsUnit.Millimeter;
    using (Pen pen = new Pen(this.ForeColor, num))
    {
      pen.DashStyle = this.LineStyle;
      context.Graphics.DrawPath(pen, path);
    }
    context.Graphics.PageUnit = pageUnit;
    context.Graphics.Transform = transform;
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  public override void InvalidateUI(Rectangle clipRectangle)
  {
    this.InvalidateUI(clipRectangle, false);
  }

  /// <summary>Получить границы полилинии</summary>
  public RectangleF GetBounds()
  {
    PointF empty1 = PointF.Empty;
    PointF empty2 = PointF.Empty;
    if (this.pathPoints == null || this.pathPoints.Length == 0)
      return RectangleF.Empty;
    PointF pathPoint = this.pathPoints[0];
    PointF pointF = pathPoint;
    for (int index = 1; index < this.pathPoints.Length; ++index)
    {
      if ((double) pathPoint.X > (double) this.pathPoints[index].X)
        pathPoint.X = this.pathPoints[index].X;
      else if ((double) pointF.X < (double) this.pathPoints[index].X)
        pointF.X = this.pathPoints[index].X;
      if ((double) pathPoint.Y > (double) this.pathPoints[index].Y)
        pathPoint.Y = this.pathPoints[index].Y;
      else if ((double) pointF.Y < (double) this.pathPoints[index].Y)
        pointF.Y = this.pathPoints[index].Y;
    }
    return RectangleF.FromLTRB(pathPoint.X, pathPoint.Y, pointF.X, pointF.Y);
  }

  /// <summary>Определить занимаемый размер для AutoSize родителя</summary>
  /// <param name="currSize">Текущий размер (начальное значение 0)</param>
  /// <param name="childOnly">Не учитывать родительский элемент</param>
  public override SizeF FindMinSize(SizeF currSize, bool childOnly = false)
  {
    RectangleF bounds = this.GetBounds();
    if ((double) currSize.Width < (double) bounds.Right)
      currSize.Width = bounds.Right;
    if ((double) currSize.Height < (double) bounds.Bottom)
      currSize.Height = bounds.Bottom;
    return currSize;
  }

  /// <summary>Найти элемент страницы под данной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="layer">Слой</param>
  /// <param name="firstOnly">Найти первый попавшийся элемент</param>
  public override VisualNode FindPageElementAtPoint(PointF point, ref int layer, bool firstOnly)
  {
    return (VisualNode) null;
  }

  /// <summary>Получить элементы страницы в заданном прямоугольнике</summary>
  /// <param name="rect">Прямоугольник</param>
  /// <param name="elements">Возвращает элементы</param>
  /// <param name="containsOnly">Выбирать только те элементы, которые полностью попадают в прямоугольник</param>
  /// <param name="childOnly">Не учитывать родительский элемент</param>
  public override void FindPageElementsInRectangle(
    RectangleF rect,
    List<VisualNode> elements,
    bool containsOnly,
    bool childOnly = false)
  {
  }

  /// <summary>Применить к элементу свойства шаблона</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isLoading">Вызов в процессе загрузки</param>
  public override void ApplyTemplateProperties(
    DocumentTreeNode template,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (template == null)
      return;
    if (!(template is PolylineData polylineData))
      throw new Exception(string.Format(ExceptionMessages.InvalideTemplateType, (object) this.Template.Id, (object) this.Id));
    bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      if ((this.overrideFlags3 & OverrideFlags3.ForeColor) == OverrideFlags3.None)
        this.foreColor = polylineData.ForeColor;
      if ((this.overrideFlags & OverrideFlags.TopBorder) == OverrideFlags.None)
      {
        this.lineStyle = polylineData.LineStyle;
        this.lineWidth = polylineData.LineWidth;
      }
      if (!this.IsOverridden(OverrideFlags.Geometry))
      {
        this.pathPoints = polylineData.pathPoints == null ? (PointF[]) null : (PointF[]) polylineData.pathPoints.Clone();
        this.pathTypes = polylineData.pathTypes == null ? (byte[]) null : (byte[]) polylineData.pathTypes.Clone();
        this.needUpdateUIGeometry = true;
      }
      base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Можно ли использовать заданный узел как шаблон</summary>
  /// <param name="node">Узел</param>
  /// <returns></returns>
  public override bool CanUseNodeAsTemplate(DocumentTreeNode node)
  {
    return node != null && node is PolylineData;
  }

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new PolylineData(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public static object EmptyConstructor() => (object) new PolylineData();

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected PolylineData(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  public PolylineData(DocumentTreeNode parent)
  {
    if (parent == null)
      return;
    this.SetParent(parent, false, false);
  }

  /// <summary>Конструктор</summary>
  public PolylineData()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public PolylineData(bool initFields)
    : base(initFields)
  {
  }

  static PolylineData() => PolylineData.InitReadFieldDict();

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    int num = this.Template != null ? 1 : 0;
    if (num == 0 || (this.overrideFlags3 & OverrideFlags3.ForeColor) != OverrideFlags3.None)
      xw.WriteAttributeString("foreColor", DocumentTreeNode.ColorConverter.ConvertToInvariantString((object) this.foreColor));
    if (num != 0 && (this.overrideFlags & OverrideFlags.TopBorder) == OverrideFlags.None)
      return;
    xw.WriteAttributeString("lineWidth", this.lineWidth.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteAttributeString("lineStyle", this.lineStyle.ToString());
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlElements(xw, objectRefId);
    if (this.Template != null && !this.IsOverridden(OverrideFlags.Geometry))
      return;
    xw.WriteStartElement("PathPoints");
    for (int index = 0; index < this.pathPoints.Length; ++index)
      xw.WriteElementString("p" + index.ToString((IFormatProvider) CultureInfo.InvariantCulture), new PointFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.pathPoints[index]));
    xw.WriteEndElement();
    xw.WriteStartElement("PathTypes");
    for (int index = 0; index < this.pathTypes.Length; ++index)
      xw.WriteElementString("p" + index.ToString((IFormatProvider) CultureInfo.InvariantCulture), this.pathTypes[index].ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteEndElement();
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (PolylineData.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      PolylineData.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    switch (readArgs.Reader.LocalName)
    {
      case "foreColor":
        PolylineData.ReadForeColor((DocumentTreeNode) this, readArgs);
        return true;
      case "lineWidth":
        PolylineData.ReadLineWidth((DocumentTreeNode) this, readArgs);
        return true;
      case "lineStyle":
        PolylineData.ReadLineStyle((DocumentTreeNode) this, readArgs);
        return true;
      case "PathPoints":
        PolylineData.ReadPathPoints((DocumentTreeNode) this, readArgs);
        return true;
      case "PathTypes":
        PolylineData.ReadPathTypes((DocumentTreeNode) this, readArgs);
        return true;
      default:
        return base.ReadFieldFromXml(readArgs);
    }
  }

  private static void InitReadFieldDict()
  {
    PolylineData.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) PageElementNode.ReadFieldsDict);
    PolylineData.ReadFieldsDict.Add("foreColor", new ReadFieldFromXmlDelegate(PolylineData.ReadForeColor));
    PolylineData.ReadFieldsDict.Add("lineWidth", new ReadFieldFromXmlDelegate(PolylineData.ReadLineWidth));
    PolylineData.ReadFieldsDict.Add("lineStyle", new ReadFieldFromXmlDelegate(PolylineData.ReadLineStyle));
    PolylineData.ReadFieldsDict.Add("PathPoints", new ReadFieldFromXmlDelegate(PolylineData.ReadPathPoints));
    PolylineData.ReadFieldsDict.Add("PathTypes", new ReadFieldFromXmlDelegate(PolylineData.ReadPathTypes));
  }

  private static void ReadPathTypes(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    List<byte> byteList = new List<byte>();
    if (!readArgs.Reader.IsEmptyElement)
    {
      bool flag = false;
      string localName = readArgs.Reader.LocalName;
      while (!flag && readArgs.Reader.Read())
      {
        switch (readArgs.Reader.NodeType)
        {
          case XmlNodeType.Element:
            readArgs.Reader.Read();
            byte num = byte.Parse(readArgs.Reader.Value);
            byteList.Add(num);
            continue;
          case XmlNodeType.EndElement:
            if (localName == readArgs.Reader.LocalName)
            {
              flag = true;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    ((PolylineData) docNode).pathTypes = byteList.ToArray();
    docNode.SetOverrideFlags(OverrideFlags.Geometry);
  }

  private static void ReadPathPoints(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    List<PointF> pointFList = new List<PointF>();
    if (!readArgs.Reader.IsEmptyElement)
    {
      bool flag = false;
      string localName = readArgs.Reader.LocalName;
      while (!flag && readArgs.Reader.Read())
      {
        switch (readArgs.Reader.NodeType)
        {
          case XmlNodeType.Element:
            readArgs.Reader.Read();
            PointF pointF = readArgs.Version >= 16 /*0x10*/ ? (PointF) new PointFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value) : (PointF) new PointFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value.Replace(',', '.'));
            pointFList.Add(pointF);
            continue;
          case XmlNodeType.EndElement:
            if (localName == readArgs.Reader.LocalName)
            {
              flag = true;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    ((PolylineData) docNode).pathPoints = pointFList.ToArray();
    docNode.SetOverrideFlags(OverrideFlags.Geometry);
  }

  private static void ReadLineStyle(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PolylineData) docNode).lineStyle = (DashStyle) Enum.Parse(typeof (DashStyle), readArgs.Reader.Value);
    docNode.overrideFlags |= OverrideFlags.TopBorder;
  }

  private static void ReadLineWidth(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PolylineData) docNode).lineWidth = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
    docNode.overrideFlags |= OverrideFlags.TopBorder;
  }

  private static void ReadForeColor(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 11)
      ((PolylineData) docNode).foreColor = Color.FromName(readArgs.Reader.Value);
    else
      ((PolylineData) docNode).foreColor = (Color) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value);
    docNode.overrideFlags3 |= OverrideFlags3.ForeColor;
  }

  /// <summary>Копировать поля из src</summary>
  /// <param name="src">Источник</param>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyData">Копировать данные</param>
  /// <param name="copyDataNodes">Копировать узлы являющиеся ячейками данных для таблиц</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  protected override void CopyFields(
    DocumentTreeNode src,
    bool copyChildren,
    bool copyData,
    bool copyDataNodes,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    base.CopyFields(src, copyChildren, copyData, copyDataNodes, templateClone, externalLink, links);
    if (!(src is PolylineData polylineData))
      return;
    this.foreColor = polylineData.foreColor;
    this.lineStyle = polylineData.lineStyle;
    this.lineWidth = polylineData.lineWidth;
    this.pathPoints = new PointF[polylineData.pathPoints.Length];
    for (int index = 0; index < this.pathPoints.Length; ++index)
      this.pathPoints[index] = polylineData.pathPoints[index];
    this.pathTypes = new byte[polylineData.pathTypes.Length];
    for (int index = 0; index < this.pathTypes.Length; ++index)
      this.pathTypes[index] = polylineData.pathTypes[index];
  }

  /// <summary>Проверить можно ли добавить заданный элемент в этот элемент</summary>
  /// <param name="child">Вставляемый элемент</param>
  /// <returns>Возвращает true, если заданный элемент можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(DocumentTreeNode child) => false;

  /// <summary>Проверить можно ли добавить элемент заданного типа в этот элемент</summary>
  /// <param name="type">Тип вставляемого элемента</param>
  /// <returns>Возвращает true, если элемент заданного типа можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(Type type) => false;
}
