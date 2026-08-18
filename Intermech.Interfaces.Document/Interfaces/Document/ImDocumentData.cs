// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ImDocumentData
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using ICSharpCode.SharpZipLib.Zip;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Данные документа. Базовый класс документа</summary>
[Serializable]
public class ImDocumentData : 
  VisualNode,
  IImDocument,
  INodeWithReference,
  IEnumerable<PageData>,
  IEnumerable
{
  /// <summary>Имя типа для словаря конструкторов</summary>
  public static string TypeNameForConstructorDictionary = "Document";
  /// <summary>Документы интермех и шаблоны</summary>
  public static List<string> ImDocumentFileExtensions = new List<string>()
  {
    "imdx",
    "zimd",
    "spx",
    "pex",
    "revx",
    "idcx"
  };
  /// <summary>Бланки интермех формата BLANKS2/Search</summary>
  public static List<string> OldBlankExtensions = new List<string>()
  {
    "bln",
    "lib"
  };
  /// <summary>
  /// Форматы старых документов интермех Search.
  /// Временно отключены, так как конвертация не отлажена и содержит ошибки (например файлы rev не удалось прочитать)
  /// </summary>
  public static List<string> OldImDocumentExtensions = new List<string>()
  {
    "imd",
    "pe",
    "rev",
    "cc",
    "rep"
  };
  /// <summary>Внешние документы встраиваемые в ImDocument</summary>
  public static List<string> ImDocumentExternalFileExtensions = new List<string>()
  {
    "doc",
    "docx",
    "imdoc",
    "imdocx"
  };
  /// <summary>
  /// Внешние документы встроенные в ImDocument и поддерживаемые на закладке просмотр
  /// </summary>
  public static List<string> ImDocumentExternalFileExtensionsVisualizer = new List<string>()
  {
    "imdoc",
    "imdocx"
  };
  [NonSerialized]
  private PageUnlocked_EventHandler pageUnlocked;
  [NonSerialized]
  private PageUnlocked_EventHandler pageLoaded;
  [NonSerialized]
  private BackgroundThreadsFinished_EventHandler backgroundThreadsFinished;
  public const string DistributeThreadName = "DistributeThread";
  /// <summary>Только для внутреннего использования.
  /// Обозначение документа, которое хранилось в XML до замены обозначением из документа в БД.</summary>
  public string LoadedFromXMLDesignation;
  private Guid documentComplectObjectGuid = Guid.Empty;
  private ObjectModifyModes? dbObjectModifyModes;
  /// <summary>Кол-во страниц в комплекте</summary>
  public static readonly string AttributePagesComplectCount = "PagesInComplect";
  public static readonly string AttributeOriginalTemplateGuid = "OriginalTemplateGuid";
  /// <summary>Контрольная сумма</summary>
  internal string checkSum = "";
  internal string printUser = "";
  internal string printDate = "";
  private bool nowPrinting;
  /// <summary>Отобразить информацию о возникшей исключительной ситуации (Exception)</summary>
  public static ShowExceptionDialogDelegate ShowExceptionDialog;
  /// <summary>
  /// Флаг для принудительного сохранения значений атрибутов полученных из БД
  /// Используется при выгрузке файла на диск со всеми значениями
  /// </summary>
  internal bool ForceSaveValuesFromRefToDBAttr;
  [NonSerialized]
  private AfterUpdatePageNumbers_EventHandler afterUpdatePageNumbers;
  [NonSerialized]
  private ModifiedChanged_EventHandler modifiedChanged;
  [NonSerialized]
  private DistributePageFinished_EventHandler distributePageFinished;
  [NonSerialized]
  private PageDistribute_EventHandler beforeDistributePage;
  [NonSerialized]
  private TextValidating_EventHandler textValidating;
  [NonSerialized]
  private TextChanged_EventHandler textChanged;
  [NonSerialized]
  private PageDistribute_EventHandler afterDistributePage;
  [NonSerialized]
  private TemplateChanged_EventHandler templateChanged;
  [NonSerialized]
  private TemplateChanging_EventHandler templateChanging;
  [NonSerialized]
  private EventHandler beforeSave;
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict;
  private static List<string> complexDesignationSuffixs = new List<string>();
  protected const bool SuspendUpdateDocumentAfterLoad = false;
  private string revision;
  private static IDocumentNotifyService notifyService;
  private string designation;
  protected ImDocumentData formulaList;
  protected List<string> materialKeyWords;
  private bool allowFormatingForReadOnlyText;
  private bool saveValueFromRefToDBAttr;
  private PointF shiftPage = (PointF) Point.Empty;
  private bool fitToPage = true;
  private int startPageNumber = 1;
  private int startComplectPageNumber = 1;
  /// <summary>Вспомогательное поле для хранения в xml количества страниц
  /// и отображения его во время фоновой загрузки документа</summary>
  private int? savedPageCount;
  private ReferenceBase reference;
  private List<FlowID> documentFlows = new List<FlowID>(1);
  private bool isTemplate;
  private bool isFormulaLib;
  private bool dbAttributeAutoSave;
  private Color defaultForeColor = PageElementNode.DefaultForeColor;
  private Color defaultBackColor = PageElementNode.DefaultBackColor;
  private CharFormat defaultCharFormat;
  private ParagraphFormat defaultParagraphFormat;
  private bool defaultDrawParentCellFrames = true;
  /// <summary>Размер правого и левого полей по умолчанию</summary>
  public float defaultLeftRightMargin = 0.529166f;
  /// <summary>Размер полей сверху и снизу по умолчанию</summary>
  public float DefaultTopBottomMargin;
  private float fixedRowSizeTrancateFraction = 0.2f;
  /// <summary>Стиль линии по умолчанию</summary>
  private BorderLine defaultBorderLine = new BorderLine();
  /// <summary>Стиль линии по умолчанию для границ страницы</summary>
  private BorderLine defaultPageBorderLine;
  public bool? defaultNonSkipAtStartPage;
  private bool? isPartOfComplectPageNumbering;
  private bool? isPartOfComplectPageCount;
  [NonSerialized]
  public PageThreadStatus pageThreadStatus = new PageThreadStatus();
  /// <summary>Запущен процесс разбивки документа</summary>
  [NonSerialized]
  protected bool isDistributing;
  /// <summary>Идёт загрузка документа из файла</summary>
  [NonSerialized]
  public bool IsFileLoading;
  /// <summary>Идёт загрузка данных или генерация документа</summary>
  [NonSerialized]
  public bool IsDocumentLoading;
  /// <summary>Индекс файлового атрибута в котором хранится документ.
  /// <remark>Только для DB.
  /// При конвертации файлов старого формата не заполняется.
  /// Используется при сохранении документа в тот же атрибут, из которого он был загружен.</remark>
  /// </summary>
  [NonSerialized]
  public int FileAttributeIndex = -1;
  /// <summary>Идентификатор файлового атрибута в котором хранится документ.
  /// <remark>Только для DB.
  /// При конвертации файлов старого формата не заполняется.
  /// Используется при сохранении документа в тот же атрибут, из которого он был загружен.</remark>
  /// </summary>
  [NonSerialized]
  public int FileAttributeID = -1;
  [NonSerialized]
  private ImDocumentData templateOwner;
  [NonSerialized]
  private ImPrintSettings imPrintSettings = new ImPrintSettings();
  [NonSerialized]
  private PrintDocument printDocument;
  [NonSerialized]
  private bool modified;
  [NonSerialized]
  private bool saveModificationDate;
  [NonSerialized]
  private DateTime? savedDateTime;
  [NonSerialized]
  private DateTime? fileModifyDate;
  [NonSerialized]
  private long? fileSize;
  /// <summary>Фоновый процесс загрузки документа</summary>
  [NonSerialized]
  public Thread LoadFromStreamThread;
  /// <summary>Фоновый процесс разбивки документа</summary>
  [NonSerialized]
  public Thread DistributeThread;
  [NonSerialized]
  private bool _isSuspendedUpdatesFromDB;
  /// <summary>Кэш объектов AttributeProcessor</summary>
  [NonSerialized]
  public object dBAttributeProcessorDictionary;
  /// <summary>Кэш в котором хранятся подписи документа</summary>
  [NonSerialized]
  private Dictionary<long, ArrayList> signes = new Dictionary<long, ArrayList>();
  /// <summary>Кэш значений атрибутов объектов системы</summary>
  [NonSerialized]
  private Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache = new Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>();
  /// <summary>Кэш значений атрибутов связей системы</summary>
  [NonSerialized]
  private Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache = new Dictionary<Guid, Dictionary<Guid, AttributeValueCache>>();
  /// <summary>Кэш информаций об объектах для ссылок по гуиду версии</summary>
  [NonSerialized]
  private Dictionary<Guid, Intermech.Interfaces.Document.DBObjectInfo> objectsInfoGuid = new Dictionary<Guid, Intermech.Interfaces.Document.DBObjectInfo>();
  /// <summary>
  /// Кэш информаций об объектах для ссылок по идентификатору версии
  /// </summary>
  [NonSerialized]
  private Dictionary<long, Intermech.Interfaces.Document.DBObjectInfo> objectsInfoId = new Dictionary<long, Intermech.Interfaces.Document.DBObjectInfo>();
  private UpdateReferencesMode updateReferencesMode = UpdateReferencesMode.All;
  /// <summary>Показывать в интерфейсе отладочную информацию и команды</summary>
  public static bool ShowDebugInfo = false;
  /// <summary>Версия приложения сохранившего документ. Только начиная с документов версии 40</summary>
  private string LoadedFileProductVersion;

  /// <summary>Статический конструктор</summary>
  static ImDocumentData()
  {
    ImDocumentData.InitReadFieldDict();
    ImDocumentData.BindPropertiesToAttributes();
  }

  protected static void BindPropertiesToAttributes()
  {
    DocumentTreeNode.BindPropertyToAdditionalAttribute("DynamicGroupHeaderIsEnabled", typeof (bool), converter: (TypeConverter) new CustomBooleanConverter());
  }

  private void InitFields(bool withTemplate)
  {
    this.IdService = (IUniqueIdService) new UniqueIdGenerator();
    this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
    if (!this.isFormulaLib)
      this.CreateFormulaList();
    if (!withTemplate)
      return;
    this.AssignDocumentTemplate(ImDocumentData.CreateTemplate(this.GetType(), true), false, false, false);
  }

  /// <summary>Конструктор. Может автоматически создавать интерфейс пользователя</summary>
  /// <param name="autoCreateUI">Создать интерфейс пользователя</param>
  /// <param name="withTemplate">Создавать пустой шаблон</param>
  public ImDocumentData(bool autoCreateUI, bool withTemplate)
  {
    this.InitFields(withTemplate);
    if (!autoCreateUI)
      return;
    this.CreateUI();
  }

  /// <summary>Конструктор. Может автоматически создавать интерфейс пользователя</summary>
  /// <param name="autoCreateUI">Создать интерфейс пользователя</param>
  /// <param name="withTemplate">Создавать пустой шаблон</param>
  public ImDocumentData(bool autoCreateUI, bool withTemplate, bool isFormulaLib)
  {
    if (LogManager.CreateLog && !isFormulaLib)
      LogManager.AddLine($"ImDocumentData(autoCreateUI:{autoCreateUI}, withTemplate:{withTemplate}, isFormulaLib:{isFormulaLib}) - START");
    this.isFormulaLib = isFormulaLib;
    this.InitFields(withTemplate);
    if (autoCreateUI)
      this.CreateUI();
    if (!LogManager.CreateLog || isFormulaLib)
      return;
    LogManager.AddLine($"ImDocumentData(autoCreateUI:{autoCreateUI}, withTemplate:{withTemplate}, isFormulaLib:{isFormulaLib}) - END");
  }

  /// <summary>Конструктор для документа входящего в структуру более высокого уровня</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="autoCreateUI">Создавать интерфейс пользователя</param>
  /// <param name="withTemplate">Создавать пустой шаблон</param>
  public ImDocumentData(DocumentTreeNode parent, bool autoCreateUI, bool withTemplate)
  {
    int num = this.SuspendedUpdateUIGeometryFlag ? 1 : 0;
    if (num == 0)
      this.SuspendUpdateUIGeometry();
    this.InitFields(withTemplate);
    if (autoCreateUI)
      this.CreateUI();
    this.SetParent(parent, false, false);
    if (num != 0)
      return;
    this.ResumeUpdateUIGeometry(true, true);
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected ImDocumentData(SerializationInfo info, StreamingContext context)
  {
    LogManager.AddLine("ImDocumentData(SerializationInfo info, StreamingContext context) - START");
    Stream stream = (Stream) new MemoryStream((byte[]) info.GetValue("Stream", typeof (byte[])));
    stream.Position = 0L;
    this.LoadDocumentDataFromXml(stream, new XmlReadArgs());
    LogManager.AddLine("ImDocumentData(SerializationInfo info, StreamingContext context) - END");
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
  public ImDocumentData()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="withTemplate">Создать документ с шаблоном.
  /// Если false, то шаблон документа не создается и можно назначить его позже.</param>
  public ImDocumentData(bool withTemplate) => this.InitFields(withTemplate);

  /// <summary>Создать пустой шаблон для документа. Работает и на сервере и на клиенте</summary>
  public static ImDocumentData CreateTemplate(Type type, bool createDefaultPage)
  {
    EmptyConstructorDelegate constructorDelegate = (EmptyConstructorDelegate) null;
    if (type != typeof (ImDocumentData))
      constructorDelegate = DocumentTreeNode.TypeConstructorDictionary[(object) nameof (ImDocumentData)] as EmptyConstructorDelegate;
    ImDocumentData template = constructorDelegate == null ? new ImDocumentData(false) : constructorDelegate() as ImDocumentData;
    template.SetIsTemplate(true);
    if (createDefaultPage)
      template.NewPage();
    return template;
  }

  /// <summary>Создать первую страницу документа</summary>
  public PageData CreateFirstPage()
  {
    if (this.nodes.Count == 0)
    {
      PageData firstPageTemplate = this.GetFirstPageTemplate();
      if (firstPageTemplate == null)
        return this.NewPage((DocumentTreeNode) this);
      this.InsertChildNode(0, firstPageTemplate.CloneFromTemplate(true, true), false, true, false, true, false);
    }
    PageData firstPage = ImDocumentData.GetFirstPage((DocumentTreeNode) this);
    if (firstPage != null)
      return firstPage;
    return this.nodes[0] is DocumentSection node ? node.CreateFirstPage(false) : (PageData) null;
  }

  /// <summary>Фабричный конструктор. Создаёт новый документ. Класс зависит от контекста: Сервер это или клиент</summary>
  /// <param name="template">Шаблон документа</param>
  /// <param name="applyTemplate">Применить шаблон</param>
  /// <param name="needFirstPage">Создавать первую страницу, даже если в шаблоне не определена страница,
  /// которая должна автоматически создаваться</param>
  public static ImDocumentData CreateDocument(bool withTemplate = true, bool needFirstPage = true)
  {
    ImDocumentData document;
    if (DocumentTreeNode.TypeConstructorDictionary[(object) ImDocumentData.TypeNameForConstructorDictionary] is EmptyConstructorDelegate typeConstructor)
    {
      document = (ImDocumentData) typeConstructor();
      document.InitFields(withTemplate);
    }
    else
      document = new ImDocumentData(withTemplate);
    if (needFirstPage)
      document.CreateFirstPage();
    return document;
  }

  /// <summary>Конструктор. Создаёт документ с шаблоном</summary>
  /// <param name="template">Шаблон документа</param>
  /// <param name="applyTemplate">Применить шаблон</param>
  /// <param name="needFirstPage">Создавать первую страницу, даже если в шаблоне не определена страница,
  /// которая должна автоматически создаваться</param>
  public static ImDocumentData CreateDocumentFromTemplate(
    ImDocumentData template,
    bool applyTemplate = true,
    bool needFirstPage = true)
  {
    if (template == null)
      throw new ArgumentNullException(nameof (template));
    ImDocumentData document = ImDocumentData.CreateDocument(false, false);
    if (template != null)
    {
      ImDocumentData imDocumentData = template.Clone(true, true) as ImDocumentData;
      document.AssignDocumentTemplate(imDocumentData, applyTemplate, false, false);
    }
    if (needFirstPage)
      document.CreateFirstPage();
    return document;
  }

  /// <summary>Конструктор. Создаёт документ с шаблоном</summary>
  /// <param name="template">Шаблон документа</param>
  /// <param name="applyTemplate">Применить шаблон</param>
  /// <param name="needFirstPage">Создавать первую страницу, даже если в шаблоне не определена страница,
  /// которая должна автоматически создаваться</param>
  public ImDocumentData(ImDocumentData template, bool applyTemplate, bool needFirstPage)
    : this(false)
  {
    if (template != null)
      this.AssignDocumentTemplate(template.Clone(true, true) as ImDocumentData, applyTemplate, false, true);
    if (!needFirstPage)
      return;
    this.CreateFirstPage();
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре констрикторов.</summary>
  public static object EmptyConstructor() => (object) new ImDocumentData(false);

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new ImDocumentData(false);
    base.CreateEmptyElement(ref element);
  }

  public override void Dispose()
  {
    LogManager.AddLine("ImDocumentData.Dispose() - START");
    this.AbortBackgroundThreads();
    base.Dispose();
    LogManager.AddLine("ImDocumentData.Dispose() - END");
  }

  /// <summary>Получить расширение из имени файла без точки</summary>
  /// <param name="fileName">Имя файла</param>
  /// <returns></returns>
  public static string GetFileExtensionWithoutDot(string fileName)
  {
    if (string.IsNullOrEmpty(fileName))
      return "";
    string extensionWithoutDot = Path.GetExtension(fileName);
    if (extensionWithoutDot != "" && extensionWithoutDot[0] == '.')
      extensionWithoutDot = extensionWithoutDot.Remove(0, 1);
    return extensionWithoutDot;
  }

  /// <summary>Заданное расширение использовалось в AVS старого формата</summary>
  /// <param name="fileExtension">Расширение файла</param>
  /// <returns></returns>
  public static bool IsOldAVSExtension(string fileExtension)
  {
    fileExtension = fileExtension != null ? fileExtension.ToUpper() : throw new ArgumentNullException(nameof (fileExtension));
    return fileExtension == "SP" || fileExtension.Contains("PE") && fileExtension != "PEX" || fileExtension == "TB";
  }

  /// <summary>Заданное расширение используется для документов интермех [ImDocument]</summary>
  /// <param name="fileExtension">Расширение файла</param>
  /// <returns></returns>
  public static bool IsImDocumentExtension(string fileExtension)
  {
    fileExtension = fileExtension != null ? fileExtension.ToLower() : throw new ArgumentNullException(nameof (fileExtension));
    return ImDocumentData.ImDocumentFileExtensions.Contains(fileExtension);
  }

  /// <summary>Заданное расширение используется для документов интермех [ImDocument]</summary>
  /// <param name="fileExtension">Расширение файла</param>
  /// <returns></returns>
  public static bool IsOldImDocumentExtension(string fileExtension)
  {
    fileExtension = fileExtension != null ? fileExtension.ToLower() : throw new ArgumentNullException(nameof (fileExtension));
    return ImDocumentData.OldImDocumentExtensions.Contains(fileExtension);
  }

  /// <summary>Заданное расширение используется для документов интермех [ImDocument]</summary>
  /// <param name="fileExtension">Расширение файла</param>
  /// <returns></returns>
  public static bool IsOldBlankExtension(string fileExtension)
  {
    fileExtension = fileExtension != null ? fileExtension.ToLower() : throw new ArgumentNullException(nameof (fileExtension));
    return ImDocumentData.OldBlankExtensions.Contains(fileExtension);
  }

  /// <summary>Найти поток сделанный по шаблону</summary>
  /// <param name="templateFlowID">Шаблон потока</param>
  /// <returns>Поток сделанный по шаблону</returns>
  public virtual FlowID FindFlowIDFromTemplate(FlowID templateFlow)
  {
    if (templateFlow == null)
      throw new ArgumentNullException("templateFlowID");
    if (this.documentFlows == null)
      return (FlowID) null;
    for (int index = 0; index < this.documentFlows.Count; ++index)
    {
      if (this.documentFlows[index].TemplateFlowID == templateFlow)
        return this.documentFlows[index];
    }
    return (FlowID) null;
  }

  /// <summary>Найти одноимённый поток в списке потоков документа, если есть именно этот поток, то возвращает его</summary>
  /// <param name="flowID">Идентификатор потока</param>
  public virtual FlowID FindFlowIDByName(FlowID flow)
  {
    if (flow == null)
      throw new ArgumentNullException("flowID");
    if (this.documentFlows == null)
      return (FlowID) null;
    FlowID flowIdByName = (FlowID) null;
    for (int index = 0; index < this.documentFlows.Count; ++index)
    {
      if (this.documentFlows[index] == flow)
        return this.documentFlows[index];
      if (flowIdByName == null && this.documentFlows[index].Name == flow.Name)
        flowIdByName = this.documentFlows[index];
    }
    return flowIdByName;
  }

  /// <summary>Найти первый элемент потока в документе</summary>
  /// <param name="flowID">Идентификатор потока</param>
  /// <param name="flowElementByName">Если не найден по идентификатору, но есть одноимённый поток</param>
  /// <returns></returns>
  public IFlowElement FindFirstFlowElement(FlowID flowID, ref IFlowElement flowElementByName)
  {
    IFlowElement firstFlowElement = (IFlowElement) null;
    foreach (PageData pageData in this)
    {
      if (pageData.PrevPage == null)
      {
        firstFlowElement = pageData.GetFirstFlowElement(flowID, ref flowElementByName);
        if (firstFlowElement != null)
          break;
      }
    }
    return firstFlowElement;
  }

  /// <summary>Добавить поток данных документа</summary>
  /// <param name="flow">Поток</param>
  /// <param name="applyTemplate">Применять по шаблону</param>
  public virtual void AddDocumentFlow(FlowID flow, bool applyTemplate)
  {
    this.documentFlows.Add(flow);
    if (!applyTemplate || !this.IsTemplate)
      return;
    List<ReferenceToNode> connectionList = this.ConnectionList;
    if (connectionList == null)
      return;
    for (int index = 0; index < connectionList.Count; ++index)
    {
      if (connectionList[index] is ReferenceToTemplate && connectionList[index].OwnerNode is ImDocumentData ownerNode)
      {
        FlowID flow1 = flow.Clone();
        flow1.TemplateFlowID = flow;
        ownerNode.AddDocumentFlow(flow1, applyTemplate);
      }
    }
  }

  /// <summary>Вставить новый лист созданный по шаблону в документ, и связать его с потоком данных</summary>
  /// <param name="pageTemplateId">Шаблон нового листа</param>
  /// <param name="index">Индекс в документе</param>
  /// <param name="updateLayout">Обновить разбивку документа</param>
  public PageData InsertNewPageInDocumentFlow(
    string pageTemplateId,
    int index,
    bool manualInserted,
    bool updateLayout)
  {
    PageData newPage = this.ClonePageFromTemplate(pageTemplateId, true);
    if (newPage != null)
    {
      newPage.ManualInserted = manualInserted;
      this.InsertPageInDocumentFlow(newPage, index, updateLayout);
    }
    return newPage;
  }

  /// <summary>Вставить новый лист в документ, и связать его с потоком данных</summary>
  /// <param name="newPage">Шаблон нового листа</param>
  /// <param name="index">Индекс в документе</param>
  /// <param name="updateLayout">Обновить разбивку документа</param>
  public void InsertPageInDocumentFlow(PageData newPage, int index, bool updateLayout)
  {
    if (newPage == null)
      throw new ArgumentNullException(nameof (newPage));
    PageData prevPage = ImDocumentData.GetPrevPage((DocumentTreeNode) this, index, true);
    prevPage?.InsertNextFlowChaineElement((IParentFlow) newPage);
    this.InsertChildNode(index, (DocumentTreeNode) newPage, false, true, false, false, false);
    if (prevPage == null)
    {
      PageData nextPage = ImDocumentData.GetNextPage((DocumentTreeNode) this, index, true);
      if (nextPage != null)
        newPage.InsertNextFlowChaineElement((IParentFlow) nextPage);
    }
    newPage.UpdateTemplateLinks(false, true, false, false);
    newPage.UpdateNodeLinks(true, true, false, false);
    newPage.SetNeedUpdateLayoutFlag(true, true, false, false);
    prevPage?.SetNeedUpdateLayoutFlag(true, true, updateLayout, updateLayout);
  }

  /// <summary>Получить уникальный текстовый идентификатор для потока</summary>
  /// <returns></returns>
  public string GetNewNameForFlowID(string prototype)
  {
    if (prototype == null || prototype == "")
      prototype = "#" + this.documentFlows.Count.ToString();
    string name = prototype;
    bool flag = false;
    for (int index = 0; index < 1000000 && !flag; ++index)
    {
      flag = this.CheckUniqueFlowName(name);
      if (!flag)
        name = $"{prototype}.{index.ToString()}";
    }
    if (!flag)
      name = Guid.NewGuid().ToString();
    return name;
  }

  /// <summary>Проверить уникальность имени потока</summary>
  /// <param name="name">Имя</param>
  /// <returns>Возвращает true, если имя не использовалось в списке потоков документа</returns>
  public bool CheckUniqueFlowName(string name)
  {
    for (int index = 0; index < this.documentFlows.Count; ++index)
    {
      if (this.documentFlows[index].Name == name)
        return false;
    }
    return true;
  }

  /// <summary>Список потоков между страницами. Первый поток является потоком по умолчанию</summary>
  [Category("Debug")]
  public virtual List<FlowID> DocumentFlows
  {
    [DebuggerStepThrough] get => this.documentFlows;
  }

  /// <summary>Событие разблокирована страница</summary>
  public event PageUnlocked_EventHandler PageUnlocked
  {
    add => this.pageUnlocked += value;
    remove => this.pageUnlocked -= value;
  }

  /// <summary>Генерирует событие PageUnlocked</summary>
  protected virtual void OnPageUnlocked(PageUnlockedArgs e)
  {
    if (e?.Page != null && e.Page.NeedUpdateLayoutFlag && !this.isDistributing && !e.IsDistributed && e.ReadArgs?.LoadFromStreamThread != null)
      this.UpdateLayout(e.Page.Index, this.IsLoading, true, false, true);
    PageUnlocked_EventHandler pageUnlocked = this.pageUnlocked;
    if (pageUnlocked == null)
      return;
    pageUnlocked((object) this, e);
  }

  /// <summary>Событие разблокирована страница</summary>
  public event PageUnlocked_EventHandler PageLoaded
  {
    add => this.pageLoaded += value;
    remove => this.pageLoaded -= value;
  }

  protected virtual void OnPageLoaded(PageUnlockedArgs e)
  {
    PageUnlocked_EventHandler pageLoaded = this.pageLoaded;
    if (pageLoaded == null)
      return;
    pageLoaded((object) this, e);
  }

  /// <summary>Событие при завершении фоновых процессов</summary>
  public event BackgroundThreadsFinished_EventHandler BackgroundThreadsFinished
  {
    add => this.backgroundThreadsFinished += value;
    remove => this.backgroundThreadsFinished -= value;
  }

  /// <summary>Генерирует событие BackgroundThreadsFinished</summary>
  protected virtual void OnBackgroundThreadsFinished(BackgroundThreadsFinishedArgs e)
  {
    if (this.backgroundThreadsFinished == null)
      return;
    this.backgroundThreadsFinished((object) this, e);
  }

  /// <summary>Запущен процесс разбивки документа</summary>
  [Browsable(false)]
  public bool IsDistributing => this.isDistributing;

  /// <summary>Обновить представление данных</summary>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public override void UpdateLayout(bool updateUI) => this.UpdateLayout(0, false, updateUI);

  /// <summary>Обновить представление данных</summary>
  /// <param name="force">Обновлять даже если SuspendedUpdateLayoutFlag</param>
  /// <param name="updateUI">Обновлять пользовательский интерфейс</param>
  public void UpdateLayout(bool force, bool updateUI) => this.UpdateLayout(0, force, updateUI);

  /// <summary>Метод вызывается перед началом разбивки в потоке</summary>
  protected virtual void OnBeforeDistributeInThread(DistributeThreadArgs threadParams)
  {
  }

  /// <summary>Метод вызывается после окончания разбивки в потоке</summary>
  protected virtual void OnAfterDistributeInThread(DistributeThreadArgs threadParams)
  {
  }

  /// <summary>Метод разбивки документа. Используется и как метод для фонового потока и как синхронный вызов</summary>
  /// <param name="args">Аргументы разбивки. Должны быть типа DistributeThreadArgs</param>
  private void DistributeDocument(object args)
  {
    if (this.isDistributing)
      return;
    try
    {
      DistributeThreadArgs distributeThreadArgs = args != null ? (DistributeThreadArgs) args : throw new ArgumentNullException(nameof (args));
      LogManager.AddLine($"ImDocumentData.DistributeDocument [IsBackgroundThread:{distributeThreadArgs.IsBackgroundThread}, Force:{distributeThreadArgs.Force}] -START");
      this.OnBeforeDistributeInThread(distributeThreadArgs);
      this.isDistributing = true;
      try
      {
        if (distributeThreadArgs.LockUndo)
          this.LockUndo();
        int num1 = 0;
        lock (this.pageThreadStatus)
          num1 = this.pageThreadStatus.StartDistributingPage;
        PageData pageData1 = (PageData) null;
        int index1 = num1;
        if (this.nodes.Count == 0)
          index1 = 0;
        while (index1 != -1)
        {
          if (index1 < this.nodes.Count)
          {
            DistributeContext distributeContext = new DistributeContext(this.nodes[index1], distributeThreadArgs.Force);
            PageData node1 = this.nodes[index1] as PageData;
            PageData pageData2 = (PageData) null;
            if (index1 + 1 < this.nodes.Count)
              pageData2 = this.nodes[index1 + 1] as PageData;
            PageData pageData3 = (PageData) null;
            if (node1 != null && node1.NeedUpdateLayoutFlag && index1 < num1)
              num1 = index1;
            if (node1 != null && index1 >= num1)
            {
              int num2 = 0;
              while (this.IsFileLoading && num2 < 100000 && (node1.IsLockedForLoad || pageData2 != null && pageData2.IsLockedForLoad || index1 >= this.nodes.Count - 2))
              {
                Thread.Sleep(10);
                ++num2;
                if (pageData2 == null && index1 + 1 < this.nodes.Count)
                  pageData2 = this.nodes[index1 + 1] as PageData;
              }
              pageData3 = this.DistributePage(distributeThreadArgs, distributeContext, node1, pageData3);
            }
            else
              this.nodes[index1].Distribute(distributeContext, distributeThreadArgs.UpdateUI);
            lock (this.pageThreadStatus)
            {
              if (node1 != null && distributeContext.VertDistributed == DistributeResult.BackToPrevious)
              {
                if (pageData1 == node1)
                {
                  LogManager.AddLine($"DistributeDocument. BackToPrevious loop. Doc: [{this.DBObjectID}] '{this.GetDefautCaption()}', Page: '{node1.GetDefautCaption()}'", true);
                  pageData1 = (PageData) null;
                }
                else
                {
                  index1 = node1.Index;
                  if (this.pageThreadStatus.StartDistributingPage >= index1)
                    this.pageThreadStatus.StartDistributingPage = index1 - 1;
                  pageData1 = node1;
                }
              }
              if (this.pageThreadStatus.StartDistributingPage < index1)
              {
                if (distributeContext == null || distributeContext.VertDistributed != DistributeResult.BackToPrevious)
                {
                  for (int distributingPage = this.pageThreadStatus.StartDistributingPage; distributingPage < this.nodes.Count; ++distributingPage)
                  {
                    if (this.nodes[distributingPage] is PageData node2)
                    {
                      foreach (RectangleElement rectangleElement in node2.Nodes.OfType<RectangleElement>())
                        rectangleElement.ResetDistributeState();
                    }
                  }
                }
                index1 = this.pageThreadStatus.StartDistributingPage;
              }
              else
              {
                ++index1;
                this.pageThreadStatus.StartDistributingPage = index1;
              }
              if (index1 >= this.nodes.Count)
              {
                for (int index2 = this.nodes.Count - 1; index2 >= 0; --index2)
                {
                  if (this.nodes[index2] is PageData node3 && !node3.IsFinalPage)
                    node3.DeletePageIfEmpty();
                }
                for (int index3 = this.nodes.Count - 1; index3 >= 0; --index3)
                {
                  if (this.nodes[index3] is PageData node4 && node4.IsFinalPage && node4.IsEmptyRemovablePageInDataFlow)
                  {
                    if (node4.PrevPage?.PrevPage == null)
                    {
                      node4.RemovePageFromDataFlow(false);
                    }
                    else
                    {
                      PageData prevPage = node4.PrevPage;
                      if (prevPage.CanMoveAllFlowDataToNextPage())
                      {
                        prevPage.RemovePageAndMoveDataFlowToNext();
                      }
                      else
                      {
                        distributeContext.VertDistributed = DistributeResult.Part;
                        distributeContext.MoveTailToFinalPage = true;
                        prevPage.SetNeedUpdateLayoutFlag(true, false, false, false);
                        foreach (DocumentTreeNode documentTreeNode in prevPage.Nodes.OfType<TableData>().Where<TableData>((Func<TableData, bool>) (t => t.IsPageFlow)))
                          documentTreeNode.SetNeedUpdateLayoutFlag(true, false, false, false);
                        PageData distributedPage = this.DistributePage(distributeThreadArgs, distributeContext, prevPage, pageData3);
                        pageData3 = this.DistributePage(distributeThreadArgs, distributeContext, node4, distributedPage);
                        distributeContext.MoveTailToFinalPage = false;
                      }
                    }
                  }
                }
                this.ResetNeedUpdateLayoutFlag(false);
              }
              if (pageData3?.Parent != null)
              {
                if (pageData3.Index < this.pageThreadStatus.StartDistributingPage)
                  this.OnPageUnlocked(new PageUnlockedArgs(pageData3, true, (XmlReadArgs) null));
              }
            }
          }
          else
            break;
        }
      }
      catch (ThreadAbortException ex)
      {
        Thread.ResetAbort();
      }
      finally
      {
        this.OnAfterDistributeInThread(distributeThreadArgs);
        lock (this.pageThreadStatus)
          this.pageThreadStatus.StartDistributingPage = -1;
        this.AssignNeedUpdateLayoutFlag(false);
        this.DistributeThread = (Thread) null;
        this.OnBackgroundThreadsFinished(new BackgroundThreadsFinishedArgs(DocumentBackgroundThreadType.DistributeThread));
        this.isDistributing = false;
        if (distributeThreadArgs.LockUndo)
          this.UnlockUndo();
      }
      LogManager.AddLine("ImDocumentData.DistributeDocument -END");
    }
    catch (Exception ex)
    {
      LogManager.AddLine(ex, true);
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      }
    }
  }

  private PageData DistributePage(
    DistributeThreadArgs distributeThreadParams,
    DistributeContext pageContext,
    PageData page,
    PageData distributedPage)
  {
    page.SetSuspendUpdateLayoutCount(0);
    if (distributeThreadParams.UpdateUI)
    {
      page.SetSuspendRefreshUICount(0);
      page.SetSuspendUpdateUIGeometryCount(0);
    }
    page.IsLockedForLayout = true;
    this.OnBeforeDistributePage(new PageDistribute_EventArgs(page));
    page.Distribute(pageContext, distributeThreadParams.UpdateUI);
    page.IsLockedForLayout = false;
    if (pageContext.VertDistributed != DistributeResult.BackToPrevious)
    {
      distributedPage = page;
      this.OnAfterDistributePage(new PageDistribute_EventArgs(distributedPage));
    }
    return distributedPage;
  }

  /// <summary>Обновить представление данных</summary>
  /// <param name="fromPage">Начиная со страницы</param>
  /// <param name="force">Обновлять даже если SuspendedUpdateLayoutFlag</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  public void UpdateLayout(int fromPage, bool force, bool updateUI)
  {
    this.UpdateLayout(fromPage, force, true, updateUI, false);
  }

  /// <summary>Обновить представление данных</summary>
  /// <param name="fromPage">Начиная со страницы</param>
  /// <param name="force">Обновлять даже если SuspendedUpdateLayoutFlag</param>
  /// <param name="lockUndo">Блокировать сохранение undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="distributeInThread">Разбивать в потоке</param>
  public virtual void UpdateLayout(
    int fromPage,
    bool force,
    bool lockUndo,
    bool updateUI,
    bool distributeInThread)
  {
    if (!force && this.isDistributing || !force && this.SuspendedUpdateLayoutFlag)
      return;
    if (force && this.SuspendedUpdateLayoutFlag)
      this.SetSuspendUpdateLayoutCount(0);
    if (LogManager.CreateLog)
    {
      LogManager.AddLine($"ImDocumentData.UpdateLayout(fromPage:{fromPage}, force:{force}, lockUndo:{lockUndo}, updateUI:{updateUI}, distributeInThread:{distributeInThread})" + $" [IsTemplate:{this.IsTemplate}] -START");
      if (!string.IsNullOrEmpty(this.FileName))
        LogManager.AddLine($"   [File: {this.FileName}]");
      LogManager.CloseFile();
    }
    bool flag = false;
    if (this.pageThreadStatus == null)
      this.pageThreadStatus = new PageThreadStatus();
    if (fromPage < 0)
      fromPage = 0;
    lock (this.pageThreadStatus)
    {
      if (this.pageThreadStatus.StartDistributingPage == -1)
      {
        this.pageThreadStatus.StartDistributingPage = fromPage;
        flag = true;
      }
      else if (this.pageThreadStatus.StartDistributingPage > fromPage)
        this.pageThreadStatus.StartDistributingPage = fromPage;
    }
    if (flag)
    {
      DistributeThreadArgs distributeThreadArgs = new DistributeThreadArgs(force, lockUndo, updateUI, distributeInThread);
      if (distributeInThread)
      {
        this.DistributeThread = new Thread(new ParameterizedThreadStart(this.DistributeDocument));
        this.DistributeThread.Name = "DistributeThread";
        this.DistributeThread.SetApartmentState(ApartmentState.STA);
        if (LogManager.CreateLog)
          LogManager.AddLine("DistributeThread.Start(distributeArgs)");
        this.DistributeThread.Start((object) distributeThreadArgs);
      }
      else
        this.DistributeDocument((object) distributeThreadArgs);
      if (updateUI)
        this.RefreshUI();
    }
    else
      LogManager.AddLine("ImDocumentData.UpdateLayout startThread == false");
    LogManager.AddLine("ImDocumentData.UpdateLayout -END");
    LogManager.CloseFile();
  }

  /// <summary>Возобновить автоматическое обновление представлений данных</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void ResumeUpdateLayout(bool updateUI, bool updateLayout)
  {
    if (updateUI)
    {
      this.SetSuspendRefreshUICount(0);
      this.SetSuspendUpdateUIGeometryCount(0);
    }
    base.ResumeUpdateLayout(updateUI, updateLayout);
  }

  /// <summary>Возобновить автоматическое обновление разбивки страниц</summary>
  /// <param name="fromPage">Обновлять начиная со страницы</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void ResumeUpdateLayout(int fromPage, bool updateUI, bool updateLayout)
  {
    if (updateUI)
    {
      this.SetSuspendRefreshUICount(0);
      this.SetSuspendUpdateUIGeometryCount(0);
    }
    if (this.suspendUpdateLayoutCount > 0)
      --this.suspendUpdateLayoutCount;
    else
      this.suspendUpdateLayoutCount = 0;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].ResumeUpdateLayout(false, false);
    }
    if (!updateLayout || this.SuspendedUpdateLayoutFlag)
      return;
    this.UpdateLayout(fromPage, false, true, updateUI, false);
  }

  /// <summary>Установить значение счетчика SuspendRefreshUI для узла и подузлов</summary>
  /// <param name="count">Значение счетчика</param>
  internal void SetSuspendRefreshUICount(int count)
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageData node)
        node.SetSuspendRefreshUICount(count);
    }
  }

  /// <summary>Установить значение счетчика SuspendUpdateUIGeometry для узла и подузлов</summary>
  /// <param name="count">Значение счетчика</param>
  internal void SetSuspendUpdateUIGeometryCount(int count)
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageData node)
        node.SetSuspendUpdateUIGeometryCount(count);
    }
  }

  /// <summary>Обновить ссылки на на узлы</summary>
  /// <param name="recursive">Для всех дочерних элементов</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateNodeLinks(
    bool recursive,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    if (this.reference != null && this.reference is ReferenceToNode)
      this.reference.UpdateLink(updateUI, updateLayout);
    base.UpdateNodeLinks(recursive, saveUndo, updateUI, updateLayout);
  }

  [Browsable(false)]
  public bool IsSuspendedUpdatesFromDB
  {
    get
    {
      if (this._isSuspendedUpdatesFromDB)
        return true;
      return this.TemplateOwner != null && this.TemplateOwner.IsSuspendedUpdatesFromDB;
    }
  }

  [Browsable(false)]
  public bool DBAttributeAutoSave
  {
    [DebuggerStepThrough] get => this.dbAttributeAutoSave;
    set => this.dbAttributeAutoSave = value;
  }

  /// <summary>Обозначение</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_555")]
  [CustomDescription("Attribute.Interfaces.Document_556")]
  [CustomCategory("Attribute.Interfaces.Document_118")]
  [Browsable(false)]
  public string Designation
  {
    [DebuggerStepThrough] get
    {
      return this.GetAttributeValue(DocumentTreeNode.AttributeName_Designation, true);
    }
    set => this.SetAttributeValue(DocumentTreeNode.AttributeName_Designation, value);
  }

  /// <summary>Размер файла документа</summary>
  [Browsable(false)]
  public long? FileSize
  {
    get => this.fileSize;
    set => this.fileSize = value;
  }

  /// <summary>Дата модификации файла документа</summary>
  [Browsable(false)]
  public DateTime? FileModifyDate
  {
    get => this.fileModifyDate;
    set => this.fileModifyDate = value;
  }

  /// <summary>Наименование файла документа используется на сервере</summary>
  [Browsable(false)]
  public string FileName
  {
    [DebuggerStepThrough] get
    {
      return this.GetAttributeValue(DocumentTreeNode.AttributeName_FileName, true);
    }
    set
    {
      if (value != null)
        this.SetAttributeValue(DocumentTreeNode.AttributeName_FileName, value, false, false, false);
      else
        this.RemoveAttribute(DocumentTreeNode.AttributeName_FileName, false, false);
    }
  }

  /// <summary>Наименование документа из шапки</summary>
  [Browsable(false)]
  public string DocumentName
  {
    [DebuggerStepThrough] get
    {
      return this.GetAttributeValue(DocumentTreeNode.AttributeName_DocName, true);
    }
    set => this.SetAttributeValue(DocumentTreeNode.AttributeName_DocName, value);
  }

  /// <summary>Guid версии комплекта в котором находится документ</summary>
  [Browsable(false)]
  public Guid DocumentComplectObjectGuid
  {
    [DebuggerStepThrough] get => this.documentComplectObjectGuid;
    set => this.documentComplectObjectGuid = value;
  }

  [Category("Debug")]
  public ObjectModifyModes? DBObjectModifyMode
  {
    get => this.dbObjectModifyModes;
    set => this.dbObjectModifyModes = value;
  }

  /// <summary>Идентификатор версии объекта в котором хранится документ</summary>
  [Browsable(false)]
  public long DBObjectID
  {
    [DebuggerStepThrough] get
    {
      return this.reference is ReferenceToDBObjectBase reference ? reference.DBObjectID : -1L;
    }
  }

  /// <summary>Идентификатор версии объекта в котором хранится документ</summary>
  [Browsable(false)]
  public DBObjectInfoBase DBObjectInfo
  {
    [DebuggerStepThrough] get
    {
      return this.reference is ReferenceToDBObjectBase reference ? reference.DBObjectInfo : (DBObjectInfoBase) null;
    }
  }

  /// <summary>Глобальный идентификатор версии объекта в котором хранится документ</summary>
  [Browsable(false)]
  public Guid DBObjectGuid
  {
    [DebuggerStepThrough] get
    {
      return this.reference is ReferenceToDBObjectBase reference ? reference.DBObjectGuid : Guid.Empty;
    }
  }

  /// <summary>Идентификатор типа объекта в котором хранится документ</summary>
  [Browsable(false)]
  public int DBObjectType
  {
    [DebuggerStepThrough] get
    {
      return this.reference is ReferenceToDBObjectBase reference ? reference.DBObjectType : -1;
    }
  }

  /// <summary>Заголовок объекта в котором хранится документ</summary>
  [Browsable(false)]
  public string DBObjectCaption
  {
    [DebuggerStepThrough] get
    {
      return this.reference is ReferenceToDBObjectBase reference ? reference.DBObjectCaption : this.GetDefautCaption();
    }
  }

  /// <summary>Значение NonSkipAtStartPage для строк по умолчанию</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_577")]
  [CustomDescription("Attribute.Interfaces.Document_578")]
  [CustomCategory("Attribute.Interfaces.Document_141")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool DefaultNonSkipAtStartPage
  {
    get
    {
      if (this.defaultNonSkipAtStartPage.HasValue)
        return this.defaultNonSkipAtStartPage.Value;
      return this.DocumentTemplate != null && this.DocumentTemplate.DefaultNonSkipAtStartPage;
    }
    set => this.SetDefaultNonSkipAtStartPage(value, true, true, true);
  }

  /// <summary>Задать новое значение свойству DefaultNonSkipAtStartPage без вызова обработчиков</summary>
  /// <param name="value">Значение</param>
  /// <param name="setOverrideFlag">Установить флаг перекрытия шаблона</param>
  public void AssignDefaultNonSkipAtStartPage(bool value)
  {
    this.defaultNonSkipAtStartPage = new bool?(value);
  }

  /// <summary>Задать новое значение свойству DefaultNonSkipAtStartPage</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void SetDefaultNonSkipAtStartPage(
    bool value,
    bool saveUndo,
    bool updateUI,
    bool updateLayout)
  {
    if (this.DefaultNonSkipAtStartPage == value)
      return;
    if (saveUndo && this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "DefaultNonSkipAtStartPage", (object) this.DefaultNonSkipAtStartPage, (object) value);
    this.defaultNonSkipAtStartPage = new bool?(value);
    ImDocumentData.SetNeedUpdateForDefaultNonSkipAtStartPage((DocumentTreeNode) this);
    if (this.needUpdateLayoutFlag & updateLayout)
      this.UpdateLayout(0, false, true, updateUI, true);
    if (this.connectionList != null)
    {
      for (int index = 0; index < this.connectionList.Count; ++index)
      {
        if (this.connectionList[index] is ReferenceToTemplate && this.connectionList[index].OwnerNode is ImDocumentData ownerNode)
        {
          ImDocumentData.SetNeedUpdateForDefaultNonSkipAtStartPage((DocumentTreeNode) ownerNode);
          if (ownerNode.needUpdateLayoutFlag & updateLayout)
            ownerNode.UpdateLayout(0, false, true, updateUI, true);
        }
      }
    }
    this.OnChanged(new Changed_EventArgs());
  }

  private static void SetNeedUpdateForDefaultNonSkipAtStartPage(DocumentTreeNode node)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    if (node.Nodes == null)
      return;
    if (node is TableData tableData)
    {
      for (int index = 0; index < tableData.Nodes.Count; ++index)
      {
        if (tableData.Nodes[index] is RectangleElement node1 && (double) node1.SkipCellsBefore != 0.0 && !node1.IsOverridden3(OverrideFlags3.NonSkipBeforeAtStartPage))
          node1.SetNeedUpdateLayoutFlag(true, true, false, false);
        ImDocumentData.SetNeedUpdateForDefaultNonSkipAtStartPage(tableData.Nodes[index]);
        if (tableData.IsColumn)
          break;
      }
    }
    else
    {
      for (int index = 0; index < node.Nodes.Count; ++index)
        ImDocumentData.SetNeedUpdateForDefaultNonSkipAtStartPage(node.Nodes[index]);
    }
  }

  [Browsable(false)]
  public bool NowPrinting
  {
    get => this.nowPrinting;
    set => this.nowPrinting = value;
  }

  /// <summary>Содержит ли объект виртуальный атрибут с указанным именем</summary>
  /// <param name="attributeName">Имя виртуального атрибута</param>
  /// <returns>Возвращает true, если объект содержит виртуальный атрибут
  /// с указанным именем</returns>
  internal override bool ContainsVirtualAttribute(string attributeName)
  {
    if (attributeName == null)
      throw new ArgumentNullException(nameof (attributeName));
    return attributeName == DocumentTreeNode.AttributeName_DocPageCount || attributeName == DocumentTreeNode.AttributeName_ComplectPageCount || attributeName == DocumentTreeNode.AttributeName_LastDocPageNumber || attributeName == DocumentTreeNode.AttributeName_CheckSum || attributeName == DocumentTreeNode.AttributeName_PrintUser || attributeName == DocumentTreeNode.AttributeName_PrintDate || attributeName == DocumentTreeNode.VirtualAttributeName_FileName || attributeName == DocumentTreeNode.VirtualAttributeName_FileSize || attributeName == DocumentTreeNode.VirtualAttributeName_FileModifyDate || attributeName == DocumentTreeNode.AttributeName_DocName || attributeName == DocumentTreeNode.AttributeName_Designation || base.ContainsVirtualAttribute(attributeName);
  }

  /// <summary>Получить корневой комплект документов</summary>
  /// <returns></returns>
  public DocumentsComplect GetRootDocumentsComplect()
  {
    DocumentsComplect documentsComplect = (DocumentsComplect) null;
    for (DocumentTreeNode parent = this.parent; parent != null; parent = parent.Parent)
    {
      if (parent is DocumentsComplect)
        documentsComplect = parent as DocumentsComplect;
      else if (documentsComplect != null)
        break;
    }
    return documentsComplect;
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
    if (attributeName == DocumentTreeNode.AttributeName_DocPageCount)
      return this.IsFileLoading && this.savedPageCount.HasValue ? new GetVirtualAttributeResult(true, this.savedPageCount.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture)) : new GetVirtualAttributeResult(true, this.PageCount.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (attributeName == DocumentTreeNode.AttributeName_LastDocPageNumber)
    {
      if (!this.IsFileLoading)
      {
        PageData lastPage = ImDocumentData.GetLastPage((DocumentTreeNode) this);
        if (lastPage != null)
          return new GetVirtualAttributeResult(true, lastPage.PageNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      }
      return new GetVirtualAttributeResult(true, "");
    }
    if (attributeName == DocumentTreeNode.AttributeName_CheckSum)
      return new GetVirtualAttributeResult(true, this.checkSum);
    if (attributeName == DocumentTreeNode.VirtualAttributeName_FileName)
      return new GetVirtualAttributeResult(true, this.FileName);
    if (attributeName == DocumentTreeNode.VirtualAttributeName_FileSize)
      return new GetVirtualAttributeResult(true, $"{this.FileSize}");
    if (attributeName == DocumentTreeNode.VirtualAttributeName_FileModifyDate)
      return new GetVirtualAttributeResult(true, $"{this.FileModifyDate}");
    if (attributeName == DocumentTreeNode.AttributeName_ComplectPageCount)
    {
      DocumentsComplect documentsComplect = this.GetRootDocumentsComplect();
      if (documentsComplect != null)
        return new GetVirtualAttributeResult(true, documentsComplect.PageCount.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      string attributeValue = this.GetAttributeValue(ImDocumentData.AttributePagesComplectCount, false, callChain);
      return attributeValue != null ? new GetVirtualAttributeResult(true, attributeValue) : new GetVirtualAttributeResult(true, this.PageCount.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }
    if (attributeName == DocumentTreeNode.AttributeName_DocName)
      return new GetVirtualAttributeResult(true, this.Name);
    if (attributeName == DocumentTreeNode.AttributeName_Designation)
      return new GetVirtualAttributeResult(true, this.designation ?? "");
    if (attributeName == DocumentTreeNode.AttributeName_PrintUser)
      return new GetVirtualAttributeResult(true, this.printUser);
    return attributeName == DocumentTreeNode.AttributeName_PrintDate ? new GetVirtualAttributeResult(true, this.printDate) : base.GetVirtualAttributeValue(attributeName, notNull, callChain);
  }

  /// <summary>Установить значение виртуального атрибута</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
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
    if (attributeName == DocumentTreeNode.AttributeName_DocPageCount)
      return new SetVirtualAttributeResult(true, true);
    if (attributeName == DocumentTreeNode.AttributeName_LastDocPageNumber)
      return new SetVirtualAttributeResult(true, true);
    if (attributeName == DocumentTreeNode.AttributeName_ComplectPageCount)
      return new SetVirtualAttributeResult(true, true);
    if (attributeName == DocumentTreeNode.VirtualAttributeName_FileName)
      return new SetVirtualAttributeResult(true, false);
    if (attributeName == DocumentTreeNode.VirtualAttributeName_FileSize)
      return new SetVirtualAttributeResult(true, false);
    if (attributeName == DocumentTreeNode.VirtualAttributeName_FileModifyDate)
      return new SetVirtualAttributeResult(true, false);
    if (attributeName == DocumentTreeNode.AttributeName_CheckSum)
    {
      this.checkSum = attributeValue;
      return new SetVirtualAttributeResult(true, true);
    }
    if (attributeName == DocumentTreeNode.AttributeName_PrintUser)
    {
      this.printUser = attributeValue;
      return new SetVirtualAttributeResult(true, true);
    }
    if (attributeName == DocumentTreeNode.AttributeName_PrintDate)
    {
      this.printDate = attributeValue;
      return new SetVirtualAttributeResult(true, true);
    }
    if (attributeName == DocumentTreeNode.AttributeName_DocName)
    {
      this.AssignName(attributeValue);
      return new SetVirtualAttributeResult(true, false);
    }
    if (!(attributeName == DocumentTreeNode.AttributeName_Designation))
      return base.SetVirtualAttributeValue(attributeName, attributeValue, updateUI, updateLayout, callChain);
    this.designation = attributeValue;
    return new SetVirtualAttributeResult(true, false);
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
    attributeNames.Add(DocumentTreeNode.AttributeName_DocPageCount);
    attributeNames.Add(DocumentTreeNode.AttributeName_LastDocPageNumber);
    attributeNames.Add(DocumentTreeNode.AttributeName_ComplectPageCount);
    attributeNames.Add(DocumentTreeNode.AttributeName_Designation);
    attributeNames.Add(DocumentTreeNode.AttributeName_DocName);
    attributeNames.Add(DocumentTreeNode.AttributeName_CheckSum);
    attributeNames.Add(DocumentTreeNode.VirtualAttributeName_FileName);
    attributeNames.Add(DocumentTreeNode.VirtualAttributeName_FileSize);
    attributeNames.Add(DocumentTreeNode.VirtualAttributeName_FileModifyDate);
    attributeNames.Add(DocumentTreeNode.AttributeName_PrintUser);
    attributeNames.Add(DocumentTreeNode.AttributeName_PrintDate);
    base.GetVirtualAttributeNames(attributeNames, forSaveOnly);
  }

  /// <summary>
  /// Отображение информации о возникшей исключительной ситуации (Exception)
  /// </summary>
  /// <param name="ex"></param>
  /// <param name="errorFormCaption"></param>
  public static void ShowException(Exception ex, string errorFormCaption = "")
  {
    if (ex is ThreadAbortException)
      return;
    if (ImDocumentData.ShowExceptionDialog != null)
    {
      ImDocumentData.ShowExceptionDialog(ex);
    }
    else
    {
      int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, errorFormCaption);
    }
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
  }

  /// <summary>Получить допустимые типы представлений данных для ячейки</summary>
  /// <returns>Массив допустимых типов представлений данных для ячейки</returns>
  public virtual Type[] GetAviableDataShowElementTypes()
  {
    Type[] types = this.GetType().Assembly.GetTypes();
    ArrayList arrayList = new ArrayList();
    Type type = typeof (TextData);
    foreach (Type c in types)
    {
      if (type.IsAssignableFrom(c) && !c.IsAbstract)
        arrayList.Add((object) c);
    }
    Type[] showElementTypes = new Type[arrayList.Count];
    arrayList.CopyTo((Array) showElementTypes);
    return showElementTypes;
  }

  /// <summary>Цвет переднего плана по умолчанию</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_130")]
  [CustomDescription("Attribute.Interfaces.Document_131")]
  [CustomCategory("Attribute.Interfaces.Document_132")]
  [Browsable(false)]
  public virtual Color DefaultForeColor
  {
    [DebuggerStepThrough] get => this.defaultForeColor;
    set
    {
      if (!(this.defaultForeColor != value))
        return;
      this.defaultForeColor = value;
      this.RefreshUI();
    }
  }

  /// <summary>Цвет фона по умолчанию</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_133")]
  [CustomDescription("Attribute.Interfaces.Document_134")]
  [CustomCategory("Attribute.Interfaces.Document_135")]
  [Browsable(false)]
  public virtual Color DefaultBackColor
  {
    [DebuggerStepThrough] get => this.defaultBackColor;
    set
    {
      if (!(this.defaultBackColor != value))
        return;
      this.defaultBackColor = value;
      this.RefreshUI();
    }
  }

  /// <summary>Шрифт по умолчанию</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_481")]
  [CustomDescription("Attribute.Interfaces.Document_137")]
  [CustomCategory("Attribute.Interfaces.Document_138")]
  public virtual CharFormat DefaultCharFormat
  {
    [DebuggerStepThrough] get
    {
      if (this.defaultCharFormat != null)
        return this.defaultCharFormat;
      return this.DocumentTemplate != null ? this.DocumentTemplate.DefaultCharFormat.Clone() : TextData.DefaultCharFormat.Clone();
    }
    set
    {
      if (this.defaultCharFormat == value)
        return;
      this.defaultCharFormat = value;
    }
  }

  /// <summary>Формат по умолчанию</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_487")]
  [CustomDescription("Attribute.Interfaces.Document_139")]
  [CustomCategory("Attribute.Interfaces.Document_141")]
  public virtual ParagraphFormat DefaultParagraphFormat
  {
    [DebuggerStepThrough] get
    {
      if (this.defaultParagraphFormat != null)
        return this.defaultParagraphFormat;
      return this.DocumentTemplate != null ? this.DocumentTemplate.DefaultParagraphFormat.Clone() : TextData.DefaultParagraphFormat.Clone();
    }
    set
    {
      if (this.defaultParagraphFormat == value)
        return;
      this.defaultParagraphFormat = value;
    }
  }

  /// <summary>Размер правого и левого полей по умолчанию</summary>
  public float DefaultLeftRightMargin
  {
    get => this.defaultLeftRightMargin;
    set
    {
      if ((double) this.defaultLeftRightMargin == (double) value)
        return;
      this.defaultLeftRightMargin = value;
      this.OnChanged(new Changed_EventArgs());
    }
  }

  [Browsable(false)]
  public virtual BorderLine DefaultBorderLine
  {
    get
    {
      if (this.defaultBorderLine == null)
      {
        if (this.DocumentTemplate != null)
          return this.DocumentTemplate.defaultBorderLine;
        this.defaultBorderLine = new BorderLine();
      }
      return this.defaultBorderLine;
    }
  }

  [CustomDisplayName("Attribute.Interfaces.Document_585")]
  [CustomDescription("Attribute.Interfaces.Document_586")]
  [CustomCategory("Attribute.Interfaces.Document_141")]
  public virtual BorderLine DefaultPageBorderLine
  {
    get
    {
      if (this.defaultPageBorderLine == null)
      {
        if (this.DocumentTemplate != null)
          return this.DocumentTemplate.DefaultPageBorderLine;
        this.defaultPageBorderLine = new BorderLine(Color.DarkGray, BorderStyles.SolidLine, 0.0f);
      }
      return this.defaultPageBorderLine;
    }
    set
    {
      if (this.defaultPageBorderLine == value)
        return;
      this.defaultPageBorderLine = value;
    }
  }

  /// <summary>Наименование типа</summary>
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get
    {
      return this.isFormulaLib ? LocalizationHolder.rm.GetString("Interfaces.Document_154") : LocalizationHolder.rm.GetString("Interfaces.Document_61");
    }
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    this.RemoveProperty(properties, "Visible");
    if (ImDocumentData.ShowDebugInfo)
      return;
    this.RemoveProperty(properties, "DocumentFlows");
    this.RemoveProperty(properties, "BackThreadIsActive");
    this.RemoveProperty(properties, "LoadThreadIsActive");
    this.RemoveProperty(properties, "DistributeThreadIsActive");
    this.RemoveProperty(properties, "DefaultLeftRightMargin");
    this.RemoveProperty(properties, "FixedRowSizeTrancateFraction");
    this.RemoveProperty(properties, "DBObjectModifyMode");
  }

  /// <summary>Обновить изображение на экране</summary>
  public override void RefreshUI()
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.RefreshUI();
    }
  }

  /// <summary>Разрешать форматирование для ReadOnly ячеек</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_142")]
  [CustomDescription("Attribute.Interfaces.Document_143")]
  [CustomCategory("Attribute.Interfaces.Document_144")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool AllowFormatingForReadOnlyText
  {
    [DebuggerStepThrough] get => this.allowFormatingForReadOnlyText;
    set
    {
      if (this.allowFormatingForReadOnlyText == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (AllowFormatingForReadOnlyText), (object) this.AllowFormatingForReadOnlyText, (object) value);
      this.allowFormatingForReadOnlyText = value;
      this.overrideFlags |= OverrideFlags.AllowFormatingForReadOnly;
      this.SetPropertiesChangedFlag(true, true, false, false, false);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Сохранять в файле документа значения атрибутов из базы данных</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_567")]
  [CustomDescription("Attribute.Interfaces.Document_568")]
  [CustomCategory("Attribute.Interfaces.Document_144")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [Browsable(false)]
  public virtual bool SaveValueFromRefToDBAttr
  {
    [DebuggerStepThrough] get => this.ForceSaveValuesFromRefToDBAttr;
    set
    {
      if (this.saveValueFromRefToDBAttr == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (SaveValueFromRefToDBAttr), (object) this.saveValueFromRefToDBAttr, (object) value);
      this.saveValueFromRefToDBAttr = value;
      this.overrideFlags3 |= OverrideFlags3.SaveValueFromRefToDBAttr;
      this.SetPropertiesChangedFlag(true, true, false, false, false);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public override void Draw(DrawContext context)
  {
    if (this.nodes == null)
      return;
    VisualNode visualNode = (VisualNode) null;
    int index = 0;
    for (int count = this.nodes.Count; visualNode == null && index < count; ++index)
      visualNode = this.nodes[index] as VisualNode;
    visualNode?.Draw(context);
  }

  /// <summary>Прервать все фоновые потоки</summary>
  public void AbortBackgroundThreads()
  {
    bool flag = false;
    if (this.LoadFromStreamThread != null && (this.LoadFromStreamThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running)
    {
      Thread fromStreamThread = this.LoadFromStreamThread;
      this.LoadFromStreamThread = (Thread) null;
      fromStreamThread.Abort();
      Thread.Sleep(50);
      fromStreamThread.Join();
      flag = true;
    }
    if (this.DistributeThread != null && (this.DistributeThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running)
    {
      Thread distributeThread = this.DistributeThread;
      this.DistributeThread = (Thread) null;
      distributeThread.Abort();
      Thread.Sleep(50);
      distributeThread.Join();
      flag = true;
    }
    if (!flag)
      return;
    this.OnBackgroundThreadsFinished(new BackgroundThreadsFinishedArgs(DocumentBackgroundThreadType.LoadThread | DocumentBackgroundThreadType.DistributeThread));
  }

  /// <summary>Это специальный документ - библиотека формул</summary>
  [Browsable(false)]
  public override bool IsFormulaLib => this.isFormulaLib;

  /// <summary>Только для внутреннего пользования. Назначить новое значение IsFormulaLib</summary>
  /// <param name="value">Новое значение IsFormulaLib</param>
  public void AssignIsFormulaLib(bool value) => this.isFormulaLib = value;

  /// <summary>Рисовать границы подтаблицы поверх границ внутренних ячеек.
  /// Необходимо для возможности отключать границы части внутренних ячеек</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_592")]
  [CustomDescription("Attribute.Interfaces.Document_593")]
  [CustomCategory("Attribute.Interfaces.Document_132")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool DefaultDrawParentCellFrames
  {
    get => this.defaultDrawParentCellFrames;
    set
    {
      if (this.defaultDrawParentCellFrames == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (DefaultDrawParentCellFrames), (object) this.DefaultDrawParentCellFrames, (object) value);
      this.SetOverrideFlags3(OverrideFlags3.DrawParentCellFrames);
      this.defaultDrawParentCellFrames = value;
      this.RefreshUI();
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Погрешность округления кратной высоты текста</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_594")]
  [CustomDescription("Attribute.Interfaces.Document_595")]
  [CustomCategory("Attribute.Interfaces.Document_132")]
  [TypeConverter(typeof (FloatConverter))]
  public float FixedRowSizeTrancateFraction
  {
    get => this.fixedRowSizeTrancateFraction;
    set => this.SetFixedRowSizeTrancateFraction(value, true);
  }

  public void SetFixedRowSizeTrancateFraction(float value, bool updateLayout)
  {
    if ((double) this.fixedRowSizeTrancateFraction == (double) value)
      return;
    this.fixedRowSizeTrancateFraction = value;
    if (this.DocumentTemplate != null)
      this.DocumentTemplate.SetFixedRowSizeTrancateFraction(value, updateLayout);
    this.SetNeedUpdateLayoutFlag(true, false, updateLayout, updateLayout);
    this.SetPropertiesChangedFlag(true, true, false, updateLayout, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Получить страницу идущую перед текущей в документе</summary>
  /// <param name="currPageParent">Владелец текущей страницы</param>
  /// <param name="currPageIndex">Индекс текущей страницы в коллекции владельца</param>
  /// <param name="onlyOwnerDocument">Только в пределах того документа, котороый владеет страницей</param>
  public static PageData GetPrevPage(
    DocumentTreeNode currPageParent,
    int currPageIndex,
    bool onlyOwnerDocument)
  {
    if (currPageParent == null)
      return (PageData) null;
    prevPage = (PageData) null;
    if (currPageIndex > 0)
    {
      if (currPageIndex - 1 > currPageParent.NodesCount)
        currPageIndex = currPageParent.NodesCount;
      for (int index = currPageIndex - 1; index >= 0; --index)
      {
        if (currPageParent.Nodes[index] is PageData prevPage)
          return prevPage;
        if (!(currPageParent.Nodes[index] is PageElementNode))
        {
          prevPage = ImDocumentData.GetLastPage(currPageParent.Nodes[index]);
          if (prevPage != null)
            return prevPage;
        }
      }
    }
    if (currPageParent.Parent != null && (!onlyOwnerDocument || !(currPageParent is ImDocumentData)))
      prevPage = ImDocumentData.GetPrevPage(currPageParent.Parent, currPageParent.Index, onlyOwnerDocument);
    return prevPage;
  }

  /// <summary>Получить страницу идущую после текущей в документе</summary>
  /// <param name="currPageParent">Владелец текущей страницы</param>
  /// <param name="currPageIndex">Индекс текущей страницы в коллекции владельца.
  /// Если -1, то будет искать первую страницу</param>
  /// <param name="onlyOwnerDocument">Только в пределах того документа, котороый владеет страницей</param>
  /// <returns></returns>
  public static PageData GetNextPage(
    DocumentTreeNode currPageParent,
    int currPageIndex,
    bool onlyOwnerDocument)
  {
    if (currPageParent == null)
      return (PageData) null;
    nextPage = (PageData) null;
    if (currPageIndex < currPageParent.NodesCount - 1)
    {
      if (currPageIndex < 0)
        currPageIndex = -1;
      for (int index = currPageIndex + 1; index < currPageParent.Nodes.Count; ++index)
      {
        if (currPageParent.Nodes[index] is PageData nextPage)
          return nextPage;
        if (!(currPageParent.Nodes[index] is PageElementNode))
        {
          nextPage = ImDocumentData.GetFirstPage(currPageParent.Nodes[index]);
          if (nextPage != null)
            return nextPage;
        }
      }
    }
    if (currPageParent.Parent != null && (!onlyOwnerDocument || !(currPageParent is ImDocumentData)))
      nextPage = ImDocumentData.GetNextPage(currPageParent.Parent, currPageParent.Index, onlyOwnerDocument);
    return nextPage;
  }

  /// <summary>Получить первую страницу у заданного владельца</summary>
  /// <param name="pageParent">Владелец страницы</param>
  public static PageData GetFirstPage(DocumentTreeNode pageParent)
  {
    if (pageParent == null)
      return (PageData) null;
    for (int index = 0; index < pageParent.NodesCount; ++index)
    {
      if (pageParent.Nodes[index] is PageData node)
        return node;
      if (!(pageParent.Nodes[index] is PageElementNode))
      {
        PageData firstPage = ImDocumentData.GetFirstPage(pageParent.Nodes[index]);
        if (firstPage != null)
          return firstPage;
      }
    }
    return (PageData) null;
  }

  /// <summary>Получить последнюю страницу у заданного владельца</summary>
  /// <param name="pageParent">Владелец страницы</param>
  public static PageData GetLastPage(DocumentTreeNode pageParent)
  {
    if (pageParent == null)
      return (PageData) null;
    for (int index = pageParent.NodesCount - 1; index >= 0; --index)
    {
      if (pageParent.Nodes[index] is PageData node)
        return node;
      if (!(pageParent.Nodes[index] is PageElementNode))
      {
        PageData lastPage = ImDocumentData.GetLastPage(pageParent.Nodes[index]);
        if (lastPage != null)
          return lastPage;
      }
    }
    return (PageData) null;
  }

  /// <summary>Проверить есть ли в списке узлов узлы заблокированные фоновым потоком</summary>
  public bool HasLockedNodes(IList<DocumentTreeNode> nodes)
  {
    if (nodes == null || !this.BackThreadIsActive)
      return false;
    for (int index = 0; index < nodes.Count; ++index)
    {
      if (nodes[index] is PageElementNode node1 && node1.Page != null && node1.Page.IsWaitForDistributed || nodes[index] is PageData node2 && node2.IsWaitForDistributed)
        return true;
    }
    return false;
  }

  /// <summary>Корень дерева документа в котором находится этот узел.
  /// <remarks>Документ который владеет этим узлом. Если узел не пренадлежит документу, то null</remarks>
  /// </summary>
  public override ImDocumentData GetDocTreeRoot() => this;

  /// <summary>Узел является владельцем сервиса уникальных идентификаторов</summary>
  public override bool IsIdServiceOwner => true;

  public List<PageData> GetAllPages()
  {
    List<PageData> allPages = new List<PageData>();
    PageEnumerator pageEnumerator = new PageEnumerator((DocumentTreeNode) this);
    while (pageEnumerator.MoveNext())
      allPages.Add(pageEnumerator.Current);
    return allPages;
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
  public override bool CanAddChildElement(Type type) => typeof (PageData).IsAssignableFrom(type);

  /// <summary>Герерирует событие Changed</summary>
  public override void OnChanged(Changed_EventArgs e)
  {
    if (this.IsLoading || this.IsChanging || this.IsVirtualNode)
      return;
    base.OnChanged(e);
    if (!this.Modified)
      this.SaveModificationDate = e.SaveModificationDate;
    else if (!e.SaveModificationDate)
      this.SaveModificationDate = false;
    this.Modified = true;
  }

  [Browsable(false)]
  public virtual IUndoManager UndoManager => (IUndoManager) null;

  [Browsable(false)]
  public virtual IExternalEditor ExternalEditor => (IExternalEditor) null;

  /// <summary>Документ изменен после сохранения или открытия</summary>
  [ReadOnly(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_145")]
  [CustomDescription("Attribute.Interfaces.Document_146")]
  [CustomCategory("Attribute.Interfaces.Document_147")]
  public virtual bool Modified
  {
    [DebuggerStepThrough] get => this.modified;
    set
    {
      if ((this.IsLoading || this.isDistributing) && value || this.modified == value)
        return;
      this.modified = value;
      if (!this.modified)
        this.SaveModificationDate = false;
      if (this.IsTemplate && this.modified && this.TemplateOwner != null)
        this.TemplateOwner.Modified = true;
      if (this.Template is ImDocumentData template)
        template.Modified = value;
      this.OnModifiedChanged(new ModifiedChanged_EventArgs());
      if (!this.modified || !(this.parent is DocumentsComplect parent))
        return;
      parent.Modified = this.modified;
    }
  }

  [Browsable(false)]
  public bool SaveModificationDate
  {
    [DebuggerStepThrough] get => this.saveModificationDate;
    set
    {
      if (this.saveModificationDate == value)
        return;
      this.saveModificationDate = value;
    }
  }

  /// <summary>Время сохранения файла в базу</summary>
  [Browsable(false)]
  public DateTime? SavedDateTime
  {
    [DebuggerStepThrough] get => this.savedDateTime;
    set
    {
      DateTime? savedDateTime = this.savedDateTime;
      DateTime? nullable = value;
      if ((savedDateTime.HasValue == nullable.HasValue ? (savedDateTime.HasValue ? (savedDateTime.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        return;
      this.savedDateTime = value;
    }
  }

  /// <summary>Очистить узел</summary>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void Clear(bool updateUI, bool updateLayout)
  {
    if (this.Template != null)
      this.Template.Clear(updateUI, updateLayout);
    base.Clear(updateUI, updateLayout);
  }

  /// <summary>Получить подпись элемента по умолчанию</summary>
  public override string GetDefautCaption()
  {
    string defautCaption = this.GetName();
    if (!string.IsNullOrEmpty(this.Designation))
      defautCaption = string.IsNullOrEmpty(defautCaption) ? this.Designation : $"{defautCaption} ({this.Designation})";
    if (string.IsNullOrEmpty(defautCaption))
      defautCaption = this.NodeTypeCaption;
    return defautCaption;
  }

  /// <summary>Получить комплект верхнего уровня в котором находится документ</summary>
  public DocumentsComplect GetRootComplect()
  {
    DocumentsComplect rootComplect = (DocumentsComplect) null;
    DocumentTreeNode documentTreeNode = (DocumentTreeNode) this;
    while (documentTreeNode.Parent != null)
    {
      documentTreeNode = documentTreeNode.Parent;
      if (documentTreeNode is DocumentsComplect documentsComplect)
        rootComplect = documentsComplect;
    }
    return rootComplect;
  }

  /// <summary>Обновить номера страниц</summary>
  /// <param name="startPage">Страница с которой нужно начать обновление номеров. Если null, то с начала</param>
  /// <param name="startComplectPageIndex">Первый номер страницы для нумерации в комплекте документов</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <returns>Возвращает первый номер страницы следующего документа для нумерации в комплекте документов</returns>
  public int UpdatePageNumbers(
    PageData startPage,
    int startComplectPageNumber,
    bool updateNextDocuments,
    bool updateUI,
    bool updateLayout)
  {
    string attributeValue1 = this.GetAttributeValue(DocumentTreeNode.AttributeName_DocPageCount, false);
    string attributeValue2 = this.GetAttributeValue(DocumentTreeNode.AttributeName_LastDocPageNumber, false);
    string attributeValue3 = this.GetAttributeValue(DocumentTreeNode.AttributeName_ComplectPageCount, false);
    PageEnumerator pageEnumerator = new PageEnumerator((DocumentTreeNode) this, startPage);
    int num = this.StartPageNumber;
    this.startComplectPageNumber = startComplectPageNumber;
    int startComplectPageNumber1 = startComplectPageNumber + this.StartPageNumber - 1;
    PageData previousPage = (PageData) null;
    if (startPage != null && startPage.Parent != null)
    {
      previousPage = ImDocumentData.GetPrevPage(startPage.Parent, startPage.Index, false);
      if (previousPage != null)
      {
        if (previousPage.IsChildForNode((DocumentTreeNode) this, false))
          num = previousPage.PageNumber + 1;
        if (this.IsPartOfComplectPageNumbering)
          startComplectPageNumber1 = previousPage.ComplectPageNumber + 1;
      }
    }
    while (pageEnumerator.MoveNext())
    {
      pageEnumerator.Current.SetPageNumber(num++, updateUI, updateLayout);
      pageEnumerator.Current.UpdateHierarhicalPageNumber(previousPage, updateUI, updateLayout);
      if (this.IsPartOfComplectPageNumbering)
        pageEnumerator.Current.SetComplectPageNumber(startComplectPageNumber1++, updateUI, updateLayout);
      previousPage = pageEnumerator.Current;
    }
    if (updateNextDocuments && this.Parent != null)
    {
      DocumentsComplect rootComplect = this.GetRootComplect();
      if (rootComplect != null)
      {
        ImDocumentData nextDocument = DocumentsComplect.GetNextDocument(this.Parent, this.Index, false);
        if (nextDocument != null)
          rootComplect.UpdatePageNumbers(nextDocument, startComplectPageNumber1, updateUI, updateLayout);
      }
    }
    this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_DocPageCount, (object) attributeValue1, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_DocPageCount, false), updateUI, updateLayout));
    this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_LastDocPageNumber, (object) attributeValue2, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_LastDocPageNumber, false), updateUI, updateLayout));
    this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_ComplectPageCount, (object) attributeValue3, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_ComplectPageCount, false), updateUI, updateLayout));
    this.OnAfterUpdatePageNumbers(new AfterUpdatePageNumbers_EventArgs(updateUI, updateLayout));
    return startComplectPageNumber1;
  }

  /// <summary>Событие После вызова обновления номеров страниц</summary>
  public event AfterUpdatePageNumbers_EventHandler AfterUpdatePageNumbers
  {
    add => this.afterUpdatePageNumbers += value;
    remove => this.afterUpdatePageNumbers -= value;
  }

  /// <summary>Вызывает событие AfterUpdatePageNumbers</summary>
  /// <param name="e">Аргументы события</param>
  protected void OnAfterUpdatePageNumbers(AfterUpdatePageNumbers_EventArgs e)
  {
    if (this.afterUpdatePageNumbers == null)
      return;
    this.afterUpdatePageNumbers((object) this, e);
  }

  /// <summary>Номер первой страницы документа</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_148")]
  [CustomDescription("Attribute.Interfaces.Document_149")]
  [CustomCategory("Attribute.Interfaces.Document_150")]
  public int StartPageNumber
  {
    [DebuggerStepThrough] get => this.startPageNumber;
    set => this.SetStartPageNumber(value, true, true);
  }

  /// <summary>Назначить новое значение свойству StartPageNumber</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="updatePageNumbers">Пересчитать номера страниц</param>
  /// <param name="updateUI">Обновить вид документа</param>
  public void SetStartPageNumber(int value, bool updatePageNumbers, bool updateUI)
  {
    if (this.startPageNumber == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "StartPageNumber", (object) this.StartPageNumber, (object) value);
    this.startPageNumber = value;
    this.overrideFlags |= OverrideFlags.StartPageNumber;
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (updatePageNumbers)
    {
      DocumentsComplect rootComplect = this.GetRootComplect();
      if (rootComplect != null)
        rootComplect.UpdatePageNumbers(this, this.startComplectPageNumber, updateUI, updateUI);
      else
        this.UpdatePageNumbers((PageData) null, this.startComplectPageNumber, false, updateUI, updateUI);
    }
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Номер первой страницы документа в комплекте документов</summary>
  [Browsable(false)]
  public int StartComplectPageNumber
  {
    [DebuggerStepThrough] get => this.startComplectPageNumber;
  }

  /// <summary>Назначить новое значение StartComplectPageNumber</summary>
  /// <param name="value">Значение</param>
  public void AssignStartComplectPageNumber(int value) => this.startComplectPageNumber = value;

  /// <summary>Документ участвует в сквозной нумерации страниц комплекта документов</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_589")]
  [CustomDescription("Attribute.Interfaces.Document_590")]
  [CustomCategory("Attribute.Interfaces.Document_150")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool IsPartOfComplectPageNumbering
  {
    get
    {
      if (this.isPartOfComplectPageNumbering.HasValue)
        return this.isPartOfComplectPageNumbering.Value;
      return this.Template == null || ((ImDocumentData) this.Template).IsPartOfComplectPageNumbering;
    }
    set => this.SetIsPartOfComplectPageNumbering(value, true, true);
  }

  /// <summary>Назначить новое значение свойству StartPageNumber</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="updatePageNumbers">Пересчитать номера страниц</param>
  /// <param name="updateUI">Обновить вид документа</param>
  public void SetIsPartOfComplectPageNumbering(bool value, bool updatePageNumbers, bool updateUI)
  {
    bool? complectPageNumbering = this.isPartOfComplectPageNumbering;
    bool flag = value;
    if (complectPageNumbering.GetValueOrDefault() == flag & complectPageNumbering.HasValue)
      return;
    this.isPartOfComplectPageNumbering = new bool?(value);
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "IsPartOfComplectPageNumbering", (object) this.IsPartOfComplectPageNumbering, (object) value);
    this.isPartOfComplectPageNumbering = new bool?(value);
    this.SetPropertiesChangedFlag(true, true, false, false, false);
    DocumentsComplect rootComplect = this.GetRootComplect();
    if (rootComplect != null)
      rootComplect.UpdatePageNumbers(this, this.startComplectPageNumber, updateUI, updateUI);
    else
      this.UpdatePageNumbers((PageData) null, this.startComplectPageNumber, false, updateUI, updateUI);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Документ участвует в расчёте общего количества страниц комплекта документов.
  /// Не путать с IsPartOfComplectPageNumbering</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_596")]
  [CustomDescription("Attribute.Interfaces.Document_597")]
  [CustomCategory("Attribute.Interfaces.Document_150")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool IsPartOfComplectPageCount
  {
    get
    {
      if (this.isPartOfComplectPageCount.HasValue)
        return this.isPartOfComplectPageCount.Value;
      return this.Template == null || ((ImDocumentData) this.Template).IsPartOfComplectPageCount;
    }
    set => this.SetIsPartOfComplectPageCount(value, true, true);
  }

  /// <summary>Назначить новое значение свойству StartPageNumber</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="updatePageNumbers">Пересчитать номера страниц</param>
  /// <param name="updateUI">Обновить вид документа</param>
  public void SetIsPartOfComplectPageCount(bool value, bool updatePageNumbers, bool updateUI)
  {
    bool? complectPageCount = this.isPartOfComplectPageCount;
    bool flag = value;
    if (complectPageCount.GetValueOrDefault() == flag & complectPageCount.HasValue)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "IsPartOfComplectPageCount", (object) this.IsPartOfComplectPageCount, (object) value);
    this.isPartOfComplectPageCount = new bool?(value);
    this.SetPropertiesChangedFlag(true, true, false, false, false);
    DocumentsComplect rootComplect = this.GetRootComplect();
    if (rootComplect != null)
      rootComplect.UpdatePageNumbers(this, this.startComplectPageNumber, updateUI, updateUI);
    else
      this.UpdatePageNumbers((PageData) null, this.startComplectPageNumber, false, updateUI, updateUI);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Группировать записи под общим заголовком.</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_607")]
  [CustomDescription("Attribute.Interfaces.Document_608")]
  [CustomCategory("Attribute.Interfaces.Document_150")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool DynamicGroupHeaderIsEnabled
  {
    get
    {
      bool? propertyValue = (bool?) this.GetPropertyValue(nameof (DynamicGroupHeaderIsEnabled));
      if (propertyValue.HasValue)
        return propertyValue ?? false;
      return this.DocumentTemplate != null && this.DocumentTemplate.DynamicGroupHeaderIsEnabled;
    }
    set => this.SetDynamicGroupHeaderIsEnabled(value, false, false);
  }

  /// <summary>Назначить новое значение свойству DynamicGroupHeaderIsEnabled</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="updatePageNumbers">Пересчитать номера страниц</param>
  /// <param name="updateUI">Обновить вид документа</param>
  public void SetDynamicGroupHeaderIsEnabled(bool value, bool updateLayout, bool updateUI)
  {
    if (this.DynamicGroupHeaderIsEnabled == value)
      return;
    if (this.OwnerDocument?.UndoManager != null && !this.OwnerDocument.IsLoading)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "DynamicGroupHeaderIsEnabled", (object) this.DynamicGroupHeaderIsEnabled, (object) value);
    this.SetPropertyValue((object) value, "DynamicGroupHeaderIsEnabled");
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.UpdateLayout(0, true, updateUI);
    if (updateLayout && this.IsTemplate)
      this.ConnectionList.Where<ReferenceToNode>((Func<ReferenceToNode, bool>) (c => c is ReferenceToTemplate)).Select<ReferenceToNode, ImDocumentData>((Func<ReferenceToNode, ImDocumentData>) (r => r.OwnerDocument)).ToList<ImDocumentData>().ForEach((Action<ImDocumentData>) (d => d?.UpdateLayout(0, true, false, updateUI, true)));
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Количество страниц в документе</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_532")]
  [CustomDescription("Attribute.Interfaces.Document_533")]
  [CustomCategory("Attribute.Interfaces.Document_150")]
  public int PageCount
  {
    [DebuggerStepThrough] get => this.nodes != null ? this.nodes.Count : 0;
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
    this.UpdatePageNumbers(child as PageData, this.startComplectPageNumber, true, updateUI, updateLayout);
    base.PostProcessAddChildNode(child, insertByShift, updateUI, updateLayout);
  }

  /// <summary>Герерирует событие ChildNodeRemoved</summary>
  public override void OnChildNodeRemoved(ChildNode_EventArgs e)
  {
    e.Child.IdService = (IUniqueIdService) null;
    this.UpdatePageNumbers(ImDocumentData.GetNextPage((DocumentTreeNode) this, e.Index - 1, false), this.startComplectPageNumber, true, e.UpdateUI, e.UpdateLayout);
    if (this.IsTemplate)
    {
      foreach (PageData pageData in this)
      {
        if (pageData.NextPageTemplateId == e.Child.Id)
          pageData.NextPageTemplateId = (string) null;
      }
    }
    base.OnChildNodeRemoved(e);
  }

  /// <summary>Событие Изменено свойство Modified</summary>
  public event ModifiedChanged_EventHandler ModifiedChanged
  {
    add => this.modifiedChanged += value;
    remove => this.modifiedChanged -= value;
  }

  /// <summary>Вызывает событие ModifiedChanged</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnModifiedChanged(ModifiedChanged_EventArgs e)
  {
    if (this.modifiedChanged == null)
      return;
    this.modifiedChanged((object) this, e);
  }

  /// <summary>Событие Закончена разбивка страницы</summary>
  public event DistributePageFinished_EventHandler DistributePageFinished
  {
    add => this.distributePageFinished += value;
    remove => this.distributePageFinished -= value;
  }

  /// <summary>Герерирует событие DistributePageFinished</summary>
  public virtual void OnDistributePageFinished(DistributePageFinishedArgs e)
  {
    if (this.distributePageFinished == null)
      return;
    this.distributePageFinished((object) this, e);
  }

  /// <summary>Обновить ссылки на страницу через её идентификатор</summary>
  /// <param name="oldPageId">Старое значение идентификатора</param>
  /// <param name="newPageId">Новое значение идентификатора</param>
  public void UpdateIDPageLinks(string oldPageId, string newPageId)
  {
    if (string.IsNullOrEmpty(oldPageId))
      return;
    foreach (PageData pageData in this)
    {
      if (pageData.NextPageTemplateId == oldPageId)
        pageData.NextPageTemplateId = newPageId;
      if (pageData.LastPageTemplateId == oldPageId)
        pageData.LastPageTemplateId = newPageId;
    }
  }

  /// <summary>Положение страницы</summary>
  [TypeConverter(typeof (CustomBooleanConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_151")]
  [CustomDescription("Attribute.Interfaces.Document_152")]
  [CustomCategory("Attribute.Interfaces.Document_153")]
  public virtual bool FitToPage
  {
    [DebuggerStepThrough] get => this.fitToPage;
    set
    {
      if (this.fitToPage == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (FitToPage), (object) this.FitToPage, (object) value);
      this.fitToPage = value;
      this.overrideFlags |= OverrideFlags.FitToPage;
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Сервис уведомления о событиях</summary>
  [Browsable(false)]
  public static IDocumentNotifyService NotifyService
  {
    get => ImDocumentData.notifyService;
    set => ImDocumentData.notifyService = value;
  }

  /// <summary>Положение страницы</summary>
  [TypeConverter(typeof (PointFConverter))]
  [CustomDisplayName("Attribute.Interfaces.Document_154")]
  [CustomDescription("Attribute.Interfaces.Document_155")]
  [CustomCategory("Attribute.Interfaces.Document_156")]
  public virtual PointF ShiftPage
  {
    [DebuggerStepThrough] get => this.shiftPage;
    set
    {
      if (!(this.shiftPage != value))
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (ShiftPage), (object) this.ShiftPage, (object) value);
      this.shiftPage = value;
      this.overrideFlags |= OverrideFlags.ShifPage;
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Получить суммарное смещение страниц. Смещение заданное в шаблоне + смещение заданное настройками для принтера</summary>
  /// <param name="printerName">Имя принтера</param>
  /// <returns></returns>
  protected virtual PointF GetSummaryShiftForPage(string printerName) => this.ShiftPage;

  /// <summary>Объект посылающий данные на печать</summary>
  [Browsable(false)]
  public PrintDocument PrintDocument
  {
    [DebuggerStepThrough] get
    {
      if (this.printDocument == null)
        this.InitPrintDocument();
      return this.printDocument;
    }
  }

  [Browsable(false)]
  public ImPrintSettings ImPrintSettings
  {
    get
    {
      DocumentsComplect rootComplect = this.GetRootComplect();
      return rootComplect != null ? rootComplect.ImPrintSettings : this.imPrintSettings;
    }
    set => this.imPrintSettings = value;
  }

  /// <summary>Обработчик события "Начало печати документа"</summary>
  public virtual void BeginPrint(object sender, PrintEventArgs e)
  {
    try
    {
      if (this.nodes == null || this.nodes.Count == 0)
      {
        e.Cancel = true;
      }
      else
      {
        if (this.LoadFromStreamThread != null && (this.LoadFromStreamThread.ThreadState & System.Threading.ThreadState.Stopped) == System.Threading.ThreadState.Running)
          this.LoadFromStreamThread.Join();
        if (this.DistributeThread != null && (this.DistributeThread.ThreadState & System.Threading.ThreadState.Stopped) == System.Threading.ThreadState.Running)
          this.DistributeThread.Join();
        this.ImPrintSettings.Reset();
        PrintDocument printDoc = sender as PrintDocument;
        foreach (DocumentTreeNode node in this.Nodes)
        {
          PageData page = node as PageData;
          if (this.NeedPrintPage(printDoc, page))
            this.ImPrintSettings.PagesForPrint.Add(page);
        }
        this.NowPrinting = true;
        if (ImDocumentData.NotifyService != null)
          ImDocumentData.NotifyService.FireBeforePrint((object) this, new BeforePrintDocumentEventArgs((DocumentTreeNode) this));
        this.UpdatePrintLinks(true, false, false, true);
      }
    }
    catch (Exception ex)
    {
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      }
      e.Cancel = true;
    }
  }

  /// <summary>Печатать ли страницу согласно настройкам печати</summary>
  /// <param name="page">Старница</param>
  /// <returns>true, если печатать страницу</returns>
  public virtual bool NeedPrintPage(PrintDocument printDoc, PageData page)
  {
    try
    {
      int globalPageNumber = page.GlobalPageNumber;
      if (printDoc.PrinterSettings.PrintRange == PrintRange.AllPages || printDoc.PrintController.IsPreview)
        return true;
      return (printDoc.PrinterSettings.PrintRange == PrintRange.Selection || printDoc.PrinterSettings.PrintRange == PrintRange.SomePages) && this.ImPrintSettings.SelectedPrintPages.Contains(globalPageNumber);
    }
    catch (Exception ex)
    {
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      }
    }
    return false;
  }

  /// <summary>Печать страницы</summary>
  /// <param name="printDoc">PrintDocument который печатается</param>
  /// <param name="e">параметр события PrintDocument.PrintPage</param>
  /// <returns>была ли отпечатана хоть 1 страница</returns>
  public virtual bool PrintPage(PrintDocument printDoc, PrintPageEventArgs e, PageData curPage)
  {
    if (printDoc == null)
      throw new ArgumentNullException(nameof (printDoc));
    if (e == null)
      throw new ArgumentNullException(nameof (e));
    try
    {
      bool flag = false;
      bool isPreview = printDoc.PrintController.IsPreview;
      PageData page = curPage != null ? curPage : this.nodes[this.ImPrintSettings.CurrentPrintPageIndex] as PageData;
      if (this.NeedPrintPage(printDoc, page))
      {
        GraphicsUnit pageUnit = e.Graphics.PageUnit;
        PointF summaryShiftForPage = this.GetSummaryShiftForPage(printDoc.PrinterSettings.PrinterName);
        if (page.Landscape)
          e.Graphics.TranslateTransform(summaryShiftForPage.Y, summaryShiftForPage.X);
        else
          e.Graphics.TranslateTransform(summaryShiftForPage.X, summaryShiftForPage.Y);
        bool? fitToPagePrint = this.ImPrintSettings.FitToPagePrint;
        bool fitToPage;
        if (fitToPagePrint.HasValue)
        {
          fitToPagePrint = this.ImPrintSettings.FitToPagePrint;
          fitToPage = fitToPagePrint.Value;
        }
        else
          fitToPage = this.FitToPage;
        if (fitToPage)
        {
          RectangleF rectangleF = e.Graphics.VisibleClipBounds;
          if (isPreview)
          {
            RectangleF printableArea = e.PageSettings.PrintableArea;
            Size size = Size.Round(printableArea.Size);
            if (e.PageSettings.Landscape)
            {
              size.Width = size.Height;
              ref Size local = ref size;
              printableArea = e.PageSettings.PrintableArea;
              int width = (int) printableArea.Size.Width;
              local.Height = width;
            }
            rectangleF = new RectangleF((PointF) new Point(0, 0), (SizeF) size);
          }
          if (!rectangleF.Location.IsEmpty)
            LogManager.AddLine("ImDocument.printDocument_PrintPage. Видимая часть смещена." + (object) page);
          if ((double) rectangleF.Location.X >= 0.0 && (double) rectangleF.Location.Y >= 0.0)
          {
            float sx;
            float sy;
            if (page.Landscape && (double) rectangleF.Width < (double) rectangleF.Height != e.PageBounds.Width < e.PageBounds.Height)
            {
              sx = rectangleF.Height / (float) e.PageBounds.Width;
              sy = rectangleF.Width / (float) e.PageBounds.Height;
            }
            else
            {
              sx = rectangleF.Width / (float) e.PageBounds.Width;
              sy = rectangleF.Height / (float) e.PageBounds.Height;
            }
            e.Graphics.ScaleTransform(sx, sy);
          }
          else
            LogManager.AddLine("ImDocument.printDocument_PrintPage. Видимая часть смещена в отрицательную область. Страница " + (object) page);
        }
        e.Graphics.PageUnit = pageUnit;
        page.Draw(new DrawContext(new ImGraphics(e.Graphics), false, VisualNode.NoClipRectangle, 0, false, false, new MatrixWrapper()));
        flag = true;
      }
      return flag;
    }
    catch (Exception ex)
    {
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      }
      e.Cancel = true;
    }
    return false;
  }

  /// <summary>Обработчик события "Печать страницы"</summary>
  private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
  {
    try
    {
      bool flag1 = false;
      if (this.ImPrintSettings.HasCurrentPage)
      {
        PageData currentPrintPage = this.ImPrintSettings.CurrentPrintPage;
        bool flag2 = false;
        if (currentPrintPage.FromNewPage && e.PageSettings.PrinterSettings.Duplex != Duplex.Default && this.ImPrintSettings.PrintPageIndex % 2 == 1)
          flag2 = true;
        if (!flag2)
        {
          flag1 = this.PrintPage(sender as PrintDocument, e, currentPrintPage);
          ++this.ImPrintSettings.CurrentPrintPageIndex;
        }
        ++this.ImPrintSettings.PrintPageIndex;
      }
      e.HasMorePages = this.ImPrintSettings.HasCurrentPage;
      if (e.HasMorePages || flag1)
        return;
      e.Cancel = true;
    }
    catch (Exception ex)
    {
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      }
      e.Cancel = true;
    }
  }

  /// <summary>Обработчик события "Конец печати"</summary>
  protected virtual void EndPrint(object sender, PrintEventArgs e)
  {
    try
    {
      this.ImPrintSettings.Reset();
      this.NowPrinting = false;
      if (ImDocumentData.NotifyService != null)
        ImDocumentData.NotifyService.FireAfterPrint((object) this, new AfterPrintDocumentEventArgs((DocumentTreeNode) this));
      this.UpdatePrintLinks(true, false, false, true);
    }
    catch (Exception ex)
    {
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      }
      e.Cancel = true;
    }
  }

  protected void PrintQueryPageSettings(PageData page, QueryPageSettingsEventArgs e)
  {
    PageSettings pageSettings = e.PageSettings;
    page.SetPagePrintSettings(ref pageSettings);
    if (e.PageSettings != null)
      return;
    e.PageSettings = pageSettings;
  }

  /// <summary>Обработчик события перед печатью каждой страницы</summary>
  private void printDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
  {
    try
    {
      if (!this.ImPrintSettings.HasCurrentPage)
        return;
      this.PrintQueryPageSettings(this.ImPrintSettings.CurrentPrintPage, e);
    }
    catch (Exception ex)
    {
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      }
      e.Cancel = true;
    }
  }

  /// <summary>Инициализировать объект для печати документа</summary>
  public void InitPrintDocument()
  {
    try
    {
      if (this.printDocument != null)
        return;
      this.printDocument = new PrintDocument();
      this.printDocument.BeginPrint += new PrintEventHandler(this.BeginPrint);
      this.printDocument.EndPrint += new PrintEventHandler(this.EndPrint);
      this.printDocument.QueryPageSettings += new QueryPageSettingsEventHandler(this.printDocument_QueryPageSettings);
      this.printDocument.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
    }
    catch (Exception ex)
    {
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Interfaces.Document_168"));
      }
    }
  }

  /// <summary>Шаблон на основе которого создается это документ</summary>
  [Browsable(false)]
  public ImDocumentData DocumentTemplate
  {
    [DebuggerStepThrough] get => (ImDocumentData) this.Template;
  }

  /// <summary>Получить первый шаблон страницы</summary>
  /// <returns>Первый шаблон страницы</returns>
  public virtual PageData GetFirstPageTemplate()
  {
    if (!this.IsTemplate)
      return this.DocumentTemplate != null ? this.DocumentTemplate.GetFirstPageTemplate() : (PageData) null;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageData node1)
        return node1;
      if (this.nodes[index] is DocumentSection node2)
        node2.GetFirstPageTemplate();
    }
    return (PageData) null;
  }

  /// <summary>Найти шаблон страницы, которая будет первой в новом документе</summary>
  public virtual PageData FindFirstPageTemplateForNewDocument()
  {
    if (!this.IsTemplate)
      return this.DocumentTemplate != null ? this.DocumentTemplate.GetFirstPageTemplate() : (PageData) null;
    PageData templateForNewDocument1 = (PageData) null;
    foreach (PageData templateForNewDocument2 in this)
    {
      if (templateForNewDocument2.CloneByTemplateWithParent)
        return templateForNewDocument2;
      if (templateForNewDocument1 == null)
        templateForNewDocument1 = templateForNewDocument2;
    }
    return templateForNewDocument1;
  }

  /// <summary>
  /// Найти в шаблоне таблицу данных, которая должна быть первой на созданных в новом документе страницах
  /// </summary>
  /// <returns></returns>
  public virtual TableData FindFirstMainTableTemplate()
  {
    if (!this.IsTemplate)
      return this.DocumentTemplate != null ? this.DocumentTemplate.FindFirstMainTableTemplate() : (TableData) null;
    foreach (PageData pageData in this.Where<PageData>((Func<PageData, bool>) (p => p.CloneByTemplateWithParent)))
    {
      TableData firstMainTable = pageData.FindFirstMainTable();
      if (firstMainTable != null)
        return firstMainTable;
    }
    foreach (PageData pageData in this.Where<PageData>((Func<PageData, bool>) (p => !p.CloneByTemplateWithParent)))
    {
      TableData firstMainTable = pageData.FindFirstMainTable();
      if (firstMainTable != null)
        return firstMainTable;
    }
    return (TableData) null;
  }

  /// <summary>Применить к элементу свойства шаблона</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isLoading">Вызов в процессе загрузки файла</param>
  public override void ApplyTemplateProperties(
    DocumentTreeNode template,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (template == null)
      return;
    base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
    if (!(template is ImDocumentData imDocumentData))
      return;
    if ((this.overrideFlags & OverrideFlags.ShifPage) == OverrideFlags.None)
      this.shiftPage = imDocumentData.shiftPage;
    if ((this.overrideFlags & OverrideFlags.FitToPage) == OverrideFlags.None)
      this.fitToPage = imDocumentData.fitToPage;
    if ((this.overrideFlags & OverrideFlags.AllowFormatingForReadOnly) == OverrideFlags.None)
      this.allowFormatingForReadOnlyText = imDocumentData.allowFormatingForReadOnlyText;
    if ((this.overrideFlags3 & OverrideFlags3.SaveValueFromRefToDBAttr) == OverrideFlags3.None)
      this.saveValueFromRefToDBAttr = imDocumentData.saveValueFromRefToDBAttr;
    if ((this.overrideFlags & OverrideFlags.StartPageNumber) == OverrideFlags.None)
      this.startPageNumber = imDocumentData.startPageNumber;
    this.isFormulaLib = imDocumentData.isFormulaLib;
    this.DefaultLeftRightMargin = imDocumentData.DefaultLeftRightMargin;
    this.DefaultTopBottomMargin = imDocumentData.DefaultTopBottomMargin;
    this.SetFixedRowSizeTrancateFraction(imDocumentData.FixedRowSizeTrancateFraction, updateLayout);
    if (!this.IsOverridden3(OverrideFlags3.DrawParentCellFrames))
      this.defaultDrawParentCellFrames = imDocumentData.defaultDrawParentCellFrames;
    int index1 = 0;
    for (int count = imDocumentData.documentFlows.Count; index1 < count; ++index1)
    {
      if (this.FindFlowIDFromTemplate(imDocumentData.documentFlows[index1]) == null)
      {
        FlowID flow = imDocumentData.documentFlows[index1].Clone();
        flow.TemplateFlowID = imDocumentData.documentFlows[index1];
        this.AddDocumentFlow(flow, true);
      }
    }
    this.defaultBorderLine = imDocumentData.DefaultBorderLine.Clone();
    for (int index2 = this.documentFlows.Count - 1; index2 >= 0; --index2)
    {
      if (this.documentFlows[index2].TemplateFlowID != null && !imDocumentData.documentFlows.Contains(this.documentFlows[index2].TemplateFlowID))
        this.documentFlows[index2].TemplateFlowID = (FlowID) null;
    }
  }

  /// <summary>Можно ли использовать заданный узел как шаблон</summary>
  /// <param name="node">Узел</param>
  /// <returns></returns>
  public override bool CanUseNodeAsTemplate(DocumentTreeNode node)
  {
    return node != null && node is ImDocumentData;
  }

  /// <summary>Корень дерева в котором должен находиться шаблон этого узла</summary>
  public override DocumentTreeNode TemplateRoot
  {
    [DebuggerStepThrough] get => this.Template;
  }

  /// <summary>Найти шаблон этого узла по идентификатору templateId</summary>
  /// <param name="templateId">Идентификатор шаблона</param>
  /// <returns>Шаблон узла</returns>
  public override DocumentTreeNode FindTemplate(string templateId)
  {
    if (templateId == null)
      throw new ArgumentNullException(nameof (templateId));
    return this.Template != null && this.Template.Id == templateId ? this.Template : (DocumentTreeNode) null;
  }

  /// <summary>Является ли страница шаблоном</summary>
  [ReadOnly(true)]
  [CustomDisplayName("Attribute.Interfaces.Document_157")]
  [CustomDescription("Attribute.Interfaces.Document_158")]
  [CustomCategory("Attribute.Interfaces.Document_159")]
  public override bool IsTemplate
  {
    [DebuggerStepThrough] get => this.isTemplate;
  }

  /// <summary>Установить флаг IsTemplate</summary>
  /// <param name="value">Значение</param>
  public virtual void SetIsTemplate(bool value)
  {
    if (this.isTemplate == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "IsTemplate", (object) this.IsTemplate, (object) value);
    this.isTemplate = value;
    if (this.isTemplate)
      this.DisconnectTemplateRecursive();
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Шаблон на основе которого создается это документ</summary>
  [Browsable(false)]
  public ImDocumentData DocumentDataTemplate
  {
    [DebuggerStepThrough] get => (ImDocumentData) this.Template;
  }

  /// <summary>Шаблон этого узла</summary>
  [Browsable(false)]
  public override DocumentTreeNode Template
  {
    [DebuggerStepThrough] get
    {
      return this.referenceToTemplate != null ? this.referenceToTemplate.NodeLink : (DocumentTreeNode) null;
    }
    set => this.AssignDocumentTemplate((ImDocumentData) value, true, true, true);
  }

  /// <summary>Назначить шаблон документу</summary>
  /// <param name="value">Шаблон</param>
  /// <param name="applyTemplate">Применить шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public void AssignDocumentTemplate(
    ImDocumentData value,
    bool applyTemplate,
    bool updateUI,
    bool updateLayout)
  {
    this.AssignDocumentTemplate(value, applyTemplate, false, updateUI, updateLayout);
  }

  /// <summary>Назначить шаблон документу</summary>
  /// <param name="value">Шаблон</param>
  /// <param name="applyTemplate">Применить шаблон</param>
  /// <param name="calledFromCatch">Вызов из обработчика исключения, чтобы вернуть старое значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void AssignDocumentTemplate(
    ImDocumentData value,
    bool applyTemplate,
    bool calledFromCatch,
    bool updateUI,
    bool updateLayout)
  {
    if (this.Template == value)
      return;
    ImDocumentData template = this.Template as ImDocumentData;
    try
    {
      this.SuspendUpdateLayout();
      this.SuspendUpdateGeometryRefreshUI();
      TemplateChanging_EventArgs e = new TemplateChanging_EventArgs((DocumentTreeNode) template, (DocumentTreeNode) value);
      this.OnTemplateChanging(e);
      if (e.Cancel)
        return;
      try
      {
        this.BeginChangingStructure();
        try
        {
          if (this.DocumentTemplate != null)
          {
            this.DocumentTemplate.TemplateOwner = (ImDocumentData) null;
            this.MergeFormulaLists(this.DocumentTemplate.FormulaList);
          }
          this.AssignTemplate((DocumentTreeNode) value, applyTemplate, false, false);
          if (value != null && this.DocumentTemplate != null)
          {
            value.TemplateOwner = this;
            this.DocumentTemplate.SetNeedUI(this.NeedUI, false);
          }
          this.UpdateTemplateLinks(applyTemplate, true, false, false);
        }
        finally
        {
          this.EndChangingStructure(updateUI, updateUI, false, false);
        }
        this.OnChanged(new Changed_EventArgs());
        this.OnTemplateChanged(new TemplateChanged_EventArgs((DocumentTreeNode) template, this.Template));
        if (this.OwnerDocument == null || this.OwnerDocument.IsLoading || this.OwnerDocument.UndoManager == null)
          return;
        this.OwnerDocument.UndoManager.CreateUndo((object) this, "Template", (object) template, (object) this.Template);
      }
      catch (Exception ex)
      {
        if (!calledFromCatch)
        {
          this.AssignDocumentTemplate(template, applyTemplate, true, updateUI, updateLayout);
          throw;
        }
        this.AssignDocumentTemplate((ImDocumentData) null, applyTemplate, true, updateUI, updateLayout);
        throw;
      }
    }
    finally
    {
      bool flag = updateUI && !updateLayout;
      this.ResumeUpdateLayout(flag, false);
      this.ResumeUpdateRefreshUI(flag, flag);
      if (updateLayout)
        this.UpdateLayout(true, updateUI);
    }
  }

  /// <summary>Происходит перед разбитием страницы</summary>
  public event PageDistribute_EventHandler BeforeDistributePage
  {
    add => this.beforeDistributePage += value;
    remove => this.beforeDistributePage -= value;
  }

  /// <summary>Герерирует событие BeforeDistributePage</summary>
  protected virtual void OnBeforeDistributePage(PageDistribute_EventArgs e)
  {
    if (this.beforeDistributePage == null)
      return;
    this.beforeDistributePage((object) this, e);
  }

  /// <summary>Событие Текст изменен в текстовых полях</summary>
  public event TextValidating_EventHandler TextValidating
  {
    add => this.textValidating += value;
    remove => this.textValidating -= value;
  }

  /// <summary>Вызывает событие Текст изменен в текстовых полях</summary>
  /// <param name="sender">Поле вызвавшее событие</param>
  /// <param name="e">Данные события</param>
  public virtual void OnTextValidating(object sender, TextValidating_EventArgs e)
  {
    if (this.textValidating == null)
      return;
    this.textValidating(sender, e);
  }

  /// <summary>Событие Текст изменен в текстовых полях</summary>
  public event TextChanged_EventHandler TextChanged
  {
    add => this.textChanged += value;
    remove => this.textChanged -= value;
  }

  /// <summary>Вызывает событие Текст изменен в текстовых полях</summary>
  /// <param name="sender">Поле вызвавшее событие</param>
  /// <param name="e">Данные события</param>
  public virtual void OnTextChanged(object sender, TextChanged_EventArgs e)
  {
    if (this.textChanged == null)
      return;
    this.textChanged(sender, e);
  }

  /// <summary>Происходит после разбития страницы</summary>
  public event PageDistribute_EventHandler AfterDistributePage
  {
    add => this.afterDistributePage += value;
    remove => this.afterDistributePage -= value;
  }

  /// <summary>Герерирует событие AfterDistributePage</summary>
  protected virtual void OnAfterDistributePage(PageDistribute_EventArgs e)
  {
    if (this.afterDistributePage == null)
      return;
    this.afterDistributePage((object) this, e);
  }

  /// <summary>Происходит когда заменён шаблон</summary>
  public event TemplateChanged_EventHandler TemplateChanged
  {
    add => this.templateChanged += value;
    remove => this.templateChanged -= value;
  }

  /// <summary>Герерирует событие TemplateChanged</summary>
  protected virtual void OnTemplateChanged(TemplateChanged_EventArgs e)
  {
    if (this.templateChanged == null)
      return;
    this.templateChanged((object) this, e);
  }

  /// <summary>Происходит перед заменой шаблона</summary>
  public event TemplateChanging_EventHandler TemplateChanging
  {
    add => this.templateChanging += value;
    remove => this.templateChanging -= value;
  }

  /// <summary>Генерирует событие TemplateChanging</summary>
  protected virtual void OnTemplateChanging(TemplateChanging_EventArgs e)
  {
    if (this.templateChanging == null)
      return;
    this.templateChanging((object) this, e);
  }

  /// <summary>Обновить ссылки на шаблоны. Восстановить по идентификатору шаблона ссылку на сам шаблон</summary>
  /// <param name="applyTemplate">Применить шаблон</param>
  /// <param name="recursive">Рекурсивно для подэлементов</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void UpdateTemplateLinks(
    bool applyTemplate,
    bool recursive,
    bool updateUI,
    bool updateLayout)
  {
    if (applyTemplate)
    {
      DocumentTreeNode template = this.Template;
      if (template == null)
        return;
      this.ApplyTemplateTreeStructure(template, false, false, updateUI, updateLayout);
      if (this.nodes != null & recursive)
      {
        for (int index = 0; index < this.nodes.Count; ++index)
          this.nodes[index].UpdateTemplateLinks(applyTemplate, recursive, updateUI, updateLayout);
      }
      this.ApplyTemplateProperties(updateUI, updateLayout);
    }
    else
    {
      if (!recursive || this.nodes == null)
        return;
      for (int index = 0; index < this.nodes.Count; ++index)
        this.nodes[index].UpdateTemplateLinks(applyTemplate, recursive, updateUI, updateLayout);
    }
  }

  /// <summary>Владелец этого шаблона. Если это не шаблон, то null</summary>
  [Browsable(false)]
  public ImDocumentData TemplateOwner
  {
    [DebuggerStepThrough] get => this.templateOwner;
    set => this.templateOwner = value;
  }

  /// <summary>Создать страницу по шаблону, не вставляя ее в документ</summary>
  /// <param name="pageTemplateId">Идентификатор шаблона страницы</param>
  /// <param name="isNextPage">Создаётся следующая страница для продолжения разбивки</param>
  /// <returns>Страницу по шаблону</returns>
  public PageData ClonePageFromTemplate(string pageTemplateId, bool isNextPage)
  {
    PageData pageData1 = (PageData) null;
    PageData pageData2 = (PageData) null;
    if (this.Template != null)
      pageData2 = this.Template.FindNode(pageTemplateId) as PageData;
    if (pageData2 != null)
      pageData1 = (PageData) pageData2.CloneFromTemplate(true, !isNextPage);
    return pageData1;
  }

  /// <summary>Создать и добавить новую страницу</summary>
  public PageData NewPage() => this.NewPage((DocumentTreeNode) this);

  /// <summary>Создать и добавить новую страницу</summary>
  public virtual PageData NewPage(DocumentTreeNode parent) => new PageData(parent);

  /// <summary>Создать шаблон для следующей страницы. Применимо только к шаблонам документа.</summary>
  /// <param name="basePage">Шаблон предыдущей страницы</param>
  /// <returns>Шаблон следующей страницы</returns>
  public virtual PageData CreateNextPageTemplate(PageData basePage)
  {
    if (basePage == null)
      throw new ArgumentNullException(nameof (basePage));
    if (!basePage.IsTemplate)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_64"), nameof (basePage));
    if (basePage.Parent == null)
      return (PageData) null;
    PageData child = (PageData) basePage.Clone(true, true);
    child.Id = basePage.Id + LocalizationHolder.rm.GetString("Interfaces.Document_65");
    basePage.Parent.InsertChildNode(basePage.Index + 1, (DocumentTreeNode) child, false, true, true, true);
    basePage.NextPageTemplateId = child.Id;
    foreach (TableData startFlowTable in child.GetStartFlowTables())
    {
      startFlowTable.UsePreviousTableTemplates = true;
      startFlowTable.Clear(false, false);
    }
    return child;
  }

  /// <summary>Создать копию элемента используя этот узел как шаблон</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyDataNodes">Копировать узлы-данные в таблицах</param>
  /// <returns>Копия узла</returns>
  public override DocumentTreeNode CloneFromTemplate(bool copyChildren, bool copyDataNodes)
  {
    return (DocumentTreeNode) new ImDocumentData(this, true, copyChildren);
  }

  /// <summary>Создать новый элемент документа по заданному шаблону</summary>
  /// <param name="templateID">Идентификатор элемента шаблона документа</param>
  /// <returns></returns>
  public DocumentTreeNode CreateDocumentElementFromTemplate(string templateID)
  {
    switch (templateID)
    {
      case null:
        throw new ArgumentNullException(nameof (templateID));
      case "":
        throw new ArgumentException("Значение аргумента не может быть пустым", nameof (templateID));
      default:
        return (this.Template?.FindNode(templateID) ?? throw new ImDocumentException($"Не найден элемент шаблона '{templateID}'")).CloneFromTemplate();
    }
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
    if (!(src is ImDocumentData imDocumentData))
      return;
    this.startPageNumber = imDocumentData.startPageNumber;
    this.startComplectPageNumber = imDocumentData.startComplectPageNumber;
    if (!templateClone)
    {
      this.isTemplate = imDocumentData.isTemplate;
      this.defaultNonSkipAtStartPage = imDocumentData.defaultNonSkipAtStartPage;
      this.isPartOfComplectPageNumbering = imDocumentData.isPartOfComplectPageNumbering;
      this.isPartOfComplectPageCount = imDocumentData.isPartOfComplectPageCount;
    }
    this.shiftPage = imDocumentData.shiftPage;
    this.fitToPage = imDocumentData.fitToPage;
    this.isFormulaLib = imDocumentData.isFormulaLib;
    if (!templateClone && imDocumentData.formulaList != null)
      this.formulaList = (ImDocumentData) imDocumentData.formulaList.Clone();
    this.documentFlows.Clear();
    if (imDocumentData.documentFlows.Count > 0)
    {
      int index = 0;
      for (int count = imDocumentData.documentFlows.Count; index < count; ++index)
      {
        FlowID flow = imDocumentData.documentFlows[index].Clone();
        if (templateClone)
          flow.TemplateFlowID = imDocumentData.documentFlows[index];
        links.Add((object) imDocumentData.documentFlows[index], (object) flow);
        this.AddDocumentFlow(flow, false);
      }
    }
    this.materialKeyWords = imDocumentData.materialKeyWords != null ? (imDocumentData.materialKeyWords.Count <= 0 ? new List<string>() : new List<string>((IEnumerable<string>) imDocumentData.materialKeyWords)) : (List<string>) null;
    if (this.reference != null)
    {
      this.reference.DisconnectLink();
      this.reference = (ReferenceBase) null;
    }
    if (imDocumentData.reference != null & copyData)
    {
      this.reference = imDocumentData.reference.Clone();
      this.reference.AssignOwnerNode((DocumentTreeNode) this);
    }
    this.dbAttributeAutoSave = imDocumentData.dbAttributeAutoSave;
    this.designation = imDocumentData.designation;
    this.revision = imDocumentData.revision;
    this.allowFormatingForReadOnlyText = imDocumentData.allowFormatingForReadOnlyText;
    this.saveValueFromRefToDBAttr = imDocumentData.saveValueFromRefToDBAttr;
    this.DefaultTopBottomMargin = imDocumentData.DefaultTopBottomMargin;
    this.DefaultLeftRightMargin = imDocumentData.DefaultLeftRightMargin;
    this.fixedRowSizeTrancateFraction = imDocumentData.FixedRowSizeTrancateFraction;
    if (imDocumentData.defaultCharFormat != null)
      this.defaultCharFormat = imDocumentData.defaultCharFormat.Clone();
    if (imDocumentData.defaultParagraphFormat != null)
      this.defaultParagraphFormat = imDocumentData.defaultParagraphFormat.Clone();
    if (!templateClone && imDocumentData.defaultBorderLine != null)
      this.defaultBorderLine = imDocumentData.defaultBorderLine.Clone();
    if (!templateClone && imDocumentData.defaultPageBorderLine != null)
      this.defaultPageBorderLine = imDocumentData.defaultPageBorderLine.Clone();
    this.defaultDrawParentCellFrames = imDocumentData.defaultDrawParentCellFrames;
  }

  /// <summary>Метод вызываемый при десериализации.
  /// Реализация IDeserializationCallback</summary>
  public override void OnDeserialization(object sender)
  {
    base.OnDeserialization(sender);
    if (this.reference != null)
    {
      this.reference.AssignOwnerNode((DocumentTreeNode) this);
      if (this.reference is ReferenceToNode reference)
        reference.UpdateLink(false, false);
    }
    ImDocumentData documentTemplate = this.DocumentTemplate;
    int index = 0;
    for (int count = this.documentFlows.Count; index < count; ++index)
    {
      if (this.documentFlows[index].TemplateFlowID != null && (documentTemplate == null || !documentTemplate.documentFlows.Contains(this.documentFlows[index].TemplateFlowID)))
        this.documentFlows[index].TemplateFlowID = (FlowID) null;
    }
  }

  /// <summary>Заменить запрещенные символы для имен файлов</summary>
  /// <param name="fileName">Имя файла</param>
  /// <returns></returns>
  public static string ReplaceForbiddenSymbols(string fileName)
  {
    return OSHelper.ReplaceForbiddenSymbols(fileName, '_');
  }

  /// <summary>Сгенерировать уникальное имя файла. Используется при сохранении во временный файл</summary>
  /// <param name="fullName">Начальное имя файла (с путем)</param>
  /// <returns>Уникальное имя файла</returns>
  public static string GenerateUniqueFileName(string fullName)
  {
    if (File.Exists(fullName))
    {
      FileInfo fileInfo = new FileInfo(fullName);
      string baseName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
      fullName = ImDocumentData.GenerateUniqueFileName(fileInfo.DirectoryName, baseName, fileInfo.Extension);
    }
    return fullName;
  }

  /// <summary>Сгенерировать уникальное имя файла. Используется при сохранении во временный файл</summary>
  /// <param name="dir">Путь</param>
  /// <param name="baseName">Базовое имя</param>
  /// <param name="extension">Расширение</param>
  /// <returns>Уникальное имя файла</returns>
  public static string GenerateUniqueFileName(string dir, string baseName, string @extension)
  {
    if (dir == null)
      dir = "";
    if (baseName == null)
      baseName = "";
    if (@extension == null)
      @extension = "";
    if (dir.Length > 0)
    {
      char ch = dir[dir.Length - 1];
      if (ch.ToString((IFormatProvider) CultureInfo.InvariantCulture) != "\\")
      {
        ch = dir[dir.Length - 1];
        if (ch.ToString((IFormatProvider) CultureInfo.InvariantCulture) != "/")
          dir += "\\";
      }
    }
    string path = $"{dir}{baseName}{@extension}";
    for (int index = 1; index < int.MaxValue; ++index)
    {
      path = $"{dir}{baseName}{index}{@extension}";
      if (!File.Exists(path))
        return path;
    }
    throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_66") + path);
  }

  /// <summary>Сохранить документ в файл в формате XML.
  /// Если файл с этим именем уже существует, он будет переписан!</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="packFile">Сжимать файл</param>
  public void SaveToXml(string fileName, bool packFile)
  {
    this.SaveToXml(fileName, packFile, false);
  }

  /// <summary>Сохранить документ в файл в формате XML.
  /// Если файл с этим именем уже существует, он будет переписан!</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="packFile">Сжимать файл</param>
  /// <param name="saveActiveReferenceToDBAttr">Принудительно сохранение всех полученных значений в ссылках на атрибуты БД</param>
  public void SaveToXml(string fileName, bool packFile, bool saveActiveReferenceToDBAttr)
  {
    bool valuesFromRefToDbAttr = this.ForceSaveValuesFromRefToDBAttr;
    this.ForceSaveValuesFromRefToDBAttr = saveActiveReferenceToDBAttr;
    try
    {
      string uniqueFileName = ImDocumentData.GenerateUniqueFileName(fileName + ".tmp");
      if (packFile)
      {
        using (ZipOutputStream zipOutputStream = new ZipOutputStream((Stream) File.Create(uniqueFileName)))
        {
          byte[] numArray = new byte[4096 /*0x1000*/];
          zipOutputStream.SetLevel(9);
          ZipEntry entry = new ZipEntry("Document.imdx");
          zipOutputStream.PutNextEntry(entry);
          this.SaveToXml((Stream) zipOutputStream);
        }
      }
      else
      {
        FileStream fileStream = new FileStream(uniqueFileName, FileMode.OpenOrCreate, FileAccess.Write);
        try
        {
          this.SaveToXml((Stream) fileStream);
        }
        finally
        {
          fileStream.Close();
        }
      }
      if (File.Exists(fileName))
        File.Delete(fileName);
      File.Move(uniqueFileName, fileName);
      this.Modified = false;
    }
    finally
    {
      this.ForceSaveValuesFromRefToDBAttr = valuesFromRefToDbAttr;
    }
  }

  /// <summary>Метод вызывается перед сохранением документа</summary>
  public void BeforeSaveDocument()
  {
    List<DocumentTreeNode> childNodes = DocumentTreeNode.GetChildNodes((DocumentTreeNode) this);
    bool flag = false;
    foreach (DocumentTreeNode documentTreeNode in childNodes)
    {
      if (documentTreeNode is INodeWithReference nodeWithReference && nodeWithReference.Reference is ReferenceToNodeAttributeBase reference && reference.AttributeName == DocumentTreeNode.AttributeName_CheckSum)
        flag = true;
    }
    this.SetAttributeValue(DocumentTreeNode.AttributeName_DocumentHasCheckSum, flag.ToString(), false, false, false);
    int result = 0;
    if (!int.TryParse(this.revision, out result))
      result = 0;
    ++result;
    this.Revision = result.ToString();
  }

  /// <summary>Сохранить документ в поток в формате XML</summary>
  /// <param name="stream">Поток</param>
  public void SaveToXml(Stream stream)
  {
    try
    {
      LogManager.AddLine("ImDocumentData.SaveToXml -START");
      LogManager.CloseFile();
      if (this.LoadFromStreamThread != null && (this.LoadFromStreamThread.ThreadState & System.Threading.ThreadState.Stopped) == System.Threading.ThreadState.Running)
        this.LoadFromStreamThread.Join();
      if (this.DistributeThread != null && (this.DistributeThread.ThreadState & System.Threading.ThreadState.Stopped) == System.Threading.ThreadState.Running)
        this.DistributeThread.Join();
      this.BeforeSaveDocument();
      XmlTextWriter xw = new XmlTextWriter(stream, Encoding.UTF8);
      try
      {
        xw.Formatting = Formatting.Indented;
        xw.Indentation = 3;
        xw.WriteStartDocument();
        ObjectIDGenerator objectRefId = new ObjectIDGenerator();
        this.WriteToXml("Document", (XmlWriter) xw, objectRefId);
        xw.WriteEndDocument();
      }
      finally
      {
        xw.Flush();
      }
      LogManager.AddLine("ImDocumentData.SaveToXml -END");
      LogManager.CloseFile();
    }
    catch (Exception ex)
    {
      LogManager.AddLine(ex);
      LogManager.CloseFile();
      throw;
    }
  }

  /// <summary>Событие перед сохранением документа</summary>
  public event EventHandler BeforeSave
  {
    add => this.beforeSave += value;
    remove => this.beforeSave -= value;
  }

  /// <summary>Герерирует событие BeforeSave</summary>
  protected virtual void OnBeforeSave(EventArgs e)
  {
    if (this.beforeSave == null)
      return;
    this.beforeSave((object) this, e);
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteAttributeString("fileVersion", DocumentTreeNode.FileVersion.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteAttributeString("productVersion", Application.ProductVersion.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.isTemplate)
      xw.WriteAttributeString("isTemplate", this.isTemplate.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    base.WriteXmlAttributes(xw, objectRefId);
    bool flag = this.Template != null;
    if ((flag || !this.shiftPage.IsEmpty) && (!flag || (this.overrideFlags & OverrideFlags.ShifPage) != OverrideFlags.None))
      xw.WriteAttributeString("shiftPage", new PointFConverter().ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) this.shiftPage));
    if (!flag || (this.overrideFlags & OverrideFlags.FitToPage) != OverrideFlags.None)
      xw.WriteAttributeString("fitToPage", this.fitToPage.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if ((flag || this.allowFormatingForReadOnlyText) && (!flag || (this.overrideFlags & OverrideFlags.AllowFormatingForReadOnly) != OverrideFlags.None))
      xw.WriteAttributeString("allowFormating", this.allowFormatingForReadOnlyText.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!flag || (this.overrideFlags3 & OverrideFlags3.SaveValueFromRefToDBAttr) != OverrideFlags3.None)
      xw.WriteAttributeString("saveDBAttrVal", this.saveValueFromRefToDBAttr ? "1" : "0");
    if ((flag || this.startPageNumber != 1) && (!flag || (this.overrideFlags & OverrideFlags.StartPageNumber) != OverrideFlags.None))
      xw.WriteAttributeString("startPageNumber", this.startPageNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.startComplectPageNumber != 1)
      xw.WriteAttributeString("startComplectPageNumber", this.startComplectPageNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.isPartOfComplectPageNumbering.HasValue)
      xw.WriteAttributeString("isPartOfComplectPageNumbering", this.isPartOfComplectPageNumbering.Value ? "1" : "0");
    if (this.isPartOfComplectPageCount.HasValue)
      xw.WriteAttributeString("isPartOfComplectPageCount", this.isPartOfComplectPageCount.Value ? "1" : "0");
    if (!this.isTemplate && this.GetType() == typeof (ImDocumentData))
      xw.WriteAttributeString("isDocData", "1");
    if (!this.isTemplate && this.needUpdateLayoutFlag)
      xw.WriteAttributeString("needUpdateLayout", "1");
    if (this.isFormulaLib)
      xw.WriteAttributeString("isFormulaLib", "1");
    if (!string.IsNullOrEmpty(this.designation))
      xw.WriteAttributeString("designation", this.designation);
    if (this.revision != null)
      xw.WriteAttributeString("revision", this.revision.ToString());
    if (!flag)
      xw.WriteAttributeString("defLRMargin", this.DefaultLeftRightMargin.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!flag)
      xw.WriteAttributeString("defTBMargin", this.DefaultTopBottomMargin.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.defaultNonSkipAtStartPage.HasValue)
      xw.WriteAttributeString("defNonSkipAtStPg", this.defaultNonSkipAtStartPage.Value ? "1" : "0");
    xw.WriteAttributeString("pageCount", this.PageCount.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (!flag || this.IsOverridden3(OverrideFlags3.DrawParentCellFrames))
      xw.WriteAttributeString("drawParentFrames", this.defaultDrawParentCellFrames ? "1" : "0");
    if (flag)
      return;
    xw.WriteAttributeString("rowTranc", this.fixedRowSizeTrancateFraction.ToString((IFormatProvider) CultureInfo.InvariantCulture));
  }

  /// <summary>Сохранить данные в элементы XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlElements(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (this.reference != null)
      this.reference.WriteToXml("Reference", xw, objectRefId);
    if (this.Template != null)
      this.Template.WriteToXml("Template", xw, objectRefId);
    if (this.formulaList != null)
      this.formulaList.WriteToXml("FormulaList", xw, objectRefId);
    this.DefaultParagraphFormat.WriteToXml("DefaultParFmt", xw, objectRefId);
    this.DefaultCharFormat.WriteToXml("DefaultFont", xw, objectRefId);
    if (this.defaultBorderLine != null)
      this.defaultBorderLine.WriteToXml("DefaultBorderLine", xw, objectRefId);
    if (this.defaultPageBorderLine != null)
      this.defaultPageBorderLine.WriteToXml("DefaultPageBorderLine", xw, objectRefId);
    if (this.documentFlows != null)
      WriteReadXmlHelper.WriteListToXml("DocumentFlows", (IList) this.documentFlows, "Flow", xw, objectRefId);
    if (this.materialKeyWords != null)
      WriteReadXmlHelper.WriteStringListToXml("MaterialKeyWords", this.materialKeyWords, "keyword", xw);
    base.WriteXmlElements(xw, objectRefId);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (ImDocumentData.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      ImDocumentData.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    if (readArgs.Reader.LocalName == "DocumentData")
    {
      ImDocumentData.ReadDocumentData((DocumentTreeNode) this, readArgs);
      return true;
    }
    return base.ReadFieldFromXml(readArgs);
  }

  private static void InitReadFieldDict()
  {
    ImDocumentData.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) VisualNode.ReadFieldsDict)
    {
      {
        "fileVersion",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadFileVersion)
      },
      {
        "productVersion",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadProductVersion)
      },
      {
        "isTemplate",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadIsTemplate)
      },
      {
        "DocumentFlows",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDocumentFlows)
      },
      {
        "MaterialKeyWords",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadMaterialKeyWords)
      },
      {
        "Template",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadTemplate)
      },
      {
        "FormulaList",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadFormulaList)
      },
      {
        "isDocData",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadIsDocData)
      },
      {
        "needUpdateLayout",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadNeedUpdateLayout)
      },
      {
        "isFormulaLib",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadIsFormulaLib)
      },
      {
        "shiftPage",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadShiftPage)
      },
      {
        "fitToPage",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadFitToPage)
      },
      {
        "designation",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDesignation)
      },
      {
        "revision",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadRevision)
      },
      {
        "Reference",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadReference)
      },
      {
        "allowFormating",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadAllowFormating)
      },
      {
        "saveDBAttrVal",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadSaveDBAttrVal)
      },
      {
        "startPageNumber",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadStartPageNumber)
      },
      {
        "DefaultParFmt",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDefaultParagraphFormat)
      },
      {
        "DefaultFont",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDefaultCharFormat)
      },
      {
        "startComplectPageNumber",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadStartComplectPageNumber)
      },
      {
        "drawParentFrames",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDrawParentFrames)
      },
      {
        "pageCount",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadPageCount)
      },
      {
        "defLRMargin",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDefLRMargin)
      },
      {
        "defTBMargin",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDefTBMargin)
      },
      {
        "defNonSkipAtStPg",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDefaultNonSkipAtStartPage)
      },
      {
        "isPartOfComplectPageNumbering",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadIsPartOfComplectPageNumbering)
      },
      {
        "isPartOfComplectPageCount",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadIsPartOfComplectPageCount)
      },
      {
        "DefaultBorderLine",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDefaultBorderLine)
      },
      {
        "DefaultPageBorderLine",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadDefaultPageBorderLine)
      },
      {
        "rowTranc",
        new ReadFieldFromXmlDelegate(ImDocumentData.ReadFixedRowSizeTrancateFraction)
      }
    };
    if (ImDocumentData.ReadFieldsDict.ContainsKey("Nodes"))
      ImDocumentData.ReadFieldsDict["Nodes"] = new ReadFieldFromXmlDelegate(ImDocumentData.ReadNodes);
    else
      ImDocumentData.ReadFieldsDict.Add("Nodes", new ReadFieldFromXmlDelegate(ImDocumentData.ReadNodes));
  }

  private static void ReadDefaultBorderLine(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    imDocumentData.defaultBorderLine = new BorderLine();
    imDocumentData.defaultBorderLine.ReadFromXml(readArgs);
  }

  private static void ReadDefaultPageBorderLine(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    imDocumentData.defaultPageBorderLine = new BorderLine();
    imDocumentData.defaultPageBorderLine.ReadFromXml(readArgs);
  }

  private static void ReadDefaultParagraphFormat(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    imDocumentData.defaultParagraphFormat = new ParagraphFormat();
    imDocumentData.defaultParagraphFormat.ReadFromXml(readArgs);
  }

  private static void ReadDefaultCharFormat(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    imDocumentData.defaultCharFormat = new CharFormat();
    imDocumentData.defaultCharFormat.ReadFromXml(readArgs);
  }

  private static void ReadStartPageNumber(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).startPageNumber = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    docNode.overrideFlags |= OverrideFlags.StartPageNumber;
  }

  private static void ReadStartComplectPageNumber(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).startComplectPageNumber = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadDrawParentFrames(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.SetOverrideFlags3(OverrideFlags3.DrawParentCellFrames);
    ((ImDocumentData) docNode).defaultDrawParentCellFrames = readArgs.Reader.Value != "0";
  }

  private static void ReadPageCount(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).savedPageCount = new int?(int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
  }

  private static void ReadDefLRMargin(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).DefaultLeftRightMargin = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadFixedRowSizeTrancateFraction(
    DocumentTreeNode docNode,
    XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).fixedRowSizeTrancateFraction = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadDefTBMargin(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).DefaultTopBottomMargin = float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadAllowFormating(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).allowFormatingForReadOnlyText = bool.Parse(readArgs.Reader.Value);
    docNode.overrideFlags |= OverrideFlags.AllowFormatingForReadOnly;
  }

  private static void ReadSaveDBAttrVal(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).saveValueFromRefToDBAttr = readArgs.Reader.Value == "1";
    docNode.overrideFlags3 |= OverrideFlags3.SaveValueFromRefToDBAttr;
  }

  private static void ReadReference(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    ReferenceBase referenceBase = ReferenceBase.LoadFromXml(readArgs);
    if (readArgs.DocumentDBReference != null)
    {
      imDocumentData.reference = readArgs.DocumentDBReference;
    }
    else
    {
      if (readArgs.Version < 26 && referenceBase is ReferenceToDBObjectBase referenceToDbObjectBase && referenceToDbObjectBase.ReferenceType == RefToDBObjectType.rtUseParentDocumentObjectLink)
        referenceToDbObjectBase.PassiveLink = false;
      imDocumentData.reference = referenceBase;
    }
    if (imDocumentData.reference == null)
      return;
    imDocumentData.reference.AssignOwnerNode((DocumentTreeNode) imDocumentData);
  }

  private static void ReadFitToPage(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).fitToPage = bool.Parse(readArgs.Reader.Value);
    docNode.overrideFlags |= OverrideFlags.FitToPage;
  }

  private static void ReadShiftPage(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 16 /*0x10*/)
      ((ImDocumentData) docNode).shiftPage = (PointF) new PointFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, DocumentTreeNode.ReplaceDS(readArgs.Reader.Value));
    else
      ((ImDocumentData) docNode).shiftPage = (PointF) new PointFConverter().ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, readArgs.Reader.Value);
    docNode.overrideFlags |= OverrideFlags.ShifPage;
  }

  protected static void ReadTemplate(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData template = ImDocumentData.CreateTemplate(docNode.GetType(), false);
    template.SuspendUpdateLayout();
    bool readInThread = readArgs.ReadInThread;
    readArgs.ReadInThread = false;
    bool internalTemplate = readArgs.IsInternalTemplate;
    readArgs.IsInternalTemplate = true;
    bool isTemplate = readArgs.IsTemplate;
    readArgs.IsTemplate = true;
    ReferenceBase documentDbReference = readArgs.DocumentDBReference;
    if (documentDbReference != null)
      readArgs.DocumentDBReference = documentDbReference.Clone();
    template._isSuspendedUpdatesFromDB = true;
    template.ReadFromXml(readArgs);
    readArgs.DocumentDBReference = documentDbReference;
    readArgs.ReadInThread = readInThread;
    ((ImDocumentData) docNode).AssignDocumentTemplate(template, false, false, false);
    readArgs.TemplateRoot = (DocumentTreeNode) template;
    template.RestoreObjectReferences(readArgs.ObjectsId, readArgs.ObjectReferences, true, true);
    template.OnDeserialization((object) null);
    template.UpdateNodeLinks(true, false, false, false);
    template.ResumeUpdateLayout(false, false);
    template.UpdatePageNumbers((PageData) null, template.startComplectPageNumber, false, false, false);
    docNode.ApplyTemplateProperties((DocumentTreeNode) template, false, false, true);
    template._isSuspendedUpdatesFromDB = false;
    readArgs.ReadInThread = readInThread;
    readArgs.IsInternalTemplate = internalTemplate;
    readArgs.IsTemplate = isTemplate;
  }

  private static void ReadFormulaList(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    if (imDocumentData.formulaList == null)
      imDocumentData.CreateFormulaList();
    imDocumentData.formulaList.SuspendUpdateLayout();
    bool readInThread = readArgs.ReadInThread;
    readArgs.ReadInThread = false;
    bool internalFormulaLib = readArgs.IsInternalFormulaLib;
    readArgs.IsInternalFormulaLib = true;
    bool isFormulaLib = readArgs.IsFormulaLib;
    readArgs.IsFormulaLib = true;
    imDocumentData.formulaList.ReadFromXml(readArgs);
    readArgs.ReadInThread = readInThread;
    readArgs.IsInternalFormulaLib = internalFormulaLib;
    readArgs.IsFormulaLib = isFormulaLib;
    imDocumentData.formulaList.RestoreObjectReferences(readArgs.ObjectsId, readArgs.ObjectReferences, true, true);
    imDocumentData.formulaList.OnDeserialization((object) null);
    imDocumentData.formulaList.ResumeUpdateLayout(false, false);
  }

  private static void ReadDocumentFlows(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    if (imDocumentData.documentFlows == null)
      imDocumentData.documentFlows = new List<FlowID>();
    else
      imDocumentData.documentFlows.Clear();
    WriteReadXmlHelper.ReadListFromXml((IList) imDocumentData.documentFlows, typeof (FlowID), readArgs);
  }

  private static void ReadMaterialKeyWords(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    if (imDocumentData.materialKeyWords == null)
      imDocumentData.materialKeyWords = new List<string>();
    else
      imDocumentData.materialKeyWords.Clear();
    WriteReadXmlHelper.ReadStringListFromXml(imDocumentData.materialKeyWords, readArgs);
  }

  private static void ReadDocumentData(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    readArgs.Reader.ReadOuterXml();
    readArgs.SkipRead = true;
  }

  private static void ReadIsTemplate(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).isTemplate = bool.Parse(readArgs.Reader.Value);
  }

  private static void ReadDefaultNonSkipAtStartPage(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).defaultNonSkipAtStartPage = new bool?(readArgs.Reader.Value != "0");
  }

  private static void ReadIsPartOfComplectPageNumbering(
    DocumentTreeNode docNode,
    XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).isPartOfComplectPageNumbering = new bool?(readArgs.Reader.Value != "0");
  }

  private static void ReadIsPartOfComplectPageCount(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).isPartOfComplectPageCount = new bool?(readArgs.Reader.Value != "0");
  }

  private static void ReadIsDocData(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    readArgs.IsDocData = readArgs.Reader.Value == "1";
    if (readArgs.RootArgs == null)
      return;
    readArgs.RootArgs.IsDocData = readArgs.IsDocData;
  }

  private static void ReadNeedUpdateLayout(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.AssignNeedUpdateLayoutFlag(readArgs.Reader.Value == "1");
  }

  private static void ReadIsFormulaLib(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).isFormulaLib = readArgs.Reader.Value == "1";
  }

  private static void ReadFileVersion(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    readArgs.Version = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    if (readArgs.Version < 37)
      imDocumentData.defaultDrawParentCellFrames = false;
    if (readArgs.Version >= 39)
      return;
    imDocumentData.fixedRowSizeTrancateFraction = 0.25f;
  }

  private static void ReadProductVersion(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).LoadedFileProductVersion = readArgs.Reader.Value;
  }

  public bool LoadedFileCreatedAfterBuilds(params string[] startBuilds)
  {
    if (string.IsNullOrEmpty(this.LoadedFileProductVersion))
      return false;
    string[] strArray1 = this.LoadedFileProductVersion.Split('.');
    string str = "0";
    foreach (string startBuild in startBuilds)
    {
      if (!string.IsNullOrEmpty(startBuild))
      {
        string[] strArray2 = startBuild.Split('.');
        if (strArray2.Length != 0)
        {
          if (string.Compare(str, strArray2[0]) < 0)
            str = strArray2[0];
          if (!(strArray2[0] == strArray1[0]))
            return string.Compare(strArray1[0], str) > 0;
          for (int index = 1; index < strArray2.Length; ++index)
          {
            int num = string.Compare(strArray1[index], strArray2[index]);
            if (num != 0)
              return num > 0;
          }
          return true;
        }
      }
    }
    return false;
  }

  private static void ReadDesignation(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ImDocumentData imDocumentData = (ImDocumentData) docNode;
    imDocumentData.designation = readArgs.Reader.Value;
    imDocumentData.LoadedFromXMLDesignation = imDocumentData.designation;
  }

  private static void ReadRevision(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((ImDocumentData) docNode).revision = readArgs.Reader.Value;
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public override void ReadFromXmlOldFormats_After(XmlReadArgs readArgs)
  {
    base.ReadFromXmlOldFormats_After(readArgs);
    if (readArgs.Version >= 17)
      return;
    this.ReadNodeFromXmlPostProcess(readArgs);
  }

  private new static void ReadNodes(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (!(docNode is ImDocumentData imDocumentData))
      return;
    imDocumentData.ReadNodes(readArgs);
  }

  private void ReadNodes(XmlReadArgs readArgs)
  {
    if (readArgs == null)
      throw new ArgumentNullException();
    if (this.nodes == null)
      this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
    string localName1 = readArgs.Reader.LocalName;
    bool flag = readArgs.Reader.IsEmptyElement;
    this.OnDeserialization((object) null);
    int num1 = 0;
    int num2 = this.startComplectPageNumber - 1;
    if (readArgs.Version > 12 && !readArgs.DataOnly)
    {
      while (!flag && readArgs.Reader.Read())
      {
        switch (readArgs.Reader.NodeType)
        {
          case XmlNodeType.Element:
            string localName2 = readArgs.Reader.LocalName;
            DocumentTreeNode nodeFromXmlTypeName = DocumentTreeNode.CreateNodeFromXmlTypeName(localName2);
            if (nodeFromXmlTypeName != null)
            {
              if (readArgs.ReadInThread)
                Monitor.Enter((object) nodeFromXmlTypeName);
              try
              {
                if (nodeFromXmlTypeName is PageData page)
                {
                  page.IsLockedForLoad = true;
                  page.SetPageNumber(this.StartPageNumber + num1++, false, false);
                  if (this.IsPartOfComplectPageNumbering)
                    page.SetComplectPageNumber(this.StartPageNumber + num2++, false, false);
                }
                nodeFromXmlTypeName.AssignNeedUpdateLayoutFlag(false);
                nodeFromXmlTypeName.suspendUpdateLayoutCount = this.suspendUpdateLayoutCount + 1;
                int num3 = this.modified ? 1 : 0;
                int index1 = this.nodes.AddInternal(nodeFromXmlTypeName);
                if (this.idService != null && nodeFromXmlTypeName.idService != this.idService)
                  nodeFromXmlTypeName.idService = this.idService;
                nodeFromXmlTypeName.AssignParent((DocumentTreeNode) this, false, false, true);
                nodeFromXmlTypeName.ReadFromXml(readArgs);
                nodeFromXmlTypeName.ApplyTemplateProperties(nodeFromXmlTypeName.Template, false, false, true);
                nodeFromXmlTypeName.RestoreObjectReferences(readArgs.ObjectsId, readArgs.ObjectReferences, true, true);
                if (page != null && page.Flows != null)
                {
                  for (int index2 = page.Flows.Count - 1; index2 > 0; --index2)
                  {
                    for (int index3 = 0; index3 < index2; ++index3)
                    {
                      if (page.Flows[index2] == page.Flows[index3])
                      {
                        page.Flows.RemoveAt(index2);
                        break;
                      }
                    }
                  }
                }
                nodeFromXmlTypeName.CallOnDeserializationRecursive();
                if (readArgs.Version < 25 && this.nodes.Count > 1)
                  this.nodes[index1 - 1].OnDeserialization((object) this);
                nodeFromXmlTypeName.UpdateTemplateLinks(false, true, false, false);
                nodeFromXmlTypeName.UpdateNodeLinks(true, false, false, false);
                if (readArgs.IsDocData && nodeFromXmlTypeName.Template != null)
                  nodeFromXmlTypeName.Template.ApplyTemplateTreeStructure(false, false, false, false);
                nodeFromXmlTypeName.ResumeUpdateLayout(false, false);
                if (readArgs.IsDocData)
                  nodeFromXmlTypeName.SetNeedUpdateLayoutFlag(true, false, false, false, true);
                this.OnChildNodeAdded(new ChildNode_EventArgs((DocumentTreeNode) this, nodeFromXmlTypeName, index1, false, false, false));
                if (page != null)
                {
                  if (this.NeedUI)
                    page.SetNeedUIRecursive(this.NeedUI, false);
                  page.IsLockedForLoad = false;
                  if (!readArgs.IsDocData)
                  {
                    if (readArgs.RootDocNodeIsLocked && readArgs.ReadInThread)
                    {
                      if (!readArgs.IsDocData)
                      {
                        page.UpdateNodeLinks(true, false, false, false);
                        this.OnPageUnlocked(new PageUnlockedArgs(page, false, readArgs));
                      }
                      if (!readArgs.IsDocData)
                        page.WaitForLayout(1000);
                      readArgs.RootDocNodeIsLocked = false;
                      Monitor.Pulse(readArgs.LockedObjectByLoadThread);
                      Monitor.Exit(readArgs.LockedObjectByLoadThread);
                      Thread.Sleep(10);
                      continue;
                    }
                    this.OnPageUnlocked(new PageUnlockedArgs(page, false, readArgs));
                    Thread.Sleep(0);
                    continue;
                  }
                  continue;
                }
                continue;
              }
              finally
              {
                if (readArgs.ReadInThread)
                  Monitor.Exit((object) nodeFromXmlTypeName);
              }
            }
            else
            {
              LogManager.AddLine(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_67"), (object) this.GetType().Namespace, (object) localName2));
              this.UnknownXmlElements += readArgs.Reader.ReadOuterXml();
              readArgs.SkipRead = true;
              continue;
            }
          case XmlNodeType.EndElement:
            if (localName1 == readArgs.Reader.LocalName)
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
    else
      DocumentTreeNode.ReadNodes((DocumentTreeNode) this, readArgs);
    if (readArgs.RootNodeIsComplect || this.LoadFromStreamThread == null)
      return;
    if (readArgs.RootDocNodeIsLocked)
    {
      readArgs.RootDocNodeIsLocked = false;
      Monitor.Pulse(readArgs.LockedObjectByLoadThread);
      Monitor.Exit(readArgs.LockedObjectByLoadThread);
    }
    this.LoadFromStreamThread = (Thread) null;
    if (this.DistributeThread != null)
      return;
    this.OnBackgroundThreadsFinished(new BackgroundThreadsFinishedArgs(DocumentBackgroundThreadType.LoadThread));
  }

  private void LoadDocumentDataFromXml(Stream stream, XmlReadArgs readArgs)
  {
    LogManager.AddLine("ImDocumentData.LoadDocumentDataFromXml(Stream stream) - START");
    LogManager.CloseFile();
    bool flag1 = false;
    XmlTextReader xmlTextReader = new XmlTextReader(stream);
    readArgs.Reader = (XmlReader) xmlTextReader;
    xmlTextReader.WhitespaceHandling = WhitespaceHandling.All;
    readArgs.DataOnly = true;
    try
    {
      bool flag2 = false;
      while (!flag2)
      {
        if (xmlTextReader.Read())
        {
          switch (xmlTextReader.NodeType)
          {
            case XmlNodeType.Element:
              if (xmlTextReader.LocalName == "Document")
              {
                flag1 = true;
                this.IsFileLoading = true;
                this.SuspendUpdateLayout();
                this.ReadFromXml(readArgs);
                continue;
              }
              continue;
            case XmlNodeType.EndElement:
              if ("Document" == xmlTextReader.LocalName)
              {
                flag2 = true;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
        else
          break;
      }
    }
    finally
    {
      xmlTextReader.Close();
    }
    if (!flag1)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_68"));
    this.RestoreObjectReferences(readArgs.ObjectsId, readArgs.ObjectReferences, true, false);
    this.OnDeserialization((object) null);
    this.ResumeUpdateLayout(false, false);
    this.IsFileLoading = false;
    this.Modified = false;
    LogManager.AddLine("ImDocumentData.LoadDocumentDataFromXml - END");
    LogManager.CloseFile();
  }

  /// <summary>Загрузить данные документа из потока.
  /// После загрузки поток закрывается в xmlReader.Close()!</summary>
  /// <param name="stream">Поток данных документа</param>
  /// <returns>Документ</returns>
  public static ImDocumentData LoadFromXml(Stream stream)
  {
    return ImDocumentData.LoadFromXml(stream, new XmlReadArgs());
  }

  /// <summary>Загрузить данные документа из потока.
  /// После загрузки поток закрывается в xmlReader.Close()!</summary>
  /// <param name="stream">Поток данных документа</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Документ</returns>
  public static ImDocumentData LoadFromXml(Stream stream, XmlReadArgs readArg)
  {
    ImDocumentData document = ImDocumentData.CreateDocument(false, false);
    document.LoadDocumentDataFromXml(stream, readArg);
    return document;
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public override void ReadFromXml(XmlReadArgs readArgs)
  {
    if (LogManager.CreateLog && !this.IsFormulaLib)
      LogManager.AddLine($"ImDocumentData.ReadFromXml(XmlReadArgs) [IsTemplate:{this.IsTemplate}, IsFormulaLib:{this.IsFormulaLib}, ReadInThread: {readArgs?.ReadInThread}] - START");
    bool isFileLoading = this.IsFileLoading;
    this.IsFileLoading = true;
    try
    {
      string attributeValue1 = this.GetAttributeValue(DocumentTreeNode.AttributeName_DocPageCount, false);
      string attributeValue2 = this.GetAttributeValue(DocumentTreeNode.AttributeName_LastDocPageNumber, false);
      XmlReadArgs readArgs1 = readArgs.Clone();
      readArgs1.RootArgs = readArgs.RootArgs == null ? readArgs : readArgs.RootArgs;
      if (!readArgs.IsInternalTemplate && !readArgs.IsInternalFormulaLib)
      {
        readArgs1.TemplateRoot = (DocumentTreeNode) null;
        readArgs1.IsDocData = false;
        readArgs1.DocumentDBReference = readArgs.DocumentDBReference;
      }
      this.isTemplate |= readArgs1.IsTemplate;
      this.isFormulaLib |= readArgs1.IsFormulaLib;
      this.DefaultCharFormat = (CharFormat) null;
      this.DefaultParagraphFormat = (ParagraphFormat) null;
      if (readArgs.Version < 43)
        this.saveValueFromRefToDBAttr = true;
      base.ReadFromXml(readArgs1);
      this.savedPageCount = new int?();
      this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_DocPageCount, (object) attributeValue1, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_DocPageCount, false), true, false));
      this.OnAttributeValueChanged(new AttributeValueChanged_EventArgs(DocumentTreeNode.AttributeName_LastDocPageNumber, (object) attributeValue2, (object) this.GetAttributeValue(DocumentTreeNode.AttributeName_LastDocPageNumber, false), true, false));
      if (!readArgs.IsInternalTemplate)
      {
        if (!readArgs.IsInternalFormulaLib)
        {
          readArgs.RootDocNodeIsLocked = readArgs1.RootDocNodeIsLocked;
          readArgs.SkipRead = readArgs1.SkipRead;
          readArgs.IsDocData = readArgs1.IsDocData;
        }
      }
    }
    finally
    {
      this.IsFileLoading = isFileLoading;
    }
    if (!LogManager.CreateLog || this.IsFormulaLib)
      return;
    LogManager.AddLine($"ImDocumentData.ReadFromXml(XmlReadArgs) [IsTemplate:{this.IsTemplate}, IsFormulaLib:{this.IsFormulaLib}, ReadInThread: {readArgs?.ReadInThread}] - END");
  }

  protected override void ReadAdditionalAttributesFromXml(XmlReadArgs readArgs)
  {
    base.ReadAdditionalAttributesFromXml(readArgs);
    if (string.IsNullOrEmpty(readArgs.FileName))
      return;
    this.SetAttributeValue(DocumentTreeNode.AttributeName_FileName, readArgs.FileName, false, false, false);
  }

  /// <summary>Идёт загрузка документа из файла</summary>
  [Browsable(false)]
  public bool IsLoading
  {
    [DebuggerStepThrough] get => this.IsFileLoading || this.IsDocumentLoading;
    set => this.IsDocumentLoading = value;
  }

  /// <summary>Есть активные фоновые потоки</summary>
  [Category("Debug")]
  public bool BackThreadIsActive
  {
    [DebuggerStepThrough] get => this.LoadThreadIsActive || this.DistributeThreadIsActive;
  }

  /// <summary>Есть активный фоновый поток загрузки</summary>
  [Category("Debug")]
  public bool LoadThreadIsActive
  {
    [DebuggerStepThrough] get
    {
      return this.LoadFromStreamThread != null && (this.LoadFromStreamThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running;
    }
  }

  /// <summary>Есть активный фоновый поток разбивки</summary>
  [Category("Debug")]
  public bool DistributeThreadIsActive
  {
    [DebuggerStepThrough] get
    {
      return this.DistributeThread != null && (this.DistributeThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running;
    }
  }

  /// <summary>Имя типа сохраняемое в XML</summary>
  public override string TypeNameForXml => "Document";

  /// <summary>Ссылка на источник данных</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_160")]
  [CustomDescription("Attribute.Interfaces.Document_161")]
  [CustomCategory("Attribute.Interfaces.Document_162")]
  [Browsable(false)]
  public virtual ReferenceBase Reference
  {
    [DebuggerStepThrough] get => this.reference;
    set
    {
      if (this.reference == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (Reference), (object) this.Reference, (object) value);
      if (this.reference != null)
        this.reference.DisconnectLink();
      this.reference = value;
      if (this.reference != null)
        this.reference.AssignOwnerNode((DocumentTreeNode) this);
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Создать список формул. Только для внутреннего пользования</summary>
  protected virtual void CreateFormulaList()
  {
    this.formulaList = new ImDocumentData(false, false, true);
  }

  protected virtual void MergeFormulaLists(ImDocumentData sourceFormulaList)
  {
    if (this.formulaList == sourceFormulaList || sourceFormulaList == null)
      return;
    for (int index1 = 0; index1 < sourceFormulaList.nodes.Count; ++index1)
    {
      if (sourceFormulaList.nodes[index1] is PageData node1 && this.FormulaList != null)
      {
        int index2;
        if (this.formulaList.FindNode(node1.Id) is PageData node)
        {
          index2 = node.Index;
          this.formulaList.RemoveChildNodeAt(index2, false, false, false);
        }
        else
          index2 = this.formulaList.nodes.Count;
        PageData child = (PageData) node1.Clone();
        this.formulaList.InsertChildNode(index2, (DocumentTreeNode) child, false, true, false, false, false);
      }
    }
  }

  /// <summary>Формулы использованные в документе</summary>
  [Browsable(false)]
  public virtual ImDocumentData FormulaList
  {
    [DebuggerStepThrough] get
    {
      if (this.formulaList == null)
        this.CreateFormulaList();
      return this.formulaList;
    }
  }

  /// <summary>Список ключевых слов для отображения материалов в виде дроби</summary>
  [Browsable(false)]
  public List<string> MaterialKeyWords
  {
    [DebuggerStepThrough] get => this.materialKeyWords;
  }

  /// <summary>Список ключевых слов для отображения материалов в виде дроби</summary>
  [Browsable(false)]
  public static List<string> ComplexDesignationSuffixs
  {
    [DebuggerStepThrough] get => ImDocumentData.complexDesignationSuffixs;
    set => ImDocumentData.complexDesignationSuffixs = value;
  }

  /// <summary>Версия документа, наматывается при сохранении</summary>
  [Browsable(false)]
  public string Revision
  {
    get => this.revision;
    set => this.revision = value;
  }

  /// <summary>Установить новое значение MaterialKeyWords</summary>
  /// <param name="value">Новое значение MaterialKeyWords</param>
  public virtual void SetMaterialKeyWords(List<string> value) => this.materialKeyWords = value;

  public IEnumerator<PageData> GetEnumerator()
  {
    return (IEnumerator<PageData>) new PageEnumerator((DocumentTreeNode) this);
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  /// <summary>Кэш в котором хранятся подписи документа</summary>
  [Browsable(false)]
  public object DBAttributeProcessorDictionary
  {
    get
    {
      ImDocumentData imDocumentData = this;
      if (this.IsTemplate && this.TemplateOwner != null)
        imDocumentData = this.TemplateOwner;
      DocumentsComplect documentsComplect = imDocumentData.GetRootDocumentsComplect();
      if (documentsComplect != null)
        imDocumentData = DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect);
      return imDocumentData != null ? imDocumentData.dBAttributeProcessorDictionary : this.dBAttributeProcessorDictionary;
    }
    set => this.dBAttributeProcessorDictionary = value;
  }

  /// <summary>Кэш в котором хранятся подписи документа</summary>
  [Browsable(false)]
  public Dictionary<long, ArrayList> Signes
  {
    get
    {
      ImDocumentData imDocumentData = this;
      if (this.IsTemplate && this.TemplateOwner != null)
        imDocumentData = this.TemplateOwner;
      DocumentsComplect documentsComplect = imDocumentData.GetRootDocumentsComplect();
      if (documentsComplect != null)
        imDocumentData = DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect);
      return imDocumentData != null ? imDocumentData.signes : this.signes;
    }
  }

  /// <summary>Кэш значений атрибутов объектов системы</summary>
  [Browsable(false)]
  public Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> ObjAttrCache
  {
    get
    {
      ImDocumentData imDocumentData = this;
      if (this.IsTemplate && this.TemplateOwner != null)
        imDocumentData = this.TemplateOwner;
      DocumentsComplect documentsComplect = imDocumentData.GetRootDocumentsComplect();
      if (documentsComplect != null)
        imDocumentData = DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect);
      return imDocumentData != null ? imDocumentData.objAttrCache : this.objAttrCache;
    }
  }

  /// <summary>Кэш значений атрибутов связей системы</summary>
  [Browsable(false)]
  public Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> RelAttrCache
  {
    get
    {
      ImDocumentData imDocumentData = this;
      if (this.IsTemplate && this.TemplateOwner != null)
        imDocumentData = this.TemplateOwner;
      DocumentsComplect documentsComplect = imDocumentData.GetRootDocumentsComplect();
      if (documentsComplect != null)
        imDocumentData = DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect);
      return imDocumentData != null ? imDocumentData.relAttrCache : this.relAttrCache;
    }
  }

  /// <summary>Кэш информаций об объектах для ссылок по гуиду версии</summary>
  [Browsable(false)]
  public Dictionary<Guid, Intermech.Interfaces.Document.DBObjectInfo> ObjectsInfoGuid
  {
    get
    {
      ImDocumentData imDocumentData = this;
      if (this.IsTemplate && this.TemplateOwner != null)
        imDocumentData = this.TemplateOwner;
      DocumentsComplect documentsComplect = imDocumentData.GetRootDocumentsComplect();
      if (documentsComplect != null)
        imDocumentData = DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect);
      return imDocumentData != null ? imDocumentData.objectsInfoGuid : this.objectsInfoGuid;
    }
  }

  /// <summary>Кэш информаций об объектах для ссылок по идентификатору версии</summary>
  [Browsable(false)]
  public Dictionary<long, Intermech.Interfaces.Document.DBObjectInfo> ObjectsInfoId
  {
    get
    {
      ImDocumentData imDocumentData = this;
      if (this.IsTemplate && this.TemplateOwner != null)
        imDocumentData = this.TemplateOwner;
      DocumentsComplect documentsComplect = imDocumentData.GetRootDocumentsComplect();
      if (documentsComplect != null)
        imDocumentData = DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect);
      return imDocumentData != null ? imDocumentData.objectsInfoId : this.objectsInfoId;
    }
  }

  /// <summary>Способ обновления ссылок</summary>
  [Browsable(false)]
  public UpdateReferencesMode UpdateReferencesMode
  {
    get => this.updateReferencesMode;
    set => this.updateReferencesMode = value;
  }
}
