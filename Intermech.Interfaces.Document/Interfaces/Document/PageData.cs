// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageData
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
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Объект страницы документа. Функции: Управление элементами страницы; Управление
/// атрибутами; Рендеринг.</summary>
[Serializable]
public class PageData : 
  VisualNode,
  IDocumentElement,
  ISerializable,
  IDeserializationCallback,
  IParentFlow,
  IFlowElement
{
  /// <summary>Имя типа для словаря конструкторов</summary>
  internal static string TypeNameForConstructorDictionary = "Page";
  [NonSerialized]
  private Image paintBuffer;
  [NonSerialized]
  public static bool UsePaintBuffer = false;
  [NonSerialized]
  private DocumentChanged_EventHandler documentChanged;
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  private bool isFormulaLib;
  private bool autoSize;
  private bool fromNewPage;
  [NonSerialized]
  private bool isLockedForLoad;
  [NonSerialized]
  private bool isLockedForLayout;
  private bool printBounds;
  private bool manualInserted;
  protected PointF location = new PointF(0.0f, 0.0f);
  private float _offset;
  private PictAlignmentInText _alignInText = PictAlignmentInText.Center;
  private SizeF size = new SizeF(210f, 297f);
  private string nextPageTemplateId;
  private string lastPageTemplateId;
  private string hierarchicalPageNumber;
  /// <summary>Коллекция потоков страницы</summary>
  [ChildLink]
  protected FlowCollection flows = new FlowCollection();
  [ExternalLink]
  private IParentFlow parentFlow;
  [ExternalLink]
  private PageData nextPage;
  [ExternalLink]
  private PageData prevPage;
  [NonSerialized]
  private int pageNumber = int.MinValue;
  [NonSerialized]
  private int complectPageNumber = int.MinValue;
  [NonSerialized]
  protected int suspendUpdateUIGeometryCount;
  [NonSerialized]
  protected int suspendRefreshUICount;
  /// <summary>Запущен процесс разбивки документа</summary>
  [NonSerialized]
  protected bool isDistributing;

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new PageData(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public static object EmptyConstructor() => (object) new PageData();

  /// <summary>Инициализировать поля объекта</summary>
  protected override void InitFields()
  {
    base.InitFields();
    this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
    this.cloneByTemplateWithParent = false;
  }

  /// <summary>Конструктор</summary>
  public PageData(DocumentTreeNode parent) => this.SetParent(parent, false, false);

  /// <summary>Конструктор</summary>
  public PageData()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public PageData(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected PageData(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  static PageData() => PageData.InitReadFieldDict();

  /// <summary>Авторазмер</summary>
  [TypeConverter(typeof (CustomBooleanConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_520")]
  [CustomDescription("Attribute.Interfaces.Document_521")]
  [CustomCategory("Attribute.Interfaces.Document_522")]
  public bool AutoSize
  {
    get => this.autoSize;
    set
    {
      if (this.autoSize == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (AutoSize), (object) this.AutoSize, (object) value);
        this.autoSize = value;
        this.SetPropertiesChangedFlag(true, true, false, true, true);
        this.SetNeedUpdateLayoutFlag(true, true, true, true);
        if (this.OwnerDocument != null)
          this.OwnerDocument.UpdateLayout(true);
        this.OnChanged(new Changed_EventArgs());
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Авторазмер</summary>
  [TypeConverter(typeof (CustomBooleanConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_598")]
  [CustomDescription("Attribute.Interfaces.Document_599")]
  [CustomCategory("Attribute.Interfaces.Document_600")]
  public bool FromNewPage
  {
    get => this.fromNewPage;
    set
    {
      if (this.fromNewPage == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (FromNewPage), (object) this.FromNewPage, (object) value);
        this.fromNewPage = value;
        this.SetPropertiesChangedFlag(true, true, false, true, true);
        this.SetNeedUpdateLayoutFlag(true, true, true, true);
        if (this.OwnerDocument != null)
          this.OwnerDocument.UpdateLayout(true);
        this.OnChanged(new Changed_EventArgs());
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  public static SizeF GetSizeForPageFormat(string pageFormat)
  {
    switch (pageFormat)
    {
      case null:
        throw new ArgumentNullException(nameof (pageFormat));
      case "":
        throw new ArgumentException("Empty string", nameof (pageFormat));
      default:
        if (pageFormat[0] == 'А' || pageFormat[0] == 'а')
          pageFormat = "A" + pageFormat.Substring(1);
        switch (pageFormat)
        {
          case "A4":
            return new SizeF(210f, 297f);
          case "A3":
            return new SizeF(297f, 420f);
          case "A2":
            return new SizeF(420f, 594f);
          case "A1":
            return new SizeF(594f, 841f);
          case "A0":
            return new SizeF(841f, 1189f);
          default:
            return new SizeF(210f, 297f);
        }
    }
  }

  public override string ToString()
  {
    string str;
    if (this.IsFormulaLib)
    {
      str = this.Name != null ? $"<<{this.Id}>> \"{this.Name}\"" : $"<<{this.Id}>>";
    }
    else
    {
      string name = this.Name;
      str = name == null || name == "" ? $"{this.Index + 1}" : $"{this.Index + 1}. {name}";
    }
    return str;
  }

  /// <summary>Наименование типа</summary>
  public override string NodeTypeCaption
  {
    get
    {
      return !this.IsFormulaLib ? LocalizationHolder.rm.GetString("Interfaces.Document_69") : LocalizationHolder.rm.GetString("Interfaces.Document_722");
    }
  }

  /// <summary>Получить подпись элемента по умолчанию</summary>
  public override string GetDefautCaption()
  {
    string defautCaption;
    if (this.IsFormulaLib)
    {
      defautCaption = this.Name != null ? $"<<{this.Id}>> \"{this.Name}\"" : $"<<{this.Id}>>";
    }
    else
    {
      string name = this.Name;
      defautCaption = name == null || name == "" ? string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_70"), (object) (this.Index + 1)) : string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_71"), (object) (this.Index + 1), (object) name);
    }
    return defautCaption;
  }

  /// <summary>Буфер изображения на экране</summary>
  [Browsable(false)]
  public Image PaintBuffer
  {
    [DebuggerStepThrough] get => this.paintBuffer;
    set
    {
      if (this.paintBuffer == value)
        return;
      if (this.paintBuffer != null)
        this.paintBuffer.Dispose();
      this.paintBuffer = value;
    }
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag || this.IsLocked)
      return;
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    if (this.AlignInText == PictAlignmentInText.CustomBaseLine && (double) this.Offset != 0.0 && (this.OwnerDocument == null ? 0 : (this.OwnerDocument.IsFormulaLib ? 1 : 0)) != 0 && context.IsPaint && context.Layer == -1 && context.ShowInvisibleLines)
    {
      context.Graphics.PageUnit = GraphicsUnit.Millimeter;
      using (Pen pen = new Pen(Color.DarkGray, 0.0f))
      {
        pen.DashStyle = DashStyle.Dash;
        context.Graphics.DrawLine(pen, new PointF(0.0f, this.Size.Height - this.Offset), new PointF(this.Size.Width, this.Size.Height - this.Offset));
      }
    }
    if (!context.IsPaint && this.printBounds)
    {
      context.Graphics.PageUnit = GraphicsUnit.Millimeter;
      Pen pen1 = this.OwnerDocument == null ? new Pen(Color.DarkGray, PageElementNode.DefaultLineWidth) : this.OwnerDocument.DefaultPageBorderLine.GetPen();
      if (pen1 != null)
      {
        ImGraphics graphics = context.Graphics;
        Pen pen2 = pen1;
        SizeF size = this.Size;
        double width = (double) size.Width - (double) pen1.Width / 2.0;
        size = this.Size;
        double height = (double) size.Height - (double) pen1.Width / 2.0;
        graphics.DrawRectangle(pen2, 0.0f, 0.0f, (float) width, (float) height);
        pen1.Dispose();
      }
    }
    base.Draw(context);
    context.Graphics.PageUnit = pageUnit;
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (!ImDocumentData.ShowDebugInfo)
    {
      this.RemoveProperty(properties, "Flows");
      this.RemoveProperty(properties, "IsWaitForDistributed");
      this.RemoveProperty(properties, "PrevPage");
      this.RemoveProperty(properties, "NextPage");
    }
    ImDocumentData ownerDocument = this.OwnerDocument;
    if (ownerDocument != null && ownerDocument.IsFormulaLib)
    {
      this.RemoveProperty(properties, "FromNewPage");
      this.RemoveProperty(properties, "Location");
      this.RemoveProperty(properties, "PageNumber");
      this.RemoveProperty(properties, "ComplectPageNumber");
      this.RemoveProperty(properties, "HierarchicalPageNumber");
      this.RemoveProperty(properties, "Landscape");
      this.RemoveProperty(properties, "PrintBounds");
      this.RemoveProperty(properties, "Template");
      this.RemoveProperty(properties, "CloneByTemplateWithParent");
      this.RemoveProperty(properties, "NextPageTemplateId");
      this.RemoveProperty(properties, "LastPageTemplateId");
      if (this._alignInText != PictAlignmentInText.CustomBaseLine)
        properties.SetReadOnlyProperty("Offset", true);
    }
    else
    {
      this.RemoveProperty(properties, "AutoSize");
      this.RemoveProperty(properties, "Offset");
      this.RemoveProperty(properties, "AlignInText");
    }
    if (this.TemplateId == null)
      return;
    if (properties[(object) "Landscape"] is CustomPropertyDescriptor property1)
      property1.SetIsReadOnly(true);
    if (properties[(object) "Size"] is CustomPropertyDescriptor property2)
      property2.SetIsReadOnly(true);
    if (!(properties[(object) "Location"] is CustomPropertyDescriptor property3))
      return;
    property3.SetIsReadOnly(true);
  }

  [CustomDisplayName("Attribute.Interfaces.Document_626")]
  [CustomDescription("Attribute.Interfaces.Document_627")]
  [CustomCategory("Attribute.Interfaces.Document_628")]
  [TypeConverter(typeof (FloatConverter))]
  public float Offset
  {
    get => this._offset;
    set
    {
      if ((double) this._offset == (double) value)
        return;
      this._offset = value;
      this.SetPropertiesChangedFlag(true, true, false, true, false);
      this.RefreshUI();
      this.OnChanged(new Changed_EventArgs());
    }
  }

  [CustomDisplayName("Attribute.Interfaces.Document_629")]
  [CustomDescription("Attribute.Interfaces.Document_630")]
  [CustomCategory("Attribute.Interfaces.Document_628")]
  [RefreshProperties(RefreshProperties.All)]
  public PictAlignmentInText AlignInText
  {
    get => this._alignInText;
    set
    {
      if (this._alignInText == value)
        return;
      this._alignInText = value;
      if (this._alignInText != PictAlignmentInText.CustomBaseLine)
        this._offset = 0.0f;
      this.SetPropertiesChangedFlag(true, true, false, true, false);
      this.RefreshUI();
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Положение страницы</summary>
  [TypeConverter(typeof (PointFConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_163")]
  [CustomDescription("Attribute.Interfaces.Document_164")]
  [CustomCategory("Attribute.Interfaces.Document_165")]
  [Browsable(false)]
  public virtual PointF Location
  {
    [DebuggerStepThrough] get => this.location;
    set
    {
      if (!(this.location != value))
        return;
      int num = this.SuspendedUpdateUIGeometryFlag ? 1 : 0;
      if (num == 0)
        this.SuspendUpdateUIGeometry();
      this.location = value;
      this.SetNeedUpdateUIGeometryRecursive(true, false);
      if (num == 0)
        this.ResumeUpdateUIGeometry(true, true);
      this.SetPropertiesChangedFlag(true, true, false, true, true);
    }
  }

  /// <summary>Назначить новое значение без обновления интерфейса</summary>
  /// <param name="value">Новое значение</param>
  public virtual void AssignLocation(PointF value) => this.location = value;

  /// <summary>Размеры страницы</summary>
  [TypeConverter(typeof (SizeFConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_166")]
  [CustomDescription("Attribute.Interfaces.Document_167")]
  [CustomCategory("Attribute.Interfaces.Document_168")]
  public virtual SizeF Size
  {
    [DebuggerStepThrough] get => this.size;
    set => this.SetSize(value, true, true);
  }

  /// <summary>Назначить новое значение Size</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="updateUI">Обновлять UI</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public virtual void SetSize(SizeF value, bool updateUI, bool updateLayout)
  {
    if (!(this.size != value))
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Size", (object) this.Size, (object) value);
    int num = !updateUI ? 1 : (this.SuspendedUpdateUIGeometryFlag ? 1 : 0);
    if (num == 0)
      this.SuspendUpdateUIGeometry();
    this.size = value;
    if (updateLayout)
      this.UpdateLayout(updateUI);
    this.SetNeedUpdateUIGeometryRecursive(true, false);
    if (num == 0)
      this.ResumeUpdateUIGeometry(true, true);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Альбомная ориентация страницы</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Interfaces.Document_169")]
  [CustomDescription("Attribute.Interfaces.Document_170")]
  [CustomCategory("Attribute.Interfaces.Document_171")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool Landscape
  {
    [DebuggerStepThrough] get
    {
      SizeF size = this.Size;
      double width = (double) size.Width;
      size = this.Size;
      double height = (double) size.Height;
      return width > height;
    }
    set
    {
      if (this.Landscape == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        {
          this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (Landscape), (object) this.Landscape, (object) value);
          this.OwnerDocument.UndoManager.LockUndo();
        }
        this.BeginChanges(false);
        this.Size = new SizeF(this.size.Height, this.size.Width);
        this.EndChanges(false);
        this.SetPropertiesChangedFlag(true, true, false, true, true);
        this.OnChanged(new Changed_EventArgs());
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        {
          this.OwnerDocument.UndoManager.UnlockUndo();
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
        }
      }
    }
  }

  /// <summary>Параметры страницы</summary>
  public void SetPagePrintSettings(ref PageSettings pageSettings)
  {
    if (pageSettings == null)
      pageSettings = new PageSettings();
    bool flag = true;
    if (this.OwnerDocument != null)
    {
      flag = this.OwnerDocument.FitToPage;
      if (this.OwnerDocument.ImPrintSettings.FitToPagePrint.HasValue)
        flag = this.OwnerDocument.ImPrintSettings.FitToPagePrint.Value;
    }
    PaperSize paperSize1 = pageSettings.PaperSize;
    if (flag)
    {
      pageSettings.PaperSize = !this.Landscape ? new PaperSize("Custom", UnitsConverter.MmToHundredthsOfInch(this.size.Width), UnitsConverter.MmToHundredthsOfInch(this.size.Height)) : new PaperSize("Custom", UnitsConverter.MmToHundredthsOfInch(this.size.Height), UnitsConverter.MmToHundredthsOfInch(this.size.Width));
    }
    else
    {
      PaperSize paperSize2 = pageSettings.PaperSize;
      PaperSize paperSize3 = pageSettings.PrinterSettings.DefaultPageSettings.PaperSize;
      PaperSize paperSize4 = !this.Landscape ? new PaperSize("Custom", UnitsConverter.MmToHundredthsOfInch(this.size.Width), UnitsConverter.MmToHundredthsOfInch(this.size.Height)) : new PaperSize("Custom", UnitsConverter.MmToHundredthsOfInch(this.size.Height), UnitsConverter.MmToHundredthsOfInch(this.size.Width));
      paperSize4.RawKind = 119;
      pageSettings.PaperSize = paperSize4.Width > paperSize3.Width || paperSize4.Height > paperSize3.Height ? paperSize4 : paperSize3;
    }
    pageSettings.Landscape = this.Landscape;
    pageSettings.Margins = new Margins(0, 0, 0, 0);
  }

  /// <summary>Рисовать границы страницы при выводе на печать</summary>
  [TypeConverter(typeof (CustomBooleanConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_172")]
  [CustomDescription("Attribute.Interfaces.Document_173")]
  [CustomCategory("Attribute.Interfaces.Document_174")]
  public virtual bool PrintBounds
  {
    [DebuggerStepThrough] get => this.printBounds;
    set
    {
      if (this.printBounds == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (PrintBounds), (object) this.PrintBounds, (object) value);
      this.printBounds = value;
      this.overrideFlags |= OverrideFlags.PrintPageBounds;
      this.RefreshUI();
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Обновить некоректную область</summary>
  public virtual void UpdateInvalidatedRegion()
  {
  }

  /// <summary>Найти элементы страницы находящиеся под данной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="layer">Слой</param>
  /// <param name="firstOnly">Искать только первый попавшийся элемент</param>
  public override VisualNode FindPageElementAtPoint(PointF point, ref int layer, bool firstOnly)
  {
    VisualNode pageElementAtPoint1 = (VisualNode) null;
    for (int index = 0; index < this.nodes.Count && (!firstOnly || pageElementAtPoint1 == null); ++index)
    {
      if (this.nodes[index] is VisualNode node)
      {
        VisualNode pageElementAtPoint2 = node.FindPageElementAtPoint(point, ref layer, firstOnly);
        if (pageElementAtPoint2 != null)
          pageElementAtPoint1 = pageElementAtPoint2;
      }
    }
    return pageElementAtPoint1;
  }

  /// <summary>Текущая система координат</summary>
  [Browsable(false)]
  public virtual PageCoorSystem UserCoorSystem
  {
    [DebuggerStepThrough] get => PageCoorSystem.TopLeft;
  }

  /// <summary>Создать метафайл с изображением страницы</summary>
  /// <param name="fileName">Имя метафайла</param>
  /// <returns>Метафайл</returns>
  public virtual void CreatePageMetafile(string fileName)
  {
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
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.GetSnapPoints(originalPoint, snapSize, snapPointList, excludeNode);
    }
    if (excludeNode == this)
      return;
    float num1 = 0.0f;
    RectangleF rectangleF = new RectangleF(PointF.Empty, this.Size);
    PointF location = rectangleF.Location;
    PointF pointF1 = new PointF(rectangleF.X, rectangleF.Bottom);
    PointF pointF2 = new PointF(rectangleF.Right, rectangleF.Y);
    PointF pointF3 = new PointF(rectangleF.Right, rectangleF.Bottom);
    SnapPoint snapPoint = (SnapPoint) null;
    float num2 = UnitsConverter.LineLength(location, originalPoint);
    if ((double) num2 <= (double) snapSize && (double) num2 < (double) num1)
    {
      snapPoint = new SnapPoint(location, SnapPointType.Node);
      num1 = num2;
    }
    float num3 = UnitsConverter.LineLength(pointF1, originalPoint);
    if ((double) num3 <= (double) snapSize && (snapPoint == null || (double) num3 < (double) num1))
    {
      snapPoint = new SnapPoint(pointF1, SnapPointType.Node);
      num1 = num3;
    }
    float num4 = UnitsConverter.LineLength(pointF2, originalPoint);
    if ((double) num4 <= (double) snapSize && (snapPoint == null || (double) num4 < (double) num1))
    {
      snapPoint = new SnapPoint(pointF2, SnapPointType.Node);
      num1 = num4;
    }
    float num5 = UnitsConverter.LineLength(pointF3, originalPoint);
    if ((double) num5 <= (double) snapSize && (snapPoint == null || (double) num5 < (double) num1))
    {
      snapPoint = new SnapPoint(pointF3, SnapPointType.Node);
      num1 = num5;
    }
    if (snapPoint == null)
    {
      float num6 = Math.Abs(originalPoint.X - rectangleF.X);
      if ((double) num6 < (double) snapSize && (double) num6 < (double) num1)
      {
        snapPoint = new SnapPoint(new PointF(rectangleF.X, originalPoint.Y), SnapPointType.LineX);
        num1 = num6;
      }
      float num7 = Math.Abs(originalPoint.X - rectangleF.Right);
      if ((double) num7 < (double) snapSize && (snapPoint == null || (double) num7 < (double) num1))
      {
        snapPoint = new SnapPoint(new PointF(rectangleF.Right, originalPoint.Y), SnapPointType.LineX);
        num1 = num7;
      }
      float num8 = Math.Abs(originalPoint.Y - rectangleF.Y);
      if ((double) num8 < (double) snapSize && (snapPoint == null || (double) num8 < (double) num1))
      {
        snapPoint = new SnapPoint(new PointF(originalPoint.X, rectangleF.Y), SnapPointType.LineY);
        num1 = num8;
      }
      float num9 = Math.Abs(originalPoint.Y - rectangleF.Bottom);
      if ((double) num9 < (double) snapSize && (snapPoint == null || (double) num9 < (double) num1))
        snapPoint = new SnapPoint(new PointF(originalPoint.X, rectangleF.Bottom), SnapPointType.LineY);
    }
    if (snapPoint == null)
      return;
    snapPointList.Add(snapPoint);
  }

  /// <summary>Узел является шаблоном</summary>
  public override bool IsTemplate
  {
    get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null && ownerDocument.IsTemplate;
    }
  }

  [Browsable(false)]
  public bool ManualInserted
  {
    get => this.manualInserted;
    set => this.manualInserted = value;
  }

  /// <summary>Корень дерева в котором должен находиться шаблон этого узла</summary>
  public override DocumentTreeNode TemplateRoot => this.OwnerDocument?.TemplateRoot;

  /// <summary>Получить шаблон страницы по ID с проверкой пустой строки</summary>
  /// <param name="templateId">Идентификатор шаблона страницы. Если не задан, то метод вернёт null</param>
  /// <returns></returns>
  private PageData CheckIdAndFindPageTemplate(string templateId)
  {
    PageData pageTemplate = (PageData) null;
    if (!string.IsNullOrEmpty(templateId))
      pageTemplate = !this.IsTemplate ? this.FindTemplate(templateId) as PageData : this.FindNode(templateId) as PageData;
    return pageTemplate;
  }

  /// <summary>Найти шаблон этого узла по идентификатору templateId</summary>
  /// <param name="templateId">Идентификатор шаблона</param>
  /// <returns>Шаблон узла</returns>
  public override DocumentTreeNode FindTemplate(string templateId)
  {
    ImDocumentData ownerDocument = this.OwnerDocument;
    return ownerDocument != null && ownerDocument.Template != null ? ownerDocument.Template.FindNode(templateId) : (DocumentTreeNode) null;
  }

  /// <summary>Идентификатор шаблона для следующей страницы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_175")]
  [CustomDescription("Attribute.Interfaces.Document_176")]
  [CustomCategory("Attribute.Interfaces.Document_177")]
  public virtual string NextPageTemplateId
  {
    [DebuggerStepThrough] get
    {
      return !(this.nextPageTemplateId != "") ? (string) null : this.nextPageTemplateId;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (NextPageTemplateId), (object) this.NextPageTemplateId, (object) value);
      if (!(this.nextPageTemplateId != value))
        return;
      this.nextPageTemplateId = value;
      this.CheckPageTemplateIdAndThrowExceptionIfFail(this.nextPageTemplateId);
      this.overrideFlags2 |= OverrideFlags2.NextPageTemplateId;
      if (!this.IsTemplate)
        this.AssignNeedUpdateLayoutFlag(true);
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Идентификатор шаблона для последней (финальной) страницы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_178")]
  [CustomDescription("Attribute.Interfaces.Document_179")]
  [CustomCategory("Attribute.Interfaces.Document_180")]
  public virtual string LastPageTemplateId
  {
    [DebuggerStepThrough] get
    {
      return !(this.lastPageTemplateId != "") ? (string) null : this.lastPageTemplateId;
    }
    set
    {
      if (!(this.lastPageTemplateId != value))
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (LastPageTemplateId), (object) this.LastPageTemplateId, (object) value);
      this.lastPageTemplateId = value;
      this.CheckPageTemplateIdAndThrowExceptionIfFail(this.lastPageTemplateId);
      this.overrideFlags2 |= OverrideFlags2.LastPageTemplateId;
      if (!this.IsTemplate)
        this.AssignNeedUpdateLayoutFlag(true);
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Проверить существование шаблона страницы и выбросить исключение если его нет</summary>
  /// <param name="pageTemplateId">Идентификатор шаблона страницы</param>
  private void CheckPageTemplateIdAndThrowExceptionIfFail(string pageTemplateId)
  {
    if (!string.IsNullOrEmpty(pageTemplateId) && this.CheckIdAndFindPageTemplate(pageTemplateId) == null)
      throw new Exception("Не найден шаблон страницы с заданным идентификатором");
  }

  /// <summary>Шаблон для следующей страницы</summary>
  [Browsable(false)]
  public PageData NextPageTemplate
  {
    [DebuggerStepThrough] get => this.CheckIdAndFindPageTemplate(this.NextPageTemplateId);
  }

  /// <summary>Шаблон для последней страницы</summary>
  [Browsable(false)]
  public PageData LastPageTemplate
  {
    [DebuggerStepThrough] get => this.CheckIdAndFindPageTemplate(this.LastPageTemplateId);
  }

  /// <summary>Найти шаблоны предыдущих страниц, т.е. страницы, для которой эта страница в настройках задана как следующая</summary>
  /// <returns></returns>
  internal List<PageData> FindPrevPageTemplate()
  {
    List<PageData> prevPageTemplate = new List<PageData>();
    if (this.IsTemplate)
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      if (ownerDocument == null)
        return prevPageTemplate;
      foreach (PageData pageData in ownerDocument)
      {
        if (pageData.NextPageTemplateId == this.Id && pageData != this)
          prevPageTemplate.Add(pageData);
      }
    }
    else
    {
      PageData firstPage = this.FindFirstPage();
      if (firstPage != this)
        prevPageTemplate.Add(firstPage);
    }
    return prevPageTemplate;
  }

  /// <summary>Заданный поток пуст</summary>
  /// <param name="flow">Поток данных</param>
  /// <returns>Заданный поток пуст</returns>
  public bool FlowIsEmpty(FlowID flow)
  {
    IFlowElement firstFlowElement = this.GetFirstFlowElement(flow);
    return firstFlowElement == null || firstFlowElement.FlowIsEmpty(flow);
  }

  /// <summary>Все потоки на странице пусты</summary>
  /// <returns></returns>
  public bool AllFlowsIsEmpty()
  {
    for (int index = 0; index < this.flows.Count; ++index)
    {
      if (!this.flows[index].AllFlowsIsEmpty())
        return false;
    }
    return true;
  }

  /// <summary>Удалить страницу, если все она пуста (все ее потоки пусты)</summary>
  public void DeletePageIfEmpty()
  {
    if (!this.IsEmptyRemovablePageInDataFlow)
      return;
    this.RemovePageFromDataFlow(false);
  }

  internal bool IsEmptyRemovablePageInDataFlow
  {
    get
    {
      return (!this.IsAdditionalPage || this.PrevPage == null || this.PrevPage.IsAdditionalPage) && this.PrevPage != null && (this.NextPage == null || this.IsLastAdditionalPageInChain) && this.AllFlowsIsEmpty() && this.flows.Count != 0 && !this.manualInserted;
    }
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
    if (!(template is PageData pageData))
      throw new Exception(string.Format(ExceptionMessages.InvalideTemplateType, (object) this.Template.Id, (object) this.Id));
    bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      this.size = pageData.Size;
      this._offset = pageData._offset;
      this._alignInText = pageData._alignInText;
      this.autoSize = pageData.autoSize;
      this.fromNewPage = pageData.fromNewPage;
      if ((this.overrideFlags & OverrideFlags.PrintPageBounds) == OverrideFlags.None)
        this.printBounds = pageData.PrintBounds;
      if ((this.overrideFlags2 & OverrideFlags2.NextPageTemplateId) == OverrideFlags2.None)
        this.nextPageTemplateId = pageData.NextPageTemplateId;
      if ((this.overrideFlags2 & OverrideFlags2.LastPageTemplateId) == OverrideFlags2.None)
        this.lastPageTemplateId = pageData.LastPageTemplateId;
      base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
      List<DocumentTreeNode> foundNodes = new List<DocumentTreeNode>();
      for (int index1 = 0; index1 < pageData.flows.Count; ++index1)
      {
        if (pageData.flows[index1] is DocumentTreeNode flow1 && flow1.CloneByTemplateWithParent)
        {
          foundNodes.Clear();
          this.FindNodesFromTemplate(flow1, foundNodes);
          for (int index2 = 0; index2 < foundNodes.Count; ++index2)
          {
            if (foundNodes[index2] is IFlowElement flow && foundNodes[index2].ClonedByTemplateWithParent)
            {
              this.AddFlow(flow);
              break;
            }
          }
        }
      }
      ImDocumentData ownerDocument = this.OwnerDocument;
      int index = 0;
      for (int count = this.flows.Count; index < count; ++index)
      {
        if (this.flows[index] is TableData flow && flow.IsPageFlow && flow.FlowID != null && ownerDocument != null && !ownerDocument.DocumentFlows.Contains(flow.FlowID))
        {
          if (flow.FlowID.TemplateFlowID != null)
          {
            flow.SetFlowID(ownerDocument.FindFlowIDFromTemplate(flow.FlowID.TemplateFlowID), false, false);
            FlowID flowId = flow.FlowID;
            if (flowId != null)
            {
              flow.SetFlowID(flowId, false, false);
              continue;
            }
          }
          ownerDocument.AddDocumentFlow(flow.FlowID, true);
        }
      }
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
    return node != null && node is PageData;
  }

  /// <summary>Первая страница</summary>
  [Browsable(false)]
  public bool IsFirstPage
  {
    [DebuggerStepThrough] get => this.PrevPage == null;
  }

  /// <summary>Заключительная страница последовательности. Первая страница не считается последней</summary>
  [Browsable(false)]
  public bool IsFinalPage
  {
    [DebuggerStepThrough] get
    {
      if (this.NextPage != null || this.IsFirstPage)
        return false;
      PageData lastPageTemplate = this.FindLastPageTemplate();
      return lastPageTemplate != null && lastPageTemplate.Id == this.TemplateId;
    }
  }

  /// <summary>Последняя страница</summary>
  [Browsable(false)]
  public bool IsLastPage
  {
    [DebuggerStepThrough] get => this.NextPage == null;
  }

  /// <summary>Найти последнюю страницу в цепочке разбивки NextPage</summary>
  /// <returns>Возвращает последнюю страницу</returns>
  public PageData FindLastPage()
  {
    PageData lastPage = this;
    while (lastPage.NextPage != null)
      lastPage = lastPage.NextPage;
    return lastPage;
  }

  /// <summary>Найти первую страницу в цепочке разбивки PrevPage</summary>
  /// <returns>Первую страницу</returns>
  public PageData FindFirstPage()
  {
    PageData firstPage = this;
    while (firstPage.PrevPage != null)
      firstPage = firstPage.PrevPage;
    return firstPage;
  }

  /// <summary>Установить флаг NeedUpdateLayoutFlag</summary>
  /// <param name="value">Значение флага</param>
  /// <param name="setInPrevCell">Установить флаг и для предыдущих ячеек</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void SetNeedUpdateLayoutFlag(
    bool value,
    bool setInPrevCell,
    bool updateUI,
    bool updateLayout)
  {
    if (!value && !this.needUpdateLayoutFlag)
      return;
    this.AssignNeedUpdateLayoutFlag(value);
    if (this.parent != null & value)
      this.parent.SetNeedUpdateLayoutFlag(true, setInPrevCell, false, false);
    if (!updateLayout || !this.needUpdateLayoutFlag || this.SuspendedUpdateLayoutFlag)
      return;
    this.UpdateLayout(updateUI);
  }

  /// <summary>Обновить представление данных</summary>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void UpdateLayout(bool updateUI)
  {
    ImDocumentData ownerDocument = this.OwnerDocument;
    if (ownerDocument != null)
    {
      if (ownerDocument.IsFormulaLib)
        this.Distribute(new DistributeContext((DocumentTreeNode) this, false), updateUI);
      else
        ownerDocument.UpdateLayout(this.Index, false, updateUI);
    }
    else
      this.Distribute(new DistributeContext((DocumentTreeNode) this, false), updateUI);
  }

  /// <summary>Вызывает разбивку по страницам</summary>
  /// <param name="context">Контекст разбивки</param>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void Distribute(DistributeContext context, bool updateUI)
  {
    if (this.SuspendedUpdateLayoutFlag)
      return;
    try
    {
      this.isDistributing = true;
      lock (this.nodes)
      {
        int num = 0;
        this.AssignNeedUpdateLayoutFlag(true);
        context.VertDistributed = DistributeResult.All;
        for (; this.NeedUpdateLayoutFlag && num < 2; ++num)
        {
          bool force = context.Force && num == 0;
          context.VertDistributed = DistributeResult.All;
          this.InternalDoDistribute(context, true, force);
          this.InternalDoDistribute(context, false);
          TableData.AlignChildElements((VisualNode) this);
          if (context.VertDistributed != DistributeResult.BackToPrevious)
          {
            this.ResetNeedUpdateLayoutFlag(false);
            ImDocumentData ownerDocument = this.OwnerDocument;
            if (ownerDocument != null)
            {
              this.IsLockedForLayout = false;
              ownerDocument.OnDistributePageFinished(new DistributePageFinishedArgs(this));
            }
          }
          else
            break;
        }
        if (!updateUI || context.VertDistributed == DistributeResult.BackToPrevious)
          return;
        this.UpdateUIGeometry(true);
      }
    }
    finally
    {
      this.isDistributing = false;
    }
  }

  private void InternalDoDistribute(DistributeContext context, bool isFirstPass, bool force = false)
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      DistributeContext context1 = (DistributeContext) null;
      if (isFirstPass && (force || this.nodes[index].NeedUpdateLayoutFlag))
      {
        context1 = new DistributeContext(this.nodes[index], context.Force);
        context1.FirstPass = true;
        context1.MoveTailToFinalPage = context.MoveTailToFinalPage;
        this.nodes[index].Distribute(context1, false);
      }
      else if (!isFirstPass && this.nodes[index] is TableData node && node.NeedSecondLayoutPass)
      {
        context1 = new DistributeContext((DocumentTreeNode) node, context.Force);
        context1.FirstPass = false;
        context1.MoveTailToFinalPage = context.MoveTailToFinalPage;
        node.Distribute(context1, false);
      }
      if (context1 != null && context1.VertDistributed == DistributeResult.BackToPrevious)
        context.VertDistributed = DistributeResult.BackToPrevious;
    }
  }

  /// <summary>Запущен процесс разбивки документа</summary>
  [Browsable(false)]
  public bool IsDistributing => this.isDistributing;

  /// <summary>Страница заблокирована потоком разбивки или загрузки</summary>
  [Browsable(false)]
  public bool IsLocked
  {
    [DebuggerStepThrough] get => this.isLockedForLayout || this.isLockedForLoad;
  }

  /// <summary>Страница заблокирована потоком разбивки</summary>
  [Browsable(false)]
  public bool IsLockedForLayout
  {
    [DebuggerStepThrough] get => this.isLockedForLayout;
    set => this.isLockedForLayout = value;
  }

  /// <summary>Ждать пока страница не разблокируется от разбивки</summary>
  /// <param name="millisecondsTimeout">Максимальное время ожидания</param>
  /// <param name="oneSleep">Время между проверкой состояния</param>
  public void WaitForLayout(int millisecondsTimeout, int oneSleep = 25)
  {
    for (int index = 0; this.IsLockedForLayout && this.OwnerDocument.DistributeThreadIsActive && index * oneSleep < millisecondsTimeout; ++index)
      Thread.Sleep(oneSleep);
  }

  /// <summary>Страница заблокирована потоком разбивки или загрузки</summary>
  [Browsable(false)]
  public bool IsLockedForLoad
  {
    [DebuggerStepThrough] get => this.isLockedForLoad;
    set => this.isLockedForLoad = value;
  }

  /// <summary>Ждать пока страница не разблокируется от чтения</summary>
  /// <param name="millisecondsTimeout">Максимальное время ожидания</param>
  /// <param name="oneSleep">Время между проверкой состояния</param>
  public void WaitForLoad(int millisecondsTimeout, int oneSleep = 25)
  {
    for (int index = 0; this.IsLockedForLoad && this.OwnerDocument.LoadThreadIsActive && index * oneSleep < millisecondsTimeout; ++index)
      Thread.Sleep(oneSleep);
  }

  /// <summary>Страница ждет разбивки</summary>
  [Category("Debug")]
  public bool IsWaitForDistributed
  {
    get
    {
      if (this.isLockedForLayout)
        return true;
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null && ownerDocument.pageThreadStatus.StartDistributingPage > -1 && ownerDocument.pageThreadStatus.StartDistributingPage < this.Index;
    }
  }

  /// <summary>Элемент принадлежит формуле</summary>
  public override bool IsFormulaLib
  {
    get => this.OwnerDocument != null ? this.OwnerDocument.IsFormulaLib : this.isFormulaLib;
  }

  /// <summary>Только для внутреннего пользования. Назначить новое значение IsFormulaLib</summary>
  /// <param name="value">Новое значение IsFormulaLib</param>
  public void AssignIsFormulaLib(bool value) => this.isFormulaLib = value;

  [RefreshProperties(RefreshProperties.All)]
  public override string Id
  {
    get => base.Id;
    set
    {
      if (!(this.Id != value))
        return;
      string id = this.Id;
      base.Id = value;
      this.UpdateIDPageLinksInDocuments(id);
    }
  }

  /// <summary>Обновить ссылки на страницу через её идентификатор</summary>
  /// <param name="oldPageId">Старое значение идентификатора</param>
  private void UpdateIDPageLinksInDocuments(string oldPageId)
  {
    if (!this.IsTemplate || this.OwnerDocument == null)
      return;
    this.OwnerDocument.UpdateIDPageLinks(oldPageId, this.Id);
    if (this.OwnerDocument.TemplateOwner == null)
      return;
    this.OwnerDocument.TemplateOwner.UpdateIDPageLinks(oldPageId, this.Id);
  }

  /// <summary>Содержит ли объект виртуальный атрибут с указанным именем</summary>
  /// <param name="attributeName">Имя виртуального атрибута</param>
  /// <returns>Возвращает true, если объект содержит виртуальный атрибут
  /// с указанным именем</returns>
  internal override bool ContainsVirtualAttribute(string attributeName)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    return attributeName == DocumentTreeNode.AttributeName_DocPageNumber || attributeName == DocumentTreeNode.AttributeName_ComplectPageNumber || attributeName == DocumentTreeNode.AttributeName_PageNumberMore1 || base.ContainsVirtualAttribute(attributeName);
  }

  /// <summary>Получить значение виртуального атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="notNull">Вернуть пустую строку вместо значения null</param>
  /// <param name="callChain">Цепочка вызовов для защиты от циклических связей. Если null, то работает без проверок</param>
  /// <returns>Результат выполнения</returns>
  protected override GetVirtualAttributeResult GetVirtualAttributeValue(
    string attributeName,
    bool notNull,
    List<DocumentTreeNode> callChain = null)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    if (attributeName == DocumentTreeNode.AttributeName_DocPageNumber)
      return new GetVirtualAttributeResult(true, this.hierarchicalPageNumber ?? this.pageNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (attributeName == DocumentTreeNode.AttributeName_ComplectPageNumber)
      return this.OwnerDocument != null && this.OwnerDocument.IsPartOfComplectPageNumbering ? new GetVirtualAttributeResult(true, this.complectPageNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)) : new GetVirtualAttributeResult(true, "");
    if (!(attributeName == DocumentTreeNode.AttributeName_PageNumberMore1))
      return base.GetVirtualAttributeValue(attributeName, notNull, callChain);
    ImDocumentData ownerDocument = this.OwnerDocument;
    return ownerDocument != null && ownerDocument.Nodes.Count > 1 ? new GetVirtualAttributeResult(true, this.hierarchicalPageNumber ?? this.pageNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)) : new GetVirtualAttributeResult(true, "");
  }

  /// <summary>Установить значение виртуального атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <param name="callChain">Цепочка вызовов, для защиты от зацикливания</param>
  /// <returns>Результат выполнения</returns>
  protected override SetVirtualAttributeResult SetVirtualAttributeValue(
    string attributeName,
    string attributeValue,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    if (attributeName == DocumentTreeNode.AttributeName_DocPageNumber || attributeName == DocumentTreeNode.AttributeName_ComplectPageNumber)
      return new SetVirtualAttributeResult(true, true);
    if (!(attributeName == DocumentTreeNode.AttributeName_PageNumberMore1))
      return base.SetVirtualAttributeValue(attributeName, attributeValue, updateUI, updateLayout, callChain);
    int result;
    if (this.OwnerDocument != null && this.Index == 0 && int.TryParse(attributeValue, out result))
      this.OwnerDocument.SetStartPageNumber(result, true, updateUI);
    return new SetVirtualAttributeResult(true, true);
  }

  /// <summary>Получить список всех имен атрибутов</summary>
  /// <param name="forSaveOnly">Добавлять в список только те атрибуты, которые должны сохраниться в XML или копироваться при копировании через буфер</param>
  /// <returns>Список всех имен атрибутов</returns>
  protected override void GetVirtualAttributeNames(
    System.Collections.Specialized.StringCollection attributeNames,
    bool forSaveOnly = false)
  {
    if (attributeNames == null)
      throw new ArgumentNullException(nameof (attributeNames));
    attributeNames.Add(DocumentTreeNode.AttributeName_DocPageNumber);
    attributeNames.Add(DocumentTreeNode.AttributeName_ComplectPageNumber);
    attributeNames.Add(DocumentTreeNode.AttributeName_PageNumberMore1);
    base.GetVirtualAttributeNames(attributeNames, forSaveOnly);
  }

  /// <summary>Вставить следующий элемент потока в цепочку</summary>
  /// <param name="newNextFlow">Новый следующий элемент потока</param>
  public override void InsertNextFlowChaineElement(IParentFlow newNextFlow)
  {
    PageData nextPage = this.nextPage;
    PageData pageData = newNextFlow as PageData;
    this.nextPage = pageData;
    pageData.prevPage = this;
    pageData.nextPage = nextPage;
    if (nextPage != null)
      nextPage.prevPage = pageData;
    for (int index = 0; index < this.nodes.Count; ++index)
      this.nodes[index].InsertNextFlowChaineElement(newNextFlow);
    if (!this.IsDistributing)
      this.SetNeedUpdateLayoutFlag(true, false, false, false, true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Коллекция потоков. Только для внутреннего использования!</summary>
  [Category("Debug")]
  public FlowCollection Flows
  {
    [DebuggerStepThrough] get => this.flows;
  }

  /// <summary>Добавить поток</summary>
  /// <param name="flow">Поток</param>
  protected virtual void AddFlow(IFlowElement flow)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    if (this.flows.Contains(flow))
      return;
    this.flows.Add(flow);
  }

  /// <summary>Удалить поток с этой страницы</summary>
  /// <param name="flow">Поток</param>
  protected virtual void RemoveFlow(IFlowElement flow) => this.flows.Remove(flow);

  /// <summary>Назначить родительский поток</summary>
  /// <param name="value">Родительский поток</param>
  public virtual void AssignParentFlow(IParentFlow value)
  {
    if (this.parentFlow == value)
      return;
    if (this.parentFlow != null)
    {
      IParentFlow parentFlow = this.parentFlow;
      this.parentFlow = (IParentFlow) null;
      parentFlow.RemoveChildFlowElement((IFlowElement) this);
    }
    this.parentFlow = value;
  }

  /// <summary>Родительский поток</summary>
  [Browsable(false)]
  public virtual IParentFlow ParentFlow
  {
    [DebuggerStepThrough] get => this.parentFlow;
    set
    {
      if (this.parentFlow == value)
        return;
      if (value != null)
        this.parentFlow.RemoveChildFlowElement((IFlowElement) this);
      this.parentFlow = value;
      if (this.parentFlow == null)
        return;
      this.parentFlow.AddChildFlowElement((IFlowElement) this);
    }
  }

  /// <summary>Следующая страница цепочки</summary>
  [Category("Debug")]
  public PageData NextPage
  {
    [DebuggerStepThrough] get => this.nextPage;
    set
    {
      if (this.nextPage == value)
        return;
      if (this.nextPage != null)
      {
        PageData nextPage = this.nextPage;
        this.nextPage = (PageData) null;
        nextPage.PrevPage = (PageData) null;
      }
      this.nextPage = value;
      if (this.nextPage != null)
        this.nextPage.PrevPage = this;
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Предыдущая страница цепочки</summary>
  [Category("Debug")]
  public PageData PrevPage
  {
    [DebuggerStepThrough] get => this.prevPage;
    set
    {
      if (this.prevPage == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (PrevPage), (object) this.PrevPage, (object) value);
      if (this.prevPage != null)
      {
        PageData prevPage = this.prevPage;
        this.prevPage = (PageData) null;
        prevPage.NextPage = (PageData) null;
      }
      this.prevPage = value;
      if (this.prevPage != null)
      {
        this.prevPage.NextPage = this;
        this.parentFlow = this.prevPage.ParentFlow;
      }
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Следующий элемент потока</summary>
  [Browsable(false)]
  public virtual IFlowElement NextFlowElement
  {
    [DebuggerStepThrough] get => (IFlowElement) this.NextPage;
    set => this.NextPage = (PageData) value;
  }

  /// <summary>Предыдущий элемент потока</summary>
  [Browsable(false)]
  public virtual IFlowElement PrevFlowElement
  {
    [DebuggerStepThrough] get => (IFlowElement) this.PrevPage;
    set => this.PrevPage = (PageData) value;
  }

  /// <summary>Добавить дочерний элемент потока</summary>
  /// <param name="child">Дочерний элемент потока</param>
  public void AddChildFlowElement(IFlowElement child)
  {
    if (child == null)
      throw new ArgumentNullException(nameof (child));
    if (this.flows.IndexOf(child) == -1)
    {
      if (child.ParentFlow != null)
        child.ParentFlow.RemoveChildFlowElement(child);
      if (child.PrevFlowElement == null)
        this.AddFlow(child);
    }
    child.AssignParentFlow((IParentFlow) this);
    this.SetPropertiesChangedFlag(true, true, false, true, true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Удалить дочерний элемент потока</summary>
  /// <param name="child">Дочерний элемент потока</param>
  public void RemoveChildFlowElement(IFlowElement child)
  {
    if (child == null)
      return;
    this.RemoveFlow(child);
    if (child is TableData tableData && tableData.FlowID != null)
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      if (ownerDocument != null && ownerDocument.DocumentFlows.Contains(tableData.FlowID))
      {
        IFlowElement flowElementByName = (IFlowElement) null;
        if (ownerDocument.FindFirstFlowElement(tableData.FlowID, ref flowElementByName) == null)
          ownerDocument.DocumentFlows.Remove(tableData.FlowID);
      }
    }
    child.AssignParentFlow((IParentFlow) null);
    this.SetPropertiesChangedFlag(true, true, false, true, true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Получить следующий элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Следующий элемент цепочки для заданного потока данных</returns>
  public IFlowElement GetNextFlowElement(FlowID flow, ref IFlowElement flowElementByName)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement nextFlowElement1 = (IFlowElement) null;
    for (IFlowElement nextFlowElement2 = this.NextFlowElement; nextFlowElement2 != null && nextFlowElement1 == null; nextFlowElement2 = nextFlowElement2.NextFlowElement)
      nextFlowElement1 = nextFlowElement2.GetFirstFlowElement(flow, ref flowElementByName);
    if (nextFlowElement1 == null && this.ParentFlow != null)
      nextFlowElement1 = this.ParentFlow.GetNextFlowElement(flow, ref flowElementByName);
    return nextFlowElement1;
  }

  /// <summary>Получить первый элемент цепочки для заданного потока данных.
  /// Ищет внутри и по цепочкам дочерних узлов</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Первый элемент цепочки для заданного потока данных</returns>
  public IFlowElement GetFirstFlowElement(FlowID flow)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement flowElementByName = (IFlowElement) null;
    return this.GetFirstFlowElement(flow, ref flowElementByName) ?? flowElementByName;
  }

  /// <summary>
  /// Вернуть перечисление таблиц, которые являются стартовыми для потоков данных на этой странице
  /// </summary>
  /// <returns></returns>
  public IEnumerable<TableData> GetStartFlowTables()
  {
    return this.Nodes.OfType<TableData>().Where<TableData>((Func<TableData, bool>) (t => t.IsStartFlowTable));
  }

  /// <summary>Ищет первый элемент конечного потока по всем промежуточным потокам.
  /// Т.е. если в первом элементе подпотока нет конечного потока, то он ищет в следующим.
  /// Поиск не переходит на следующие элементы этого уровня.</summary>
  /// <param name="flow">Идентификатор конечного потока</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Возвращает указатель на первый элемент потока, или null если поток не найден.</returns>
  public IFlowElement GetFirstFlowElement(FlowID flow, ref IFlowElement flowElementByName)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement firstFlowElement = (IFlowElement) null;
    for (int index = 0; index < this.flows.Count && firstFlowElement == null; ++index)
    {
      for (IFlowElement flowElement = this.flows[index]; firstFlowElement == null && flowElement != null; flowElement = flowElement.NextFlowElement)
        firstFlowElement = flowElement.GetFirstFlowElement(flow, ref flowElementByName);
    }
    return firstFlowElement;
  }

  /// <summary>Найти последний элемент цепочки</summary>
  /// <param name="flowElement">Любой элемент цепочки</param>
  /// <returns>Последний элемент цепочки</returns>
  protected IFlowElement FindLastChainElement(IFlowElement flowElement)
  {
    if (flowElement == null)
      throw new ArgumentNullException(nameof (flowElement));
    while (flowElement.NextFlowElement != null)
      flowElement = flowElement.NextFlowElement;
    return flowElement;
  }

  /// <summary>Получить последний элемент цепочки представления потока</summary>
  /// <param name="flow">Поток</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Последний элемент цепочки представления потока</returns>
  public IFlowElement GetLastFlowElement(FlowID flow, ref IFlowElement flowElementByName)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement lastFlowElement = (IFlowElement) null;
    for (int index = 0; index < this.flows.Count && lastFlowElement == null; ++index)
    {
      for (IFlowElement flowElement = this.FindLastChainElement(this.flows[index]); lastFlowElement == null && flowElement != null; flowElement = flowElement.PrevFlowElement)
        lastFlowElement = flowElement.GetLastFlowElement(flow, ref flowElementByName);
    }
    return lastFlowElement;
  }

  /// <summary>Получить предыдущий элемент цепочки для заданного потока данных</summary>
  /// <param name="flow">Идентификатор потока данных</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns>Предыдущий элемент цепочки для заданного потока данных</returns>
  public IFlowElement GetPrevFlowElement(FlowID flow, ref IFlowElement flowElementByName)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    IFlowElement prevFlowElement1 = (IFlowElement) null;
    for (IFlowElement prevFlowElement2 = this.PrevFlowElement; prevFlowElement2 != null && prevFlowElement1 == null; prevFlowElement2 = prevFlowElement2.PrevFlowElement)
      prevFlowElement1 = prevFlowElement2.GetLastFlowElement(flow, ref flowElementByName);
    if (prevFlowElement1 == null && this.ParentFlow != null)
      prevFlowElement1 = this.ParentFlow.GetPrevFlowElement(flow, ref flowElementByName);
    return prevFlowElement1;
  }

  /// <summary>Распределить поток</summary>
  /// <param name="flow">Поток</param>
  public virtual void DistributeFlowData(FlowID flow)
  {
  }

  /// <summary>Создать страницу по шаблону, не вставляя ее в документ</summary>
  /// <param name="pageTemplateId">Идентификатор шаблона страницы</param>
  /// <param name="isNextPage">Создаётся следующая страница для продолжения разбивки</param>
  /// <returns>Страницу по шаблону</returns>
  public PageData ClonePageFromTemplate(string pageTemplateId, bool isNextPage)
  {
    if (string.IsNullOrEmpty(pageTemplateId))
      throw new ArgumentNullException(nameof (pageTemplateId));
    return this.FindTemplate(pageTemplateId) is PageData template ? (PageData) template.CloneFromTemplate(true, !isNextPage) : throw new Exception($"Шаблон страницы \"{pageTemplateId}\" не найден.");
  }

  /// <summary>Найти таблицу на странице заданного потока</summary>
  /// <param name="flow">Поток данных, распределяемый по страницам</param>
  /// <returns></returns>
  public TableData FindTableForFlow(IFlowElement flow)
  {
    if (flow == null)
      throw new ArgumentNullException(nameof (flow));
    return flow is TableData flowTable ? this.FindTableForFlow(flowTable) : (TableData) null;
  }

  /// <summary>Найти таблицу на странице для того же потока что в заданной таблице</summary>
  /// <param name="flowTable">Таблица содержащая поток данных, распределяемая по страницам</param>
  /// <returns></returns>
  public TableData FindTableForFlow(TableData flowTable)
  {
    if (flowTable == null)
      throw new ArgumentNullException(nameof (flowTable));
    return flowTable.FlowID == null ? (TableData) null : this.GetFirstFlowElement(flowTable.FlowID) as TableData;
  }

  /// <summary>Найти первую таблицу с разбивкой данных по страницам</summary>
  /// <returns></returns>
  public TableData FindFirstMainTable() => this.GetStartFlowTables().FirstOrDefault<TableData>();

  /// <summary>Получить все подтаблицы с данными для текущей страницы</summary>
  /// <returns>список таблиц-ячеек данных</returns>
  protected List<TableData> GetAllDataTables(TableData upLevelTable = null, bool textNodesOnly = true)
  {
    List<TableData> allDataTables = new List<TableData>();
    upLevelTable = upLevelTable ?? this.FindFirstMainTable();
    IEnumerable<TableData> tableDatas = upLevelTable != null ? upLevelTable.Nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (t => t is TableData)).Cast<TableData>() : (IEnumerable<TableData>) null;
    if (tableDatas != null)
    {
      foreach (TableData upLevelTable1 in tableDatas)
      {
        if (!textNodesOnly || upLevelTable1.Nodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (n => n.NodeClass.Equals("TextBoxElement", StringComparison.Ordinal))))
        {
          allDataTables.Add(upLevelTable1);
          if (textNodesOnly)
            continue;
        }
        allDataTables.AddRange((IEnumerable<TableData>) this.GetAllDataTables(upLevelTable1, textNodesOnly));
      }
    }
    return allDataTables;
  }

  private PageData FindLastPageTemplate()
  {
    PageData pageTemplate = this.CheckIdAndFindPageTemplate(this.FindFirstPage().LastPageTemplateId);
    if (pageTemplate == null)
    {
      for (PageData prevPage = this.PrevPage; prevPage != null && pageTemplate == null; prevPage = prevPage.PrevPage)
        pageTemplate = this.CheckIdAndFindPageTemplate(prevPage.LastPageTemplateId);
    }
    return pageTemplate;
  }

  /// <summary>Создать новую страницу для разбивки данных и добавить ее в документ</summary>
  /// <param name="lockUpdateLayout">Блокировать обновление представлений данных</param>
  /// <returns>Следующая страница</returns>
  public PageData AddNewDataPage(bool lockUpdateLayout, string hierarchicalPageNumber = null)
  {
    if (this.parent == null)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_76"));
    if (!this.IsTemplate && this.Template == null)
      throw new ImDocumentException("Невозможен перенос данных на следующую страницу, так как у документа остутсвует внутренний шаблон!");
    PageData pageData1 = (PageData) null;
    PageData pageData2 = this;
    PageData firstPage = this.FindFirstPage();
    if (this.IsLastPage)
    {
      if (this.IsFinalPage)
      {
        pageData2 = this.PrevPage;
        pageData1 = this.CheckIdAndFindPageTemplate(pageData2.NextPageTemplateId);
      }
      else
        pageData1 = this.FindLastPageTemplate();
    }
    if (pageData1 == null)
      pageData1 = this.CheckIdAndFindPageTemplate(this.NextPageTemplateId);
    if (pageData1 == null)
      pageData1 = this.CheckIdAndFindPageTemplate(firstPage.NextPageTemplateId);
    if (pageData1 == null)
      pageData1 = this.CheckIdAndFindPageTemplate(this.TemplateId);
    if (pageData1 == null)
      return (PageData) null;
    PageData pageData3 = this.ClonePageFromTemplate(pageData1.Id, true);
    if (pageData3 != null)
    {
      if (lockUpdateLayout)
        pageData3.SuspendUpdateLayout();
      if (!string.IsNullOrWhiteSpace(hierarchicalPageNumber))
        pageData3.HierarchicalPageNumber = hierarchicalPageNumber;
      pageData2.InsertNextFlowChaineElement((IParentFlow) pageData3);
      this.parent.InsertChildNode(pageData2.Index + 1, (DocumentTreeNode) pageData3, false, true, !lockUpdateLayout, !lockUpdateLayout);
      pageData3.UpdateTemplateLinks(false, true, false, false);
      pageData3.UpdateNodeLinks(true, true, false, false);
      if (lockUpdateLayout)
        pageData3.ResumeUpdateLayout(false, false);
    }
    return pageData3;
  }

  public PageData AddNewAdditionalPage(string hierarchicalPageNumber, bool updateLayout = false)
  {
    PageData pageData = this.AddNewDataPage(true, hierarchicalPageNumber);
    if (pageData == null)
      return (PageData) null;
    foreach (object node in this.Nodes)
    {
      if (node is TableData tableData)
        tableData.RecursiveConnectNextPageByEmptyTables();
    }
    pageData.SetNeedUpdateLayoutFlag(true, true, updateLayout, updateLayout, true);
    this.SetNeedUpdateLayoutFlag(true, true, updateLayout, updateLayout);
    return pageData;
  }

  /// <summary>Присвоить значение свойству Parent</summary>
  /// <param name="value">Новое значение Parent</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void AssignParent(
    DocumentTreeNode value,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (this.parent == value)
      return;
    ImDocumentData parent = this.parent as ImDocumentData;
    base.AssignParent(value, updateUI, updateLayout, isLoading);
    this.RestoreFlowIds();
    if (this.documentChanged == null)
      return;
    this.documentChanged((object) this, new DocumentChanged_EventArgs(parent, this.parent as ImDocumentData));
  }

  /// <summary>Событие Добавлен дочерний узел</summary>
  public event DocumentChanged_EventHandler DocumentChanged
  {
    add => this.documentChanged += value;
    remove => this.documentChanged -= value;
  }

  /// <summary>Корень дерева документа в котором находится этот узел.
  /// <remarks>Документ который владеет этим узлом. Если узел не пренадлежит документу, то null</remarks>
  /// </summary>
  public override ImDocumentData GetDocTreeRoot() => this.OwnerDocument;

  /// <summary>Документ владелец страницы</summary>
  [Browsable(false)]
  public override ImDocumentData OwnerDocument
  {
    [DebuggerStepThrough] get
    {
      DocumentTreeNode parent = this.parent;
      ImDocumentData ownerDocument;
      for (ownerDocument = this.parent as ImDocumentData; ownerDocument == null && parent != null; ownerDocument = parent as ImDocumentData)
        parent = parent.Parent;
      return ownerDocument;
    }
  }

  /// <summary>Документ, который использует данный документ как шаблон (=OwnerDocument.TemplateOwner)</summary>
  [Browsable(false)]
  public virtual ImDocumentData DocumentTemplateOwner
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null && ownerDocument.IsTemplate ? ownerDocument.TemplateOwner : (ImDocumentData) null;
    }
  }

  /// <summary>Проверить можно ли добавить заданный элемент в этот элемент</summary>
  /// <param name="child">Вставляемый элемент</param>
  /// <returns>Возвращает true, если заданный элемент можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(DocumentTreeNode child)
  {
    return this.CanAddChildElement(child.GetType());
  }

  /// <summary>Проверить можно ли добавить элемент заданного типа в этот элемент</summary>
  /// <param name="type">Тип вставляемого элемента</param>
  /// <returns>Возвращает true, если элемент заданного типа можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(Type type)
  {
    return typeof (PageElementNode).IsAssignableFrom(type);
  }

  /// <summary>Герерирует событие Changed</summary>
  public override void OnChanged(Changed_EventArgs e)
  {
    if (this.IsChanging || this.IsVirtualNode)
      return;
    base.OnChanged(e);
    ImDocumentData ownerDocument = this.OwnerDocument;
    if (ownerDocument == null)
      return;
    if (!ownerDocument.Modified)
      ownerDocument.SaveModificationDate = e.SaveModificationDate;
    else if (!e.SaveModificationDate)
      ownerDocument.SaveModificationDate = false;
    ownerDocument.Modified = true;
  }

  /// <summary>Метод вызывается после добавления дочернего элемента, но до вызова события ChildNodeAdded</summary>
  /// <param name="child">Дочерний элемент</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected override void PostProcessAddChildNode(
    DocumentTreeNode child,
    bool insertByShift,
    bool updateUI,
    bool updateLayout)
  {
    child.IdService = this.IdService;
    if (child is VisualNode visualNode)
      visualNode.SetNeedUIRecursive(this.NeedUI, updateUI);
    base.PostProcessAddChildNode(child, insertByShift, updateUI, updateLayout);
  }

  /// <summary>номер страницы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_181")]
  [CustomDescription("Attribute.Interfaces.Document_182")]
  [CustomCategory("Attribute.Interfaces.Document_183")]
  public int PageNumber
  {
    [DebuggerStepThrough] get
    {
      if (this.pageNumber != int.MinValue)
        return this.pageNumber;
      int index = this.Index;
      return index != -1 ? index + 1 : 1;
    }
    set => this.SetPageNumber(value, true, true);
  }

  /// <summary>Иерархический номер страницы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_1811")]
  [CustomDescription("Attribute.Interfaces.Document_182")]
  [CustomCategory("Attribute.Interfaces.Document_183")]
  public string HierarchicalPageNumber
  {
    [DebuggerStepThrough] get
    {
      return string.IsNullOrWhiteSpace(this.hierarchicalPageNumber) ? this.PageNumber.ToString() : this.hierarchicalPageNumber;
    }
    set
    {
      if (!(this.hierarchicalPageNumber != value))
        return;
      string attributeValue1 = this.GetAttributeValue(DocumentTreeNode.AttributeName_DocPageNumber, false);
      string attributeValue2 = this.GetAttributeValue(DocumentTreeNode.AttributeName_PageNumberMore1, false);
      this.hierarchicalPageNumber = value;
      this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_DocPageNumber, (object) attributeValue1, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_DocPageNumber, false), true, true));
      this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_PageNumberMore1, (object) attributeValue2, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_PageNumberMore1, false), true, true));
    }
  }

  /// <summary>Является ли страница дополнительной</summary>
  [Browsable(false)]
  public bool IsAdditionalPage
  {
    get
    {
      return !string.IsNullOrWhiteSpace(this.hierarchicalPageNumber) && !PageNumberingHelper.IsNumericString(this.hierarchicalPageNumber);
    }
  }

  /// <summary>
  /// Является ли страница последним доп.листом в цепочке или последним перед обычным
  /// </summary>
  [Browsable(false)]
  public bool IsLastAdditionalPageInChain
  {
    get
    {
      if (!this.IsAdditionalPage)
        return false;
      return this.NextPage == null || this.NextPage.IsAdditionalPage;
    }
  }

  /// <summary>
  /// Является ли страница первым доп.листом, в начале цепочки или перед обычным листом в цепочке данных
  /// </summary>
  [Browsable(false)]
  public bool IsFirstAdditionalPageInChain
  {
    get
    {
      if (!this.IsAdditionalPage)
        return false;
      return this.PrevPage == null || !this.PrevPage.IsAdditionalPage;
    }
  }

  /// <summary>
  /// Является ли страница обычной, следующей за доп.листом в цепочке данных
  /// </summary>
  [Browsable(false)]
  public bool IsNextToAdditionalPage
  {
    get
    {
      if (this.IsAdditionalPage)
        return false;
      PageData prevPage = this.PrevPage;
      return prevPage != null && prevPage.IsAdditionalPage;
    }
  }

  /// <summary>Является ли страница титульным листом</summary>
  [Browsable(false)]
  public bool IsTitlePage
  {
    get
    {
      return (this.Id.StartsWith("TL") || this.Id.ToLower().Contains("титульный лист")) && this.PrevPage == null;
    }
  }

  /// <summary>Установить значение PageNumber</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetPageNumber(int value, bool updateUI, bool updateLayout)
  {
    if (this.pageNumber == value)
      return;
    string attributeValue1 = this.GetAttributeValue(DocumentTreeNode.AttributeName_DocPageNumber, false);
    string attributeValue2 = this.GetAttributeValue(DocumentTreeNode.AttributeName_PageNumberMore1, false);
    this.pageNumber = value;
    this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_DocPageNumber, (object) attributeValue1, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_DocPageNumber, false), updateUI, updateLayout));
    this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_PageNumberMore1, (object) attributeValue2, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_PageNumberMore1, false), updateUI, updateLayout));
  }

  /// <summary>Глобальный номер страницы в пределах комплекта</summary>
  [Browsable(false)]
  public int GlobalPageNumber
  {
    [DebuggerStepThrough] get
    {
      if (this.OwnerDocument == null)
        return this.PageNumber;
      DocumentsComplect rootComplect = this.OwnerDocument.GetRootComplect();
      if (rootComplect == null)
        return this.PageNumber;
      List<PageData> pageDataList = new List<PageData>();
      foreach (ImDocumentData allDocument in rootComplect.GetAllDocuments())
        pageDataList.AddRange((IEnumerable<PageData>) allDocument.GetAllPages());
      return pageDataList.IndexOf(this) + 1;
    }
  }

  /// <summary>номер страницы</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_538")]
  [CustomDescription("Attribute.Interfaces.Document_539")]
  [CustomCategory("Attribute.Interfaces.Document_183")]
  public int ComplectPageNumber
  {
    [DebuggerStepThrough] get
    {
      return this.complectPageNumber != int.MinValue ? this.complectPageNumber : this.PageNumber;
    }
    set => this.SetComplectPageNumber(value, true, true);
  }

  /// <summary>Установить значение ComplectPageNumber</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void SetComplectPageNumber(int value, bool updateUI, bool updateLayout)
  {
    if (this.complectPageNumber == value)
      return;
    int complectPageNumber = this.complectPageNumber;
    this.complectPageNumber = value;
    this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_ComplectPageNumber, (object) complectPageNumber, (object) this.complectPageNumber, updateUI, updateLayout));
  }

  /// <summary>Пересчитать и установить значение HierarchicalPageNumber</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void UpdateHierarhicalPageNumber(PageData previousPage, bool updateUI, bool updateLayout)
  {
    string str = (string) null;
    if (previousPage == null)
      return;
    if (previousPage.IsAdditionalPage)
    {
      PageNumBuilder pageNumBuilder1 = PageNumBuilder.Parse(previousPage.HierarchicalPageNumber);
      if (this.IsAdditionalPage)
      {
        str = pageNumBuilder1.IncrementExtension().ToString();
      }
      else
      {
        PageNumBuilder pageNumBuilder2 = pageNumBuilder1.IncrementMainNumber();
        pageNumBuilder2.Extension = string.Empty;
        str = pageNumBuilder2.ToString();
      }
    }
    else if (this.IsAdditionalPage)
    {
      PageNumBuilder pageNumBuilder = PageNumBuilder.Parse(this.hierarchicalPageNumber);
      pageNumBuilder.MainPart = (int) byte.Parse(previousPage.HierarchicalPageNumber);
      pageNumBuilder.ResetExtension();
      str = pageNumBuilder.ToString();
    }
    else
    {
      int num = previousPage.PageNumber;
      if (num.ToString() != previousPage.HierarchicalPageNumber)
      {
        num = PageNumBuilder.Parse(previousPage.HierarchicalPageNumber).IncrementMainNumber().MainPart;
        str = num.ToString();
      }
    }
    if (!(this.hierarchicalPageNumber != str))
      return;
    string hierarchicalPageNumber = this.hierarchicalPageNumber;
    this.hierarchicalPageNumber = !this.pageNumber.ToString().Equals(str, StringComparison.Ordinal) ? str : (string) null;
    this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_DocPageNumber, (object) hierarchicalPageNumber, (object) this.hierarchicalPageNumber, updateUI, updateLayout));
  }

  /// <summary>Генерирует событие Removed</summary>
  protected override void OnRemoved(Removed_EventArgs e)
  {
    if (this.PrevPage != null)
    {
      this.PrevPage.NextPage = this.NextPage;
      this.prevPage = (PageData) null;
    }
    base.OnRemoved(e);
  }

  /// <summary>Герерирует событие ChildNodeRemoved</summary>
  public override void OnChildNodeRemoved(ChildNode_EventArgs e)
  {
    e.Child.IdService = (IUniqueIdService) null;
    base.OnChildNodeRemoved(e);
    if (!e.UpdateUI)
      return;
    this.RefreshUI();
  }

  /// <summary>Удалить страницу из потока данных, не разрывая поток</summary>
  /// <param name="updateUI">Обновлять интерфейс и разбивку документа</param>
  public void RemovePageFromDataFlow(bool updateUI)
  {
    DocumentTreeNode documentTreeNode = (DocumentTreeNode) this.PrevPage ?? this.parent;
    if (this.IsAdditionalPage)
      this.hierarchicalPageNumber = (string) null;
    if (this.PrevPage != null)
      this.MoveFlowDataToPrevPage();
    else
      this.MoveFlowDataToNextPage();
    this.Remove(false, false);
    if (!updateUI)
      return;
    documentTreeNode.SetNeedUpdateLayoutFlag(true, false, updateUI, updateUI);
  }

  /// <summary>Удалить страницу вместе со всей цепочкой потока данных</summary>
  /// <param name="uniteDataTables">Собрать все потоки данных</param>
  /// <param name="update">Обновлять интерфейс и разбивку документа</param>
  public void RemovePageWithDataFlow(bool uniteDataTables, bool update)
  {
    List<PageData> pageDataList = new List<PageData>();
    PageData pageData = this.FindFirstPage();
    ImDocumentData ownerDocument = this.OwnerDocument;
    if (uniteDataTables)
    {
      foreach (RectangleElement rectangleElement in pageData.Flows.OfType<RectangleElement>())
        rectangleElement.UniteTable();
    }
    do
    {
      pageDataList.Add(pageData);
      pageData = pageData.NextPage;
    }
    while (pageData != null);
    for (int index = pageDataList.Count - 1; index >= 0; --index)
      pageDataList[index].Remove(true, false, false);
    ownerDocument?.SetNeedUpdateLayoutFlag(true, false, update, update);
  }

  /// <summary>Удалить страницу из потока данных, не разрывая поток</summary>
  /// <param name="updateUI">Обновлять интерфейс и разбивку документа</param>
  internal void RemovePageAndMoveDataFlowToNext()
  {
    if (this.NextPage == null)
      return;
    if (this.IsAdditionalPage)
      this.hierarchicalPageNumber = (string) null;
    this.MoveFlowDataToNextPage();
    this.Remove(false, false);
  }

  /// <summary>Переместить поточные данные на предыдущую страницу и исключить эту страницу из потока данных</summary>
  private void MoveFlowDataToPrevPage()
  {
    if (this.PrevPage == null)
      return;
    IFlowElement[] array = this.Flows.OfType<IFlowElement>().ToArray<IFlowElement>();
    if (array.Length != 0)
    {
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index] is TableData tableData)
          tableData.MoveFlowDataToPrevTable();
      }
    }
    PageData prevPage = this.PrevPage;
    this.PrevPage.NextPage = this.NextPage;
    prevPage.SetNeedUpdateLayoutFlag(true, false, false, false);
  }

  /// <summary>Переместить поточные данные на следующую страницу и исключить эту страницу из потока данных</summary>
  internal void MoveFlowDataToNextPage()
  {
    if (this.NextPage == null)
      return;
    foreach (IFlowElement flow in this.Flows)
    {
      if (flow is TableData tableData)
        tableData.MoveFlowDataToNextTable();
    }
    PageData nextPage = this.NextPage;
    this.NextPage.PrevPage = this.PrevPage;
    nextPage.SetNeedUpdateLayoutFlag(true, false, false, false);
  }

  /// <summary>Переместить поточные данные на следующую страницу и исключить эту страницу из потока данных</summary>
  internal bool CanMoveAllFlowDataToNextPage()
  {
    if (this.NextPage == null)
      return false;
    bool nextPage = false;
    foreach (IFlowElement flow in this.Flows)
    {
      if (flow is TableData tableData)
      {
        if (tableData.NextCell == null || !VisualNode.LessOrEqualWithMiscalculation(tableData.CellsMinHeight, tableData.NextCell.MaxHeight))
          return false;
        nextPage = true;
      }
    }
    return nextPage;
  }

  /// <summary>Команда пользователя "Удалить". В общем случае не совпадает с Remove()</summary>
  /// <param name="update">Обновлять внешний вид и разбивку по страницам</param>
  public override void UserCommand_Delete(bool update)
  {
    PageData pageData1 = this.FindLastPage();
    while (pageData1 != null)
    {
      PageData pageData2 = pageData1;
      pageData1 = pageData2.prevPage;
      pageData2.Remove(pageData1 == null, pageData1 == null);
    }
  }

  /// <summary>Вернуть разрешение дисплея</summary>
  /// <returns>Разрешение дисплея</returns>
  public virtual PointF GetDisplayDpi() => new PointF(96f, 96f);

  /// <summary>Перевести миллиметры в пиксели</summary>
  /// <param name="x">Координата X в мм</param>
  /// <returns>Пиксели</returns>
  public int ConvertXMmToPixel(float x)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return UnitsConverter.MmToPixels(x, displayDpi.X);
  }

  /// <summary>Перевести миллиметры в пиксели</summary>
  /// <param name="x">Координата X в мм</param>
  /// <returns>Пиксели</returns>
  public float ConvertXMmToPixelF(float x)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return (float) UnitsConverter.MmToPixels(x, displayDpi.X);
  }

  /// <summary>Перевести миллиметры в пиксели</summary>
  /// <param name="y">Координата Y в мм</param>
  /// <returns>Пиксели</returns>
  public int ConvertYMmToPixel(float y)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return UnitsConverter.MmToPixels(y, displayDpi.Y);
  }

  /// <summary>Перевести миллиметры в пиксели</summary>
  /// <param name="y">Координата Y в мм</param>
  /// <returns>Пиксели</returns>
  public float ConvertYMmToPixelF(float y)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return (float) UnitsConverter.MmToPixels(y, displayDpi.Y);
  }

  /// <summary>Перевести миллиметры в пиксели</summary>
  /// <param name="point">Координаты точки в миллиметрах</param>
  /// <returns>Координаты точки в пикселях</returns>
  public Point ConvertMmToPixel(PointF point)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return new Point(UnitsConverter.MmToPixels(point.X, displayDpi.X), UnitsConverter.MmToPixels(point.Y, displayDpi.Y));
  }

  /// <summary>Перевести размеры из миллиметров в пиксели</summary>
  /// <param name="size">Размеры в миллиметрах</param>
  /// <returns>Размеры в пикселях</returns>
  public System.Drawing.Size ConvertMmToPixel(SizeF size)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return new System.Drawing.Size(UnitsConverter.MmToPixels(size.Width, displayDpi.X), UnitsConverter.MmToPixels(size.Height, displayDpi.Y));
  }

  /// <summary>Перевести миллиметры в пиксели</summary>
  /// <param name="point">Координаты точки в миллиметрах</param>
  /// <returns>Координаты точки в пикселях</returns>
  public PointF ConvertMmToPixelF(PointF point)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return new PointF(UnitsConverter.MmToPixelsF(point.X, displayDpi.X), UnitsConverter.MmToPixelsF(point.Y, displayDpi.Y));
  }

  /// <summary>Перевести миллиметры в пиксели</summary>
  /// <param name="rectangle">Координаты в миллиметрах</param>
  /// <returns>Координаты точки в пикселях</returns>
  public Rectangle ConvertMmToPixel(RectangleF rectangle)
  {
    PointF displayDpi = this.GetDisplayDpi();
    Point location = new Point(UnitsConverter.MmToPixels(rectangle.X, displayDpi.X), UnitsConverter.MmToPixels(rectangle.Y, displayDpi.Y));
    Point point = new Point(UnitsConverter.MmToPixels(rectangle.Right, displayDpi.X), UnitsConverter.MmToPixels(rectangle.Bottom, displayDpi.Y));
    return new Rectangle(location, new System.Drawing.Size(point.X - location.X, point.Y - location.Y));
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="rectangle">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public virtual Rectangle ConvertWorldToPixel(RectangleF rectangle, MatrixWrapper m)
  {
    PointF pointF1 = m.TransformPoint(rectangle.Location);
    PointF pointF2 = m.TransformPoint(new PointF(rectangle.Right, rectangle.Bottom));
    return this.ConvertMmToPixel(RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y));
  }

  public virtual Point ConvertWorldToPixel(PointF point, MatrixWrapper m)
  {
    return this.ConvertMmToPixel(m.TransformPoint(point));
  }

  /// <summary>Перевести мировые координаты в пиксели</summary>
  /// <param name="rectangle">Мировые координаты</param>
  /// <returns>Пиксели</returns>
  public virtual Rectangle ConvertWorldToPixel(RectangleF rectangle) => Rectangle.Empty;

  public virtual Point ConvertWorldToPixel(PointF point) => Point.Empty;

  /// <summary>Перевести миллиметры в пиксели</summary>
  /// <param name="rectangle">Координаты в миллиметрах</param>
  /// <param name="dpi">dpi</param>
  /// <returns>Координаты точки в пикселях</returns>
  public static Rectangle ConvertMmToPixel(RectangleF rectangle, PointF dpi)
  {
    Point location = new Point(UnitsConverter.MmToPixels(rectangle.X, dpi.X), UnitsConverter.MmToPixels(rectangle.Y, dpi.Y));
    Point point = new Point(UnitsConverter.MmToPixels(rectangle.Right, dpi.X), UnitsConverter.MmToPixels(rectangle.Bottom, dpi.Y));
    return new Rectangle(location, new System.Drawing.Size(point.X - location.X, point.Y - location.Y));
  }

  /// <summary>Перевести пиксели в миллиметры</summary>
  /// <param name="point">Координаты точки в пикселях</param>
  /// <returns>Координаты точки в миллиметрах</returns>
  public PointF ConvertPixelToMm(Point point)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return new PointF(UnitsConverter.PixelsToMm(point.X, displayDpi.X), UnitsConverter.PixelsToMm(point.Y, displayDpi.Y));
  }

  /// <summary>Перевести пиксели в миллиметры</summary>
  /// <param name="rectangle">Координаты прямоугольника в пикселях</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public RectangleF ConvertPixelToMm(Rectangle rectangle)
  {
    PointF displayDpi = this.GetDisplayDpi();
    return new RectangleF(UnitsConverter.PixelsToMm(rectangle.X, displayDpi.X), UnitsConverter.PixelsToMm(rectangle.Y, displayDpi.Y), UnitsConverter.PixelsToMm(rectangle.Width, displayDpi.X), UnitsConverter.PixelsToMm(rectangle.Height, displayDpi.Y));
  }

  /// <summary>Перевести пиксели в миллиметры</summary>
  /// <param name="rectangle">Координаты прямоугольника в пикселях</param>
  /// <param name="dpi">dpi</param>
  /// <returns>Координаты прямоугольника в миллиметрах</returns>
  public static RectangleF ConvertPixelToMm(Rectangle rectangle, PointF dpi)
  {
    return new RectangleF(UnitsConverter.PixelsToMm(rectangle.X, dpi.X), UnitsConverter.PixelsToMm(rectangle.Y, dpi.Y), UnitsConverter.PixelsToMm(rectangle.Width, dpi.X), UnitsConverter.PixelsToMm(rectangle.Height, dpi.Y));
  }

  /// <summary>Конвертировать координаты в формат пользователя</summary>
  /// <param name="point">Точка во внутреннем формате</param>
  /// <returns>Точка в пользовательском формате</returns>
  public virtual PointF ConvertInternalToUser(PointF point) => point;

  /// <summary>Конвертировать размер в формат пользователя</summary>
  /// <param name="size">Размер во внутреннем формате</param>
  /// <returns>Размер в пользовательском формате</returns>
  public virtual SizeF ConvertInternalToUser(SizeF size) => size;

  /// <summary>Конвертировать прямоугольник в пользовательский формат</summary>
  /// <param name="rectangle">Прямоугольник во внутреннем формате</param>
  /// <returns>Прямоугольник в пользовательском формате</returns>
  public virtual RectangleF ConvertInternalToUser(RectangleF rectangle)
  {
    PointF user1 = this.ConvertInternalToUser(rectangle.Location);
    PointF user2 = this.ConvertInternalToUser(new PointF(rectangle.Right, rectangle.Bottom));
    if ((double) user1.Y > (double) user2.Y)
    {
      float y = user1.Y;
      user1.Y = user2.Y;
      user2.Y = y;
    }
    return UnitsConverter.RoundPectangle(RectangleF.FromLTRB(user1.X, user1.Y, user2.X, user2.Y), 5);
  }

  /// <summary>Преобразовать точку из пользовательского формата</summary>
  /// <param name="point">Точка в пользовательском формате</param>
  /// <returns>Точка во внутреннем формате</returns>
  public virtual PointF ConvertUserToInternal(PointF point) => point;

  /// <summary>Преобразовать размер из пользовательского формата</summary>
  /// <param name="size">Размер в пользовательском формате</param>
  /// <returns>Размер во внутреннем формате</returns>
  public virtual SizeF ConvertUserToInternal(SizeF size) => size;

  /// <summary>Преобразовать прямоугольник из пользовательского формата</summary>
  /// <param name="rectangle">Прямоугольник в пользовательском формате</param>
  /// <returns>Прямоугольник во внутреннем формате</returns>
  public virtual RectangleF ConvertUserToInternal(RectangleF rectangle)
  {
    PointF pointF1 = this.ConvertUserToInternal(rectangle.Location);
    PointF pointF2 = this.ConvertUserToInternal(new PointF(rectangle.Right, rectangle.Bottom));
    if ((double) pointF1.Y > (double) pointF2.Y)
    {
      float y = pointF1.Y;
      pointF1.Y = pointF2.Y;
      pointF2.Y = y;
    }
    return UnitsConverter.RoundPectangle(RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y), 5);
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    bool flag = this.Template != null;
    if (this.manualInserted)
      xw.WriteAttributeString("manualInserted", "1");
    xw.WriteAttributeString("size", new SizeFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.size));
    if ((double) this._offset != 0.0 && this.IsFormulaLib && !flag)
      xw.WriteAttributeString("offset", this._offset.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this._alignInText != PictAlignmentInText.Center && this.IsFormulaLib && !flag)
      xw.WriteAttributeString("alignInText", ((int) this._alignInText).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!flag || (this.overrideFlags & OverrideFlags.PrintPageBounds) != OverrideFlags.None)
      xw.WriteAttributeString("printBounds", this.printBounds.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.nextPageTemplateId != null && this.nextPageTemplateId != "" && (!flag || (this.overrideFlags2 & OverrideFlags2.NextPageTemplateId) != OverrideFlags2.None))
      xw.WriteAttributeString("nextPageTemplateId", this.nextPageTemplateId);
    if (this.lastPageTemplateId != null && this.lastPageTemplateId != "" && (!flag || (this.overrideFlags2 & OverrideFlags2.LastPageTemplateId) != OverrideFlags2.None))
      xw.WriteAttributeString("lastPageTemplateId", this.lastPageTemplateId);
    bool firstTime = false;
    long id;
    if (this.prevPage != null)
    {
      XmlWriter xmlWriter = xw;
      id = objectRefId.GetId((object) this.prevPage, out firstTime);
      string str = id.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("prevPageRef", str);
    }
    if (this.parentFlow != null)
    {
      XmlWriter xmlWriter = xw;
      id = objectRefId.GetId((object) this.parentFlow, out firstTime);
      string str = id.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("parentFlowRef", str);
    }
    if (this.autoSize)
      xw.WriteAttributeString("autosize", "1");
    if (this.fromNewPage)
      xw.WriteAttributeString("fromNewPage", "1");
    if (string.IsNullOrWhiteSpace(this.hierarchicalPageNumber))
      return;
    xw.WriteAttributeString("hierarchicalPageNumber", this.hierarchicalPageNumber);
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (this.flows.Count > 0)
      this.flows.WriteToXml("flows", xw, objectRefId);
    base.WriteXmlElements(xw, objectRefId);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (PageData.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      PageData.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    switch (readArgs.Reader.LocalName)
    {
      case "autosize":
        PageData.ReadAutosize((DocumentTreeNode) this, readArgs);
        return true;
      case "flows":
        PageData.ReadFlows((DocumentTreeNode) this, readArgs);
        return true;
      case "fromNewPage":
        PageData.ReadFromNewPage((DocumentTreeNode) this, readArgs);
        return true;
      case "hierarchicalPageNumber":
        PageData.ReadHierarchicalPageNumber((DocumentTreeNode) this, readArgs);
        return true;
      case "lastPageTemplateId":
        PageData.ReadLastPageTemplateId((DocumentTreeNode) this, readArgs);
        return true;
      case "location":
        PageData.ReadLocation((DocumentTreeNode) this, readArgs);
        return true;
      case "manualInserted":
        PageData.ReadManualInserted((DocumentTreeNode) this, readArgs);
        return true;
      case "nextPageRef":
        PageData.ReadNextPageRef((DocumentTreeNode) this, readArgs);
        return true;
      case "nextPageTemplateId":
        PageData.ReadNextPageTemplateId((DocumentTreeNode) this, readArgs);
        return true;
      case "parentFlowRef":
        PageData.ReadParentFlowRef((DocumentTreeNode) this, readArgs);
        return true;
      case "prevPageRef":
        PageData.ReadPrevPageRef((DocumentTreeNode) this, readArgs);
        return true;
      case "printBounds":
        PageData.ReadPrintBounds((DocumentTreeNode) this, readArgs);
        return true;
      case "size":
        PageData.ReadSize((DocumentTreeNode) this, readArgs);
        return true;
      default:
        return base.ReadFieldFromXml(readArgs);
    }
  }

  private static void InitReadFieldDict()
  {
    PageData.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) VisualNode.ReadFieldsDict);
    PageData.ReadFieldsDict.Add("location", new ReadFieldFromXmlDelegate(PageData.ReadLocation));
    PageData.ReadFieldsDict.Add("size", new ReadFieldFromXmlDelegate(PageData.ReadSize));
    PageData.ReadFieldsDict.Add("offset", new ReadFieldFromXmlDelegate(PageData.ReadOffset));
    PageData.ReadFieldsDict.Add("alignInText", new ReadFieldFromXmlDelegate(PageData.ReadAlignInText));
    PageData.ReadFieldsDict.Add("autosize", new ReadFieldFromXmlDelegate(PageData.ReadAutosize));
    PageData.ReadFieldsDict.Add("printBounds", new ReadFieldFromXmlDelegate(PageData.ReadPrintBounds));
    PageData.ReadFieldsDict.Add("nextPageTemplateId", new ReadFieldFromXmlDelegate(PageData.ReadNextPageTemplateId));
    PageData.ReadFieldsDict.Add("lastPageTemplateId", new ReadFieldFromXmlDelegate(PageData.ReadLastPageTemplateId));
    PageData.ReadFieldsDict.Add("nextPageRef", new ReadFieldFromXmlDelegate(PageData.ReadNextPageRef));
    PageData.ReadFieldsDict.Add("prevPageRef", new ReadFieldFromXmlDelegate(PageData.ReadPrevPageRef));
    PageData.ReadFieldsDict.Add("parentFlowRef", new ReadFieldFromXmlDelegate(PageData.ReadParentFlowRef));
    PageData.ReadFieldsDict.Add("flows", new ReadFieldFromXmlDelegate(PageData.ReadFlows));
    PageData.ReadFieldsDict.Add("manualInserted", new ReadFieldFromXmlDelegate(PageData.ReadManualInserted));
    PageData.ReadFieldsDict.Add("hierarchicalPageNumber", new ReadFieldFromXmlDelegate(PageData.ReadHierarchicalPageNumber));
  }

  private static void ReadFlows(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode).flows.ReadFromXml(readArgs);
  }

  private static void ReadParentFlowRef(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    string str = readArgs.Reader.Value;
    if (readArgs.ObjectsId[(object) str] is IParentFlow parentFlow)
      ((PageData) docNode).parentFlow = parentFlow;
    else
      DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "parentFlow", str);
  }

  private static void ReadPrevPageRef(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    string str = readArgs.Reader.Value;
    if (readArgs.ObjectsId[(object) str] is PageData pageData)
    {
      ((PageData) docNode).prevPage = pageData;
      pageData.nextPage = (PageData) docNode;
    }
    else
      DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "prevPage", str);
  }

  private static void ReadNextPageRef(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    DocumentTreeNode.AddObjectReference((object) docNode, readArgs.ObjectReferences, "nextPage", readArgs.Reader.Value);
  }

  private static void ReadLastPageTemplateId(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode).lastPageTemplateId = readArgs.Reader.Value;
    docNode.overrideFlags2 |= OverrideFlags2.LastPageTemplateId;
  }

  private static void ReadNextPageTemplateId(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode).nextPageTemplateId = readArgs.Reader.Value;
    docNode.overrideFlags2 |= OverrideFlags2.NextPageTemplateId;
  }

  private static void ReadPrintBounds(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode).printBounds = bool.Parse(readArgs.Reader.Value);
    docNode.overrideFlags |= OverrideFlags.PrintPageBounds;
  }

  private static void ReadSize(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 16 /*0x10*/)
      ((PageData) docNode).size = (SizeF) new SizeFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value.Replace(',', '.'));
    else
      ((PageData) docNode).size = (SizeF) new SizeFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value);
  }

  private static void ReadOffset(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    PageData pageData = (PageData) docNode;
    float.TryParse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out ((PageData) docNode)._offset);
    if ((double) pageData._offset == 0.0)
      return;
    pageData._alignInText = PictAlignmentInText.CustomBaseLine;
  }

  private static void ReadAlignInText(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode)._alignInText = (PictAlignmentInText) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadAutosize(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode).autoSize = readArgs.Reader.Value == "1";
  }

  private static void ReadFromNewPage(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode).fromNewPage = readArgs.Reader.Value == "1";
  }

  private static void ReadManualInserted(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode).manualInserted = readArgs.Reader.Value == "1";
  }

  private static void ReadLocation(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
  }

  private static void ReadHierarchicalPageNumber(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((PageData) docNode).hierarchicalPageNumber = string.IsNullOrWhiteSpace(readArgs.Reader.Value) ? (string) null : readArgs.Reader.Value;
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
    if (!(src is PageData pageData))
      return;
    this._offset = pageData._offset;
    this._alignInText = pageData._alignInText;
    this.size = pageData.size;
    this.pageNumber = pageData.pageNumber;
    this.nextPageTemplateId = pageData.nextPageTemplateId;
    this.lastPageTemplateId = pageData.lastPageTemplateId;
    this.printBounds = pageData.printBounds;
    this.autoSize = pageData.autoSize;
    this.fromNewPage = pageData.fromNewPage;
    this.hierarchicalPageNumber = pageData.hierarchicalPageNumber;
  }

  /// <summary>Восстановить сохраненные ссылки</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  public override void RestoreLinks(
    bool copyChildren,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    if (externalLink && (this.parentFlow != null || this.nextPage != null || this.prevPage != null) || copyChildren && this.flows.Count > 0)
    {
      PageData link1 = (PageData) links[(object) this];
      if (link1 != null)
      {
        if (copyChildren)
        {
          for (int index = 0; index < this.flows.Count; ++index)
          {
            IParentFlow link2 = (IParentFlow) links[(object) this.flows[index]];
            if (link2 == null)
              LogManager.AddLine("PageData.RestoreLinks(): flow == null");
            if (link2 != null && !link1.flows.Contains((IFlowElement) link2))
              link1.flows.Add((IFlowElement) link2);
          }
        }
        if (externalLink)
        {
          if (this.parentFlow != null)
            link1.parentFlow = (IParentFlow) links[(object) this.parentFlow];
          if (this.nextPage != null)
            link1.nextPage = (PageData) links[(object) this.nextPage];
          if (this.prevPage != null)
            link1.prevPage = (PageData) links[(object) this.prevPage];
        }
      }
    }
    base.RestoreLinks(copyChildren, templateClone, externalLink, links);
  }

  /// <summary>Метод вызываемый при десериализации.
  /// Реализация IDeserializationCallback</summary>
  public override void OnDeserialization(object sender)
  {
    base.OnDeserialization(sender);
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageElementNode node)
        node.AssignPage(this, false, false);
    }
    if (this.nextPage == null)
      return;
    this.nextPage.PrevPage = this;
  }

  /// <summary>Обновить идентификаторы в ссылках на данные по установленным связям с данными</summary>
  internal virtual void UpdateDataIdCacheLinks()
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageElementNode node)
        node.UpdateDataIdCacheLinks();
    }
  }

  /// <summary>Восстановить идентификаторы потоков</summary>
  public virtual void RestoreFlowIds()
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageElementNode node)
        node.RestoreFlowId();
    }
  }

  /// <summary>Заблокировать обновление геометрии интерфейса и изображения</summary>
  public override void SuspendUpdateGeometryRefreshUI()
  {
    ++this.suspendUpdateUIGeometryCount;
    ++this.suspendRefreshUICount;
  }

  /// <summary>Разблокировать и провести обновление геометрии интерфейса и изображения</summary>
  public override void ResumeUpdateRefreshUI(bool update, bool refresh)
  {
    if (this.suspendUpdateUIGeometryCount > 0)
      --this.suspendUpdateUIGeometryCount;
    else
      this.suspendUpdateUIGeometryCount = 0;
    if (this.suspendRefreshUICount > 0)
      --this.suspendRefreshUICount;
    else
      this.suspendRefreshUICount = 0;
    base.ResumeUpdateRefreshUI(update, refresh);
  }

  /// <summary>Обновление геометрии интерфейса пользователя заблокировано</summary>
  [Category("Debug")]
  public override bool SuspendedUpdateUIGeometryFlag
  {
    [DebuggerStepThrough] get => this.suspendUpdateUIGeometryCount > 0;
    set
    {
      if (value == this.SuspendedUpdateUIGeometryFlag)
        return;
      if (value)
        ++this.suspendUpdateUIGeometryCount;
      else
        this.suspendUpdateUIGeometryCount = 0;
    }
  }

  /// <summary>Заблокировать обновление геометрии интерфейса пользователя
  /// <remarks>Блокировка увеличивает значение счетчика. Разблокировка соответственно уменьшает значение счетчика. При нулевом значении счетчика обновление разрешено.</remarks>&gt;
  /// </summary>
  public override void SuspendUpdateUIGeometry() => ++this.suspendUpdateUIGeometryCount;

  /// <summary>Разблокировать обновление геометрии интерфейса пользователя</summary>
  /// <param name="update">Обновить геометрию</param>
  /// <param name="refresh">Обновить изображение</param>
  public override void ResumeUpdateUIGeometry(bool update, bool refresh)
  {
    if (this.suspendUpdateUIGeometryCount > 0)
      --this.suspendUpdateUIGeometryCount;
    else
      this.suspendUpdateUIGeometryCount = 0;
    base.ResumeUpdateUIGeometry(update, refresh);
  }

  /// <summary>Установить значение счетчика SuspendRefreshUI для узла и подузлов</summary>
  /// <param name="count">Значение счетчика</param>
  internal void SetSuspendRefreshUICount(int count) => this.suspendRefreshUICount = count;

  /// <summary>Установить значение счетчика SuspendUpdateUIGeometry для узла и подузлов</summary>
  /// <param name="count">Значение счетчика</param>
  internal void SetSuspendUpdateUIGeometryCount(int count)
  {
    this.suspendUpdateUIGeometryCount = count;
  }

  /// <summary>Заблокировать обновление изображения
  /// <remarks>Блокировка увеличивает значение счетчика. Разблокировка соответственно уменьшает значение счетчика. При нулевом значении счетчика обновление разрешено.</remarks>&gt;
  /// </summary>
  public override void SuspendRefreshUI() => ++this.suspendRefreshUICount;

  /// <summary>Разблокировать обновление изображения</summary>
  public override void ResumeRefreshUI(bool refresh)
  {
    if (this.suspendRefreshUICount > 0)
      --this.suspendRefreshUICount;
    else
      this.suspendRefreshUICount = 0;
    if (!refresh)
      return;
    this.RefreshUI();
  }

  /// <summary>Обновление изображения интерфейса пользователя заблокировано</summary>
  [Category("Debug")]
  public override bool SuspendedRefreshUIFlag
  {
    [DebuggerStepThrough] get => this.suspendRefreshUICount > 0;
    set
    {
      if (value == this.SuspendedRefreshUIFlag)
        return;
      if (value)
        ++this.suspendRefreshUICount;
      else
        this.suspendRefreshUICount = 0;
    }
  }
}
