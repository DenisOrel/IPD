
// Type: Intermech.Navigator.AdjustableViewsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Views;


namespace Intermech.Navigator;

/// <summary>
/// Статический класс, метод которого вызывается в главной форме для регистрации всех вьюшек "Навигатора"
/// </summary>
public static class AdjustableViewsService
{
  public const string DocumentsThumbnailView = "DocumentsThumbnailView";
  public const string ChildrenView = "ChildrenView";

  /// <summary>
  /// Метод вызывается в главной форме для регистрации всех вьюшек "Навигатора".
  /// 
  /// Каждый, кто создаёт вьюшки в основной сборке, должен дописать сюда код,
  /// регистрирующий их в сервисе настройки закладок.
  /// 
  /// Кто создаёт вьюшки в плагинах, должен регистрировать их с помощью
  /// статического класса Intermech.Navigator.Views.AdjustableViewsHelper
  /// в методе инициализации плагина.
  /// </summary>
  public static void RegisterNavigatorViews()
  {
    AdjustableViewsHelper.RegisterView("ObjectFiles", LocalizationHolder.rm.GetString("Client.Core_297"), LocalizationHolder.rm.GetString("Client.Core_706"), "", "imgFilesList", true, 50);
    AdjustableViewsHelper.RegisterView("ChildrenView", LocalizationHolder.rm.GetString("Client.Core_480"), LocalizationHolder.rm.GetString("Client.Core_707"), "", "imgContains", true, 20);
    AdjustableViewsHelper.RegisterView("ApplicabilityView", LocalizationHolder.rm.GetString("Client.Core_1339"), LocalizationHolder.rm.GetString("Client.Core_1524"), "", "imgEntersTo", true, 27);
    AdjustableViewsHelper.RegisterView("ObjectsView", LocalizationHolder.rm.GetString("Client.Core_1351"), LocalizationHolder.rm.GetString("Client.Core_1377"), "", "", true, 20);
    AdjustableViewsHelper.RegisterView("ObjectProperties", LocalizationHolder.rm.GetString("Client.Core_146"), LocalizationHolder.rm.GetString("Client.Core_708"), "", "imgProp", true, 10);
    AdjustableViewsHelper.RegisterView("RelationProperties", LocalizationHolder.rm.GetString("Client.Core_312"), LocalizationHolder.rm.GetString("Client.Core_709"), "", "imgRelation", true, 11);
    AdjustableViewsHelper.RegisterView("ObjectVisualizer", LocalizationHolder.rm.GetString("Client.Core_378"), LocalizationHolder.rm.GetString("Client.Core_710"), "", "imgView", true, 40);
    AdjustableViewsHelper.RegisterView("ObjectSecurity", LocalizationHolder.rm.GetString("Client.Core_154"), LocalizationHolder.rm.GetString("Client.Core_711"), "", "imgKeys", true, 60);
    AdjustableViewsHelper.RegisterView("ObjectEvents", LocalizationHolder.rm.GetString("Client.Core_31"), LocalizationHolder.rm.GetString("Client.Core_712"), "", "imgEventLogIcon", true, 63 /*0x3F*/);
    AdjustableViewsHelper.RegisterView("SnapshotsView", LocalizationHolder.rm.GetString("Client.Core_1410"), LocalizationHolder.rm.GetString("Client.Core_1405"), "", "imgSnapshot", true, 12);
    AdjustableViewsHelper.RegisterView("PerformanceOfDuities", "Исполнение обязанностей", "Исполнение обязанностей для объекта Пользователи", "", "", true, 17);
    AdjustableViewsHelper.RegisterView("ObjectFiles.FileStorageView", LocalizationHolder.rm.GetString("Client.Core_713"), "", "", "imgFilesList", true, 50);
    AdjustableViewsHelper.RegisterView("Events", LocalizationHolder.rm.GetString("Client.Core_610"), LocalizationHolder.rm.GetString("Client.Core_714"), "", "imgEventLogIcon", true, 20);
    AdjustableViewsHelper.RegisterView("FilterConfig", LocalizationHolder.rm.GetString("Client.Core_715"), LocalizationHolder.rm.GetString("Client.Core_716"), "", "imgEventLogFilterIcon", true, 10);
    AdjustableViewsHelper.RegisterView("Config", LocalizationHolder.rm.GetString("Client.Core_717"), LocalizationHolder.rm.GetString("Client.Core_718"), "", "", true, 30);
    AdjustableViewsHelper.RegisterView("LogStatistics", "Статистика", "Просмотр статистики журнала событий", "", "", true, 20);
    AdjustableViewsHelper.RegisterView("UserEvents", LocalizationHolder.rm.GetString("Client.Core_323"), LocalizationHolder.rm.GetString("Client.Core_719"), "", "imgEventLogIcon", true, 66);
    AdjustableViewsHelper.RegisterView("Thumbnails", LocalizationHolder.rm.GetString("Client.Core_720"), LocalizationHolder.rm.GetString("Client.Core_721"), "", "imgThumbnails", true, 35);
    AdjustableViewsHelper.RegisterView("CalcFormulaView", LocalizationHolder.rm.GetString("Client.Core_264"), LocalizationHolder.rm.GetString("Client.Core_722"), "", "imgVersionRuleEditor", true, 11);
    AdjustableViewsHelper.RegisterView("EventLogPropertiesView", LocalizationHolder.rm.GetString("Client.Core_609"), LocalizationHolder.rm.GetString("Client.Core_725"), "", "imgDocument", true, 3);
    AdjustableViewsHelper.RegisterView("CacheMonitoring", LocalizationHolder.rm.GetString("Client.Core_611"), LocalizationHolder.rm.GetString("Client.Core_727"), "", "", true, 1010);
    AdjustableViewsHelper.RegisterView("RolesSettingsView", LocalizationHolder.rm.GetString("Client.Core_667"), LocalizationHolder.rm.GetString("Client.Core_728"), "", "imgRolesSettings", true, 40);
    AdjustableViewsHelper.RegisterView("RolesContextMenusView", LocalizationHolder.rm.GetString("Client.Core_644"), LocalizationHolder.rm.GetString("Client.Core_729"), "", "imgRolesContextMenus", true, 40);
    AdjustableViewsHelper.RegisterView("RolesPluginsView", LocalizationHolder.rm.GetString("Client.Core_648"), LocalizationHolder.rm.GetString("Client.Core_730"), "", "imgRolesPlugins", true, 40);
    AdjustableViewsHelper.RegisterView("Thumbnails.HiveViews", LocalizationHolder.rm.GetString("Client.Core_439"), LocalizationHolder.rm.GetString("Client.Core_731"), "", "imgThumbnails", true, 35);
    AdjustableViewsHelper.RegisterView("SelectionViewObject", LocalizationHolder.rm.GetString("Client.Core_418"), LocalizationHolder.rm.GetString("Client.Core_732"), "", "", true, 30);
    AdjustableViewsHelper.RegisterView("SelectionPropertiesView", LocalizationHolder.rm.GetString("Client.Core_1215"), LocalizationHolder.rm.GetString("Client.Core_1215"), "", "", true, 1);
    AdjustableViewsHelper.RegisterView("ClassificatorPropertiesView", LocalizationHolder.rm.GetString("Client.Core_1520"), LocalizationHolder.rm.GetString("Client.Core_1520"), "", "", true, 1);
    AdjustableViewsHelper.RegisterView("UserToRolesView", LocalizationHolder.rm.GetString("Client.Core_733"), LocalizationHolder.rm.GetString("Client.Core_734"), "", "imgUserRoles", true, 16 /*0x10*/);
    AdjustableViewsHelper.RegisterView("VersionRulesEditorView", LocalizationHolder.rm.GetString("Client.Core_735"), LocalizationHolder.rm.GetString("Client.Core_736"), "", "imgVersionRuleEditor", true, 5);
    AdjustableViewsHelper.RegisterView("ProjectTeamsView", LocalizationHolder.rm.GetString("Client.Core_627"), LocalizationHolder.rm.GetString("Client.Core_627"), "", "imgProject", true, 27);
    AdjustableViewsHelper.RegisterView("ContextsSearchView", LocalizationHolder.rm.GetString("Client.Core_614"), LocalizationHolder.rm.GetString("Client.Core_614"), "", "imgTreeView", true, 27);
    AdjustableViewsHelper.RegisterView("@FormDesignerObject", LocalizationHolder.rm.GetString("Client.Core_737"), LocalizationHolder.rm.GetString("Client.Core_738"), LocalizationHolder.rm.GetString("Client.Core_739"), "imgCard", true, 8);
    AdjustableViewsHelper.RegisterView(LocalizationHolder.rm.GetString("Client.Core_179"), LocalizationHolder.rm.GetString("Client.Core_740"), LocalizationHolder.rm.GetString("Client.Core_741"), LocalizationHolder.rm.GetString("Client.Core_739"), "", true, 7);
    AdjustableViewsHelper.RegisterView("FormDesignerEditorObjects", LocalizationHolder.rm.GetString("Client.Core_742"), LocalizationHolder.rm.GetString("Client.Core_743"), LocalizationHolder.rm.GetString("Client.Core_739"), "imgNewWindow", true, 7);
    AdjustableViewsHelper.RegisterView("RecordTypeProperties", LocalizationHolder.rm.GetString("Client.Core_744"), LocalizationHolder.rm.GetString("Client.Core_745"), "Imbase", "imgDocumentLayout", true, 10100);
    AdjustableViewsHelper.RegisterView("RecordTypeAttributes", LocalizationHolder.rm.GetString("Client.Core_746"), LocalizationHolder.rm.GetString("Client.Core_747"), "Imbase", "imgDocumentLayout", true, 10200);
    AdjustableViewsHelper.RegisterView("ImbaseIndexesView", LocalizationHolder.rm.GetString("IndexesView_Name"), "", "Imbase", "imgIndexes", false, 10300);
    AdjustableViewsHelper.RegisterView("ChildrenView", LocalizationHolder.rm.GetString("Client.Core_748"), LocalizationHolder.rm.GetString("Client.Core_749"), LocalizationHolder.rm.GetString("Client.Core_750"), "imgContains", true, 20);
    AdjustableViewsHelper.RegisterView("ChildrenView", LocalizationHolder.rm.GetString("Client.Core_751"), LocalizationHolder.rm.GetString("Client.Core_752"), LocalizationHolder.rm.GetString("Client.Core_750"), "imgContains", true, 20);
    AdjustableViewsHelper.RegisterView("TableEdit", LocalizationHolder.rm.GetString("Client.Core_755"), LocalizationHolder.rm.GetString("Client.Core_756"), "Intermech.Expert.Editor", "", true, 0);
    AdjustableViewsHelper.RegisterView("SignsCheck", LocalizationHolder.rm.GetString("Client.Core_757"), LocalizationHolder.rm.GetString("Client.Core_758"), LocalizationHolder.rm.GetString("Client.Core_759"), "imgSign2", true, 70);
    AdjustableViewsHelper.RegisterView("Graphs", LocalizationHolder.rm.GetString("Client.Core_760"), LocalizationHolder.rm.GetString("Client.Core_761"), LocalizationHolder.rm.GetString("Client.Core_759"), "", true, 21);
    AdjustableViewsHelper.RegisterView("OpenKeysView", LocalizationHolder.rm.GetString("Client.Core_762"), LocalizationHolder.rm.GetString("Client.Core_763"), LocalizationHolder.rm.GetString("Client.Core_759"), "", true, 21);
    AdjustableViewsHelper.RegisterView("SignsView", LocalizationHolder.rm.GetString("Client.Core_759"), LocalizationHolder.rm.GetString("Client.Core_764"), LocalizationHolder.rm.GetString("Client.Core_759"), "imgSign", true, 51);
    AdjustableViewsHelper.RegisterView(LocalizationHolder.rm.GetString("Client.Core_765"), LocalizationHolder.rm.GetString("Client.Core_765"), LocalizationHolder.rm.GetString("Client.Core_766"), LocalizationHolder.rm.GetString("Client.Core_767"), "", true, 12);
    AdjustableViewsHelper.RegisterView("PublishObjectsView", LocalizationHolder.rm.GetString("Client.Core_1526"), LocalizationHolder.rm.GetString("Client.Core_1527"), "Intermech.Portal.Client", "", true, 0);
    AdjustableViewsHelper.RegisterView("ObjectsVisibilityView", LocalizationHolder.rm.GetString("Client.Core_1183"), LocalizationHolder.rm.GetString("Client.Core_1237"), "Navigator", "imgObjectVisibility", true, 65);
    AdjustableViewsHelper.RegisterView("DocumentsThumbnailView", "Превью", "Предпросмотр списка документов в виде содержимого", "", "imgThumbnails", true, 36);
  }
}
