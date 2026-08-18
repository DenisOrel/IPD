
// Type: Intermech.Interfaces.XmlReadArgsIPS
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Threading;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>Аргументы чтения из XML</summary>
    public class XmlReadArgsIPS : ICloneable
    {
      /// <summary>Средство чтения, обеспечивающее доступ к данным XML</summary>
      public XmlReader Reader;
      /// <summary>Список идентификаторов загруженных объектов</summary>
      public IDictionary ObjectsId;
      /// <summary>Список ссылок на загруженные объекты</summary>
      public IDictionary ObjectReferences;
      /// <summary>Версия загружаемого файла</summary>
      public int Version;
      /// <summary>Специальный режим загрузки документа. Грузятся только данные
      /// (все что объявлено в Intermech.Interfaces.Document)</summary>
      public bool DataOnly;
      /// <summary>Пропустить один вызов Read, в цикле чтения XML</summary>
      public bool SkipRead;
      /// <summary>На данный момент загружается внутренний шаблон документа</summary>
      public bool IsInternalTemplate;
      /// <summary>На данный момент загружается внутренний список формул</summary>
      public bool IsInternalFormulaList;
      /// <summary>Этот документ был сгенерирован только с данными (ImDocumentData)</summary>
      public bool IsDocData;
      /// <summary>Загружать документ в фоновом режиме</summary>
      public bool ReadInThread;
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

      /// <summary>Конструктор</summary>
      /// <param name="reader">Средство чтения, обеспечивающее доступ к данным XML</param>
      public XmlReadArgsIPS(XmlReader reader)
      {
        this.Reader = reader;
        this.ObjectsId = (IDictionary) new Hashtable();
        this.ObjectReferences = (IDictionary) new Hashtable();
      }

      /// <summary>Конструктор</summary>
      /// <param name="reader">Средство чтения, обеспечивающее доступ к данным XML</param>
      /// <param name="objectsId">Список идентификаторов загруженных объектов</param>
      /// <param name="objectReferences">Список ссылок на загруженные объекты</param>
      public XmlReadArgsIPS(XmlReader reader, IDictionary objectsId, IDictionary objectReferences)
      {
        this.Reader = reader;
        this.ObjectsId = objectsId;
        this.ObjectReferences = objectReferences;
      }

      public XmlReadArgsIPS Clone()
      {
        return new XmlReadArgsIPS(this.Reader, this.ObjectsId, this.ObjectReferences)
        {
          Version = this.Version,
          DataOnly = this.DataOnly,
          SkipRead = this.SkipRead,
          IsInternalTemplate = this.IsInternalTemplate,
          IsInternalFormulaList = this.IsInternalFormulaList,
          IsDocData = this.IsDocData,
          ReadInThread = this.ReadInThread,
          LoadFromStreamThread = this.LoadFromStreamThread,
          RootDocNode = this.RootDocNode,
          RootDocNodeIsLocked = this.RootDocNodeIsLocked,
          RootNodeIsComplect = this.RootNodeIsComplect,
          GridCellIndex = this.GridCellIndex,
          IUserSession = this.IUserSession,
          NotCloseStream = this.NotCloseStream
        };
      }

      object ICloneable.Clone() => (object) this.Clone();
    }
}
