// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ContainerData
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Контейнер OLE объектов и рисунков</summary>
[Serializable]
public class ContainerData : RectangleElement, INodeWithReference
{
  /// <summary>Имя типа элемента</summary>
  public static string ElementTypeName = LocalizationHolder.rm.GetString("Interfaces.Document_3");
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  public const int BufferSize = 65536 /*0x010000*/;
  protected static readonly int AutoCAD_SIGN = 1094922544;
  protected static readonly int Root_SIGN = 1383034740;
  protected VertAlignment vertAlignment;
  protected ContainerHorzAlignment horzAlignment = ContainerHorzAlignment.Center;
  protected ImageScaleMode scaleMode = ImageScaleMode.FitWidthHeight;
  protected SizeF originalSize = SizeF.Empty;
  protected DataSourceType dataSourceType;
  protected Image image;
  protected Stream dataStream;
  protected ReferenceToGraphicsBase reference;
  protected List<string> layers;
  protected ArcMethods arcMethod;
  protected string streamFileName;
  /// <summary>Не удалось отобразить изображение</summary>
  protected bool drawImageFailed;
  /// <summary>
  /// Первая отрисовка после загрузки, флаг используется для попытки обновления Image после загрузки
  /// </summary>
  private bool firstDrawImage = true;

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected ContainerData(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Конструктор</summary>
  public ContainerData()
  {
  }

  protected override void InitFields() => base.InitFields();

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public ContainerData(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  public ContainerData(DocumentTreeNode parent, RectangleF bounds, bool visible)
    : base(parent, bounds, visible)
  {
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public static object EmptyConstructor() => (object) new ContainerData();

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new ContainerData(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустую ячейку таблицы</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  /// <returns>Ячейка таблицы</returns>
  protected override RectangleElement CreateEmptySingleCell(
    DocumentTreeNode parent,
    RectangleF bounds,
    bool visible)
  {
    return (RectangleElement) new TextData(parent, bounds, visible);
  }

  /// <summary>Создать пустую таблицу</summary>
  /// <param name="isColumn">Столбец</param>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Размеры элемента</param>
  /// <param name="visible">Видимый</param>
  /// <returns>Таблица</returns>
  protected override TableData CreateEmptyTable(
    bool isColumn,
    DocumentTreeNode parent,
    RectangleF bounds,
    bool visible)
  {
    return new TableData(isColumn, parent, bounds, visible);
  }

  static ContainerData() => ContainerData.InitReadFieldDict();

  [Browsable(false)]
  public override float MaxHeight
  {
    get => base.MaxHeight;
    set => base.MaxHeight = value;
  }

  public override string NodeTypeCaption
  {
    get => ContainerData.ElementTypeName;
    set => base.NodeTypeCaption = value;
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    this.DrawCell(context, (List<RowColParams>) null, -1, (List<RowColParams>) null, -1, true);
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  /// <param name="gridCols">Столбцы сетки</param>
  /// <param name="colIndex">Индекс столбца</param>
  /// <param name="gridRows">Строки сетки</param>
  /// <param name="rowIndex">Индекс строки</param>
  /// <param name="findGridParams">Искать столбец и строк если не заданы</param>
  public override void DrawCell(
    DrawContext context,
    List<RowColParams> gridCols,
    int colIndex,
    List<RowColParams> gridRows,
    int rowIndex,
    bool findGridParams)
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag)
      return;
    RectangleF properBounds = this.ProperBounds;
    bool flag = this.ParentCell != null && ((double) this.SkipCellsBefore >= 1.0 || (double) this.SkipCellsAfter >= 1.0);
    if (!(!flag ? properBounds : this.Bounds).IntersectsWith(context.ClipRectangle))
      return;
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    context.Graphics.PageUnit = GraphicsUnit.Millimeter;
    this.DrawBackground(context, properBounds);
    if (!context.WithoutData && this.image != null)
    {
      if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value)
        context.Graphics.DrawImage(this.image, new PointF[3]
        {
          properBounds.Location,
          new PointF(properBounds.Right, properBounds.Top),
          new PointF(properBounds.Left, properBounds.Bottom)
        }, new RectangleF(0.0f, 0.0f, (float) this.image.Width, (float) this.image.Height), GraphicsUnit.Pixel, VisualNode.NegativeImageAttributes);
      else
        context.Graphics.DrawImage(this.image, new PointF[3]
        {
          properBounds.Location,
          new PointF(properBounds.Right, properBounds.Top),
          new PointF(properBounds.Left, properBounds.Bottom)
        }, new RectangleF(0.0f, 0.0f, (float) this.image.Width, (float) this.image.Height), GraphicsUnit.Pixel);
    }
    RowColParams gridRow = (RowColParams) null;
    if (gridRows != null && rowIndex >= 0 && rowIndex < gridRows.Count)
      gridRow = gridRows[rowIndex];
    RowColParams gridCol = (RowColParams) null;
    if (gridCols != null && colIndex >= 0 && colIndex < gridCols.Count)
      gridCol = gridCols[colIndex];
    if (this.drawEllipse)
      this.DrawEllipseBounds(context, properBounds, gridCol, gridRow, findGridParams);
    else
      this.DrawFrame(context, properBounds, gridCol, gridRow, findGridParams);
    if (!context.WithoutData & flag)
      this.DrawSkipedSpace(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
    context.Graphics.PageUnit = pageUnit;
  }

  [DllImport("Gdi32.dll")]
  public static extern uint GetEnhMetaFileBits(IntPtr hemf, uint cbBuffer, byte[] lpbBuffer);

  [DllImport("Gdi32.dll")]
  public static extern bool DeleteEnhMetaFile(IntPtr hemf);

  /// <summary>Сохранить Image в файл</summary>
  /// <param name="image">Изображение</param>
  /// <param name="filename">Путь к файлу</param>
  public static void SaveImageToFile(Image image, string filename)
  {
    using (FileStream fileStream = File.Create(filename))
      ContainerData.SaveImageToStream(image, (Stream) fileStream);
  }

  /// <summary>Сохранить Image в Stream</summary>
  /// <param name="image">Изображение</param>
  /// <param name="stream">Поток</param>
  public static void SaveImageToStream(Image image, Stream stream)
  {
    if (image is Metafile metafile1)
    {
      Metafile metafile = (Metafile) metafile1.Clone();
      IntPtr henhmetafile = metafile.GetHenhmetafile();
      uint enhMetaFileBits1 = ContainerData.GetEnhMetaFileBits(henhmetafile, 0U, (byte[]) null);
      byte[] numArray = new byte[(int) enhMetaFileBits1];
      int enhMetaFileBits2 = (int) ContainerData.GetEnhMetaFileBits(henhmetafile, enhMetaFileBits1, numArray);
      ContainerData.DeleteEnhMetaFile(henhmetafile);
      metafile.Dispose();
      stream.Write(numArray, 0, numArray.Length);
    }
    else
    {
      try
      {
        ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
        ImageFormat rawFormat = image.RawFormat;
        ImageCodecInfo encoder = (ImageCodecInfo) null;
        Guid guid1 = ImageFormat.Png.Guid;
        Guid guid2 = rawFormat.Guid;
        foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
        {
          Guid formatId = imageCodecInfo.FormatID;
          if (formatId.Equals(guid2))
          {
            encoder = imageCodecInfo;
            break;
          }
          if (encoder == null)
          {
            formatId = imageCodecInfo.FormatID;
            if (formatId.Equals(guid1))
              encoder = imageCodecInfo;
          }
        }
        image.Save(stream, encoder, (EncoderParameters) null);
      }
      catch (Exception ex)
      {
        stream.SetLength(0L);
        stream.Position = 0L;
        image.Save(stream, ImageFormat.Png);
      }
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
    if (!(template is ContainerData containerData))
      throw new Exception(string.Format(ExceptionMessages.InvalideTemplateType, (object) template.GetDefautCaption(), (object) this.GetDefautCaption()));
    if ((this.overrideFlags & OverrideFlags.ScaleMode) == OverrideFlags.None)
      this.scaleMode = containerData.scaleMode;
    if ((this.overrideFlags & OverrideFlags.Data) == OverrideFlags.None)
    {
      this.originalSize = containerData.originalSize;
      this.dataSourceType = containerData.dataSourceType;
      this.image = containerData.image;
      this.dataStream = containerData.dataStream;
      this.drawImageFailed = false;
      this.arcMethod = containerData.ArcMethod;
    }
    if ((this.overrideFlags & OverrideFlags.ImageLayers) == OverrideFlags.None)
      this.layers = containerData.layers;
    if ((this.overrideFlags3 & OverrideFlags3.ContainerVertAlign) == OverrideFlags3.None)
      this.vertAlignment = containerData.vertAlignment;
    if ((this.overrideFlags3 & OverrideFlags3.ContainerHorzAlign) == OverrideFlags3.None)
      this.horzAlignment = containerData.horzAlignment;
    base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
  }

  /// <summary>Можно ли использовать заданный узел как шаблон</summary>
  /// <param name="node">Узел</param>
  /// <returns></returns>
  public override bool CanUseNodeAsTemplate(DocumentTreeNode node)
  {
    return node != null && node is ContainerData;
  }

  /// <summary>Ссылка на объект содержащий данные</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_81")]
  [CustomDescription("Attribute.Interfaces.Document_82")]
  [CustomCategory("Attribute.Interfaces.Document_83")]
  public virtual ReferenceToGraphicsBase Reference
  {
    [DebuggerStepThrough] get => this.reference;
    set => this.AssignReference(value, true, true, true);
  }

  /// <summary>Назначить ссылку на изображение</summary>
  /// <param name="value">Значение ссылки</param>
  /// <param name="resetImageAndDataStream">Сбросить значения для Image и DataStream</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AssignReference(
    ReferenceToGraphicsBase value,
    bool resetImageAndDataStream,
    bool updateUI,
    bool updateLayout)
  {
    if (this.reference == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Reference", (object) this.Reference, (object) value);
    if (this.reference != null)
    {
      this.reference.DisconnectLink();
      this.reference.AssignOwnerNode((DocumentTreeNode) null);
    }
    this.reference = value;
    this.dataSourceType = DataSourceType.Unknown;
    if (resetImageAndDataStream)
    {
      this.SetImage((Image) null, false, false);
      this.AssignDataStream((Stream) null, DataSourceType.Unknown, false, false, false, false);
    }
    if (this.reference != null)
      this.reference.AssignOwnerNode((DocumentTreeNode) this);
    this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Свойство для интерфейса INodeWithReference</summary>
  [Browsable(false)]
  ReferenceBase INodeWithReference.Reference => (ReferenceBase) this.reference;

  /// <summary>Оригинальный размер рисунка, мм</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_84")]
  [CustomDescription("Attribute.Interfaces.Document_85")]
  [CustomCategory("Attribute.Interfaces.Document_86")]
  [TypeConverter(typeof (SizeFConverter))]
  public virtual SizeF OriginalSize
  {
    [DebuggerStepThrough] get => this.originalSize;
  }

  /// <summary>Назначить свойство OriginalSize</summary>
  public virtual void AssignOriginalSize(SizeF value, bool updateUI, bool updateLayout)
  {
    if (!(this.originalSize != value))
      return;
    this.originalSize = value;
    if (!this.originalSize.IsEmpty && this.scaleMode == ImageScaleMode.OriginalAutoSize)
      this.AssignProperBounds(this.Location, this.originalSize, true, false, updateLayout);
    if (!updateUI)
      return;
    this.UpdateUIGeometry(true);
  }

  /// <summary>Получить оригинальный размер изображения в мм</summary>
  public SizeF GetOriginalImageSizeInMM(Image image)
  {
    SizeF pixels = SizeF.Empty;
    if (image != null)
    {
      pixels = image.PhysicalDimension;
      pixels = !(image is Metafile) ? UnitsConverter.PixelsToMm(pixels, new PointF(image.HorizontalResolution, image.VerticalResolution)) : new SizeF(pixels.Width / 100f, pixels.Height / 100f);
    }
    return pixels;
  }

  /// <summary>Высота содержимого ячейки</summary>
  public override float ContentHeight
  {
    get
    {
      if (this.ScaleMode != ImageScaleMode.OriginalAutoSize)
        return this.MinHeight;
      if (this.image != null && this.originalSize == SizeF.Empty)
        this.originalSize = this.GetOriginalImageSizeInMM(this.image);
      float contentHeight = this.originalSize.Height;
      if (VisualNode.MoreWithMiscalculation(this.originalSize.Width, this.properBounds.Width))
        contentHeight = this.originalSize.Height * (this.properBounds.Width / this.originalSize.Width);
      if (VisualNode.LessWithMiscalculation(contentHeight, this.MinHeight))
        contentHeight = this.MinHeight;
      else if (VisualNode.MoreWithMiscalculation(contentHeight, this.properBounds.Height))
        contentHeight = this.properBounds.Height;
      return contentHeight;
    }
  }

  protected virtual Image CreateImageFromDataStream(bool showExceptionOnfail) => (Image) null;

  /// <summary>Распределить данные по ячейке представления</summary>
  /// <param name="newSize">Новый размер</param>
  /// <param name="maxSize">Максимальный размер</param>
  /// <param name="distributed">Результат распределения</param>
  public override void DistributeCell(DistributeContext context)
  {
    context.VertDistributed = DistributeResult.All;
    context.IsFixedSizeRow = new bool?(this.GetIsFixedSizeRows(context.Template, (CellContext) context));
    context.RowSize = new float?(this.GetDefaultRowSize(context.Template, (CellContext) context));
    context.TryNotBreak |= this.tryNotBreak;
    float minSize = (double) this.MinHeight > 0.0 ? this.MinHeight : context.NewSize.Height;
    float num = context.NewSize.Width;
    if (this.image == null)
    {
      try
      {
        this.image = this.CreateImageFromDataStream(false);
        if (this.image != null)
        {
          if (this.reference != null)
            this.reference.ImageCache = this.image;
          this.needUpdateLayoutFlag = true;
        }
      }
      catch (Exception ex)
      {
        LogManager.AddLine(ex);
      }
    }
    if (this.ScaleMode == ImageScaleMode.OriginalAutoSize)
    {
      if (this.image != null && this.originalSize == SizeF.Empty)
        this.originalSize = this.GetOriginalImageSizeInMM(this.image);
      float width = this.originalSize.Width;
      minSize = this.originalSize.Height;
      if (VisualNode.MoreWithMiscalculation(width, context.NewSize.Width))
        minSize = this.originalSize.Height * (context.NewSize.Width / this.originalSize.Width);
      if (VisualNode.LessWithMiscalculation(minSize, this.minHeight))
        minSize = this.minHeight;
      else if (VisualNode.MoreWithMiscalculation(minSize, context.MaxSize.Height))
      {
        minSize = context.MaxSize.Height;
        num = this.originalSize.Width * (minSize / this.originalSize.Height);
        context.VertDistributed = DistributeResult.None;
      }
    }
    context.NewSize.Height = minSize;
    if (context.IsFixedSizeRow_NN)
      context.NewSize.Height = this.RoundForFixedSizeRow(context.NewSize.Height, context.RowSize_NN, minSize);
    this.AssignBounds(this.Location, context.NewSize, false, false, false);
    SizeF size = this.Size;
    if (VisualNode.MoreWithMiscalculation(size.Height, context.MaxSize.Height) || VisualNode.MoreWithMiscalculation(size.Width, context.MaxSize.Width))
      context.VertDistributed = DistributeResult.None;
    if (context.FirstDataOnPage && context.VertDistributed == DistributeResult.None)
      context.VertDistributed = DistributeResult.All;
    this.AssignNeedUpdateLayoutFlag(context.DistributeResultIsNeedUpdateLayout);
  }

  /// <summary>Режим масштабирования</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_87")]
  [CustomDescription("Attribute.Interfaces.Document_88")]
  [CustomCategory("Attribute.Interfaces.Document_89")]
  public virtual ImageScaleMode ScaleMode
  {
    [DebuggerStepThrough] get => this.scaleMode;
    set => this.AssignScaleMode(value, true, true, true);
  }

  /// <summary>Назначить свойство ScaleMode</summary>
  public virtual void AssignScaleMode(
    ImageScaleMode value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if (this.scaleMode == value)
      return;
    this.scaleMode = value;
    if (setOverrideFlag)
      this.overrideFlags |= OverrideFlags.ScaleMode;
    if (!this.originalSize.IsEmpty && this.scaleMode == ImageScaleMode.OriginalAutoSize)
      this.AssignProperBounds(this.Location, this.originalSize, true, false, false);
    if (updateLayout)
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    else if (updateUI)
      this.UpdateUIGeometry(true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (ImDocumentData.ShowDebugInfo)
      return;
    this.RemoveProperty(properties, "DataStreamCount");
    this.RemoveProperty(properties, "DataSourceType");
  }

  /// <summary>Вертикальное выравнивания изображения</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_204")]
  [CustomDescription("Attribute.Interfaces.Document_564")]
  [CustomCategory("Attribute.Interfaces.Document_486")]
  public VertAlignment VertAlignment
  {
    [DebuggerStepThrough] get => this.vertAlignment;
    set => this.SetVertAlignment(value, true);
  }

  /// <summary>Назначить значение VertAlignment</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  public virtual void SetVertAlignment(VertAlignment value, bool updateUI)
  {
    if (this.VertAlignment == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "VertAlignment", (object) this.vertAlignment, (object) value);
    this.vertAlignment = value;
    if (updateUI)
      this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Горизонтальное выравнивания изображения</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_201")]
  [CustomDescription("Attribute.Interfaces.Document_563")]
  [CustomCategory("Attribute.Interfaces.Document_486")]
  public ContainerHorzAlignment HorzAlignment
  {
    [DebuggerStepThrough] get => this.horzAlignment;
    set => this.SetHorzAlignment(value, true);
  }

  /// <summary>Назначить значение HorzAlignment</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  public virtual void SetHorzAlignment(ContainerHorzAlignment value, bool updateUI)
  {
    if (this.HorzAlignment == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "HorzAlignment", (object) this.horzAlignment, (object) value);
    this.horzAlignment = value;
    if (updateUI)
      this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    bool flag = this.Template != null;
    if (!flag || (this.overrideFlags & OverrideFlags.Data) != OverrideFlags.None)
    {
      if (flag || !this.originalSize.IsEmpty)
        xw.WriteAttributeString("originalSize", new SizeFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.originalSize));
      if (flag || this.dataSourceType != DataSourceType.Unknown)
        xw.WriteAttributeString("srcType ", this.dataSourceType.ToString());
    }
    if (this.streamFileName != null)
      xw.WriteAttributeString("fileName ", this.streamFileName);
    if ((flag || this.scaleMode != ImageScaleMode.FitWidthHeight) && (!flag || (this.overrideFlags & OverrideFlags.ScaleMode) != OverrideFlags.None))
      xw.WriteAttributeString("scaleMode", this.scaleMode.ToString());
    int num;
    if (flag && (this.overrideFlags3 & OverrideFlags3.ContainerVertAlign) != OverrideFlags3.None || !flag && this.vertAlignment != VertAlignment.Top)
    {
      XmlWriter xmlWriter = xw;
      num = (int) this.vertAlignment;
      string str = num.ToString();
      xmlWriter.WriteAttributeString("cVertAlign", str);
    }
    if ((!flag || (this.overrideFlags3 & OverrideFlags3.ContainerHorzAlign) == OverrideFlags3.None) && (flag || this.horzAlignment == ContainerHorzAlignment.Left))
      return;
    XmlWriter xmlWriter1 = xw;
    num = (int) this.horzAlignment;
    string str1 = num.ToString();
    xmlWriter1.WriteAttributeString("cHorzAlign", str1);
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlElements(xw, objectRefId);
    if (this.reference != null)
      this.reference.WriteToXml("ImgRef", xw, objectRefId);
    bool flag = this.Template != null;
    if (this.image != null && (!flag || this.IsOverridden(OverrideFlags.Data)))
    {
      xw.WriteStartElement("Image");
      bool firstTime;
      string str = objectRefId.GetId((object) this.image, out firstTime).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xw.WriteAttributeString("refId", str);
      if (firstTime)
      {
        ImChunkedStream inStream = new ImChunkedStream();
        ContainerData.SaveImageToStream(this.image, (Stream) inStream);
        Stream stream;
        if (this.image is Metafile)
        {
          stream = (Stream) new ImChunkedStream();
          ZLibStreamHelper.PackStream((Stream) inStream, ZLibCompressLevels.LevelNormal, stream);
          xw.WriteAttributeString("zipped", "true");
        }
        else
          stream = (Stream) inStream;
        stream.Position = 0L;
        WriteReadXmlHelper.WriteBase64ToCurrentXmlElement(stream, xw);
        stream.Dispose();
        inStream.Dispose();
      }
      xw.WriteEndElement();
    }
    if (this.dataStream != null && this.dataSourceType != DataSourceType.Image && (!flag || this.IsOverridden(OverrideFlags.Data)))
    {
      lock (this.dataStream)
      {
        xw.WriteStartElement("DataStream");
        if (this.arcMethod != ArcMethods.NotPacked)
          xw.WriteAttributeString("arcMethod", ((int) this.arcMethod).ToString());
        this.dataStream.Position = 0L;
        WriteReadXmlHelper.WriteBase64ToCurrentXmlElement(this.dataStream, xw);
        xw.WriteEndElement();
      }
    }
    if (this.layers == null)
      return;
    WriteReadXmlHelper.WriteStringListToXml("Layers", this.layers, "Layer", xw);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (ContainerData.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      ContainerData.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    if (base.ReadFieldFromXml(readArgs))
      return true;
    switch (readArgs.Reader.LocalName)
    {
      case "DataStream":
        ContainerData.ReadDataStream((DocumentTreeNode) this, readArgs);
        return true;
      case "Image":
        ContainerData.ReadImage((DocumentTreeNode) this, readArgs);
        return true;
      case "ImgRef":
        ContainerData.ReadImgRef((DocumentTreeNode) this, readArgs);
        return true;
      case "Layers":
        ContainerData.ReadLayers((DocumentTreeNode) this, readArgs);
        return true;
      case "cHorzAlign":
        ContainerData.ReadHorzAlignment((DocumentTreeNode) this, readArgs);
        return true;
      case "cVertAlign":
        ContainerData.ReadVertAlignment((DocumentTreeNode) this, readArgs);
        return true;
      case "originalSize":
        ContainerData.ReadOriginalSize((DocumentTreeNode) this, readArgs);
        return true;
      case "scaleMode":
        ContainerData.ReadScaleMode((DocumentTreeNode) this, readArgs);
        return true;
      case "srcType":
        ContainerData.ReadSrcType((DocumentTreeNode) this, readArgs);
        return true;
      default:
        return false;
    }
  }

  private static void InitReadFieldDict()
  {
    ContainerData.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) RectangleElement.ReadFieldsDict);
    ContainerData.ReadFieldsDict.Add("Image", new ReadFieldFromXmlDelegate(ContainerData.ReadImage));
    ContainerData.ReadFieldsDict.Add("DataStream", new ReadFieldFromXmlDelegate(ContainerData.ReadDataStream));
    ContainerData.ReadFieldsDict.Add("ImgRef", new ReadFieldFromXmlDelegate(ContainerData.ReadImgRef));
    ContainerData.ReadFieldsDict.Add("originalSize", new ReadFieldFromXmlDelegate(ContainerData.ReadOriginalSize));
    ContainerData.ReadFieldsDict.Add("scaleMode", new ReadFieldFromXmlDelegate(ContainerData.ReadScaleMode));
    ContainerData.ReadFieldsDict.Add("srcType", new ReadFieldFromXmlDelegate(ContainerData.ReadSrcType));
    ContainerData.ReadFieldsDict.Add("Layers", new ReadFieldFromXmlDelegate(ContainerData.ReadLayers));
    ContainerData.ReadFieldsDict.Add("cVertAlign", new ReadFieldFromXmlDelegate(ContainerData.ReadVertAlignment));
    ContainerData.ReadFieldsDict.Add("cHorzAlign", new ReadFieldFromXmlDelegate(ContainerData.ReadHorzAlignment));
    ContainerData.ReadFieldsDict.Add("fileName", new ReadFieldFromXmlDelegate(ContainerData.ReadFileName));
  }

  private static void ReadFileName(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ContainerData) docNode).streamFileName = readArgs.Reader.Value;
  }

  private static void ReadSrcType(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ContainerData) docNode).dataSourceType = (DataSourceType) Enum.Parse(typeof (DataSourceType), readArgs.Reader.Value);
  }

  private static void ReadScaleMode(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ContainerData) docNode).scaleMode = (ImageScaleMode) Enum.Parse(typeof (ImageScaleMode), readArgs.Reader.Value);
    docNode.overrideFlags |= OverrideFlags.ScaleMode;
  }

  private static void ReadOriginalSize(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 16 /*0x10*/)
      ((ContainerData) docNode).originalSize = (SizeF) new SizeFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value.Replace(',', '.'));
    else
      ((ContainerData) docNode).originalSize = (SizeF) new SizeFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value);
  }

  private static void ReadImgRef(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ContainerData) docNode).reference = ReferenceBase.LoadFromXml(readArgs) as ReferenceToGraphicsBase;
    if (((ContainerData) docNode).reference == null)
      return;
    ((ContainerData) docNode).reference.AssignOwnerNode(docNode);
  }

  private static void ReadDataStream(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    lock (docNode)
    {
      ContainerData containerData = (ContainerData) docNode;
      containerData.arcMethod = ArcMethods.NotPacked;
      if (readArgs.Reader.HasAttributes)
      {
        if (readArgs.Reader.MoveToAttribute("arcMethod"))
          containerData.arcMethod = (ArcMethods) Convert.ToInt32(readArgs.Reader.Value);
        readArgs.Reader.MoveToElement();
      }
      if (!readArgs.Reader.HasValue && !readArgs.Reader.IsEmptyElement)
        readArgs.Reader.Read();
      ImChunkedStream outStream = new ImChunkedStream();
      WriteReadXmlHelper.ReadBase64FromCurrentXmlElement((Stream) outStream, readArgs.Reader);
      containerData.dataStream = containerData.UnpackDataStream((Stream) outStream, true);
      containerData.arcMethod = ArcMethods.NotPacked;
      if (containerData.dataSourceType != DataSourceType.ShowNET && containerData.dataStream != null && containerData.FindAcadDrawingSign(containerData.dataStream) != -1L)
        containerData.dataSourceType = DataSourceType.ShowNET;
      docNode.overrideFlags |= OverrideFlags.Data;
    }
  }

  private static void ReadImage(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    string str = (string) null;
    bool flag = false;
    if (readArgs.Reader.HasAttributes)
    {
      int i = 0;
      for (int attributeCount = readArgs.Reader.AttributeCount; i < attributeCount; ++i)
      {
        readArgs.Reader.MoveToAttribute(i);
        if (readArgs.Reader.LocalName == "refId")
          str = readArgs.Reader.Value;
        if (readArgs.Reader.LocalName == "zipped")
          flag = readArgs.Reader.Value == "true";
      }
      readArgs.Reader.MoveToElement();
    }
    ContainerData containerData = (ContainerData) docNode;
    if (!readArgs.Reader.HasValue && !readArgs.Reader.IsEmptyElement)
      readArgs.Reader.Read();
    if (readArgs.Reader.HasValue)
    {
      ImChunkedStream outStream = new ImChunkedStream();
      WriteReadXmlHelper.ReadBase64FromCurrentXmlElement((Stream) outStream, readArgs.Reader);
      Stream stream;
      if (flag)
      {
        outStream.Position = 0L;
        stream = ContainerData.UnpackStream((Stream) outStream);
        outStream.Dispose();
      }
      else
        stream = (Stream) outStream;
      stream.Position = 0L;
      containerData.image = Image.FromStream(stream);
      if (containerData.image is Metafile)
        stream.Dispose();
      docNode.overrideFlags |= OverrideFlags.Data;
    }
    if (str == null)
      return;
    if (containerData.image == null)
      containerData.image = readArgs.ObjectsId[(object) str] as Image;
    else if (!readArgs.ObjectsId.Contains((object) str))
      readArgs.ObjectsId.Add((object) str, (object) containerData.image);
    if (containerData.image != null)
      return;
    DocumentTreeNode.AddObjectReference((object) containerData, readArgs.ObjectReferences, "image", str);
  }

  private static void ReadLayers(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ContainerData) docNode).layers = new List<string>();
    WriteReadXmlHelper.ReadStringListFromXml(((ContainerData) docNode).layers, readArgs);
    docNode.overrideFlags |= OverrideFlags.ImageLayers;
  }

  private static void ReadVertAlignment(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ContainerData) docNode).vertAlignment = (VertAlignment) int.Parse(readArgs.Reader.Value);
    ((ContainerData) docNode).overrideFlags3 |= OverrideFlags3.ContainerVertAlign;
  }

  private static void ReadHorzAlignment(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ContainerData) docNode).horzAlignment = (ContainerHorzAlignment) int.Parse(readArgs.Reader.Value);
    ((ContainerData) docNode).overrideFlags3 |= OverrideFlags3.ContainerHorzAlign;
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
    if (!(src is ContainerData containerData))
      return;
    this.vertAlignment = containerData.vertAlignment;
    this.horzAlignment = containerData.horzAlignment;
    this.streamFileName = containerData.streamFileName;
    this.arcMethod = ArcMethods.NotPacked;
    if (copyData)
    {
      if (templateClone)
      {
        this.image = containerData.image;
        this.dataStream = containerData.dataStream;
        this.layers = containerData.layers;
      }
      else
      {
        if (containerData.image != null)
          this.image = (Image) containerData.image.Clone();
        if (containerData.dataStream != null)
        {
          ImChunkedStream destination = new ImChunkedStream();
          lock (containerData.dataStream)
          {
            containerData.dataStream.Position = 0L;
            containerData.dataStream.CopyTo((Stream) destination);
          }
          this.dataStream = (Stream) destination;
        }
        if (containerData.layers != null)
          this.layers = new List<string>((IEnumerable<string>) containerData.layers);
      }
      this.drawImageFailed = false;
      this.arcMethod = containerData.arcMethod;
      this.originalSize = containerData.originalSize;
      this.scaleMode = containerData.scaleMode;
      this.dataSourceType = containerData.dataSourceType;
    }
    if (containerData.reference != null & copyData)
    {
      this.reference = (ReferenceToGraphicsBase) containerData.reference.Clone();
      this.reference.AssignOwnerNode((DocumentTreeNode) this);
    }
    else
    {
      if (this.reference == null)
        return;
      this.reference.DisconnectLink();
      this.reference = (ReferenceToGraphicsBase) null;
    }
  }

  /// <summary>Метод вызывается при удалении ветки, в которой находится этот узел</summary>
  protected override void OnBranchRemoved(Removed_EventArgs e)
  {
    if (!this.IsVirtualNode && !e.RemovedByShift && this.reference != null)
      this.reference.DisconnectLink();
    base.OnBranchRemoved(e);
  }

  /// <summary>Тип данных хранимых в dataStream или image</summary>
  [Category("Debug")]
  public DataSourceType DataSourceType
  {
    [DebuggerStepThrough] get => this.dataSourceType;
  }

  /// <summary>Преобразовать в ячейку-шапку рекурсивно. Удаляет ячейки данных</summary>
  /// <param name="removeData">Удалить данные</param>
  public override void ConvertToHeader(bool removeData)
  {
  }

  /// <summary>Рисунок</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_90")]
  [CustomDescription("Attribute.Interfaces.Document_91")]
  [CustomCategory("Attribute.Interfaces.Document_92")]
  [TypeConverter(typeof (ImageContainerConverter))]
  public virtual Image Image
  {
    [DebuggerStepThrough] get
    {
      if (this.image != null)
        return this.image;
      return this.Template is ContainerData template ? template.Image : (Image) null;
    }
    set => this.SetImage(value, true, true);
  }

  /// <summary>Назначить значение Image</summary>
  /// <param name="value"></param>
  /// <param name="updateUI"></param>
  public void SetImage(Image value, bool resetDataStreamAndReference, bool updateUI)
  {
    if (this.image == value)
      return;
    if (resetDataStreamAndReference)
      this.AssignDataStream((Stream) null, DataSourceType.Image, false, false, true);
    this.drawImageFailed = false;
    this.AssignImage(value, SizeF.Empty, updateUI, updateUI, true);
  }

  /// <summary>Назначить значение свойства Image</summary>
  public virtual void AssignImage(
    Image value,
    SizeF imageSize,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if (this.Image == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Image", (object) this.Image, (object) value);
    this.image = value;
    if (setOverrideFlag)
      this.overrideFlags |= OverrideFlags.Data;
    this.originalSize = !imageSize.IsEmpty || this.image == null ? imageSize : this.GetOriginalImageSizeInMM(this.image);
    if (!this.originalSize.IsEmpty && this.scaleMode == ImageScaleMode.OriginalAutoSize)
      this.AssignProperBounds(this.Location, this.originalSize, true, false, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
    if (!updateUI)
      return;
    this.UpdateUIGeometry(true);
  }

  [Category("Debug")]
  public long DataStreamCount
  {
    get
    {
      Stream dataStream = this.GetDataStream();
      return dataStream != null ? dataStream.Length : 0L;
    }
  }

  /// <summary>Поток данных рисунка. OLE или DWG файл.
  /// Желательно устанавливать соответвующее значение DataSourceType</summary>
  [Browsable(false)]
  protected virtual Stream GetDataStream()
  {
    if (this.dataStream != null)
      return this.dataStream;
    if (this.reference != null)
      return this.reference.GetGraphicsStream();
    return this.Template is ContainerData template ? template.dataStream : this.dataStream;
  }

  public virtual Stream UnpackDataStream(Stream stream, bool closeIfPacked)
  {
    lock (stream)
    {
      if (this.ArcMethod == ArcMethods.NotPacked)
        return stream;
      if (this.ArcMethod == ArcMethods.ZLibPacked)
      {
        Stream stream1 = ContainerData.UnpackStream(stream);
        if (closeIfPacked)
          stream.Close();
        return stream1;
      }
    }
    return (Stream) null;
  }

  public static Stream UnpackStream(Stream stream)
  {
    lock (stream)
    {
      ImChunkedStream outStream = new ImChunkedStream();
      ZLibStreamHelper.UnpackStream(stream, (Stream) outStream);
      return (Stream) outStream;
    }
  }

  /// <summary>Установить значение поля</summary>
  /// <param name="fieldName">Имя поля</param>
  /// <param name="value">Значение поля</param>
  /// <returns>true, если поле найдено</returns>
  protected override bool SetFieldValue(string fieldName, object value)
  {
    if (LogManager.CreateLog)
      LogManager.AddLine($"ContainerData.SetFieldValue(fieldName:{fieldName}, value:{value})");
    bool flag = false;
    switch (fieldName)
    {
      case "Intermech.Interfaces.Document.ContainerData.image":
        if (value is byte[])
        {
          Stream stream = (Stream) new MemoryStream((byte[]) value);
          stream.Position = 0L;
          this.image = Image.FromStream(stream);
          break;
        }
        this.image = value as Image;
        break;
      case "Intermech.Interfaces.Document.ContainerData.dataStream":
        if (value is byte[])
        {
          this.dataStream = (Stream) new MemoryStream((byte[]) value);
          this.dataStream.Position = 0L;
          break;
        }
        break;
      default:
        flag = true;
        break;
    }
    return flag;
  }

  protected override bool GetFieldValue(string fieldName, out object value)
  {
    switch (fieldName)
    {
      case "Intermech.Interfaces.Document.ContainerData.image":
        if (this.image != null)
        {
          ImChunkedStream imChunkedStream = new ImChunkedStream();
          ContainerData.SaveImageToStream(this.image, (Stream) imChunkedStream);
          imChunkedStream.Position = 0L;
          value = (object) imChunkedStream.ToArray();
        }
        else
          value = (object) null;
        return true;
      case "Intermech.Interfaces.Document.ContainerData.dataStream":
        if (this.dataStream != null)
        {
          using (ImChunkedStream destination = new ImChunkedStream())
          {
            this.dataStream.Position = 0L;
            this.dataStream.CopyTo((Stream) destination);
            destination.Position = 0L;
            value = (object) destination.ToArray();
          }
        }
        else
          value = (object) null;
        return true;
      default:
        return base.GetFieldValue(fieldName, out value);
    }
  }

  /// <summary>Метод упаковки данных в DataStream</summary>
  [Browsable(false)]
  public virtual ArcMethods ArcMethod
  {
    [DebuggerStepThrough] get => this.arcMethod;
  }

  public long FindAcadDrawingSign(Stream stream)
  {
    if (stream == null || stream.Length == 0L)
      return -1;
    lock (stream)
    {
      long position = stream.Position;
      try
      {
        uint num1 = 0;
        byte[] buffer = new byte[4];
        if (stream.Length > 4L)
        {
          stream.Position = 0L;
          num1 = 0U;
          if (stream.Read(buffer, 0, 4) == 4 && (long) (uint) ((int) buffer[3] | (int) buffer[2] << 8 | (int) buffer[1] << 16 /*0x10*/ | (int) buffer[0] << 24) == (long) ContainerData.AutoCAD_SIGN)
            return 0;
        }
        if (stream.Length > 2576L)
        {
          stream.Position = 2572L;
          num1 = 0U;
          if (stream.Read(buffer, 0, 4) == 4 && (long) (uint) ((int) buffer[3] | (int) buffer[2] << 8 | (int) buffer[1] << 16 /*0x10*/ | (int) buffer[0] << 24) == (long) ContainerData.AutoCAD_SIGN)
            return 2572;
        }
        if (stream.Length > 1033L)
        {
          stream.Position = 1024L /*0x0400*/;
          uint num2 = 0;
          for (int index = 1; index <= 4; ++index)
          {
            int num3 = stream.ReadByte();
            num2 = (uint) (((int) num2 << 8) + num3);
            ++stream.Position;
          }
          if ((long) num2 == (long) ContainerData.Root_SIGN)
          {
            int num4 = -1;
            while (stream.Position < stream.Length - 4L)
            {
              if (num4 == -1)
                num4 = stream.ReadByte();
              if (num4 != -1)
              {
                if (num4 == 65)
                {
                  num4 = stream.ReadByte();
                  if (num4 != -1)
                  {
                    if (num4 == 67)
                    {
                      num4 = stream.ReadByte();
                      if (num4 != -1)
                      {
                        if (num4 == 49)
                        {
                          num4 = stream.ReadByte();
                          if (num4 != -1)
                          {
                            if (num4 == 48 /*0x30*/)
                              return stream.Position - 4L;
                          }
                          else
                            break;
                        }
                      }
                      else
                        break;
                    }
                  }
                  else
                    break;
                }
                else
                  num4 = -1;
              }
              else
                break;
            }
          }
        }
      }
      finally
      {
        stream.Position = position;
      }
    }
    return -1;
  }

  /// <summary>Назначить значение свойства DataStream</summary>
  /// <param name="value">Поток данных</param>
  /// <param name="sourceType">Тип данных</param>
  /// <param name="resetReference">Сбросить ссылку на данные</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия шаблона</param>
  public void AssignDataStream(
    Stream value,
    DataSourceType sourceType,
    bool resetReference,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    this.AssignDataStream(value, ArcMethods.NotPacked, sourceType, resetReference, updateUI, updateLayout, setOverrideFlag, false);
  }

  /// <summary>Назначить значение свойства DataStream</summary>
  /// <param name="value">Поток данных</param>
  /// <param name="sourceType">Тип данных</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия шаблона</param>
  public void AssignDataStream(
    Stream value,
    DataSourceType sourceType,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    this.AssignDataStream(value, ArcMethods.NotPacked, sourceType, true, updateUI, updateLayout, setOverrideFlag, false);
  }

  /// <summary>Назначить значение свойства DataStream</summary>
  /// <param name="value">Поток данных</param>
  /// <param name="arcMethod">Тип упаковки поданного потока данных, поток хранится в поданном виде и распаковывается по необходимости
  /// Этот метод сам не производит упаковку!</param>
  /// <param name="sourceType">Тип данных</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия шаблона</param>
  public void AssignDataStream(
    Stream value,
    ArcMethods arcMethod,
    DataSourceType sourceType,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    this.AssignDataStream(value, arcMethod, sourceType, true, updateUI, updateLayout, setOverrideFlag, false);
  }

  public void AssignFileDataStream(
    Stream value,
    string fileName,
    ArcMethods arcMethod,
    DataSourceType sourceType,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    this.streamFileName = fileName;
    this.AssignDataStream(value, arcMethod, sourceType, true, updateUI, updateLayout, setOverrideFlag, false);
  }

  /// <summary>Назначить значение свойства DataStream</summary>
  /// <param name="value">Поток данных</param>
  /// <param name="arcMethod">Тип упаковки поданного потока данных, поток хранится в поданном виде и распаковывается по необходимости
  /// Этот метод сам не производит упаковку!</param>
  /// <param name="sourceType">Тип данных</param>
  /// <param name="resetReference">Сбросить ссылку на данные</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия шаблона</param>
  /// <param name="check">Проверка на возможность вставки</param>
  public virtual void AssignDataStream(
    Stream value,
    ArcMethods arcMethod,
    DataSourceType sourceType,
    bool resetReference,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag,
    bool check)
  {
    Stream dataStream = this.GetDataStream();
    if (dataStream != value)
    {
      if (this.dataStream != null)
      {
        lock (this.dataStream)
          this.dataStream.Close();
      }
      this.dataStream = value;
      if (setOverrideFlag)
        this.overrideFlags |= OverrideFlags.Data;
      if (this.dataStream != null)
        this.AssignImage((Image) null, SizeF.Empty, false, false, false);
      if (resetReference)
        this.AssignReference((ReferenceToGraphicsBase) null, false, false, false);
      if (sourceType == DataSourceType.Unknown && this.dataStream != null && this.FindAcadDrawingSign(this.dataStream) != -1L)
        sourceType = DataSourceType.OLE;
      this.drawImageFailed = false;
      this.dataSourceType = sourceType;
      this.arcMethod = arcMethod;
      this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
      this.OnChanged(new Changed_EventArgs());
      if (updateUI)
        this.RefreshUI();
    }
    dataStream?.Close();
  }

  /// <summary>Загрузить поток в поток в памяти</summary>
  /// <param name="sourceStream">Поток источник</param>
  /// <param name="bufferSize">Размер буфера чтения</param>
  /// <returns>Возвращает поток в памяти с данными из потока источника</returns>
  public static Stream LoadToMemoryStream(Stream sourceStream, int bufferSize)
  {
    byte[] buffer = new byte[bufferSize];
    int count = bufferSize;
    long position = sourceStream.Position;
    sourceStream.Position = 0L;
    ImChunkedStream memoryStream = new ImChunkedStream();
    while (count == bufferSize)
    {
      count = sourceStream.Read(buffer, 0, bufferSize);
      if (count > 0)
        memoryStream.Write(buffer, 0, count);
    }
    sourceStream.Position = position;
    memoryStream.Position = 0L;
    return (Stream) memoryStream;
  }

  /// <summary>Имя файла, из которого по ссылке был загружен поток</summary>
  [Browsable(false)]
  public virtual string FileName
  {
    [DebuggerStepThrough] get => this.reference != null ? this.reference.FileName : (string) null;
  }

  /// <summary>Слои в ссылке</summary>
  [Browsable(false)]
  public virtual List<string> Layers
  {
    [DebuggerStepThrough] get
    {
      return this.reference != null ? this.reference.Layers : (List<string>) null;
    }
  }

  public bool FirstDrawImage
  {
    get => this.firstDrawImage;
    set => this.firstDrawImage = value;
  }

  public virtual void AssignLayers(
    List<string> value,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    if (this.Layers == value)
      return;
    if (this.reference != null)
      this.reference.Layers = value;
    else
      this.layers = value;
    if (setOverrideFlag)
      this.overrideFlags |= OverrideFlags.ImageLayers;
    this.AssignImage((Image) null, SizeF.Empty, false, false, false);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
    if (!updateUI)
      return;
    this.RefreshUI();
  }

  /// <summary>Освободить все ресурсы</summary>
  public override void Dispose()
  {
    if (this.image != null)
    {
      this.image.Dispose();
      this.image = (Image) null;
    }
    if (this.dataStream != null)
    {
      lock (this.dataStream)
        this.dataStream.Dispose();
      this.dataStream = (Stream) null;
    }
    if (this.reference != null && this.reference.ImageCache != null)
    {
      this.reference.ImageCache.Dispose();
      this.reference.ImageCache = (Image) null;
    }
    base.Dispose();
  }

  protected override bool IsAllowableLocalDataLink() => false;
}
