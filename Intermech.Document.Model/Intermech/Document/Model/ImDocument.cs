// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImDocument
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using ICSharpCode.SharpZipLib.Zip;
using Intermech.Controls.OleContainer;
using Intermech.Document.Model.ImportBlanks;
using Intermech.Document.Model.PdfGenerator;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Документ</summary>
[Serializable]
public class ImDocument : ImDocumentData
{
  [NonSerialized]
  protected int suspendUpdateUIGeometryCount;
  [NonSerialized]
  protected int suspendRefreshUICount;
  private const int MM_ANISOTROPIC = 8;
  [NonSerialized]
  private CancelEventHandler inplaceEditorActivating;
  [NonSerialized]
  private EventHandler inplaceEditorActivated;
  [NonSerialized]
  private CancelEventHandler inplaceEditorDeactivating;
  [NonSerialized]
  private EventHandler inplaceEditorDeactivated;
  /// <summary>Словарь формул</summary>
  [NonSerialized]
  private Dictionary<string, PageData> formulaIndex;
  [NonSerialized]
  internal DocumentControl _documentControl;
  /// <summary>Буферный экземпляр ImRtfEditor для рисования</summary>
  private ImRtfEditor ternPaintBuffer;
  /// <summary>Буферный экземпляр ImRtfEditor для подбора размера шрифта</summary>
  private ImRtfEditor ternFontMetricsBuffer;
  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  private ImRtfEditor ternDistributeSpecSymbolsBufferB;
  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  private ImRtfEditor ternSpecSymvolsBufferB;
  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  private ImRtfEditor ternDistributeSpecSymbolsBuffer;
  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  private ImRtfEditor ternSpecSymvolsBuffer;
  /// <summary>Буферный экземпляр ImRtfEditor для печати</summary>
  private ImRtfEditor ternPrintBuffer;
  internal static readonly ConcurrentDictionary<Thread, ImRtfEditor> TernDistributeBufferPool = new ConcurrentDictionary<Thread, ImRtfEditor>();
  internal static readonly ConcurrentDictionary<Thread, ImRtfEditor> TernDistributeBufferPoolInFormula = new ConcurrentDictionary<Thread, ImRtfEditor>();
  internal static readonly ConcurrentDictionary<Thread, ImRtfEditor> TernDistributeBufferPoolForPrint = new ConcurrentDictionary<Thread, ImRtfEditor>();
  /// <summary>Буферный экземпяр ImOleContainer</summary>
  public ImOleContainer ImOleContainerBuffer;

  private void InitFields(bool withTemplate, bool applyDefaultFormatFromConfig = true)
  {
    if (applyDefaultFormatFromConfig)
    {
      this.DefaultCharFormat = ImDocumentEditorConfig.Instance.DefaultCharFormat.Clone();
      this.DefaultParagraphFormat = ImDocumentEditorConfig.Instance.DefaultParagraphFormat.Clone();
    }
    bool isDocumentLoading = this.IsDocumentLoading;
    this.IsDocumentLoading = true;
    this.IdService = (IUniqueIdService) new UniqueIdGenerator();
    this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
    if (withTemplate)
      this.AssignDocumentTemplate(ImDocumentData.CreateTemplate(this.GetType(), true), false, false, false);
    this.IsDocumentLoading = isDocumentLoading;
  }

  /// <summary>Конструктор. Вызывается с готовым DocumentControl</summary>
  /// <param name="documentControl">Интерфейс пользователя</param>
  /// <param name="withTemplate">Создавать пустой шаблон</param>
  public ImDocument(DocumentControl documentControl, bool withTemplate)
  {
    this.InitFields(withTemplate);
    this.DocumentControl = documentControl;
  }

