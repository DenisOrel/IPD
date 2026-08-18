// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.XmlReadArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Threading;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы чтения из XML</summary>
public class XmlReadArgs : ICloneable, IDisposable
{
  /// <summary>Средство чтения, обеспечивающее доступ к данным XML</summary>
  public XmlReader Reader;
  /// <summary>Список идентификаторов загруженных объектов</summary>
  public IDictionary ObjectsId;
  /// <summary>Список ссылок на загруженные объекты</summary>
  public IDictionary ObjectReferences;
  /// <summary>Аргументы чтения для корня дерева</summary>
  public XmlReadArgs RootArgs;
  /// <summary>Объект используемый для блокировки потока и ожидания загрузки первой страницы документа</summary>
  public object LockedObjectByLoadThread;
  /// <summary>Версия загружаемого файла</summary>
  public int Version;
  /// <summary>Имя загружаемого файла</summary>
  public string FileName;
  /// <summary>Размер загружаемого файла (реальный размер без упаковки)</summary>
  public long FileSize;
  /// <summary>Дата модификации файла</summary>
  public DateTime? FileModifyDate;
  /// <summary>Специальный режим загрузки документа. Грузятся только данные
  /// (все что объявлено в Intermech.Interfaces.Document)</summary>
  public bool DataOnly;
  /// <summary>Пропустить один вызов Read, в цикле чтения XML</summary>
  public bool SkipRead;
  /// <summary>Корень дерева шаблона документа</summary>
  public DocumentTreeNode TemplateRoot;
  /// <summary>На данный момент загружается внутренний шаблон документа</summary>
  public bool IsInternalTemplate;
  /// <summary>На данный момент загружается внутренний список формул</summary>
  public bool IsInternalFormulaLib;
  /// <summary>Загружаемый документ является шаблоном</summary>
  public bool IsTemplate;
  /// <summary>Загружаемый документ является библиотекой формул</summary>
  public bool IsFormulaLib;
  /// <summary>Этот документ был сгенерирован только с данными (ImDocumentData)</summary>
  public bool IsDocData;
  /// <summary>Загружать документ в фоновом режиме</summary>
  public bool ReadInThread;
  /// <summary>Документ загружается во внешнем фоновом потоке</summary>
  public bool ThreadIsExternal;
  /// <summary>Фоновый процесс загрузки документа</summary>
  public Thread LoadFromStreamThread;
  /// <summary>Корневой узел загружаемого дерева документов</summary>
  public object RootDocNode;
  /// <summary>Документ заблокирован фоновым потоком</summary>
  public bool RootDocNodeIsLocked;
  /// <summary>Корень загружаемого дерева является комплектом документов</summary>
  public bool RootNodeIsComplect;
  /// <summary>Индекс ячейки в гриде</summary>
  public int GridCellIndex;
  /// <summary> Пользовательская сессия </summary>
  public IUserSession IUserSession;
  /// <summary>Не закрывать поток после загрузки</summary>
  public bool NotCloseStream;
  public ReferenceBase DocumentDBReference;

  /// <summary>Конструктор</summary>
  /// <param name="reader">Средство чтения, обеспечивающее доступ к данным XML</param>
  public XmlReadArgs(XmlReader reader)
    : this()
  {
    this.Reader = reader;
  }

  /// <summary>Конструктор</summary>
  public XmlReadArgs()
  {
    this.ObjectsId = (IDictionary) new Hashtable();
    this.ObjectReferences = (IDictionary) new Hashtable();
  }

  /// <summary>Конструктор</summary>
  /// <param name="reader">Средство чтения, обеспечивающее доступ к данным XML</param>
  /// <param name="objectsId">Список идентификаторов загруженных объектов</param>
  /// <param name="objectReferences">Список ссылок на загруженные объекты</param>
  public XmlReadArgs(XmlReader reader, IDictionary objectsId, IDictionary objectReferences)
  {
    this.Reader = reader;
    this.ObjectsId = objectsId;
    this.ObjectReferences = objectReferences;
  }

  public XmlReadArgs Clone()
  {
    return new XmlReadArgs(this.Reader, this.ObjectsId, this.ObjectReferences)
    {
      Version = this.Version,
      DataOnly = this.DataOnly,
      SkipRead = this.SkipRead,
      TemplateRoot = this.TemplateRoot,
      IsInternalTemplate = this.IsInternalTemplate,
      IsTemplate = this.IsTemplate,
      IsInternalFormulaLib = this.IsInternalFormulaLib,
      IsFormulaLib = this.IsFormulaLib,
      IsDocData = this.IsDocData,
      ReadInThread = this.ReadInThread,
      LoadFromStreamThread = this.LoadFromStreamThread,
      RootDocNode = this.RootDocNode,
      RootDocNodeIsLocked = this.RootDocNodeIsLocked,
      LockedObjectByLoadThread = this.LockedObjectByLoadThread,
      RootNodeIsComplect = this.RootNodeIsComplect,
      GridCellIndex = this.GridCellIndex,
      IUserSession = this.IUserSession,
      NotCloseStream = this.NotCloseStream,
      FileName = this.FileName
    };
  }

  object ICloneable.Clone() => (object) this.Clone();

  public void Dispose()
  {
    this.Reader = (XmlReader) null;
    this.ObjectsId.Clear();
    this.ObjectsId = (IDictionary) null;
    this.ObjectReferences.Clear();
    this.ObjectReferences = (IDictionary) null;
  }
}
