
// Type: Intermech.Interfaces.Contexts.EditingContextsObjectContainer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Класс-контейнер, в котором хранится информация контекста редактирования, а также
    /// связанных с ним контекстов (по идентификатору изменения)
    /// </summary>
    [Serializable]
    public sealed class EditingContextsObjectContainer : IAssignable, ICloneable
    {
      /// <summary>
      /// Идентификатор типа объектов "Контексты редактирования"
      /// </summary>
      internal static int objtypeEditingContext = -1;
      /// <summary>Идентификатор типа объектов "Извещения"</summary>
      internal static int objtypeECO = -1;
      /// <summary>Идентификатор типа объектов "Производственные заказы"</summary>
      internal static int objtypeProdOrders = -1;
      /// <summary>Идентификатор версии объекта-контекста</summary>
      public long ContextID;
      /// <summary>
      /// Номер группы изменений. Используется для объединения нескольких контекстов редактирования
      /// в логическую группу
      /// </summary>
      public long ModificationID;
      /// <summary>Идентификатор типа версии объекта-контекста</summary>
      public int ContextTypeID = -1;
      /// <summary>
      /// Информация об архивных версиях объектов, включённых в данный контекст, а также в
      /// связанные контексты (строки из таблицы "IMS_VERSIONS_CONTEXT")
      /// </summary>
      public List<EditingContextsObjectVersion> Objects = new List<EditingContextsObjectVersion>();
      /// <summary>
      /// Подробная информация о версиях объектов, включённых в данный контекст, а также в
      /// связанные контексты (заголовки объектов, т.п.)
      /// </summary>
      public List<ObjectVersionDescription> Descriptions = new List<ObjectVersionDescription>();
      /// <summary>
      /// Словарик, позволяющий по идентификатору версии получить её описание (строится динамически)
      /// </summary>
      [NonSerialized]
      private Dictionary<long, ObjectVersionDescription> _descriptionsCache;
      /// <summary>
      /// Словарик, позволяющий по идентификатору версии получить строку данных (строится динамически)
      /// </summary>
      [NonSerialized]
      private Dictionary<long, List<EditingContextsObjectVersion>> _rowsCache;
      /// <summary>
      /// Список всех контекстов в группе (строится динамически)
      /// </summary>
      [NonSerialized]
      private List<long> _contextsCache;
      /// <summary>
      /// Список всех идентификаторов версий объектов (строится динамически)
      /// </summary>
      [NonSerialized]
      private List<long> _versionsCache;
      /// <summary>
      /// Список всех идентификаторов объектов (строится динамически)
      /// </summary>
      [NonSerialized]
      private List<long> _objectsCache;
      /// <summary>
      /// Вспомогательный класс для сортировки списков версий объектов по их заголовкам
      /// </summary>
      [NonSerialized]
      private EditingContextsObjectContainer.ContextsComparer _comparer;
      /// <summary>Количество версий объектов в основном контексте</summary>
      [NonSerialized]
      private int _contextVersionsCount;
      /// <summary>Количество всех версий объектов</summary>
      [NonSerialized]
      private int _allVersionsCount;

      /// <summary>Создать экземпляр класса</summary>
      public EditingContextsObjectContainer()
      {
      }

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="contextID">Идентификатор версии объекта-контекста</param>
      /// <param name="modificationID">Номер группы изменений. Используется для объединения нескольких контекстов.</param>
      /// <param name="contextTypeID">Идентификатор типа версии объекта-контекста</param>
      /// <_param name="rule">Правило подбора версий, хранящееся в контексте редактирования.</_param>
      /// <param name="objects">Список версий объектов, включённых в данный контекст, а также
      /// во все остальные контексты с таким же идентификатором изменения.
      /// Ключ - ID версии, значение - ID версии контекста, в который включена данная версия.</param>
      /// <param name="descriptions">Список с описаниями всех версий объектов из контейнера (включая описания контекстов)</param>
      public EditingContextsObjectContainer(
        long contextID,
        long modificationID,
        int contextTypeID,
        List<EditingContextsObjectVersion> objects,
        List<ObjectVersionDescription> descriptions)
      {
        this.ContextID = contextID;
        this.ModificationID = modificationID;
        this.ContextTypeID = contextTypeID;
        if (descriptions != null)
        {
          this.Descriptions.Clear();
          for (int index = 0; index < descriptions.Count; ++index)
            this.Descriptions.Add(descriptions[index].Clone() as ObjectVersionDescription);
        }
        if (objects == null)
          return;
        this.Objects.Clear();
        for (int index = 0; index < objects.Count; ++index)
          this.Objects.Add(objects[index].Clone() as EditingContextsObjectVersion);
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public EditingContextsObjectContainer(object source) => this.Assign(source);

      /// <summary>
      /// Является ли контекст упрощённым
      /// (не меняет содержимое номера группы изенений у контекстных объектов, не может
      /// быть связанным, допускает применение в своём содержимом версий объектов, принадлежащих
      /// другим контекстам редактирования)
      /// </summary>
      public bool SimpleContext
      {
        [DebuggerStepThrough] get => MetaDataHelper.IsSimpleEditingContext(this.ContextTypeID);
      }

      /// <summary>Количество версий объектов в текущем контексте</summary>
      public int ContextVersionsCount
      {
        [DebuggerStepThrough] get => this._contextVersionsCount;
      }

      /// <summary>Количество всех версий объектов</summary>
      public int AllVersionsCount
      {
        [DebuggerStepThrough] get => this._allVersionsCount;
      }

      /// <summary>Удаляет справочные таблицы.</summary>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void ClearCacheTables()
      {
        this._descriptionsCache = (Dictionary<long, ObjectVersionDescription>) null;
        this._rowsCache = (Dictionary<long, List<EditingContextsObjectVersion>>) null;
        this._contextsCache = (List<long>) null;
        this._versionsCache = (List<long>) null;
        this._objectsCache = (List<long>) null;
      }

      /// <summary>Перестраивает справочные таблицы.</summary>
      private void RebuildCacheTables()
      {
        if (EditingContextsObjectContainer.objtypeEditingContext == 0 || EditingContextsObjectContainer.objtypeEditingContext == -1)
        {
          EditingContextsObjectContainer.objtypeEditingContext = MetaDataHelper.GetObjectTypeID("cad0146b-306c-11d8-b4e9-00304f19f545");
          EditingContextsObjectContainer.objtypeECO = MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545");
          EditingContextsObjectContainer.objtypeProdOrders = MetaDataHelper.GetObjectTypeID("cadd92e9-306c-11d8-b4e9-00304f19f545");
        }
        if (this._comparer == null)
          this._comparer = new EditingContextsObjectContainer.ContextsComparer(this);
        this._descriptionsCache = new Dictionary<long, ObjectVersionDescription>(this.Descriptions.Count);
        for (int index = 0; index < this.Descriptions.Count; ++index)
          this._descriptionsCache[Math.Abs(this.Descriptions[index].F_OBJECT_ID)] = this.Descriptions[index];
        this._rowsCache = new Dictionary<long, List<EditingContextsObjectVersion>>();
        if (this.Descriptions.Count > 0)
        {
          for (int index = this.Objects.Count - 1; index >= 0; --index)
          {
            if (!this._descriptionsCache.ContainsKey(Math.Abs(this.Objects[index].F_OBJECT_ID)))
            {
              ObjectVersionDescription versionDescription = new ObjectVersionDescription(this.Objects[index].F_ID, this.Objects[index].F_OBJECT_ID, -1, -1, 0L, 0L, LocalizationHolder.rm.GetString("Interfaces_624"), 0L, this.Objects[index].F_MODIFICATION_ID, 0L, ObjectVersionDescriptionOptions.InvalidDescription);
              this._descriptionsCache.Add(Math.Abs(this.Objects[index].F_OBJECT_ID), versionDescription);
              this.Descriptions.Add(versionDescription);
            }
          }
        }
        for (int index = 0; index < this.Objects.Count; ++index)
        {
          EditingContextsObjectVersion contextsObjectVersion = this.Objects[index];
          if (!this._rowsCache.ContainsKey(Math.Abs(contextsObjectVersion.F_OBJECT_ID)))
            this._rowsCache[Math.Abs(contextsObjectVersion.F_OBJECT_ID)] = new List<EditingContextsObjectVersion>();
          this._rowsCache[Math.Abs(contextsObjectVersion.F_OBJECT_ID)].Add(contextsObjectVersion);
        }
        this._contextsCache = new List<long>();
        this._versionsCache = new List<long>();
        this._objectsCache = new List<long>();
        this._contextVersionsCount = 0;
        this._allVersionsCount = 0;
        if (this.Descriptions.Count > 0 && this.ContextID != 0L)
        {
          for (int index = 0; index < this.Descriptions.Count; ++index)
          {
            if (MetaDataHelper.IsObjectTypeEditingContext(this.Descriptions[index].F_OBJECT_TYPE))
            {
              if (!this._contextsCache.Contains(Math.Abs(this.Descriptions[index].F_OBJECT_ID)) && !this._contextsCache.Contains(-Math.Abs(this.Descriptions[index].F_OBJECT_ID)))
                this._contextsCache.Add(this.Descriptions[index].F_OBJECT_ID);
            }
            else
            {
              if (!this._versionsCache.Contains(Math.Abs(this.Descriptions[index].F_OBJECT_ID)))
                this._versionsCache.Add(Math.Abs(this.Descriptions[index].F_OBJECT_ID));
              this._objectsCache.Add(this.Descriptions[index].F_ID);
            }
          }
        }
        else
        {
          foreach (EditingContextsObjectVersion contextsObjectVersion in this.Objects)
          {
            if (!this._versionsCache.Contains(Math.Abs(contextsObjectVersion.F_OBJECT_ID)))
              this._versionsCache.Add(Math.Abs(contextsObjectVersion.F_OBJECT_ID));
            if (!this._objectsCache.Contains(contextsObjectVersion.F_ID))
              this._objectsCache.Add(contextsObjectVersion.F_ID);
            if (!this._contextsCache.Contains(Math.Abs(contextsObjectVersion.F_CONTEXT_ID)) && !this._contextsCache.Contains(-Math.Abs(contextsObjectVersion.F_CONTEXT_ID)))
              this._contextsCache.Add(contextsObjectVersion.F_CONTEXT_ID);
          }
        }
        if (this._contextsCache != null)
        {
          this._contextsCache.Remove(Math.Abs(this.ContextID));
          this._contextsCache.Remove(-Math.Abs(this.ContextID));
          if (this.SimpleContext)
            this._contextsCache.Clear();
          else
            this._contextsCache.Sort((IComparer<long>) this._comparer);
          this._contextsCache.Insert(0, this.ContextID);
        }
        this._versionsCache.Sort();
        this._objectsCache.Sort();
        foreach (EditingContextsObjectVersion contextsObjectVersion in this.Objects)
        {
          ++this._allVersionsCount;
          if (Math.Abs(contextsObjectVersion.F_CONTEXT_ID) == Math.Abs(this.ContextID))
            ++this._contextVersionsCount;
        }
      }

      /// <summary>
      /// Проверить, есть ли указанная версия в контексте, либо в связанных контекстах
      /// </summary>
      /// <param name="versionID">Искомая версия объекта</param>
      /// <returns>true - версия найдена в текущем контексте, либо в связанном контексте</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public bool ExistsVersion(long versionID)
      {
        if (this._versionsCache == null)
          this.RebuildCacheTables();
        return this._versionsCache.Contains(Math.Abs(versionID));
      }

      /// <summary>
      /// Проверить, есть ли указанная версия в контексте, либо в связанных контекстах
      /// </summary>
      /// <param name="versionID">Искомая версия объекта</param>
      /// <param name="linkedContexts">true - проверять и в связанных контекстах</param>
      /// <returns>true - версия найдена в текущем контексте, либо в связанном контексте</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public bool ExistsVersion(long versionID, bool linkedContexts)
      {
        if (this._rowsCache == null)
          this.RebuildCacheTables();
        if (!this._rowsCache.ContainsKey(Math.Abs(versionID)))
          return false;
        List<EditingContextsObjectVersion> contextsObjectVersionList = this._rowsCache[Math.Abs(versionID)];
        for (int index = 0; index < contextsObjectVersionList.Count; ++index)
        {
          if (Math.Abs(contextsObjectVersionList[index].F_CONTEXT_ID) == Math.Abs(this.ContextID) || linkedContexts && !this.SimpleContext)
            return true;
        }
        return false;
      }

      /// <summary>
      /// Проверить наличие версии в связанных контекстах, но не в основном
      /// </summary>
      /// <param name="versionID">Искомая версия объекта</param>
      /// <returns>true - версия найдена в одном из связанных контекстов</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public bool ExistsLinkedVersion(long versionID)
      {
        if (this._rowsCache == null)
          this.RebuildCacheTables();
        List<EditingContextsObjectVersion> contextsObjectVersionList = this._rowsCache.ContainsKey(Math.Abs(versionID)) ? this._rowsCache[Math.Abs(versionID)] : (List<EditingContextsObjectVersion>) null;
        if (contextsObjectVersionList == null || contextsObjectVersionList.Count == 0)
          return false;
        for (int index = 0; index < contextsObjectVersionList.Count; ++index)
        {
          if (Math.Abs(contextsObjectVersionList[index].F_CONTEXT_ID) != Math.Abs(this.ContextID) && !this.SimpleContext)
            return true;
        }
        return false;
      }

      /// <summary>
      /// Проверить, есть ли версия указанного объекта в контексте, лио в связанных контекстах
      /// </summary>
      /// <param name="ID">Идентификатор объекта</param>
      /// <returns>true - объект найден в текущем контексте, либо в связанном контексте</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public bool ExistsObject(long ID)
      {
        if (this._objectsCache == null)
          this.RebuildCacheTables();
        return this._objectsCache.Contains(ID);
      }

      /// <summary>
      /// Получить идентификатор версии для указанного объекта во всех контекстах
      /// </summary>
      /// <param name="ID">Идентификатор объекта</param>
      /// <returns>Идентификатор версии или Intermech.Consts.UnknownObjectId</returns>
      public long GetObjectVersion(long ID) => this.GetObjectVersion(ID, false);

      /// <summary>Получить идентификатор версии для указанного объекта</summary>
      /// <param name="ID">Идентификатор объекта</param>
      /// <param name="onlyInLinked">Искать только в связанных контекстах, иначе - везде</param>
      /// <returns>Идентификатор версии или Intermech.Consts.UnknownObjectId</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public long GetObjectVersion(long ID, bool onlyInLinked)
      {
        for (int index = 0; index < this.Objects.Count; ++index)
        {
          if (this.Objects[index].F_ID == ID && (!onlyInLinked || this.SimpleContext || Math.Abs(this.Objects[index].F_CONTEXT_ID) != Math.Abs(this.ContextID)))
            return this.Objects[index].F_OBJECT_ID;
        }
        return 0;
      }

      /// <summary>
      /// Получить список контекстов, в которых присутствует версия
      /// </summary>
      /// <param name="versionID">Искомая версия объекта</param>
      /// <returns>Список контекстов, в которых присутствует версия</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public List<long> GetVersionContextID(long versionID)
      {
        List<EditingContextsObjectVersion> version = this.GetVersion(versionID);
        if (version == null)
          return (List<long>) null;
        List<long> versionContextId = new List<long>(version.Count);
        for (int index = 0; index < version.Count; ++index)
          versionContextId.Add(version[index].F_CONTEXT_ID);
        return versionContextId;
      }

      /// <summary>
      /// Получить список идентификаторов версий всех контекстов редактирования,
      /// которые имеют текущий номер группы изменений
      /// </summary>
      /// <returns>Список идентификаторов версий всех контекстов редактирования</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public List<long> GetContextsID()
      {
        if (this._contextsCache == null)
          this.RebuildCacheTables();
        return new List<long>((IEnumerable<long>) this._contextsCache);
      }

      /// <summary>
      /// Получить список идентификаторов всех версий объектов из текущего контекста,
      /// а также из связанных, если это требуется
      /// </summary>
      /// <param name="addFromLinked">true - в список добавлять также версии объектов из связанных контекстов</param>
      /// <param name="forUser">Идентификатор пользователя, для которого создаётся список</param>
      /// <returns>Список идентификаторов версий объектов из контекста, а также из связанных контекстов</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public List<long> GetVersionsID(bool addFromLinked, long forUser)
      {
        if (this._comparer == null)
          this._comparer = new EditingContextsObjectContainer.ContextsComparer(this);
        List<long> versionsId = new List<long>();
        if (this.Objects.Count == 0)
          return versionsId;
        foreach (EditingContextsObjectVersion contextsObjectVersion in this.Objects)
        {
          if ((addFromLinked || Math.Abs(contextsObjectVersion.F_CONTEXT_ID) == Math.Abs(this.ContextID)) && !this.SimpleContext)
            versionsId.Add(contextsObjectVersion.F_OBJECT_ID);
        }
        versionsId.Sort((IComparer<long>) this._comparer);
        return versionsId;
      }

      /// <summary>
      /// Получить список идентификаторов всех версий объектов из указанного контекста
      /// </summary>
      /// <param name="contextID">Идентификатор версии контекста</param>
      /// <param name="forUser">Идентификатор пользователя, для которого создаётся список</param>
      /// <returns>Список идентификаторов версий объектов из указанного контекста</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public List<long> GetVersionsID(long contextID, long forUser)
      {
        if (this._comparer == null)
          this._comparer = new EditingContextsObjectContainer.ContextsComparer(this);
        List<long> versionsId = new List<long>();
        if (this.Objects.Count == 0)
          return versionsId;
        foreach (EditingContextsObjectVersion contextsObjectVersion in this.Objects)
        {
          if (Math.Abs(contextsObjectVersion.F_CONTEXT_ID) == Math.Abs(contextID))
            versionsId.Add(contextsObjectVersion.F_OBJECT_ID);
        }
        versionsId.Sort((IComparer<long>) this._comparer);
        return versionsId;
      }

      /// <summary>Дать описание указанного объекта по его версии</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <returns>Описание или null</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public ObjectVersionDescription GetDescription(long objectID)
      {
        if (this._descriptionsCache == null)
          this.RebuildCacheTables();
        return objectID == 0L || !this._descriptionsCache.ContainsKey(Math.Abs(objectID)) ? (ObjectVersionDescription) null : this._descriptionsCache[Math.Abs(objectID)];
      }

      /// <summary>Дать информацию по версии объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <returns>Информация по версии объекта или null</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public List<EditingContextsObjectVersion> GetVersion(long objectID)
      {
        if (this._rowsCache == null)
          this.RebuildCacheTables();
        if (objectID == 0L)
          return (List<EditingContextsObjectVersion>) null;
        return this._rowsCache.ContainsKey(Math.Abs(objectID)) ? this._rowsCache[Math.Abs(objectID)] : (List<EditingContextsObjectVersion>) null;
      }

      /// <summary>Дать информацию по версии объекта</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="verContextID">Контекст версии</param>
      /// <returns>Информация по версии объекта или null</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public EditingContextsObjectVersion GetVersion(long objectID, long verContextID)
      {
        if (this._rowsCache == null)
          this.RebuildCacheTables();
        if (objectID == 0L)
          return (EditingContextsObjectVersion) null;
        if (this._rowsCache.ContainsKey(Math.Abs(objectID)))
        {
          List<EditingContextsObjectVersion> contextsObjectVersionList = this._rowsCache[Math.Abs(objectID)];
          for (int index = 0; index < contextsObjectVersionList.Count; ++index)
          {
            if (Math.Abs(contextsObjectVersionList[index].F_CONTEXT_ID) == Math.Abs(verContextID))
              return contextsObjectVersionList[index];
          }
        }
        return (EditingContextsObjectVersion) null;
      }

      /// <summary>Дать заголовок указанного объекта по его версии</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <returns>Заголовок или String.Empty</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public string GetCaption(long objectID)
      {
        if (this._descriptionsCache == null)
          this.RebuildCacheTables();
        return objectID == 0L || !this._descriptionsCache.ContainsKey(Math.Abs(objectID)) ? string.Empty : this._descriptionsCache[Math.Abs(objectID)].CAPTION;
      }

      /// <summary>Добавить новую версию в контекст</summary>
      /// <param name="newVersion">Новая версия</param>
      /// <param name="newVerDesc">Описание новой версии</param>
      /// <returns>true - версия добавлена, false - указанный объект уже есть в контексте</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public bool AddVersion(
        EditingContextsObjectVersion newVersion,
        ObjectVersionDescription newVerDesc)
      {
        if (newVersion == null || this.ExistsObject(newVersion.F_ID) && !this.ExistsVersion(newVersion.F_OBJECT_ID))
          return false;
        newVersion.F_CONTEXT_ID = this.ContextID;
        newVersion.F_MODIFICATION_ID = this.ModificationID;
        this.Objects.Add(newVersion);
        if (newVerDesc != null)
        {
          for (int index = this.Descriptions.Count - 1; index >= 0; --index)
          {
            if (this.Descriptions[index].F_OBJECT_ID == newVerDesc.F_OBJECT_ID)
            {
              this.Descriptions.RemoveAt(index);
              break;
            }
          }
          this.Descriptions.Add(newVerDesc);
        }
        this.ClearCacheTables();
        return true;
      }

      /// <summary>Удалить описание версии из списка</summary>
      /// <param name="version">Удаляемая версия</param>
      /// <returns>true - версия была удалена</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public bool DeleteVersion(long version)
      {
        if (version == 0L)
          return false;
        List<EditingContextsObjectVersion> version1 = this.GetVersion(version);
        bool flag = true;
        if (version1 != null)
        {
          for (int index = 0; index < version1.Count; ++index)
          {
            if (Math.Abs(version1[index].F_CONTEXT_ID) != Math.Abs(this.ContextID))
            {
              flag = false;
              break;
            }
          }
        }
        for (int index = this.Objects.Count - 1; index >= 0; --index)
        {
          if (Math.Abs(this.Objects[index].F_OBJECT_ID) == Math.Abs(version) && Math.Abs(this.Objects[index].F_CONTEXT_ID) == Math.Abs(this.ContextID))
            this.Objects.RemoveAt(index);
        }
        if (flag)
        {
          for (int index = this.Descriptions.Count - 1; index >= 0; --index)
          {
            if (this.Descriptions[index].F_OBJECT_ID == version)
              this.Descriptions.RemoveAt(index);
          }
        }
        this.ClearCacheTables();
        return true;
      }

      /// <summary>Удалить описание объекта из списка</summary>
      /// <param name="fID">Удаляемый объект</param>
      /// <returns>true - объект был удалён</returns>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public bool DeleteObject(long fID) => this.DeleteVersion(this.GetObjectVersion(fID));

      /// <summary>Заменить одну версию на другую</summary>
      /// <param name="version">Старая версия</param>
      /// <param name="newVersion">Новая версия</param>
      /// <param name="newVerDesc">Описание новой версии</param>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void ReplaceVersion(
        long version,
        EditingContextsObjectVersion newVersion,
        ObjectVersionDescription newVerDesc)
      {
        if (version == 0L || newVersion == null)
          return;
        this.DeleteVersion(version);
        this.AddVersion(newVersion, newVerDesc);
      }

      /// <summary>
      /// Получить представление экземпляра класса в виде строки
      /// </summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString()
      {
        return $"ContextID: {this.ContextID}; ModificationID: {this.ModificationID}; Objects: {(this.Objects != null ? this.Objects.Count : 0)}; With descriptions: {this.Descriptions != null}";
      }

      /// <summary>Очистить экземпляр класса</summary>
      [MethodImpl(MethodImplOptions.Synchronized)]
      public void Clear()
      {
        this.ContextID = 0L;
        this.ModificationID = 0L;
        this.ContextTypeID = -1;
        this.Objects.Clear();
        this.Descriptions.Clear();
        this.ClearCacheTables();
      }

      /// <summary>Скопировать информацию из указанного объекта</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is EditingContextsObjectContainer contextsObjectContainer))
          return;
        this.ContextID = contextsObjectContainer.ContextID;
        this.ModificationID = contextsObjectContainer.ModificationID;
        this.ContextTypeID = contextsObjectContainer.ContextTypeID;
        if (contextsObjectContainer.Objects != null)
        {
          this.Objects.Clear();
          for (int index = 0; index < contextsObjectContainer.Objects.Count; ++index)
            this.Objects.Add(contextsObjectContainer.Objects[index].Clone() as EditingContextsObjectVersion);
        }
        if (contextsObjectContainer.Descriptions == null)
          return;
        this.Descriptions.Clear();
        for (int index = 0; index < contextsObjectContainer.Descriptions.Count; ++index)
          this.Descriptions.Add(contextsObjectContainer.Descriptions[index].Clone() as ObjectVersionDescription);
      }

      /// <summary>Вернуть точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new EditingContextsObjectContainer((object) this);

      /// <summary>
      /// Вернуть точную копию экземпляра, из которой будет удалена вся информация,
      /// кроме списка версий объектов главного контекста. Упрощённая копия предназначена
      /// для передачи на серверную сторону (в целях внесения изменений в контекст редактирования)
      /// </summary>
      /// <returns>Точная копия, из которой будет удалена вся информация,
      /// кроме списка версий объектов главного контекста</returns>
      public EditingContextsObjectContainer SimpleClone()
      {
        EditingContextsObjectContainer contextsObjectContainer = new EditingContextsObjectContainer((object) this);
        List<long> longList = new List<long>();
        longList.Add(this.ContextID);
        if (contextsObjectContainer.Objects != null)
        {
          for (int index = this.Objects.Count - 1; index >= 0; --index)
          {
            EditingContextsObjectVersion contextsObjectVersion = this.Objects[index];
            if (Math.Abs(contextsObjectVersion.F_CONTEXT_ID) != Math.Abs(contextsObjectContainer.ContextID))
              this.Objects.RemoveAt(index);
            else
              longList.Add(Math.Abs(contextsObjectVersion.F_OBJECT_ID));
          }
        }
        if (contextsObjectContainer.Descriptions != null)
        {
          for (int index = contextsObjectContainer.Descriptions.Count - 1; index >= 0; --index)
          {
            if (!longList.Contains(Math.Abs(contextsObjectContainer.Descriptions[index].F_OBJECT_ID)))
              contextsObjectContainer.Descriptions.RemoveAt(index);
            else
              contextsObjectContainer.Descriptions[index].CAPTION = string.Empty;
          }
        }
        contextsObjectContainer.ClearCacheTables();
        return contextsObjectContainer;
      }

      /// <summary>
      /// Вспомогательный класс для сортировки контекстов в списке
      /// </summary>
      private class ContextsComparer : IComparer<long>
      {
        /// <summary>Владелец</summary>
        private EditingContextsObjectContainer owner;

        /// <summary>Создать экземпляр класса, связать его с владельцем</summary>
        /// <param name="owner">Владелец</param>
        public ContextsComparer(EditingContextsObjectContainer owner) => this.owner = owner;

        /// <summary>Сравнить два контекста</summary>
        /// <param name="x">Идентификатор версии первого контекста</param>
        /// <param name="y">Идентификатор версии второго контекста</param>
        /// <returns>-1, 0, 1</returns>
        public int Compare(long x, long y)
        {
          if (this.owner == null)
            return 0;
          ObjectVersionDescription description1 = this.owner.GetDescription(x);
          ObjectVersionDescription description2 = this.owner.GetDescription(y);
          if (description1 == null || description2 == null)
            return 0;
          int num = MetaDataHelper.GetObjectTypeName(description1.F_OBJECT_TYPE).CompareTo(MetaDataHelper.GetObjectTypeName(description2.F_OBJECT_TYPE));
          return num != 0 ? num : this.owner.GetCaption(x).CompareTo(this.owner.GetCaption(y));
        }
      }
    }
}
