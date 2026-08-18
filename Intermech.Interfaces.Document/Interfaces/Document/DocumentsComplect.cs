// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocumentsComplect
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
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

[Serializable]
public class DocumentsComplect : 
  DocumentSection,
  IEnumerable<ImDocumentData>,
  IEnumerable,
  ISerializable
{
  /// <summary>Имя типа для словаря конструкторов</summary>
  internal static string TypeNameForConstructorDictionary = nameof (DocumentsComplect);
  /// <summary>Имя типа элемента</summary>
  public new static string ElementTypeName = LocalizationHolder.rm.GetString("Interfaces.Document_159");
  [NonSerialized]
  private ModifiedChanged_EventHandler modifiedChanged;
  private PrintDocument printDocument;
  private ImPrintSettings imPrintSettings = new ImPrintSettings();
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict;
  [NonSerialized]
  private BackgroundThreadsFinished_EventHandler backgroundLoadFinished;
  [NonSerialized]
  private bool modified;
  /// <summary>Идёт загрузка документа из файла</summary>
  [NonSerialized]
  public bool IsFileLoading;
  /// <summary>Идёт загрузка данных документа</summary>
  [NonSerialized]
  public bool IsDocumentLoading;
  /// <summary>Фоновый процесс загрузки комплекта</summary>
  [NonSerialized]
  public Thread LoadFromStreamThread;
  /// <summary>Версия приложения сохранившего документ. Только начиная с документов версии 40</summary>
  private string LoadedFileProductVersion;

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new DocumentsComplect(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public new static object EmptyConstructor() => (object) new DocumentsComplect(false);

  /// <summary>Инициализировать поля объекта</summary>
  protected override void InitFields()
  {
    base.InitFields();
    this.IdService = (IUniqueIdService) new UniqueIdGenerator();
    this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
    this.cloneByTemplateWithParent = false;
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызвать метод InitFields()</param>
  public DocumentsComplect(bool initFields)
    : base(initFields)
  {
  }

  protected DocumentsComplect(SerializationInfo info, StreamingContext context)
  {
    Stream stream = (Stream) new MemoryStream((byte[]) info.GetValue("Stream", typeof (byte[])));
    stream.Position = 0L;
    this.LoadComplectFromXml(stream, false, false);
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
  public DocumentsComplect(DocumentTreeNode parent) => this.SetParent(parent, false, false);

  /// <summary>Конструктор</summary>
  public DocumentsComplect()
  {
  }

  /// <summary>Конструктор</summary>
  static DocumentsComplect() => DocumentsComplect.InitReadFieldDict();

  /// <summary>Наименование типа</summary>
  [ReadOnly(true)]
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => DocumentsComplect.ElementTypeName;
    set => DocumentsComplect.ElementTypeName = value;
  }

  /// <summary>Получить подпись элемента по умолчанию</summary>
  public override string GetDefautCaption()
  {
    string defautCaption = this.Name;
    if (defautCaption == null || defautCaption == "")
      defautCaption = string.Format(this.NodeTypeCaption + " {0}", (object) (this.Index + 1));
    return defautCaption;
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
    return documentsComplect ?? this;
  }

  /// <summary>Получить документ идущий перед текущим</summary>
  /// <param name="currDocumentParent">Владелец текущего документа</param>
  /// <param name="currDocumentIndex">Индекс текущего документа в коллекции владельца</param>
  /// <param name="inOwnerComplectOnly">Только в пределах комплекта документов</param>
  public static ImDocumentData GetPrevDocument(
    DocumentTreeNode currDocumentParent,
    int currDocumentIndex,
    bool inOwnerComplectOnly)
  {
    prevDocument = (ImDocumentData) null;
    if (currDocumentIndex > 0)
    {
      if (currDocumentIndex - 1 > currDocumentParent.Nodes.Count)
        currDocumentIndex = currDocumentParent.Nodes.Count;
      for (int index = currDocumentIndex - 1; index >= 0; --index)
      {
        if (currDocumentParent.Nodes[index] is ImDocumentData prevDocument)
          return prevDocument;
        if (!(currDocumentParent.Nodes[index] is PageData) && !(currDocumentParent.Nodes[index] is PageElementNode))
        {
          prevDocument = DocumentsComplect.GetLastDocument(currDocumentParent.Nodes[index]);
          if (prevDocument != null)
            return prevDocument;
        }
      }
    }
    if (currDocumentParent.Parent != null && (!inOwnerComplectOnly || !(currDocumentParent is DocumentsComplect)))
      prevDocument = DocumentsComplect.GetPrevDocument(currDocumentParent.Parent, currDocumentParent.Index, inOwnerComplectOnly);
    return prevDocument;
  }

  /// <summary>Получить страницу идущую после текущей в документе</summary>
  /// <param name="currDocumentParent">Владелец текущего документа</param>
  /// <param name="currDocumentIndex">Индекс текущего документа в коллекции владельца</param>
  /// <param name="inOwnerComplectOnly">Только в пределах комплекта документов</param>
  public static ImDocumentData GetNextDocument(
    DocumentTreeNode currDocumentParent,
    int currDocumentIndex,
    bool inOwnerComplectOnly)
  {
    nextDocument = (ImDocumentData) null;
    if (currDocumentIndex < currDocumentParent.Nodes.Count - 1)
    {
      if (currDocumentIndex < 0)
        currDocumentIndex = -1;
      for (int index = currDocumentIndex + 1; index < currDocumentParent.Nodes.Count; ++index)
      {
        if (currDocumentParent.Nodes[index] is ImDocumentData nextDocument)
          return nextDocument;
        if (!(currDocumentParent.Nodes[index] is PageData) && !(currDocumentParent.Nodes[index] is PageElementNode))
        {
          nextDocument = DocumentsComplect.GetFirstDocument(currDocumentParent.Nodes[index]);
          if (nextDocument != null)
            return nextDocument;
        }
      }
    }
    if (currDocumentParent.Parent != null && (!inOwnerComplectOnly || !(currDocumentParent is DocumentsComplect)))
      nextDocument = DocumentsComplect.GetNextDocument(currDocumentParent.Parent, currDocumentParent.Index, inOwnerComplectOnly);
    return nextDocument;
  }

  /// <summary>Получить все документы входящие в комплект</summary>
  /// <returns></returns>
  public List<ImDocumentData> GetAllDocuments()
  {
    List<ImDocumentData> allDocuments = new List<ImDocumentData>();
    if (this.Nodes != null)
    {
      foreach (DocumentTreeNode node in this.Nodes)
      {
        if (node is ImDocumentData)
          allDocuments.Add(node as ImDocumentData);
        if (node is DocumentsComplect)
          allDocuments.AddRange((IEnumerable<ImDocumentData>) (node as DocumentsComplect).GetAllDocuments());
      }
    }
    return allDocuments;
  }

  /// <summary>Получить первый документ у заданного владельца</summary>
  /// <param name="documentParent">Владелец документа</param>
  public static ImDocumentData GetFirstDocument(DocumentTreeNode documentParent)
  {
    if (documentParent != null && documentParent.Nodes != null)
    {
      for (int index = 0; index < documentParent.Nodes.Count; ++index)
      {
        if (documentParent.Nodes[index] is ImDocumentData node)
          return node;
        if (!(documentParent.Nodes[index] is PageData) && !(documentParent.Nodes[index] is PageElementNode))
        {
          ImDocumentData firstDocument = DocumentsComplect.GetFirstDocument(documentParent.Nodes[index]);
          if (firstDocument != null)
            return firstDocument;
        }
      }
    }
    return (ImDocumentData) null;
  }

  /// <summary>Получить последний документ у заданного владельца</summary>
  /// <param name="documentParent">Владелец документа</param>
  public static ImDocumentData GetLastDocument(DocumentTreeNode documentParent)
  {
    if (documentParent != null && documentParent.Nodes != null)
    {
      for (int index = documentParent.Nodes.Count - 1; index >= 0; --index)
      {
        if (documentParent.Nodes[index] is ImDocumentData node)
          return node;
        if (!(documentParent.Nodes[index] is PageData) && !(documentParent.Nodes[index] is PageElementNode))
        {
          ImDocumentData lastDocument = DocumentsComplect.GetLastDocument(documentParent.Nodes[index]);
          if (lastDocument != null)
            return lastDocument;
        }
      }
    }
    return (ImDocumentData) null;
  }

  /// <summary>Получить энумератор для проматывания страниц</summary>
  /// <returns></returns>
  public IEnumerator<PageData> GetPageEnumerator()
  {
    return (IEnumerator<PageData>) new PageEnumerator((DocumentTreeNode) this);
  }

  /// <summary>Получить энумератор для проматывания документов</summary>
  public IEnumerator<ImDocumentData> GetEnumerator()
  {
    return (IEnumerator<ImDocumentData>) new ImDocumentEnumerator((DocumentTreeNode) this);
  }

  /// <summary>Получить энумератор для проматывания документов</summary>
  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new ImDocumentEnumerator((DocumentTreeNode) this);
  }

  /// <summary>Документ изменен после сохранения или открытия</summary>
  [Browsable(false)]
  [ReadOnly(true)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool Modified
  {
    [DebuggerStepThrough] get => this.modified;
    set
    {
      if (this.IsLoading && value || this.modified == value)
        return;
      this.modified = value;
      this.OnModifiedChanged(new ModifiedChanged_EventArgs());
      if (this.modified)
      {
        if (!(this.parent is DocumentsComplect parent))
          return;
        parent.Modified = this.modified;
      }
      else
        this.SetModifiedRecursive(false);
    }
  }

  /// <summary>Назначить вниз по всему дереву значение флага Modified</summary>
  /// <param name="value">Значение</param>
  public void SetModifiedRecursive(bool value)
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is DocumentsComplect node2)
        node2.SetModifiedRecursive(value);
      else if (this.nodes[index] is ImDocumentData node1)
        node1.Modified = value;
    }
  }

  /// <summary>Назначить вниз по всему дереву значение флага IsDocumentLoading</summary>
  /// <param name="value">Значение</param>
  public void SetIsDocumentLoadingRecursive(bool value)
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is DocumentsComplect node2)
        node2.SetIsDocumentLoadingRecursive(value);
      else if (this.nodes[index] is ImDocumentData node1)
        node1.IsDocumentLoading = value;
    }
    this.IsDocumentLoading = value;
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

  /// <summary>Количество страниц в комплекте</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_536")]
  [CustomDescription("Attribute.Interfaces.Document_537")]
  [CustomCategory("Attribute.Interfaces.Document_150")]
  public int PageCount
  {
    get
    {
      int pageCount = 0;
      foreach (ImDocumentData allDocument in this.GetAllDocuments())
      {
        if (allDocument.IsPartOfComplectPageCount)
          pageCount += allDocument.PageCount;
      }
      return pageCount;
    }
  }

  /// <summary>Обновить номера страниц</summary>
  /// <param name="startIndex">Индекс страницы с которой начать обновление</param>
  public void UpdatePageNumbers(
    ImDocumentData startDocument,
    int startComplectPageNumber,
    bool updateUI,
    bool updateLayout)
  {
    ImDocumentEnumerator documentEnumerator = new ImDocumentEnumerator((DocumentTreeNode) this, startDocument);
    int startComplectPageNumber1 = 1;
    if (startDocument != null)
      startComplectPageNumber1 = startComplectPageNumber == -1 ? startDocument.StartComplectPageNumber : startComplectPageNumber;
    while (documentEnumerator.MoveNext())
      startComplectPageNumber1 = documentEnumerator.Current.UpdatePageNumbers((PageData) null, startComplectPageNumber1, false, updateUI, updateLayout);
  }

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

  /// <summary>Инициализировать объект для печати документа</summary>
  public void InitPrintDocument()
  {
    if (this.printDocument != null)
      return;
    this.printDocument = new PrintDocument();
    this.printDocument.BeginPrint += new PrintEventHandler(this.BeginPrint);
    this.printDocument.EndPrint += new PrintEventHandler(this.EndPrint);
    this.printDocument.QueryPageSettings += new QueryPageSettingsEventHandler(this.printDocument_QueryPageSettings);
    this.printDocument.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
  }

  /// <summary>Обработчик события "Начало печати документа"</summary>
  protected virtual void BeginPrint(object sender, PrintEventArgs e)
  {
    if (this.PageCount == 0)
    {
      e.Cancel = true;
    }
    else
    {
      foreach (ImDocumentData allDocument in this.GetAllDocuments())
      {
        allDocument?.BeginPrint(sender, e);
        e.Cancel = false;
      }
      this.ImPrintSettings.Reset();
      List<ImDocumentData> allDocuments = this.GetAllDocuments();
      PrintDocument printDoc = sender as PrintDocument;
      foreach (ImDocumentData imDocumentData in allDocuments)
      {
        imDocumentData.NowPrinting = true;
        foreach (DocumentTreeNode node in imDocumentData.Nodes)
        {
          PageData page = node as PageData;
          if (imDocumentData.NeedPrintPage(printDoc, page))
            this.ImPrintSettings.PagesForPrint.Add(page);
        }
      }
      if (ImDocumentData.NotifyService != null)
        ImDocumentData.NotifyService.FireBeforePrint((object) this, new BeforePrintDocumentEventArgs((DocumentTreeNode) this));
      this.UpdatePrintLinks(true, false, false, true);
    }
  }

  /// <summary>Метод вызывается перед открытием диалога печати</summary>
  public void BeforeShowPrintDialog()
  {
    foreach (ImDocumentData allDocument in this.GetAllDocuments())
    {
      if (allDocument != null)
      {
        if (allDocument.LoadFromStreamThread != null && (allDocument.LoadFromStreamThread.ThreadState & System.Threading.ThreadState.Stopped) == System.Threading.ThreadState.Running)
          allDocument.LoadFromStreamThread.Join();
        if (allDocument.DistributeThread != null && (allDocument.DistributeThread.ThreadState & System.Threading.ThreadState.Stopped) == System.Threading.ThreadState.Running)
          allDocument.DistributeThread.Join();
      }
    }
  }

  /// <summary>В документе имеются страницы для печати</summary>
  /// <param name="doc"></param>
  /// <returns></returns>
  private bool HasPagesForPrint(ImDocumentData doc)
  {
    foreach (PageData node in doc.Nodes)
    {
      if (doc.NeedPrintPage(this.PrintDocument, node))
        return true;
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
        PageData curPage = this.ImPrintSettings.PagesForPrint[this.ImPrintSettings.CurrentPrintPageIndex];
        bool flag2 = false;
        if (curPage.FromNewPage && e.PageSettings.PrinterSettings.Duplex != Duplex.Default && this.ImPrintSettings.PrintPageIndex % 2 == 0)
          flag2 = true;
        if (!flag2)
        {
          flag1 = curPage.OwnerDocument.PrintPage(sender as PrintDocument, e, curPage);
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
    this.ImPrintSettings.Reset();
    foreach (ImDocumentData allDocument in this.GetAllDocuments())
      allDocument.NowPrinting = false;
    if (ImDocumentData.NotifyService != null)
      ImDocumentData.NotifyService.FireAfterPrint((object) this, new AfterPrintDocumentEventArgs((DocumentTreeNode) this));
    this.UpdatePrintLinks(true, false, false, true);
  }

  /// <summary>Обработчик события перед печатью каждой страницы</summary>
  private void printDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
  {
    try
    {
      if (!this.ImPrintSettings.HasCurrentPage)
        return;
      PageData currentPrintPage = this.ImPrintSettings.CurrentPrintPage;
      PageSettings pageSettings = e.PageSettings;
      ref PageSettings local = ref pageSettings;
      currentPrintPage.SetPagePrintSettings(ref local);
      if (e.PageSettings != null)
        return;
      e.PageSettings = pageSettings;
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

  private static void InitReadFieldDict()
  {
    DocumentsComplect.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) VisualNode.ReadFieldsDict)
    {
      {
        "fileVersion",
        new ReadFieldFromXmlDelegate(DocumentsComplect.ReadFileVersion)
      },
      {
        "productVersion",
        new ReadFieldFromXmlDelegate(DocumentsComplect.ReadProductVersion)
      }
    };
  }

  private static void ReadFileVersion(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    readArgs.Version = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private static void ReadProductVersion(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    ((DocumentsComplect) docNode).LoadedFileProductVersion = readArgs.Reader.Value;
  }

  /// <summary>Сохранить комплект в поток в формате XML</summary>
  /// <param name="stream">Поток</param>
  public void SaveToXml(Stream stream)
  {
    XmlTextWriter xw = new XmlTextWriter(stream, Encoding.UTF8);
    try
    {
      xw.Formatting = Formatting.Indented;
      xw.Indentation = 3;
      xw.WriteStartDocument();
      ObjectIDGenerator objectRefId = new ObjectIDGenerator();
      this.WriteToXml(nameof (DocumentsComplect), (XmlWriter) xw, objectRefId);
      xw.WriteEndDocument();
    }
    finally
    {
      xw.Flush();
    }
  }

  /// <summary>Сохранить комплект в файл в формате XML.
  /// Если файл с этим именем уже существует, он будет переписан!</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="packFile">Сжимать файл</param>
  public void SaveToXml(string fileName, bool packFile)
  {
    string uniqueFileName = ImDocumentData.GenerateUniqueFileName(fileName + ".tmp");
    if (packFile)
    {
      using (ZipOutputStream zipOutputStream = new ZipOutputStream((Stream) File.Create(uniqueFileName)))
      {
        byte[] numArray = new byte[4096 /*0x1000*/];
        zipOutputStream.SetLevel(9);
        ZipEntry entry = new ZipEntry("DocumentsComplect.idcx");
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

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (DocumentsComplect.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      DocumentsComplect.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    return base.ReadFieldFromXml(readArgs);
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteAttributeString("fileVersion", DocumentTreeNode.FileVersion.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteAttributeString("productVersion", Application.ProductVersion.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    base.WriteXmlAttributes(xw, objectRefId);
  }

  public void LoadComplectFromXml(Stream stream, bool notCloseStream, bool loadInThread)
  {
    bool flag1 = false;
    XmlTextReader reader = new XmlTextReader(stream);
    reader.WhitespaceHandling = WhitespaceHandling.All;
    XmlReadArgs readArgs = new XmlReadArgs((XmlReader) reader);
    readArgs.RootNodeIsComplect = true;
    readArgs.RootDocNode = (object) null;
    if (readArgs.ReadInThread)
    {
      Monitor.Enter(readArgs.LockedObjectByLoadThread = (object) readArgs);
      readArgs.RootDocNodeIsLocked = true;
    }
    readArgs.DataOnly = false;
    try
    {
      bool flag2 = false;
      while (!flag2)
      {
        if (reader.Read())
        {
          switch (reader.NodeType)
          {
            case XmlNodeType.Element:
              if (reader.LocalName == nameof (DocumentsComplect))
              {
                flag1 = true;
                readArgs.RootDocNode = (object) this;
                this.SuspendUpdateLayout();
                this.ReadFromXml(readArgs);
                continue;
              }
              continue;
            case XmlNodeType.EndElement:
              if (nameof (DocumentsComplect) == reader.LocalName)
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
      if (readArgs.RootDocNodeIsLocked)
      {
        readArgs.RootDocNodeIsLocked = false;
        Monitor.Pulse(readArgs.LockedObjectByLoadThread);
        Monitor.Exit(readArgs.LockedObjectByLoadThread);
      }
      if (!notCloseStream)
        reader.Close();
    }
    if (!flag1)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_163"));
    this.RestoreObjectReferences(readArgs.ObjectsId, readArgs.ObjectReferences, true, false);
    this.OnDeserialization((object) null);
    this.ResumeUpdateLayout(false, false);
    this.Modified = false;
  }

  /// <summary>Загрузить данные документа из потока</summary>
  /// <param name="stream">Поток данных документа</param>
  /// <param name="notCloseStream">Не закрывать поток после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <returns>Документ</returns>
  public static DocumentsComplect LoadFromXml(
    Stream stream,
    bool notCloseStream,
    bool loadInThread)
  {
    DocumentsComplect documentsComplect = new DocumentsComplect(true);
    documentsComplect.LoadComplectFromXml(stream, notCloseStream, loadInThread);
    return documentsComplect;
  }

  /// <summary>Идёт загрузка документа из файла</summary>
  [Browsable(false)]
  public bool IsLoading
  {
    [DebuggerStepThrough] get => this.IsFileLoading || this.IsDocumentLoading;
  }

  /// <summary>Есть активные фоновые потоки</summary>
  [Category("Debug")]
  [Browsable(false)]
  public bool BackThreadIsActive
  {
    get
    {
      return this.LoadFromStreamThread != null && (this.LoadFromStreamThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running;
    }
  }

  [Browsable(false)]
  public ImPrintSettings ImPrintSettings
  {
    get => this.imPrintSettings;
    set => this.imPrintSettings = value;
  }

  /// <summary>Событие при завершении фоновой процесса загрузки</summary>
  public event BackgroundThreadsFinished_EventHandler BackgroundLoadFinished
  {
    add => this.backgroundLoadFinished += value;
    remove => this.backgroundLoadFinished -= value;
  }

  /// <summary>Генерирует событие BackgroundLoadFinished</summary>
  public virtual void OnBackgroundLoadFinished(BackgroundThreadsFinishedArgs e)
  {
    if (this.backgroundLoadFinished == null)
      return;
    this.backgroundLoadFinished((object) this, e);
  }
}
