// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.PDMPluginConsts
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Свалка констант для класса PDMPlugin (клиентский плагин "PDM")
/// </summary>
public static class PDMPluginConsts
{
  /// <summary>Внимание</summary>
  public static readonly string Dialog1 = LocalizationHolder.rm.GetString("Interfaces.Pdm_22");
  /// <summary>
  /// На сервере приложений не загружен плагин \"Intermech.Pdm.Server\".\nРабота клиентского плагина \"Intermech.Pdm\" будет заблокирована.
  /// </summary>
  public static readonly string Dialog2 = LocalizationHolder.rm.GetString("Interfaces.Pdm_23");
  /// <summary>Информация</summary>
  public static readonly string Dialog3 = LocalizationHolder.rm.GetString("Interfaces.Pdm_24");
  /// <summary>
  /// Актуализация указанных допустимых заменителей выполнена успешна.
  /// </summary>
  public static readonly string Dialog4 = LocalizationHolder.rm.GetString("Interfaces.Pdm_25");
  /// <summary>
  /// Удаление информации о допустимых заменах выполнено успешно.
  /// </summary>
  public static readonly string Dialog5 = LocalizationHolder.rm.GetString("Interfaces.Pdm_26");
  /// <summary>Удаление информации о допустимых заменах</summary>
  public static readonly string Dialog6 = LocalizationHolder.rm.GetString("Interfaces.Pdm_27");
  /// <summary>
  /// Вы хотите удалить всю информацию о допустимых заменах для текущего состава?
  /// </summary>
  public static readonly string Dialog7 = LocalizationHolder.rm.GetString("Interfaces.Pdm_28");
  /// <summary>
  /// Удалить допустимые замены только в одном исполнении "{0}"\nлибо во всех найденых ({1} шт.) ?
  /// </summary>
  public static readonly string Dialog8 = LocalizationHolder.rm.GetString("Interfaces.Pdm_68");
  /// <summary>В одном исполнении</summary>
  public static readonly string Dialog9 = LocalizationHolder.rm.GetString("Interfaces.Pdm_69");
  /// <summary>Во всех найденных</summary>
  public static readonly string Dialog10 = LocalizationHolder.rm.GetString("Interfaces.Pdm_70");
  /// <summary>Отмена</summary>
  public static readonly string Dialog11 = LocalizationHolder.rm.GetString("Interfaces.Pdm_71");
  /// <summary>
  /// Название плагина - "Управление информацией об изделии (PDM)"
  /// </summary>
  public static readonly string PDMPluginName = LocalizationHolder.rm.GetString("Interfaces.Pdm_67");
  /// <summary>Название плагина - "InterMech.Interfaces.PDM"</summary>
  public const string InterfacesPDMPluginName = "InterMech.Interfaces.PDM";
  /// <summary>
  /// Файл с рисунками плагина - "Intermech.Pdm.Resources.SubstitutionsBitmaps.bmp"
  /// </summary>
  public const string PDMPluginBitmaps = "Intermech.Pdm.Resources.SubstitutionsBitmaps.bmp";
  /// <summary>Нотификация - "ObjectsChanged"</summary>
  public const string ObjectsChangedNotification = "ObjectsChanged";
  /// <summary>Нотификация - "RelationsCreated"</summary>
  public const string RelationsCreatedNotification = "RelationsCreated";
  /// <summary>Нотификация - "RelationsRemoves"</summary>
  public const string RelationsRemovedNotification = "RelationsRemoved";
  /// <summary>Нотификация - "RelationsChanged"</summary>
  public const string RelationsChangedNotification = "RelationsChanged";
  /// <summary>imgCreateContext.PDM</summary>
  public const string imgCreateContext = "imgCreateContext.PDM";
  /// <summary>imgSubstitutes.PDM</summary>
  public const string imgSubstitutes = "imgSubstitutes.PDM";
  /// <summary>imgContextComposition.PDM</summary>
  public const string imgContextComposition = "imgContextComposition.PDM";
  /// <summary>imgDesignContext.PDM</summary>
  public const string imgDesignContext = "imgDesignContext.PDM";
  /// <summary>imgCreateSubstitutesGroup.PDM</summary>
  public const string imgCreateSubstitutesGroup = "imgCreateSubstitutesGroup.PDM";
  public const string icoCreateSubstitutesGroup = "icoCreateSubstitutesGroup.PDM";
  /// <summary>imgMakeActualSubstitute.PDM</summary>
  public const string imgMakeActualSubstitute = "imgMakeActualSubstitute.PDM";
  public const string icoMakeActualSubstitute = "icoMakeActualSubstitute.PDM";
  /// <summary>imgEditSubstitutesGroup.PDM</summary>
  public const string imgEditSubstitutesGroup = "imgEditSubstitutesGroup.PDM";
  public const string icoEditSubstitutesGroup = "icoEditSubstitutesGroup.PDM";
  /// <summary>imgDeleteSubstitutesGroup.PDM</summary>
  public const string imgDeleteSubstitutesGroup = "imgDeleteSubstitutesGroup.PDM";
  public const string icoDeleteSubstitutesGroup = "icoDeleteSubstitutesGroup.PDM";
  /// <summary>imgSubstitutes.PDM</summary>
  public const string imgToolbarSubstitutes = "imgSubstitutes.PDM";
  /// <summary>imgListView.PDM</summary>
  public const string imgListView = "imgListView.PDM";
  /// <summary>imgHiddenChilds.PDM</summary>
  public const string imgHiddenChilds = "imgHiddenChilds.PDM";
  /// <summary>imgHiddenComposition.PDM</summary>
  public const string imgHiddenComposition = "imgHiddenComposition.PDM";
  /// <summary>imgComposition.PDM</summary>
  public const string imgComposition = "imgComposition.PDM";
  /// <summary>imgHideComposition.PDM</summary>
  public const string imgHideComposition = "imgHideComposition.PDM";
  /// <summary>imgObjects.PDM</summary>
  public const string imgObjects = "imgObjects.PDM";
  /// <summary>imgObjects.ActualSubstitute</summary>
  public const string imgActualSubstitute = "imgObjects.ActualSubstitute";
  /// <summary>imgObjects.Substitute</summary>
  public const string imgSubstitute = "imgObjects.Substitute";
  /// <summary>imgObjects.DesignVariant</summary>
  public const string imgDesignVariant = "imgObjects.DesignVariant";
  /// <summary>PDM.CreateContext</summary>
  public const string cmdCreateContext = "PDM.CreateContext";
  /// <summary>Intermech.PDM</summary>
  public const string cmdIntermechPdm = "Intermech.PDM";
  /// <summary>PDM.CreateSubstitutesGroup</summary>
  public const string cmdCreateSubstitutesGroup = "PDM.CreateSubstitutesGroup";
  /// <summary>PDM.AddZagotovkaForPart</summary>
  public const string cmdAddZagotovkaForPart = "PDM.AddZagotovkaForPart";
  /// <summary>PDM.MakeActualSubstitute</summary>
  public const string cmdMakeActualSubstitute = "PDM.MakeActualSubstitute";
  /// <summary>PDM.EditSubstitutesGroup</summary>
  public const string cmdEditSubstitutesGroup = "PDM.EditSubstitutesGroup";
  /// <summary>PDM.HideComposition</summary>
  public const string cmdHideComposition = "PDM.HideComposition";
  /// <summary>PDM.DeleteSubstitutesGroup</summary>
  public const string cmdDeleteSubstitutesGroup = "PDM.DeleteSubstitutesGroup";
  /// <summary>PDM.HiddenChilds</summary>
  public const string cmdHiddenChilds = "PDM.HiddenChilds";
  /// <summary>PDM.HiddenComposition</summary>
  public const string cmdHiddenComposition = "PDM.HiddenComposition";
  /// <summary>PDM.InsertTechInComposition</summary>
  public const string cmdInsertTechInComposition = "PDM.InsertTechInComposition";
  /// <summary>PDM.InsertAdditionalComplect</summary>
  public const string cmdInsertAdditionalComplect = "PDM.InsertAdditionalComplect";
  /// <summary>Исполнение</summary>
  public const string cmdInstance = "PDM.CreateInstance";
  /// <summary>Экземпляр/партия</summary>
  public const string cmdExemplar = "PDM.Exemplar";
  /// <summary>Создать экземпляр/партию</summary>
  public const string cmdCreateExemplar = "PDM.CreateExemplar";
  /// <summary>Дерево экземпляров и партий</summary>
  public const string cmdTreeExemplars = "PDM.TreeExemplars";
  /// <summary>Список исполнений</summary>
  public const string cmdListInstance = "PDM.ListInstance";
  /// <summary>Визуализатор связей</summary>
  public const string cmdRelationVisualizer = "PDM.RelationVisualizer";
  /// <summary>Сравнить состав</summary>
  public const string cmdCompareComposition = "PDM.CompareComposition";
  /// <summary>
  /// Команда Состав для пункта меню Сравнить. Аналог Сравнить состав.
  /// </summary>
  public const string cmdCompareCompositionForCompareObjectsMenu = "PDM.CompareCompositionForCompareObjectsMenu";
  /// <summary>Команда Состав для пункта меню Сравнить версии</summary>
  public const string cmdCompareVersionComposition = "PDM.CompareVersionComposition";
  /// <summary>Cостав</summary>
  public static readonly string menuCompareCompositionForCompareObjectsMenu = LocalizationHolder.rm.GetString("Interfaces.Pdm_81");
  /// <summary>Дерево сравнения</summary>
  public const string cmdTreeCompare = "PDM.TreeCompare";
  /// <summary>Команда Дерево сравнения для пункта меню Сравнить.</summary>
  public const string cmdTreeCompareForCompareObjectsMenu = "PDM.TreeCompareForCompareObjectsMenu";
  /// <summary>
  /// Команда Дерево сравнения для пункта меню Сравнить версии.
  /// </summary>
  public const string cmdTreeCompareForCompareVersionObjectsMenu = "PDM.TreeCompareForCompareVersionObjectsMenu";
  /// <summary>Заполнить первичную применяемость</summary>
  public const string cmdFillFirstEntersTo = "PDM.FillFirstEntersTo";
  /// <summary>Развернуть состав</summary>
  public const string cmdExpandContains = "PDM.ExpandContains";
  /// <summary>Развернуть применяемость</summary>
  public const string cmdExpandEntersTo = "PDM.ExpandEntersTo";
  /// <summary>Контекстный состав</summary>
  public static readonly string menuCreateContext = LocalizationHolder.rm.GetString("Interfaces.Pdm_29");
  /// <summary>Создать экземпляр/партию</summary>
  public static readonly string menuCreateExemplar = LocalizationHolder.rm.GetString("Interfaces.Pdm_30");
  /// <summary>Создать экземпляр/партию</summary>
  public static readonly string menuTreeExemplars = LocalizationHolder.rm.GetString("Interfaces.Pdm_31");
  /// <summary>Экземпляр/партия</summary>
  public static readonly string menuExemplar = LocalizationHolder.rm.GetString("Interfaces.Pdm_32");
  /// <summary>Заменители</summary>
  public static readonly string menuIntermechPdm = LocalizationHolder.rm.GetString("Interfaces.Pdm_33");
  /// <summary>Создать группу заменителей</summary>
  public static readonly string menuCreateSubstitutesGroup = LocalizationHolder.rm.GetString("Interfaces.Pdm_34");
  /// <summary>Сделать заменитель актуальным</summary>
  public static readonly string menuMakeActualSubstitute = LocalizationHolder.rm.GetString("Interfaces.Pdm_35");
  /// <summary>Настроить допустимые замены...</summary>
  public static readonly string menuEditSubstitutesGroup = LocalizationHolder.rm.GetString("Interfaces.Pdm_36");
  /// <summary>Скрыть состав</summary>
  public static readonly string menuHideComposition = LocalizationHolder.rm.GetString("Interfaces.Pdm_37");
  /// <summary>Добавить технологическое изделие</summary>
  public static readonly string menuInsertTechInComposition = LocalizationHolder.rm.GetString("Interfaces.Pdm_66");
  /// <summary>Добавить комплект, поставляемый отдельно</summary>
  public static readonly string menuInsertAdditionalComplect = LocalizationHolder.rm.GetString("Interfaces.Pdm_79");
  /// <summary>Удалить допустимые замены</summary>
  public static readonly string menuDeleteSubstitutesGroup = LocalizationHolder.rm.GetString("Interfaces.Pdm_38");
  /// <summary>Не показывать скрытый состав объектов</summary>
  public static readonly string menuHiddenChilds = LocalizationHolder.rm.GetString("Interfaces.Pdm_39");
  /// <summary>Не показывать объекты со скрытым составом</summary>
  public static readonly string menuHiddenComposition = LocalizationHolder.rm.GetString("Interfaces.Pdm_40");
  /// <summary>Добавить заготовку для изделия</summary>
  public static readonly string menuAddZagotovkaForPart = LocalizationHolder.rm.GetString("Interfaces.Pdm_78");
  /// <summary>Актуальные заменители</summary>
  public static readonly string buttonSubstitutesText = LocalizationHolder.rm.GetString("Interfaces.Pdm_41");
  /// <summary>Контекст состава</summary>
  public static readonly string buttonContextCompositionText = LocalizationHolder.rm.GetString("Interfaces.Pdm_42");
  /// <summary>Показывать актуальные заменители или все заменители</summary>
  public static readonly string buttonSubstitutesHint = LocalizationHolder.rm.GetString("Interfaces.Pdm_43");
  /// <summary>
  /// Выберите контексты, в рамках которых будут просматриваться составы
  /// </summary>
  public static readonly string buttonContextCompositionHint = LocalizationHolder.rm.GetString("Interfaces.Pdm_44");
  /// <summary>Показывать скрытый состав объектов</summary>
  public static readonly string buttonHiddenChildsHint = LocalizationHolder.rm.GetString("Interfaces.Pdm_45");
  /// <summary>Показывать объекты со скрытым составом</summary>
  public static readonly string buttonHiddenCompositionHint = LocalizationHolder.rm.GetString("Interfaces.Pdm_46");
  /// <summary>Исполнение</summary>
  public static readonly string menuInstance = LocalizationHolder.rm.GetString("Interfaces.Pdm_47");
  /// <summary>Список исполнений</summary>
  public static readonly string menuListInstance = LocalizationHolder.rm.GetString("Interfaces.Pdm_48");
  /// <summary>Добавить исполнение</summary>
  public static readonly string menuAddInstance = LocalizationHolder.rm.GetString("Interfaces.Pdm_72");
  /// <summary>Добавить исполнение</summary>
  public static readonly string menuMakeInstance = LocalizationHolder.rm.GetString("Interfaces.Pdm_73");
  /// <summary>Исключить исполнение</summary>
  public static readonly string menuExcludeInstance = LocalizationHolder.rm.GetString("Interfaces.Pdm_77");
  /// <summary>PDM.AddInstance</summary>
  public const string cmdAddInstance = "PDM.AddInstance";
  /// <summary>PDM.MadeInstance</summary>
  public const string cmdMakeInstance = "PDM.MadeInstance";
  /// <summary>PDM.Exclude</summary>
  public const string cmdExcludeInstance = "PDM.Exclude";
  /// <summary>Визуализатор связей</summary>
  public static readonly string menuRelationVisualizer = LocalizationHolder.rm.GetString("Interfaces.Pdm_60");
  /// <summary>Список исполнений</summary>
  public static readonly string ListInstancesWindow = LocalizationHolder.rm.GetString("Interfaces.Pdm_59");
  /// <summary>Сравнить составы</summary>
  public static readonly string menuCompareComposition = LocalizationHolder.rm.GetString("Interfaces.Pdm_49");
  /// <summary>Дерево сравнения</summary>
  public static readonly string menuTreeCompare = LocalizationHolder.rm.GetString("Interfaces.Pdm_75");
  /// <summary>Заполнить первичную применяемость</summary>
  public static readonly string menuFillFirstEntersTo = LocalizationHolder.rm.GetString("Interfaces.Pdm_50");
  /// <summary>Развернуть состав</summary>
  public static readonly string menuExpandContains = LocalizationHolder.rm.GetString("Interfaces.Pdm_51");
  /// <summary>Развернуть применяемость</summary>
  public static readonly string menuExpandEntersTo = LocalizationHolder.rm.GetString("Interfaces.Pdm_52");
  /// <summary>Обновить примечания спецификаций основного заказа</summary>
  public static readonly string menuUpdateSpecificationNotes = LocalizationHolder.rm.GetString("Interfaces.Pdm_74");
  /// <summary>Обновить примечания спецификаций основного заказа</summary>
  public static readonly string cmdUpdateSpecificationNotes = "OrderPoint.UpdateSpecification";
  /// <summary>Добавить в точку</summary>
  public static readonly string menuAddToOrderPoint = LocalizationHolder.rm.GetString("Interfaces.Pdm_76");
  /// <summary>Добавить в точку</summary>
  public static readonly string cmdAddToOrderPoint = "OrderPoint.AddToOrderPoint";
  /// <summary>Заголовок закладки сравнения составов</summary>
  public static readonly string CompareObjectsWindow = LocalizationHolder.rm.GetString("Interfaces.Pdm_63");
  /// <summary>
  /// Заголовок корневого нода в дереве со списком сравниваемых объектов
  /// </summary>
  public static readonly string ListCompareObjects = LocalizationHolder.rm.GetString("Interfaces.Pdm_64");
  /// <summary>
  /// Заголовок корневого нода в дереве со списком сравниваемых объектов
  /// </summary>
  public static readonly string TreeCompareObjects = "Дерево";
  /// <summary>Заголовок вьюшки</summary>
  public static readonly string CompareObjectComposition = LocalizationHolder.rm.GetString("Interfaces.Pdm_65");
  /// <summary>
  /// 
  /// </summary>
  public static int CategoryCompareObjectsRoot = -1;
  /// <summary>Category IDs</summary>
  public static int CategoryCompareObject = -1;
  /// <summary>Category IDs</summary>
  public static int CategoryInstance = -1;
  /// <summary>Category IDs</summary>
  public static int CategoryContains = -1;
  /// <summary>Категория узла SubstitutesNode</summary>
  public static int CategorySubstitutes = -1;
  /// <summary>???</summary>
  public static int CategoryArticles = -1;
  /// <summary>Категория типов объектов</summary>
  public static int ObjectTypesCategoryID = -1;
  public static bool DisableCreateTauCommand;
}