  /// <summary>Конструктор. Может автоматически создавать интерфейс пользователя</summary>
  /// <param name="autoCreateUI">Создать интерфейс пользователя</param>
  /// <param name="withTemplate">Создавать пустой шаблон</param>
  public ImDocument(bool autoCreateUI, bool withTemplate, bool applyDefaultFormatFromConfig = true)
  {
    this.InitFields(withTemplate, applyDefaultFormatFromConfig);
    if (!autoCreateUI)
      return;
    this.CreateUI();
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected ImDocument(SerializationInfo info, StreamingContext context)
  {
    DocumentPlugin.InitDocumentPlugin();
    Stream stream = (Stream) new MemoryStream((byte[]) info.GetValue("Stream", typeof (byte[])));
    stream.Position = 0L;
    this.LoadDocumentFromXml(stream, false, false, false);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      this.SaveToXml((Stream) imChunkedStream);
      byte[] array = imChunkedStream.ToArray();
      info.AddValue("Stream", (object) array, typeof (byte[]));
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="withTemplate">Создать документ с шаблоном.
  /// Если false, то шаблон документа не создается и можно назначить его позже.</param>
  public ImDocument(bool withTemplate) => this.InitFields(withTemplate);

  /// <summary>Конструктор</summary>
  public ImDocument()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="template">Шаблон документа</param>
  /// <param name="applyTemplate">Применить шаблон</param>
  /// <param name="needFirstPage">Создавать первую страницу, даже если в шаблоне не определена страница,
  /// которая должна автоматически создаваться</param>
  public ImDocument(ImDocument template, bool applyTemplate, bool needFirstPage)
    : base((ImDocumentData) template, applyTemplate, needFirstPage)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="docData">Данные документа</param>
  /// <param name="template">Шаблон документа</param>
  public ImDocument(ImDocumentData docData, ImDocument template)
    : this(false)
  {
    if (docData == null)
      throw new ArgumentNullException(nameof (docData));
    this.IsDocumentLoading = true;
    IDictionary links = (IDictionary) new HybridDictionary();
    this.CopyFields((DocumentTreeNode) docData, false, true, true, false, true, links);
    int index = 0;
    for (int count = docData.Nodes.Count; index < count; ++index)
      this.AddChildNode(docData.Nodes[index].RestoreToOriginalType(links), false, false);
    this.RestoreFieldsFromUnknownXml();
    this.OnDeserialization((object) this);
    docData.RestoreLinks(true, false, true, links);
    this.AssignDocumentTemplate((ImDocumentData) template, true, false, false);
    this.UpdateLayout(0, true, false);
    this.IsDocumentLoading = false;
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public new static object EmptyConstructor() => (object) new ImDocument(false);

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new ImDocument(false, false, false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Иконка для кнопки статическая версия</summary>
  public static Image Icon
  {
    get
    {
      return PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.Document.png");
    }
  }

  /// <summary>Разрешать форматирование для ReadOnly ячеек</summary>
  public override bool AllowFormatingForReadOnlyText
  {
    get => base.AllowFormatingForReadOnlyText;
    set
    {
      if (this.AllowFormatingForReadOnlyText == value)
        return;
      base.AllowFormatingForReadOnlyText = value;
      this.UpdateFormatCommands();
    }
  }

  /// <summary>Обновить команды форматирования в меню</summary>
  public void UpdateFormatCommands()
  {
    if (this.documentControl == null)
      return;
    this.documentControl.UpdateFormatCommands();
  }

  /// <summary>Получить допустимые типы представлений данных для ячейки</summary>
  /// <returns>Массив допустимых типов представлений данных для ячейки</returns>
  public override System.Type[] GetAviableDataShowElementTypes()
  {
    System.Type[] types = this.GetType().Assembly.GetTypes();
    ArrayList arrayList = new ArrayList();
    System.Type type = typeof (TextData);
    foreach (System.Type c in types)
    {
      if (type.IsAssignableFrom(c) && !c.IsAbstract)
        arrayList.Add((object) c);
    }
    System.Type[] showElementTypes = new System.Type[arrayList.Count];
    arrayList.CopyTo((Array) showElementTypes);
    return showElementTypes;
  }

  /// <summary>Добавить и связать объекты интерфейса пользователя</summary>
  /// <param name="child">Дочерний узел</param>
  public override void AddChildUI(DocumentTreeNode child, bool createUI)
  {
    if (!this.IsVirtualNode)
      return;
    base.AddChildUI(child, createUI);
  }

  [Browsable(false)]
  public override IUndoManager UndoManager
  {
    get
    {
      return this.DocumentControl != null && this.DocumentControl.DocumentEditorForm != null ? this.DocumentControl.DocumentEditorForm.UndoManager : (IUndoManager) null;
    }
  }

  [Browsable(false)]
  public override IExternalEditor ExternalEditor
  {
    get
    {
      return this.DocumentControl != null ? this.DocumentControl.ExternalEditor : (IExternalEditor) null;
    }
  }

  /// <summary>Интерфейсный элемент управления</summary>
  [Browsable(false)]
  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get => this.documentControl;
    set
    {
      if (this.documentControl == value)
        return;
      bool updateUiGeometryFlag = this.SuspendedUpdateUIGeometryFlag;
      if (!updateUiGeometryFlag)
        this.SuspendUpdateGeometryRefreshUI();
      try
      {
        if (this.documentControl != null)
          this.documentControl.Parent = (Control) null;
        this.documentControl = value;
        if (this.documentControl != null)
        {
          if (this.Parent is VisualNode parent)
            parent.AddChildUI((DocumentTreeNode) this, false);
          if (this.documentControl.Document != this)
            this.documentControl.Document = this;
          this.SetNeedUIRecursive(true, false);
          this.SetNeedUpdateUIGeometryRecursive(true, false);
        }
        else
          this.SetNeedUIRecursive(false, false);
      }
      finally
      {
        if (!updateUiGeometryFlag)
          this.ResumeUpdateRefreshUI(this.documentControl != null, this.documentControl != null);
      }
    }
  }

  /// <summary>Контрол интерфейса страниц</summary>
  [Browsable(false)]
  public PageControl PageControl
  {
    get => this.DocumentControl != null ? this.DocumentControl.PageControl : (PageControl) null;
  }

  /// <summary>Запущен процесс разбивки</summary>
  internal new bool IsDistributing => this.isDistributing;

  /// <summary>Обновить представление данных</summary>
  /// <param name="fromPage">Начиная со страницы</param>
  /// <param name="force">Обновлять даже если SuspendedUpdateLayoutFlag</param>
  /// <param name="lockUndo">Блокировать сохранение undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="distributeInThread">Разбивать в потоке</param>
  public override void UpdateLayout(
    int fromPage,
    bool force,
    bool lockUndo,
    bool updateUI,
    bool distributeInThread)
  {
    base.UpdateLayout(fromPage, force, lockUndo, updateUI, distributeInThread);
  }

  /// <summary>Обновить представление данных</summary>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void UpdateLayout(bool updateUI)
  {
    base.UpdateLayout(updateUI);
    if (!updateUI)
      return;
    this.UpdateUIGeometry(true);
  }

  protected override void OnPageUnlocked(PageUnlockedArgs e)
  {
    if (e.IsDistributed)
      this.DocumentControl?.RestoreActiveEditorSelection();
    base.OnPageUnlocked(e);
  }

  /// <summary>Обновить геометрию интерфейса пользователя</summary>
  public override void UpdateUIGeometry(bool refreshUI)
  {
    if (this.SuspendedUpdateUIGeometryFlag || this.PageControl == null)
      return;
    this.PageControl.LockUpdate();
    try
    {
      this.PageControl.UpdateSettings();
      this.PageControl.LockUpdateSettings();
      try
      {
        base.UpdateUIGeometry(refreshUI);
      }
      finally
      {
        this.PageControl.UnLockUpdateSettings();
      }
    }
    finally
    {
      this.PageControl.UnLockUpdate();
    }
    if (!refreshUI)
      return;
    this.PageControl.Refresh();
  }

  /// <summary>Заблокировать обновление геометрии интерфейса и изображения</summary>
  public override void SuspendUpdateGeometryRefreshUI()
  {
    ++this.suspendUpdateUIGeometryCount;
    ++this.suspendRefreshUICount;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.SuspendUpdateGeometryRefreshUI();
    }
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
  /// <remarks>Блокирова увеличивает значение счетчика. Разблокировка соответственно уменьшает значение счетчика. При нулевом значении счетчика обновление разрешено.</remarks>&gt;
  /// </summary>
  public override void SuspendUpdateUIGeometry()
  {
    ++this.suspendUpdateUIGeometryCount;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.SuspendUpdateUIGeometry();
    }
  }

  /// <summary>Заблокировать обновление изображения
  /// <remarks>Блокирова увеличивает значение счетчика. Разблокировка соответственно уменьшает значение счетчика. При нулевом значении счетчика обновление разрешено.</remarks>&gt;
  /// </summary>
  public override void SuspendRefreshUI()
  {
    ++this.suspendRefreshUICount;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.SuspendRefreshUI();
    }
  }

  /// <summary>Разблокировать обновление изображения</summary>
  public override void ResumeRefreshUI(bool refresh)
  {
    if (this.suspendRefreshUICount > 0)
      --this.suspendRefreshUICount;
    else
      this.suspendRefreshUICount = 0;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is VisualNode node)
          node.ResumeRefreshUI(false);
      }
    }
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
    if (this.PageControl != null)
      this.PageControl.LockUpdateSettings();
    try
    {
      if (this.nodes != null)
      {
        for (int index = 0; index < this.nodes.Count; ++index)
        {
          if (this.nodes[index] is VisualNode node)
            node.ResumeUpdateUIGeometry(false, false);
        }
      }
    }
    finally
    {
      if (this.PageControl != null)
        this.PageControl.UnLockUpdateSettings();
    }
    if (update && !this.SuspendedUpdateUIGeometryFlag)
      this.UpdateUIGeometry(false);
    if (this.nodes != null)
    {
      int index = 0;
      for (int count = this.nodes.Count; index < count; ++index)
      {
        if (this.nodes[index] is VisualNode node)
          node.ResumeRefreshUI(false);
      }
    }
    if (!refresh || this.SuspendedRefreshUIFlag)
      return;
    this.RefreshUI();
  }

  /// <summary>Разблокировать обновление геометрии интерфейса пользователя</summary>
  /// <param name="update">Обновить геометрию</param>
  /// <param name="refresh">Обновить изображение</param>
  public override void ResumeUpdateUIGeometry(bool update, bool refresh)
  {
    if (this.suspendUpdateUIGeometryCount > 0)
      --this.suspendUpdateUIGeometryCount;
    else
      this.suspendUpdateUIGeometryCount = 0;
    if (this.PageControl != null)
      this.PageControl.LockUpdateSettings();
    try
    {
      if (this.nodes != null)
      {
        int index = 0;
        for (int count = this.nodes.Count; index < count; ++index)
        {
          if (this.nodes[index] is VisualNode node)
            node.ResumeUpdateUIGeometry(false, false);
        }
      }
    }
    finally
    {
      if (this.PageControl != null)
        this.PageControl.UnLockUpdateSettings();
    }
    if (update && !this.SuspendedUpdateUIGeometryFlag)
      this.UpdateUIGeometry(false);
    if (!refresh)
      return;
    this.RefreshUI();
  }

  /// <summary>Обновить изображение на экране</summary>
  public override void RefreshUI()
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag || this.DocumentControl == null || this.PageControl == null || this.PageControl.LockedUpdateSettings)
      return;
    this.PageControl.Refresh();
    if (this.DocumentControl.ActiveElement == null || !(this.DocumentControl.ActiveElement is TextBoxElement))
      return;
    TextBoxElement activeElement = this.DocumentControl.ActiveElement as TextBoxElement;
    if (!activeElement.InPlaceEditorActive || activeElement.InPlaceEditorControl == null || activeElement.InPlaceEditorControl.InvokeRequired)
      return;
    activeElement.InPlaceEditorControl.Refresh();
  }

  /// <summary>Создать соответствующий элемент управления</summary>
  public override void CreateUI()
  {
    if (!this.IsVirtualNode && this.needUI && this.documentControl == null)
      this.documentControl = new DocumentControl(this, (IImDocumentManager) null);
    base.CreateUI();
  }

  /// <summary>Удалить объекты интерфейса пользователя</summary>
  public override void DestroyUI()
  {
    Control documentControl = (Control) this.DocumentControl;
    this.DocumentControl = (DocumentControl) null;
    base.DestroyUI();
    if (documentControl == null)
      return;
    documentControl.Parent = (Control) null;
  }

  [DllImport("gdiplus.dll", SetLastError = true)]
  private static extern int GdipEmfToWmfBits(
    int hEmf,
    int uBufferSize,
    byte[] bBuffer,
    int iMappingMode,
    ImDocument.EmfToWmfBitsFlags flags);

  /// <summary>Сохранить документ в файл формата pdf</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="autostart">Запустить файл после сохранения на просмотр</param>
  public void SaveToPdf(string fileName, bool autostart = true, bool showProgress = false)
  {
    ImDocument.SaveToPdf(this.PrintDocument, new ImDocumentData[1]
    {
      (ImDocumentData) this
    }, fileName, (autostart ? 1 : 0) != 0);
  }

  public static void SaveToXLS(
    ImDocumentData[] docs,
    Stream outputStream,
    bool autostart = false,
    bool showProgress = false)
  {
    ExcelConverter.Save(docs, outputStream, (string) null, autostart, showProgress);
  }

  public static void SaveToXLS(
    ImDocumentData[] docs,
    string filename,
    bool autostart = false,
    bool showProgress = false)
  {
    ExcelConverter.Save(docs, (Stream) null, filename, autostart, showProgress);
  }

  public void SaveToPdf(Stream outputStream, bool showProgress = false)
  {
    PDFCreatePrinter.SaveToPdf((PrintDocument) null, new ImDocumentData[1]
    {
      (ImDocumentData) this
    }, outputStream, (showProgress ? 1 : 0) != 0);
  }

  /// <summary>Сохранить документ в файл формата pdf</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="autostart">Запустить файл после сохранения на просмотр</param>
  public static void SaveToPdf(
    PrintDocument printdoc,
    ImDocumentData[] docs,
    string fileName,
    bool autostart = true,
    bool showProgress = false)
  {
    PDFCreatePrinter.SaveToPdf(printdoc, docs, fileName, autostart, showProgress);
  }

  /// <summary>Сгенерировать метафайлы для страниц</summary>
  /// <param name="pages">Список номеров страниц (индекс в nodes)</param>
  /// <param name="baseFilename">Базовое имени файла</param>
  public void GeneratePageMetafiles(int[] pages, string baseFilename)
  {
    this.TernPrintBuffer = RtfInSiteEditorWrapper.CreateTernPrintBuffer();
    if (pages == null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is Page node2)
          node2.CreatePageMetafile($"{baseFilename}#{index.ToString()}.wmf");
        else if (this.nodes[index] is DocumentSection node1)
          node1.GeneratePageMetafiles(pages, baseFilename);
      }
    }
    else
    {
      for (int index = 0; index < pages.Length; ++index)
      {
        if (this.nodes[pages[index]] is Page node4)
          node4.CreatePageMetafile($"{baseFilename}#{pages[index].ToString()}.wmf");
        else if (this.nodes[pages[index]] is DocumentSection node3)
          node3.GeneratePageMetafiles(pages, baseFilename);
      }
    }
  }

  /// <summary>Размер сетки</summary>
  [CustomDisplayName("Attribute.Document.Model_57")]
  [CustomCategory("Attribute.Document.Model_58")]
  [TypeConverter(typeof (FloatConverter))]
  public float GridSize
  {
    [DebuggerStepThrough] get
    {
      return this.documentControl != null ? this.documentControl.GridSize : 0.1f;
    }
  }

  /// <summary>Запрет на изменение пользователем структуры узла</summary>
  public override bool ReadOnlyStructure
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentControl != null && this.DocumentControl.ReadOnly || base.ReadOnlyStructure;
    }
  }

  protected override void OnBeforeDistributeInThread(DistributeThreadArgs threadParams)
  {
    base.OnBeforeDistributeInThread(threadParams);
  }

  protected override void OnAfterDistributeInThread(DistributeThreadArgs threadParams)
  {
    base.OnAfterDistributeInThread(threadParams);
    if (!threadParams.IsBackgroundThread)
      return;
    ImDocument.ReleaseMainDistributeBuffer();
    ImDocument.ReleaseFormulaDistributeBuffer();
    ImDocument.ReleasePrintDistributeBuffer();
  }

  protected override void OnChildNodeAdded(ChildNode_EventArgs e) => base.OnChildNodeAdded(e);

  /// <summary>Герерирует событие ChildNodeRemoved</summary>
  public override void OnChildNodeRemoved(ChildNode_EventArgs e)
  {
    base.OnChildNodeRemoved(e);
    if (!(e.Child is PageData child) || this.documentControl == null || this.documentControl.ActivePage != child)
      return;
    PageData pageData = ImDocumentData.GetPrevPage(e.Parent, e.Index, true) ?? ImDocumentData.GetNextPage(e.Parent, e.Index, true);
    if (pageData == null)
      return;
    this.documentControl.ActivePage = pageData as Page;
  }

  /// <summary>Обработчик события "Начало печати документа"</summary>
  public override void BeginPrint(object sender, PrintEventArgs e)
  {
    try
    {
      this.TernPrintBuffer = RtfInSiteEditorWrapper.CreateTernPrintBuffer();
      RtfInSiteEditorWrapper.BeginPrint(this.TernPrintBuffer);
      base.BeginPrint(sender, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
      e.Cancel = true;
    }
  }

  public override bool PrintPage(PrintDocument printDoc, PrintPageEventArgs e, PageData curPage)
  {
    if (this.TernPrintBuffer != null)
    {
      RtfInSiteEditorWrapper.EndPrint(this.TernPrintBuffer);
      this.TernPrintBuffer.TerDeleteAll(false);
      this.TernPrintBuffer.Dispose();
    }
    this.TernPrintBuffer = (ImRtfEditor) null;
    return base.PrintPage(printDoc, e, curPage);
  }

  /// <summary>Обработчик события "Конец печати"</summary>
  protected override void EndPrint(object sender, PrintEventArgs e)
  {
    try
    {
      RtfInSiteEditorWrapper.EndPrint(this.TernPrintBuffer);
      this.TernPrintBuffer = (ImRtfEditor) null;
      base.EndPrint(sender, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
      e.Cancel = true;
    }
  }

  /// <summary>Печатать ли страницу согласно настройкам печати</summary>
  /// <param name="page">Старница</param>
  /// <returns>true, если печатать страницу</returns>
  public override bool NeedPrintPage(PrintDocument printDoc, PageData page)
  {
    if (base.NeedPrintPage(printDoc, page))
      return true;
    if (this.ImPrintSettings.SelectedPrintPages.Count == 0 && printDoc.PrinterSettings.PrintRange == PrintRange.Selection && this.DocumentControl != null)
      return this.DocumentControl.SelectedNodes.Contains((DocumentTreeNode) page) || this.DocumentControl.ActivePage == page;
    if (printDoc.PrinterSettings.PrintRange != PrintRange.CurrentPage || this.DocumentControl == null)
      return false;
    return this.DocumentControl.SelectedNodes.Contains((DocumentTreeNode) page) || this.DocumentControl.ActivePage == page;
  }

  protected override GetVirtualAttributeResult GetVirtualAttributeValue(
    string attributeName,
    bool notNull,
    List<DocumentTreeNode> callChain = null)
  {
    return attributeName == DocumentTreeNode.AttributeName_CheckSum && this.DocumentControl != null && !this.DocumentControl.DocumentViewMode.HasFlag((Enum) DocumentViewMode.ShowCRC) ? new GetVirtualAttributeResult(true, "") : base.GetVirtualAttributeValue(attributeName, notNull, callChain);
  }

  /// <summary>Получить суммарное смещение страниц. Смещение заданное в шаблоне + смещение заданное настройками для принтера</summary>
  /// <param name="printerName">Имя принтера</param>
  /// <returns></returns>
  protected override PointF GetSummaryShiftForPage(string printerName)
  {
    PointF shiftPage = ImDocumentEditorConfig.Instance.GetShiftPage(printerName);
    return new PointF(shiftPage.X + this.ShiftPage.X, shiftPage.Y + this.ShiftPage.Y);
  }

  /// <summary>Создать и добавить новую страницу</summary>
  public override PageData NewPage(DocumentTreeNode parent)
  {
    Page page = new Page(parent);
    if (this.DocumentControl != null)
      this.DocumentControl.ActivePage = page;
    return (PageData) page;
  }

  /// <summary>Создать копию элемента используя этот узел как шаблон</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyDataNodes">Копировать узлы-данные в таблицах</param>
  /// <returns>Копия узла</returns>
  public override DocumentTreeNode CloneFromTemplate(bool copyChildren, bool copyDataNodes)
  {
    if (this.IsTemplate)
      return (DocumentTreeNode) new ImDocument(this, true, copyChildren);
    return this.DocumentTemplate != null ? (DocumentTreeNode) new ImDocument((ImDocument) this.DocumentTemplate, true, copyChildren) : (DocumentTreeNode) new ImDocument();
  }

  /// <summary>
  /// Найти на страницах из старого бланка ячейки с одинаковым атрибутом BLN.ID и связать их, для переноса данных с первого листа на следующие
  /// Поддержка старого механизма в бланках
  /// </summary>
  public void FindAndLinkTextWithSomeBlankID()
  {
    Dictionary<string, TextData> genericCollection = new Dictionary<string, TextData>();
    foreach (PageData pageData in (ImDocumentData) this)
    {
      bool result;
      bool.TryParse(pageData.GetAttributeValue("BLN.CanBeFirst", true), out result);
      if (result)
      {
        genericCollection.Clear();
        foreach (TextData textData in pageData.NodesRecursiveByCondition((Func<DocumentTreeNode, bool>) (n => n is RectangleElement rectangleElement && !rectangleElement.IsCellInDataFlowTable)).OfType<TextData>())
        {
          string attributeValue = textData.GetAttributeValue("BLN.ID", true);
          if (!attributeValue.IsEmpty<char>() && !genericCollection.ContainsKey(attributeValue))
            genericCollection.Add(attributeValue, textData);
        }
      }
      else if (!genericCollection.IsEmpty<KeyValuePair<string, TextData>>())
      {
        foreach (TextData ownerNode in pageData.NodesRecursiveByCondition((Func<DocumentTreeNode, bool>) (n => n is RectangleElement rectangleElement1 && !rectangleElement1.IsCellInDataFlowTable)).OfType<TextData>())
        {
          string attributeValue = ownerNode.GetAttributeValue("BLN.ID", true);
          TextData textData;
          if (!attributeValue.IsEmpty<char>() && genericCollection.TryGetValue(attributeValue, out textData))
          {
            ReferenceToNodeAttribute referenceToNodeAttribute = new ReferenceToNodeAttribute((DocumentTreeNode) ownerNode, BaseReferenceNodeType.ntSelectedNode, textData.Id, DocumentTreeNode.AttributeName_Text);
            ownerNode.AssignReferenceToTextSource((ReferenceBase) referenceToNodeAttribute, true, false, false);
          }
        }
      }
    }
  }

  /// <summary>Событие перед активацией редактора по месту для полей с встроенным редактором</summary>
  public event CancelEventHandler InplaceEditorActivating
  {
    add => this.inplaceEditorActivating += value;
    remove => this.inplaceEditorActivating -= value;
  }

  /// <summary>
  /// Событие перед активацией редактора по месту для полей с встроенным редактором
  /// </summary>
  /// <param name="sender">Поле вызвавшее событие</param>
  /// <param name="args">Параметры</param>
  internal void OnInplaceEditorActivating(object sender, CancelEventArgs args)
  {
    if (this.inplaceEditorActivating == null)
      return;
    this.inplaceEditorActivating(sender, args);
  }

  /// <summary>Событие после активации редактора по месту для полей с встроенным редактором</summary>
  public event EventHandler InplaceEditorActivated
  {
    add => this.inplaceEditorActivated += value;
    remove => this.inplaceEditorActivated -= value;
  }

  /// <summary>Событие после активации редактора по месту для полей с встроенным редактором</summary>
  /// <param name="sender">Поле вызвавшее событие</param>
  /// <param name="args">Параметры</param>
  internal void OnInplaceEditorActivated(object sender, EventArgs args)
  {
    if (this.inplaceEditorActivated == null)
      return;
    this.inplaceEditorActivated(sender, args);
  }

  /// <summary>Событие перед деактивацией редактора по месту для полей с встроенным редактором</summary>
  public event CancelEventHandler InplaceEditorDeactivating
  {
    add => this.inplaceEditorDeactivating += value;
    remove => this.inplaceEditorDeactivating -= value;
  }

  /// <summary>Событие перед деактивацией редактора по месту для полей с встроенным редактором</summary>
  /// <param name="sender">Поле вызвавшее событие</param>
  /// <param name="args">Параметры</param>
  internal void OnInplaceEditorDeactivating(object sender, CancelEventArgs args)
  {
    if (this.inplaceEditorDeactivating == null)
      return;
    this.inplaceEditorDeactivating(sender, args);
  }

  /// <summary>Событие после деактивации редактора по месту для полей с встроенным редактором</summary>
  public event EventHandler InplaceEditorDeactivated
  {
    add => this.inplaceEditorDeactivated += value;
    remove => this.inplaceEditorDeactivated -= value;
  }

  /// <summary>Событие после деактивации редактора по месту</summary>
  /// <param name="sender">Поле вызвавшее событие</param>
  /// <param name="args">Параметры</param>
  internal void OnInplaceEditorDeactivated(object sender, EventArgs args)
  {
    if (this.inplaceEditorDeactivated == null)
      return;
    this.inplaceEditorDeactivated(sender, args);
  }

  /// <summary>Метод загрузки документа или комплекта из XML для фонового потока</summary>
  /// <param name="args">Аргументы загрузки из XML. Должны быть типа XmlReadArgs</param>
  public static void LoadDocOrComplectFromXmlInThread(object args)
  {
    XmlReadArgs readArgs = (XmlReadArgs) args;
    try
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      try
      {
        bool flag = false;
        string str = (string) null;
        while (!flag && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              if (readArgs.Reader.LocalName == "Document")
              {
                str = readArgs.Reader.LocalName;
                ImDocument imDocument = new ImDocument(false, false, false);
                imDocument.LockUndo();
                imDocument.LoadFromStreamThread = readArgs.LoadFromStreamThread;
                imDocument.IsFileLoading = true;
                imDocument.IsDocumentLoading = true;
                try
                {
                  if (readArgs.DocumentDBReference != null)
                    imDocument.Reference = readArgs.DocumentDBReference;
                  imDocument.Modified = false;
                  imDocument.BeginChanges(false);
                  readArgs.RootDocNode = (object) imDocument;
                  readArgs.RootNodeIsComplect = false;
                  imDocument.FileSize = new long?(readArgs.FileSize);
                  imDocument.FileModifyDate = readArgs.FileModifyDate;
                  if (readArgs.ReadInThread)
                  {
                    readArgs.RootDocNodeIsLocked = true;
                    Monitor.Enter(readArgs.LockedObjectByLoadThread = (object) readArgs);
                  }
                  imDocument.ReadFromXml(readArgs);
                  imDocument.FileName = readArgs.FileName;
                  imDocument.EndChanges(false);
                  imDocument.Modified = false;
                }
                finally
                {
                  imDocument.IsFileLoading = false;
                  imDocument.UnlockUndo();
                }
              }
              else if (readArgs.Reader.LocalName == "DocumentsComplect")
              {
                ReferenceBase documentDbReference = readArgs.DocumentDBReference;
                readArgs.DocumentDBReference = (ReferenceBase) null;
                str = readArgs.Reader.LocalName;
                DocumentsComplect documentsComplect = new DocumentsComplect();
                documentsComplect.LoadFromStreamThread = readArgs.LoadFromStreamThread;
                documentsComplect.LockUndo();
                documentsComplect.IsFileLoading = true;
                documentsComplect.IsDocumentLoading = true;
                try
                {
                  readArgs.RootDocNode = (object) documentsComplect;
                  readArgs.RootNodeIsComplect = true;
                  if (readArgs.ReadInThread)
                  {
                    readArgs.RootDocNodeIsLocked = true;
                    Monitor.Enter(readArgs.LockedObjectByLoadThread = (object) readArgs);
                  }
                  documentsComplect.ReadFromXml(readArgs);
                }
                finally
                {
                  documentsComplect.UnlockUndo();
                  documentsComplect.IsFileLoading = false;
                  readArgs.DocumentDBReference = documentDbReference;
                }
              }
              if (str == readArgs.Reader.LocalName)
              {
                flag = true;
                continue;
              }
              continue;
            case XmlNodeType.EndElement:
              if (str == readArgs.Reader.LocalName)
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
      catch (ThreadAbortException ex)
      {
        Thread.ResetAbort();
      }
      finally
      {
        try
        {
          if (readArgs.RootDocNodeIsLocked && readArgs.ReadInThread)
          {
            readArgs.RootDocNodeIsLocked = false;
            ImDocumentData rootDocNode1 = readArgs.RootDocNode as ImDocumentData;
            DocumentsComplect rootDocNode2 = readArgs.RootDocNode as DocumentsComplect;
            if (rootDocNode1 != null && rootDocNode1.LoadFromStreamThread != null && (rootDocNode1.LoadFromStreamThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running || rootDocNode2 != null && rootDocNode2.LoadFromStreamThread != null && (rootDocNode2.LoadFromStreamThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running)
            {
              Monitor.Pulse(readArgs.LockedObjectByLoadThread);
              Monitor.Exit(readArgs.LockedObjectByLoadThread);
            }
          }
          else if (readArgs.RootDocNode == null && readArgs.ReadInThread)
          {
            Monitor.Enter(readArgs.LockedObjectByLoadThread);
            Monitor.Pulse(readArgs.LockedObjectByLoadThread);
            Monitor.Exit(readArgs.LockedObjectByLoadThread);
          }
          if (!readArgs.NotCloseStream)
            readArgs.Reader.Close();
          if (readArgs.RootNodeIsComplect)
          {
            DocumentsComplect rootDocNode = readArgs.RootDocNode as DocumentsComplect;
            if (rootDocNode.LoadFromStreamThread != null)
            {
              rootDocNode.LoadFromStreamThread = (Thread) null;
              rootDocNode.OnBackgroundLoadFinished(new BackgroundThreadsFinishedArgs(DocumentBackgroundThreadType.LoadThread));
            }
          }
          readArgs.LoadFromStreamThread = (Thread) null;
        }
        catch (Exception ex)
        {
          LogManager.AddLine(ex, true);
          if (readArgs.ThreadIsExternal)
            throw;
          string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
          ImDocumentData.ShowException(ex, errorFormCaption);
        }
      }
    }
    catch (Exception ex)
    {
      if (readArgs.ThreadIsExternal)
        throw;
      System.Threading.ThreadState threadState = Thread.CurrentThread.ThreadState;
      LogManager.AddLine(ex, true);
      if (threadState == System.Threading.ThreadState.Aborted || threadState == System.Threading.ThreadState.AbortRequested)
        return;
      ImDocumentData.ShowException(ex, LocalizationHolder.rm.GetString("Document.Model_617"));
    }
  }

  /// <summary>Метод загрузки из XML для фонового потока</summary>
  /// <param name="args">Аргументы загрузки из XML. Должны быть типа XmlReadArgs</param>
  private void LoadFromXmlInThread(object args)
  {
    XmlReadArgs readArgs = args != null ? (XmlReadArgs) args : throw new ArgumentNullException(nameof (args));
    try
    {
      this.LockUndo();
      this.IsFileLoading = true;
      try
      {
        readArgs.RootNodeIsComplect = false;
        readArgs.RootDocNode = (object) this;
        if (readArgs.ReadInThread)
        {
          Monitor.Enter(readArgs.LockedObjectByLoadThread = (object) readArgs);
          readArgs.RootDocNodeIsLocked = true;
        }
        bool flag = false;
        while (!flag && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              if (readArgs.Reader.LocalName == "Document")
              {
                this.ReadFromXml(readArgs);
                continue;
              }
              continue;
            case XmlNodeType.EndElement:
              if ("Document" == readArgs.Reader.LocalName)
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
      catch (ThreadAbortException ex)
      {
        Thread.ResetAbort();
      }
      finally
      {
        this.UnlockUndo();
        if (readArgs.RootDocNodeIsLocked)
        {
          readArgs.RootDocNodeIsLocked = false;
          try
          {
            if (this.LoadFromStreamThread != null)
            {
              if ((this.LoadFromStreamThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running)
              {
                Monitor.Pulse(readArgs.LockedObjectByLoadThread);
                Monitor.Exit(readArgs.LockedObjectByLoadThread);
              }
            }
          }
          catch (Exception ex)
          {
            LogManager.AddLine($"ImDocument.LoadFromXmlInThread Exception:{ex.Message}\r\n{ex.StackTrace}", true);
            if (readArgs.ThreadIsExternal)
              throw;
            ImDocumentData.ShowException(ex, LocalizationHolder.rm.GetString("Document.Model_617"));
          }
        }
        if (!readArgs.NotCloseStream)
          readArgs.Reader.Close();
        this.IsFileLoading = false;
      }
    }
    catch (Exception ex)
    {
      LogManager.AddLine($"ImDocument.LoadFromXmlInThread Exception:{ex.Message}\r\n{ex.StackTrace}", true);
      if (readArgs.ThreadIsExternal)
        throw;
      ImDocumentData.ShowException(ex, LocalizationHolder.rm.GetString("Document.Model_617"));
    }
  }

  /// <summary>Получить перечисление узлов структуры документа</summary>
  public IEnumerable<DocumentTreeNode> EnumerateDocumentTreeNodes()
  {
    return ImDocument.Flatten((IEnumerable<DocumentTreeNode>) this.Nodes);
  }

  /// <summary>
  /// Разложить древовидную структуру документа в плоский энемератор
  /// </summary>
  internal static IEnumerable<DocumentTreeNode> Flatten(IEnumerable<DocumentTreeNode> ncol)
  {
    ncol = ncol ?? Enumerable.Empty<DocumentTreeNode>();
    IEnumerable<DocumentTreeNode> second = ncol.SelectMany<DocumentTreeNode, DocumentTreeNode>((Func<DocumentTreeNode, IEnumerable<DocumentTreeNode>>) (n => ImDocument.Flatten((IEnumerable<DocumentTreeNode>) n.Nodes)));
    return ncol.Concat<DocumentTreeNode>(second);
  }

  /// <summary>Сравнить шаблоны на совместимость</summary>
  public static bool AreCompatibleTemplates(
    ImDocument templateOne,
    ImDocument templateTwo,
    out string resultDescription,
    bool checkBackCompatibility = false,
    List<string> exclusionList = null)
  {
    resultDescription = "";
    if (templateOne == null)
      throw new ArgumentException(nameof (templateOne));
    if (templateTwo == null)
      throw new ArgumentException(nameof (templateTwo));
    List<DocumentTreeNode> firstNodes = templateOne.EnumerateDocumentTreeNodes().Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (e => !string.IsNullOrWhiteSpace(e.Id))).ToList<DocumentTreeNode>();
    List<DocumentTreeNode> secondNodes = templateTwo.EnumerateDocumentTreeNodes().Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (e => !string.IsNullOrWhiteSpace(e.Id))).ToList<DocumentTreeNode>();
    bool flag1 = firstNodes.All<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (fn => secondNodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (sn => fn.Id == sn.Id && fn.NodeClass == sn.NodeClass && (fn.Parent?.Id ?? "") == (sn.Parent?.Id ?? "")))));
    List<DocumentTreeNode> list = firstNodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (fn => !secondNodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (sn => fn.Id == sn.Id && fn.NodeClass == sn.NodeClass && (fn.Parent?.Id ?? "") == (sn.Parent?.Id ?? ""))))).ToList<DocumentTreeNode>();
    if (list.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("Данные элементы либо отсутствуют в новом шаблоне, либо имеют разный родительский элемент, либо принадлежат к другому типу элементов:\r\n\r\n");
      stringBuilder.Append(" \r\n");
      foreach (string str in list.Select<DocumentTreeNode, string>((Func<DocumentTreeNode, string>) (m2 => $"{m2.NodeTypeCaption} [{m2.Id}]")).ToList<string>())
        stringBuilder.Append(str + "\r\n");
      resultDescription = stringBuilder.ToString();
    }
    if (list.Count > 0 && exclusionList != null && exclusionList.Count > 0)
      flag1 = list.All<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (ii => exclusionList.Any<string>((Func<string, bool>) (x => ii.FindParentNodeByNameOrId(x) != null))));
    bool flag2 = !checkBackCompatibility || secondNodes.All<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (sn => firstNodes.Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (fn => sn.Id == fn.Id && sn.NodeClass == fn.NodeClass && (sn.Parent?.Id ?? "") == (fn.Parent?.Id ?? "")))));
    return flag1 & flag2;
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (readArgs.Reader.LocalName == "Template")
    {
      ImDocumentData.ReadTemplate((DocumentTreeNode) this, readArgs);
      return true;
    }
    return base.ReadFieldFromXml(readArgs);
  }

  /// <summary>Загрузить документ XML из потока. Основной метод.</summary>
  /// <param name="stream">Поток содержащий документ</param>
  /// <param name="updateDoc">Обновить ссылки и разбивку в документе после загрузки</param>
  /// <param name="notCloseStream">Не закрывать поток после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом процессе</param>
  /// <param name="readArgs">Аргументы загрузки.
  /// Необходимы при загрузке с внешним фоновым процессом</param>
  /// <returns>Документ</returns>
  public static DocumentTreeNode LoadDocOrComplectFromXml(
    Stream stream,
    bool updateDoc,
    bool notCloseStream,
    bool loadInThread,
    XmlReadArgs readArgs = null)
  {
    XmlTextReader reader = new XmlTextReader(stream);
    reader.WhitespaceHandling = WhitespaceHandling.All;
    if (readArgs == null)
      readArgs = new XmlReadArgs((XmlReader) reader);
    else
      readArgs.Reader = (XmlReader) reader;
    readArgs.FileSize = stream.Length;
    readArgs.ReadInThread = loadInThread;
    readArgs.NotCloseStream = notCloseStream;
    if (loadInThread && !readArgs.ThreadIsExternal)
    {
      Monitor.Enter(readArgs.LockedObjectByLoadThread = (object) readArgs);
      try
      {
        readArgs.LoadFromStreamThread = new Thread(new ParameterizedThreadStart(ImDocument.LoadDocOrComplectFromXmlInThread), 2000000);
        readArgs.LoadFromStreamThread.SetApartmentState(ApartmentState.STA);
        readArgs.LoadFromStreamThread.Name = "LoadImDocumentFromStreamThread";
        try
        {
          readArgs.LoadFromStreamThread.Start((object) readArgs);
        }
        catch
        {
          if (readArgs.LoadFromStreamThread.ThreadState != System.Threading.ThreadState.Running)
            readArgs.LoadFromStreamThread.Start((object) readArgs);
        }
        Monitor.Wait(readArgs.LockedObjectByLoadThread);
      }
      finally
      {
        Monitor.Exit(readArgs.LockedObjectByLoadThread);
      }
    }
    else
      ImDocument.LoadDocOrComplectFromXmlInThread((object) readArgs);
    if (readArgs.RootDocNode == null)
      return (DocumentTreeNode) null;
    return !readArgs.ThreadIsExternal ? ImDocument.PreparePartlyLoadedDoc(readArgs, updateDoc) : readArgs.RootDocNode as DocumentTreeNode;
  }

  /// <summary>Служебный метод, только для внутреннего пользования.
  /// Готовит документ к показу, пока он догружается в фоновом потоке</summary>
  /// <param name="readArgs">Параметры загрузки документа</param>
  /// <param name="updateDoc">Обновлять документ</param>
  /// <returns>Возвращает ссылку на загружаемый документ или комплект</returns>
  public static DocumentTreeNode PreparePartlyLoadedDoc(XmlReadArgs readArgs, bool updateDoc)
  {
    if (readArgs.RootDocNode is ImDocument rootDocNode1)
    {
      rootDocNode1.OnDeserialization((object) null);
      rootDocNode1.needUpdateLayoutFlag |= readArgs.IsDocData;
      if (((readArgs.IsInternalTemplate || readArgs.IsInternalFormulaLib ? 0 : (rootDocNode1.needUpdateLayoutFlag ? 1 : 0)) & (updateDoc ? 1 : 0)) != 0)
      {
        rootDocNode1.UpdateLayout(0, true, true, false, readArgs.ReadInThread);
        rootDocNode1.UpdatePageNumbers((PageData) null, rootDocNode1.StartComplectPageNumber, false, false, false);
        rootDocNode1.Modified = false;
      }
      else
        rootDocNode1.Modified = false;
      rootDocNode1.IsDocumentLoading = false;
      return (DocumentTreeNode) rootDocNode1;
    }
    if (readArgs.RootDocNode is DocumentsComplect rootDocNode2)
    {
      rootDocNode2.SetModifiedRecursive(false);
      rootDocNode2.SetIsDocumentLoadingRecursive(false);
    }
    return (DocumentTreeNode) rootDocNode2;
  }

  private void LoadDocumentFromXml(
    Stream stream,
    bool updateDoc,
    bool notCloseStream,
    bool loadInThread)
  {
    XmlReadArgs xmlReadArgs = new XmlReadArgs((XmlReader) new XmlTextReader(stream)
    {
      WhitespaceHandling = WhitespaceHandling.All
    });
    xmlReadArgs.ReadInThread = loadInThread;
    this.IsDocumentLoading = true;
    xmlReadArgs.NotCloseStream = notCloseStream;
    if (loadInThread)
    {
      Monitor.Enter(xmlReadArgs.LockedObjectByLoadThread = (object) xmlReadArgs);
      try
      {
        this.LoadFromStreamThread = new Thread(new ParameterizedThreadStart(this.LoadFromXmlInThread));
        this.LoadFromStreamThread.SetApartmentState(ApartmentState.STA);
        xmlReadArgs.LoadFromStreamThread = this.LoadFromStreamThread;
        this.LoadFromStreamThread.Name = "LoadImDocumentFromStreamThread";
        this.LoadFromStreamThread.Start((object) xmlReadArgs);
        Monitor.Wait(xmlReadArgs.LockedObjectByLoadThread);
      }
      finally
      {
        Monitor.Exit(xmlReadArgs.LockedObjectByLoadThread);
      }
    }
    else
      this.LoadFromXmlInThread((object) xmlReadArgs);
    this.OnDeserialization((object) null);
    this.needUpdateLayoutFlag |= xmlReadArgs.IsDocData;
    if (((xmlReadArgs.IsInternalTemplate || xmlReadArgs.IsInternalFormulaLib ? 0 : (this.needUpdateLayoutFlag ? 1 : 0)) & (updateDoc ? 1 : 0)) != 0)
    {
      this.Modified = true;
      this.UpdateLayout(0, true, true, false, false);
      this.UpdatePageNumbers((PageData) null, this.StartComplectPageNumber, false, false, false);
    }
    else
      this.Modified = false;
    this.IsDocumentLoading = false;
  }

  /// <summary>Загрузить документ XML из потока. Основной метод.</summary>
  /// <param name="stream">Поток содержащий документ</param>
  /// <param name="updateDoc">Обновить ссылки и разбивку в документе после загрузки</param>
  /// <param name="notCloseStream">Не закрывать поток после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом процессе</param>
  /// <returns>Документ</returns>
  public static ImDocument LoadFromXml(
    Stream stream,
    bool updateDoc,
    bool notCloseStream,
    bool loadInThread)
  {
    ImDocument imDocument = new ImDocument(false);
    imDocument.LoadDocumentFromXml(stream, updateDoc, notCloseStream, loadInThread);
    return imDocument;
  }

  /// <summary>Загрузить документ XML из потока.
  /// После загрузки поток закрывается в xmlReader.Close()!</summary>
  /// <param name="stream">Поток содержащий документ</param>
  /// <param name="updateDoc">Обновить ссылки и разбивку в документе после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом процессе</param>
  /// <returns>Документ</returns>
  public static ImDocument LoadFromXml(Stream stream, bool updateDoc, bool loadInThread)
  {
    return ImDocument.LoadFromXml(stream, updateDoc, false, loadInThread);
  }

  /// <summary>Загрузить документ из файла XML</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="updateDoc">Обновить ссылки и разбивку в документе после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом процессе</param>
  /// <returns>Документ</returns>
  public static ImDocument LoadFromXml(string fileName, bool updateDoc, bool loadInThread)
  {
    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
    ImDocument imDocument = ImDocument.LoadFromXml((Stream) fileStream, updateDoc, loadInThread);
    if (!loadInThread)
      fileStream.Close();
    imDocument.Modified = false;
    return imDocument;
  }

  /// <summary>Создать документ на основе бланка UEdit</summary>
  /// <param name="fileName">Имя файла бланка</param>
  /// <param name="blank">Загрузчик бланка</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  /// <returns>Документ на основе бланка</returns>
  public static ImDocument LoadFromOldBlank(string fileName, out BlankLoader blank)
  {
    blank = new BlankLoader();
    blank.LoadFile(fileName);
    ImDocument imDocument = blank.GeneateDocument();
    imDocument.Modified = true;
    return imDocument;
  }

  /// <summary>Создать документ на основе бланка UEdit</summary>
  /// <param name="stream">Поток данных</param>
  /// <param name="defaultPathForStdLib">Путь по умолчанию к библиотеке стандартных элементов</param>
  /// <param name="blank">Загрузчик бланка</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  /// <returns>Документ на основе бланка</returns>
  public static ImDocument LoadFromOldBlank(
    Stream stream,
    string defaultPathForStdLib,
    out BlankLoader blank,
    string preReadedHeaderSignature)
  {
    blank = new BlankLoader();
    if (defaultPathForStdLib != null && defaultPathForStdLib != "" && defaultPathForStdLib[defaultPathForStdLib.Length - 1] != '\\')
      defaultPathForStdLib += "\\";
    blank.LoadingFile = defaultPathForStdLib + "IM_STD.LIB";
    blank.Load(stream, preReadedHeaderSignature);
    ImDocument imDocument = blank.GeneateDocument();
    imDocument.Modified = true;
    return imDocument;
  }

  /// <summary>Загрузить библиотеку примитивов старого формата "LIB"</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="defaultPathForBlank">Путь по умолчанию для поиска бланка</param>
  /// <param name="ueDoc">Загрузчик документа</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  /// <returns>Документ</returns>
  public static ImDocument LoadFromPrimitiveLib(
    Stream stream,
    out PrimLibraryLoader primitiveLib,
    string preReadedHeaderSignature)
  {
    primitiveLib = new PrimLibraryLoader();
    primitiveLib.Load(stream, preReadedHeaderSignature);
    ImDocument imDocument = primitiveLib.GeneateDocument();
    imDocument.Modified = true;
    return imDocument;
  }

  /// <summary>Создать документ на основе документа UEdit</summary>
  /// <param name="fileName">Имя файла документа UEdit</param>
  /// <param name="ueDoc">Загрузчик документа</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  /// <returns>Документ</returns>
  public static ImDocument LoadFromUEditDocument(string fileName, out UEditDocument ueDoc)
  {
    ueDoc = new UEditDocument((GroupClone) null, (RectPrimitive) null);
    ueDoc.LoadFromFile(fileName);
    ImDocument newDocumentNode = (ImDocument) ueDoc.CreateNewDocumentNode((DocumentTreeNode) null);
    newDocumentNode.Modified = true;
    return newDocumentNode;
  }

  /// <summary>Создать документ на основе документа UEdit</summary>
  /// <param name="fileName">Имя файла документа UEdit</param>
  /// <param name="defaultPathForBlank">Путь по умолчанию для поиска бланка</param>
  /// <param name="ueDoc">Загрузчик документа</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  /// <returns>Документ</returns>
  public static ImDocument LoadFromUEditDocument(
    Stream stream,
    string defaultPathForBlank,
    out UEditDocument ueDoc,
    string preReadedHeaderSignature)
  {
    ueDoc = new UEditDocument((GroupClone) null, (RectPrimitive) null);
    ueDoc.Load(stream, defaultPathForBlank, preReadedHeaderSignature);
    ImDocument newDocumentNode = (ImDocument) ueDoc.CreateNewDocumentNode((DocumentTreeNode) null);
    newDocumentNode.Modified = true;
    return newDocumentNode;
  }

  /// <summary>Загрузить документ из файла. Анализирует
  /// Анализирует заголовок и может грузить файлы разных версий.</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="docType">Возвращает тип документа</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <returns>Загруженный документ</returns>
  public static DocumentTreeNode LoadFromFile(
    string fileName,
    out DocumentFileType docType,
    bool loadInThread)
  {
    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
    string directoryName = Path.GetDirectoryName(fileName);
    FileInfo fileInfo = new FileInfo(fileName);
    XmlReadArgs readArgs = new XmlReadArgs()
    {
      FileName = fileName,
      FileSize = fileStream.Length,
      FileModifyDate = new DateTime?(fileInfo.LastWriteTime)
    };
    return ImDocument.LoadFromStream((Stream) fileStream, directoryName, out docType, true, loadInThread, true, readArgs);
  }

  /// <summary>Получить формат документа исходя из расширения или сигнатур в бинарном потоке</summary>
  /// <param name="stream"></param>
  /// <param name="fileName"></param>
  /// <returns></returns>
  private static DocumentFileType GetFileFormat(Stream stream, string fileName)
  {
    DocumentFileType fileFormat = ImDocument.GetDocumentFileTypeFromExtension(fileName);
    switch (fileFormat)
    {
      case DocumentFileType.Unknown:
      case DocumentFileType.OldBlank:
        fileFormat = ImDocument.GetBinaryFileFormat(stream);
        break;
    }
    return fileFormat;
  }

  /// <summary>Получить тип документа по расширению имени файла</summary>
  /// <param name="fileName">Имя файла</param>
  /// <returns></returns>
  private static DocumentFileType GetDocumentFileTypeFromExtension(string fileName)
  {
    string extensionWithoutDot = ImDocumentData.GetFileExtensionWithoutDot(fileName);
    if (string.IsNullOrEmpty(extensionWithoutDot))
      return DocumentFileType.Unknown;
    if (ImDocumentData.IsImDocumentExtension(extensionWithoutDot))
      return extensionWithoutDot.Substring(0, 1).ToUpper() == "Z" ? DocumentFileType.ImDocument_IsPacked : DocumentFileType.ImDocument;
    if (ImDocumentData.IsOldAVSExtension(extensionWithoutDot))
      return DocumentFileType.OldAVS;
    if (ImDocumentData.IsOldBlankExtension(extensionWithoutDot))
      return DocumentFileType.OldBlank;
    return ImDocumentData.IsOldImDocumentExtension(extensionWithoutDot) ? DocumentFileType.OldUEditDocument : DocumentFileType.Unknown;
  }

  /// <summary>Получить формат документа исходя из сигнатур в бинарном потоке</summary>
  /// <param name="stream">Поток</param>
  /// <returns></returns>
  private static DocumentFileType GetBinaryFileFormat(Stream stream)
  {
    DocumentFileType binaryFileFormat = DocumentFileType.Unknown;
    byte[] buffer = new byte[128 /*0x80*/];
    stream.Read(buffer, 0, 128 /*0x80*/);
    stream.Position = 0L;
    BinaryReader binaryReader = new BinaryReader((Stream) new MemoryStream(buffer), Encoding.GetEncoding(1251));
    try
    {
      string str = new string(binaryReader.ReadChars(50));
      if (str.IndexOf(BlankLoader.BlankSign) == 0)
        binaryFileFormat = DocumentFileType.OldBlank;
      else if (str.IndexOf(UEditDocument.File_Sign) == 0)
        binaryFileFormat = DocumentFileType.OldUEditDocument;
      else if (str.IndexOf(PrimLibraryLoader.File_Sign) == 0)
        binaryFileFormat = DocumentFileType.OldPrimitiveLib;
      else if (str.IndexOf("PK") == 0)
        binaryFileFormat = DocumentFileType.ImDocument_IsPacked;
      else if (str.IndexOf("iSP2") == 0)
        binaryFileFormat = DocumentFileType.OldAVS;
      else if (str.Contains("?xml"))
      {
        if (str.Contains("version=\"1.0\""))
          binaryFileFormat = DocumentFileType.ImDocument;
      }
    }
    finally
    {
      binaryReader.Close();
    }
    return binaryFileFormat;
  }

  /// <summary>Загрузить документ из потока. Основной метод
  /// Анализирует заголовок и может грузить файлы разных версий.</summary>
  /// <param name="stream">Поток с документом или комплектом документов</param>
  /// <param name="filePath">Путь к файлу, если загружается из файла. Необходимо для документов старого формата</param>
  /// <param name="docType">Возвращает тип документа</param>
  /// <param name="updateDoc">Обновлять документ после загрузки (ссылки, разбивку на страницы)</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <param name="failIfUnknownFormat">Генерировать исключение, если формат файла не определён</param>
  /// <param name="readArgs">Аргументы загрузки.
  /// Необходимы при загрузке с внешним фоновым процессом</param>
  /// <returns>Загруженный документ</returns>
  public static DocumentTreeNode LoadFromStream(
    Stream stream,
    string filePath,
    out DocumentFileType docType,
    bool updateDoc,
    bool loadInThread,
    bool failIfUnknownFormat,
    XmlReadArgs readArgs = null)
  {
    DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
    string fileName = readArgs != null ? readArgs.FileName : "";
    docType = ImDocument.GetFileFormat(stream, fileName);
    if (docType == DocumentFileType.OldBlank)
    {
      BlankLoader blank = (BlankLoader) null;
      documentTreeNode = (DocumentTreeNode) ImDocument.LoadFromOldBlank(stream, filePath, out blank, (string) null);
    }
    else if (docType == DocumentFileType.OldUEditDocument)
    {
      UEditDocument ueDoc = (UEditDocument) null;
      documentTreeNode = (DocumentTreeNode) ImDocument.LoadFromUEditDocument(stream, filePath, out ueDoc, (string) null);
    }
    else if (docType != DocumentFileType.OldAVS)
    {
      if (docType == DocumentFileType.OldPrimitiveLib)
      {
        PrimLibraryLoader primitiveLib = (PrimLibraryLoader) null;
        documentTreeNode = (DocumentTreeNode) ImDocument.LoadFromPrimitiveLib(stream, out primitiveLib, (string) null);
      }
      else if (docType == DocumentFileType.ImDocument_IsPacked)
      {
        using (ZipInputStream zipInputStream = new ZipInputStream(stream))
        {
          while (zipInputStream.GetNextEntry() != null)
          {
            documentTreeNode = ImDocument.LoadDocOrComplectFromXml((Stream) zipInputStream, updateDoc, true, false, readArgs);
            if (documentTreeNode is ImDocument)
              docType = DocumentFileType.ImDocument_IsPacked;
            else if (documentTreeNode is DocumentsComplect)
              docType = DocumentFileType.ImDocumentsComplect_IsPacked;
          }
        }
      }
      else if (docType == DocumentFileType.ImDocument)
      {
        documentTreeNode = ImDocument.LoadDocOrComplectFromXml(stream, updateDoc, false, loadInThread, readArgs);
        switch (documentTreeNode)
        {
          case ImDocument _:
            docType = DocumentFileType.ImDocument;
            break;
          case DocumentsComplect _:
            docType = DocumentFileType.ImDocumentsComplect;
            break;
        }
      }
      else if (failIfUnknownFormat)
        throw new Exception(LocalizationHolder.rm.GetString("Document.Model_491"));
    }
    return documentTreeNode;
  }

  /// <summary>Загрузить документ из файла.
  /// Анализирует заголовок и может грузить файлы разных версий.</summary>
  /// <param name="stream">Поток с документом или комплектом документов</param>
  /// <param name="updateDoc">Обновлять документ после загрузки (ссылки, разбивку на страницы)</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <param name="failIfUnknownFormat">Генерировать исключение, если формат файла неопределён</param>
  /// <returns>Загруженный документ</returns>
  public static ImDocument LoadFromStream(
    Stream stream,
    bool updateDoc,
    bool loadInThread,
    bool failIfUnknownFormat)
  {
    return ImDocument.LoadFromStream(stream, (string) null, updateDoc, loadInThread, failIfUnknownFormat);
  }

  /// <summary>Загрузить документ из файла.
  /// Анализирует заголовок и может грузить файлы разных версий.</summary>
  /// <param name="stream">Поток с документом или комплектом документов</param>
  /// <param name="filePath">Путь к файлу, если загружается из файла. Необходимо для документов старого формата</param>
  /// <param name="updateDoc">Обновлять документ после загрузки (ссылки, разбивку на страницы)</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <param name="failIfUnknownFormat">Генерировать исключение, если формат файла неопределён</param>
  /// <returns>Загруженный документ</returns>
  public static ImDocument LoadFromStream(
    Stream stream,
    string filePath,
    bool updateDoc,
    bool loadInThread,
    bool failIfUnknownFormat)
  {
    return ImDocument.LoadFromStream(stream, filePath, out DocumentFileType _, updateDoc, loadInThread, failIfUnknownFormat) as ImDocument;
  }

  /// <summary>Ссылка на источник данных</summary>
  [Editor(typeof (ReferenceToObjectUIEditor), typeof (UITypeEditor))]
  public override ReferenceBase Reference
  {
    get => base.Reference;
    set => base.Reference = value;
  }

  /// <summary>Создать список формул. Только для внутреннего пользования</summary>
  protected override void CreateFormulaList()
  {
    this.formulaList = (ImDocumentData) new ImDocument(false, false);
    this.formulaList.AssignIsFormulaLib(true);
  }

  /// <summary>Обновить словарь формул</summary>
  private void UpdateFormulaIndex()
  {
    if (this.IsFormulaLib)
      this.UpdateFormulaIndex((DocumentTreeNode) this);
    else if (this.formulaList != null)
    {
      this.formulaIndex = new Dictionary<string, PageData>(this.formulaList.Nodes.Count);
      this.UpdateFormulaIndex((DocumentTreeNode) this.formulaList);
    }
    else
      this.formulaIndex = new Dictionary<string, PageData>();
  }

  /// <summary>Обновить словарь формул</summary>
  /// <param name="node">Ветка с формулами, которую нужно сканировать</param>
  private void UpdateFormulaIndex(DocumentTreeNode node)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    if (node.Nodes == null)
      return;
    for (int index = 0; index < node.Nodes.Count; ++index)
    {
      if (node.Nodes[index] is PageData node1)
      {
        string upper;
        if (!this.formulaIndex.ContainsKey(upper = node1.Id.ToUpper()))
          this.formulaIndex.Add(upper, node1);
        else if (upper == node1.Id)
          this.formulaIndex[upper] = node1;
      }
      else
        this.UpdateFormulaIndex(node.Nodes[index]);
    }
  }

  /// <summary>Создать сервисный объект со списком формул</summary>
  /// <param name="formulaText">Текст формулы</param>
  /// <returns></returns>
  public FormList FindFormulas(string formulaText)
  {
    FormList formulas = new FormList(formulaText);
    List<Formula> list = formulas.List;
    if (this.formulaList == null)
      this.CreateFormulaList();
    ImDocumentData documentTemplate = this.DocumentTemplate;
    for (int index = 0; index < list.Count; ++index)
    {
      if (!(this.formulaList.FindNode(list[index].Id) is PageData child) && documentTemplate != null && documentTemplate.FormulaList != null)
        child = documentTemplate.FormulaList.FindNode(list[index].Id) as PageData;
      if (child == null)
      {
        bool flag = false;
        if (this.formulaIndex == null)
        {
          this.UpdateFormulaIndex();
          flag = true;
        }
        string upper = list[index].Id.ToUpper();
        if (this.formulaIndex.ContainsKey(upper))
        {
          child = this.formulaIndex[upper];
          if (!flag && child.OwnerDocument != this.formulaList)
            child = (PageData) null;
        }
        if (child == null && !flag)
        {
          this.UpdateFormulaIndex();
          if (this.formulaIndex.ContainsKey(upper))
            child = this.formulaIndex[upper];
        }
        if (child == null)
        {
          child = this.FindFormulaInLib(upper);
          if (child != null)
          {
            child = child.Clone() as PageData;
            this.formulaList.AddChildNode((DocumentTreeNode) child, false, false);
            if (!this.formulaIndex.ContainsKey(upper))
              this.formulaIndex.Add(upper, child);
          }
        }
      }
      if (child != null)
        list[index].page = (Page) child;
    }
    return formulas;
  }

  public PageData FindFormulaInLib(string formulaId)
  {
    string str = !string.IsNullOrEmpty(formulaId) ? formulaId.ToUpper() : throw new ArgumentNullException(nameof (formulaId));
    PageData formulaInLib = (PageData) null;
    if (TemplateHolderBase.Instance != null)
    {
      if (TemplateHolderBase.Instance.templates == null || TemplateHolderBase.Instance.templates.Count == 0)
        TemplateHolderBase.Instance.LoadTemplates();
      if (TemplateHolderBase.Instance.templates != null && TemplateHolderBase.Instance.templates.ContainsKey((object) str) && TemplateHolderBase.Instance.templates[(object) str] is FormSearch template)
        formulaInLib = template.node as PageData;
    }
    if (formulaInLib == null)
    {
      Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.defaultformulas.imdx");
      if (manifestResourceStream != null)
      {
        ImDocument imDocument = ImDocument.LoadFromStream(manifestResourceStream, false, false, false);
        if (imDocument != null)
          formulaInLib = imDocument.FindNode(str) as PageData;
      }
    }
    return formulaInLib;
  }

  public void UpdateFormulasInDocument()
  {
    this.UpdateFormulasInTextBox();
    this.UpdateLayout(false, true);
  }

  /// <summary>Освободить все ресурсы</summary>
  public override void Dispose()
  {
    base.Dispose();
    if (this.documentControl != null)
    {
      this.documentControl.Document = (ImDocument) null;
      this.documentControl = (DocumentControl) null;
    }
    try
    {
      if (this.Template != null)
        this.Template.Dispose();
      lock (this.RelAttrCache)
        this.RelAttrCache.Clear();
      lock (this.ObjAttrCache)
        this.ObjAttrCache.Clear();
      this.ClearRtfEditorCaches();
    }
    catch (Exception ex)
    {
      LogManager.AddLine(ex);
    }
  }

  public void ClearRtfEditorCaches()
  {
    RtfInSiteEditorWrapper.ClearPaintCache(this);
    this.TernDistributeSpecSymbolsBuffer = (ImRtfEditor) null;
    this.TernSpecSymbolsBuffer = (ImRtfEditor) null;
    this.TernDistributeSpecSymbolsBufferB = (ImRtfEditor) null;
    this.TernSpecSymbolsBufferB = (ImRtfEditor) null;
    this.TernPaintBuffer = (ImRtfEditor) null;
    this.TernPrintBuffer = (ImRtfEditor) null;
    if (this.ImOleContainerBuffer == null)
      return;
    this.ImOleContainerBuffer.Parent = (Control) null;
    this.ImOleContainerBuffer.Dispose();
    this.ImOleContainerBuffer = (ImOleContainer) null;
  }

  /// <summary>Контрол документа</summary>
  [Browsable(false)]
  public DocumentControl documentControl
  {
    [DebuggerStepThrough] get => this._documentControl;
    set => this._documentControl = value;
  }

  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  [Browsable(false)]
  public ImRtfEditor TernPaintBuffer
  {
    get => this.ternPaintBuffer;
    set
    {
      if (this.ternPaintBuffer == value)
        return;
      if (this.ternPaintBuffer != null && !this.ternPaintBuffer.InvokeRequired)
        this.ternPaintBuffer.Dispose();
      this.ternPaintBuffer = value;
      if (this.ternPaintBuffer == null)
        return;
      this.ternPaintBuffer.Name = "doc.TernPaintBuffer";
    }
  }

  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  [Browsable(false)]
  public ImRtfEditor TernFontMetricsBuffer
  {
    get => this.ternFontMetricsBuffer;
    set
    {
      if (this.ternFontMetricsBuffer == value)
        return;
      if (this.ternFontMetricsBuffer != null && !this.ternFontMetricsBuffer.InvokeRequired)
        this.ternFontMetricsBuffer.Dispose();
      this.ternFontMetricsBuffer = value;
      if (this.ternFontMetricsBuffer == null)
        return;
      this.ternFontMetricsBuffer.Name = "doc.TernFontMetricsBuffer";
    }
  }

  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  [Browsable(false)]
  public ImRtfEditor TernDistributeSpecSymbolsBufferB
  {
    get
    {
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      return documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this ? firstDocument.TernDistributeSpecSymbolsBufferB : this.ternDistributeSpecSymbolsBufferB;
    }
    set
    {
      if (this.ternDistributeSpecSymbolsBufferB != null && this.ternDistributeSpecSymbolsBufferB != value && !this.ternDistributeSpecSymbolsBufferB.InvokeRequired)
        this.ternDistributeSpecSymbolsBufferB.Dispose();
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      if (documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this)
      {
        this.ternDistributeSpecSymbolsBufferB = (ImRtfEditor) null;
        firstDocument.TernDistributeSpecSymbolsBufferB = value;
      }
      else
      {
        this.ternDistributeSpecSymbolsBufferB = value;
        if (this.ternDistributeSpecSymbolsBufferB == null)
          return;
        this.ternDistributeSpecSymbolsBufferB.Name = "ternDistributeSpecSymbolsBufferB";
      }
    }
  }

  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  [Browsable(false)]
  public ImRtfEditor TernSpecSymbolsBufferB
  {
    get
    {
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      return documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this ? firstDocument.TernSpecSymbolsBufferB : this.ternSpecSymvolsBufferB;
    }
    set
    {
      if (this.ternSpecSymvolsBufferB != null && this.ternSpecSymvolsBufferB != value && !this.ternSpecSymvolsBufferB.InvokeRequired)
        this.ternSpecSymvolsBufferB.Dispose();
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      if (documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this)
      {
        this.ternSpecSymvolsBufferB = (ImRtfEditor) null;
        firstDocument.TernSpecSymbolsBufferB = value;
      }
      else
      {
        this.ternSpecSymvolsBufferB = value;
        if (this.ternSpecSymvolsBufferB == null)
          return;
        this.ternSpecSymvolsBufferB.Name = "ternSpecSymvolsBufferB";
      }
    }
  }

  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  [Browsable(false)]
  public ImRtfEditor TernDistributeSpecSymbolsBuffer
  {
    get
    {
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      return documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this ? firstDocument.TernDistributeSpecSymbolsBuffer : this.ternDistributeSpecSymbolsBuffer;
    }
    set
    {
      if (this.ternDistributeSpecSymbolsBuffer != null && this.ternDistributeSpecSymbolsBuffer != value && !this.ternDistributeSpecSymbolsBuffer.InvokeRequired)
        this.ternDistributeSpecSymbolsBuffer.Dispose();
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      if (documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this)
      {
        this.ternDistributeSpecSymbolsBuffer = (ImRtfEditor) null;
        firstDocument.TernDistributeSpecSymbolsBuffer = value;
      }
      else
      {
        this.ternDistributeSpecSymbolsBuffer = value;
        if (this.ternDistributeSpecSymbolsBuffer == null)
          return;
        this.ternDistributeSpecSymbolsBuffer.Name = "ternDistributeSpecSymbolsBuffer";
      }
    }
  }

  /// <summary>Буферный экземпляр ImRtfEditor</summary>
  [Browsable(false)]
  public ImRtfEditor TernSpecSymbolsBuffer
  {
    get
    {
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      return documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this ? firstDocument.TernSpecSymbolsBuffer : this.ternSpecSymvolsBuffer;
    }
    set
    {
      if (this.ternSpecSymvolsBuffer != null && this.ternSpecSymvolsBuffer != value && !this.ternSpecSymvolsBuffer.InvokeRequired)
        this.ternSpecSymvolsBuffer.Dispose();
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      if (documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this)
      {
        this.ternSpecSymvolsBuffer = (ImRtfEditor) null;
        firstDocument.TernSpecSymbolsBuffer = value;
      }
      else
      {
        this.ternSpecSymvolsBuffer = value;
        if (this.ternSpecSymvolsBuffer == null)
          return;
        this.ternSpecSymvolsBuffer.Name = "ternSpecSymvolsBuffer";
      }
    }
  }

  /// <summary>Буферный экземпляр ImRtfEditor для печати</summary>
  [Browsable(false)]
  public ImRtfEditor TernPrintBuffer
  {
    get
    {
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      return documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this ? firstDocument.TernPrintBuffer : this.ternPrintBuffer;
    }
    set
    {
      if (this.ternPrintBuffer != null && this.ternPrintBuffer != value && !this.ternPrintBuffer.InvokeRequired)
        this.ternPrintBuffer.Dispose();
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      if (documentsComplect != null && DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument && firstDocument != this)
      {
        this.ternPrintBuffer = (ImRtfEditor) null;
        firstDocument.TernPrintBuffer = value;
      }
      else
      {
        this.ternPrintBuffer = value;
        if (this.ternPrintBuffer == null)
          return;
        this.ternPrintBuffer.Name = "ternPrintBuffer";
      }
    }
  }

  internal static ImRtfEditor GetCurrentTernDistributeBufferFromPool(
    ConcurrentDictionary<Thread, ImRtfEditor> ternDistributeBufferPool)
  {
    ImRtfEditor distributeBuffer;
    if (!ternDistributeBufferPool.TryGetValue(Thread.CurrentThread, out distributeBuffer))
    {
      distributeBuffer = RtfInSiteEditorWrapper.CreateTernDistributeBuffer();
      distributeBuffer.Name = Thread.CurrentThread.Name;
      ternDistributeBufferPool.TryAdd(Thread.CurrentThread, distributeBuffer);
    }
    return distributeBuffer;
  }

  internal static void RemoveCurrentTernDistributeBufferEditorFromPool(
    ConcurrentDictionary<Thread, ImRtfEditor> ternDistributeBufferPool)
  {
    ImRtfEditor imRtfEditor;
    if (!ternDistributeBufferPool.TryRemove(Thread.CurrentThread, out imRtfEditor) || imRtfEditor == null || imRtfEditor.IsDisposed || imRtfEditor.InvokeRequired)
      return;
    imRtfEditor.Dispose();
  }

  /// <summary>Буферный экземпляр ImRtfEditor для разбивки текста</summary>
  [Browsable(false)]
  public static ImRtfEditor TernDistributeBuffer
  {
    get => ImDocument.GetCurrentTernDistributeBufferFromPool(ImDocument.TernDistributeBufferPool);
  }

  public static void ReleaseMainDistributeBuffer()
  {
    ImDocument.RemoveCurrentTernDistributeBufferEditorFromPool(ImDocument.TernDistributeBufferPool);
  }

  /// <summary>Буферный экземпляр ImRtfEditor для разбивки текста в формулах</summary>
  [Browsable(false)]
  public static ImRtfEditor TernDistributeBufferInFormula
  {
    get
    {
      return ImDocument.GetCurrentTernDistributeBufferFromPool(ImDocument.TernDistributeBufferPoolInFormula);
    }
  }

  public static void ReleaseFormulaDistributeBuffer()
  {
    ImDocument.RemoveCurrentTernDistributeBufferEditorFromPool(ImDocument.TernDistributeBufferPoolInFormula);
  }

  /// <summary>Буферный экземпляр ImRtfEditor для рисования текста в метафайл в формулах и при выводе на печать</summary>
  [Browsable(false)]
  public static ImRtfEditor TernDistributeBufferForPrint
  {
    get
    {
      return ImDocument.GetCurrentTernDistributeBufferFromPool(ImDocument.TernDistributeBufferPoolForPrint);
    }
  }

  public static void ReleasePrintDistributeBuffer()
  {
    ImDocument.RemoveCurrentTernDistributeBufferEditorFromPool(ImDocument.TernDistributeBufferPoolForPrint);
  }

  [Flags]
  private enum EmfToWmfBitsFlags
  {
    EmfToWmfBitsFlagsDefault = 0,
    EmfToWmfBitsFlagsEmbedEmf = 1,
    EmfToWmfBitsFlagsIncludePlaceable = 2,
    EmfToWmfBitsFlagsNoXORClip = 4,
  }
}
