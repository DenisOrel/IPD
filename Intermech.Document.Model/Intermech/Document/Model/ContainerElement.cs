// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ContainerElement
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Controls;
using Intermech.Controls.OleContainer;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Show;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Контейнер OLE объектов и рисунков</summary>
[Serializable]
public class ContainerElement : ContainerData, IPageElementWithInterface
{
  public static GetShowDwgObjectDelegate GetShowDwgObject = (GetShowDwgObjectDelegate) null;
  /// <summary>Имя типа элемента</summary>
  public new static string ElementTypeName = LocalizationHolder.rm.GetString("Document.Model_165");
  [NonSerialized]
  private CancelEventHandler inplaceEditorActivating;
  [NonSerialized]
  private EventHandler inplaceEditorActivated;
  [NonSerialized]
  private CancelEventHandler inplaceEditorDeactivating;
  [NonSerialized]
  private EventHandler inplaceEditorDeactivated;
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  private static int CF_ENHMETAFILE = 14;
  [NonSerialized]
  private PageElementUI pageUI;
  [NonSerialized]
  private ImOleContainer oleContainer;

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected ContainerElement(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Конструктор</summary>
  public ContainerElement()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public ContainerElement(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <param name="visible">Видимый элемент</param>
  public ContainerElement(DocumentTreeNode parent, RectangleF bounds, bool visible)
    : base(parent, bounds, visible)
  {
  }

  public ContainerElement(RectangleElement source)
  {
    IDictionary links = (IDictionary) new HybridDictionary();
    this.CopyFields((DocumentTreeNode) source, true, true, true, false, true, links);
    this.OnDeserialization((object) this);
    this.RestoreLinks(true, false, true, links);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public new static object EmptyConstructor() => (object) new ContainerElement();

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new ContainerElement(false);
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
    return (RectangleElement) new TextBoxElement(parent, bounds, visible);
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
    return (TableData) new TableElement(isColumn, parent, bounds, visible);
  }

  static ContainerElement() => ContainerElement.InitReadFieldDict();

  public override Rectangle GetPixelBounds(DrawContext context)
  {
    return this.pageUI != null ? this.pageUI.Bounds : base.GetPixelBounds(context);
  }

  public override bool ShowFocused
  {
    get => this.pageUI != null ? this.pageUI.IsActiveElement : base.ShowFocused;
  }

  public override bool ShowSelected
  {
    get => this.pageUI != null ? this.pageUI.IsSelected : base.ShowSelected;
  }

  /// <summary>Контейнер для управления размерами и положением прямоугольного
  /// элемента управления</summary>
  [Browsable(false)]
  public PageElementUI PageUI
  {
    [DebuggerStepThrough] get => this.pageUI;
    set
    {
      if (this.pageUI == value)
        return;
      int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
      if (num == 0)
        this.SuspendUpdateGeometryRefreshUI();
      if (this.pageUI != null)
      {
        this.pageUI.Element = (PageElementNode) null;
        this.pageUI.Parent = (PageElementUI) null;
      }
      this.pageUI = value;
      if (this.pageUI != null)
      {
        this.pageUI.Element = (PageElementNode) this;
        if (this.Parent is VisualNode parent)
          parent.AddChildUI((DocumentTreeNode) this, false);
      }
      this.SetNeedUpdateUIGeometryRecursive(true, false);
      if (num != 0 || this.pageUI == null)
        return;
      this.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Наименование типа</summary>
  [TypeConverter(typeof (NodeTypeCaptionConverter))]
  [System.ComponentModel.ReadOnly(false)]
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => ContainerElement.ElementTypeName;
    set
    {
      DocumentMenuHelper.ConvertToElement(new DocumentTreeNode[1]
      {
        (DocumentTreeNode) this
      }, value);
    }
  }

  /// <summary>Событие перед активацией редактора по месту</summary>
  public event CancelEventHandler InplaceEditorActivating
  {
    add => this.inplaceEditorActivating += value;
    remove => this.inplaceEditorActivating -= value;
  }

  /// <summary>Событие после активации редактора по месту</summary>
  public event EventHandler InplaceEditorActivated
  {
    add => this.inplaceEditorActivated += value;
    remove => this.inplaceEditorActivated -= value;
  }

  /// <summary>Событие перед деактивацией редактора по месту</summary>
  public event CancelEventHandler InplaceEditorDeactivating
  {
    add => this.inplaceEditorDeactivating += value;
    remove => this.inplaceEditorDeactivating -= value;
  }

  /// <summary>Событие после деактивации редактора по месту</summary>
  public event EventHandler InplaceEditorDeactivated
  {
    add => this.inplaceEditorDeactivated += value;
    remove => this.inplaceEditorDeactivated -= value;
  }

  /// <summary>Активизировать редактор на месте</summary>
  /// <param name="pageUI">Элемент управления в контексте которого должен быть редактор</param>
  /// <param name="mouseEventArgs">Аргументы события MouseDown</param>
  public void ActivateInPlaceEditor(PageElementUI pageUI, MouseEventArgs mouseEventArgs)
  {
  }

  /// <summary>Контрол редактора по месту</summary>
  [Browsable(false)]
  public Control InPlaceEditorControl
  {
    [DebuggerStepThrough] get => (Control) null;
  }

  /// <summary>Создать соответсвующий элемент управления. Должен быть перекрыт</summary>
  public override void CreateUI()
  {
    if (!this.IsVirtualNode && this.needUI && this.pageUI == null)
    {
      if (!(this.parent is Intermech.Document.Model.Page parent2))
      {
        if (!(this.parent is IPageElementWithInterface parent1) || parent1.PageUI == null)
          return;
      }
      else if (parent2.PageUI == null)
        return;
      TableData parentCell = this.ParentCell;
      this.PageUI = parentCell == null || parentCell.IsFixedStructureArea ? (PageElementUI) new RectanglePageElementUI() : (PageElementUI) new TableCellUI();
    }
    base.CreateUI();
  }

  /// <summary>Удалить объекты интерфейса пользователя</summary>
  public override void DestroyUI()
  {
    this.PageUI = (PageElementUI) null;
    base.DestroyUI();
  }

  /// <summary>Обновить экранные координаты</summary>
  public override void UpdateUIGeometry(bool refreshUI)
  {
    if (this.SuspendedUpdateUIGeometryFlag)
      return;
    bool flag = false;
    if (this.pageUI == null && this.NeedUI)
    {
      this.CreateUI();
      flag = true;
    }
    if (this.pageUI == null)
      return;
    int num = this.SuspendedRefreshUIFlag ? 1 : 0;
    if (num == 0)
      this.SuspendRefreshUI();
    this.InvalidateUI(this.pageUI.Bounds);
    if (this.needUpdateUIGeometry && !flag)
      this.pageUI.UpdateGeometry();
    base.UpdateUIGeometry(false);
    if (num != 0)
      return;
    this.ResumeRefreshUI(refreshUI);
  }

  /// <summary>Обновить мировые координаты элемента преобразовав экранные координаты</summary>
  public override void UpdateWorldCoor()
  {
    if (this.PageUI == null)
      return;
    int num = !this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0);
    if (num == 0)
      this.SuspendUpdateGeometryRefreshUI();
    this.PageUI.UpdateElementGeometry();
    if (num != 0)
      return;
    this.ResumeUpdateRefreshUI(true, true);
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(bool force)
  {
    if (!force && this.SuspendedRefreshUIFlag || this.pageUI == null)
      return;
    if (this.page != null)
      this.page.InvalidateUI(this.pageUI.Bounds);
    this.pageUI.InvalidateUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  public override void InvalidateUI(Rectangle clipRectangle)
  {
    this.InvalidateUI(clipRectangle, false);
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public override void InvalidateUI(Rectangle clipRectangle, bool force)
  {
    if (this.SuspendedRefreshUIFlag)
      return;
    if (this.page != null)
      this.page.InvalidateUI(clipRectangle);
    if (this.pageUI == null)
      return;
    this.pageUI.InvalidateUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  public override void RefreshUI()
  {
    if (this.SuspendedRefreshUIFlag || this.page == null)
      return;
    if (this.pageUI != null)
      this.RefreshUI(this.pageUI.Bounds);
    else
      this.page.RefreshUI();
  }

  [Editor(typeof (ReferenceToGraphicsUIEditor), typeof (UITypeEditor))]
  public override ReferenceToGraphicsBase Reference
  {
    get => base.Reference;
    set => base.Reference = value;
  }

  /// <summary>Пользователь не может редактировать данные элемента</summary>
  public override bool ReadOnly => base.ReadOnly;

  /// <summary>Можно активировать редактирование по месту</summary>
  public override bool CanActivateInPlaceEditor => base.CanActivateInPlaceEditor;

  /// <summary>Создать элемент типа LabelElement, перенести туда все данные,
  /// и заменить этот элемент на новый</summary>
  public virtual void ConvertToLabel()
  {
    LabelElement child = new LabelElement((RectangleElement) this);
    DocumentTreeNode parent = this.Parent;
    VisualNode visualNode = parent as VisualNode;
    if (parent == null)
      return;
    int index = this.Index;
    bool updateUiGeometryFlag = this.SuspendedUpdateUIGeometryFlag;
    if (!updateUiGeometryFlag && visualNode != null)
      visualNode.SuspendUpdateGeometryRefreshUI();
    bool updateLayoutFlag = this.SuspendedUpdateLayoutFlag;
    if (!updateLayoutFlag)
      parent.SuspendUpdateLayout();
    try
    {
      parent.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false);
      parent.RemoveChildNodeAt(index + 1, false, false);
    }
    finally
    {
      if (!updateLayoutFlag)
        parent.ResumeUpdateLayout(false, true);
      if (!updateUiGeometryFlag && visualNode != null)
        visualNode.ResumeUpdateUIGeometry(true, true);
    }
  }

  /// <summary>Создать элемент типа TextBoxElement, перенести туда все данные,
  /// и заменить этот элемент на новый</summary>
  public virtual void ConvertToTextBox()
  {
    TextBoxElement child = new TextBoxElement((RectangleElement) this);
    DocumentTreeNode parent = this.Parent;
    VisualNode visualNode = parent as VisualNode;
    if (parent == null)
      return;
    int index = this.Index;
    bool updateUiGeometryFlag = this.SuspendedUpdateUIGeometryFlag;
    if (!updateUiGeometryFlag && visualNode != null)
      visualNode.SuspendUpdateGeometryRefreshUI();
    bool updateLayoutFlag = this.SuspendedUpdateLayoutFlag;
    if (!updateLayoutFlag)
      parent.SuspendUpdateLayout();
    try
    {
      parent.InsertChildNode(index, (DocumentTreeNode) child, false, true, false, false);
      parent.RemoveChildNodeAt(index + 1, false, false);
    }
    finally
    {
      if (!updateLayoutFlag)
        parent.ResumeUpdateLayout(false, true);
      if (!updateUiGeometryFlag && visualNode != null)
        visualNode.ResumeUpdateUIGeometry(true, true);
    }
  }

  protected virtual ImOleContainer GetOleContainerBuffer()
  {
    ImDocument ownerDocument = this.OwnerDocument as ImDocument;
    bool flag1 = !Thread.CurrentThread.IsBackground;
    bool flag2 = ownerDocument != null && ownerDocument.DocumentControl != null && !ownerDocument.DocumentControl.InvokeRequired;
    ImOleContainer oleContainerBuffer = (ImOleContainer) null;
    if (ownerDocument != null & flag2)
      oleContainerBuffer = ownerDocument.ImOleContainerBuffer;
    if (oleContainerBuffer == null)
    {
      oleContainerBuffer = new ImOleContainer();
      if (ownerDocument != null & flag2)
        oleContainerBuffer.Parent = (Control) ownerDocument.DocumentControl;
      oleContainerBuffer.Width = 100;
      oleContainerBuffer.Height = 100;
      oleContainerBuffer.CreateControl();
      ((ISupportInitialize) oleContainerBuffer).BeginInit();
      oleContainerBuffer.BackgroundImageLayout = ImageLayout.None;
      oleContainerBuffer.SizeMode = DocumentSizeMode.Zoom;
      ((ISupportInitialize) oleContainerBuffer).EndInit();
      if (ownerDocument != null & flag2)
        ownerDocument.ImOleContainerBuffer = oleContainerBuffer;
    }
    return oleContainerBuffer;
  }

  protected ImOleContainer CreateOleContainer()
  {
    ImOleContainer oleContainer = new ImOleContainer();
    if (this.pageUI != null)
    {
      oleContainer.Bounds = this.pageUI.Bounds;
      oleContainer.CreateControl();
      ((ISupportInitialize) oleContainer).BeginInit();
      oleContainer.BackgroundImageLayout = ImageLayout.None;
      if (this.scaleMode == ImageScaleMode.FitWidthHeight)
        oleContainer.SizeMode = DocumentSizeMode.Zoom;
      else if (this.scaleMode == ImageScaleMode.OriginalAutoSize || this.scaleMode == ImageScaleMode.OriginalClip)
        oleContainer.SizeMode = DocumentSizeMode.Clip;
      ((ISupportInitialize) oleContainer).EndInit();
    }
    return oleContainer;
  }

  public static Metafile SetMetafileHeader(Metafile metafile, Rectangle bounds, RectangleF frame)
  {
    if (metafile == null)
      return (Metafile) null;
    IntPtr henhmetafile = metafile.GetHenhmetafile();
    uint enhMetaFileBits1 = ContainerElement.GetEnhMetaFileBits(henhmetafile, 0U, (byte[]) null);
    byte[] numArray = new byte[(int) enhMetaFileBits1];
    int enhMetaFileBits2 = (int) ContainerElement.GetEnhMetaFileBits(henhmetafile, enhMetaFileBits1, numArray);
    ContainerElement.DeleteEnhMetaFile(henhmetafile);
    metafile.Dispose();
    ImChunkedStream metafileStream = new ImChunkedStream();
    metafileStream.Write(numArray, 0, numArray.Length);
    metafileStream.Position = 0L;
    ContainerElement.SetMetafileHeader((Stream) metafileStream, bounds, frame);
    metafileStream.Position = 0L;
    return new Metafile((Stream) metafileStream);
  }

  public static void SetMetafileHeader(Stream metafileStream, Rectangle bounds, RectangleF frame)
  {
    long position = metafileStream.Position;
    ENHMETAHEADER enhmetaheader = new ENHMETAHEADER(metafileStream);
    float num1 = 96f;
    float num2 = 96f;
    enhmetaheader.rclFrame_left = (int) Math.Round((double) enhmetaheader.rclBounds_left * 25.4 * 100.0 / (double) num1);
    enhmetaheader.rclFrame_top = (int) Math.Round((double) enhmetaheader.rclBounds_top * 25.4 * 100.0 / (double) num1);
    enhmetaheader.rclFrame_right = (int) Math.Round((double) enhmetaheader.rclBounds_right * 25.4 * 100.0 / (double) num2);
    enhmetaheader.rclFrame_bottom = (int) Math.Round((double) enhmetaheader.rclBounds_bottom * 25.4 * 100.0 / (double) num2);
    enhmetaheader.szlDevice_cx = enhmetaheader.rclBounds_right >= 10 ? (int) Math.Round((double) enhmetaheader.rclBounds_right * 100.0 * (double) enhmetaheader.szlMillimeters_cx / (double) enhmetaheader.rclFrame_right) : (int) Math.Round(1000.0 * (double) enhmetaheader.szlMillimeters_cx / (double) enhmetaheader.rclFrame_right);
    enhmetaheader.szlDevice_cy = (int) Math.Round((double) enhmetaheader.rclBounds_bottom * 100.0 * (double) enhmetaheader.szlMillimeters_cy / (double) enhmetaheader.rclFrame_bottom);
    metafileStream.Position = position;
    enhmetaheader.WriteToStream(metafileStream);
  }

  protected virtual Image CreateImageFromOLE(ImOleContainer oleContainer)
  {
    SizeF extentMm = oleContainer.GetExtentMm();
    Metafile imageFromOle = (Metafile) null;
    IntPtr dc = Intermech.Document.Model.Page.GetDC(IntPtr.Zero);
    try
    {
      imageFromOle = new Metafile(dc, EmfType.EmfOnly);
      using (Graphics graphics = Graphics.FromImage((Image) imageFromOle))
      {
        Rectangle.Empty.Size = new System.Drawing.Size(Convert.ToInt32(extentMm.Width / 25.4f * graphics.DpiX), Convert.ToInt32(extentMm.Height / 25.4f * graphics.DpiY));
        float num = 0.0f;
        PointF pointF = new PointF(graphics.DpiX, graphics.DpiY);
        RectangleF rect = new RectangleF(0.0f, 0.0f, extentMm.Width, extentMm.Height);
        Rectangle rectangle = new Rectangle((int) ((double) rect.X * ((double) pointF.X / 25.4)), (int) ((double) rect.Y * ((double) pointF.Y / 25.4)), (int) ((double) rect.Width * ((double) pointF.X / 25.4)), (int) ((double) rect.Height * ((double) pointF.Y / 25.4)));
        graphics.FillRectangle((Brush) new SolidBrush(this.BackColor), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        rect = new RectangleF(num, num, extentMm.Width - num * 2f, extentMm.Height - num * 2f);
        oleContainer.Draw(graphics, rect);
      }
    }
    finally
    {
      Intermech.Document.Model.Page.ReleaseDC(IntPtr.Zero, dc);
    }
    return (Image) imageFromOle;
  }

  /// <summary>включить указанные слои DWG</summary>
  /// <param name="layersDWG">таблица слоёв DWG</param>
  /// <param name="visibleLayers">список имён включенных слоёв</param>
  /// <returns>габариты включённых слоёв</returns>
  private RectangleD SetVisibleLayerDWG(ILayerTable layersDWG, List<string> visibleLayers)
  {
    RectangleD first = RectangleD.Empty;
    ILayer layer1 = (ILayer) null;
    foreach (ILayer layer2 in (IEnumerable) layersDWG)
    {
      if (layer2.Name.ToUpperInvariant() == "BLANK")
        layer1 = layer2;
      layer2.Visible = false;
    }
    if (visibleLayers == null)
      return first;
    bool flag = false;
    foreach (string visibleLayer in visibleLayers)
    {
      string upperInvariant1 = visibleLayer.ToUpperInvariant();
      string str1 = upperInvariant1 + "_";
      string str2 = upperInvariant1 + "_BLANK";
      foreach (ILayer layer3 in (IEnumerable) layersDWG)
      {
        string upperInvariant2 = layer3.Name.ToUpperInvariant();
        if (!(upperInvariant2 == ""))
        {
          if (upperInvariant2 == upperInvariant1)
            layer3.Visible = true;
          else if (upperInvariant2.IndexOf(str1) != -1)
          {
            if (upperInvariant2 == str2)
            {
              flag = true;
              if (first.Height == 0.0)
                first = layer3.Bound;
              first = RectangleD.Union(first, layer3.Bound);
            }
            else
              layer3.Visible = true;
          }
        }
      }
    }
    if (!flag && layer1 != null)
      first = RectangleD.Union(first, layer1.Bound);
    return first;
  }

  /// <summary>Проверить поток OLE на наличие рисунка</summary>
  /// <returns>рисунок если есть</returns>
  protected override Image CreateImageFromDataStream(bool showExceptionOnfail)
  {
    Stream stream = this.GetDataStream();
    if (stream == null || stream.Length == 0L || this.drawImageFailed)
      return this.reference != null && this.reference.ImageCache != null ? this.reference.ImageCache : (Image) null;
    stream.Position = 0L;
    if (this.ArcMethod != ArcMethods.NotPacked)
      stream = ContainerData.UnpackStream(stream);
    bool flag = this.DataSourceType == DataSourceType.OLE_Clipboard;
    if (flag)
      this.dataSourceType = DataSourceType.OLE;
    lock (stream)
    {
      try
      {
        long num1 = -1;
        if (this.DataSourceType == DataSourceType.Unknown || this.DataSourceType == DataSourceType.OLE || this.DataSourceType == DataSourceType.OLE_File || this.DataSourceType == DataSourceType.ShowNET)
          num1 = this.FindAcadDrawingSign(stream);
        if (num1 == -1L && (this.DataSourceType == DataSourceType.Unknown || this.DataSourceType == DataSourceType.Image))
        {
          try
          {
            stream.Position = 0L;
            Image imageFromDataStream = Image.FromStream(stream);
            this.dataSourceType = DataSourceType.Image;
            this.arcMethod = ArcMethods.NotPacked;
            this.originalSize = SizeF.Empty;
            this.image = imageFromDataStream;
            this.needUpdateLayoutFlag = true;
            return imageFromDataStream;
          }
          catch
          {
          }
        }
        if (num1 != -1L)
        {
          if (this.DataSourceType != DataSourceType.Unknown && this.DataSourceType != DataSourceType.ShowNET)
          {
            if (this.DataSourceType != DataSourceType.OLE)
              goto label_44;
          }
          if (ContainerElement.GetShowDwgObject != null)
          {
            byte[] numArray = new byte[stream.Length - num1];
            stream.Position = num1;
            stream.Read(numArray, 0, numArray.Length);
            IShowDwg showDwg = (IShowDwg) null;
            if (!this.drawImageFailed)
            {
              try
              {
                showDwg = ContainerElement.GetShowDwgObject(-1L, 0, !string.IsNullOrEmpty(this.FileName) ? this.FileName : "ole.dwg", numArray);
              }
              catch
              {
                this.drawImageFailed = true;
                throw;
              }
            }
            if (showDwg != null)
            {
              this.dataSourceType = DataSourceType.ShowNET;
              ILayout inFile = showDwg.Layouts.InFile;
              if (inFile != null)
              {
                RectangleD rect = RectangleD.Empty;
                if (this.Layers != null)
                  rect = this.SetVisibleLayerDWG(showDwg.Layers, this.Layers);
                else if (this.layers != null)
                  rect = this.SetVisibleLayerDWG(showDwg.Layers, this.layers);
                if (rect.Height == 0.0)
                  rect = inFile.Bounds;
                double num2 = 0.2;
                RectangleD rectangleD = RectangleD.Inflate(rect, num2, num2);
                RectangleF rectangleF = RectangleD.ToRectangleF(rectangleD);
                Metafile imageFromDataStream = (Metafile) null;
                if (!rectangleF.IsEmpty)
                {
                  IntPtr dc = Intermech.Document.Model.Page.GetDC(IntPtr.Zero);
                  try
                  {
                    imageFromDataStream = new Metafile(dc, rectangleF, MetafileFrameUnit.Millimeter, EmfType.EmfPlusDual);
                  }
                  finally
                  {
                    Intermech.Document.Model.Page.ReleaseDC(IntPtr.Zero, dc);
                  }
                  using (Graphics graphics = Graphics.FromImage((Image) imageFromDataStream))
                  {
                    MetafileHeader metafileHeader = imageFromDataStream.GetMetafileHeader();
                    float sx = metafileHeader.DpiX / graphics.DpiX;
                    float sy = metafileHeader.DpiY / graphics.DpiY;
                    graphics.ScaleTransform(sx, sy);
                    graphics.PageUnit = GraphicsUnit.Millimeter;
                    inFile.Paint(graphics, rectangleD, num2 + 0.1);
                  }
                  if (showDwg is IDisposable)
                    (showDwg as IDisposable).Dispose();
                  this.originalSize = rectangleF.Size;
                }
                this.image = (Image) imageFromDataStream;
                this.needUpdateLayoutFlag = true;
                return (Image) imageFromDataStream;
              }
            }
          }
        }
      }
      catch (OutOfMemoryException ex)
      {
        LogManager.AddLine((Exception) ex);
      }
label_44:
      if (this.DataSourceType != DataSourceType.Unknown && this.DataSourceType != DataSourceType.OLE)
      {
        if (this.DataSourceType != DataSourceType.OLE_File)
          goto label_75;
      }
      try
      {
        Image imageFromDataStream = (Image) null;
        Stream oleData = OleHelper.ExtractOleData(stream);
        if (oleData != null)
        {
          try
          {
            try
            {
              oleData.Position = 0L;
              imageFromDataStream = Image.FromStream(oleData);
            }
            catch
            {
            }
            this.originalSize = SizeF.Empty;
            this.image = imageFromDataStream;
            this.needUpdateLayoutFlag = true;
          }
          finally
          {
            if (!(this.Image is Bitmap))
              oleData.Close();
          }
        }
        if (imageFromDataStream == null)
        {
          ImOleContainer oleContainerBuffer = this.GetOleContainerBuffer();
          stream.Position = 0L;
          this.SetupOleContainerStream(oleContainerBuffer, stream);
          SizeF extentMm = oleContainerBuffer.GetExtentMm();
          try
          {
            if (flag && oleContainerBuffer.DocumentClassName == "Paint.Picture")
            {
              if (Clipboard.ContainsImage())
                imageFromDataStream = Clipboard.GetImage();
            }
            else
              imageFromDataStream = this.CreateImageFromOLE(oleContainerBuffer);
          }
          finally
          {
            oleContainerBuffer.SourceData = (Stream) null;
            if (oleContainerBuffer.Parent == null)
              oleContainerBuffer.Dispose();
          }
          this.originalSize = extentMm;
          this.image = imageFromDataStream;
          this.needUpdateLayoutFlag = true;
        }
        return imageFromDataStream;
      }
      catch (Exception ex)
      {
        Exception exception = ex;
        this.drawImageFailed = true;
        if (showExceptionOnfail)
        {
          if (exception is COMException)
            exception = new Exception("Формат файла не поддерживается", exception);
          ImDocumentData.ShowException(exception, LocalizationHolder.rm.GetString("Document.Model_617"));
        }
        else
        {
          if (this.DataSourceType != DataSourceType.Unknown || this.streamFileName == null)
            throw exception;
          this.drawImageFailed = false;
          ImChunkedStream dstStream = new ImChunkedStream();
          this.WriteFileToStreamWithFileName(this.streamFileName, (Stream) dstStream, stream);
          dstStream.Position = 0L;
          this.AssignDataStream((Stream) dstStream, DataSourceType.OLE_File, true, false, false, true);
          return this.CreateImageFromDataStream(false);
        }
      }
    }
label_75:
    return (Image) null;
  }

  /// <summary>Установить поток данных для OLE Container</summary>
  /// <param name="oleContainer">OLE Container</param>
  /// <param name="stream">Поток данных OLE</param>
  protected void SetupOleContainerStream(ImOleContainer oleContainer, Stream stream)
  {
    stream.Position = 0L;
    if (this.dataSourceType == DataSourceType.OLE_File)
    {
      string fileNameToTempFile = this.ExtractStreamWithFileNameToTempFile(stream);
      try
      {
        oleContainer.LoadFrom(fileNameToTempFile);
      }
      finally
      {
        if (File.Exists(fileNameToTempFile))
          File.Delete(fileNameToTempFile);
      }
    }
    else
      oleContainer.SourceData = stream;
  }

  /// <summary>Загрузить имя файла хранящееся вначале потока.
  /// Позиция в потоке остаётся за именем файла, в начале собственно потока данных OLE</summary>
  /// <param name="stream">Поток данных с именем файла в начале</param>
  /// <returns></returns>
  protected string ReadFileNameFromStartOfStream(Stream stream)
  {
    using (BinaryReader binaryReader = new BinaryReader(stream, (Encoding) new UTF8Encoding(), true))
      return binaryReader.ReadString();
  }

  /// <summary>Сохранить имя файла в вначале потока.
  /// Позиция в потоке остаётся за именем файла, в начале собственно потока данных OLE</summary>
  /// <param name="stream">Поток данных с именем файла в начале</param>
  /// <returns></returns>
  protected void WriteFileToStreamWithFileName(
    string fileName,
    Stream dstStream,
    Stream sourceStream)
  {
    Stream scrStream = sourceStream != null ? sourceStream : (Stream) new FileStream(fileName, FileMode.Open);
    scrStream.Position = 0L;
    this.WriteStreamWithFileName(scrStream, fileName, dstStream);
    if (sourceStream != null)
      return;
    scrStream.Dispose();
  }

  /// <summary>Переписать поток в другой поток, сохранив имя файла в начале</summary>
  /// <param name="scrStream">Исходный поток</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="dstStream">Поток приёмник</param>
  protected void WriteStreamWithFileName(Stream scrStream, string fileName, Stream dstStream)
  {
    using (BinaryWriter binaryWriter = new BinaryWriter(dstStream, (Encoding) new UTF8Encoding(), true))
      binaryWriter.Write(fileName);
    scrStream.CopyTo(dstStream);
  }

  /// <summary>Извлечь поток с именем файла в начале на диск во временный файл</summary>
  /// <param name="stream">Поток</param>
  /// <returns>Возвращает полное имя временного файла</returns>
  protected string ExtractStreamWithFileNameToTempFile(Stream stream)
  {
    string tempFileName = Path.GetTempFileName();
    string str = Path.GetExtension(this.ReadFileNameFromStartOfStream(stream));
    string path = $"{Path.GetDirectoryName(tempFileName)}\\{Path.GetFileNameWithoutExtension(tempFileName)}{str}";
    using (FileStream destination = new FileStream(path, FileMode.Create))
      stream.CopyTo((Stream) destination);
    return path;
  }

  public override void AssignImage(
    Image value,
    SizeF imageSize,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag)
  {
    base.AssignImage(value, imageSize, updateUI, updateLayout, setOverrideFlag);
    this.drawImageFailed = false;
  }

  public override void AssignDataStream(
    Stream value,
    ArcMethods arcMethod,
    DataSourceType sourceType,
    bool resetReference,
    bool updateUI,
    bool updateLayout,
    bool setOverrideFlag,
    bool check)
  {
    Stream dataStream = this.dataStream;
    this.drawImageFailed = false;
    DataSourceType dataSourceType = this.dataSourceType;
    ArcMethods arcMethod1 = this.arcMethod;
    base.AssignDataStream(value, arcMethod, sourceType, resetReference, false, updateLayout, setOverrideFlag, check);
    if (check)
    {
      if (value != null)
      {
        try
        {
          this.CreateImageFromDataStream(false);
        }
        catch (Exception ex)
        {
          Exception exception = ex;
          if (exception is COMException)
            exception = new Exception("Формат файла не поддерживается", exception);
          ImDocumentData.ShowException(exception, LocalizationHolder.rm.GetString("Document.Model_617"));
          this.dataStream = dataStream;
          this.dataSourceType = dataSourceType;
          arcMethod = arcMethod1;
        }
      }
    }
    if (!updateUI)
      return;
    this.RefreshUI();
  }

  /// <summary>пОлучить координаты элемента в пикселях на экране</summary>
  /// <param name="context"></param>
  /// <param name="propBounds"></param>
  /// <returns></returns>
  internal virtual Rectangle GetPixelBounds(DrawContext context, RectangleF propBounds)
  {
    if (this.Page is Intermech.Document.Model.Page page && page.PageUI != null)
      return page.PageUI.ConvertWorldToPixel(this.ClientBounds);
    Matrix matrix = (Matrix) null;
    PointF dpi = new PointF(96f, 96f);
    if (context != null)
    {
      matrix = context.TransformMatrix.Matrix;
      dpi = context.DisplayDPI;
    }
    Rectangle pixelBounds = Rectangle.Empty;
    if (matrix != null)
      pixelBounds = UnitsConverter.ConvertWorldToPixel(propBounds, matrix, dpi);
    return pixelBounds;
  }

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
    RectangleF rectangleF1 = this.ProperBounds;
    TableData parentCell = this.ParentCell;
    if (parentCell != null && parentCell.IsFixedStructureArea)
      rectangleF1 = this.Bounds;
    bool flag1 = parentCell != null && ((double) this.SkipCellsBefore >= 1.0 || (double) this.SkipCellsAfter >= 1.0);
    if (!(!flag1 ? rectangleF1 : this.Bounds).IntersectsWith(context.ClipRectangle))
      return;
    bool? isSelected = context.IsSelected;
    bool? isFocused = context.IsFocused;
    if (context.IsPaint && (!context.IsSelected.HasValue || !context.IsSelected.Value))
      context.IsSelected = new bool?(this.ShowSelected);
    if (context.IsPaint && context.IsSelected.Value && !context.IsFocused.HasValue)
      context.IsFocused = parentCell == null || !parentCell.IsColumn ? new bool?(this.ShowFocused) : new bool?(false);
    GraphicsUnit pageUnit = context.Graphics.PageUnit;
    Region clip = context.Graphics.Clip;
    GraphicsState gstate1 = context.Graphics.Save();
    RectangleBorder borders = context.Borders;
    context.Borders = (RectangleBorder) null;
    try
    {
      context.Graphics.PageUnit = GraphicsUnit.Millimeter;
      this.DrawBackground(context, rectangleF1);
      if (!context.WithoutData && context.Layer == 0)
      {
        GraphicsState gstate2 = context.Graphics.Save();
        try
        {
          if (this.image == null || this.FirstDrawImage && this.DataSourceType == DataSourceType.ShowNET)
          {
            this.FirstDrawImage = false;
            Image image = this.image;
            try
            {
              this.image = this.CreateImageFromDataStream(false);
              if (this.image != null)
              {
                if (this.reference != null)
                  this.reference.ImageCache = this.image;
                this.needUpdateLayoutFlag = true;
                image?.Dispose();
              }
              else
                this.image = image;
            }
            catch (Exception ex)
            {
              this.image = image;
              LogManager.AddLine(ex);
            }
          }
          if (this.image == null)
          {
            if (!this.drawImageFailed)
              goto label_132;
          }
          bool flag2 = context.IsPaint && this.image is Bitmap;
          RectangleF rectangleF2 = rectangleF1;
          if (flag2)
          {
            rectangleF1 = (RectangleF) this.GetPixelBounds(context, rectangleF1);
            context.Graphics.PageUnit = GraphicsUnit.Pixel;
            context.Graphics.ResetTransform();
          }
          Image image1 = (Image) null;
          try
          {
            Image image2 = this.image;
            Image image3 = this.image;
            if (this.drawImageFailed)
            {
              Bitmap bitmap = new Bitmap((int) ((double) this.Bounds.Width * 20.0), (int) ((double) this.Bounds.Height * 20.0));
              using (Graphics graphics = Graphics.FromImage((Image) bitmap))
              {
                graphics.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawLine(new Pen(Color.Red, 3f), 0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawLine(new Pen(Color.Red, 3f), bitmap.Width, 0, 0, bitmap.Height);
              }
              image2 = (Image) bitmap;
              image3 = (Image) bitmap;
            }
            if (context.IsPdf && image3 is Metafile)
            {
              double num1 = 10.0;
              int width1 = image3.Width;
              int height1 = image3.Height;
              if (image3.Width < 200 && image3.Height < 200)
              {
                width1 = (int) ((double) width1 * num1);
                height1 = (int) ((double) height1 * num1);
              }
              double width2 = (double) image3.Width;
              RectangleF bounds = this.Bounds;
              double width3 = (double) bounds.Width;
              if (width2 / width3 >= 5.0)
              {
                double height2 = (double) image3.Height;
                bounds = this.Bounds;
                double height3 = (double) bounds.Height;
                if (height2 / height3 >= 5.0)
                  goto label_40;
              }
              width1 = (int) ((double) width1 * num1);
              height1 = (int) ((double) height1 * num1);
label_40:
              int num2 = 3000;
              bool flag3 = width1 > height1;
              if (flag3 && width1 > num2 || !flag3 && height1 > num2)
              {
                double num3 = !flag3 ? (double) num2 / (double) height1 : (double) num2 / (double) width1;
                width1 = (int) ((double) width1 * num3);
                height1 = (int) ((double) height1 * num3);
              }
              image2 = (Image) new Bitmap(width1, height1);
              image1 = image2;
              using (Graphics graphics = Graphics.FromImage(image2))
              {
                graphics.Clear(this.BackColor);
                graphics.DrawImage(image3, new Rectangle(0, 0, image2.Width, image2.Height));
              }
            }
            if (context.IsMetafile && image3 is Bitmap)
            {
              int width = image3.Width;
              int height = image3.Height;
              bool flag4 = width > height;
              int num4 = 3000;
              if (flag4 && width > num4 || !flag4 && height > num4)
              {
                double num5 = !flag4 ? (double) num4 / (double) height : (double) num4 / (double) width;
                width = (int) ((double) width * num5);
                height = (int) ((double) height * num5);
              }
              Bitmap bitmap = new Bitmap(width, height);
              using (Graphics graphics = Graphics.FromImage((Image) bitmap))
                graphics.DrawImage(image3, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
              image2 = (Image) bitmap;
            }
            RectangleF rect = rectangleF1;
            bool flag5 = context.IsPaint && context.IsSelected.Value && !context.IsFocused.Value;
            RectangleF rectangleF3 = !(image2 is Metafile metafile) ? new RectangleF(0.0f, 0.0f, (float) image3.Width, (float) image3.Height) : (RectangleF) metafile.GetMetafileHeader().Bounds;
            bool flag6 = this.scaleMode == ImageScaleMode.FitWidthHeight;
            if (!flag6)
            {
              PointF dpi = new PointF(image3.HorizontalResolution, image3.VerticalResolution);
              if (image2 is Metafile)
              {
                if (Math.Abs((double) dpi.X - 101.6) < 1.0)
                  dpi.X = 96f;
                if (Math.Abs((double) dpi.Y - 101.6) < 1.0)
                  dpi.Y = 96f;
              }
              if (this.originalSize != SizeF.Empty && (this.DataSourceType == DataSourceType.OLE || this.DataSourceType == DataSourceType.OLE_File || this.DataSourceType == DataSourceType.ShowNET))
              {
                if (flag2)
                {
                  if (this.Page is Intermech.Document.Model.Page page1 && page1.PageUI != null)
                  {
                    System.Drawing.Size pixel = page1.ConvertMmToPixel(this.originalSize);
                    rect.Size = new SizeF((float) (pixel.Width - 2), (float) (pixel.Height - 2));
                  }
                }
                else
                  rect.Size = this.originalSize;
              }
              else if (flag2)
              {
                if (this.Page is Intermech.Document.Model.Page page2 && page2.PageUI != null)
                {
                  PointF pointF1 = page2.PageUI.TransformMatrix.TransformPoint(new PointF(0.0f, 0.0f));
                  PointF pointF2 = page2.PageUI.TransformMatrix.TransformPoint(new PointF(rectangleF3.Width, rectangleF3.Height));
                  rect.Size = new SizeF((float) (int) Math.Round((double) pointF2.X - (double) pointF1.X), (float) (int) Math.Round((double) pointF2.Y - (double) pointF1.Y));
                }
              }
              else
              {
                RectangleF mm = UnitsConverter.PixelsToMm(rectangleF3, dpi);
                rect.Size = mm.Size;
              }
              if ((double) rect.Width < (double) rectangleF1.Width)
                rect.X += (float) (((double) rectangleF1.Width - (double) rect.Width) / 2.0);
              else if ((double) rect.Width > (double) rectangleF1.Width)
                flag6 = this.scaleMode == ImageScaleMode.OriginalAutoSize;
              if ((double) rect.Height < (double) rectangleF1.Height)
                rect.Y += (float) (((double) rectangleF1.Height - (double) rect.Height) / 2.0);
              else if ((double) rect.Height > (double) rectangleF1.Height)
                flag6 = this.scaleMode == ImageScaleMode.OriginalAutoSize;
              if (flag2)
              {
                rect.X = (float) (int) rect.X;
                rect.Y = (float) (int) rect.Y;
              }
              if (!flag6)
              {
                context.Graphics.SetClip(rectangleF1);
                if (flag5)
                  context.Graphics.DrawImage(image2, new PointF[3]
                  {
                    rect.Location,
                    new PointF(rect.Right, rect.Top),
                    new PointF(rect.Left, rect.Bottom)
                  }, rectangleF3, GraphicsUnit.Pixel, VisualNode.NegativeImageAttributes);
                else if (image2 is Metafile)
                {
                  if (context.IsPdf)
                    context.Graphics.DrawImage(image2, rect);
                  else
                    context.Graphics.DrawImage(image2, new PointF[3]
                    {
                      rect.Location,
                      new PointF(rect.Right, rect.Top),
                      new PointF(rect.Left, rect.Bottom)
                    }, rectangleF3, GraphicsUnit.Pixel);
                }
                else if (context.IsPdf)
                  context.Graphics.DrawImage(image2, rect);
                else if (flag2)
                  context.Graphics.DrawImage(image2, rect);
                else
                  context.Graphics.DrawImage(image2, rect);
                if (ImDocumentData.ShowDebugInfo)
                {
                  using (Pen pen = new Pen(RectangleElement.InvisibleLineColor, PageElementNode.DefaultLineWidth))
                    context.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                }
                context.Graphics.SetClip(clip, CombineMode.Replace);
              }
            }
            if (flag6)
            {
              if (image2.Height > 0)
              {
                if (image2.Width > 0)
                {
                  rect = rectangleF1;
                  if ((double) rectangleF3.Height * (double) rect.Width > (double) rectangleF3.Width * (double) rect.Height)
                  {
                    rect.Width = rect.Height * (rectangleF3.Width / rectangleF3.Height);
                    if (this.horzAlignment == ContainerHorzAlignment.Center)
                      rect.X += (float) (((double) rectangleF1.Width - (double) rect.Width) / 2.0);
                    else if (this.horzAlignment == ContainerHorzAlignment.Left)
                      rect.X = rectangleF1.X;
                    else if (this.horzAlignment == ContainerHorzAlignment.Right)
                      rect.X += rectangleF1.Width - rect.Width;
                  }
                  else
                  {
                    rect.Height = rect.Width * (rectangleF3.Height / rectangleF3.Width);
                    if (this.vertAlignment == VertAlignment.Center)
                      rect.Y += (float) (((double) rectangleF1.Height - (double) rect.Height) / 2.0);
                    else if (this.vertAlignment == VertAlignment.Top)
                      rect.Y = rectangleF1.Y;
                    else if (this.vertAlignment == VertAlignment.Bottom)
                      rect.Y += rectangleF1.Height - rect.Height;
                  }
                  if (flag5)
                    context.Graphics.DrawImage(image2, new PointF[3]
                    {
                      rect.Location,
                      new PointF(rect.Right, rect.Top),
                      new PointF(rect.Left, rect.Bottom)
                    }, rectangleF3, GraphicsUnit.Pixel, VisualNode.NegativeImageAttributes);
                  else
                    context.Graphics.DrawImage(image2, rect);
                  if (ImDocumentData.ShowDebugInfo)
                  {
                    using (Pen pen = new Pen(RectangleElement.InvisibleLineColor, PageElementNode.DefaultLineWidth))
                      context.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            this.drawImageFailed = true;
            string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
            ImDocumentData.ShowException(ex, errorFormCaption);
          }
          finally
          {
            image1?.Dispose();
            if (flag2)
              rectangleF1 = rectangleF2;
          }
        }
        finally
        {
          context.Graphics.Restore(gstate2);
        }
      }
label_132:
      RowColParams gridRow = (RowColParams) null;
      if (gridRows != null && rowIndex >= 0 && rowIndex < gridRows.Count)
        gridRow = gridRows[rowIndex];
      RowColParams gridCol = (RowColParams) null;
      if (gridCols != null && colIndex >= 0 && colIndex < gridCols.Count)
        gridCol = gridCols[colIndex];
      if (context.IsSkipedSpace)
        rectangleF1.Height = context.SkipedSpaceSize;
      if (this.drawEllipse)
        this.DrawEllipseBounds(context, rectangleF1, gridCol, gridRow, findGridParams);
      else
        this.DrawFrame(context, rectangleF1, gridCol, gridRow, findGridParams);
      if (!(!context.WithoutData & flag1))
        return;
      this.DrawSkipedSpace(context, gridCols, colIndex, gridRows, rowIndex, findGridParams);
    }
    finally
    {
      context.Graphics.PageUnit = pageUnit;
      context.IsSelected = isSelected;
      context.IsFocused = isFocused;
      context.Graphics.Restore(gstate1);
      context.Borders = borders;
    }
  }

  public override bool CanCallEditor
  {
    get
    {
      if (this.ReadOnly)
        return false;
      if (base.CanCallEditor)
        return true;
      return (this.dataSourceType == DataSourceType.OLE || this.dataSourceType == DataSourceType.OLE_File) && this.dataStream != null && this.dataStream.Length > 0L;
    }
  }

  public override void CallEditor()
  {
    if (this.ReadOnly)
      return;
    if (this.dataSourceType == DataSourceType.OLE)
    {
      try
      {
        if (this.oleContainer != null)
        {
          if (this.oleContainer.ActivationState == ActivationState.Active)
            this.oleContainer.Deactivate();
          this.oleContainer.Deactivated -= new EventHandler(this.oleContainer_Closed);
          this.oleContainer.DocumentModified -= new EventHandler(this.oleContainer_DocumentModified);
          this.oleContainer.Saved -= new EventHandler(this.oleContainer_Saved);
          this.oleContainer.Closed -= new EventHandler(this.oleContainer_Closed);
          this.oleContainer.SourceData = (Stream) null;
          this.oleContainer.Parent = (Control) null;
          this.oleContainer.Dispose();
        }
        this.oleContainer = this.CreateOleContainer();
        this.dataStream.Position = 0L;
        if (this.pageUI != null)
        {
          this.oleContainer.Bounds = this.pageUI.Bounds;
          this.SetupOleContainerStream(this.oleContainer, this.dataStream);
          this.oleContainer.Deactivated += new EventHandler(this.oleContainer_Closed);
          this.oleContainer.DocumentModified += new EventHandler(this.oleContainer_DocumentModified);
          this.oleContainer.Saved += new EventHandler(this.oleContainer_Saved);
          this.oleContainer.Closed += new EventHandler(this.oleContainer_Closed);
          this.oleContainer.Activate();
        }
      }
      catch (Exception ex)
      {
        LogManager.AddLine(ex);
      }
    }
    if (this.dataSourceType != DataSourceType.OLE_File)
      return;
    this.dataStream.Position = 0L;
    FilesEditor.Instance.EditFile((DocumentTreeNode) this, this.dataStream, this.ReadFileNameFromStartOfStream(this.dataStream));
  }

  private void oleContainer_Closed(object sender, EventArgs e)
  {
    if (this.oleContainer == null)
      return;
    this.oleContainer.Deactivated -= new EventHandler(this.oleContainer_Closed);
    this.oleContainer.DocumentModified -= new EventHandler(this.oleContainer_DocumentModified);
    this.oleContainer.Saved -= new EventHandler(this.oleContainer_Saved);
    this.oleContainer.Closed -= new EventHandler(this.oleContainer_Closed);
    this.oleContainer.SourceData = (Stream) null;
    this.oleContainer.Parent = (Control) null;
    this.oleContainer = (ImOleContainer) null;
  }

  private void oleContainer_Saved(object sender, EventArgs e)
  {
    if (this.oleContainer == null || this.oleContainer.IsDocumentDataDirty || this.DataSourceType == DataSourceType.OLE_File)
      return;
    SizeF extentMm = this.oleContainer.GetExtentMm();
    Stream sourceData = this.oleContainer.SourceData;
    if (sourceData == null || sourceData.Length <= 0L)
      return;
    sourceData.Position = 0L;
    this.AssignDataStream(ContainerData.LoadToMemoryStream(sourceData, 65536 /*0x010000*/), DataSourceType.OLE, false, false, false, true);
    this.originalSize = extentMm;
    this.AssignImage(this.CreateImageFromOLE(this.oleContainer), extentMm, false, false, true);
    this.UpdateLayout(true);
    if (this.ParentCell == null)
      return;
    this.RefreshUI();
  }

  private void oleContainer_DocumentModified(object sender, EventArgs e)
  {
  }

  [DllImport("Gdi32.dll")]
  public new static extern uint GetEnhMetaFileBits(IntPtr hemf, uint cbBuffer, byte[] lpbBuffer);

  [DllImport("Gdi32.dll")]
  public new static extern bool DeleteEnhMetaFile(IntPtr hemf);

  public static string GetDefaultExtension(Image image)
  {
    if (image == null)
      throw new ArgumentNullException(nameof (image));
    if (image is Metafile)
      return "*.emf";
    ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
    ImageFormat rawFormat = image.RawFormat;
    ImageCodecInfo imageCodecInfo1 = (ImageCodecInfo) null;
    Guid guid1 = ImageFormat.Png.Guid;
    Guid guid2 = rawFormat.Guid;
    foreach (ImageCodecInfo imageCodecInfo2 in imageEncoders)
    {
      Guid formatId = imageCodecInfo2.FormatID;
      if (formatId.Equals(guid2))
      {
        imageCodecInfo1 = imageCodecInfo2;
        break;
      }
      if (imageCodecInfo1 == null)
      {
        formatId = imageCodecInfo2.FormatID;
        if (formatId.Equals(guid1))
          imageCodecInfo1 = imageCodecInfo2;
      }
    }
    return imageCodecInfo1.FilenameExtension;
  }

  /// <summary>Пересоздать метафайл. Внимание! Оригинал портится и ему похоже не нужен даже Dispose</summary>
  /// <param name="metafile">Оригинал</param>
  /// <returns>Пересозданный методами GDI32 API метафайла</returns>
  public static Metafile RecreateMetafile(Metafile metafile)
  {
    return new Metafile(ContainerElement.CopyEnhMetaFile(metafile.GetHenhmetafile(), IntPtr.Zero), true);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (ContainerElement.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      ContainerElement.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    return base.ReadFieldFromXml(readArgs);
  }

  private static void InitReadFieldDict()
  {
    ContainerElement.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) ContainerData.ReadFieldsDict);
  }

  /// <summary>Преобразовать в ячейку-шапку рекурсивно. Удаляет ячейки данных</summary>
  /// <param name="removeData">Удалить данные</param>
  public override void ConvertToHeader(bool removeData)
  {
  }

  public virtual void CreateOleObject()
  {
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_567"));
    try
    {
      ImOleContainer oleContainerBuffer = this.GetOleContainerBuffer();
      if (!oleContainerBuffer.CallInsertDlg())
        return;
      string sourceDocument = oleContainerBuffer.SourceDocument;
      SizeF extentMm = oleContainerBuffer.GetExtentMm();
      Stream sourceData = oleContainerBuffer.SourceData;
      if (sourceData == null || sourceData.Length <= 0L)
        return;
      Stream memoryStream = ContainerData.LoadToMemoryStream(sourceData, 65536 /*0x010000*/);
      oleContainerBuffer.SourceData = (Stream) null;
      memoryStream.Position = 0L;
      this.AssignDataStream(memoryStream, DataSourceType.OLE, true, false, false, true);
      this.originalSize = extentMm;
      try
      {
        Image imageFromDataStream = this.CreateImageFromDataStream(false);
        if (imageFromDataStream.Width == 0)
        {
          if (imageFromDataStream.Height == 0)
            throw new Exception("OLE не имеет рисунка, пробуем создать рисунок из файла");
        }
      }
      catch
      {
        if (!string.IsNullOrEmpty(sourceDocument))
        {
          if (File.Exists(sourceDocument))
          {
            Stream dstStream = (Stream) new ImChunkedStream();
            this.WriteFileToStreamWithFileName(sourceDocument, dstStream, (Stream) null);
            dstStream.Position = 0L;
            this.AssignDataStream(dstStream, DataSourceType.OLE_File, true, false, false, true);
            this.CreateImageFromDataStream(true);
          }
        }
      }
      this.UpdateLayout(true);
      if (this.ParentCell == null)
        return;
      this.RefreshUI();
    }
    finally
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Загрузить данные из файла</summary>
  /// <param name="fileName">Имя файла</param>
  public virtual void LoadDataObjectFromFile(string fileName)
  {
    if (!File.Exists(fileName))
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Document.Model_166"), (object) fileName));
    DataSourceType sourceType1 = DataSourceType.Unknown;
    try
    {
      string str1 = Path.GetExtension(fileName);
      if (str1 != null && str1 != "" && ContainerElement.GetShowDwgObject != null)
      {
        string lower = str1.ToLower();
        if (lower == ".dwg" || lower == ".dxf" || lower == ".sld" || lower == ".slb")
        {
          byte[] numArray;
          using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
          {
            numArray = new byte[fileStream.Length];
            fileStream.Read(numArray, 0, numArray.Length);
          }
          IShowDwg showDwg = ContainerElement.GetShowDwgObject(-1L, 0, fileName, numArray);
          if (showDwg != null)
          {
            ILayout inFile = showDwg.Layouts.InFile;
            if (inFile != null)
            {
              RectangleD bounds = inFile.Bounds;
              RectangleF mm = new RectangleF((float) bounds.X, (float) bounds.Y, (float) bounds.Width, (float) bounds.Height);
              Rectangle empty = Rectangle.Empty;
              IntPtr dc = Intermech.Document.Model.Page.GetDC(IntPtr.Zero);
              try
              {
                using (Graphics graphics = Graphics.FromImage((Image) new Metafile(dc, EmfType.EmfOnly)))
                {
                  graphics.PageUnit = GraphicsUnit.Pixel;
                  UnitsConverter.MmToPixels(mm, new PointF(graphics.DpiX, graphics.DpiY));
                  inFile.Paint(graphics);
                }
              }
              finally
              {
                Intermech.Document.Model.Page.ReleaseDC(IntPtr.Zero, dc);
              }
              sourceType1 = DataSourceType.ShowNET;
              this.AssignDataStream((Stream) new MemoryStream(numArray), DataSourceType.ShowNET, true, false, false, true);
              this.CreateImageFromDataStream(true);
            }
          }
        }
      }
      if (sourceType1 == DataSourceType.Unknown)
      {
        string str2 = Path.GetExtension(fileName);
        Image image = (Image) null;
        if (str2 == ".jpg" || str2 == ".jpeg")
        {
          using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
          {
            ImChunkedStream destination = new ImChunkedStream();
            fileStream.CopyTo((Stream) destination);
            destination.Position = 0L;
            image = Image.FromStream((Stream) destination, true, false);
          }
        }
        else
          image = Image.FromFile(fileName, true);
        sourceType1 = DataSourceType.Image;
        this.AssignReference((ReferenceToGraphicsBase) null, false, false, false);
        this.AssignDataStream((Stream) null, sourceType1, true, false, false, true);
        this.AssignImage(image, SizeF.Empty, true, true, true);
      }
    }
    catch (OutOfMemoryException ex)
    {
      sourceType1 = DataSourceType.Unknown;
    }
    if (sourceType1 != DataSourceType.Unknown)
      return;
    try
    {
      if (fileName != null && File.Exists(fileName) && ImDocumentEditorConfig.Instance.EditOleAsFiles)
      {
        this.LoadOleFromFile(fileName);
      }
      else
      {
        try
        {
          ImOleContainer oleContainerBuffer = this.GetOleContainerBuffer();
          oleContainerBuffer.LoadFrom(fileName);
          SizeF extentMm = oleContainerBuffer.GetExtentMm();
          Stream sourceData = oleContainerBuffer.SourceData;
          if (sourceData == null || sourceData.Length <= 0L)
            return;
          Stream memoryStream = ContainerData.LoadToMemoryStream(sourceData, 65536 /*0x010000*/);
          oleContainerBuffer.SourceData = (Stream) null;
          memoryStream.Position = 0L;
          DataSourceType sourceType2 = DataSourceType.OLE;
          this.AssignDataStream(memoryStream, sourceType2, true, false, false, true);
          this.originalSize = extentMm;
          this.CreateImageFromDataStream(false);
          this.UpdateLayout(true);
        }
        catch (Exception ex)
        {
          if (fileName == null || !File.Exists(fileName))
            throw ex;
          this.LoadOleFromFile(fileName);
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Document.Model_168"), (object) Environment.NewLine, (object) ex.Message));
    }
  }

  /// <summary>Проверить помещается ли изображение в оригинальном размере и запросить у пользователя режим отображения</summary>
  public void CheckOriginalSizeAndAskUser()
  {
    if (!VisualNode.LessWithMiscalculation(this.ProperBounds.Width, this.originalSize.Width) && !VisualNode.LessWithMiscalculation(this.ProperBounds.Height, this.originalSize.Height))
      return;
    string FormCaption = "Вставка изображения";
    string Message = $"Оригинальные размеры изображения больше чем размеры контейнера:\r\n{$"Размеры изображения: ширина {this.originalSize.Width} мм, высота {this.originalSize.Height} мм\r\n"}{$"Размеры контейнера: ширина {this.ProperBounds.Width} мм, высота {this.ProperBounds.Height} мм"}";
    if (this.ScaleMode == ImageScaleMode.OriginalAutoSize)
    {
      if (IMMessageBox.Show(FormCaption, Message, new IMMessageBoxButton[2]
      {
        new IMMessageBoxButton("Уменьшать изображение", DialogResult.Ignore),
        new IMMessageBoxButton("Обрезать изображение", DialogResult.Yes)
      }, IMMessageBoxImage.Warning) != DialogResult.Yes)
        return;
      this.AssignScaleMode(ImageScaleMode.OriginalClip, true, true, true);
    }
    else
    {
      if (this.ScaleMode != ImageScaleMode.OriginalClip)
        return;
      if (IMMessageBox.Show(FormCaption, Message, new IMMessageBoxButton[2]
      {
        new IMMessageBoxButton("Уменьшать изображение", DialogResult.Yes),
        new IMMessageBoxButton("Обрезать изображение", DialogResult.Ignore)
      }, IMMessageBoxImage.Warning) != DialogResult.Yes)
        return;
      this.AssignScaleMode(ImageScaleMode.OriginalAutoSize, true, true, true);
    }
  }

  private void LoadOleFromFile(string fileName)
  {
    ImChunkedStream dstStream = new ImChunkedStream();
    this.WriteFileToStreamWithFileName(fileName, (Stream) dstStream, (Stream) null);
    dstStream.Position = 0L;
    this.AssignDataStream((Stream) dstStream, DataSourceType.OLE_File, true, false, false, true);
    this.CreateImageFromDataStream(true);
    this.UpdateLayout(true);
  }

  /// <summary>Получить оригинальный размер изображения у showDwg</summary>
  /// <param name="showDwg">Экземпляр ShowDwg</param>
  /// <returns></returns>
  public SizeF GetOriginalImageSizeInMM(IShowDwg showDwg)
  {
    SizeF originalImageSizeInMm = SizeF.Empty;
    if (showDwg != null)
    {
      RectangleD bounds = showDwg.Layouts.InFile.Bounds;
      originalImageSizeInMm = new SizeF((float) bounds.Width, (float) bounds.Height);
    }
    return originalImageSizeInMm;
  }

  /// <summary>Получить оригинальный размер изображения у showDwg</summary>
  /// <param name="oleObject">Экземпляр OLE Container</param>
  public SizeF GetOriginalImageSizeInMM(ImOleContainer oleObject)
  {
    SizeF originalImageSizeInMm = SizeF.Empty;
    if (oleObject != null)
      originalImageSizeInMm = oleObject.GetExtentMm();
    return originalImageSizeInMm;
  }

  /// <summary>Получить список расширений поддерживаемых форматов файлов</summary>
  public static List<string> GetFileExtensions()
  {
    return new List<string>()
    {
      "dwg",
      "dxf",
      "dwg",
      "dxf",
      "sld",
      "slb",
      "bmp",
      "jpg",
      "jpeg",
      "tif",
      "tiff",
      "png",
      "wmf",
      "emf",
      "gif",
      "ico"
    };
  }

  /// <summary>Можно вставить содержимое буфера в контейнер</summary>
  public bool CanPasteFromClipboard()
  {
    System.Windows.Forms.IDataObject dataObject = Clipboard.GetDataObject();
    if (dataObject != null)
    {
      dataObject.GetFormats();
      if (dataObject.GetDataPresent(DataFormats.Bitmap) || dataObject.GetDataPresent(DataFormats.EnhancedMetafile) || dataObject.GetDataPresent(DataFormats.MetafilePict) || dataObject.GetDataPresent(DataFormats.Dib) || dataObject.GetDataPresent(DataFormats.Tiff))
        return true;
    }
    return ImOleContainer.CanPaste();
  }

  /// <summary>Вставить содержимое буфера в контейнер</summary>
  /// <param name="hWnd">Handle окна</param>
  public void PasteFromClipboard(IntPtr hWnd)
  {
    if (ImOleContainer.CanPaste())
    {
      ImOleContainer oleContainerBuffer = this.GetOleContainerBuffer();
      if (oleContainerBuffer != null)
      {
        oleContainerBuffer.Paste();
        SizeF extentMm = oleContainerBuffer.GetExtentMm();
        Stream sourceData = oleContainerBuffer.SourceData;
        if (sourceData != null && sourceData.Length > 0L)
        {
          Stream memoryStream = ContainerData.LoadToMemoryStream(sourceData, 65536 /*0x010000*/);
          oleContainerBuffer.SourceData = (Stream) null;
          memoryStream.Position = 0L;
          this.AssignDataStream(memoryStream, DataSourceType.OLE_Clipboard, true, false, false, true);
          this.originalSize = extentMm;
          this.CreateImageFromDataStream(true);
          this.SetNeedUpdateLayoutFlag(true, true, true, true);
          return;
        }
      }
    }
    System.Windows.Forms.IDataObject dataObject = Clipboard.GetDataObject();
    if (dataObject == null)
      return;
    object obj = (object) null;
    if (dataObject.GetDataPresent(DataFormats.EnhancedMetafile))
      obj = (object) ContainerElement.GetMetafileFromClipboard(hWnd);
    else if (dataObject.GetDataPresent(DataFormats.Tiff))
      obj = dataObject.GetData(DataFormats.Tiff);
    else if (dataObject.GetDataPresent(DataFormats.Bitmap))
      obj = dataObject.GetData(DataFormats.Bitmap);
    if (!(obj is Image image))
      return;
    this.AssignDataStream((Stream) null, DataSourceType.Image, true, false, false, true);
    this.AssignImage(image, SizeF.Empty, true, true, true);
  }

  /// <summary>Копировать в буфер</summary>
  /// <param name="docNodeDataObject">Экземпляр IDataObject совсемтно с которым нужно помесить в буфер изображение</param>
  /// <param name="hWnd">Handle окна</param>
  public void CopyToClipboard(System.Windows.Forms.IDataObject docNodeDataObject, IntPtr hWnd)
  {
    if (!(this.image is Metafile))
      return;
    ContainerElement.PutEnhMetafileOnClipboard(hWnd, (Metafile) this.image, false);
  }

  [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern bool OpenClipboard(IntPtr hWnd);

  [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern bool EmptyClipboard();

  [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern IntPtr SetClipboardData(int uFormat, IntPtr hWnd);

  [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern bool CloseClipboard();

  [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern IntPtr GetClipboardData(int uFormat);

  [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern short IsClipboardFormatAvailable(int uFormat);

  /// <summary>Получить метафайл из буфера. Работает через Win32API в обход буфера .NET из-за ошибки в оном</summary>
  /// <param name="hWnd">Handle окна</param>
  /// <returns></returns>
  public static Metafile GetMetafileFromClipboard(IntPtr hWnd)
  {
    Metafile metafileFromClipboard = (Metafile) null;
    if (ContainerElement.OpenClipboard(hWnd) && ContainerElement.IsClipboardFormatAvailable(ContainerElement.CF_ENHMETAFILE) != (short) 0)
    {
      IntPtr clipboardData = ContainerElement.GetClipboardData(ContainerElement.CF_ENHMETAFILE);
      if (clipboardData != IntPtr.Zero)
        metafileFromClipboard = new Metafile(ContainerElement.CopyEnhMetaFile(clipboardData, IntPtr.Zero), true);
      ContainerElement.CloseClipboard();
    }
    return metafileFromClipboard;
  }

  [DllImport("gdi32.dll")]
  private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNULL);

  /// <summary>
  /// Установить метафайл в клипбоард так, чтобы его было видно из других приложений
  /// </summary>
  /// <param name="hWnd"></param>
  /// <param name="mf"></param>
  /// <returns></returns>
  public static bool MetafileToClipboard(IntPtr hWnd, Metafile mf)
  {
    bool clipboard = false;
    IntPtr henhmetafile = mf.GetHenhmetafile();
    if (!henhmetafile.Equals((object) IntPtr.Zero))
    {
      IntPtr hWnd1 = ContainerElement.CopyEnhMetaFile(henhmetafile, IntPtr.Zero);
      if (!hWnd1.Equals((object) IntPtr.Zero) && ContainerElement.OpenClipboard(hWnd) && ContainerElement.EmptyClipboard())
      {
        clipboard = ContainerElement.SetClipboardData(ContainerElement.CF_ENHMETAFILE, hWnd1).Equals((object) hWnd1);
        ContainerElement.CloseClipboard();
      }
    }
    return clipboard;
  }

  /// <summary>Получить метафайл из буфера. Работает через Win32API в обход буфера .NET из-за ошибки в оном</summary>
  /// <param name="hWnd">Handle окна</param>
  /// <param name="mf">Метафайл</param>
  /// <param name="addClipboardData">Добавить как еще один формат к данным в буфере, иначе затирает буфер</param>
  /// <returns></returns>
  public static bool PutEnhMetafileOnClipboard(IntPtr hWnd, Metafile mf, bool addClipboardData)
  {
    bool flag = false;
    IntPtr henhmetafile = mf.GetHenhmetafile();
    if (!henhmetafile.Equals((object) new IntPtr(0)))
    {
      IntPtr hWnd1 = ContainerElement.CopyEnhMetaFile(henhmetafile, new IntPtr(0));
      if (!hWnd1.Equals((object) new IntPtr(0)) && ContainerElement.OpenClipboard(hWnd) && (addClipboardData || ContainerElement.EmptyClipboard()))
      {
        flag = ContainerElement.SetClipboardData(ContainerElement.CF_ENHMETAFILE, hWnd1).Equals((object) hWnd1);
        ContainerElement.CloseClipboard();
      }
      ContainerElement.DeleteEnhMetaFile(henhmetafile);
    }
    return flag;
  }

  public delegate ImOleContainer GetOleContainerBufferDelegate();
}
