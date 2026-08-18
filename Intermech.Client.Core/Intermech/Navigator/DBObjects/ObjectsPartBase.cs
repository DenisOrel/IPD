
// Type: Intermech.Navigator.DBObjects.ObjectsPartBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using Intermech.Navigator.Selections.Implementation;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Класс, служащий основой для создания всех частей элементов навигации,
/// работающих с объектами базы данных.
/// </summary>
public abstract class ObjectsPartBase : ObjectsItems, INodePart, INodeItems, INodeQuerySupport
{
  /// <summary>Интерфейс по обработке колонки "Статусы элемента"</summary>
  private static INodeStatusesInfo _statusesInfoService;
  /// <summary>Название набора колонок - "Атрибуты объектов"</summary>
  internal static string columnsSetNameObj = LocalizationHolder.rm.GetString("Client.Core_310");
  /// <summary>Коллекция названий наборов колонок</summary>
  internal static List<string> columnSetNames = new List<string>(0);
  /// <summary>
  /// Составное значение: атрибут F_OBJECT_TYPE : источник - объект
  /// </summary>
  public static NodeColumnID ncF_OBJECT_TYPE = new NodeColumnID((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут F_OBJECT_ID : источник - объект
  /// </summary>
  public static NodeColumnID ncF_OBJECT_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object);
  /// <summary>Составное значение: атрибут F_ID : источник - объект</summary>
  public static NodeColumnID ncF_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут F_CHKOUT_BY : источник - объект
  /// </summary>
  public static NodeColumnID ncF_CHKOUT_BY = new NodeColumnID((object) ObligatoryObjectAttributes.F_CHKOUT_BY, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут F_LC_STEP : источник - объект
  /// </summary>
  public static NodeColumnID ncF_LC_STEP = new NodeColumnID((object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут F_LEVEL_ID : источник - объект
  /// </summary>
  public static NodeColumnID ncF_LEVEL_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_LEVEL_ID, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут CAPTION : источник - объект
  /// </summary>
  public static NodeColumnID ncCAPTION = new NodeColumnID((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут "Ручная выборка" : источник - объект
  /// </summary>
  public static NodeColumnID ncHANDS_SELECTION = new NodeColumnID((object) new Guid("cad00155-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут "Принадлежность выборки" : источник - объект
  /// </summary>
  public static NodeColumnID ncSELECTION_TYPE = new NodeColumnID((object) new Guid("cad00158-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут "Назначение выборки" : источник - объект
  /// </summary>
  public static NodeColumnID ncSAMPLE_FUNCTION = new NodeColumnID((object) new Guid("cad00345-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут "Искать среди объектов глобальных и локальных типов" : источник - объект
  /// </summary>
  public static NodeColumnID ncSEARCH_LOCALTYPES = new NodeColumnID((object) Intermech.Navigator.Selections.Consts.attTypeSearchInLocalTypes, AttributeSourceTypes.Object);
  /// <summary>Составное значение: атрибут OWNER : источник - объект</summary>
  public static NodeColumnID ncOWNER = new NodeColumnID((object) ObligatoryObjectAttributes.F_OWNER_ID, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут VERSION : источник - объект
  /// </summary>
  public static NodeColumnID ncVERSION = new NodeColumnID((object) ObligatoryObjectAttributes.F_VERSION_ID, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут BASE_VERSION : источник - объект
  /// </summary>
  public static NodeColumnID ncBASE_VERSION = new NodeColumnID((object) ObligatoryObjectAttributes.F_BASE_VERSION, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут MODIFICATION_ID : источник - объект
  /// </summary>
  public static NodeColumnID ncMODIFICATION_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут SITE_ID : источник - объект
  /// </summary>
  public static NodeColumnID ncSITE_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_SITE_ID, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут "Сортировка" : источник - связь
  /// </summary>
  public static NodeColumnID ncSORTING = new NodeColumnID((object) -1L, AttributeSourceTypes.Relation);
  /// <summary>
  /// Составное значение: атрибут "Видимость" : источник - объект
  /// </summary>
  public static NodeColumnID ncVISIBILITY = new NodeColumnID((object) -1L, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут F_ELEMENT_STATUSES : источник - связь
  /// </summary>
  public static NodeColumnID ncF_ELEMENT_STATUSES = new NodeColumnID((object) -77, AttributeSourceTypes.Relation);
  /// <summary>
  /// Массив условий, которым должны удовлетворять объекты, с которыми
  /// работает эта часть.
  /// </summary>
  private ConditionStructure[] _conditions;
  /// <summary>
  /// Провайдер условий, которым должны удовлетворять объекты, с которыми
  /// работает эта часть.
  /// </summary>
  /// <remarks>
  /// Провайдер условий может применяться в тех случаях, когда условия
  /// динамически изменяются в зависимости от внешних по отношению к этой
  /// части причин. Если указан провайдер условий, то набор статических
  /// условий, заданных с помошью <see cref="F:Intermech.Navigator.DBObjects.ObjectsPartBase._conditions" />, полностью
  /// игнорируется.
  /// </remarks>
  private IConditionsProvider _conditionsProvider;
  /// <summary>Кэш условий запроса</summary>
  private ConditionStructure[] _conditionsCache;
  /// <summary>Владелец</summary>
  private object _owner;

  /// <summary>Интерфейс по обработке колонки "Статусы элемента"</summary>
  protected static INodeStatusesInfo StatusesInfoService
  {
    get
    {
      ObjectsPartBase._statusesInfoService = ObjectsPartBase._statusesInfoService ?? (INodeStatusesInfo) (ServicesManager.GetService(typeof (Intermech.Navigator.DBObjects.StatusesInfoService)) as Intermech.Navigator.DBObjects.StatusesInfoService);
      return ObjectsPartBase._statusesInfoService;
    }
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая не накладывает
  /// каких-либо условий на объекты, с которыми она работает.
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPartBase(IServiceProvider services)
    : this((ConditionStructure[]) null, (IConditionsProvider) null, services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая накладывает одно
  /// условие на объекты, с которыми она работает.
  /// </summary>
  /// <param name="condition">Условие, которому должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPartBase(ConditionStructure condition, IServiceProvider services)
    : this(new ConditionStructure[1]{ condition }, (IConditionsProvider) null, services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая накладывает несколько
  /// ограничений на объекты, с которыми она работает.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPartBase(ConditionStructure[] conditions, IServiceProvider services)
    : this(conditions, (IConditionsProvider) null, services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая накладывает динамически
  /// изменяющийся набор условий на объекты, с которыми она работает.
  /// </summary>
  /// <param name="conditionsProvider">Провайдер условий.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPartBase(IConditionsProvider conditionsProvider, IServiceProvider services)
    : this((ConditionStructure[]) null, conditionsProvider, services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая накладывает динамически
  /// изменяющийся набор условий на объекты, с которыми она работает.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <param name="conditionsProvider">Провайдер условий.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPartBase(
    ConditionStructure[] conditions,
    IConditionsProvider conditionsProvider,
    IServiceProvider services)
  {
    if (ObjectsPartBase.ncSORTING.ID.Equals((object) -1L))
      ObjectsPartBase.ncSORTING.ID = (object) MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545");
    if (ObjectsPartBase.ncVISIBILITY.ID.Equals((object) -1L))
      ObjectsPartBase.ncVISIBILITY.ID = (object) MetaDataHelper.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545");
    this._conditions = conditions;
    this._conditionsProvider = conditionsProvider;
    this._conditionsCache = (ConditionStructure[]) null;
    this.Services = services;
  }

  /// <summary>Владелец объекта</summary>
  public object Owner
  {
    get => this._owner;
    set => this._owner = value;
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого можно
  /// получить список объектов, с которыми работает эта часть. Если у
  /// данной части нет дочерних элементов, то метод вернет null.
  /// </summary>
  /// <returns>Ссылка на интерфейс объекта-запроса</returns>
  public INodeQuery GetQuery() => this.GetQuery(this.Conditions);

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею объектов.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected abstract INodeQuery GetQuery(ConditionStructure[] conditions);

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// для данного элемента. Используется только в том случае, если для
  /// данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public virtual NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columnCollection, true, false);
    return this.GetSchemeDefaultColumns(columnCollection);
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public virtual NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columnCollection, true, true);
    Helper.AddObligatoryColumnsAdv(columnCollection);
    if (ColumnSetName == Intermech.Navigator.Consts.ColumnSetNameAllAttrs || ColumnSetName == string.Empty)
    {
      Helper.AddAllColumns(columnCollection);
      Helper.AddAllColumnsRelation(columnCollection);
    }
    return this.GetSchemeSupportedColumns(columnCollection, ColumnSetName);
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <param name="columns">Коллекция колонок</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public virtual void GetSupportedColumns(string ColumnSetName, NodeColumnCollection columns)
  {
    if (columns == null)
      return;
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    Helper.AddObligatoryColumns(columns, true, true);
    Helper.AddObligatoryColumnsAdv(columns);
    if (ColumnSetName == Intermech.Navigator.Consts.ColumnSetNameAllAttrs || ColumnSetName == string.Empty)
    {
      Helper.AddAllColumns(columns);
      Helper.AddAllColumnsRelation(columns);
    }
    this.GetSchemeSupportedColumns(columns, ColumnSetName);
  }

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (Intermech.Navigator.Consts.NavigatorDefaultColumnSetName)
  /// </summary>
  /// <returns></returns>
  public virtual List<string> GetSupportedColumnSetNames()
  {
    if (!ObjectsPartBase.columnSetNames.Contains(ObjectsPartBase.columnsSetNameObj))
      ObjectsPartBase.columnSetNames.Add(ObjectsPartBase.columnsSetNameObj);
    return this.GetSchemeSupportedColumnSetNames(ObjectsPartBase.columnSetNames);
  }

  /// <summary>
  /// Восстанавливает идентификатор объекта базы данных по указанному
  /// имени из адресной строки. Если найти адресуемый объект не удается,
  /// то метод вернет null.
  /// </summary>
  /// <param name="address">Адрес объекта базы данных</param>
  /// <returns>Унифицированный идентификатор объекта базы данных</returns>
  public override INodeID ParseAddress(string address)
  {
    INodeQuery query = this.GetQuery(this.GetAddressConditions(address));
    query.Execute((object) null, 1);
    return query.RecordCount == 1 ? query.GetRecordNodeID(0) : (INodeID) null;
  }

  /// <summary>
  /// Возвращает набор условий, которым должны удовлетворять объекты, с которыми
  /// работает эта часть. Набор условий актуален на момент обращения к этому
  /// свойству.
  /// </summary>
  protected ConditionStructure[] Conditions
  {
    get
    {
      if (this._conditionsProvider != null && this._conditionsProvider.ConditionsChanged)
        this._conditionsCache = (ConditionStructure[]) null;
      if (this._conditionsCache == null)
      {
        if (this._conditionsProvider != null)
          this._conditionsCache = this._conditionsProvider.GetConditions();
        this._conditionsCache = ConditionStructure.Join(this._conditions, this._conditionsCache);
      }
      return this._conditionsCache;
    }
  }

  /// <summary>
  /// Создает и возвращает условие, позволяющее найти объекты с
  /// указанным адресом.
  /// </summary>
  /// <param name="address">Адрес объекта</param>
  /// <returns>Условие запроса к базе данных</returns>
  private ConditionStructure GetAddressCondition(string address)
  {
    long result;
    return long.TryParse(address, out result) ? new ConditionStructure(-2, RelationalOperators.Equal, (object) result, LogicalOperators.NONE, 0, false) : new ConditionStructure(-50, RelationalOperators.Equal, (object) address, LogicalOperators.NONE, 0, false);
  }

  /// <summary>
  /// Создает и возвращает массив условий, позволяющий найти объекты с
  /// указанным адресом.
  /// </summary>
  /// <param name="address">Адрес объекта</param>
  /// <returns>Массив условий запроса к базе данных</returns>
  private ConditionStructure[] GetAddressConditions(string address)
  {
    int length = 1;
    if (this.Conditions != null)
      length += this.Conditions.Length;
    ConditionStructure[] addressConditions = new ConditionStructure[length];
    addressConditions[0] = this.GetAddressCondition(address);
    if (this.Conditions != null)
    {
      addressConditions[0].LogicalOperator = LogicalOperators.AND;
      this.Conditions.CopyTo((Array) addressConditions, 1);
    }
    return addressConditions;
  }

  /// <summary>Отразить колонку "Навигатора" на поле</summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Поле</returns>
  public virtual object MapColumnToField(NodeColumn column)
  {
    if (column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_CAPTION"))
      return (object) new NodeColumnID((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object);
    return column.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid ? (object) new NodeColumnID(column.ID, AttributeSourceTypes.Object) : this.MapVirtualColumnToField(column);
  }

  /// <summary>
  /// Получить список служебных полей (которые загружаются в узел независимо от настройки вида)
  /// </summary>
  /// <returns>Список служебных полей (которые загружаются в узел независимо от настройки вида)</returns>
  public virtual List<object> GetSpecialFields()
  {
    List<object> collection = new List<object>();
    collection.Add((object) ObjectsPartBase.ncF_OBJECT_TYPE);
    collection.Add((object) ObjectsPartBase.ncF_OBJECT_ID);
    collection.Add((object) ObjectsPartBase.ncF_ID);
    collection.Add((object) ObjectsPartBase.ncF_CHKOUT_BY);
    collection.Add((object) ObjectsPartBase.ncF_LC_STEP);
    collection.Add((object) ObjectsPartBase.ncF_LEVEL_ID);
    collection.Add((object) ObjectsPartBase.ncCAPTION);
    collection.Add((object) ObjectsPartBase.ncOWNER);
    collection.Add((object) ObjectsPartBase.ncVERSION);
    collection.Add((object) ObjectsPartBase.ncBASE_VERSION);
    collection.Add((object) ObjectsPartBase.ncSITE_ID);
    collection.Add((object) ObjectsPartBase.ncMODIFICATION_ID);
    if (this.Owner is DesktopObjectNode)
    {
      collection.Add((object) ObjectsPartBase.ncHANDS_SELECTION);
      collection.Add((object) ObjectsPartBase.ncSORTING);
      collection.Add((object) ObjectsPartBase.ncSELECTION_TYPE);
      collection.Add((object) ObjectsPartBase.ncSAMPLE_FUNCTION);
      collection.Add((object) ObjectsPartBase.ncSEARCH_LOCALTYPES);
    }
    ISpecialFieldsSupported service = ServiceUtils.GetService<ISpecialFieldsSupported>((object) this.Services, false);
    if (service != null)
    {
      List<object> specialFields = service.GetSpecialFields();
      if (specialFields != null)
        collection.SafeAddRange<object>((IEnumerable<object>) specialFields);
    }
    return collection;
  }

  /// <summary>Создать описание корневого узла</summary>
  /// <param name="fieldValues">Значения полей</param>
  /// <param name="adapter">Адаптер</param>
  /// <returns>Описание корневого узла</returns>
  public virtual INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    string caption = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    long int64_4 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]);
    long int64_5 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION)]);
    long int64_6 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncBASE_VERSION)]);
    string siteID = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)]);
    long int64_7 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)]);
    byte[] fieldValue = adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ELEMENT_STATUSES) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ELEMENT_STATUSES)] == DBNull.Value ? (byte[]) null : fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ELEMENT_STATUSES)] as byte[];
    ObjectFiltrationState state = ObjectFiltrationState.fsNotRequired;
    if (fieldValue != null)
      state = (ObjectFiltrationState) (ServicesManager.GetService(typeof (IElementStatusesClientService)) as IElementStatusesClientService).GetElementStatuses32("cad005f2-306c-11d8-b4e9-00304f19f545", fieldValue);
    if (int32_1 == Intermech.Navigator.Services._objectTypeIDPersonalSelection || int32_1 == Intermech.Navigator.Services._objectTypeIDCommonSelection)
    {
      SelectionType selectionType = adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE)] == DBNull.Value ? SelectionType.None : (SelectionType) Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE)]);
      bool handSelection = adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION)] != DBNull.Value && Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION)]) == 1L;
      long int64_8 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)] == DBNull.Value ? 0L : Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)]);
      int int32_3 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION)] == DBNull.Value ? 0 : Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION)]);
      bool searchInLocalTypes = adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES)] != DBNull.Value && Convert.ToBoolean(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES)]);
      if (this.Owner is DesktopObjectNode)
        return (INodeID) new SelectionNodeID((CreateObjectNodeParams) new CreateSelectionNodeParams(int32_1, int64_1, int64_2, int64_3, -1L, int32_2, caption, -1, int64_4, int64_8, state, int64_5, int64_6, handSelection, selectionType, siteID, 0L, Guid.Empty, int64_7, -1, int32_3, searchInLocalTypes));
    }
    return this.CreateObjectNodeIdFromParams(fieldValues, adapter, new CreateObjectNodeParams(int32_1, int64_1, int64_2, int64_3, -1L, int32_2, caption, -1, int64_4, 0L, state, int64_5, int64_6, siteID, 0L, Guid.Empty, int64_7));
  }

  /// <summary>Создание идентификатора ноды из подготовленный структуры с параметрами онного</summary>
  protected virtual INodeID CreateObjectNodeIdFromParams(
    object[] fieldValues,
    RecordAdapter adapter,
    CreateObjectNodeParams createObjectNodeParams)
  {
    return (INodeID) new NodeID(createObjectNodeParams);
  }

  /// <summary>Вернуть идентификатор описания узла</summary>
  /// <param name="nodeId">Описание узла</param>
  /// <returns></returns>
  public virtual object CreateRecordId(INodeID nodeId) => (object) ((NodeID) nodeId).ObjectID;
}
