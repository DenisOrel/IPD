
// Type: Intermech.Interfaces.MetaDataHelperService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Caches.Metadata;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using Intermech.Search.CompositionSelectionContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


namespace Intermech.Interfaces
{
    public class MetaDataHelperService : LongLifeObject, IMetaDataHelper, IMetaDataHelperCache
    {
      private static ApplicationServiceRef<MetaDataHelperService> instance = new ApplicationServiceRef<MetaDataHelperService>(true);
      /// <summary>Принудительное обновление содержимого кэша</summary>
      private bool _forced;
      /// <summary>
      /// Заблокировано ли обновление кэша (наивысший приоритет)
      /// </summary>
      private bool _locked;
      /// <summary>Дата и время последней модификации кэша метаданных</summary>
      private DateTime _syncDateTime = DateTime.MinValue;
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекциям, связанным с глобальным словариком
      /// </summary>
      private object _syncRootGlobals = new object();
      /// <summary>
      /// В данном словарике хранятся соответствия [Guid чего-то] =&gt; [IMSGlobals - информация о типе метаданных]
      /// </summary>
      private Dictionary<Guid, IMSGlobals> _globalsGuid = new Dictionary<Guid, IMSGlobals>();
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекциям, связанным с типами объектов
      /// </summary>
      private object _syncRootObjectTypes = new object();
      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных с типами объектов,
      /// типами связей по умолчанию для типов объектов
      /// </summary>
      private DateTime _lastObjectsSyncTime = DateTime.MinValue;
      /// <summary>
      /// В данном словарике хранятся соответствия [Guid типа объекта] =&gt; [ID типа объекта]
      /// </summary>
      private Dictionary<Guid, int> _objectsGuid2Id = new Dictionary<Guid, int>();
      /// <summary>
      /// В данном словарике хранятся краткие описания типов объектов
      /// [ID типа объекта] =&gt; [IMSObjectType - описание типа объекта]
      /// </summary>
      private Dictionary<int, IMSObjectType> _objectTypes = new Dictionary<int, IMSObjectType>();
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекциям, связанным с типами связей
      /// </summary>
      private object _syncRootRelationTypes = new object();
      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных с типами связей
      /// </summary>
      private DateTime _lastRelationsSyncTime = DateTime.MinValue;
      /// <summary>
      /// В данном словарике хранятся соответствия [Guid типа связи] =&gt; [ID типа связи]
      /// </summary>
      private Dictionary<Guid, int> _relationsGuid2Id = new Dictionary<Guid, int>();
      /// <summary>
      /// В данном словарике хранятся краткие описания типов связей
      /// [ID типа связи] =&gt; [IMSRelationType - краткое описание типа связи]
      /// </summary>
      private Dictionary<int, IMSRelationType> RelationTypes = new Dictionary<int, IMSRelationType>();
      /// <summary>
      /// В данном словарике хранятся допустимые типы связей для родительских типов объектов
      /// [ID родительского типа объекта] =&gt; [Список допустимых типов связей (applicabilities)]
      /// </summary>
      private Dictionary<int, List<IMSApplicability>> _applicabilities = new Dictionary<int, List<IMSApplicability>>();
      /// <summary>
      /// В данном словарике хранятся унаследованные допустимые типы связей для родительских типов объектов
      /// [ID родительского типа объекта] =&gt; [Список допустимых типов связей (applicabilities)]
      /// </summary>
      private Dictionary<int, List<IMSApplicability>> _inheritedApplicabilities = new Dictionary<int, List<IMSApplicability>>();
      /// <summary>
      /// В данном словарике даты и время синхронизации списков допустимых типов связей с кэшем метаданных
      /// [ID родительского типа объекта] =&gt; [Дата и время создания списка]
      /// </summary>
      private Dictionary<int, DateTime> _applicabilitiesSyncTime = new Dictionary<int, DateTime>();
      /// <summary>
      /// Кэш наличия применяемостей между различными типами объектов различными типами связей
      /// </summary>
      private readonly Dictionary<ApplicabilitiesKey, IMSApplicability> _applicabilitiesCache = new Dictionary<ApplicabilitiesKey, IMSApplicability>();
      /// <summary>
      /// Временный ключ (поскольку используется синхронизация, то нет смысла создавать множество экземпляров ключа)
      /// </summary>
      private readonly ApplicabilitiesKey _tempApplicabilitiesKey = new ApplicabilitiesKey(0, 0, 0);
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекциям, связанным с иерархией типов объектов
      /// </summary>
      private ReaderWriterLockSlim _syncRootObjectHierarchyTypes = new ReaderWriterLockSlim();
      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных с иерархией типов объектов
      /// </summary>
      private DateTime _lastObjectsHierarchySyncTime = DateTime.MinValue;
      /// <summary>
      ///  Иерархия типов объектов [ID дочернего типа объекта] = [ID родительского типа объекта].
      /// </summary>
      private Dictionary<int, int> _objectsHierarchy = new Dictionary<int, int>();
      /// <summary>
      ///  Иерархия типов объектов [ID родительского типа объекта] = [List[ID] дочерних типов объектов].
      /// </summary>
      private Dictionary<int, List<int>> _objectsHierarchyRev = new Dictionary<int, List<int>>();
      /// <summary>
      ///  Иерархия типов объектов [Guid дочернего типа объекта] = [Guid родительского типа объекта].
      /// </summary>
      private Dictionary<Guid, Guid> _objectsHierarchyGuids = new Dictionary<Guid, Guid>();
      /// <summary>
      ///  Иерархия типов объектов [Guid родительского типа объекта] = [List[Guid] дочерних типов объектов].
      /// </summary>
      private Dictionary<Guid, List<Guid>> _objectsHierarchyRevGuids = new Dictionary<Guid, List<Guid>>();
      /// <summary>Список идентификаторов типов объектов верхнего уровня</summary>
      private List<int> _topObjectTypes = new List<int>();
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекциям, связанным со специальными типами связей
      /// </summary>
      private object _syncRootSpecialRelationTypes = new object();
      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных со специальными типами связей
      /// </summary>
      private DateTime _lastSpecialRelationTypesSyncTime = DateTime.MinValue;
      /// <summary>
      /// Коллекция Int32-идентификаторов типов связей, в составе которых есть атрибуты, управляющие допустимыми заменами
      /// </summary>
      private List<int> _specialSubstitutesRelations = new List<int>();
      /// <summary>
      /// Коллекция Int32-идентификаторов типов связей, в составе которых есть атрибут "Сортировка"
      /// </summary>
      private List<int> _specialSortedRelations = new List<int>();
      /// <summary>
      /// Коллекция Int32-идентификаторов типов связей, которые используются для группирования объектов
      /// </summary>
      private List<int> _specialGroupingRelations = new List<int>();
      /// <summary>
      /// Список идентификаторов типов связей, составы которых можно конфигурировать.
      /// [ID типа связи] =&gt; [true - тип конфигурируемый]
      /// </summary>
      private Dictionary<int, bool> _specialConfigurableRelationTypes = new Dictionary<int, bool>();
      /// <summary>
      /// Список идентификаторов типов связей, составы которых можно конфигурировать.
      /// [ID типа связи] =&gt; [true - тип конфигурируемый]
      /// </summary>
      private Dictionary<int, bool> _specialPartiallyConfigurableRelationTypes = new Dictionary<int, bool>();
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекциям, связанным со специальными типами объектов
      /// </summary>
      private object _syncRootSpecialObjectTypes = new object();
      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных со специальными типами объектов
      /// </summary>
      private DateTime _lastSpecialObjectTypesSyncTime = DateTime.MinValue;
      /// <summary>
      /// Полный список Int32-идентификаторов всех типов объектов, которые могут быть связаны связями с заменителями
      /// </summary>
      private List<int> _specialSubstitutesObjectTypes = new List<int>();
      /// <summary>
      /// Полный список Int32-идентификаторов всех типов объектов, которые могут быть связаны связями с сортировкой
      /// </summary>
      private List<int> _specialSortedObjectTypes = new List<int>();
      /// <summary>
      /// Полный список Int32-идентификаторов всех типов объектов, которые могут быть связаны проектной связью
      /// </summary>
      private List<int> _specialDesignedObjectTypes = new List<int>();
      /// <summary>
      /// Полный список Int32-идентификаторов всех типов объектов, которые являются группирующими
      /// </summary>
      private List<int> _specialGroupingObjectTypes = new List<int>();
      /// <summary>
      /// Полный список Int32-идентификаторов всех типов объектов, у которых есть атрибут "Номер группы изменений"
      /// </summary>
      private List<int> _specialGrouppedObjectTypes = new List<int>();
      /// <summary>
      /// Полный список Int32-идентификаторов всех типов объектов, у которых есть атрибут "Видимость объекта"
      /// </summary>
      private List<int> _specialVisibilityObjectTypes = new List<int>();
      /// <summary>
      /// Список типов объектов, которые можно считать контекстами редактирования, верхний уровень (без наследования)
      /// </summary>
      private List<int> _specialTopContextObjectTypes = new List<int>();
      /// <summary>
      /// Список идентификаторов типов объектов, составы которых можно конфигурировать.
      /// [ID типа объекта] =&gt; [true - тип конфигурируемый]
      /// </summary>
      private Dictionary<int, bool> _specialConfigurableObjectTypes = new Dictionary<int, bool>();
      /// <summary>
      /// Список идентификаторов типов объектов, которые могут выступать в роли контекстов конфигураторов составов
      /// [ID типа объекта] =&gt; [true - тип может выступать в роли контекста конфигуратора составов]
      /// </summary>
      private Dictionary<int, bool> _specialContextableObjectTypes = new Dictionary<int, bool>();
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекции идентификаторов связей и их типов
      /// </summary>
      private object _syncRootRelationsPrjLinkTypes = new object();
      /// <summary>
      /// Коллекция позволяет хранить список идентификаторов связей и их типов
      /// [Int64 идентификатор связи (F_PRJLINK_ID)] = [Int32 тип этой связи]
      /// </summary>
      private Dictionary<long, int> _relationsPrjLinkTypes = new Dictionary<long, int>();
      /// <summary>Количество "попаданий" в кэш</summary>
      private long _counterRelationsPrjLinkTypesHit;
      /// <summary>Количество "промахов" в кэше</summary>
      private long _counterRelationsPrjLinkTypesMiss;
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекциям, связанным с типами атрибутов
      /// </summary>
      private object _syncRootAttrTypes = new object();
      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных с типами атрибутов
      /// </summary>
      private DateTime _lastAttrsSyncTime = DateTime.MinValue;
      /// <summary>
      /// В данном словарике хранятся соответствия [Guid типа атрибута] =&gt; [ID типа атрибута]
      /// </summary>
      private Dictionary<Guid, int> _attrsGuid2Id = new Dictionary<Guid, int>();
      /// <summary>
      /// В данном словарике хранятся краткие описания типов атрибутов для типов объектов
      /// [ID типа объекта] =&gt; [IMSAttribute4ObjectType - описание типа атрибута для типа объекта]
      /// </summary>
      private Dictionary<int, List<IMSAttribute4ObjectType>> _attr4ObjectTypes = new Dictionary<int, List<IMSAttribute4ObjectType>>();
      /// <summary>
      /// В данном словарике хранятся краткие описания типов атрибутов для типов объектов
      /// [ID типа атрибута] =&gt; [IMSAttribute4ObjectType - описания типа атрибута для всех типов объектов]
      /// </summary>
      private Dictionary<int, List<IMSAttribute4ObjectType>> _attrs4ObjectTypes = new Dictionary<int, List<IMSAttribute4ObjectType>>();
      /// <summary>
      /// В данном словарике хранятся краткие описания типов атрибутов для типов связей
      /// [ID типа связи] =&gt; [IMSAttribute4RelationType - описание типа атрибута для типа связи]
      /// </summary>
      private Dictionary<int, List<IMSAttribute4RelationType>> _attr4RelationTypes = new Dictionary<int, List<IMSAttribute4RelationType>>();
      /// <summary>
      /// В данном словарике хранятся краткие описания типов атрибутов для типов связей
      /// [ID типа атрибута] =&gt; [IMSAttribute4RelationType - описания типов атрибута для всех типов связей]
      /// </summary>
      private Dictionary<int, List<IMSAttribute4RelationType>> _attrs4RelationTypes = new Dictionary<int, List<IMSAttribute4RelationType>>();
      /// <summary>Список ссылочных типов атрибутов</summary>
      private List<IMSAttributeType> _linkAttributeTypesList = new List<IMSAttributeType>();
      /// <summary>
      /// В данном словарике хранятся ссылочные типы атрибутов и списки типов объектов, на которые ссылаются данные атрибуты
      /// [ID типа атрибута] =&gt; [Список ID типов объектов, на которые ссылается указанный атрибут]
      /// </summary>
      private Dictionary<int, List<int>> _linkAttributeTypes = new Dictionary<int, List<int>>();
      /// <summary>
      /// В данном словарике хранятся списки типов объектов, для которых сформированы списки ссылочных атрибутов,
      /// которые могут ссылаться на указанные типы объектов
      /// [ID типа объекта] =&gt; [Список ID типов ссылочных атрибутов, которые могут ссылаться на указанный тип объекта]
      /// </summary>
      private Dictionary<int, List<int>> _linkAttributeTypesRev = new Dictionary<int, List<int>>();
      /// <summary>
      /// В данном словарике хранятся соответствия [Guid группы атрибутов] =&gt; [ID группы атрибутов]
      /// </summary>
      private Dictionary<Guid, int> _attrGroupsGuid2Id = new Dictionary<Guid, int>();
      /// <summary>
      /// Объект для потокобезопасного доступа к коллекциям, связанным с шагами ЖЦ, уровнями продвижения, схемами ЖЦ
      /// </summary>
      private object _syncRootLcSteps = new object();
      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных с шагами ЖЦ
      /// </summary>
      private DateTime _lastLcStepsSyncTime = DateTime.MinValue;
      /// <summary>
      /// В данном словарике хранятся краткие описания схем жизненного цикла
      /// [ID схемы ЖЦ] =&gt; [IMSLifeCycleScheme - описание схемы ЖЦ]
      /// </summary>
      private Dictionary<int, IMSLifeCycleScheme> _lcSchemes = new Dictionary<int, IMSLifeCycleScheme>();
      /// <summary>
      /// В данном словарике хранятся краткие описания уровней продвижения
      /// [ID уровня продвижения] =&gt; [IMSLifeCycleLevel - описание уровня продвижения]
      /// </summary>
      private Dictionary<int, IMSLifeCycleLevel> _lcLevels = new Dictionary<int, IMSLifeCycleLevel>();
      /// <summary>
      /// В данном словарике хранятся краткие описания шагов жизненного цикла
      /// [ID шага ЖЦ] =&gt; [IMSLifeCycleStep - описание шага ЖЦ]
      /// </summary>
      private Dictionary<int, IMSLifeCycleStep> _lcSteps = new Dictionary<int, IMSLifeCycleStep>();
      /// <summary>
      /// В данном словарике хранятся соответствия [Guid схемы ЖЦ] =&gt; [ID схемы ЖЦ]
      /// </summary>
      private Dictionary<Guid, int> _lcSchemesGuid2Id = new Dictionary<Guid, int>();
      /// <summary>
      /// В данном словарике хранятся соответствия [Guid уровня продвижения] =&gt; [ID уровня продвижения]
      /// </summary>
      private Dictionary<Guid, int> _lcLevelsGuid2Id = new Dictionary<Guid, int>();
      /// <summary>
      /// В данном словарике хранятся соответствия [Guid шага ЖЦ] =&gt; [ID шага ЖЦ]
      /// </summary>
      private Dictionary<Guid, int> _lcStepsGuid2Id = new Dictionary<Guid, int>();
      /// <summary>Идентификатор типа объектов "Производственные заказы"</summary>
      private int _objtypeProdOrders = -1;

      public static MetaDataHelperService Instance
      {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return MetaDataHelperService.instance.Value;
        }
      }

      public MetaDataHelperService()
      {
        this.SyncRoot = new object();
        this.SyncDelta = new TimeSpan(0, 1, 0);
        this.MetaDataGenerationModule = "Kernel";
        this.MetaDataGenerationSection = "MetaData";
        this.MetaDataGenerationKey = "MetaDataGeneration";
        this.AttrTypes = new Dictionary<int, IMSAttributeType>();
        this.AttrNameTypes = new Dictionary<string, int>();
        this.AttrGroups = new Dictionary<int, IMSAttributeGroup>();
        this.AttrInGroups = new Dictionary<int, List<int>>();
        this.AttrsApplicability = new Dictionary<int, IMSAttributeTypeApplicability>();
      }

      /// <summary>
      /// Событие генерируется после того, как перечитывается кэш
      /// </summary>
      public event MetaDataHelperEventHandler OnCacheReloaded;

      /// <summary>
      /// Объект для потокобезопасного доступа к словарикам класса (если есть необходимость изменить их значение)
      /// </summary>
      public object SyncRoot { get; set; }

      /// <summary>
      /// Если с момента последнего обращения к методу SyncMetadata прошло меньше указанного
      /// периода, то обращений к серверу приложений не будет вообще - синхронизация прерывается сразу
      /// </summary>
      public TimeSpan SyncDelta { get; set; }

      /// <summary>
      /// Имя модуля, для которого хранится значение счетчика MetaDataGenerationName
      /// </summary>
      public string MetaDataGenerationModule { get; }

      /// <summary>
      /// Имя модуля, для которого хранится значение счетчика MetaDataGenerationName
      /// </summary>
      public string MetaDataGenerationSection { get; }

      /// <summary>
      /// Счетчик, указывающий на поколение метаданных, записанных в СУБД.
      /// Данное значение хранится в системной конфигурации IPS
      /// </summary>
      public string MetaDataGenerationKey { get; }

      /// <summary>Дата и время последней модификации кэша метаданных</summary>
      public DateTime SyncDateTime => this._syncDateTime;

      /// <summary>Принудительное обновление содержимого кэша</summary>
      public bool Forced
      {
        [DebuggerStepThrough] get
        {
          lock (this.SyncRoot)
            return this._forced;
        }
        set
        {
          lock (this.SyncRoot)
            this._forced = value;
        }
      }

      /// <summary>
      /// Заблокировано ли обновление кэша (наивысший приоритет)
      /// </summary>
      public bool Locked
      {
        [DebuggerStepThrough] get
        {
          lock (this.SyncRoot)
            return this._locked;
        }
        set
        {
          lock (this.SyncRoot)
            this._locked = value;
        }
      }

      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных с типами объектов,
      /// типами связей по умолчанию для типов объектов
      /// </summary>
      public DateTime LastObjectsSyncTime => this._lastObjectsSyncTime;

      /// <summary>
      /// Полный список типов объектов, которые можно считать контекстами редактирования
      /// </summary>
      public List<int> SpecialContextObjectTypes { get; set; } = new List<int>();

      /// <summary>
      /// В данном словарике хранятся краткие описания типов атрибутов
      /// [ID типа атрибута] =&gt; [IMSAttributeType - описание типа атрибута]
      /// </summary>
      internal Dictionary<int, IMSAttributeType> AttrTypes { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; set; }

      /// <summary>
      /// В данном словарике хранятся соответствия имен атрибутов их идентификаторам
      /// [Имя типа атрибута] =&gt; [Int32 идентификатор типа атрибута]
      /// </summary>
      internal Dictionary<string, int> AttrNameTypes { get; set; }

      /// <summary>
      /// В данном словарике хранятся краткие описания типов атрибутов
      /// [ID типа атрибута] =&gt; [IMSAttributeType - описание типа атрибута]
      /// </summary>
      internal Dictionary<int, IMSAttributeGroup> AttrGroups { get; set; }

      /// <summary>
      ///  Принадлежность атрибутов группам [ID группы атрибутов] = [Список ID типов атрибутов].
      /// </summary>
      internal Dictionary<int, List<int>> AttrInGroups { get; set; }

      /// <summary>
      /// Принадлежность атрибутов типам объектов/связей.
      /// Атрибуты, которые не применяются нигде, в данном словаре отсутствуют.
      /// [ID типа атрибута] = [Применяемость]
      /// </summary>
      internal Dictionary<int, IMSAttributeTypeApplicability> AttrsApplicability { get; set; }

      /// <summary>
      /// Выполнить полную синхронизацию всех внутренних коллекций с кэшем метаданных
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncMetadata(DataSet cacheTables) => this.SyncMetadata(cacheTables, false);

      /// <summary>
      /// Выполнить полную синхронизацию всех внутренних коллекций с кэшем метаданных
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      /// <param name="forced">true - принудительно синхронизировать</param>
      public void SyncMetadata(DataSet cacheTables, bool forced)
      {
        if (this.Locked)
          return;
        if (forced)
        {
          lock (this.SyncRoot)
          {
            this._lastObjectsSyncTime = DateTime.MinValue;
            this._forced = true;
          }
        }
        if (DateTime.UtcNow - this._lastObjectsSyncTime < this.SyncDelta)
          return;
        try
        {
          this.SyncObjectTypesMetadata(cacheTables);
          this.SyncObjectTypesHierarchy(cacheTables);
          this.SyncRelationTypesMetadata(cacheTables);
          this.SyncAttrTypesMetadata(cacheTables);
          this.SyncSpecialRelationTypes(cacheTables);
          this.SyncSpecialObjectTypes(cacheTables);
          this.SyncLCStepsMetadata(cacheTables);
          this.SyncGlobals(cacheTables);
        }
        finally
        {
          lock (this.SyncRoot)
          {
            try
            {
              this.Locked = true;
              if (this.OnCacheReloaded != null)
                this.OnCacheReloaded((object) null, EventArgs.Empty);
            }
            finally
            {
              this.Locked = false;
            }
            this._syncDateTime = DateTime.UtcNow;
            this._forced = false;
          }
        }
      }

