
// Type: Intermech.Interfaces.CompositionsAutosortRule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, описывающий правило сортировки составов, позволяющее управлять видимостью типов связей
    /// </summary>
    [Serializable]
    public class CompositionsAutosortRule : 
      ICompositionsAutosortRule,
      IXMLStoredClass,
      ICloneable,
      IComparable,
      IComparable<CompositionsAutosortRule>
    {
      /// <summary>
      /// Идентификатор версии объекта "Конфигурации роли", в атрибуте которого
      /// хранится экземпляр указанного правила
      /// </summary>
      protected long _objectID;
      /// <summary>
      /// Уникальный идентификатор правила сортировки составов
      /// (совпадает с Guid версии объекта "Настройки роли")
      /// </summary>
      protected Guid _guid;
      /// <summary>
      /// Заголовок объекта "Конфигурации роли", в атрибуте которого
      /// хранится экземпляр указанного правила
      /// </summary>
      protected string _name;
      /// <summary>
      /// Использовать события для фильтрации списков типов связей
      /// </summary>
      protected bool _useEvents;
      /// <summary>
      /// Список родительских типов объектов, составы которых будут сортироваться
      /// </summary>
      protected List<ParentObjectType> _parentObjectTypes;

      /// <summary>Создать экземпляр класса</summary>
      public CompositionsAutosortRule()
      {
        this._objectID = 0L;
        this._guid = Guid.NewGuid();
        this._name = string.Empty;
        this._parentObjectTypes = new List<ParentObjectType>();
      }

      /// <summary>
      /// Создать экземпляр класса, скопировав в него данные из коллекции родительских типов объектов
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта "Настройки роли", в атрибуте которого
      /// хранится экземпляр указанного правила</param>
      public CompositionsAutosortRule(long objectID)
        : this()
      {
        this._objectID = objectID;
      }

      /// <summary>
      /// Создать экземпляр класса, скопировав в него данные из коллекции родительских типов объектов
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта "Настройки роли", в атрибуте которого
      /// которого хранится экземпляр указанного правила</param>
      /// <param name="guid">Уникальный идентификатор правила сортировки составов
      /// (совпадает с Guid версии объекта "Настройки роли")</param>
      /// <param name="name">Заголовок объекта "Конфигурации роли", в атрибуте
      /// которого хранится экземпляр указанного правила</param>
      /// <param name="parentObjectTypes">Список родительских типов объектов, составы которых будут сортироваться</param>
      public CompositionsAutosortRule(
        long objectID,
        Guid guid,
        string name,
        List<ParentObjectType> parentObjectTypes)
      {
        this._objectID = objectID;
        this._guid = guid;
        this._name = name;
        this._parentObjectTypes = new List<ParentObjectType>();
        if (parentObjectTypes == null)
          return;
        for (int index = 0; index < parentObjectTypes.Count; ++index)
          this._parentObjectTypes.Add(parentObjectTypes[index].Clone() as ParentObjectType);
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is CompositionsAutosortRule compositionsAutosortRule) ? base.Equals(obj) : this.Guid.Equals(compositionsAutosortRule.Guid);
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.Guid.GetHashCode();

      /// <summary>
      /// Идентификатор версии объекта "Настройки роли", в атрибуте которого
      /// хранится экземпляр указанного правила
      /// </summary>
      public virtual long ObjectID
      {
        [DebuggerStepThrough] get => this._objectID;
        set => this._objectID = value;
      }

      /// <summary>
      /// Уникальный идентификатор правила сортировки составов
      /// (совпадает с Guid версии объекта "Настройки роли")
      /// </summary>
      public virtual Guid Guid
      {
        [DebuggerStepThrough] get => this._guid;
        set => this._guid = value;
      }

      /// <summary>
      /// Заголовок объекта "Конфигурации роли", в атрибуте
      /// которого хранится экземпляр указанного правила
      /// </summary>
      public virtual string Name
      {
        [DebuggerStepThrough] get => this._name;
        set => this._name = value;
      }

      /// <summary>
      /// Использовать события для фильтрации списков типов связей
      /// </summary>
      public virtual bool UseEvents
      {
        [DebuggerStepThrough] get => this._useEvents;
        set => this._useEvents = value;
      }

      /// <summary>
      /// Список родительских типов объектов, составы которых будут сортироваться
      /// </summary>
      public virtual List<ParentObjectType> ParentObjectTypes
      {
        get
        {
          if (this._parentObjectTypes == null)
            this._parentObjectTypes = new List<ParentObjectType>();
          return this._parentObjectTypes;
        }
      }

      /// <summary>
      /// Найти номер родительского типа объектов с указанным идентификатором
      /// </summary>
      /// <param name="parObjTypeId">Идентификатор искомого родительского типа объектов</param>
      /// <param name="withInherited">true - искать и родительские типы для данного типа объекта</param>
      /// <returns>Номер родительского типа объектов с указанным идентификатором или -1</returns>
      public virtual int IndexOfParentObjectType(int parObjTypeId, bool withInherited)
      {
        List<ParentObjectType> parentObjectTypes = this.ParentObjectTypes;
        int count = parentObjectTypes.Count;
        for (int index = 0; index < count; ++index)
        {
          if (parentObjectTypes[index].ObjectTypeID == parObjTypeId)
            return index;
        }
        if (!withInherited)
          return -1;
        parObjTypeId = MetaDataHelper.GetObjectTypeParentID(parObjTypeId);
        return parObjTypeId != -1 ? this.IndexOfParentObjectType(parObjTypeId, withInherited) : -1;
      }

      /// <summary>Очистить все поля правила, за исключением Guid</summary>
      public virtual void Clear() => this.ParentObjectTypes.Clear();

      /// <summary>
      /// Загрузить информацию в текущий объект из указанного объекта
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public virtual void Assign(object source)
      {
        if (!(source is CompositionsAutosortRule compositionsAutosortRule))
          return;
        this.Clear();
        this.ObjectID = compositionsAutosortRule.ObjectID;
        this.Guid = compositionsAutosortRule.Guid;
        this.Name = compositionsAutosortRule.Name;
        List<ParentObjectType> parentObjectTypes = this.ParentObjectTypes;
        if (parentObjectTypes.Capacity < parentObjectTypes.Count + compositionsAutosortRule.ParentObjectTypes.Count)
          parentObjectTypes.Capacity = parentObjectTypes.Count + compositionsAutosortRule.ParentObjectTypes.Count;
        foreach (ParentObjectType parentObjectType in compositionsAutosortRule.ParentObjectTypes)
          this.ParentObjectTypes.Add(parentObjectType.Clone() as ParentObjectType);
        this._useEvents = compositionsAutosortRule.UseEvents;
        this.GenerateStartSortingValues();
      }

      /// <summary>
      /// Перегенерировать стартовые значения атрибута "Сортировка" у всей коллекции дочерних типов объектов
      /// </summary>
      public virtual void GenerateStartSortingValues()
      {
        for (int index = 0; index < this.ParentObjectTypes.Count; ++index)
          this.ParentObjectTypes[index].GenerateStartSortingValues();
      }

      /// <summary>Выполнить синхронизацию с кэшем метаданных</summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с кэшем метаданных</param>
      public virtual void SyncMetadata(IUserSession session)
      {
        if (!(session is IUserSessionCacheDataSet))
          return;
        MetaDataHelper.SyncMetadata((session as IUserSessionCacheDataSet).CacheDataSet);
        for (int index = 0; index < this.ParentObjectTypes.Count; ++index)
          this.ParentObjectTypes[index].SyncMetadata(session);
      }

      /// <summary>
      /// Получить список видимых связей для указанного типа объекта.
      /// </summary>
      /// <param name="ObjectType">Тип объекта</param>
      /// <param name="returnDefault">Если true, то вернуть тип связи по умолчанию, если нет видимых типов связей</param>
      /// <returns>Cписок видимых связей для указанного типа объекта</returns>
      public virtual List<int> GetObjectTypeVisibleRelations(int ObjectType, bool returnDefault)
      {
        List<int> visibleRelTypes = new List<int>();
        int index1 = this.IndexOfParentObjectType(ObjectType, true);
        int num = 0;
        if (index1 >= 0)
        {
          ParentObjectType parentObjectType = this.ParentObjectTypes[index1];
          num = parentObjectType.ChildRelationTypes.Count;
          for (int index2 = 0; index2 < parentObjectType.ChildRelationTypes.Count; ++index2)
          {
            if (parentObjectType.ChildRelationTypes[index2].Visible)
              visibleRelTypes.Add(parentObjectType.ChildRelationTypes[index2].RelationTypeID);
          }
        }
        if (((visibleRelTypes.Count != 0 ? 0 : (num == 0 ? 1 : 0)) & (returnDefault ? 1 : 0)) != 0)
        {
          int defaultRelationTypeId = MetaDataHelper.GetDefaultRelationTypeID(ObjectType);
          visibleRelTypes.Add(defaultRelationTypeId);
        }
        CompositionsAutosortRuleEventArgs e = new CompositionsAutosortRuleEventArgs(ObjectType, returnDefault, visibleRelTypes);
        if (this.UseEvents)
          CompositionsAutosortRule.FireCompositionsGetVisibleRelationsEvent((object) this, e);
        return e.VisibleRelTypes;
      }

      /// <summary>
      /// Получить список видимых связей для указанного типа объекта.
      /// </summary>
      /// <param name="ObjectTypeGuid">Тип объекта</param>
      /// <param name="returnDefault">Если true, то вернуть тип связи по умолчанию, если нет видимых типов связей</param>
      /// <returns>Cписок видимых связей для указанного типа объекта</returns>
      public virtual List<int> GetObjectTypeVisibleRelations(Guid ObjectTypeGuid, bool returnDefault)
      {
        return this.GetObjectTypeVisibleRelations(MetaDataHelper.GetObjectTypeID(ObjectTypeGuid), returnDefault);
      }

      /// <summary>
      /// Получить список видимых связей для указанного типа объекта.
      /// </summary>
      /// <param name="ObjectType">Тип объекта</param>
      /// <param name="returnDefault">Если true, то вернуть тип связи по умолчанию, если нет видимых типов связей</param>
      /// <returns>Cписок видимых связей для указанного типа объекта</returns>
      public virtual List<Guid> GetObjectTypeVisibleRelationsGuids(int ObjectType, bool returnDefault)
      {
        List<Guid> visibleRelTypes = new List<Guid>();
        int index1 = this.IndexOfParentObjectType(ObjectType, true);
        int num = 0;
        if (index1 >= 0)
        {
          ParentObjectType parentObjectType = this.ParentObjectTypes[index1];
          num = parentObjectType.ChildRelationTypes.Count;
          for (int index2 = 0; index2 < parentObjectType.ChildRelationTypes.Count; ++index2)
          {
            if (parentObjectType.ChildRelationTypes[index2].Visible)
              visibleRelTypes.Add(MetaDataHelper.GetRelationTypeGuid(parentObjectType.ChildRelationTypes[index2].RelationTypeID));
          }
        }
        if (((visibleRelTypes.Count != 0 ? 0 : (num == 0 ? 1 : 0)) & (returnDefault ? 1 : 0)) != 0)
        {
          Guid relationTypeGuid = MetaDataHelper.GetDefaultRelationTypeGuid(ObjectType);
          visibleRelTypes.Add(relationTypeGuid);
        }
        CompositionsAutosortRuleGuidEventArgs e = new CompositionsAutosortRuleGuidEventArgs(MetaDataHelper.GetObjectTypeGuid(ObjectType), returnDefault, visibleRelTypes);
        if (this.UseEvents)
          CompositionsAutosortRule.FireCompositionsGetVisibleRelationsGuidsEvent((object) this, e);
        return e.VisibleRelTypes == null ? new List<Guid>(0) : e.VisibleRelTypes.Where<Guid>((Func<Guid, bool>) (o => o != Guid.Empty)).ToList<Guid>();
      }

      /// <summary>
      /// Получить список видимых связей для указанного типа объекта.
      /// </summary>
      /// <param name="ObjectTypeGuid">Тип объекта</param>
      /// <param name="returnDefault">Если true, то вернуть тип связи по умолчанию, если нет видимых типов связей</param>
      /// <returns>Cписок видимых связей для указанного типа объекта</returns>
      public virtual List<Guid> GetObjectTypeVisibleRelationsGuids(
        Guid ObjectTypeGuid,
        bool returnDefault)
      {
        return this.GetObjectTypeVisibleRelationsGuids(MetaDataHelper.GetObjectTypeID(ObjectTypeGuid), returnDefault);
      }

      /// <summary>
      /// Разрешено ли отображать выборки и классификаторы для указанного родительского типа объекта
      /// </summary>
      /// <param name="ObjectType">Родительский тип объекта</param>
      /// <param name="defaultValue">Значение по умолчанию, если тип не найден в коллекции</param>
      /// <returns>Разрешено ли отображать выборки и классификаторы для указанного родительского типа объекта</returns>
      public bool AreSelectionsAndClassifiersEnabled(int ObjectType, bool defaultValue = true)
      {
        int index = this.IndexOfParentObjectType(ObjectType, true);
        return index >= 0 ? this.ParentObjectTypes[index].EnableSelectionsAndClassifiers : defaultValue;
      }

      /// <summary>
      /// Установить признак разрешения отображения выборок и классификаторов для указанного родительского типа объекта
      /// </summary>
      /// <param name="ObjectType">Родительский тип объекта</param>
      /// <param name="value">Разрешено ли отображать выборки и классификаторы для указанного родительского типа объекта</param>
      public void SetSelectionsAndClassifiersEnabled(int ObjectType, bool value)
      {
        int index = this.IndexOfParentObjectType(ObjectType, true);
        if (index < 0)
          return;
        this.ParentObjectTypes[index].EnableSelectionsAndClassifiers = value;
      }

      /// <summary>
      /// Загрузить описание правила сортировки составов из указанного узла настроек
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Узел, из которого загружается информация</param>
      public virtual void Load(XMLSettingsStorage storage, XmlNode node)
      {
        this.Clear();
        if (storage == null || storage.document == null)
          return;
        if (node == null)
          node = storage.FindNode((XmlNode) storage.document.DocumentElement, "SortingRule", false);
        if (node == null)
          return;
        for (int i = 0; i < node.ChildNodes.Count; ++i)
        {
          XmlNode childNode = node.ChildNodes[i];
          if (!(childNode.Name != "ParentObjectType"))
          {
            ParentObjectType parentObjectType = new ParentObjectType();
            parentObjectType.Load(storage, childNode);
            if (parentObjectType.ObjectTypeID != -1 && !this._parentObjectTypes.Contains(parentObjectType))
              this._parentObjectTypes.Add(parentObjectType);
          }
        }
        this.GenerateStartSortingValues();
      }

      /// <summary>
      /// Загрузить информацию из атрибута "Сортировка и отображение составов" указанного объекта типа "Конфигурации ролей"
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="stream">Поток, содержащий XML-документ</param>
      /// <param name="throwException">Генерировать исключение, если возникнут проблемы при загрузке информации</param>
      protected virtual void LoadFromStream(IUserSession session, Stream stream, bool throwException)
      {
        try
        {
          if (stream == null || stream.Length <= 0L)
            return;
          XmlDocument xmlDocument = new XmlDocument();
          stream.Position = 0L;
          XMLSettingsStorage storage = new XMLSettingsStorage(stream);
          XmlNode node = storage == null || storage.document == null ? (XmlNode) null : storage.FindNode((XmlNode) storage.document.DocumentElement, "SortingRule", false);
          this.Load(storage, node);
        }
        catch
        {
          if (!throwException)
            return;
          throw;
        }
      }

      /// <summary>
      /// Загрузить информацию из атрибута "Сортировка и отображение составов" указанного объекта типа "Конфигурации ролей"
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="roleSettiongsObj">Объект типа "Конфигурации ролей"</param>
      /// <param name="throwException">Генерировать исключение, если возникнут проблемы при загрузке информации</param>
      protected virtual void LoadFromIDBObject(
        IUserSession session,
        IDBObject roleSettiongsObj,
        bool throwException)
      {
        try
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00690-306c-11d8-b4e9-00304f19f545"));
          if (!MetaDataHelper.IsObjectTypeChildOf(roleSettiongsObj.ObjectType, objectTypeId))
          {
            if (throwException)
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_3"), (object) MetaDataHelper.GetObjectTypeName(objectTypeId)));
          }
          else
          {
            IDBAttribute attributeByGuid = roleSettiongsObj.GetAttributeByGuid(new Guid("cad00691-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid == null)
            {
              if (throwException)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_4"), (object) roleSettiongsObj.NameInMessages, (object) LocalizationHolder.rm.GetString("Interfaces_5")));
            }
            else
            {
              this.Name = roleSettiongsObj.Caption;
              this.ObjectID = roleSettiongsObj.ObjectID;
              this.Guid = roleSettiongsObj.ObjectGUID;
              MemoryStream inStream = new MemoryStream();
              try
              {
                if (!(attributeByGuid is IBlobReader blobReader))
                  return;
                BlobInformation blobInformation = blobReader.OpenBlob(0);
                if (blobInformation.RealFileSize <= 0L)
                  return;
                byte[] buffer = blobReader.ReadDataBlock(0);
                if (buffer == null)
                  return;
                inStream.Write(buffer, 0, buffer.Length);
                inStream.Position = 0L;
                if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
                {
                  MemoryStream outStream = new MemoryStream();
                  ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
                  inStream.Close();
                  inStream = outStream;
                }
                this.LoadFromStream(session, (Stream) inStream, throwException);
              }
              finally
              {
                inStream.Close();
              }
            }
          }
        }
        catch
        {
          if (!throwException)
            return;
          throw;
        }
      }

      /// <summary>
      /// Загрузить информацию из атрибута "Сортировка и отображение составов" указанного объекта типа "Конфигурации ролей"
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="objectID">Идентификатор версии объекта типа "Конфигурации ролей"</param>
      /// <param name="throwException">Генерировать исключение, если возникнут проблемы при загрузке информации</param>
      public virtual void Load(IUserSession session, long objectID, bool throwException)
      {
        this.Clear();
        if (session == null)
        {
          if (throwException)
            throw new Exception(LocalizationHolder.rm.GetString("Interfaces_6"));
        }
        else
        {
          try
          {
            IDBObject roleSettiongsObj = session.GetObject(objectID);
            this.LoadFromIDBObject(session, roleSettiongsObj, throwException);
            this.SyncMetadata(session);
          }
          catch
          {
            if (!throwException)
              return;
            throw;
          }
        }
      }

      /// <summary>
      /// Сохранить описание правила сортировки в родительский узел в XML-хранилище
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Родительский узел или null (тогда узел создаётся прямо в корневом узле документа XML)</param>
      public virtual void Save(XMLSettingsStorage storage, XmlNode node)
      {
        if (this._parentObjectTypes == null)
          this._parentObjectTypes = new List<ParentObjectType>();
        if (storage == null)
          return;
        node = node == null ? (XmlNode) storage.document.DocumentElement : node;
        XmlNode nodeWithAttr1 = storage.FindNodeWithAttr(node, "SortingRule", "Guid", this._guid.ToString(), true);
        node.RemoveChild(nodeWithAttr1);
        XmlNode nodeWithAttr2 = storage.FindNodeWithAttr(node, "SortingRule", "Guid", this._guid.ToString(), true);
        for (int index = 0; index < this._parentObjectTypes.Count; ++index)
          this._parentObjectTypes[index].Save(storage, nodeWithAttr2);
      }

      /// <summary>
      /// Сохранить информацию в атрибут "Сортировка и отображение составов" указанного объекта типа "Конфигурации ролей"
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="stream">Поток, содержащий XML-документ</param>
      /// <param name="throwException">Генерировать исключение, если возникнут проблемы при загрузке информации</param>
      protected virtual void SaveToStream(IUserSession session, Stream stream, bool throwException)
      {
        try
        {
          if (stream == null)
            return;
          XMLSettingsStorage storage = new XMLSettingsStorage();
          this.Save(storage, (XmlNode) null);
          stream.Position = 0L;
          storage.Save(stream);
        }
        catch
        {
          if (!throwException)
            return;
          throw;
        }
      }

      /// <summary>
      /// Сохранить информацию в атрибут "Сортировка и отображение составов" указанного объекта типа "Конфигурации ролей"
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="roleSettiongsObj">Объект типа "Конфигурации ролей"</param>
      /// <param name="throwException">Генерировать исключение, если возникнут проблемы при загрузке информации</param>
      protected virtual void SaveToIDBObject(
        IUserSession session,
        IDBObject roleSettiongsObj,
        bool throwException)
      {
        try
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00690-306c-11d8-b4e9-00304f19f545"));
          if (!MetaDataHelper.IsObjectTypeChildOf(roleSettiongsObj.ObjectType, objectTypeId))
          {
            if (throwException)
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_7"), (object) MetaDataHelper.GetObjectTypeName(objectTypeId)));
          }
          else
          {
            IDBAttribute attributeByGuid = roleSettiongsObj.GetAttributeByGuid(new Guid("cad00691-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid == null)
            {
              if (throwException)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_8"), (object) roleSettiongsObj.NameInMessages, (object) LocalizationHolder.rm.GetString("Interfaces_9")));
            }
            else
            {
              MemoryStream inStream = new MemoryStream();
              MemoryStream outStream = new MemoryStream();
              try
              {
                this.SaveToStream(session, (Stream) inStream, throwException);
                inStream.Position = 0L;
                ZLibStreamHelper.PackStream((Stream) inStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
                if (!(attributeByGuid is IBlobWriter blobWriter))
                  return;
                byte[] array = outStream.ToArray();
                BlobInformation blobInfo = new BlobInformation(inStream.Length, outStream.Length, DateTime.Now, "SortingAndVisualize.xml", ArcMethods.ZLibPacked, LocalizationHolder.rm.GetString("Interfaces_611"));
                blobWriter.OpenBlob(blobInfo, false);
                blobWriter.WriteDataBlock(array);
              }
              finally
              {
                inStream.Close();
                outStream.Close();
              }
            }
          }
        }
        catch
        {
          if (!throwException)
            return;
          throw;
        }
      }

      /// <summary>
      /// Записать информацию в атрибута "Сортировка и отображение составов" указанного объекта типа "Конфигурации ролей"
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="objectID">Идентификатор версии объекта типа "Конфигурации ролей"</param>
      /// <param name="throwException">Генерировать исключение, если возникнут проблемы при сохранении информации</param>
      public virtual void Save(IUserSession session, long objectID, bool throwException)
      {
        if (session == null)
        {
          if (throwException)
            throw new Exception(LocalizationHolder.rm.GetString("Interfaces_10"));
        }
        else
        {
          MetaDataHelper.GetObjectTypeID(new Guid("cad00690-306c-11d8-b4e9-00304f19f545"));
          try
          {
            this.SyncMetadata(session);
            IDBObject roleSettiongsObj = session.GetObject(objectID);
            this.SaveToIDBObject(session, roleSettiongsObj, throwException);
          }
          catch
          {
            if (!throwException)
              return;
            throw;
          }
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone()
      {
        CompositionsAutosortRule compositionsAutosortRule = new CompositionsAutosortRule();
        compositionsAutosortRule.Assign((object) this);
        return (object) compositionsAutosortRule;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as CompositionsAutosortRule);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(CompositionsAutosortRule other)
      {
        return other == null ? 1 : this._guid.CompareTo(other._guid);
      }

      /// <summary>
      /// Сравнить расположение типов связей в составе указанного родительского типа объекта
      /// </summary>
      /// <param name="projObjType">Идентификатор родительского типа объекта</param>
      /// <param name="relType1">Первый тип связи</param>
      /// <param name="relType2">Второй тип связи</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(int projObjType, int relType1, int relType2)
      {
        List<int> visibleRelations = this.GetObjectTypeVisibleRelations(projObjType, true);
        return visibleRelations == null || visibleRelations.Count == 0 ? 0 : visibleRelations.IndexOf(relType1).CompareTo(visibleRelations.IndexOf(relType2));
      }

      /// <summary>
      /// Сравнить расположение дочерних типов объектов в составе указанного родительского типа объекта указанными типами связей
      /// </summary>
      /// <param name="projObjType">Идентификатор родительского типа объекта</param>
      /// <param name="relType1">Первый тип связи</param>
      /// 
      ///             /// <param name="relType2">Второй тип связи</param>
      /// <param name="childType1">Первый дочерний тип объекта</param>
      /// <param name="childType2">Второй дочерний тип объекта</param>
      /// <param name="fullSorting">true - сравнивается положение дочерних типов объектов в правиле</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(
        int projObjType,
        int relType1,
        int relType2,
        int childType1,
        int childType2,
        bool fullSorting)
      {
        int index = this.IndexOfParentObjectType(projObjType, true);
        if (index < 0)
          return 0;
        ParentObjectType parentObjectType = this.ParentObjectTypes[index];
        ChildRelationType childRelationType1 = parentObjectType.ChildRelationTypes.Find((Predicate<ChildRelationType>) (item => item.RelationTypeID == relType1));
        ChildRelationType childRelationType2 = relType2 != relType1 ? parentObjectType.ChildRelationTypes.Find((Predicate<ChildRelationType>) (item => item.RelationTypeID == relType2)) : childRelationType1;
        if (childRelationType1 == null || childRelationType2 == null || childRelationType1.ChildObjectTypes.Count == 0 || childRelationType2.ChildObjectTypes.Count == 0)
          return 0;
        int num1 = parentObjectType.ChildRelationTypes.IndexOf(childRelationType1);
        int num2 = relType2 != relType1 ? parentObjectType.ChildRelationTypes.IndexOf(childRelationType2) : num1;
        int num3 = num1.CompareTo(num2);
        return num3 != 0 | fullSorting ? num3 : childRelationType1.GetNearestBaseParentObjectTypeIndex(childType1).CompareTo(childRelationType2.GetNearestBaseParentObjectTypeIndex(childType2));
      }

      /// <summary>
      /// Событие генерируется при получении списка видимых типов связей для указанного родительского типа объектов
      /// </summary>
      public static event CompositionsGetVisibleRelationsEventHandler OnGetVisibleRelations;

      /// <summary>
      /// Событие генерируется при получении списка Guid видимых типов связей для указанного родительского типа объектов
      /// </summary>
      public static event CompositionsGetVisibleRelationsGuidEventHandler OnGetVisibleRelationsGuids;

      /// <summary>
      /// Сгенерировать событие CompositionsGetVisibleRelationsEventHandler
      /// </summary>
      /// <param name="sender">Отправитель</param>
      /// <param name="e">Аргументы события</param>
      private static void FireCompositionsGetVisibleRelationsEvent(
        object sender,
        CompositionsAutosortRuleEventArgs e)
      {
        if (sender == null || e == null || CompositionsAutosortRule.OnGetVisibleRelations == null)
          return;
        CompositionsAutosortRuleEventArgs autosortRuleEventArgs = e.Clone() as CompositionsAutosortRuleEventArgs;
        try
        {
          CompositionsAutosortRule.OnGetVisibleRelations(sender, e);
        }
        catch
        {
          e = autosortRuleEventArgs;
        }
      }

      /// <summary>
      /// Сгенерировать событие CompositionsGetVisibleRelationsGuidsEventHandler
      /// </summary>
      /// <param name="sender">Отправитель</param>
      /// <param name="e">Аргументы события</param>
      private static void FireCompositionsGetVisibleRelationsGuidsEvent(
        object sender,
        CompositionsAutosortRuleGuidEventArgs e)
      {
        if (sender == null || e == null || CompositionsAutosortRule.OnGetVisibleRelations == null)
          return;
        CompositionsAutosortRuleGuidEventArgs ruleGuidEventArgs = e.Clone() as CompositionsAutosortRuleGuidEventArgs;
        try
        {
          CompositionsAutosortRule.OnGetVisibleRelationsGuids(sender, e);
        }
        catch
        {
          e = ruleGuidEventArgs;
        }
      }
    }
}