      /// <summary>
      /// Указать, что содержимое кэша в целом изменилось, сбросить содержимое флажка Forced
      /// </summary>
      public void Touch()
      {
        lock (this.SyncRoot)
        {
          this._syncDateTime = DateTime.UtcNow;
          this._forced = false;
        }
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных с типами объектов, с кэшем метаданных
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncObjectTypesMetadata(DataSet cacheTables)
      {
        if (this.Locked || DateTime.UtcNow - this._lastObjectsSyncTime < this.SyncDelta && !this._forced || cacheTables == null)
          return;
        lock (this._syncRootObjectTypes)
        {
          this._objectsGuid2Id.Clear();
          this._objectTypes.Clear();
          DataTable table = cacheTables.Tables["IMS_OBJECT_TYPES"];
          if (table != null)
          {
            lock (table)
            {
              for (int index = 0; index < table.Rows.Count; ++index)
              {
                try
                {
                  DataRow row = table.Rows[index];
                  int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
                  Guid key = new Guid(row["F_GUID"].ToString());
                  IMSObjectType imsObjectType = new IMSObjectType();
                  imsObjectType.Load(row);
                  imsObjectType.Freeze();
                  this._objectsGuid2Id[key] = int32;
                  this._objectTypes[int32] = imsObjectType;
                }
                catch
                {
                }
              }
            }
          }
          this._lastObjectsSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных с иерархией типов объектов.
      /// Внимание! Перед вызовом этого метода должен быть вызван метод SyncObjectTypesMetadata,
      /// который корректно заполняет коллекции [Guid типа объекта] = [ID типа объекта]
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncObjectTypesHierarchy(DataSet cacheTables)
      {
        if (this.Locked || DateTime.UtcNow - this._lastObjectsHierarchySyncTime < this.SyncDelta && !this._forced || cacheTables == null)
          return;
        this._syncRootObjectHierarchyTypes.EnterWriteLock();
        try
        {
          this._objectsHierarchy.Clear();
          this._objectsHierarchyGuids.Clear();
          this._objectsHierarchyRev.Clear();
          this._objectsHierarchyRevGuids.Clear();
          this._topObjectTypes.Clear();
          DataTable table = cacheTables.Tables["IMS_OBJTYPES_TREE"];
          if (table != null)
          {
            lock (table)
            {
              for (int index = 0; index < table.Rows.Count; ++index)
              {
                try
                {
                  DataRow row = table.Rows[index];
                  int int32_1 = Convert.ToInt32(row["F_PARENT_ID"]);
                  Guid guid1 = this._objectTypes[int32_1].Guid;
                  int int32_2 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
                  Guid guid2 = this._objectTypes[int32_2].Guid;
                  this._objectsHierarchy[int32_2] = int32_1;
                  this._objectsHierarchyGuids[guid2] = guid1;
                  List<int> intList;
                  this._objectsHierarchyRev.TryGetValue(int32_1, out intList);
                  if (intList == null)
                  {
                    intList = new List<int>();
                    this._objectsHierarchyRev[int32_1] = intList;
                  }
                  intList.Add(int32_2);
                  List<Guid> guidList;
                  this._objectsHierarchyRevGuids.TryGetValue(guid1, out guidList);
                  if (guidList == null)
                  {
                    guidList = new List<Guid>();
                    this._objectsHierarchyRevGuids[guid1] = guidList;
                  }
                  guidList.Add(guid2);
                }
                catch
                {
                }
              }
            }
          }
          new List<int>((IEnumerable<int>) this._objectTypes.Keys).ForEach((Action<int>) (type =>
          {
            if (this._objectsHierarchy.ContainsKey(type))
              return;
            this._topObjectTypes.Add(type);
          }));
          this._lastObjectsHierarchySyncTime = DateTime.UtcNow;
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitWriteLock();
        }
      }

      /// <summary>
      /// Получить унаследованные применяемости у родительских типов объектов (вверх по иерархии)
      /// </summary>
      /// <param name="childObjType">Дочерний тип объекта, для которого собираются унаследованные применяемости</param>
      /// <returns>Унаследованные применяемости у родительских типов объектов (вверх по иерархии)</returns>
      private List<IMSApplicability> GetParentApplicabilities(int childObjType)
      {
        List<IMSApplicability> parentApplicabilities = new List<IMSApplicability>(10);
        List<int> objectTypeParentsId = this.GetObjectTypeParentsID(childObjType);
        for (int index1 = 0; index1 < objectTypeParentsId.Count; ++index1)
        {
          List<IMSApplicability> collection;
          lock (this._syncRootRelationTypes)
            this._applicabilities.TryGetValue(objectTypeParentsId[index1], out collection);
          if (collection != null)
          {
            for (int index2 = collection.Count - 1; index2 >= 0; --index2)
            {
              if (collection[index2].Public == InheritModes.Inherited)
                collection.RemoveAt(index2);
            }
            parentApplicabilities.AddRange((IEnumerable<IMSApplicability>) collection);
          }
        }
        return parentApplicabilities;
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных с типами связей, с кэшем метаданных.
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncRelationTypesMetadata(DataSet cacheTables)
      {
        if (this.Locked || DateTime.UtcNow - this._lastRelationsSyncTime < this.SyncDelta && !this._forced || cacheTables == null)
          return;
        lock (this._syncRootRelationTypes)
        {
          this._relationsGuid2Id.Clear();
          this.RelationTypes.Clear();
          this._applicabilities.Clear();
          this._inheritedApplicabilities.Clear();
          this._applicabilitiesSyncTime.Clear();
          this._applicabilitiesCache.Clear();
          DataTable table1 = cacheTables.Tables["IMS_RELATION_TYPES"];
          if (table1 != null)
          {
            lock (table1)
            {
              for (int index = 0; index < table1.Rows.Count; ++index)
              {
                try
                {
                  DataRow row = table1.Rows[index];
                  int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
                  Guid key = new Guid(row["F_GUID"].ToString());
                  IMSRelationType imsRelationType = new IMSRelationType();
                  imsRelationType.Load(row);
                  imsRelationType.Freeze();
                  this.RelationTypes[int32] = imsRelationType;
                  this._relationsGuid2Id[key] = int32;
                }
                catch
                {
                }
              }
            }
          }
          DataTable table2 = cacheTables.Tables["IMS_TYPES_APPLICABILITY"];
          if (table2 != null)
          {
            int count = table2.Rows.Count;
            lock (table2)
            {
              for (int index = 0; index < count; ++index)
              {
                DataRow row = table2.Rows[index];
                int int32 = Convert.ToInt32(row["F_INOBJECT_TYPE"]);
                List<IMSApplicability> imsApplicabilityList;
                this._applicabilities.TryGetValue(int32, out imsApplicabilityList);
                if (imsApplicabilityList == null)
                {
                  imsApplicabilityList = new List<IMSApplicability>();
                  this._applicabilities[int32] = imsApplicabilityList;
                }
                IMSApplicability imsApplicability = new IMSApplicability();
                imsApplicability.Load(row);
                imsApplicability.Freeze();
                if (!imsApplicabilityList.Contains(imsApplicability))
                  imsApplicabilityList.Add(imsApplicability);
                this._applicabilitiesSyncTime[int32] = DateTime.UtcNow;
              }
            }
          }
          lock (this._syncRootObjectTypes)
          {
            int[] array = new int[this._objectTypes.Keys.Count];
            this._objectTypes.Keys.CopyTo(array, 0);
            for (int index = 0; index < array.Length; ++index)
              this._inheritedApplicabilities[array[index]] = this.GetParentApplicabilities(array[index]);
          }
          this._lastRelationsSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных со специальными типами объектов.
      /// Внимание! Перед вызовом этого метода должны быть вызваны методы SyncObjectTypesMetadata,
      /// SyncSpecialRelationTypes
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncSpecialObjectTypes(DataSet cacheTables)
      {
        if (this.Locked || DateTime.UtcNow - this._lastSpecialObjectTypesSyncTime < this.SyncDelta && !this._forced || cacheTables == null)
          return;
        lock (this._syncRootSpecialObjectTypes)
        {
          this._specialDesignedObjectTypes.Clear();
          this._specialSortedObjectTypes.Clear();
          this._specialSubstitutesObjectTypes.Clear();
          this._specialGroupingObjectTypes.Clear();
          this._specialGrouppedObjectTypes.Clear();
          this._specialVisibilityObjectTypes.Clear();
          this.SpecialContextObjectTypes.Clear();
          this._specialTopContextObjectTypes.Clear();
          this._specialConfigurableObjectTypes.Clear();
          this._specialContextableObjectTypes.Clear();
          int[] array;
          lock (this._syncRootObjectTypes)
          {
            array = new int[this._objectTypes.Keys.Count];
            this._objectTypes.Keys.CopyTo(array, 0);
          }
          int attributeTypeId1 = this.GetAttributeTypeID("cad00697-306c-11d8-b4e9-00304f19f545");
          int attributeTypeId2 = this.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545");
          Guid parentType1 = new Guid("cad0146b-306c-11d8-b4e9-00304f19f545");
          Guid parentType2 = new Guid("cad00348-306c-11d8-b4e9-00304f19f545");
          Guid parentType3 = new Guid("cadd92e9-306c-11d8-b4e9-00304f19f545");
          for (int index1 = 0; index1 < array.Length; ++index1)
          {
            IMSObjectType objectType = this.GetObjectType(array[index1]);
            if (objectType != null)
            {
              if (this.CanAddObjTypeToEditingContext(objectType.ObjectTypeID, false))
                this._specialGrouppedObjectTypes.Add(objectType.ObjectTypeID);
              List<IMSAttribute4ObjectType> attribute4ObjectTypeList = this.GetAttribute4ObjectTypeList(array[index1]);
              List<int> intList = new List<int>(attribute4ObjectTypeList.Count);
              for (int index2 = 0; index2 < attribute4ObjectTypeList.Count; ++index2)
                intList.Add(attribute4ObjectTypeList[index2].AttributeID);
              if (intList.IndexOf(attributeTypeId2) >= 0)
                this._specialVisibilityObjectTypes.Add(array[index1]);
              if (this.IsObjectTypeChildOf(objectType.Guid, parentType1) || this.IsObjectTypeChildOf(objectType.Guid, parentType2) || this.IsObjectTypeChildOf(objectType.Guid, parentType3))
                this.SpecialContextObjectTypes.Add(objectType.ObjectTypeID);
              List<IMSApplicability> typeApplicabilities = this.GetObjectTypeApplicabilities(array[index1]);
              for (int index3 = 0; index3 < typeApplicabilities.Count; ++index3)
              {
                if (this.HasRelationTypeSubstitutes(typeApplicabilities[index3].RelationTypeID))
                {
                  this._specialSubstitutesObjectTypes.Add(array[index1]);
                  break;
                }
              }
              for (int index4 = 0; index4 < typeApplicabilities.Count; ++index4)
              {
                if (this.HasRelationTypeSorting(typeApplicabilities[index4].RelationTypeID))
                {
                  this._specialSortedObjectTypes.Add(array[index1]);
                  break;
                }
              }
              for (int index5 = 0; index5 < typeApplicabilities.Count; ++index5)
              {
                if (this.GetRelationTypeGuid(typeApplicabilities[index5].RelationTypeID).ToString() == "cad00023-306c-11d8-b4e9-00304f19f545")
                {
                  this._specialDesignedObjectTypes.Add(array[index1]);
                  break;
                }
              }
              for (int index6 = 0; index6 < typeApplicabilities.Count; ++index6)
              {
                if (this.HasRelationTypeGrouping(typeApplicabilities[index6].RelationTypeID))
                {
                  if (intList.IndexOf(attributeTypeId1) >= 0)
                  {
                    this._specialGroupingObjectTypes.Add(array[index1]);
                    break;
                  }
                  break;
                }
              }
            }
          }
          for (int index7 = 0; index7 < this.SpecialContextObjectTypes.Count; ++index7)
          {
            int contextObjectType = this.SpecialContextObjectTypes[index7];
            bool flag = false;
            for (int index8 = 0; index8 < this.SpecialContextObjectTypes.Count; ++index8)
            {
              if (contextObjectType != this.SpecialContextObjectTypes[index8] && this.IsObjectTypeChildOf(contextObjectType, this.SpecialContextObjectTypes[index8]))
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              this._specialTopContextObjectTypes.Add(contextObjectType);
          }
          this._lastSpecialObjectTypesSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных со специальными типами связей.
      /// Внимание! Перед вызовом этого метода должен быть вызван метод SyncRelationTypesMetadata,
      /// который корректно заполняет коллекции [Guid типа связи] = [ID типа связи]
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncSpecialRelationTypes(DataSet cacheTables)
      {
        if (this.Locked || DateTime.UtcNow - this._lastSpecialRelationTypesSyncTime < this.SyncDelta && !this._forced || cacheTables == null)
          return;
        lock (this._syncRootSpecialRelationTypes)
        {
          this._specialSortedRelations.Clear();
          this._specialSubstitutesRelations.Clear();
          this._specialGroupingRelations.Clear();
          this._specialConfigurableRelationTypes.Clear();
          this._specialPartiallyConfigurableRelationTypes.Clear();
          lock (this._syncRootRelationTypes)
          {
            int[] array = new int[this.RelationTypes.Keys.Count];
            this.RelationTypes.Keys.CopyTo(array, 0);
            int attributeTypeId1 = this.GetAttributeTypeID("cad001c0-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId2 = this.GetAttributeTypeID("cad001c1-306c-11d8-b4e9-00304f19f545");
            this.GetAttributeTypeID("cad00274-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId3 = this.GetAttributeTypeID("cad00817-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId4 = this.GetAttributeTypeID("cad00818-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId5 = this.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId6 = this.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545");
            for (int index1 = 0; index1 < array.Length; ++index1)
            {
              int relTypeID = array[index1];
              if (this.GetRelationType(relTypeID) != null)
              {
                List<IMSAttribute4RelationType> relationTypeList = this.GetAttribute4RelationTypeList(relTypeID);
                List<int> intList = new List<int>(relationTypeList.Count);
                for (int index2 = 0; index2 < relationTypeList.Count; ++index2)
                  intList.Add(relationTypeList[index2].AttributeID);
                if (intList.IndexOf(attributeTypeId1) >= 0 && intList.IndexOf(attributeTypeId2) >= 0 && intList.IndexOf(attributeTypeId3) >= 0 && intList.IndexOf(attributeTypeId4) >= 0)
                  this._specialSubstitutesRelations.Add(relTypeID);
                if (intList.IndexOf(attributeTypeId5) >= 0)
                  this._specialSortedRelations.Add(relTypeID);
                if (intList.IndexOf(attributeTypeId5) >= 0 && intList.IndexOf(attributeTypeId6) >= 0)
                  this._specialGroupingRelations.Add(relTypeID);
              }
            }
          }
          this._lastSpecialRelationTypesSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных с типами атрибутов, с кэшем метаданных
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncAttrTypesMetadata(DataSet cacheTables)
      {
        if (this.Locked || DateTime.UtcNow - this._lastAttrsSyncTime < this.SyncDelta && !this._forced || cacheTables == null)
          return;
        lock (this._syncRootAttrTypes)
        {
          this._attrsGuid2Id.Clear();
          this.AttrTypes.Clear();
          this.AttrNameTypes.Clear();
          this._attr4ObjectTypes.Clear();
          this._attrs4ObjectTypes.Clear();
          this._attr4RelationTypes.Clear();
          this._attrs4RelationTypes.Clear();
          this._linkAttributeTypesList.Clear();
          this._linkAttributeTypes.Clear();
          this._linkAttributeTypesRev.Clear();
          this._attrGroupsGuid2Id.Clear();
          this.AttrGroups.Clear();
          this.AttrInGroups.Clear();
          this.AttrsApplicability.Clear();
          DataTable table1 = cacheTables.Tables["IMS_ATTRIBUTES"];
          DataTable table2 = cacheTables.Tables["IMS_ATTR4OBJ_TYPES"];
          DataTable table3 = cacheTables.Tables["IMS_ATTR4RELATION_TYPES"];
          DataTable table4 = cacheTables.Tables["IMS_POSSIBLE_VALUES"];
          DataTable table5 = cacheTables.Tables["IMS_MD_EXTENSIONS"];
          DataTable table6 = cacheTables.Tables["IMS_ATTR_GROUPS"];
          DataTable table7 = cacheTables.Tables["IMS_ATTR_IN_GROUPS"];
          if (table1 != null)
          {
            lock (table1)
            {
              for (int index = 0; index < table1.Rows.Count; ++index)
              {
                try
                {
                  IMSAttributeType imsAttributeType = new IMSAttributeType();
                  imsAttributeType.Load(table1.Rows[index]);
                  imsAttributeType.Freeze();
                  this._attrsGuid2Id[imsAttributeType.AttributeGuid] = imsAttributeType.AttributeID;
                  this.AttrTypes[imsAttributeType.AttributeID] = imsAttributeType;
                  this.AttrNameTypes[imsAttributeType.Name.ToUpperInvariant()] = imsAttributeType.AttributeID;
                  if (imsAttributeType.FieldType != FieldTypes.ftObjectLink)
                  {
                    if (imsAttributeType.FieldType != FieldTypes.ftObjectLinkByID)
                      continue;
                  }
                  this._linkAttributeTypesList.Add(imsAttributeType);
                }
                catch
                {
                }
              }
            }
          }
          if (table2 != null)
          {
            lock (table2)
            {
              for (int index = 0; index < table2.Rows.Count; ++index)
              {
                try
                {
                  IMSAttribute4ObjectType attribute4ObjectType = new IMSAttribute4ObjectType();
                  attribute4ObjectType.Load(table2.Rows[index]);
                  attribute4ObjectType.Freeze();
                  if (!this._attr4ObjectTypes.ContainsKey(attribute4ObjectType.ObjectTypeID))
                    this._attr4ObjectTypes[attribute4ObjectType.ObjectTypeID] = new List<IMSAttribute4ObjectType>();
                  if (!this._attrs4ObjectTypes.ContainsKey(attribute4ObjectType.AttributeID))
                    this._attrs4ObjectTypes[attribute4ObjectType.AttributeID] = new List<IMSAttribute4ObjectType>();
                  this._attr4ObjectTypes[attribute4ObjectType.ObjectTypeID].Add(attribute4ObjectType);
                  this._attrs4ObjectTypes[attribute4ObjectType.AttributeID].Add(attribute4ObjectType);
                  if (!this.AttrsApplicability.ContainsKey(attribute4ObjectType.AttributeID))
                    this.AttrsApplicability[attribute4ObjectType.AttributeID] = IMSAttributeTypeApplicability.ObjectType;
                  else
                    this.AttrsApplicability[attribute4ObjectType.AttributeID] = this.AttrsApplicability[attribute4ObjectType.AttributeID] | IMSAttributeTypeApplicability.ObjectType;
                }
                catch
                {
                }
              }
            }
          }
          if (table3 != null)
          {
            lock (table3)
            {
              for (int index = 0; index < table3.Rows.Count; ++index)
              {
                try
                {
                  IMSAttribute4RelationType attribute4RelationType = new IMSAttribute4RelationType();
                  attribute4RelationType.Load(table3.Rows[index]);
                  attribute4RelationType.Freeze();
                  if (!this._attr4RelationTypes.ContainsKey(attribute4RelationType.RelationTypeID))
                    this._attr4RelationTypes[attribute4RelationType.RelationTypeID] = new List<IMSAttribute4RelationType>();
                  if (!this._attrs4RelationTypes.ContainsKey(attribute4RelationType.AttributeID))
                    this._attrs4RelationTypes[attribute4RelationType.AttributeID] = new List<IMSAttribute4RelationType>();
                  this._attr4RelationTypes[attribute4RelationType.RelationTypeID].Add(attribute4RelationType);
                  this._attrs4RelationTypes[attribute4RelationType.AttributeID].Add(attribute4RelationType);
                  if (!this.AttrsApplicability.ContainsKey(attribute4RelationType.AttributeID))
                    this.AttrsApplicability[attribute4RelationType.AttributeID] = IMSAttributeTypeApplicability.RelationType;
                  else
                    this.AttrsApplicability[attribute4RelationType.AttributeID] = this.AttrsApplicability[attribute4RelationType.AttributeID] | IMSAttributeTypeApplicability.RelationType;
                }
                catch
                {
                }
              }
            }
          }
          if (table4 != null && table4.Rows.Count > 0)
          {
            lock (table4)
            {
              foreach (KeyValuePair<int, IMSAttributePossibleValues> keyValuePair in IMSAttributePossibleValues.LoadFromDataTable(table4))
              {
                IMSAttributeType imsAttributeType1;
                this.AttrTypes.TryGetValue(keyValuePair.Value.F_ATTRIBUTE_ID, out imsAttributeType1);
                if (imsAttributeType1 != null)
                {
                  IMSAttributeType imsAttributeType2 = imsAttributeType1.Clone();
                  string fieldName = imsAttributeType2.ValueFieldName;
                  if (imsAttributeType2.FieldType == FieldTypes.ftMeasured)
                    fieldName = "F_STRING_VALUE";
                  imsAttributeType2.PossibleValues = keyValuePair.Value[fieldName];
                  imsAttributeType2.PossibleValuesDescriptions = keyValuePair.Value.Descriptions;
                  imsAttributeType2.Freeze();
                  this.AttrTypes[keyValuePair.Value.F_ATTRIBUTE_ID] = imsAttributeType2;
                  if (imsAttributeType2.FieldType == FieldTypes.ftObjectLink || imsAttributeType2.FieldType == FieldTypes.ftObjectLinkByID)
                  {
                    int index = this._linkAttributeTypesList.IndexOf(imsAttributeType1);
                    if (index >= 0)
                      this._linkAttributeTypesList[index] = imsAttributeType2;
                  }
                }
              }
            }
          }
          if (table5 != null && this._linkAttributeTypesList.Count > 0)
          {
            lock (table5)
            {
              for (int index1 = 0; index1 < this._linkAttributeTypesList.Count; ++index1)
              {
                IMSAttributeType linkAttributeTypes = this._linkAttributeTypesList[index1];
                List<int> intList1 = new List<int>();
                if (linkAttributeTypes.SizeType > 0L)
                {
                  intList1.Add(Convert.ToInt32(linkAttributeTypes.SizeType));
                }
                else
                {
                  int[] mdValuesInt = MetadataExtensions.GetMDValuesInt(table5, "OBJ_LINKS_ID", linkAttributeTypes.AttributeID, -1, -1);
                  if (mdValuesInt.Length != 0)
                    intList1.AddRange((IEnumerable<int>) mdValuesInt);
                  else if (intList1.IndexOf(-1) < 0)
                    intList1.Add(-1);
                }
                this._linkAttributeTypes[linkAttributeTypes.AttributeID] = intList1;
                for (int index2 = 0; index2 < intList1.Count; ++index2)
                {
                  if (!this._linkAttributeTypesRev.ContainsKey(intList1[index2]))
                    this._linkAttributeTypesRev.Add(intList1[index2], new List<int>());
                  List<int> intList2 = this._linkAttributeTypesRev[intList1[index2]];
                  if (intList2.IndexOf(linkAttributeTypes.AttributeID) < 0)
                    intList2.Add(linkAttributeTypes.AttributeID);
                }
              }
            }
          }
          if (table6 != null)
          {
            lock (table6)
            {
              for (int index = 0; index < table6.Rows.Count; ++index)
              {
                try
                {
                  IMSAttributeGroup imsAttributeGroup = new IMSAttributeGroup();
                  imsAttributeGroup.Load(table6.Rows[index]);
                  imsAttributeGroup.Freeze();
                  this._attrGroupsGuid2Id.Add(imsAttributeGroup.Guid, imsAttributeGroup.ID);
                  this.AttrGroups.Add(imsAttributeGroup.ID, imsAttributeGroup);
                }
                catch
                {
                }
              }
            }
          }
          if (table7 != null)
          {
            lock (table7)
            {
              for (int index = 0; index < table7.Rows.Count; ++index)
              {
                try
                {
                  DataRow row = table7.Rows[index];
                  int int32Value1 = DataSetProcessor.GetInt32Value(row, "F_GROUP_ID", 0);
                  int int32Value2 = DataSetProcessor.GetInt32Value(row, "F_ATTRIBUTE_ID", 0);
                  if (!this.AttrInGroups.ContainsKey(int32Value1))
                    this.AttrInGroups.Add(int32Value1, new List<int>());
                  this.AttrInGroups[int32Value1].Add(int32Value2);
                }
                catch
                {
                }
              }
            }
          }
          this._lastAttrsSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных со схемами ЖЦ, уровнями продвижения, шагами ЖЦ
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncLCStepsMetadata(DataSet cacheTables)
      {
        if (this.Locked || DateTime.UtcNow - this._lastLcStepsSyncTime < this.SyncDelta && !this._forced || cacheTables == null)
          return;
        lock (this._syncRootLcSteps)
        {
          this._lcSchemes.Clear();
          this._lcLevels.Clear();
          this._lcSteps.Clear();
          this._lcSchemesGuid2Id.Clear();
          this._lcLevelsGuid2Id.Clear();
          this._lcStepsGuid2Id.Clear();
          DataTable table1 = cacheTables.Tables["IMS_LC_SCHEMAS"];
          DataTable table2 = cacheTables.Tables["IMS_LEVELS"];
          DataTable table3 = cacheTables.Tables["IMS_LC_STEPS"];
          if (table1 != null)
          {
            lock (table1)
            {
              for (int index = 0; index < table1.Rows.Count; ++index)
              {
                try
                {
                  IMSLifeCycleScheme imsLifeCycleScheme = new IMSLifeCycleScheme();
                  imsLifeCycleScheme.Load(table1.Rows[index]);
                  imsLifeCycleScheme.Freeze();
                  this._lcSchemesGuid2Id[imsLifeCycleScheme.Guid] = imsLifeCycleScheme.SchemaID;
                  this._lcSchemes[imsLifeCycleScheme.SchemaID] = imsLifeCycleScheme;
                }
                catch
                {
                }
              }
            }
          }
          if (table2 != null)
          {
            lock (table2)
            {
              for (int index = 0; index < table2.Rows.Count; ++index)
              {
                try
                {
                  IMSLifeCycleLevel imsLifeCycleLevel = new IMSLifeCycleLevel();
                  imsLifeCycleLevel.Load(table2.Rows[index]);
                  imsLifeCycleLevel.Freeze();
                  this._lcLevelsGuid2Id[imsLifeCycleLevel.Guid] = imsLifeCycleLevel.LevelID;
                  this._lcLevels[imsLifeCycleLevel.LevelID] = imsLifeCycleLevel;
                }
                catch
                {
                }
              }
            }
          }
          if (table3 != null)
          {
            lock (table3)
            {
              for (int index = 0; index < table3.Rows.Count; ++index)
              {
                try
                {
                  DataRow row = table3.Rows[index];
                  IMSLifeCycleStep imsLifeCycleStep = new IMSLifeCycleStep();
                  imsLifeCycleStep.Load(row);
                  imsLifeCycleStep.Freeze();
                  this._lcStepsGuid2Id[imsLifeCycleStep.Guid] = imsLifeCycleStep.LCStepID;
                  this._lcSteps[imsLifeCycleStep.LCStepID] = imsLifeCycleStep;
                }
                catch
                {
                }
              }
            }
          }
        }
        this._lastLcStepsSyncTime = DateTime.UtcNow;
      }

      /// <summary>
      /// Пополнить какой-либо глобальный словарик информацией из указанной ключей
      /// </summary>
      /// <typeparam name="T">Тип данных ключа в глобальном словарике</typeparam>
      /// <param name="syncObject">Объект для синхронизации для доступа к коллекции с ключами</param>
      /// <param name="items">Коллекция с ключами</param>
      /// <param name="dict">Глобальный словарик</param>
      /// <param name="itemType">Тип метаданных для ключей</param>
      private void AddToGlobals<T>(
        object syncObject,
        IEnumerable<T> items,
        Dictionary<T, IMSGlobals> dict,
        IMSGlobals itemType)
      {
        List<T> objList = new List<T>();
        lock (syncObject)
          objList.AddRange(items);
        objList.ForEach((Action<T>) (item =>
        {
          if (dict.ContainsKey(item))
            return;
          dict.Add(item, itemType);
        }));
      }

      /// <summary>Выполнить синхронизацию глобальной коллекции</summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public void SyncGlobals(DataSet cacheTables)
      {
        if (this.Locked)
          return;
        lock (this._syncRootGlobals)
        {
          this._globalsGuid.Clear();
          this.AddToGlobals<Guid>(this._syncRootObjectTypes, (IEnumerable<Guid>) this._objectsGuid2Id.Keys, this._globalsGuid, IMSGlobals.IMSObjectType);
          this.AddToGlobals<Guid>(this._syncRootRelationTypes, (IEnumerable<Guid>) this._relationsGuid2Id.Keys, this._globalsGuid, IMSGlobals.IMSRelationType);
          this.AddToGlobals<Guid>(this._syncRootAttrTypes, (IEnumerable<Guid>) this._attrsGuid2Id.Keys, this._globalsGuid, IMSGlobals.IMSAttributeType);
          this.AddToGlobals<Guid>(this._syncRootAttrTypes, (IEnumerable<Guid>) this._attrGroupsGuid2Id.Keys, this._globalsGuid, IMSGlobals.IMSAttributeGroup);
          this.AddToGlobals<Guid>(this._syncRootLcSteps, (IEnumerable<Guid>) this._lcSchemesGuid2Id.Keys, this._globalsGuid, IMSGlobals.IMSLifeCycleScheme);
          this.AddToGlobals<Guid>(this._syncRootLcSteps, (IEnumerable<Guid>) this._lcLevelsGuid2Id.Keys, this._globalsGuid, IMSGlobals.IMSLifeCycleLevel);
          this.AddToGlobals<Guid>(this._syncRootLcSteps, (IEnumerable<Guid>) this._lcStepsGuid2Id.Keys, this._globalsGuid, IMSGlobals.IMSLifeCycleStep);
        }
      }

      /// <summary>
      /// Выполнить полную загрузку всех внутренних коллекций кэша метаданных.
      /// Метод выполняет ряд проверок, чтобы избежать лишних операций по работе с
      /// кэшем метаданных
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      /// <param name="forced">true - принудительно загрузить</param>
      void IMetaDataHelperCache.LoadMetadata(DataSet dataSet, bool forced)
      {
        if (forced)
        {
          lock (this.SyncRoot)
          {
            this._lastObjectsSyncTime = DateTime.MinValue;
            this._forced = true;
          }
        }
        if (DateTime.UtcNow - this._lastObjectsSyncTime < this.SyncDelta)
          return;
        try
        {
          this.LoadObjectTypesMetadata(dataSet);
          this.LoadObjectTypesHierarchy(dataSet);
          this.LoadRelationTypesMetadata(dataSet);
          this.LoadAttrTypesMetadata(dataSet);
          this.LoadSpecialRelationTypes(dataSet);
          this.LoadSpecialObjectTypes(dataSet);
          this.LoadLCStepsMetadata(dataSet);
        }
        finally
        {
          lock (this.SyncRoot)
          {
            this._syncDateTime = DateTime.UtcNow;
            this._forced = false;
          }
        }
      }

      /// <summary>
      /// Выполнить загрузку коллекций, связанных с типами объектов, с кэшем метаданных
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      private void LoadObjectTypesMetadata(DataSet dataSet)
      {
        if (DateTime.UtcNow - this._lastObjectsSyncTime < this.SyncDelta && !this._forced || dataSet == null)
          return;
        lock (this._syncRootObjectTypes)
        {
          this._objectsGuid2Id.Clear();
          this._objectTypes.Clear();
          int index1 = dataSet.Tables.IndexOf("IMS_OBJECT_TYPES");
          if (index1 == -1)
            return;
          DataTable table = dataSet.Tables[index1];
          if (table != null)
          {
            for (int index2 = 0; index2 < table.Rows.Count; ++index2)
            {
              try
              {
                DataRow row = table.Rows[index2];
                int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
                Guid key = new Guid(row["F_GUID"].ToString());
                IMSObjectType imsObjectType = new IMSObjectType();
                imsObjectType.Load(row);
                imsObjectType.Freeze();
                this._objectsGuid2Id[key] = int32;
                this._objectTypes[int32] = imsObjectType;
              }
              catch
              {
              }
            }
          }
          this._lastObjectsSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить загрузку коллекций, связанных с иерархией типов объектов.
      /// Внимание! Перед вызовом этого метода должен быть вызван метод LoadObjectTypesMetadata,
      /// который корректно заполняет коллекции [Guid типа объекта] = [ID типа объекта]
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      private void LoadObjectTypesHierarchy(DataSet dataSet)
      {
        if (DateTime.UtcNow - this._lastObjectsHierarchySyncTime < this.SyncDelta && !this._forced || dataSet == null)
          return;
        lock (this.SyncRoot)
        {
          this._objectsHierarchy.Clear();
          this._objectsHierarchyGuids.Clear();
          this._objectsHierarchyRev.Clear();
          this._objectsHierarchyRevGuids.Clear();
          int index1 = dataSet.Tables.IndexOf("IMS_OBJTYPES_TREE");
          if (index1 == -1)
            return;
          DataTable table = dataSet.Tables[index1];
          if (table != null)
          {
            for (int index2 = 0; index2 < table.Rows.Count; ++index2)
            {
              try
              {
                DataRow row = table.Rows[index2];
                int int32_1 = Convert.ToInt32(row["F_PARENT_ID"]);
                Guid guid1 = this._objectTypes[int32_1].Guid;
                int int32_2 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
                Guid guid2 = this._objectTypes[int32_2].Guid;
                this._objectsHierarchy[int32_2] = int32_1;
                this._objectsHierarchyGuids[guid2] = guid1;
                List<int> intList;
                this._objectsHierarchyRev.TryGetValue(int32_1, out intList);
                if (intList == null)
                {
                  intList = new List<int>();
                  this._objectsHierarchyRev[int32_1] = intList;
                }
                intList.Add(int32_2);
                List<Guid> guidList;
                this._objectsHierarchyRevGuids.TryGetValue(guid1, out guidList);
                if (guidList == null)
                {
                  guidList = new List<Guid>();
                  this._objectsHierarchyRevGuids[guid1] = guidList;
                }
                guidList.Add(guid2);
              }
              catch
              {
              }
            }
            table.Dispose();
          }
          this._lastObjectsHierarchySyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить загрузку коллекций, связанных с типами связей, с кэшем метаданных.
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      private void LoadRelationTypesMetadata(DataSet dataSet)
      {
        if (DateTime.UtcNow - this._lastRelationsSyncTime < this.SyncDelta && !this._forced || dataSet == null)
          return;
        lock (this._syncRootRelationTypes)
        {
          this._relationsGuid2Id.Clear();
          this.RelationTypes.Clear();
          this._applicabilities.Clear();
          this._inheritedApplicabilities.Clear();
          this._applicabilitiesSyncTime.Clear();
          this._applicabilitiesCache.Clear();
          DataTable dataTable1 = (DataTable) null;
          int index1 = dataSet.Tables.IndexOf("IMS_RELATION_TYPES");
          if (index1 != -1)
            dataTable1 = dataSet.Tables[index1];
          if (dataTable1 != null)
          {
            for (int index2 = 0; index2 < dataTable1.Rows.Count; ++index2)
            {
              try
              {
                DataRow row = dataTable1.Rows[index2];
                int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
                Guid key = new Guid(row["F_GUID"].ToString());
                IMSRelationType imsRelationType = new IMSRelationType();
                imsRelationType.Load(row);
                imsRelationType.Freeze();
                this.RelationTypes[int32] = imsRelationType;
                this._relationsGuid2Id[key] = int32;
              }
              catch
              {
              }
            }
          }
          DataTable dataTable2 = (DataTable) null;
          int index3 = dataSet.Tables.IndexOf("IMS_TYPES_APPLICABILITY");
          if (index3 != -1)
            dataTable2 = dataSet.Tables[index3];
          if (dataTable2 != null)
          {
            int count = dataTable2.Rows.Count;
            for (int index4 = 0; index4 < count; ++index4)
            {
              DataRow row = dataTable2.Rows[index4];
              int int32 = Convert.ToInt32(row["F_INOBJECT_TYPE"]);
              List<IMSApplicability> imsApplicabilityList;
              this._applicabilities.TryGetValue(int32, out imsApplicabilityList);
              if (imsApplicabilityList == null)
              {
                imsApplicabilityList = new List<IMSApplicability>();
                this._applicabilities[int32] = imsApplicabilityList;
              }
              IMSApplicability imsApplicability = new IMSApplicability();
              imsApplicability.Load(row);
              imsApplicability.Freeze();
              if (!imsApplicabilityList.Contains(imsApplicability))
                imsApplicabilityList.Add(imsApplicability);
              this._applicabilitiesSyncTime[int32] = DateTime.UtcNow;
            }
          }
          lock (this._syncRootObjectTypes)
          {
            int[] array = new int[this._objectTypes.Keys.Count];
            this._objectTypes.Keys.CopyTo(array, 0);
            for (int index5 = 0; index5 < array.Length; ++index5)
              this._inheritedApplicabilities[array[index5]] = this.GetParentApplicabilities(array[index5]);
          }
          this._lastRelationsSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить загрузку коллекций, связанных со специальными типами объектов.
      /// Внимание! Перед вызовом этого метода должны быть вызваны методы LoadObjectTypesMetadata,
      /// LoadSpecialRelationTypes
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      private void LoadSpecialObjectTypes(DataSet dataSet)
      {
        if (DateTime.UtcNow - this._lastSpecialObjectTypesSyncTime < this.SyncDelta && !this._forced || dataSet == null)
          return;
        lock (this._syncRootSpecialObjectTypes)
        {
          this._specialDesignedObjectTypes.Clear();
          this._specialSortedObjectTypes.Clear();
          this._specialSubstitutesObjectTypes.Clear();
          this._specialGroupingObjectTypes.Clear();
          this._specialGrouppedObjectTypes.Clear();
          this._specialVisibilityObjectTypes.Clear();
          this.SpecialContextObjectTypes.Clear();
          this._specialTopContextObjectTypes.Clear();
          int[] array;
          lock (this._syncRootObjectTypes)
          {
            array = new int[this._objectTypes.Keys.Count];
            this._objectTypes.Keys.CopyTo(array, 0);
          }
          int attributeTypeId1 = this.GetAttributeTypeID("cad00697-306c-11d8-b4e9-00304f19f545");
          int attributeTypeId2 = this.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545");
          Guid parentType1 = new Guid("cad0146b-306c-11d8-b4e9-00304f19f545");
          Guid parentType2 = new Guid("cad00348-306c-11d8-b4e9-00304f19f545");
          for (int index1 = 0; index1 < array.Length; ++index1)
          {
            IMSObjectType objectType = this.GetObjectType(array[index1]);
            if (objectType != null)
            {
              if (this.CanAddObjTypeToEditingContext(objectType.ObjectTypeID, false))
                this._specialGrouppedObjectTypes.Add(objectType.ObjectTypeID);
              List<IMSAttribute4ObjectType> attribute4ObjectTypeList = this.GetAttribute4ObjectTypeList(array[index1]);
              List<int> intList = new List<int>(attribute4ObjectTypeList.Count);
              for (int index2 = 0; index2 < attribute4ObjectTypeList.Count; ++index2)
                intList.Add(attribute4ObjectTypeList[index2].AttributeID);
              if (intList.IndexOf(attributeTypeId2) >= 0)
                this._specialVisibilityObjectTypes.Add(array[index1]);
              if (this.IsObjectTypeChildOf(objectType.Guid, parentType1) || this.IsObjectTypeChildOf(objectType.Guid, parentType2))
                this.SpecialContextObjectTypes.Add(objectType.ObjectTypeID);
              List<IMSApplicability> typeApplicabilities = this.GetObjectTypeApplicabilities(array[index1]);
              for (int index3 = 0; index3 < typeApplicabilities.Count; ++index3)
              {
                if (this.HasRelationTypeSubstitutes(typeApplicabilities[index3].RelationTypeID))
                {
                  this._specialSubstitutesObjectTypes.Add(array[index1]);
                  break;
                }
              }
              for (int index4 = 0; index4 < typeApplicabilities.Count; ++index4)
              {
                if (this.HasRelationTypeSorting(typeApplicabilities[index4].RelationTypeID))
                {
                  this._specialSortedObjectTypes.Add(array[index1]);
                  break;
                }
              }
              for (int index5 = 0; index5 < typeApplicabilities.Count; ++index5)
              {
                if (this.GetRelationTypeGuid(typeApplicabilities[index5].RelationTypeID).ToString() == "cad00023-306c-11d8-b4e9-00304f19f545")
                {
                  this._specialDesignedObjectTypes.Add(array[index1]);
                  break;
                }
              }
              for (int index6 = 0; index6 < typeApplicabilities.Count; ++index6)
              {
                if (this.HasRelationTypeGrouping(typeApplicabilities[index6].RelationTypeID))
                {
                  if (intList.IndexOf(attributeTypeId1) >= 0)
                  {
                    this._specialGroupingObjectTypes.Add(array[index1]);
                    break;
                  }
                  break;
                }
              }
            }
          }
          for (int index7 = 0; index7 < this.SpecialContextObjectTypes.Count; ++index7)
          {
            int contextObjectType = this.SpecialContextObjectTypes[index7];
            bool flag = false;
            for (int index8 = 0; index8 < this.SpecialContextObjectTypes.Count; ++index8)
            {
              if (contextObjectType != this.SpecialContextObjectTypes[index8] && this.IsObjectTypeChildOf(contextObjectType, this.SpecialContextObjectTypes[index8]))
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              this._specialTopContextObjectTypes.Add(contextObjectType);
          }
          this._lastSpecialObjectTypesSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить загрузку коллекций, связанных со специальными типами связей.
      /// Внимание! Перед вызовом этого метода должен быть вызван метод LoadRelationTypesMetadata,
      /// который корректно заполняет коллекции [Guid типа связи] = [ID типа связи]
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      private void LoadSpecialRelationTypes(DataSet dataSet)
      {
        if (DateTime.UtcNow - this._lastSpecialRelationTypesSyncTime < this.SyncDelta && !this._forced || dataSet == null)
          return;
        lock (this._syncRootSpecialRelationTypes)
        {
          this._specialSortedRelations.Clear();
          this._specialSubstitutesRelations.Clear();
          this._specialGroupingRelations.Clear();
          lock (this._syncRootRelationTypes)
          {
            int[] array = new int[this.RelationTypes.Keys.Count];
            this.RelationTypes.Keys.CopyTo(array, 0);
            int attributeTypeId1 = this.GetAttributeTypeID("cad001c0-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId2 = this.GetAttributeTypeID("cad001c1-306c-11d8-b4e9-00304f19f545");
            this.GetAttributeTypeID("cad00274-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId3 = this.GetAttributeTypeID("cad00817-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId4 = this.GetAttributeTypeID("cad00818-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId5 = this.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545");
            int attributeTypeId6 = this.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545");
            for (int index1 = 0; index1 < array.Length; ++index1)
            {
              int relTypeID = array[index1];
              if (this.GetRelationType(relTypeID) != null)
              {
                List<IMSAttribute4RelationType> relationTypeList = this.GetAttribute4RelationTypeList(relTypeID);
                List<int> intList = new List<int>(relationTypeList.Count);
                for (int index2 = 0; index2 < relationTypeList.Count; ++index2)
                  intList.Add(relationTypeList[index2].AttributeID);
                if (intList.IndexOf(attributeTypeId1) >= 0 && intList.IndexOf(attributeTypeId2) >= 0 && intList.IndexOf(attributeTypeId3) >= 0 && intList.IndexOf(attributeTypeId4) >= 0)
                  this._specialSubstitutesRelations.Add(relTypeID);
                if (intList.IndexOf(attributeTypeId5) >= 0)
                  this._specialSortedRelations.Add(relTypeID);
                if (intList.IndexOf(attributeTypeId5) >= 0 && intList.IndexOf(attributeTypeId6) >= 0)
                  this._specialGroupingRelations.Add(relTypeID);
              }
            }
          }
          this._lastSpecialRelationTypesSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить загрузку коллекций, связанных с типами атрибутов, с кэшем метаданных
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      private void LoadAttrTypesMetadata(DataSet dataSet)
      {
        if (DateTime.UtcNow - this._lastAttrsSyncTime < this.SyncDelta && !this._forced || dataSet == null)
          return;
        lock (this._syncRootAttrTypes)
        {
          this._attrsGuid2Id.Clear();
          this.AttrTypes.Clear();
          this.AttrNameTypes.Clear();
          this._attr4ObjectTypes.Clear();
          this._attrs4ObjectTypes.Clear();
          this._attr4RelationTypes.Clear();
          this._attrs4RelationTypes.Clear();
          DataTable dataTable1 = (DataTable) null;
          DataTable dataTable2 = (DataTable) null;
          DataTable dataTable3 = (DataTable) null;
          DataTable table = (DataTable) null;
          int index1 = dataSet.Tables.IndexOf("IMS_ATTRIBUTES");
          if (index1 != -1)
            dataTable1 = dataSet.Tables[index1];
          int index2 = dataSet.Tables.IndexOf("IMS_ATTR4OBJ_TYPES");
          if (index2 != -1)
            dataTable2 = dataSet.Tables[index2];
          int index3 = dataSet.Tables.IndexOf("IMS_ATTR4RELATION_TYPES");
          if (index3 != -1)
            dataTable3 = dataSet.Tables[index3];
          int index4 = dataSet.Tables.IndexOf("IMS_POSSIBLE_VALUES");
          if (index4 != -1)
            table = dataSet.Tables[index4];
          if (dataTable1 != null)
          {
            for (int index5 = 0; index5 < dataTable1.Rows.Count; ++index5)
            {
              try
              {
                IMSAttributeType imsAttributeType = new IMSAttributeType();
                imsAttributeType.Load(dataTable1.Rows[index5]);
                imsAttributeType.Freeze();
                this._attrsGuid2Id[imsAttributeType.AttributeGuid] = imsAttributeType.AttributeID;
                this.AttrTypes[imsAttributeType.AttributeID] = imsAttributeType;
                this.AttrNameTypes[imsAttributeType.Name.ToUpperInvariant()] = imsAttributeType.AttributeID;
              }
              catch
              {
              }
            }
          }
          if (dataTable2 != null)
          {
            for (int index6 = 0; index6 < dataTable2.Rows.Count; ++index6)
            {
              try
              {
                IMSAttribute4ObjectType attribute4ObjectType = new IMSAttribute4ObjectType();
                attribute4ObjectType.Load(dataTable2.Rows[index6]);
                attribute4ObjectType.Freeze();
                if (!this._attr4ObjectTypes.ContainsKey(attribute4ObjectType.ObjectTypeID))
                  this._attr4ObjectTypes[attribute4ObjectType.ObjectTypeID] = new List<IMSAttribute4ObjectType>();
                if (!this._attrs4ObjectTypes.ContainsKey(attribute4ObjectType.AttributeID))
                  this._attrs4ObjectTypes[attribute4ObjectType.AttributeID] = new List<IMSAttribute4ObjectType>();
                this._attr4ObjectTypes[attribute4ObjectType.ObjectTypeID].Add(attribute4ObjectType);
                this._attrs4ObjectTypes[attribute4ObjectType.AttributeID].Add(attribute4ObjectType);
                if (!this.AttrsApplicability.ContainsKey(attribute4ObjectType.AttributeID))
                  this.AttrsApplicability[attribute4ObjectType.AttributeID] = IMSAttributeTypeApplicability.ObjectType;
                else
                  this.AttrsApplicability[attribute4ObjectType.AttributeID] = this.AttrsApplicability[attribute4ObjectType.AttributeID] | IMSAttributeTypeApplicability.ObjectType;
              }
              catch
              {
              }
            }
          }
          if (dataTable3 != null)
          {
            for (int index7 = 0; index7 < dataTable3.Rows.Count; ++index7)
            {
              try
              {
                IMSAttribute4RelationType attribute4RelationType = new IMSAttribute4RelationType();
                attribute4RelationType.Load(dataTable3.Rows[index7]);
                attribute4RelationType.Freeze();
                if (!this._attr4RelationTypes.ContainsKey(attribute4RelationType.RelationTypeID))
                  this._attr4RelationTypes[attribute4RelationType.RelationTypeID] = new List<IMSAttribute4RelationType>();
                if (!this._attrs4RelationTypes.ContainsKey(attribute4RelationType.AttributeID))
                  this._attrs4RelationTypes[attribute4RelationType.AttributeID] = new List<IMSAttribute4RelationType>();
                this._attr4RelationTypes[attribute4RelationType.RelationTypeID].Add(attribute4RelationType);
                this._attrs4RelationTypes[attribute4RelationType.AttributeID].Add(attribute4RelationType);
                if (!this.AttrsApplicability.ContainsKey(attribute4RelationType.AttributeID))
                  this.AttrsApplicability[attribute4RelationType.AttributeID] = IMSAttributeTypeApplicability.RelationType;
                else
                  this.AttrsApplicability[attribute4RelationType.AttributeID] = this.AttrsApplicability[attribute4RelationType.AttributeID] | IMSAttributeTypeApplicability.RelationType;
              }
              catch
              {
              }
            }
          }
          if (table != null && table.Rows.Count > 0)
          {
            foreach (KeyValuePair<int, IMSAttributePossibleValues> keyValuePair in IMSAttributePossibleValues.LoadFromDataTable(table))
            {
              IMSAttributeType imsAttributeType1;
              this.AttrTypes.TryGetValue(keyValuePair.Value.F_ATTRIBUTE_ID, out imsAttributeType1);
              if (imsAttributeType1 != null)
              {
                IMSAttributeType imsAttributeType2 = imsAttributeType1.Clone();
                imsAttributeType2.PossibleValues = keyValuePair.Value[imsAttributeType2.ValueFieldName];
                imsAttributeType2.PossibleValuesDescriptions = keyValuePair.Value.Descriptions;
                imsAttributeType2.Freeze();
                this.AttrTypes[keyValuePair.Value.F_ATTRIBUTE_ID] = imsAttributeType2;
                if (imsAttributeType2.FieldType == FieldTypes.ftObjectLink || imsAttributeType2.FieldType == FieldTypes.ftObjectLinkByID)
                {
                  int index8 = this._linkAttributeTypesList.IndexOf(imsAttributeType1);
                  if (index8 >= 0)
                    this._linkAttributeTypesList[index8] = imsAttributeType2;
                }
              }
            }
          }
          this._lastAttrsSyncTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Выполнить загрузку коллекций, связанных со схемами ЖЦ, уровнями продвижения, шагами ЖЦ
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      private void LoadLCStepsMetadata(DataSet dataSet)
      {
        if (DateTime.UtcNow - this._lastLcStepsSyncTime < this.SyncDelta && !this._forced || dataSet == null)
          return;
        lock (this._syncRootLcSteps)
        {
          this._lcSchemes.Clear();
          this._lcLevels.Clear();
          this._lcSchemesGuid2Id.Clear();
          this._lcLevelsGuid2Id.Clear();
          this._lcStepsGuid2Id.Clear();
          DataTable dataTable1 = (DataTable) null;
          DataTable dataTable2 = (DataTable) null;
          DataTable dataTable3 = (DataTable) null;
          int index1 = dataSet.Tables.IndexOf("IMS_LC_SCHEMAS");
          if (index1 != -1)
            dataTable1 = dataSet.Tables[index1];
          int index2 = dataSet.Tables.IndexOf("IMS_LEVELS");
          if (index2 != -1)
            dataTable2 = dataSet.Tables[index2];
          int index3 = dataSet.Tables.IndexOf("IMS_LC_STEPS");
          if (index3 != -1)
            dataTable3 = dataSet.Tables[index3];
          if (dataTable1 != null)
          {
            for (int index4 = 0; index4 < dataTable1.Rows.Count; ++index4)
            {
              try
              {
                IMSLifeCycleScheme imsLifeCycleScheme = new IMSLifeCycleScheme();
                imsLifeCycleScheme.Load(dataTable1.Rows[index4]);
                imsLifeCycleScheme.Freeze();
                this._lcSchemesGuid2Id[imsLifeCycleScheme.Guid] = imsLifeCycleScheme.SchemaID;
                this._lcSchemes[imsLifeCycleScheme.SchemaID] = imsLifeCycleScheme;
              }
              catch
              {
              }
            }
          }
          if (dataTable2 != null)
          {
            for (int index5 = 0; index5 < dataTable2.Rows.Count; ++index5)
            {
              try
              {
                IMSLifeCycleLevel imsLifeCycleLevel = new IMSLifeCycleLevel();
                imsLifeCycleLevel.Load(dataTable2.Rows[index5]);
                imsLifeCycleLevel.Freeze();
                this._lcLevelsGuid2Id[imsLifeCycleLevel.Guid] = imsLifeCycleLevel.LevelID;
                this._lcLevels[imsLifeCycleLevel.LevelID] = imsLifeCycleLevel;
              }
              catch
              {
              }
            }
          }
          if (dataTable3 != null)
          {
            for (int index6 = 0; index6 < dataTable3.Rows.Count; ++index6)
            {
              try
              {
                DataRow row = dataTable3.Rows[index6];
                IMSLifeCycleStep imsLifeCycleStep = new IMSLifeCycleStep();
                imsLifeCycleStep.Load(row);
                imsLifeCycleStep.Freeze();
                this._lcStepsGuid2Id[imsLifeCycleStep.Guid] = imsLifeCycleStep.LCStepID;
                this._lcSteps[imsLifeCycleStep.LCStepID] = imsLifeCycleStep;
              }
              catch
              {
              }
            }
          }
        }
        this._lastLcStepsSyncTime = DateTime.UtcNow;
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе объекта
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>true, если тип объекта существует</returns>
      public bool ExistsObjectType(int objTypeID)
      {
        lock (this._syncRootObjectTypes)
          return this._objectTypes.ContainsKey(objTypeID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе объекта
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если тип объекта существует</returns>
      public bool ExistsObjectType(Guid objTypeGuid)
      {
        return this.ExistsObjectType(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>Получить краткую информацию о типе объекта</summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Краткая информация о типе объекта или null</returns>
      public IMSObjectType GetObjectType(int objTypeID)
      {
        lock (this._syncRootObjectTypes)
        {
          IMSObjectType objectType;
          if (this._objectTypes.TryGetValue(objTypeID, out objectType))
            return objectType;
        }
        return (IMSObjectType) null;
      }

      /// <summary>Получить краткую информацию о типе объекта</summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Краткая информация о типе объекта или null</returns>
      public IMSObjectType GetObjectType(Guid objTypeGuid)
      {
        return this.GetObjectType(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>Получить название типа объектов (например, "Детали")</summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Название типа объектов (например, "Детали")</returns>
      public string GetObjectTypeName(int objTypeID)
      {
        lock (this._syncRootObjectTypes)
        {
          IMSObjectType imsObjectType;
          if (this._objectTypes.TryGetValue(objTypeID, out imsObjectType))
            return imsObjectType.ObjectTypeName;
        }
        return string.Empty;
      }

      /// <summary>Получить название типа объектов (например, "Детали")</summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Название типа объектов (например, "Детали")</returns>
      public string GetObjectTypeName(Guid objTypeGuid)
      {
        return this.GetObjectTypeName(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить полное название типа объектов (например, "Изделия\Детали")
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Полное название типа объектов (например, "Изделия\Детали")</returns>
      public string GetObjectTypeFullName(int objTypeID)
      {
        if (objTypeID == -1 || objTypeID == -1)
          return string.Empty;
        List<int> parentsIdReverse = this.GetObjectTypeParentsIDReverse(objTypeID);
        parentsIdReverse.Add(objTypeID);
        StringBuilder stringBuilder = new StringBuilder();
        lock (this._syncRootObjectTypes)
        {
          for (int index = 0; index < parentsIdReverse.Count; ++index)
          {
            IMSObjectType imsObjectType;
            if (this._objectTypes.TryGetValue(parentsIdReverse[index], out imsObjectType))
            {
              stringBuilder.Append(imsObjectType.ObjectTypeName);
              if (index < parentsIdReverse.Count - 1)
                stringBuilder.Append("\\");
            }
          }
        }
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Получить название экземпляра типа объектов (например, "Деталь")
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Название экземпляра типа объектов (например, "Деталь")</returns>
      public string GetObjectName(int objTypeID)
      {
        lock (this._syncRootObjectTypes)
        {
          IMSObjectType imsObjectType;
          if (this._objectTypes.TryGetValue(objTypeID, out imsObjectType))
            return imsObjectType.ObjectName;
        }
        return string.Empty;
      }

      /// <summary>
      /// Получить название экземпляра типа объектов (например, "Деталь")
      /// </summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Название экземпляра типа объектов (например, "Деталь")</returns>
      public string GetObjectName(Guid objTypeGuid)
      {
        return this.GetObjectName(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить по наименованию типа объекта его Int32-идентификатор
      /// </summary>
      /// <param name="objTypeName">Наименование типа объекта</param>
      /// <returns>Идентификатор типа объекта. -1 - тип объекта не найден</returns>
      public int GetObjectTypeIDFromName(string objTypeName)
      {
        lock (this._syncRootObjectTypes)
        {
          KeyValuePair<int, IMSObjectType>? nullable = this._objectTypes.Where<KeyValuePair<int, IMSObjectType>>((System.Func<KeyValuePair<int, IMSObjectType>, bool>) (e => e.Value.ObjectTypeName.Equals(objTypeName))).Select<KeyValuePair<int, IMSObjectType>, KeyValuePair<int, IMSObjectType>?>((System.Func<KeyValuePair<int, IMSObjectType>, KeyValuePair<int, IMSObjectType>?>) (e => new KeyValuePair<int, IMSObjectType>?(e))).FirstOrDefault<KeyValuePair<int, IMSObjectType>?>();
          if (nullable.HasValue)
          {
            if (nullable.HasValue)
              return nullable.Value.Key;
          }
        }
        return -1;
      }

      /// <summary>Получить по Guid типа объекта его Int32-идентификатор</summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>Идентификатор типа объекта. -1 - тип объекта не найден</returns>
      public int GetObjectTypeID(Guid objTypeGuid)
      {
        lock (this._syncRootObjectTypes)
        {
          int objectTypeId;
          if (this._objectsGuid2Id.TryGetValue(objTypeGuid, out objectTypeId))
            return objectTypeId;
        }
        return -1;
      }

      /// <summary>
      /// Получить по Int32-идентификатору типа объекта его Guid-идентификатор
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Идентификатор типа объекта. Guid.Empty - тип объекта не найден</returns>
      public Guid GetObjectTypeGuid(int objTypeID)
      {
        lock (this._syncRootObjectTypes)
        {
          IMSObjectType imsObjectType;
          if (this._objectTypes.TryGetValue(objTypeID, out imsObjectType))
            return imsObjectType.Guid;
        }
        return Guid.Empty;
      }

      /// <summary>
      /// Возвращает идентификатор типа объектов по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа объекта в виде строки</param>
      public int GetObjectTypeID(string Guid) => this.GetObjectTypeID(new Guid(Guid));

      /// <summary>Получить по Guid типа связи его Int32-идентификатор</summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Идентификатор типа связи. -1 - тип связи не найден</returns>
      public int GetRelationTypeID(Guid relTypeGuid)
      {
        lock (this._syncRootRelationTypes)
        {
          int relationTypeId;
          if (this._relationsGuid2Id.TryGetValue(relTypeGuid, out relationTypeId))
            return relationTypeId;
        }
        return -1;
      }

      /// <summary>
      /// Получить по Int32-идентификатору типа связи ее Guid-идентификатор
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Идентификатор типа связи. Guid.Empty - тип связи не найден</returns>
      public Guid GetRelationTypeGuid(int relTypeID)
      {
        lock (this._syncRootRelationTypes)
        {
          IMSRelationType imsRelationType;
          if (this.RelationTypes.TryGetValue(relTypeID, out imsRelationType))
            return imsRelationType.Guid;
        }
        return Guid.Empty;
      }

      /// <summary>
      /// Возвращает идентификатор типа связи по строковому представлению ее глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа связи в виде строки</param>
      public int GetRelationTypeID(string Guid) => this.GetRelationTypeID(new Guid(Guid));

      /// <summary>
      /// Получить Guid родительского типа объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Guid родительского типа объектов для указанного дочернего типа объекта или Guid.Empty</returns>
      public Guid GetObjectTypeParentID(Guid childTypeGuid)
      {
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          Guid guid;
          return this._objectsHierarchyGuids.TryGetValue(childTypeGuid, out guid) ? guid : Guid.Empty;
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
      }

      /// <summary>
      /// Получить ID родительского типа объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>ID родительского типа объектов для указанного дочернего типа объекта или -1</returns>
      public int GetObjectTypeParentID(int childTypeID)
      {
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          int num;
          return this._objectsHierarchy.TryGetValue(childTypeID, out num) ? num : -1;
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
      }

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetObjectTypeParentsID(Guid childTypeGuid)
      {
        return this.GetObjectTypeParentsID(this.GetObjectTypeID(childTypeGuid));
      }

      /// <summary>
      /// Получить список Guid всех родительских типов объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список Guid всех родительских типов объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<Guid> GetObjectTypeParentsGuid(int childTypeID)
      {
        return this.GetObjectTypeParentsGuid(this.GetObjectTypeGuid(childTypeID));
      }

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetObjectTypeParentsID(int childTypeID)
      {
        List<int> objectTypeParentsId = new List<int>(10);
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          int key;
          if (!this._objectsHierarchy.TryGetValue(childTypeID, out key))
            key = -1;
          while (key >= 0)
          {
            objectTypeParentsId.Add(key);
            if (!this._objectsHierarchy.TryGetValue(key, out key))
              key = -1;
          }
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        return objectTypeParentsId;
      }

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта.
      /// Родительские объекты следуют в списке в порядке от самого верхнего типа объекта к дочерним.
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetObjectTypeParentsIDReverse(int childTypeID)
      {
        List<int> parentsIdReverse = new List<int>();
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          int key;
          if (!this._objectsHierarchy.TryGetValue(childTypeID, out key))
            key = -1;
          while (key >= 0)
          {
            parentsIdReverse.Insert(0, key);
            if (!this._objectsHierarchy.TryGetValue(key, out key))
              key = -1;
          }
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        return parentsIdReverse;
      }

      /// <summary>
      /// Получить список Guid всех родительских типов объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Список Guid всех родительских типов объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<Guid> GetObjectTypeParentsGuid(Guid childTypeGuid)
      {
        List<Guid> objectTypeParentsGuid = new List<Guid>();
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          Guid empty;
          if (!this._objectsHierarchyGuids.TryGetValue(childTypeGuid, out empty))
            empty = Guid.Empty;
          while (empty != Guid.Empty)
          {
            objectTypeParentsGuid.Add(empty);
            if (!this._objectsHierarchyGuids.TryGetValue(empty, out empty))
              empty = Guid.Empty;
          }
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        return objectTypeParentsGuid;
      }

      /// <summary>
      /// Проверить, является ли тип объекта parentType родительским типом для типа объекта childType
      /// </summary>
      /// <param name="childType">Проверяемый дочерний тип объекта</param>
      /// <param name="parentType">Проверяемый родительский тип объекта (он может быть в любом месте родительской иерархии)</param>
      /// <returns>true, если parentType является родительским типом для childType</returns>
      public bool IsObjectTypeChildOf(Guid childType, Guid parentType)
      {
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          if (childType == parentType)
            return true;
          Guid key;
          if (!this._objectsHierarchyGuids.TryGetValue(childType, out key))
            return false;
          if (key == parentType)
            return true;
          while (this._objectsHierarchyGuids.TryGetValue(key, out key))
          {
            if (key == parentType)
              return true;
          }
          return false;
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
      }

      /// <summary>
      /// Проверить, является ли тип объекта parentType родительским типом для типа объекта c идентификатором childTypeId
      /// </summary>
      /// <param name="childTypeId">Идентификатор проверяемого дочернего типа объекта</param>
      /// <param name="parentType">Проверяемый родительский тип объекта (он может быть в любом месте родительской иерархии)</param>
      /// <returns>true, если parentType является родительским типом для типа с идентификатором childTypeId</returns>
      public bool IsObjectTypeChildOf(int childTypeId, Guid parentType)
      {
        IMSObjectType objectType = this.GetObjectType(childTypeId);
        return objectType != null && this.IsObjectTypeChildOf(objectType.Guid, parentType);
      }

      /// <summary>
      /// Определить уровень вложенности указанного типа объектов в иерархии. Значение 0 - типы объектов верхнего уровня
      /// </summary>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <returns>-1 - тип объекта не найден, 0 - тип верхнего уровня, больше нуля - уровень вложенности в иерархии</returns>
      public int GetObjectTypeLevel(int objectTypeID)
      {
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          if (this.GetObjectTypeGuid(objectTypeID) == Guid.Empty)
            return -1;
          int key;
          if (!this._objectsHierarchy.TryGetValue(objectTypeID, out key))
            return 0;
          int objectTypeLevel = 1;
          while (this._objectsHierarchy.TryGetValue(key, out key))
            ++objectTypeLevel;
          return objectTypeLevel;
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
      }

      /// <summary>
      /// Проверить, является ли тип объекта parentType родительским типом для типа объекта childType
      /// </summary>
      /// <param name="childType">Проверяемый дочерний тип объекта</param>
      /// <param name="parentType">Проверяемый родительский тип объекта (он может быть в любом месте родительской иерархии)</param>
      /// <returns>true, если parentType является родительским типом для childType</returns>
      public bool IsObjectTypeChildOf(int childType, int parentType)
      {
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          if (childType == parentType)
            return true;
          int key;
          if (!this._objectsHierarchy.TryGetValue(childType, out key))
            return false;
          if (key == parentType)
            return true;
          while (this._objectsHierarchy.TryGetValue(key, out key))
          {
            if (key == parentType)
              return true;
          }
          return false;
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
      }

      /// <summary>
      /// Получить список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список ID всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetObjectTypeChildrenID(Guid parentTypeGuid)
      {
        return this.GetObjectTypeChildrenID(this.GetObjectTypeID(parentTypeGuid));
      }

      /// <summary>
      /// Получить список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<Guid> GetObjectTypeChildrenGuid(int parentTypeID)
      {
        return this.GetObjectTypeChildrenGuid(this.GetObjectTypeGuid(parentTypeID));
      }

      /// <summary>
      /// Получить список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetObjectTypeChildrenID(int parentTypeID)
      {
        List<int> objectTypeChildrenId = new List<int>();
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          List<int> intList;
          this._objectsHierarchyRev.TryGetValue(parentTypeID, out intList);
          if (intList != null)
          {
            for (int index = 0; index < intList.Count; ++index)
            {
              if (objectTypeChildrenId.IndexOf(intList[index]) < 0)
                objectTypeChildrenId.Add(intList[index]);
            }
          }
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        List<int> intList1 = new List<int>();
        for (int index1 = objectTypeChildrenId.Count - 1; index1 >= 0; --index1)
        {
          int num = objectTypeChildrenId[index1];
          for (int index2 = 0; index2 < objectTypeChildrenId.Count; ++index2)
          {
            if (index1 != index2)
            {
              int parentType = objectTypeChildrenId[index2];
              if (this.IsObjectTypeChildOf(num, parentType) && !this.IsLocalObjectType(num) && num != parentType && intList1.IndexOf(num) < 0)
                intList1.Add(num);
            }
          }
        }
        for (int index = 0; index < intList1.Count; ++index)
          objectTypeChildrenId.Remove(intList1[index]);
        return objectTypeChildrenId;
      }

      /// <summary>
      /// Получить список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<Guid> GetObjectTypeChildrenGuid(Guid parentTypeGuid)
      {
        List<Guid> typeChildrenGuid = new List<Guid>();
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          List<Guid> guidList;
          this._objectsHierarchyRevGuids.TryGetValue(parentTypeGuid, out guidList);
          if (guidList != null)
          {
            for (int index = 0; index < guidList.Count; ++index)
            {
              if (typeChildrenGuid.IndexOf(guidList[index]) < 0)
                typeChildrenGuid.Add(guidList[index]);
            }
          }
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        List<Guid> guidList1 = new List<Guid>();
        for (int index1 = typeChildrenGuid.Count - 1; index1 >= 0; --index1)
        {
          Guid guid = typeChildrenGuid[index1];
          for (int index2 = 0; index2 < typeChildrenGuid.Count; ++index2)
          {
            if (index1 != index2)
            {
              Guid parentType = typeChildrenGuid[index2];
              if (this.IsObjectTypeChildOf(guid, parentType) && !this.IsLocalObjectType(guid) && guid != parentType && guidList1.IndexOf(guid) < 0)
                guidList1.Add(guid);
            }
          }
        }
        for (int index = 0; index < guidList1.Count; ++index)
          typeChildrenGuid.Remove(guidList1[index]);
        return typeChildrenGuid;
      }

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <param name="result">Список, в который заносятся результаты поиска</param>
      internal void _GetObjectTypeChildrenIDRecursive(int parentTypeID, IList<int> result)
      {
        List<int> intList;
        this._objectsHierarchyRev.TryGetValue(parentTypeID, out intList);
        if (intList == null)
          return;
        for (int index = 0; index < intList.Count; ++index)
        {
          result.Add(intList[index]);
          this._GetObjectTypeChildrenIDRecursive(intList[index], result);
        }
      }

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов).
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetObjectTypeChildrenIDRecursive(int parentTypeID)
      {
        List<int> result = new List<int>();
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          result.Add(parentTypeID);
          this._GetObjectTypeChildrenIDRecursive(parentTypeID, (IList<int>) result);
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        return result;
      }

      /// <summary>
      /// Получить рекурсивно список ID всех локальных дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов).
      /// Добавляется также и parentTypeID, даже если он не является локальным типом (в начало списка).
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних локальных типов объектов для указанного родительского типа объекта
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetLocalObjectTypeChildrenIDRecursive(int parentTypeID)
      {
        List<int> result = new List<int>();
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          result.Add(parentTypeID);
          this._GetObjectTypeChildrenIDRecursive(parentTypeID, (IList<int>) result);
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        if (result.Count <= 1)
          return result;
        if (!this.GetObjectType(result[0]).IsLocalType)
        {
          for (int index = result.Count - 1; index > 0; --index)
          {
            if (!this.GetObjectType(result[index]).IsLocalType)
              result.RemoveAt(index);
          }
        }
        else
        {
          int num = result.Find((Predicate<int>) (item => !this.GetObjectType(item).IsLocalType));
          if (num > 0)
          {
            for (int index = result.Count - 1; index > num; --index)
            {
              if (!this.GetObjectType(result[index]).IsLocalType)
                result.RemoveAt(index);
            }
          }
        }
        return result;
      }

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeIDs">Список Int32-идентификаторов родительских типов объектов</param>
      /// <returns>Список ID всех дочерних объектов для указанных родительских типов объектов (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetObjectTypeChildrenIDRecursive(IEnumerable<int> parentTypeIDs)
      {
        List<int> result = new List<int>();
        if (parentTypeIDs == null || !parentTypeIDs.Any<int>())
          return result;
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          foreach (int parentTypeId in parentTypeIDs)
          {
            result.Add(parentTypeId);
            this._GetObjectTypeChildrenIDRecursive(parentTypeId, (IList<int>) result);
          }
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        return result;
      }

      /// <summary>
      /// Получить рекурсивно список ID всех локальных дочерних типов объектов для указанных родительских типов объектов
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляются также и parentTypeIDs.
      /// </summary>
      /// <param name="parentTypeIDs">Список Int32-идентификаторов родительских типов объектов</param>
      /// <returns>Список ID всех дочерних локальных типов объектов для указанных родительских типов объектов.
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetLocalObjectTypeChildrenIDRecursive(IEnumerable<int> parentTypeIDs)
      {
        List<int> result = new List<int>();
        if (parentTypeIDs == null || !parentTypeIDs.Any<int>())
          return result;
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          foreach (int parentTypeId in parentTypeIDs)
          {
            result.Add(parentTypeId);
            this._GetObjectTypeChildrenIDRecursive(parentTypeId, (IList<int>) result);
          }
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        if (result.Count <= 1)
          return result;
        if (!this.GetObjectType(result[0]).IsLocalType)
        {
          for (int index = result.Count - 1; index >= 0; --index)
          {
            IMSObjectType objectType = this.GetObjectType(result[index]);
            if (!objectType.IsLocalType && !parentTypeIDs.Contains<int>(objectType.ObjectTypeID))
              result.RemoveAt(index);
          }
        }
        else
        {
          int num = result.Find((Predicate<int>) (item => !this.GetObjectType(item).IsLocalType));
          if (num > 0)
          {
            for (int index = result.Count - 1; index > num; --index)
            {
              if (!this.GetObjectType(result[index]).IsLocalType)
                result.RemoveAt(index);
            }
          }
        }
        return result;
      }

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<int> GetObjectTypeChildrenIDRecursive(Guid parentTypeGuid)
      {
        return this.GetObjectTypeChildrenIDRecursive(this.GetObjectTypeID(parentTypeGuid));
      }

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <param name="result">Результат</param>
      internal void _GetObjectTypeChildrenGuidRecursive(Guid parentTypeGuid, List<Guid> result)
      {
        List<Guid> guidList;
        this._objectsHierarchyRevGuids.TryGetValue(parentTypeGuid, out guidList);
        if (guidList == null)
          return;
        for (int index = 0; index < guidList.Count; ++index)
        {
          result.Add(guidList[index]);
          this._GetObjectTypeChildrenGuidRecursive(guidList[index], result);
        }
      }

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<Guid> GetObjectTypeChildrenGuidRecursive(Guid parentTypeGuid)
      {
        List<Guid> result = new List<Guid>();
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          result.Add(parentTypeGuid);
          this._GetObjectTypeChildrenGuidRecursive(parentTypeGuid, result);
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        return result;
      }

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeGuids">Список Guid идентификаторов родительских типов объектов</param>
      /// <returns>Список Guid всех дочерних объектов для указанных родительских типов объектов (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<Guid> GetObjectTypeChildrenGuidRecursive(IEnumerable<Guid> parentTypeGuids)
      {
        List<Guid> result = new List<Guid>();
        if (parentTypeGuids == null || !parentTypeGuids.Any<Guid>())
          return result;
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          foreach (Guid parentTypeGuid in parentTypeGuids)
          {
            result.Add(parentTypeGuid);
            this._GetObjectTypeChildrenGuidRecursive(parentTypeGuid, result);
          }
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
        return result;
      }

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeID">Int32-идентификатор родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public List<Guid> GetObjectTypeChildrenGuidRecursive(int parentTypeID)
      {
        return this.GetObjectTypeChildrenGuidRecursive(this.GetObjectTypeGuid(parentTypeID));
      }

      /// <summary>
      /// Метод получает на вход список допустимых типов объектов. Затем он "раскручивает" их родительские
      /// типы объектов (вверх по иерархии) до абстрактных родительских типов, а затем готовит список
      /// верхних допустимых родительских типов объектов. Метод можно использовать для подготовки списка
      /// типов объектов для окна по выбору объектов из списка допустимых типов, например, в команде
      /// "Добавить в состав"
      /// </summary>
      /// <param name="typeList">Список допустимых типов объектов</param>
      /// <returns>Список допустимых типов объектов верхнего уровня</returns>
      public List<Guid> GetTopParentEnabledObjectTypesGuid(IEnumerable<Guid> typeList)
      {
        if (typeList != null && typeList.Count<Guid>() <= 1)
          return typeList.ToList<Guid>();
        List<Guid> guidList = new List<Guid>();
        List<Guid> enabledObjectTypesGuid = new List<Guid>(0);
        if (typeList != null)
        {
          foreach (Guid type in typeList)
          {
            if (!guidList.Contains(type))
            {
              guidList.Add(type);
              List<Guid> objectTypeParentsGuid = this.GetObjectTypeParentsGuid(type);
              if (objectTypeParentsGuid != null && objectTypeParentsGuid.Count != 0)
              {
                for (int index = 0; index < objectTypeParentsGuid.Count; ++index)
                {
                  IMSObjectType objectType = this.GetObjectType(objectTypeParentsGuid[index]);
                  if (objectType != null && objectType.VersionsMode == ObjectVersionModes.Abstract)
                    guidList.Add(objectType.Guid);
                }
              }
            }
          }
        }
        for (int index1 = 0; index1 < guidList.Count; ++index1)
        {
          Guid childTypeGuid = guidList[index1];
          List<Guid> objectTypeParentsGuid = this.GetObjectTypeParentsGuid(childTypeGuid);
          if (objectTypeParentsGuid == null || objectTypeParentsGuid.Count == 0)
          {
            if (!enabledObjectTypesGuid.Contains(childTypeGuid))
              enabledObjectTypesGuid.Add(childTypeGuid);
          }
          else
          {
            if (!enabledObjectTypesGuid.Contains(childTypeGuid))
              enabledObjectTypesGuid.Add(childTypeGuid);
            for (int index2 = 0; index2 < objectTypeParentsGuid.Count; ++index2)
            {
              if (guidList.Contains(objectTypeParentsGuid[index2]))
              {
                enabledObjectTypesGuid.Remove(childTypeGuid);
                childTypeGuid = objectTypeParentsGuid[index2];
                if (!enabledObjectTypesGuid.Contains(childTypeGuid))
                  enabledObjectTypesGuid.Add(childTypeGuid);
              }
              else if (!enabledObjectTypesGuid.Contains(childTypeGuid))
                enabledObjectTypesGuid.Add(childTypeGuid);
            }
          }
        }
        return enabledObjectTypesGuid;
      }

      /// <summary>
      /// Метод получает на вход список допустимых типов объектов. Затем он "раскручивает" их родительские
      /// типы объектов (вверх по иерархии) до абстрактных родительских типов, а затем готовит список
      /// верхних допустимых родительских типов объектов. Метод можно использовать для подготовки списка
      /// типов объектов для окна по выбору объектов из списка допустимых типов, например, в команде
      /// "Добавить в состав"
      /// </summary>
      /// <param name="typeList">Список допустимых типов объектов</param>
      /// <returns>Список допустимых типов объектов верхнего уровня</returns>
      public List<int> GetTopParentEnabledObjectTypes(IEnumerable<int> typeList)
      {
        if (typeList != null && typeList.Count<int>() <= 1)
          return typeList.ToList<int>();
        List<int> intList = new List<int>();
        List<int> enabledObjectTypes = new List<int>(0);
        if (typeList != null)
        {
          foreach (int type in typeList)
          {
            if (!intList.Contains(type))
            {
              intList.Add(type);
              List<int> objectTypeParentsId = this.GetObjectTypeParentsID(type);
              if (objectTypeParentsId != null && objectTypeParentsId.Count != 0)
              {
                for (int index = 0; index < objectTypeParentsId.Count; ++index)
                {
                  IMSObjectType objectType = this.GetObjectType(objectTypeParentsId[index]);
                  if (objectType != null && objectType.VersionsMode == ObjectVersionModes.Abstract)
                    intList.Add(objectType.ObjectTypeID);
                }
              }
            }
          }
        }
        for (int index1 = 0; index1 < intList.Count; ++index1)
        {
          int childTypeID = intList[index1];
          List<int> objectTypeParentsId = this.GetObjectTypeParentsID(childTypeID);
          if (objectTypeParentsId == null || objectTypeParentsId.Count == 0)
          {
            if (!enabledObjectTypes.Contains(childTypeID))
              enabledObjectTypes.Add(childTypeID);
          }
          else
          {
            if (!enabledObjectTypes.Contains(childTypeID))
              enabledObjectTypes.Add(childTypeID);
            for (int index2 = 0; index2 < objectTypeParentsId.Count; ++index2)
            {
              if (intList.Contains(objectTypeParentsId[index2]))
              {
                enabledObjectTypes.Remove(childTypeID);
                childTypeID = objectTypeParentsId[index2];
                if (!enabledObjectTypes.Contains(childTypeID))
                  enabledObjectTypes.Add(childTypeID);
              }
              else if (!enabledObjectTypes.Contains(childTypeID))
                enabledObjectTypes.Add(childTypeID);
            }
          }
        }
        return enabledObjectTypes;
      }

      /// <summary>Получить список типов объектов верхнего уровня</summary>
      /// <returns>Список типов объектов верхнего уровня</returns>
      public List<int> GetTopObjectTypesIDs()
      {
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          return new List<int>((IEnumerable<int>) this._topObjectTypes);
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
      }

      /// <summary>Получить список типов объектов верхнего уровня</summary>
      /// <returns>Список типов объектов верхнего уровня</returns>
      public List<Guid> GetTopObjectTypesGuids()
      {
        this._syncRootObjectHierarchyTypes.EnterReadLock();
        try
        {
          return this._topObjectTypes.ConvertAll<Guid>(new Converter<int, Guid>(this.GetObjectTypeGuid));
        }
        finally
        {
          this._syncRootObjectHierarchyTypes.ExitReadLock();
        }
      }

      /// <summary>
      /// Вернуть нелокальный или абстрактный родительский тип для указанного дочернего типа.
      /// Если дочерний тип является локальным, либо абстрактным, либо типом верхнего уровня,
      /// возвращается он сам. Используется для оптимизации запросов
      /// в коллекции объектов и связей.
      /// </summary>
      /// <param name="childType">Дочерний тип объекта, для которого надо найти родительский тип объекта</param>
      /// <returns>Нелокальный или абстрактный родительский тип для указанного дочернего типа</returns>
      public int GetTopParentObjectTypeID(int childType)
      {
        if (childType == -1 || this.IsLocalObjectType(childType))
          return childType;
        List<int> objectTypeParentsId = this.GetObjectTypeParentsID(childType);
        if (objectTypeParentsId == null || objectTypeParentsId.Count == 0)
          return childType;
        for (int index = 0; index < objectTypeParentsId.Count; ++index)
        {
          IMSObjectType objectType = this.GetObjectType(objectTypeParentsId[index]);
          if (objectType == null || this.IsLocalObjectType(objectType.ObjectTypeID))
            return childType;
          childType = objectType.ObjectTypeID;
        }
        return childType;
      }

      /// <summary>
      /// Попытаться отыскать общий нелокальный или абстрактный родительский тип для указанных типов,
      /// с условием, что они не являются локальными типами. Используется для оптимизации запросов
      /// в коллекции объектов и связей. Если общий тип найти нельзя, возвращается значение
      /// Intermech.Consts.UnknownObjectTypeId
      /// </summary>
      /// <param name="childType1">Первый дочерний тип объекта</param>
      /// <param name="childType2">Второй дочерний тип объекта</param>
      /// <returns>Общий нелокальный или абстрактный родительский тип для указанных типов,
      /// с условием, что они не являются локальными типами. Используется для оптимизации запросов
      /// в коллекции объектов и связей. Если общий тип найти нельзя, возвращается значение
      /// Intermech.Consts.UnknownObjectTypeId</returns>
      public int GetCommonParentObjectTypeID(int childType1, int childType2)
      {
        if (childType1 == -1 || childType2 == -1 || this.IsLocalObjectType(childType1) || this.IsLocalObjectType(childType2))
          return -1;
        childType1 = this.GetTopParentObjectTypeID(childType1);
        childType2 = this.GetTopParentObjectTypeID(childType2);
        return childType1 == childType2 ? childType1 : -1;
      }

      /// <summary>Попытаться отыскать общий родительский тип для указанных типов.
      /// Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</summary>
      /// <param name="objectTypes">Перечисление идентификаторов типов объектов</param>
      /// <returns>Общий указанных типов. Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</returns>
      public int GetCommonParentObjectTypeID(IEnumerable<int> objectTypes)
      {
        using (IEnumerator<int> enumerator = objectTypes.GetEnumerator())
        {
          if (!enumerator.MoveNext())
            return -1;
          int parentObjectTypeId = enumerator.Current;
          while (parentObjectTypeId != -1 && enumerator.MoveNext())
          {
            int current = enumerator.Current;
            if (current == -1)
              return -1;
            if (current != parentObjectTypeId && !this.IsObjectTypeChildOf(current, parentObjectTypeId))
            {
              if (this.IsObjectTypeChildOf(parentObjectTypeId, current))
              {
                parentObjectTypeId = current;
              }
              else
              {
                do
                {
                  parentObjectTypeId = this.GetObjectTypeParentID(parentObjectTypeId);
                }
                while (parentObjectTypeId != -1 && this.IsObjectTypeChildOf(current, parentObjectTypeId));
              }
            }
          }
          return parentObjectTypeId;
        }
      }

      /// <summary>Попытаться отыскать общий родительский тип для указанных объектов.
      /// Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</summary>
      /// <param name="objectVersionIDs">Перечисление идентификаторов версий объектов</param>
      /// <returns>Общий указанных типов. Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</returns>
      public int GetCommonParentObjectTypeID(IEnumerable<long> objectVersionIDs)
      {
        List<long> versionIDs = objectVersionIDs.ToList<long>();
        return versionIDs.Any<long>() ? Session.Invoke<int>((Session.SessionHandler<int>) (session => this.GetCommonParentObjectTypeID(versionIDs.Where<long>((System.Func<long, bool>) (objectVersionID => objectVersionID != 0L)).Select<long, QuickObjectInfo>(new System.Func<long, QuickObjectInfo>(session.GetObjectInfo)).Where<QuickObjectInfo>((System.Func<QuickObjectInfo, bool>) (objectInfo => !objectInfo.Empty)).Select<QuickObjectInfo, int>((System.Func<QuickObjectInfo, int>) (objectInfo => objectInfo.ObjectTypeID)).Distinct<int>()))) : -1;
      }

      /// <summary>
      /// Оптимизировать список (удалить вложенные нелокальные дочерние типы объектов, если в списке есть их родительские типы
      /// </summary>
      /// <param name="childObjectTypes">Список дочерних типов объектов для типизированного запроса в коллекцию связей</param>
      /// <returns>Оптимизированный список типов дочерних объектов</returns>
      public List<int> OptimizeChildObjectTypes(IEnumerable<int> childObjectTypes)
      {
        childObjectTypes = childObjectTypes ?? (IEnumerable<int>) new List<int>();
        List<int> intList1 = new List<int>();
        foreach (int childObjectType in childObjectTypes)
        {
          int childType = childObjectType;
          if (this.IsLocalObjectType(childType) && !intList1.Contains(childType))
            intList1.Add(childType);
          else if (!childObjectTypes.Any<int>((System.Func<int, bool>) (parent => parent != childType && this.IsObjectTypeChildOf(childType, parent) && !MetaDataHelper.IsLocalObjectType(parent))) && !intList1.Contains(childType))
            intList1.Add(childType);
        }
        List<int> commonTypes = new List<int>(intList1.Count);
        intList1.ForEach((Action<int>) (childType =>
        {
          int parentObjectTypeId = this.GetTopParentObjectTypeID(childType);
          if (commonTypes.Contains(parentObjectTypeId))
            return;
          commonTypes.Add(parentObjectTypeId);
        }));
        List<int> intList2 = commonTypes;
        List<int> intList3 = new List<int>(intList2.Count);
        List<int> intList4 = new List<int>(intList2.Count);
        for (int index1 = 0; index1 < intList2.Count; ++index1)
        {
          int parentObjectTypeId = intList2[index1];
          if (this.IsLocalObjectType(parentObjectTypeId))
          {
            if (intList4.IndexOf(parentObjectTypeId) < 0)
              intList4.Add(parentObjectTypeId);
            if (intList3.IndexOf(parentObjectTypeId) < 0)
              intList3.Add(parentObjectTypeId);
          }
          else if (intList3.IndexOf(parentObjectTypeId) < 0)
          {
            int num = -1;
            for (int index2 = index1 + 1; index2 < intList2.Count; ++index2)
            {
              num = this.GetCommonParentObjectTypeID(parentObjectTypeId, intList2[index2]);
              if (num != -1)
              {
                if (intList4.IndexOf(num) < 0)
                  intList4.Add(num);
                if (intList3.IndexOf(parentObjectTypeId) < 0)
                  intList3.Add(parentObjectTypeId);
                if (intList3.IndexOf(intList2[index2]) < 0)
                  intList3.Add(intList2[index2]);
                if (intList3.IndexOf(num) < 0)
                {
                  intList3.Add(num);
                  break;
                }
                break;
              }
            }
            if (index1 == intList2.Count - 1)
              parentObjectTypeId = this.GetTopParentObjectTypeID(intList2[index1]);
            if (num == -1)
            {
              if (intList4.IndexOf(parentObjectTypeId) < 0)
                intList4.Add(parentObjectTypeId);
              if (intList3.IndexOf(parentObjectTypeId) < 0)
                intList3.Add(parentObjectTypeId);
            }
          }
        }
        return intList4;
      }

      /// <summary>
      /// Получить Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectType">Идентификатор родительского типа объектов</param>
      /// <returns>Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернет -1 - если тип объекта или связи не найден</returns>
      public int GetDefaultRelationTypeID(int parentObjectType)
      {
        lock (this._syncRootObjectTypes)
        {
          IMSObjectType imsObjectType;
          if (this._objectTypes.TryGetValue(parentObjectType, out imsObjectType))
            return imsObjectType.DefaultRelation;
        }
        return -1;
      }

      /// <summary>
      /// Получить Guid типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectType">Идентификатор родительского типа объектов</param>
      /// <returns>Guid типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернет Guid.Empty - если тип объекта или связи не найден</returns>
      public Guid GetDefaultRelationTypeGuid(int parentObjectType)
      {
        lock (this._syncRootObjectTypes)
        {
          IMSObjectType imsObjectType;
          return this.GetRelationTypeGuid(this._objectTypes.TryGetValue(parentObjectType, out imsObjectType) ? imsObjectType.DefaultRelation : -1);
        }
      }

      /// <summary>
      /// Получить Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернет -1 - если тип объекта или связи не найден</returns>
      public int GetDefaultRelationTypeID(Guid parentObjectTypeGuid)
      {
        lock (this._syncRootObjectTypes)
        {
          int key;
          if (!this._objectsGuid2Id.TryGetValue(parentObjectTypeGuid, out key))
            key = -1;
          IMSObjectType imsObjectType;
          if (this._objectTypes.TryGetValue(key, out imsObjectType))
            return imsObjectType.DefaultRelation;
        }
        return -1;
      }

      /// <summary>
      /// Получить Guid типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Guid типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернет Guid.Empty - если тип объекта или связи не найден</returns>
      public Guid GetDefaultRelationTypeGuid(Guid parentObjectTypeGuid)
      {
        lock (this._syncRootObjectTypes)
        {
          int key;
          if (!this._objectsGuid2Id.TryGetValue(parentObjectTypeGuid, out key))
            key = -1;
          IMSObjectType imsObjectType;
          return this.GetRelationTypeGuid(this._objectTypes.TryGetValue(key, out imsObjectType) ? imsObjectType.DefaultRelation : -1);
        }
      }

      /// <summary>Получить список описаний всех типов объектов</summary>
      /// <returns>Список описаний всех типов объектов</returns>
      public List<IMSObjectType> GetObjectTypesList()
      {
        lock (this._syncRootObjectTypes)
        {
          IMSObjectType[] imsObjectTypeArray = new IMSObjectType[this._objectTypes.Count];
          this._objectTypes.Values.CopyTo(imsObjectTypeArray, 0);
          return new List<IMSObjectType>((IEnumerable<IMSObjectType>) imsObjectTypeArray);
        }
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе связи
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если тип связи существует</returns>
      public bool ExistsRelationType(int relTypeID)
      {
        lock (this._syncRootRelationTypes)
          return this.RelationTypes.ContainsKey(relTypeID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе связи
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если тип связи существует</returns>
      public bool ExistsRelationType(Guid relTypeGuid)
      {
        return this.ExistsRelationType(this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>Получить краткую информацию о типе связи</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Краткая информация о типе связи или null</returns>
      public IMSRelationType GetRelationType(int relTypeID)
      {
        lock (this._syncRootRelationTypes)
        {
          IMSRelationType relationType;
          if (this.RelationTypes.TryGetValue(relTypeID, out relationType))
            return relationType;
        }
        return (IMSRelationType) null;
      }

      /// <summary>Получить краткую информацию о типе связи</summary>
      /// <param name="relTypeGuid">Идентификатор типа связи</param>
      /// <returns>Краткая информация о типе связи или null</returns>
      public IMSRelationType GetRelationType(Guid relTypeGuid)
      {
        return this.GetRelationType(this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>
      /// Получить название типа связи (например, "Проектная связь")
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Название типа связи (например, "")</returns>
      public string GetRelationTypeName(int relTypeID)
      {
        lock (this._syncRootRelationTypes)
        {
          IMSRelationType imsRelationType;
          if (this.RelationTypes.TryGetValue(relTypeID, out imsRelationType))
            return imsRelationType.Description;
        }
        return string.Empty;
      }

      /// <summary>
      /// Получить название типа связи (например, "Проектная связь")
      /// </summary>
      /// <param name="relTypeGuid">Идентификатор типа связи</param>
      /// <returns>Название типа связи (например, "Проектная связь")</returns>
      public string GetRelationTypeName(Guid relTypeGuid)
      {
        return this.GetRelationTypeName(this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>Объединить списки с применяемостями</summary>
      /// <param name="main">Главный список</param>
      /// <param name="source">Список, из которого данные следует добавить в главный список</param>
      private void MergeApplicabilitiesLists(List<IMSApplicability> main, List<IMSApplicability> source)
      {
        if (main == null || source == null)
          return;
        main.AddRange((IEnumerable<IMSApplicability>) source);
      }

      /// <summary>
      /// Получить список всех типов объектов имеющих допустимые типы связей
      /// </summary>
      /// <returns>Список всех типов объектов имеющих допустимые типы связей </returns>
      public List<int> GetObjectTypesWithApplicabilities()
      {
        List<int> withApplicabilities = new List<int>();
        lock (this._syncRootRelationTypes)
          withApplicabilities.AddRange((IEnumerable<int>) this._applicabilities.Keys);
        return withApplicabilities;
      }

      /// <summary>
      /// Получить список всех дочерних типов объектов имеющих допустимые типы связей с родительскими типами
      /// </summary>
      /// <returns></returns>
      public List<int> GetObjectTypesWithEnterInApplicabilities()
      {
        List<int> list = new List<int>();
        lock (this._syncRootRelationTypes)
          list.AddRange(this._applicabilities.Values.SelectMany<List<IMSApplicability>, IMSApplicability>((System.Func<List<IMSApplicability>, IEnumerable<IMSApplicability>>) (item => (IEnumerable<IMSApplicability>) item)).Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (item => item.ChildObjectTypeID)));
        GenericListHelper.MakeUnique<int>(list);
        return list;
      }

      /// <summary>
      /// Получить список допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список допустимых типов связей для указанного родительского типа объектов или null</returns>
      public List<IMSApplicability> GetObjectTypeApplicabilities(int objTypeID)
      {
        List<IMSApplicability> main = new List<IMSApplicability>();
        lock (this._syncRootRelationTypes)
        {
          List<IMSApplicability> source;
          if (this._applicabilities.TryGetValue(objTypeID, out source))
            this.MergeApplicabilitiesLists(main, source);
        }
        this.MergeApplicabilitiesLists(main, this.GetParentApplicabilities(objTypeID));
        return main;
      }

      /// <summary>
      /// Получить список допустимых типов связей для указанного дочернего типа объектов
      /// </summary>
      /// <param name="partTypeID">Идентификатор дочернего типа объекта</param>
      /// <returns>Список допустимых типов связей для указанного дочернего типа объекта или null</returns>
      public List<IMSApplicability> GetObjectTypeParentApplicabilities(int partTypeId)
      {
        List<IMSApplicability> parentApplicabilities = new List<IMSApplicability>();
        lock (this._syncRootRelationTypes)
        {
          foreach (List<IMSApplicability> source in this._applicabilities.Values)
            parentApplicabilities.AddRange(source.Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (item => this.IsObjectTypeChildOf(partTypeId, item.ChildObjectTypeID))));
        }
        return parentApplicabilities;
      }

      /// <summary>
      /// Проверить, может ли указанный дочерний тип объекта входить хотя бы
      /// в один родительский тип хотя бы одним типом связи
      /// </summary>
      /// <param name="partTypeID">id дочернего типа объекта</param>
      /// <returns>true - объект может входить в состав родительского, false - объект не может входить в состав родительского</returns>
      public bool CanEntersIn(int partTypeID)
      {
        lock (this._syncRootRelationTypes)
        {
          foreach (List<IMSApplicability> imsApplicabilityList in this._applicabilities.Values)
          {
            foreach (IMSApplicability imsApplicability in imsApplicabilityList)
            {
              if (this.IsObjectTypeChildOf(partTypeID, imsApplicability.ChildObjectTypeID))
                return true;
            }
          }
        }
        return false;
      }

      /// <summary>
      /// Получить список допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список допустимых типов связей для указанного родительского типа объектов или null</returns>
      public List<IMSApplicability> GetObjectTypeApplicabilities(Guid objTypeGuid)
      {
        return this.GetObjectTypeApplicabilities(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список идентификаторов допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список идентификаторов допустимых типов связей для указанного родительского типа объектов</returns>
      public List<int> GetApplicabilityRelationTypesID(int objTypeID)
      {
        List<int> applicabilityRelationTypesId = new List<int>();
        List<IMSApplicability> typeApplicabilities = this.GetObjectTypeApplicabilities(objTypeID);
        for (int index = 0; index < typeApplicabilities.Count; ++index)
        {
          if (!applicabilityRelationTypesId.Contains(typeApplicabilities[index].RelationTypeID))
            applicabilityRelationTypesId.Add(typeApplicabilities[index].RelationTypeID);
        }
        return applicabilityRelationTypesId;
      }

      /// <summary>
      /// Получить список идентификаторов допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список идентификаторов допустимых типов связей для указанного родительского типа объектов</returns>
      public List<int> GetApplicabilityRelationTypesID(Guid objTypeGuid)
      {
        return this.GetApplicabilityRelationTypesID(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список Guid допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список Guid допустимых типов связей для указанного родительского типа объектов</returns>
      public List<Guid> GetApplicabilityRelationTypesGuids(int objTypeID)
      {
        List<Guid> relationTypesGuids = new List<Guid>();
        List<IMSApplicability> typeApplicabilities = this.GetObjectTypeApplicabilities(objTypeID);
        lock (this._syncRootRelationTypes)
        {
          for (int index = 0; index < typeApplicabilities.Count; ++index)
          {
            Guid guid = this.RelationTypes[typeApplicabilities[index].RelationTypeID].Guid;
            if (!relationTypesGuids.Contains(guid))
              relationTypesGuids.Add(guid);
          }
        }
        return relationTypesGuids;
      }

      /// <summary>
      /// Получить список Guid допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список Guid допустимых типов связей для указанного родительского типа объектов</returns>
      public List<Guid> GetApplicabilityRelationTypesGuids(Guid objTypeGuid)
      {
        return this.GetApplicabilityRelationTypesGuids(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Проверить, допустимо ли включить указанный дочерний тип объекта в указанный
      /// родительский тип объекта по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Родительский тип объекта</param>
      /// <param name="childObjTypeID">Дочерний тип объекта</param>
      /// <param name="relTypeID">Тип связи</param>
      /// <returns>true - такая связь допустима</returns>
      public bool HasApplicability(int parObjTypeID, int childObjTypeID, int relTypeID)
      {
        return this.GetApplicability(parObjTypeID, childObjTypeID, relTypeID) != null;
      }

      /// <summary>
      /// Проверить, может ли входить в состав указанного родительского типа объекта
      /// хотя бы один дочерний тип объектов хотя бы одним типом связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true - у объекта может быть состав, false - у объекта не может быть состав</returns>
      public bool HasApplicability(Guid parObjTypeGuid)
      {
        return this.HasApplicability(this.GetObjectTypeID(parObjTypeGuid));
      }

      /// <summary>
      /// Проверить, может ли входить в состав указанного родительского типа объекта
      /// хотя бы один дочерний тип объектов хотя бы одним типом связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объекта</param>
      /// <returns>true - у объекта может быть состав, false - у объекта не может быть состав</returns>
      public bool HasApplicability(int parObjTypeID)
      {
        lock (this._syncRootRelationTypes)
          return this.GetObjectTypeApplicabilities(parObjTypeID).Count > 0;
      }

      /// <summary>
      /// Получить список описаний дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список описаний дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<IMSObjectType> GetApplicabilityChildObjectTypes(int parObjTypeID, int relTypeID)
      {
        lock (this._syncRootRelationTypes)
        {
          if (!this.RelationTypes.ContainsKey(relTypeID))
            return (List<IMSObjectType>) null;
          List<IMSObjectType> childObjectTypes = new List<IMSObjectType>();
          List<IMSApplicability> typeApplicabilities = this.GetObjectTypeApplicabilities(parObjTypeID);
          for (int index = 0; index < typeApplicabilities.Count; ++index)
          {
            IMSApplicability imsApplicability = typeApplicabilities[index];
            if (imsApplicability.RelationTypeID == relTypeID)
            {
              IMSObjectType objectType = this.GetObjectType(imsApplicability.ChildObjectTypeID);
              childObjectTypes.Add(objectType);
            }
          }
          List<IMSObjectType> imsObjectTypeList = new List<IMSObjectType>();
          for (int index1 = childObjectTypes.Count - 1; index1 >= 0; --index1)
          {
            IMSObjectType objA = childObjectTypes[index1];
            for (int index2 = 0; index2 < childObjectTypes.Count; ++index2)
            {
              if (index1 != index2)
              {
                IMSObjectType objB = childObjectTypes[index2];
                if (this.IsObjectTypeChildOf(objA.ObjectTypeID, objB.ObjectTypeID) && !this.IsLocalObjectType(objA.ObjectTypeID) && !object.Equals((object) objA, (object) objB) && imsObjectTypeList.IndexOf(objA) < 0)
                  imsObjectTypeList.Add(objA);
              }
            }
          }
          for (int index = 0; index < imsObjectTypeList.Count; ++index)
            childObjectTypes.Remove(imsObjectTypeList[index]);
          return childObjectTypes;
        }
      }

      /// <summary>
      /// Получить применяемость для указанного дочернего типа объектов в составе указанного
      /// родительского типа объектов по указанному типу связи
      /// Если для childObjTypeID применяемость не найдена, рекурсивно вверх искать применяемость для родительского
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="childObjTypeID">Идентификатор дочернего типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Применяемость или null</returns>
      public IMSApplicability GetApplicability(int parObjTypeID, int childObjTypeID, int relTypeID)
      {
        lock (this._syncRootRelationTypes)
        {
          this._tempApplicabilitiesKey.ParType = parObjTypeID;
          this._tempApplicabilitiesKey.ChildType = childObjTypeID;
          this._tempApplicabilitiesKey.RelType = relTypeID;
          IMSApplicability applicability;
          if (this._applicabilitiesCache.TryGetValue(this._tempApplicabilitiesKey, out applicability))
            return applicability;
          try
          {
            if (!this.RelationTypes.ContainsKey(relTypeID))
              return (IMSApplicability) null;
            List<IMSApplicability> typeApplicabilities = this.GetObjectTypeApplicabilities(parObjTypeID);
            for (int childTypeID = childObjTypeID; childTypeID != -1; childTypeID = this.GetObjectTypeParentID(childTypeID))
            {
              for (int index = 0; index < typeApplicabilities.Count; ++index)
              {
                IMSApplicability imsApplicability = typeApplicabilities[index];
                if (imsApplicability.RelationTypeID == relTypeID && imsApplicability.ChildObjectTypeID == childTypeID)
                {
                  applicability = imsApplicability;
                  return applicability;
                }
              }
            }
            return (IMSApplicability) null;
          }
          finally
          {
            this._applicabilitiesCache[(ApplicabilitiesKey) this._tempApplicabilitiesKey.Clone()] = applicability;
          }
        }
      }

      /// <summary>
      /// Получить список описаний дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список описаний дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<IMSObjectType> GetApplicabilityChildObjectTypes(Guid parObjTypeGuid, Guid relTypeGuid)
      {
        return this.GetApplicabilityChildObjectTypes(this.GetObjectTypeID(parObjTypeGuid), this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<int> GetApplicabilityChildObjectTypesID(int parObjTypeID, int relTypeID)
      {
        lock (this._syncRootRelationTypes)
        {
          List<int> childObjectTypesId = new List<int>();
          List<IMSApplicability> typeApplicabilities = this.GetObjectTypeApplicabilities(parObjTypeID);
          for (int index = 0; index < typeApplicabilities.Count; ++index)
          {
            IMSApplicability imsApplicability = typeApplicabilities[index];
            if (imsApplicability.ApplicabilityMode != ApplicabilityModes.Disabled && imsApplicability.RelationTypeID == relTypeID && !childObjectTypesId.Contains(imsApplicability.ChildObjectTypeID))
              childObjectTypesId.Add(imsApplicability.ChildObjectTypeID);
          }
          return childObjectTypesId;
        }
      }

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeIDs">Идентификаторы типов связей</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<int> GetApplicabilityChildObjectTypesID(int parObjTypeID, IEnumerable<int> relTypeIDs)
      {
        if (relTypeIDs == null || !relTypeIDs.Any<int>())
          return new List<int>();
        List<int> childObjectTypesId1 = new List<int>();
        foreach (int relTypeId in relTypeIDs)
        {
          List<int> childObjectTypesId2 = this.GetApplicabilityChildObjectTypesID(parObjTypeID, relTypeId);
          for (int index = 0; index < childObjectTypesId2.Count; ++index)
          {
            if (!childObjectTypesId1.Contains(childObjectTypesId2[index]))
              childObjectTypesId1.Add(childObjectTypesId2[index]);
          }
        }
        return childObjectTypesId1;
      }

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<int> GetApplicabilityChildObjectTypesID(Guid parObjTypeGuid, Guid relTypeGuid)
      {
        return this.GetApplicabilityChildObjectTypesID(this.GetObjectTypeID(parObjTypeGuid), this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuids">Guid типов связей</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<int> GetApplicabilityChildObjectTypesID(
        Guid parObjTypeGuid,
        IEnumerable<Guid> relTypeGuids)
      {
        return relTypeGuids == null || !relTypeGuids.Any<Guid>() ? new List<int>() : this.GetApplicabilityChildObjectTypesID(this.GetObjectTypeID(parObjTypeGuid), relTypeGuids.Select<Guid, int>((System.Func<Guid, int>) (item => this.GetRelationTypeID(item))));
      }

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<Guid> GetApplicabilityChildObjectTypesGuid(int parObjTypeID, int relTypeID)
      {
        lock (this._syncRootRelationTypes)
        {
          if (!this.RelationTypes.ContainsKey(relTypeID))
            return (List<Guid>) null;
          List<Guid> childObjectTypesGuid = new List<Guid>();
          List<IMSApplicability> typeApplicabilities = this.GetObjectTypeApplicabilities(parObjTypeID);
          for (int index = 0; index < typeApplicabilities.Count; ++index)
          {
            IMSApplicability imsApplicability = typeApplicabilities[index];
            if (imsApplicability.RelationTypeID == relTypeID)
            {
              IMSObjectType objectType = this.GetObjectType(imsApplicability.ChildObjectTypeID);
              childObjectTypesGuid.Add(objectType.Guid);
            }
          }
          List<Guid> guidList = new List<Guid>();
          for (int index1 = childObjectTypesGuid.Count - 1; index1 >= 0; --index1)
          {
            Guid guid = childObjectTypesGuid[index1];
            for (int index2 = 0; index2 < childObjectTypesGuid.Count; ++index2)
            {
              if (index1 != index2)
              {
                Guid parentType = childObjectTypesGuid[index2];
                if (this.IsObjectTypeChildOf(guid, parentType) && !this.IsLocalObjectType(guid) && guid != parentType && guidList.IndexOf(guid) < 0)
                  guidList.Add(guid);
              }
            }
          }
          for (int index = 0; index < guidList.Count; ++index)
            childObjectTypesGuid.Remove(guidList[index]);
          return childObjectTypesGuid;
        }
      }

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeIDs">Список идентификаторов типов связей</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<Guid> GetApplicabilityChildObjectTypesGuid(
        int parObjTypeID,
        IEnumerable<int> relTypeIDs)
      {
        if (relTypeIDs == null || !relTypeIDs.Any<int>())
          return new List<Guid>();
        List<Guid> childObjectTypesGuid1 = new List<Guid>();
        foreach (int relTypeId in relTypeIDs)
        {
          List<Guid> childObjectTypesGuid2 = this.GetApplicabilityChildObjectTypesGuid(parObjTypeID, relTypeId);
          for (int index = 0; index < childObjectTypesGuid2.Count; ++index)
          {
            if (!childObjectTypesGuid1.Contains(childObjectTypesGuid2[index]))
              childObjectTypesGuid1.Add(childObjectTypesGuid2[index]);
          }
        }
        return childObjectTypesGuid1;
      }

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<Guid> GetApplicabilityChildObjectTypesGuid(Guid parObjTypeGuid, Guid relTypeGuid)
      {
        return this.GetApplicabilityChildObjectTypesGuid(this.GetObjectTypeID(parObjTypeGuid), this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuids">Список Guid типов связей</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public List<Guid> GetApplicabilityChildObjectTypesGuid(
        Guid parObjTypeGuid,
        IEnumerable<Guid> relTypeGuids)
      {
        return relTypeGuids == null || !relTypeGuids.Any<Guid>() ? new List<Guid>() : this.GetApplicabilityChildObjectTypesGuid(this.GetObjectTypeID(parObjTypeGuid), relTypeGuids.Select<Guid, int>((System.Func<Guid, int>) (item => this.GetRelationTypeID(item))));
      }

      /// <summary>
      /// Проверить, разрешен ли указанный родительский тип объектов,
      /// если есть списки разрешенных и запрещенных родительских типов объектов.
      /// Метод учитывает иерархию типов объектов для последовательного поиска, в какой
      /// из списков раньше попадет проверяемый тип объекта, либо его родительские типы
      /// </summary>
      /// <param name="parentObjType">Проверяемый родительский тип объекта</param>
      /// <param name="enabledParents">Список разрешенных родительских типов объектов</param>
      /// <param name="disabledParents">Список запрещенных родительских типов объектов</param>
      /// <param name="defValue">Значение по умолчанию, если информации в списках оказалось недостаточно</param>
      /// <returns>true - применяемость с указанным родительским типом разрешена</returns>
      public bool IsEnabledParentType(
        int parentObjType,
        IEnumerable<int> enabledParents,
        IEnumerable<int> disabledParents,
        bool defValue)
      {
        if (parentObjType == -1)
          return false;
        enabledParents = enabledParents ?? (IEnumerable<int>) new List<int>();
        disabledParents = disabledParents ?? (IEnumerable<int>) new List<int>();
        if (!enabledParents.Any<int>() && !disabledParents.Any<int>())
          return defValue;
        for (; parentObjType != -1; parentObjType = this.GetObjectTypeParentID(parentObjType))
        {
          if (disabledParents.Contains<int>(parentObjType))
            return false;
          if (enabledParents.Contains<int>(parentObjType))
            return true;
        }
        return defValue;
      }

      /// <summary>Поддерживает ли указанный тип связи ручную сортировку</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает ручную сортировку</returns>
      public bool HasRelationTypeSorting(int relTypeID)
      {
        lock (this._syncRootSpecialRelationTypes)
          return this._specialSortedRelations.IndexOf(relTypeID) >= 0;
      }

      /// <summary>Поддерживает ли указанный тип связи ручную сортировку</summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает ручную сортировку</returns>
      public bool HasRelationTypeSorting(Guid relTypeGuid)
      {
        return this.HasRelationTypeSorting(this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов связей, поддерживающих ручную сортировку
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов связей, поддерживающих ручную сортировку</returns>
      public List<int> GetSpecialSortingRelationsIDs()
      {
        lock (this._syncRootSpecialRelationTypes)
        {
          List<int> sortingRelationsIds = new List<int>(this._specialSortedRelations.Count);
          for (int index = 0; index < this._specialSortedRelations.Count; ++index)
            sortingRelationsIds.Add(this._specialSortedRelations[index]);
          return sortingRelationsIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов связей, поддерживающих ручную сортировку
      /// </summary>
      /// <returns>Список Guid идентификаторов типов связей, поддерживающих ручную сортировку</returns>
      public List<Guid> GetSpecialSortingRelationsGuids()
      {
        lock (this._syncRootSpecialRelationTypes)
        {
          List<Guid> sortingRelationsGuids = new List<Guid>(this._specialSortedRelations.Count);
          for (int index = 0; index < this._specialSortedRelations.Count; ++index)
            sortingRelationsGuids.Add(this.GetRelationTypeGuid(this._specialSortedRelations[index]));
          return sortingRelationsGuids;
        }
      }

      /// <summary>
      /// Поддерживает ли указанный тип связи работу с допустимыми заменами
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает работу с допустимыми заменами</returns>
      public bool HasRelationTypeSubstitutes(int relTypeID)
      {
        lock (this._syncRootSpecialRelationTypes)
          return this._specialSubstitutesRelations.IndexOf(relTypeID) >= 0;
      }

      /// <summary>
      /// Поддерживает ли указанный тип связи работу с допустимыми заменами
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает работу с допустимыми заменами</returns>
      public bool HasRelationTypeSubstitutes(Guid relTypeGuid)
      {
        return this.HasRelationTypeSubstitutes(this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов связей, позволяющих работу с допустимыми заменами
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов связей, позволяющих работу с допустимыми заменами</returns>
      public List<int> GetSpecialSubstitutesRelationsIDs()
      {
        lock (this._syncRootSpecialRelationTypes)
        {
          List<int> substitutesRelationsIds = new List<int>(this._specialSubstitutesRelations.Count);
          for (int index = 0; index < this._specialSubstitutesRelations.Count; ++index)
            substitutesRelationsIds.Add(this._specialSubstitutesRelations[index]);
          return substitutesRelationsIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов связей, позволяющих работу с допустимыми заменами
      /// </summary>
      /// <returns>Список Guid идентификаторов типов связей, позволяющих работу с допустимыми заменами</returns>
      public List<Guid> GetSpecialSubstitutesRelationsGuids()
      {
        lock (this._syncRootSpecialRelationTypes)
        {
          List<Guid> substitutesRelationsGuids = new List<Guid>(this._specialSubstitutesRelations.Count);
          for (int index = 0; index < this._specialSubstitutesRelations.Count; ++index)
            substitutesRelationsGuids.Add(this.GetRelationTypeGuid(this._specialSubstitutesRelations[index]));
          return substitutesRelationsGuids;
        }
      }

      /// <summary>
      /// Поддерживает ли указанный тип связи группирование объектов
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает группирование объектов</returns>
      public bool HasRelationTypeGrouping(int relTypeID)
      {
        lock (this._syncRootSpecialRelationTypes)
          return this._specialGroupingRelations.IndexOf(relTypeID) >= 0;
      }

      /// <summary>
      /// Поддерживает ли указанный тип связи группирование объектов
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает группирование объектов</returns>
      public bool HasRelationTypeGrouping(Guid relTypeGuid)
      {
        return this.HasRelationTypeGrouping(this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов группирующих типов связей
      /// </summary>
      /// <returns>Список Int32-идентификаторов группирующих типов связей</returns>
      public List<int> GetSpecialGroupingRelationsIDs()
      {
        lock (this._syncRootSpecialRelationTypes)
        {
          List<int> groupingRelationsIds = new List<int>(this._specialGroupingRelations.Count);
          for (int index = 0; index < this._specialGroupingRelations.Count; ++index)
            groupingRelationsIds.Add(this._specialGroupingRelations[index]);
          return groupingRelationsIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов группирующих типов связей
      /// </summary>
      /// <returns>Список Guid идентификаторов группирующих типов связей</returns>
      public List<Guid> GetSpecialGroupingRelationsGuids()
      {
        lock (this._syncRootSpecialRelationTypes)
        {
          List<Guid> groupingRelationsGuids = new List<Guid>(this._specialGroupingRelations.Count);
          for (int index = 0; index < this._specialGroupingRelations.Count; ++index)
            groupingRelationsGuids.Add(this.GetRelationTypeGuid(this._specialGroupingRelations[index]));
          return groupingRelationsGuids;
        }
      }

      /// <summary>
      /// Проверить, является ли указанный тип связи конфигурируемым
      /// </summary>
      /// <param name="relType">Проверяемый тип связи</param>
      /// <returns>true - тип связи допускает конфигурирование составов</returns>
      public bool IsPdmConfigurableRelationType(int relType)
      {
        lock (this._syncRootSpecialRelationTypes)
        {
          bool flag;
          if (this._specialConfigurableRelationTypes.TryGetValue(relType, out flag))
            return flag;
        }
        bool flag1 = false;
        try
        {
          if (this.GetAttribute4RelationType(relType, this.GetAttributeTypeID("cad015a6-306c-11d8-b4e9-00304f19f545")) == null || this.GetAttribute4RelationType(relType, this.GetAttributeTypeID("cad015ac-306c-11d8-b4e9-00304f19f545")) == null)
            return false;
          flag1 = true;
          return true;
        }
        finally
        {
          lock (this._syncRootSpecialRelationTypes)
            this._specialConfigurableRelationTypes[relType] = flag1;
        }
      }

      /// <summary>
      /// Проверить, является ли указанный тип связи частично конфигурируемым
      /// (в наличии есть атрибут "Контекст конфигуратора составов")
      /// </summary>
      /// <param name="relType">Проверяемый тип связи</param>
      /// <returns>true - тип связи допускает частичное конфигурирование составов</returns>
      public bool IsPdmPartiallyConfigurableRelationType(int relType)
      {
        lock (this._syncRootSpecialRelationTypes)
        {
          bool flag;
          if (this._specialPartiallyConfigurableRelationTypes.TryGetValue(relType, out flag))
            return flag;
        }
        bool flag1 = false;
        try
        {
          if (this.GetAttribute4RelationType(relType, this.GetAttributeTypeID("cad015a6-306c-11d8-b4e9-00304f19f545")) == null)
            return false;
          flag1 = true;
          return true;
        }
        finally
        {
          lock (this._syncRootSpecialRelationTypes)
            this._specialPartiallyConfigurableRelationTypes[relType] = flag1;
        }
      }

      /// <summary>Получить список описаний всех типов связей</summary>
      /// <returns>Список описаний всех типов связей</returns>
      public List<IMSRelationType> GetRelationTypesList()
      {
        lock (this._syncRootRelationTypes)
        {
          IMSRelationType[] imsRelationTypeArray = new IMSRelationType[this.RelationTypes.Count];
          this.RelationTypes.Values.CopyTo(imsRelationTypeArray, 0);
          return new List<IMSRelationType>((IEnumerable<IMSRelationType>) imsRelationTypeArray);
        }
      }

      /// <summary>Проверить, является ли тип объектов локальным</summary>
      /// <param name="type">Идентификатор типа объектов</param>
      /// <returns>true - тип объектов является локальным</returns>
      public bool IsLocalObjectType(int type)
      {
        IMSObjectType objectType = this.GetObjectType(type);
        return objectType != null && objectType.IsLocalType;
      }

      /// <summary>Проверить, является ли тип объектов локальным</summary>
      /// <param name="type">Идентификатор типа объектов</param>
      /// <returns>true - тип объектов является локальным</returns>
      public bool IsLocalObjectType(Guid type)
      {
        IMSObjectType objectType = this.GetObjectType(type);
        return objectType != null && objectType.IsLocalType;
      }

      /// <summary>
      /// Проверить, есть ли в списке хотя бы один основной или вложенный локальный тип объектов
      /// </summary>
      /// <param name="types">Список идентификаторов типов объектов</param>
      /// <returns>true - найден основной или вложенный локальный тип объектов</returns>
      public bool HasLocalObjectType(IEnumerable<int> types)
      {
        if (types == null || !types.Any<int>())
          return false;
        List<int> childrenIdRecursive = this.GetLocalObjectTypeChildrenIDRecursive(types);
        for (int index = 0; index < childrenIdRecursive.Count; ++index)
        {
          if (this.IsLocalObjectType(childrenIdRecursive[index]))
            return true;
        }
        return false;
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, участвующие в допустимых заменах
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, участвующие в допустимых заменах</returns>
      public bool HasObjectTypeSubstRelTypes(int objTypeID)
      {
        lock (this._syncRootSpecialObjectTypes)
          return this._specialSubstitutesObjectTypes.IndexOf(objTypeID) >= 0;
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, участвующие в допустимых заменах
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, участвующие в допустимых заменах</returns>
      public bool HasObjectTypeSubstRelTypes(Guid objTypeGuid)
      {
        return this.HasObjectTypeSubstRelTypes(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, участвующих в допустимых заменах
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, участвующих в допустимых заменах</returns>
      public List<int> GetSubstituteObjectsIDs()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<int> substituteObjectsIds = new List<int>(this._specialSubstitutesObjectTypes.Count);
          for (int index = 0; index < this._specialSubstitutesObjectTypes.Count; ++index)
            substituteObjectsIds.Add(this._specialSubstitutesObjectTypes[index]);
          return substituteObjectsIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, участвующих в допустимых заменах
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, участвующих в допустимых заменах</returns>
      public List<Guid> GetSubstituteObjectsGuids()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<Guid> substituteObjectsGuids = new List<Guid>(this._specialSubstitutesObjectTypes.Count);
          for (int index = 0; index < this._specialSubstitutesObjectTypes.Count; ++index)
            substituteObjectsGuids.Add(this.GetObjectTypeGuid(this._specialSubstitutesObjectTypes[index]));
          return substituteObjectsGuids;
        }
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, позволяющие выполнять ручную сортировку
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, позволяющие выполнять ручную сортировку</returns>
      public bool HasObjectTypeSortingRelTypes(int objTypeID)
      {
        lock (this._syncRootSpecialObjectTypes)
          return this._specialSortedObjectTypes.IndexOf(objTypeID) >= 0;
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, позволяющие выполнять ручную сортировку
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, позволяющие выполнять ручную сортировку</returns>
      public bool HasObjectTypeSortingRelTypes(Guid objTypeGuid)
      {
        return this.HasObjectTypeSortingRelTypes(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать связи с сортировкой
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать связи с сортировкой</returns>
      public List<int> GetSortingObjectsIDs()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<int> sortingObjectsIds = new List<int>(this._specialSortedObjectTypes.Count);
          for (int index = 0; index < this._specialSortedObjectTypes.Count; ++index)
            sortingObjectsIds.Add(this._specialSortedObjectTypes[index]);
          return sortingObjectsIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать связи с сортировкой
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать связи с сортировкой</returns>
      public List<Guid> GetSortingObjectsGuids()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<Guid> sortingObjectsGuids = new List<Guid>(this._specialSortedObjectTypes.Count);
          for (int index = 0; index < this._specialSortedObjectTypes.Count; ++index)
            sortingObjectsGuids.Add(this.GetObjectTypeGuid(this._specialSortedObjectTypes[index]));
          return sortingObjectsGuids;
        }
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи типа "Состав изделия"
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи типа "Состав изделия"</returns>
      public bool HasObjectTypeDesignedRelType(int objTypeID)
      {
        lock (this._syncRootSpecialObjectTypes)
          return this._specialDesignedObjectTypes.IndexOf(objTypeID) >= 0;
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи типа "Состав изделия"
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи типа "Состав изделия"</returns>
      public bool HasObjectTypeDesignedRelType(Guid objTypeGuid)
      {
        return this.HasObjectTypeDesignedRelType(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"</returns>
      public List<int> GetDesignedObjectsIDs()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<int> designedObjectsIds = new List<int>(this._specialDesignedObjectTypes.Count);
          for (int index = 0; index < this._specialDesignedObjectTypes.Count; ++index)
            designedObjectsIds.Add(this._specialDesignedObjectTypes[index]);
          return designedObjectsIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"</returns>
      public List<Guid> GetDesignedObjectsGuids()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<Guid> designedObjectsGuids = new List<Guid>(this._specialDesignedObjectTypes.Count);
          for (int index = 0; index < this._specialDesignedObjectTypes.Count; ++index)
            designedObjectsGuids.Add(this.GetObjectTypeGuid(this._specialDesignedObjectTypes[index]));
          return designedObjectsGuids;
        }
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать группирующие связи и сам является группирующим
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать группирующие связи и сам является группирующим</returns>
      public bool HasObjectTypeGroupingRelTypes(int objTypeID)
      {
        lock (this._syncRootSpecialObjectTypes)
          return this._specialGroupingObjectTypes.IndexOf(objTypeID) >= 0;
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать группирующие связи и сам является группирующим
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать группирующие связи и сам является группирующим</returns>
      public bool HasObjectTypeGroupingRelTypes(Guid objTypeGuid)
      {
        return this.HasObjectTypeGroupingRelTypes(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов группирующих типов объектов
      /// </summary>
      /// <returns>Список Int32-идентификаторов группирующих типов объектов</returns>
      public List<int> GetSpecialGroupingIDs()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<int> specialGroupingIds = new List<int>(this._specialGroupingObjectTypes.Count);
          for (int index = 0; index < this._specialGroupingObjectTypes.Count; ++index)
            specialGroupingIds.Add(this._specialGroupingObjectTypes[index]);
          return specialGroupingIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов группирующих типов объектов
      /// </summary>
      /// <returns>Список Guid идентификаторов группирующих типов объектов</returns>
      public List<Guid> GetSpecialGroupingGuids()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<Guid> specialGroupingGuids = new List<Guid>(this._specialGroupingObjectTypes.Count);
          for (int index = 0; index < this._specialGroupingObjectTypes.Count; ++index)
            specialGroupingGuids.Add(this.GetObjectTypeGuid(this._specialGroupingObjectTypes[index]));
          return specialGroupingGuids;
        }
      }

      /// <summary>
      /// Может ли указанный тип объекта входить в состав группирующих объектов
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может входить в состав группирующих объектов</returns>
      public bool HasObjectTypeGrouppedRelTypes(int objTypeID)
      {
        lock (this._syncRootSpecialObjectTypes)
          return this._specialGrouppedObjectTypes.IndexOf(objTypeID) >= 0;
      }

      /// <summary>
      /// Может ли указанный тип объекта входить в состав группирующих объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта может входить в состав группирующих объектов</returns>
      public bool HasObjectTypeGrouppedRelTypes(Guid objTypeGuid)
      {
        return this.HasObjectTypeGrouppedRelTypes(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут входить в состав группирующих объектов
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут входить в состав группирующих объектов</returns>
      public List<int> GetSpecialGrouppedIDs()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<int> specialGrouppedIds = new List<int>(this._specialGrouppedObjectTypes.Count);
          for (int index = 0; index < this._specialGrouppedObjectTypes.Count; ++index)
            specialGrouppedIds.Add(this._specialGrouppedObjectTypes[index]);
          return specialGrouppedIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут входить в состав группирующих объектов
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут входить в состав группирующих объектов</returns>
      public List<Guid> GetSpecialGrouppedGuids()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<Guid> specialGrouppedGuids = new List<Guid>(this._specialGrouppedObjectTypes.Count);
          for (int index = 0; index < this._specialGrouppedObjectTypes.Count; ++index)
            specialGrouppedGuids.Add(this.GetObjectTypeGuid(this._specialGrouppedObjectTypes[index]));
          return specialGrouppedGuids;
        }
      }

      /// <summary>
      /// Может ли указанный тип объекта содержать атрибут "Видимость объекта"
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный тип объекта может содержать атрибут "Видимость объекта"</returns>
      public bool HasObjectTypeVisibilityAttr(int objTypeID)
      {
        lock (this._syncRootSpecialObjectTypes)
          return this._specialVisibilityObjectTypes.IndexOf(objTypeID) >= 0;
      }

      /// <summary>
      /// Может ли указанный тип объекта содержать атрибут "Видимость объекта"
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта может содержать атрибут "Видимость объекта"</returns>
      public bool HasObjectTypeVisibilityAttr(Guid objTypeGuid)
      {
        return this.HasObjectTypeVisibilityAttr(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"</returns>
      public List<int> GetVisibilityObjectsIDs()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<int> visibilityObjectsIds = new List<int>(this._specialVisibilityObjectTypes.Count);
          for (int index = 0; index < this._specialVisibilityObjectTypes.Count; ++index)
            visibilityObjectsIds.Add(this._specialVisibilityObjectTypes[index]);
          return visibilityObjectsIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"</returns>
      public List<Guid> GetVisibilityObjectsGuids()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<Guid> visibilityObjectsGuids = new List<Guid>(this._specialVisibilityObjectTypes.Count);
          for (int index = 0; index < this._specialVisibilityObjectTypes.Count; ++index)
            visibilityObjectsGuids.Add(this.GetObjectTypeGuid(this._specialVisibilityObjectTypes[index]));
          return visibilityObjectsGuids;
        }
      }

      /// <summary>
      /// Проверка на необходимость включения версии объектов указанного типа в контекст, при условии что он доступен в сессии
      /// (без проверки на наличие другой версии объекта в контексте)
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="objectType">Тип проверяемого объекта</param>
      /// <param name="customFunc">Кастом функция позволяющая переопределить поведение для определенных типов объектов</param>
      /// <returns>true - данный объект необходимо добавлять в текущий контекст редактирования (без проверки на наличие другой версии объекта в контексте)</returns>
      public bool MustAppendVersionToEditingContext(
        IUserSession session,
        int objectType,
        Func<EditingContextMode> customFunc = null)
      {
        bool editingContext = false;
        IMSObjectType objectType1 = this.GetObjectType(objectType);
        EditingContextMode editingContextMode;
        if (customFunc != null)
        {
          int num = (int) customFunc();
          editingContextMode = customFunc();
        }
        else
          editingContextMode = session.EditingContextMode;
        if (objectType1 == null || objectType1.VersionsMode != ObjectVersionModes.MultiVersion || editingContextMode == EditingContextMode.AutoUpdate && (objectType1.Options & ObjectTypeOptions.AutoContextEnabled) != ObjectTypeOptions.AutoContextEnabled)
          return false;
        if (editingContextMode == EditingContextMode.AutoUpdate && session.EditingContextID != 0L)
          editingContext = true;
        return editingContext;
      }

      /// <summary>
      /// Проверить, является ли указанный тип объектов-контекстов упрощенным контекстом
      /// (не меняет содержимое номера группы изменений у контекстных объектов, не может
      /// быть связанным, допускает применение в своем содержимом версий объектов, принадлежащих
      /// другим контекстам редактирования)
      /// </summary>
      /// <param name="contextTypeID">Идентификатор типа объекта-контекста</param>
      /// <returns>true - указанный тип объекта является упрощенным контекстом</returns>
      public bool IsSimpleEditingContext(int contextTypeID)
      {
        if (this._objtypeProdOrders == -1)
          this._objtypeProdOrders = this.GetObjectTypeID("cadd92e9-306c-11d8-b4e9-00304f19f545");
        return this.IsObjectTypeChildOf(contextTypeID, this._objtypeProdOrders) || this.IsObjectTypeChildOf(contextTypeID, Constants.CompositionSelectionContextObjectTypeId);
      }

      /// <summary>
      /// Является ли указанный тип объекта контекстом редактирования
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный тип объекта является контекстом редактирования</returns>
      public bool IsObjectTypeEditingContext(int objTypeID)
      {
        lock (this._syncRootSpecialObjectTypes)
          return this.SpecialContextObjectTypes.IndexOf(objTypeID) >= 0;
      }

      /// <summary>
      /// Является ли указанный тип объекта контекстом редактирования
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта является контекстом редактирования</returns>
      public bool IsObjectTypeEditingContext(Guid objTypeGuid)
      {
        return this.IsObjectTypeEditingContext(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые являются контекстами редактирования</returns>
      public List<int> GetEditingContextObjectsIDs()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<int> contextObjectsIds = new List<int>(this.SpecialContextObjectTypes.Count);
          for (int index = 0; index < this.SpecialContextObjectTypes.Count; ++index)
            contextObjectsIds.Add(this.SpecialContextObjectTypes[index]);
          return contextObjectsIds;
        }
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования</returns>
      public List<int> GetEditingContextTopObjectsIDs()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<int> contextTopObjectsIds = new List<int>(this._specialTopContextObjectTypes.Count);
          for (int index = 0; index < this._specialTopContextObjectTypes.Count; ++index)
            contextTopObjectsIds.Add(this._specialTopContextObjectTypes[index]);
          return contextTopObjectsIds;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые являются контекстами редактирования</returns>
      public List<Guid> GetEditingContextObjectsGuids()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<Guid> contextObjectsGuids = new List<Guid>(this.SpecialContextObjectTypes.Count);
          for (int index = 0; index < this.SpecialContextObjectTypes.Count; ++index)
            contextObjectsGuids.Add(this.GetObjectTypeGuid(this.SpecialContextObjectTypes[index]));
          return contextObjectsGuids;
        }
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования</returns>
      public List<Guid> GetEditingContextTopObjectsGuids()
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          List<Guid> contextTopObjectsGuids = new List<Guid>(this._specialTopContextObjectTypes.Count);
          for (int index = 0; index < this._specialTopContextObjectTypes.Count; ++index)
            contextTopObjectsGuids.Add(this.GetObjectTypeGuid(this._specialTopContextObjectTypes[index]));
          return contextTopObjectsGuids;
        }
      }

      /// <summary>
      /// Проверить, можно ли добавлять указанный тип объекта в контекст редактирования
      /// </summary>
      /// <param name="objTypeGuid">Guid проверяемого типа объекта</param>
      /// <param name="autoMode">Включен ли режим автоматического пополнения</param>
      /// <returns>true - указанный тип объекта допускается добавлять в контекст редактирования</returns>
      public bool CanAddObjTypeToEditingContext(Guid objTypeGuid, bool autoMode)
      {
        return this.CanAddObjTypeToEditingContext(this.GetObjectTypeID(objTypeGuid), autoMode);
      }

      /// <summary>
      /// Проверить, можно ли добавлять указанный тип объекта в контекст редактирования
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <param name="autoMode">Включен ли режим автоматического пополнения</param>
      /// <returns>true - указанный тип объекта допускается добавлять в контекст редактирования</returns>
      public bool CanAddObjTypeToEditingContext(int objType, bool autoMode)
      {
        IMSObjectType objectType = this.GetObjectType(objType);
        return objectType != null && !this.IsObjectTypeEditingContext(objType) && (!autoMode || (objectType.Options & ObjectTypeOptions.AutoContextEnabled) == ObjectTypeOptions.AutoContextEnabled);
      }

      /// <summary>
      /// Проверить, является ли указанный тип объекта корнем конфигурируемого состава
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта может являться корнем конфигурируемого состава</returns>
      public bool IsPdmRootObjectType(int objType)
      {
        return this.IsObjectTypeChildOf(objType, this.GetObjectTypeID("cad015b1-306c-11d8-b4e9-00304f19f545")) || this.IsObjectTypeChildOf(objType, this.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545")) || this.IsObjectTypeChildOf(objType, this.GetObjectTypeID("cadd92e9-306c-11d8-b4e9-00304f19f545"));
      }

      /// <summary>
      /// Проверить, является ли указанный тип объекта конфигурируемым
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта допускает конфигурирование составов</returns>
      public bool IsPdmConfigurableObjectType(int objType)
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          bool flag;
          if (this._specialConfigurableObjectTypes.TryGetValue(objType, out flag))
            return flag;
        }
        bool flag1 = false;
        try
        {
          if (this.GetAttribute4ObjectType(objType, this.GetAttributeTypeID("cad015a9-306c-11d8-b4e9-00304f19f545")) == null || this.GetAttribute4ObjectType(objType, this.GetAttributeTypeID("cad015a1-306c-11d8-b4e9-00304f19f545")) == null || this.GetAttribute4ObjectType(objType, this.GetAttributeTypeID("cad015ab-306c-11d8-b4e9-00304f19f545")) == null)
            return false;
          flag1 = true;
          return true;
        }
        finally
        {
          lock (this._syncRootSpecialObjectTypes)
            this._specialConfigurableObjectTypes[objType] = flag1;
        }
      }

      /// <summary>
      /// Проверить, может ли указанный тип объекта выступать в роли контекста конфигуратора составов
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта может выступать в роли контекста конфигуратора составов</returns>
      public bool IsPdmContextableObjectType(int objType)
      {
        lock (this._syncRootSpecialObjectTypes)
        {
          bool flag;
          if (this._specialContextableObjectTypes.TryGetValue(objType, out flag))
            return flag;
        }
        bool flag1 = false;
        try
        {
          if (this.GetAttribute4ObjectType(objType, this.GetAttributeTypeID("cad015a6-306c-11d8-b4e9-00304f19f545")) == null || this.GetAttribute4ObjectType(objType, this.GetAttributeTypeID("cad015a3-306c-11d8-b4e9-00304f19f545")) == null)
            return false;
          flag1 = true;
          return true;
        }
        finally
        {
          lock (this._syncRootSpecialObjectTypes)
            this._specialContextableObjectTypes[objType] = flag1;
        }
      }

      /// <summary>
      /// Получить из кэша или из базы данных тип указанной связи. Если не задавать
      /// значение session, то значение будет получено из кэша. Если в кэше значения
      /// нет, вернется -1. Если задать значение session, то будет выполнено обращение
      /// к базе данных, а новое значение будет помещено в кэш (при необходимости - поверх
      /// старого значения)
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="prjLinkID">Идентификатор связи, тип которой требуется получить</param>
      /// <returns>Идентификатор типа указанной связи или -1</returns>
      public int GetRelationType4PrjLinkID(IUserSession session, long prjLinkID)
      {
        if (prjLinkID == 0L)
          return -1;
        if (session == null)
        {
          lock (this._syncRootRelationsPrjLinkTypes)
          {
            int relationType4PrjLinkId;
            if (this._relationsPrjLinkTypes.TryGetValue(prjLinkID, out relationType4PrjLinkId))
            {
              ++this._counterRelationsPrjLinkTypesHit;
              return relationType4PrjLinkId;
            }
            ++this._counterRelationsPrjLinkTypesMiss;
            return -1;
          }
        }
        ++this._counterRelationsPrjLinkTypesMiss;
        IDBRelation relation = session.GetRelation(prjLinkID, false);
        if (relation == null)
          return -1;
        lock (this._syncRootRelationsPrjLinkTypes)
        {
          this._relationsPrjLinkTypes[prjLinkID] = relation.RelationType;
          return this._relationsPrjLinkTypes[prjLinkID];
        }
      }

      /// <summary>
      /// Получить Int32-идентификатор типа атрибута по его имени, Guid или числовому идентификатору.
      /// Сгенерирует исключение, если в метод засунуть объект некорректного типа
      /// </summary>
      /// <param name="attributeID">Имя атрибута, Guid или числовой идентификатор</param>
      /// <returns>Int32-идентификатор или Intermech.Consts.NavigatorUndefinedAttributeID, если тип атрибута не найден</returns>
      public int GetAttributeID(object attributeID)
      {
        switch (attributeID)
        {
          case null:
            return -10000;
          case ObligatoryObjectAttributes _:
            return (int) attributeID;
          case int attributeId2:
            return attributeId2;
          case Guid attrTypeGuid:
            return this.GetAttributeTypeID(attrTypeGuid);
          case string _:
            string str = (string) attributeID;
            int attributeId1 = this.GetAttributeByTypeNameID(str);
            if (attributeId1 == -10000 && GuidHelper.IsGuid(str))
              attributeId1 = this.GetAttributeTypeID(new Guid(str));
            return attributeId1;
          default:
            return Convert.ToInt32(attributeID);
        }
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе атрибута
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если тип атрибута существует</returns>
      public bool ExistsAttributeType(int attrTypeID)
      {
        lock (this._syncRootAttrTypes)
          return this.AttrTypes.ContainsKey(attrTypeID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе атрибута
      /// </summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если тип атрибута существует</returns>
      public bool ExistsAttributeType(Guid attrTypeGuid)
      {
        return this.ExistsAttributeType(this.GetAttributeTypeID(attrTypeGuid));
      }

      /// <summary>Получить краткую информацию о типе атрибута</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Краткая информация о типе атрибута или null</returns>
      public IMSAttributeType GetAttributeType(int attrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeType attributeType;
          if (this.AttrTypes.TryGetValue(attrTypeID, out attributeType))
            return attributeType;
        }
        return (IMSAttributeType) null;
      }

      /// <summary>Хранятся ли в атрибуте системные данные</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если в атрибуте хранятся системные данные</returns>
      public bool HasAttributeSystemData(int attrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeType imsAttributeType;
          if (this.AttrTypes.TryGetValue(attrTypeID, out imsAttributeType))
            return imsAttributeType.FieldType == FieldTypes.ftSystem;
        }
        return false;
      }

      /// <summary>Хранятся ли в атрибуте системные данные</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если в атрибуте хранятся системные данные</returns>
      public bool HasAttributeSystemData(Guid attrTypeGuid)
      {
        return this.HasAttributeSystemData(this.GetAttributeTypeID(attrTypeGuid));
      }

      /// <summary>Хранится ли в атрибуте список допустимых значений</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если в атрибуте хранится список допустимых значений</returns>
      public bool HasAttributePossibleValues(int attrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeType imsAttributeType;
          if (this.AttrTypes.TryGetValue(attrTypeID, out imsAttributeType))
            return imsAttributeType.MultiValueMode == MultiValueModes.SingleValueFromList;
        }
        return false;
      }

      /// <summary>Хранится ли в атрибуте список допустимых значений</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если в атрибуте хранится список допустимых значений</returns>
      public bool HasAttributePossibleValues(Guid attrTypeGuid)
      {
        return this.HasAttributePossibleValues(this.GetAttributeTypeID(attrTypeGuid));
      }

      /// <summary>Можно ли отображать атрибут</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если атрибут можно отображать</returns>
      public bool IsAttributeGridable(int attrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeType imsAttributeType;
          if (this.AttrTypes.TryGetValue(attrTypeID, out imsAttributeType))
            return imsAttributeType.IsGridable;
        }
        return false;
      }

      /// <summary>Можно ли отображать атрибут</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если атрибут можно отображать</returns>
      public bool IsAttributeGridable(Guid attrTypeGuid)
      {
        return this.IsAttributeGridable(this.GetAttributeTypeID(attrTypeGuid));
      }

      /// <summary>Получить краткую информацию о типе атрибута</summary>
      /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
      /// <returns>Краткая информация о типе атрибута или null</returns>
      public IMSAttributeType GetAttributeType(Guid attrTypeGuid)
      {
        return this.GetAttributeType(this.GetAttributeTypeID(attrTypeGuid));
      }

      /// <summary>Получить название типа атрибута</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Название типа атрибута</returns>
      public string GetAttributeTypeName(int attrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeType imsAttributeType;
          if (this.AttrTypes.TryGetValue(attrTypeID, out imsAttributeType))
            return imsAttributeType.Name;
        }
        return string.Empty;
      }

      /// <summary>Получить название типа атрибута</summary>
      /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
      /// <returns>Название типа атрибута</returns>
      public string GetAttributeTypeName(Guid attrTypeGuid)
      {
        return this.GetAttributeTypeName(this.GetAttributeTypeID(attrTypeGuid));
      }

      /// <summary>
      /// Получить по Guid типа атрибута его Int32-идентификатор
      /// </summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>Идентификатор типа атрибута. -1 - тип атрибута не найден</returns>
      public int GetAttributeTypeID(Guid attrTypeGuid)
      {
        lock (this._syncRootAttrTypes)
        {
          int attributeTypeId;
          if (this._attrsGuid2Id.TryGetValue(attrTypeGuid, out attributeTypeId))
            return attributeTypeId;
        }
        return -10000;
      }

      /// <summary>
      /// Получить по Int32-идентификатору типа атрибута его Guid-идентификатор
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Идентификатор типа атрибута. Guid.Empty - тип атрибута не найден</returns>
      public Guid GetAttributeTypeGuid(int attrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeType imsAttributeType;
          if (this.AttrTypes.TryGetValue(attrTypeID, out imsAttributeType))
            return imsAttributeType.AttributeGuid;
        }
        return Guid.Empty;
      }

      /// <summary>
      /// Возвращает идентификатор типа атрибута по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа атрибута в виде строки</param>
      public int GetAttributeTypeID(string Guid) => this.GetAttributeTypeID(new Guid(Guid));

      /// <summary>
      /// Возвращает идентификатор типа атрибута по его названию
      /// </summary>
      /// <param name="attrName">Название типа атрибута</param>
      public int GetAttributeByTypeNameID(string attrName)
      {
        lock (this._syncRootAttrTypes)
        {
          int attributeByTypeNameId;
          if (this.AttrNameTypes.TryGetValue(attrName.ToUpperInvariant(), out attributeByTypeNameId))
            return attributeByTypeNameId;
        }
        return -10000;
      }

      /// <summary>Возвращает Guid типа атрибута по его названию</summary>
      /// <param name="attrName">Название типа атрибута</param>
      public Guid GetAttributeByTypeNameGuid(string attrName)
      {
        lock (this._syncRootAttrTypes)
        {
          int key;
          if (this.AttrNameTypes.TryGetValue(attrName.ToUpperInvariant(), out key))
          {
            IMSAttributeType imsAttributeType;
            if (this.AttrTypes.TryGetValue(key, out imsAttributeType))
              return imsAttributeType.AttributeGuid;
          }
        }
        return Guid.Empty;
      }

      /// <summary>Получить список всех типов атрибутов</summary>
      /// <returns>Список всех типов атрибутов</returns>
      public List<int> GetAttributeTypesIDList()
      {
        lock (this._syncRootAttrTypes)
        {
          int[] numArray = new int[this.AttrTypes.Count];
          this.AttrTypes.Keys.CopyTo(numArray, 0);
          return new List<int>((IEnumerable<int>) numArray);
        }
      }

      /// <summary>Получить список Guid всех типов атрибутов</summary>
      /// <returns>Список Guid всех типов атрибутов</returns>
      public List<Guid> GetAttributeTypesGuidList()
      {
        lock (this._syncRootAttrTypes)
        {
          List<Guid> attributeTypesGuidList = new List<Guid>(this.AttrTypes.Count);
          foreach (KeyValuePair<int, IMSAttributeType> attrType in this.AttrTypes)
            attributeTypesGuidList.Add(attrType.Value.AttributeGuid);
          return attributeTypesGuidList;
        }
      }

      /// <summary>Получить список описаний всех типов атрибутов</summary>
      /// <returns>Список описаний всех типов атрибутов</returns>
      public List<IMSAttributeType> GetAttributeTypesList()
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeType[] imsAttributeTypeArray = new IMSAttributeType[this.AttrTypes.Count];
          this.AttrTypes.Values.CopyTo(imsAttributeTypeArray, 0);
          return new List<IMSAttributeType>((IEnumerable<IMSAttributeType>) imsAttributeTypeArray);
        }
      }

      /// <summary>
      /// Получить список описаний атрибута для всех типов объектов, которым он назначен
      /// </summary>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов объектов, которым он назначен</returns>
      public List<IMSAttribute4ObjectType> GetAllAttributes4ObjectTypeList(Guid AttrTypeGuid)
      {
        return this.GetAllAttributes4ObjectTypeList(this.GetAttributeTypeID(AttrTypeGuid));
      }

      /// <summary>
      /// Получить список описаний атрибута для всех типов объектов, которым он назначен
      /// </summary>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов объектов, которым он назначен</returns>
      public List<IMSAttribute4ObjectType> GetAllAttributes4ObjectTypeList(int AttrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          List<IMSAttribute4ObjectType> attribute4ObjectTypeList;
          if (!this._attrs4ObjectTypes.TryGetValue(AttrTypeID, out attribute4ObjectTypeList))
            return new List<IMSAttribute4ObjectType>();
          IMSAttribute4ObjectType[] attribute4ObjectTypeArray = new IMSAttribute4ObjectType[attribute4ObjectTypeList.Count];
          attribute4ObjectTypeList.CopyTo(attribute4ObjectTypeArray, 0);
          return new List<IMSAttribute4ObjectType>((IEnumerable<IMSAttribute4ObjectType>) attribute4ObjectTypeArray);
        }
      }

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа объекта
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа объекта</returns>
      public List<IMSAttribute4ObjectType> GetAttribute4ObjectTypeList(Guid objTypeGuid)
      {
        return this.GetAttribute4ObjectTypeList(this.GetObjectTypeID(objTypeGuid));
      }

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeID">Идентификатор типа объекта</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа объекта</returns>
      public List<IMSAttribute4ObjectType> GetAttribute4ObjectTypeList(int ObjectTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          List<IMSAttribute4ObjectType> attribute4ObjectTypeList;
          if (!this._attr4ObjectTypes.TryGetValue(ObjectTypeID, out attribute4ObjectTypeList))
            return new List<IMSAttribute4ObjectType>();
          IMSAttribute4ObjectType[] attribute4ObjectTypeArray = new IMSAttribute4ObjectType[attribute4ObjectTypeList.Count];
          attribute4ObjectTypeList.CopyTo(attribute4ObjectTypeArray, 0);
          return new List<IMSAttribute4ObjectType>((IEnumerable<IMSAttribute4ObjectType>) attribute4ObjectTypeArray);
        }
      }

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeGuid">Guid типа объекта</param>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Описание типа атрибута для указанного типа объекта, или null</returns>
      public IMSAttribute4ObjectType GetAttribute4ObjectType(Guid ObjectTypeGuid, Guid AttrTypeGuid)
      {
        return this.GetAttribute4ObjectType(this.GetObjectTypeID(ObjectTypeGuid), this.GetAttributeTypeID(AttrTypeGuid));
      }

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeID">Идентификатор типа объекта</param>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Описание типа атрибута для указанного типа объекта, или null</returns>
      public IMSAttribute4ObjectType GetAttribute4ObjectType(int ObjectTypeID, int AttrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          List<IMSAttribute4ObjectType> attribute4ObjectTypeList;
          if (!this._attr4ObjectTypes.TryGetValue(ObjectTypeID, out attribute4ObjectTypeList))
            return (IMSAttribute4ObjectType) null;
          foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
          {
            if (attribute4ObjectType.AttributeID == AttrTypeID)
              return attribute4ObjectType;
          }
          return (IMSAttribute4ObjectType) null;
        }
      }

      /// <summary>
      /// Получить список описаний атрибута для всех типов связей, которым он назначен
      /// </summary>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов связей, которым он назначен</returns>
      public List<IMSAttribute4RelationType> GetAllAttributes4RelationTypeList(Guid AttrTypeGuid)
      {
        return this.GetAllAttributes4RelationTypeList(this.GetAttributeTypeID(AttrTypeGuid));
      }

      /// <summary>
      /// Получить список описаний атрибута для всех типов связей, которым он назначен
      /// </summary>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов связей, которым он назначен</returns>
      public List<IMSAttribute4RelationType> GetAllAttributes4RelationTypeList(int AttrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          List<IMSAttribute4RelationType> attribute4RelationTypeList;
          if (!this._attrs4RelationTypes.TryGetValue(AttrTypeID, out attribute4RelationTypeList))
            return new List<IMSAttribute4RelationType>();
          IMSAttribute4RelationType[] attribute4RelationTypeArray = new IMSAttribute4RelationType[attribute4RelationTypeList.Count];
          attribute4RelationTypeList.CopyTo(attribute4RelationTypeArray, 0);
          return new List<IMSAttribute4RelationType>((IEnumerable<IMSAttribute4RelationType>) attribute4RelationTypeArray);
        }
      }

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа связи
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа связи</returns>
      public List<IMSAttribute4RelationType> GetAttribute4RelationTypeList(Guid relTypeGuid)
      {
        return this.GetAttribute4RelationTypeList(this.GetRelationTypeID(relTypeGuid));
      }

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа связи
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа связи</returns>
      public List<IMSAttribute4RelationType> GetAttribute4RelationTypeList(int relTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          List<IMSAttribute4RelationType> attribute4RelationTypeList;
          if (!this._attr4RelationTypes.TryGetValue(relTypeID, out attribute4RelationTypeList))
            return new List<IMSAttribute4RelationType>();
          IMSAttribute4RelationType[] attribute4RelationTypeArray = new IMSAttribute4RelationType[attribute4RelationTypeList.Count];
          attribute4RelationTypeList.CopyTo(attribute4RelationTypeArray, 0);
          return new List<IMSAttribute4RelationType>((IEnumerable<IMSAttribute4RelationType>) attribute4RelationTypeArray);
        }
      }

      /// <summary>
      /// Получить описание типа атрибута для указанного типа связи
      /// </summary>
      /// <param name="RelationTypeGuid">Guid типа связи</param>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Описание типа атрибута для указанного типа связи, или null</returns>
      public IMSAttribute4RelationType GetAttribute4RelationType(
        Guid RelationTypeGuid,
        Guid AttrTypeGuid)
      {
        return this.GetAttribute4RelationType(this.GetRelationTypeID(RelationTypeGuid), this.GetAttributeTypeID(AttrTypeGuid));
      }

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="RelationTypeID">Идентификатор типа объекта</param>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Описание типа атрибута для указанного типа объекта, или null</returns>
      public IMSAttribute4RelationType GetAttribute4RelationType(int RelationTypeID, int AttrTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          List<IMSAttribute4RelationType> attribute4RelationTypeList;
          if (!this._attr4RelationTypes.TryGetValue(RelationTypeID, out attribute4RelationTypeList))
            return (IMSAttribute4RelationType) null;
          foreach (IMSAttribute4RelationType attribute4RelationType in attribute4RelationTypeList)
          {
            if (attribute4RelationType.AttributeID == AttrTypeID)
              return attribute4RelationType;
          }
          return (IMSAttribute4RelationType) null;
        }
      }

      /// <summary>
      /// Получить список типов объектов, на которые может ссылаться указанный тип атрибута
      /// </summary>
      /// <param name="attrID">Идентификатор типа атрибута</param>
      /// <returns>Список типов объектов, на которые может ссылаться указанный тип атрибута.
      /// Пустой список - допускается ссылка на любой тип объектов,
      /// null - атрибут не является ссылочным</returns>
      public List<int> GetLinkedObjectTypes(int attrID)
      {
        lock (this._syncRootAttrTypes)
        {
          List<int> collection;
          if (!this._linkAttributeTypes.TryGetValue(attrID, out collection))
            return (List<int>) null;
          return collection.Count == 0 || collection.Count == 1 && collection[0] == -1 ? new List<int>() : new List<int>((IEnumerable<int>) collection);
        }
      }

      /// <summary>
      /// Получить список типов атрибутов, которые могут ссылаться на указанный тип объекта
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Список типов атрибутов, которые могут ссылаться на указанный тип объекта</returns>
      public List<int> GetLinkAttributeTypes(int objTypeID)
      {
        lock (this._syncRootAttrTypes)
        {
          List<int> intList = new List<int>();
          List<int> collection;
          if (this._linkAttributeTypesRev.TryGetValue(objTypeID, out collection))
            intList.AddRange((IEnumerable<int>) collection);
          if (this._linkAttributeTypesRev.TryGetValue(-1, out collection))
            intList.AddRange((IEnumerable<int>) collection);
          List<int> res = new List<int>();
          intList.ForEach((Action<int>) (objType =>
          {
            if (objType == 0 || res.IndexOf(objType) >= 0)
              return;
            res.Add(objType);
          }));
          return res;
        }
      }

      /// <summary>
      /// Получить по Guid группы атрибутов ее Int32-идентификатор
      /// </summary>
      /// <param name="attrGroupGuid">Guid типа атрибута</param>
      /// <returns>Идентификатор группы атрибутов. -1 - группа атрибутов не найдена</returns>
      public int GetAttributeGroupID(Guid attrGroupGuid)
      {
        lock (this._syncRootAttrTypes)
        {
          int attributeGroupId;
          if (this._attrGroupsGuid2Id.TryGetValue(attrGroupGuid, out attributeGroupId))
            return attributeGroupId;
        }
        return -1;
      }

      /// <summary>
      /// Получить по Int32-идентификатору группы атрибутов ее Guid-идентификатор
      /// </summary>
      /// <param name="attrGroupID">Идентификатор типа атрибута</param>
      /// <returns>Идентификатор группы атрибутов. Guid.Empty - группа атрибутов не найдена</returns>
      public Guid GetAttributeGroupGuid(int attrGroupID)
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeGroup imsAttributeGroup;
          if (this.AttrGroups.TryGetValue(attrGroupID, out imsAttributeGroup))
            return imsAttributeGroup.Guid;
        }
        return Guid.Empty;
      }

      /// <summary>
      /// Возвращает идентификатор группы атрибутов по строковому представлению ее глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid группы атрибутов в виде строки</param>
      public int GetAttributeGroupID(string Guid) => this.GetAttributeGroupID(new Guid(Guid));

      /// <summary>Получить по Guid группы атрибутов описание группы</summary>
      /// <param name="attrGroupGuid">Guid типа группы атрибутов</param>
      /// <returns>Описание группы атрибутов или null</returns>
      public IMSAttributeGroup GetAttributeGroup(Guid attrGroupGuid)
      {
        return this.GetAttributeGroup(this.GetAttributeGroupID(attrGroupGuid));
      }

      /// <summary>
      /// Получить по строковому Guid группы атрибутов описание группы
      /// </summary>
      /// <param name="Guid">Guid типа группы атрибутов в виде строки</param>
      /// <returns>Описание группы атрибутов или null</returns>
      public IMSAttributeGroup GetAttributeGroup(string Guid) => this.GetAttributeGroup(new Guid(Guid));

      /// <summary>Получить по ID группы атрибутов описание группы</summary>
      /// <param name="attrGroupID">ID типа группы атрибутов</param>
      /// <returns>Описание группы атрибутов или null</returns>
      public IMSAttributeGroup GetAttributeGroup(int attrGroupID)
      {
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeGroup attributeGroup;
          if (this.AttrGroups.TryGetValue(attrGroupID, out attributeGroup))
            return attributeGroup;
        }
        return (IMSAttributeGroup) null;
      }

      /// <summary>
      /// Получить список типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="guid">Guid группы атрибутов</param>
      /// <returns>Список типов атрибутов для указанной группы атрибутов</returns>
      public List<int> GetAttributesInGroup(Guid guid)
      {
        return this.GetAttributesInGroup(this.GetAttributeGroupID(guid));
      }

      /// <summary>
      /// Получить список типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="groupID">Идентификатор группы атрибутов: -1 для группы "Все атрибуты", -10 для группы "Назначенные типам" (собираются списки всех id атрибутов, которые назначены типам объектов и типам связей)</param>
      /// <returns>Список типов атрибутов для указанной группы атрибутов</returns>
      public List<int> GetAttributesInGroup(int groupID)
      {
        switch (groupID)
        {
          case -10:
            lock (this._syncRootAttrTypes)
            {
              List<int> attributesInGroup = new List<int>();
              ICollection<int> keys1 = (ICollection<int>) this._attrs4ObjectTypes.Keys;
              if (keys1 != null)
                attributesInGroup.AddRange((IEnumerable<int>) keys1);
              ICollection<int> keys2 = (ICollection<int>) this._attrs4RelationTypes.Keys;
              if (keys2 != null)
                attributesInGroup.AddRange((IEnumerable<int>) keys2);
              attributesInGroup.Sort();
              if (attributesInGroup.Count > 0)
              {
                int num = attributesInGroup[attributesInGroup.Count - 1];
                for (int index = attributesInGroup.Count - 2; index >= 0; --index)
                {
                  if (attributesInGroup[index] == num)
                    attributesInGroup.RemoveAt(index);
                  else
                    num = attributesInGroup[index];
                }
              }
              return attributesInGroup;
            }
          case -1:
            return this.GetAttributeTypesIDList();
          default:
            lock (this._syncRootAttrTypes)
            {
              List<int> collection;
              return !this.AttrInGroups.TryGetValue(groupID, out collection) ? new List<int>() : new List<int>((IEnumerable<int>) collection);
            }
        }
      }

      /// <summary>
      /// Получить список Guid типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="guid">Guid группы атрибутов</param>
      /// <returns>Список Guid типов атрибутов для указанной группы атрибутов</returns>
      public List<Guid> GetAttributesInGroupGuids(Guid guid)
      {
        return this.GetAttributesInGroupGuids(this.GetAttributeGroupID(guid));
      }

      /// <summary>
      /// Получить список Guid типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="groupID">Идентификатор группы атрибутов</param>
      /// <returns>Список Guid типов атрибутов для указанной группы атрибутов</returns>
      public List<Guid> GetAttributesInGroupGuids(int groupID)
      {
        if (groupID == -1)
          return this.GetAttributeTypesGuidList();
        lock (this._syncRootAttrTypes)
        {
          List<int> collection;
          return !this.AttrInGroups.TryGetValue(groupID, out collection) ? new List<Guid>() : new List<int>((IEnumerable<int>) collection).ConvertAll<Guid>((Converter<int, Guid>) (item => this.AttrTypes[item].AttributeGuid));
        }
      }

      /// <summary>
      /// Получить информацию о том, где применяется указанный тип атрибута
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Применяемость указанного типа атрибута</returns>
      public IMSAttributeTypeApplicability GetAttributeTypeApplicability(int attrTypeID)
      {
        if (attrTypeID == 0 || attrTypeID == -10000)
          return IMSAttributeTypeApplicability.None;
        lock (this._syncRootAttrTypes)
        {
          IMSAttributeTypeApplicability typeApplicability;
          return this.AttrsApplicability.TryGetValue(attrTypeID, out typeApplicability) ? typeApplicability : IMSAttributeTypeApplicability.None;
        }
      }

      /// <summary>
      /// Получить информацию о том, где применяется указанный тип атрибута
      /// </summary>
      /// <param name="attrTypeGuid">Уникальный глобальный идентификатор типа атрибута</param>
      /// <returns>Применяемость указанного типа атрибута</returns>
      public IMSAttributeTypeApplicability GetAttributeTypeApplicability(Guid attrTypeGuid)
      {
        return attrTypeGuid.Equals(Guid.Empty) ? IMSAttributeTypeApplicability.None : this.GetAttributeTypeApplicability(this.GetAttributeTypeID(attrTypeGuid));
      }

      /// <summary>
      /// Проверить, применяется ли указанный тип атрибута в типах объектов/связей
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true - указанный тип атрибута применяется в типах объектов/связей</returns>
      public bool IsAttributeInUse(int attrTypeID)
      {
        if (attrTypeID == 0 || attrTypeID == -10000)
          return false;
        lock (this._syncRootAttrTypes)
          return this.AttrsApplicability.ContainsKey(attrTypeID);
      }

      /// <summary>
      /// Проверить, применяется ли указанный тип атрибута в типах объектов/связей
      /// </summary>
      /// <param name="attrTypeGuid">Уникальный глобальный идентификатор типа атрибута</param>
      /// <returns>true - указанный тип атрибута применяется в типах объектов/связей</returns>
      public bool IsAttributeInUse(Guid attrTypeGuid)
      {
        return !attrTypeGuid.Equals(Guid.Empty) && this.IsAttributeInUse(this.GetAttributeTypeID(attrTypeGuid));
      }

      /// <summary>
      /// Получить список идентификаторов типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по идентификатору типа атрибута
      /// </summary>
      /// <returns>Список идентификаторов типов атрибутов, которые применяются в типах объектов/связей</returns>
      public List<int> GetUsedUnsortedAttributesIDs()
      {
        lock (this._syncRootAttrTypes)
          return new List<int>((IEnumerable<int>) this.AttrsApplicability.Keys);
      }

      /// <summary>
      /// Получить список идентификаторов типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по названию типа атрибута
      /// </summary>
      /// <returns>Список идентификаторов типов атрибутов, которые применяются в типах объектов/связей</returns>
      public List<int> GetUsedSortedAttributesIDs()
      {
        List<int> sortedAttributesIds;
        lock (this._syncRootAttrTypes)
          sortedAttributesIds = new List<int>((IEnumerable<int>) this.AttrsApplicability.Keys);
        sortedAttributesIds.Sort((IComparer<int>) new MetaDataHelperService.AttrTypeByCaptionComparer());
        return sortedAttributesIds;
      }

      /// <summary>
      /// Получить список описаний типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по названию типа атрибута
      /// </summary>
      /// <returns>Список описаний типов атрибутов, которые применяются в типах объектов/связей</returns>
      public List<IMSAttributeType> GetUsedSortedAttributes()
      {
        return this.GetUsedSortedAttributesIDs().ConvertAll<IMSAttributeType>(new Converter<int, IMSAttributeType>(this.GetAttributeType));
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанной схеме ЖЦ
      /// </summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>true, если указанная схема ЖЦ существует</returns>
      public bool ExistsLCSchema(int schemaID)
      {
        lock (this._syncRootLcSteps)
          return this._lcSchemes.ContainsKey(schemaID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанной схеме ЖЦ
      /// </summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>true, если указанная схема ЖЦ существует</returns>
      public bool ExistsLCSchema(Guid schemaGuid)
      {
        return this.ExistsLCSchema(this.GetLCSchemaID(schemaGuid));
      }

      /// <summary>Получить краткую информацию о схеме ЖЦ</summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Краткая информация о схеме ЖЦ или null</returns>
      public IMSLifeCycleScheme GetLCSchema(int schemaID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleScheme lcSchema;
          if (this._lcSchemes.TryGetValue(schemaID, out lcSchema))
            return lcSchema;
        }
        return (IMSLifeCycleScheme) null;
      }

      /// <summary>Получить краткую информацию о схеме ЖЦ</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Краткая информация о схеме ЖЦ или null</returns>
      public IMSLifeCycleScheme GetLCSchema(Guid schemaGuid)
      {
        return this.GetLCSchema(this.GetLCSchemaID(schemaGuid));
      }

      /// <summary>Получить название схемы ЖЦ</summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Название схемы ЖЦ</returns>
      public string GetLCSchemaName(int schemaID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleScheme imsLifeCycleScheme;
          if (this._lcSchemes.TryGetValue(schemaID, out imsLifeCycleScheme))
            return imsLifeCycleScheme.Name;
        }
        return string.Empty;
      }

      /// <summary>Получить название схемы ЖЦ</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Название схемы ЖЦ</returns>
      public string GetLCSchemaName(Guid schemaGuid)
      {
        return this.GetLCSchemaName(this.GetLCSchemaID(schemaGuid));
      }

      /// <summary>Получить по Guid схемы ЖЦ ее Int32-идентификатор</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Идентификатор схемы ЖЦ. -1 - схема не найдена</returns>
      public int GetLCSchemaID(Guid schemaGuid)
      {
        lock (this._syncRootLcSteps)
        {
          int lcSchemaId;
          if (this._lcSchemesGuid2Id.TryGetValue(schemaGuid, out lcSchemaId))
            return lcSchemaId;
        }
        return -1;
      }

      /// <summary>
      /// Получить по Int32-идентификатору схемы ЖЦ ее Guid-идентификатор
      /// </summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Идентификатор схемы ЖЦ. Guid.Empty - схема ЖЦ не найдена</returns>
      public Guid GetLCSchemaGuid(int schemaID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleScheme imsLifeCycleScheme;
          if (this._lcSchemes.TryGetValue(schemaID, out imsLifeCycleScheme))
            return imsLifeCycleScheme.Guid;
        }
        return Guid.Empty;
      }

      /// <summary>
      /// Возвращает идентификатор схемы ЖЦ по строковому представлению ее глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid схемы ЖЦ в виде строки</param>
      public int GetLCSchemaID(string Guid) => this.GetLCSchemaID(new Guid(Guid));

      /// <summary>Получить список описаний всех схем ЖЦ</summary>
      /// <returns>Список описаний всех схем ЖЦ</returns>
      public List<IMSLifeCycleScheme> GetLCSchemesList()
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleScheme[] imsLifeCycleSchemeArray = new IMSLifeCycleScheme[this._lcSchemes.Count];
          this._lcSchemes.Values.CopyTo(imsLifeCycleSchemeArray, 0);
          return new List<IMSLifeCycleScheme>((IEnumerable<IMSLifeCycleScheme>) imsLifeCycleSchemeArray);
        }
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном уровне продвижения
      /// </summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>true, если указанный уровень продвижения существует</returns>
      public bool ExistsLCLevel(int levelID)
      {
        lock (this._syncRootLcSteps)
          return this._lcLevels.ContainsKey(levelID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном уровне продвижения
      /// </summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>true, если  указанный уровень продвижения существует</returns>
      public bool ExistsLCLevel(Guid levelGuid) => this.ExistsLCLevel(this.GetLCLevelID(levelGuid));

      /// <summary>Получить краткую информацию об уровне продвижения</summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Краткая информация об уровне продвижения или null</returns>
      public IMSLifeCycleLevel GetLCLevel(int levelID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleLevel lcLevel;
          if (this._lcLevels.TryGetValue(levelID, out lcLevel))
            return lcLevel;
        }
        return (IMSLifeCycleLevel) null;
      }

      /// <summary>Получить краткую информацию об уровне продвижения</summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Краткая информация об уровне продвижения или null</returns>
      public IMSLifeCycleLevel GetLCLevel(Guid levelGuid)
      {
        return this.GetLCLevel(this.GetLCLevelID(levelGuid));
      }

      /// <summary>Получить название уровня продвижения</summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Название уровня продвижения</returns>
      public string GetLCLevelName(int levelID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleLevel imsLifeCycleLevel;
          if (this._lcLevels.TryGetValue(levelID, out imsLifeCycleLevel))
            return imsLifeCycleLevel.Name;
        }
        return string.Empty;
      }

      /// <summary>Получить название уровня продвижения</summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Название уровня продвижения</returns>
      public string GetLCLevelName(Guid levelGuid) => this.GetLCLevelName(this.GetLCLevelID(levelGuid));

      /// <summary>
      /// Получить по Guid уровня продвижения его Int32-идентификатор
      /// </summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Идентификатор уровня продвижения. -1 - уровень продвижения не найден</returns>
      public int GetLCLevelID(Guid levelGuid)
      {
        lock (this._syncRootLcSteps)
        {
          int lcLevelId;
          if (this._lcLevelsGuid2Id.TryGetValue(levelGuid, out lcLevelId))
            return lcLevelId;
        }
        return 0;
      }

      /// <summary>
      /// Получить по Int32-идентификатору уровня продвижения его Guid-идентификатор
      /// </summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Идентификатор уровня продвижения. Guid.Empty - уровень продвижения не найден</returns>
      public Guid GetLCLevelGuid(int levelID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleLevel imsLifeCycleLevel;
          if (this._lcLevels.TryGetValue(levelID, out imsLifeCycleLevel))
            return imsLifeCycleLevel.Guid;
        }
        return Guid.Empty;
      }

      /// <summary>
      /// Возвращает идентификатор уровня продвижения по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid уровня продвижения в виде строки</param>
      public int GetLCLevelID(string Guid) => this.GetLCLevelID(new Guid(Guid));

      /// <summary>Получить список описаний всех уровней продвижения</summary>
      /// <returns>Список описаний всех уровней продвижения</returns>
      public List<IMSLifeCycleLevel> GetLCLevelsList()
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleLevel[] imsLifeCycleLevelArray = new IMSLifeCycleLevel[this._lcLevels.Count];
          this._lcLevels.Values.CopyTo(imsLifeCycleLevelArray, 0);
          return new List<IMSLifeCycleLevel>((IEnumerable<IMSLifeCycleLevel>) imsLifeCycleLevelArray);
        }
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном шаге ЖЦ
      /// </summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>true, если указанный шаг ЖЦ существует</returns>
      public bool ExistsLCStep(int lcstepID)
      {
        lock (this._syncRootLcSteps)
          return this._lcSteps.ContainsKey(lcstepID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном шаге ЖЦ
      /// </summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>true, если  указанный шаг ЖЦ существует</returns>
      public bool ExistsLCStep(Guid lcstepGuid) => this.ExistsLCStep(this.GetLCStepID(lcstepGuid));

      /// <summary>Получить краткую информацию о шаге ЖЦ</summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Краткая информация о шаге ЖЦ или null</returns>
      public IMSLifeCycleStep GetLCStep(int lcstepID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleStep lcStep;
          if (this._lcSteps.TryGetValue(lcstepID, out lcStep))
            return lcStep;
        }
        return (IMSLifeCycleStep) null;
      }

      /// <summary>Получить краткую информацию о шаге ЖЦ</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Краткая информация о шаге ЖЦ или null</returns>
      public IMSLifeCycleStep GetLCStep(Guid lcstepGuid)
      {
        return this.GetLCStep(this.GetLCStepID(lcstepGuid));
      }

      /// <summary>Получить название шага ЖЦ</summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Название шага ЖЦ</returns>
      public string GetLCStepName(int lcstepID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleStep imsLifeCycleStep;
          if (this._lcSteps.TryGetValue(lcstepID, out imsLifeCycleStep))
            return imsLifeCycleStep.Name;
        }
        return string.Empty;
      }

      /// <summary>Получить название шага ЖЦ</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Название шага ЖЦ</returns>
      public string GetLCStepName(Guid lcstepGuid) => this.GetLCStepName(this.GetLCStepID(lcstepGuid));

      /// <summary>Получить по Guid шага ЖЦ его Int32-идентификатор</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Идентификатор шага ЖЦ. -1 - шаг ЖЦ не найден</returns>
      public int GetLCStepID(Guid lcstepGuid)
      {
        lock (this._syncRootLcSteps)
        {
          int lcStepId;
          if (this._lcStepsGuid2Id.TryGetValue(lcstepGuid, out lcStepId))
            return lcStepId;
        }
        return -1;
      }

      /// <summary>
      /// Получить по Int32-идентификатору шага ЖЦ его Guid-идентификатор
      /// </summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Идентификатор шага ЖЦ. Guid.Empty - шаг ЖЦ не найден</returns>
      public Guid GetLCStepGuid(int lcstepID)
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleStep imsLifeCycleStep;
          if (this._lcSteps.TryGetValue(lcstepID, out imsLifeCycleStep))
            return imsLifeCycleStep.Guid;
        }
        return Guid.Empty;
      }

      /// <summary>
      /// Возвращает идентификатор шага ЖЦ по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid шага ЖЦ в виде строки</param>
      public int GetLCStepID(string Guid) => this.GetLCStepID(new Guid(Guid));

      /// <summary>Получить список описаний всех шагов ЖЦ</summary>
      /// <returns>Список описаний всех шагов ЖЦ</returns>
      public List<IMSLifeCycleStep> GetLCStepsList()
      {
        lock (this._syncRootLcSteps)
        {
          IMSLifeCycleStep[] imsLifeCycleStepArray = new IMSLifeCycleStep[this._lcSteps.Count];
          this._lcSteps.Values.CopyTo(imsLifeCycleStepArray, 0);
          return new List<IMSLifeCycleStep>((IEnumerable<IMSLifeCycleStep>) imsLifeCycleStepArray);
        }
      }

      /// <summary>
      /// Получить по Guid какого-то элемента метаданных его тип
      /// </summary>
      /// <param name="guid">Guid какого-то элемента метаданных</param>
      /// <returns>Тип метаданных для указанного элемента</returns>
      public IMSGlobals GetGlobalsByGuid(Guid guid)
      {
        lock (this._syncRootGlobals)
        {
          IMSGlobals globalsByGuid;
          if (this._globalsGuid.TryGetValue(guid, out globalsByGuid))
            return globalsByGuid;
        }
        return IMSGlobals.Unknown;
      }

      /// <summary>Отыскать описание элемента метаданных по его Guid</summary>
      /// <param name="type">Тип метаданных</param>
      /// <param name="guid">Guid элемента метаданных</param>
      /// <returns>Описание элемента метаданных</returns>
      private IDisplayable GetMetaDataDisplayableByGuid(IMSGlobals type, Guid guid)
      {
        switch (type)
        {
          case IMSGlobals.IMSAttributeType:
            return (IDisplayable) this.GetAttributeType(guid);
          case IMSGlobals.IMSAttributeGroup:
            return (IDisplayable) this.GetAttributeGroup(guid);
          case IMSGlobals.IMSLifeCycleLevel:
            return (IDisplayable) this.GetLCLevel(guid);
          case IMSGlobals.IMSLifeCycleScheme:
            return (IDisplayable) this.GetLCSchema(guid);
          case IMSGlobals.IMSLifeCycleStep:
            return (IDisplayable) this.GetLCStep(guid);
          case IMSGlobals.IMSObjectType:
            return (IDisplayable) this.GetObjectType(guid);
          case IMSGlobals.IMSRelationType:
            return (IDisplayable) this.GetRelationType(guid);
          default:
            return (IDisplayable) null;
        }
      }

      /// <summary>
      /// Получить по Guid какого-то элемента метаданных его описание
      /// </summary>
      /// <param name="guid">Guid какого-то элемента метаданных</param>
      /// <returns>Описание метаданных для указанного элемента</returns>
      public IDisplayable GetDisplayableByGuid(Guid guid)
      {
        if (guid == Guid.Empty)
          return (IDisplayable) null;
        lock (this._syncRootGlobals)
        {
          IMSGlobals type;
          if (this._globalsGuid.TryGetValue(guid, out type))
            return this.GetMetaDataDisplayableByGuid(type, guid);
        }
        return (IDisplayable) null;
      }

      /// <summary>
      /// Вспомогательный класс для сравнения типов атрибутов по их названиям
      /// </summary>
      private sealed class AttrTypeByCaptionComparer : IComparer<int>
      {
        /// <summary>Сравнить два типа атрибутов по их названиям</summary>
        /// <param name="x">Идентификатор первого типа атрибута</param>
        /// <param name="y">Идентификатор второго типа атрибута</param>
        /// <returns>-1, 0, 1</returns>
        public int Compare(int x, int y)
        {
          IMSAttributeType attributeType1 = MetaDataHelperService.Instance.GetAttributeType(x);
          IMSAttributeType attributeType2 = MetaDataHelperService.Instance.GetAttributeType(y);
          return attributeType1 == null || attributeType2 == null ? 0 : attributeType1.Name.CompareTo(attributeType2.Name);
        }
      }
    }
}
