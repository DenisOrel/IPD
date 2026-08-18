
// Type: Intermech.Navigator.ContextMenu.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.Search.ContextMenus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Navigator.ContextMenu;

public sealed class Services
{
  private static IDictionary Converters = (IDictionary) new HybridDictionary();
  private static ICollector[] Collectors = new ICollector[0];
  /// <summary>
  /// Событие, вызываемое сразу после создания контекстного меню.
  /// </summary>
  public static AfterCreateMenuHandler AfterCreateMenu = (AfterCreateMenuHandler) null;

  /// <summary>
  /// Возвращает таблицу команд, которые могут быть выполнены для указанных
  /// элементов навигации.
  /// </summary>
  /// <param name="items">Коллекция элементов навигации</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  /// <returns>Таблица команд</returns>
  public static CommandsTable GetCommandsTable(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return Services.GetCommandsTable(items, viewServices, true);
  }

  /// <summary>
  /// Возвращает таблицу команд, которые могут быть выполнены для указанных
  /// элементов навигации.
  /// </summary>
  /// <param name="items">Коллекция элементов навигации</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  /// <param name="excludeInvisible">Исключить из списка команд те, которые не должны отображаться в контекстных меню</param>
  /// <returns>Таблица команд</returns>
  public static CommandsTable GetCommandsTable(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    bool excludeInvisible)
  {
    Services.Check(items);
    Services.Check(viewServices);
    Services.CheckServices(viewServices);
    CommandsTableBuilder builder = new CommandsTableBuilder(true);
    if (items.Count > 0)
    {
      ISourceData sourceData = (ISourceData) new SourceData(items, viewServices);
      builder.ExcludeInvisible = excludeInvisible;
      for (int index = 0; index < Services.Collectors.Length; ++index)
        Services.Collectors[index].Execute(sourceData, builder);
    }
    builder.KeepSuppressed = false;
    return builder.ToCommandsTable();
  }

  /// <summary>Выполняет команду с указанным именем.</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandsTable">Таблица команд</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  public static void InvokeCommand(
    string commandName,
    CommandsTable commandsTable,
    System.IServiceProvider viewServices)
  {
    Services.Check(commandName);
    Services.Check(commandsTable);
    Services.Check(viewServices);
    Services.CheckServices(viewServices);
    if (!commandsTable.Contains(commandName))
      throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3811.ssp_imclient_3812()));
    CommandHelper commandHelper = new CommandHelper(commandName, commandsTable[commandName], viewServices);
    string text = Holder.Factory.ContextMenuTemplate[commandName].Text;
    try
    {
      commandHelper.Execute(text);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>
  /// Преобразует таблицу команд контекстного меню в компонент пользовательского интерфейса
  /// указанного типа.
  /// </summary>
  /// <param name="commandsTable">Таблица команд</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  /// <param name="componentType">Тип компонента пользовательского интерфейса</param>
  /// <returns>Компонент пользовательского интерфейса</returns>
  public static Component GetMenu(
    CommandsTable commandsTable,
    System.IServiceProvider viewServices,
    System.Type componentType)
  {
    Services.Check(commandsTable);
    Component contextMenu = ((IConverter) Services.Converters[(object) componentType] ?? throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3811.ssp_imclient_3813()))).ToContextMenu(commandsTable, viewServices);
    if (Services.AfterCreateMenu != null)
      Services.AfterCreateMenu(contextMenu, viewServices);
    return contextMenu;
  }

  /// <summary>
  /// Преобразует таблицу команд контекстного меню в компонент пользовательского интерфейса.
  /// </summary>
  /// <param name="items">Коллекция элементов навигации</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  /// <returns>Компонент пользовательского интерфейса</returns>
  public static MenuBarItem GetMenu(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return (MenuBarItem) Services.GetMenu(Services.GetCommandsTable(items, viewServices), viewServices, typeof (MenuBarItem));
  }

  public static MenuBarItem GetMenuForObjectType(
    ISelectedItems selectedItems,
    System.IServiceProvider serviceProvider)
  {
    try
    {
      IDBTypedObjectID[] typedObjectIds = (IDBTypedObjectID[]) null;
      SelectedItemsHelper.TryGetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIds(selectedItems, out typedObjectIds);
      if (typedObjectIds != null)
      {
        if (typedObjectIds.Length == selectedItems.Count)
        {
          Intermech.Search.ContextMenus.ContextMenu menuForObjectTypes = ServiceLocator.Get<IContextMenuClientService>().FindContextMenuForObjectTypes(((IEnumerable<IDBTypedObjectID>) typedObjectIds).Select<IDBTypedObjectID, int>((Func<IDBTypedObjectID, int>) (o => o.ObjectType)).Distinct<int>().ToArray<int>());
          if (menuForObjectTypes != null)
          {
            if (menuForObjectTypes.GetDescendants().Any<ContextMenuItem>((Func<ContextMenuItem, bool>) (o => !string.IsNullOrEmpty(o.CommandName))))
            {
              MenuTemplate templateFromContextMenu = ContextMenuClientHelper.CreateMenuTemplateFromContextMenu(menuForObjectTypes);
              IFactory factory = ServiceLocator.Get<IFactory>();
              MenuTemplate contextMenuTemplate = factory.ConfiguredContextMenuTemplate;
              try
              {
                factory.ConfiguredContextMenuTemplate = templateFromContextMenu;
                return Services.GetMenu(selectedItems, serviceProvider);
              }
              finally
              {
                factory.ConfiguredContextMenuTemplate = contextMenuTemplate;
              }
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
    return (MenuBarItem) null;
  }

  /// <summary>
  /// Создает по списку идентификаторов версий объектов базы данных коллекцию
  /// элементов навигации.
  /// </summary>
  /// <param name="objectIDs">Массив идентификаторов версий объектов</param>
  /// <returns>Коллекция элементов навигации</returns>
  public static ISelectedItems GetItems(params long[] objectIDs)
  {
    return ObjectExtensions.GetItems(objectIDs);
  }

  private static void InitCollectors()
  {
    Services.Collectors = new ICollector[2];
    Services.Collectors[0] = (ICollector) new MergedCommandsCollector();
    Services.Collectors[1] = (ICollector) new GroupCommandsCollector();
  }

  private static void InitConverters()
  {
    Services.Converters.Add((object) typeof (MenuBarItem), (object) new MenuBarItemConverter());
  }

  private static void InitTemplate()
  {
    MenuTemplate contextMenuTemplate = Holder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ExpandNode", LocalizationHolder.rm.GetString("Client.Core_451"), -1, 10, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ExpandNodeRecursive", LocalizationHolder.rm.GetString("Client.Core_1378"), -1, 10, 11, Keys.Down | Keys.Alt));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CollapseNode", LocalizationHolder.rm.GetString("Client.Core_452"), -1, 10, 12));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("OpenInNewWindow", LocalizationHolder.rm.GetString("Client.Core_453"), Holder.NamedImageList.ImageIndex("imgNavigator"), 5, 40, Keys.Return | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("OpenInParentComposition", "Открыть в составе родительского объекта", -1, 5, 50));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AddTypeToFavorites", LocalizationHolder.rm.GetString("AddTypeToFavorites"), Holder.NamedImageList.ImageIndex("imgAddToFavoritesNavigator"), 6, 40));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SaveToDisk", LocalizationHolder.rm.GetString("Client.Core_1177"), -1, 20, 30, Keys.S | Keys.Shift | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SetVersionsRule", LocalizationHolder.rm.GetString("Client.Core_1230"), Holder.NamedImageList.ImageIndex("imgVersionRuleEditor"), 10, 31 /*0x1F*/));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("EditingContextActivate", LocalizationHolder.rm.GetString("Client.Core_1231"), Holder.NamedImageList.ImageIndex("imgEditingContextsMode"), 10, 32 /*0x20*/));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("EditingContextAdd", LocalizationHolder.rm.GetString("Client.Core_1232"), Holder.NamedImageList.ImageIndex("imgEditingContextsAdd"), 10, 33));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("EditingContextAddComposition", LocalizationHolder.rm.GetString("Client.Core_1233"), Holder.NamedImageList.ImageIndex("imgEditingContextsAddComposition"), 10, 34));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("EditingContextReplaceVersion", "Заменить версию в контексте", -1, 10, 35));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ParametersCard", LocalizationHolder.rm.GetString("Client.Core_455"), Holder.NamedImageList.ImageIndex("imgCard"), 5, 50, Keys.F4));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Refresh", LocalizationHolder.rm.GetString("Client.Core_97"), Holder.NamedImageList.ImageIndex("imgRefresh"), 5, 30, Keys.R | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RestoreSelectionValues", LocalizationHolder.rm.GetString("Client.Core_1379"), -1, 5, 31 /*0x1F*/));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Find", LocalizationHolder.rm.GetString("Client.Core_456"), Holder.NamedImageList.ImageIndex("imgSearch"), 5, 60));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SeekInTree", LocalizationHolder.rm.GetString("Client.Core_457"), Holder.NamedImageList.ImageIndex("imgSearchTree"), 5, 65, Keys.F | Keys.Control));
      MenuTemplateNode node1 = new MenuTemplateNode("Create", LocalizationHolder.rm.GetString("Client.Core_458"), -1, 15, 10);
      contextMenuTemplate.Nodes.Add(node1);
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AddFolder", LocalizationHolder.rm.GetString("Client.Core_459"), Holder.NamedImageList.ImageIndex("imgNewFolder"), 15, 2));
      node1.Nodes.Add(new MenuTemplateNode("CreateNew", LocalizationHolder.rm.GetString("Client.Core_461"), Holder.NamedImageList.ImageIndex("imgNewItem"), 10, 10, Keys.N | Keys.Control));
      node1.Nodes.Add(new MenuTemplateNode("CreateProto", LocalizationHolder.rm.GetString("Client.Core_462"), -1, 10, 20));
      node1.Nodes.Add(new MenuTemplateNode("CreateLinkedProto", "По прототипу в составе", -1, 10, 25));
      node1.Nodes.Add(new MenuTemplateNode("CreateVersion", LocalizationHolder.rm.GetString("Client.Core_463"), -1, 10, 30, Keys.N | Keys.Control | Keys.Alt));
      node1.Nodes.Add(new MenuTemplateNode("CreateVersionAnotherType", LocalizationHolder.rm.GetString("Client.Core_1675"), -1, 10, 30));
      node1.Nodes.Add(new MenuTemplateNode("CreateInclude", LocalizationHolder.rm.GetString("Client.Core_464"), -1, 10, 15));
      node1.Nodes.Add(new MenuTemplateNode("CreateLinkedContext", LocalizationHolder.rm.GetString("Client.Core_CreateLinkedContext"), -1, 10, 100));
      node1.Nodes.Add(new MenuTemplateNode("CreateSnapshot", LocalizationHolder.rm.GetString("Snapshot"), Holder.NamedImageList.ImageIndex("imgSnapshot"), 20, 35));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RestoreSnapshot", LocalizationHolder.rm.GetString("RestoreSnapshot"), -1, 1, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("DeleteSnapshot", LocalizationHolder.rm.GetString("Client.Core_1380"), Holder.NamedImageList.ImageIndex("imgDelete"), 1, 3));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RenameSnapshot", LocalizationHolder.rm.GetString("Client.Core_1625"), -1, 1, 2));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CompareSnapshot", LocalizationHolder.rm.GetString("Client.Core_1632"), -1, 1, 5));
      MenuTemplateNode node2 = new MenuTemplateNode("Attributes", LocalizationHolder.rm.GetString("Client.Core_54"), -1, 30, 70);
      contextMenuTemplate.Nodes.Add(node2);
      node2.Nodes.Add(new MenuTemplateNode("ShowAttributeHistory", LocalizationHolder.rm.GetString("AttributeHistory"), -1, 5, 10));
      node2.Nodes.Add(new MenuTemplateNode("EditAttributeValue", LocalizationHolder.rm.GetString("Client.Core_465"), -1, 10, 10));
      node2.Nodes.Add(new MenuTemplateNode("AddAttribute", LocalizationHolder.rm.GetString("Client.Core_466"), -1, 10, 20));
      node2.Nodes.Add(new MenuTemplateNode("AddAttributeGroup", LocalizationHolder.rm.GetString("Client.Core_467"), -1, 10, 30));
      node2.Nodes.Add(new MenuTemplateNode("DeleteAttribute", LocalizationHolder.rm.GetString("Client.Core_1212"), -1, 10, 40));
      node2.Nodes.Add(new MenuTemplateNode("DeleteAttributeGroup", LocalizationHolder.rm.GetString("Client.Core_468"), -1, 10, 50));
      node2.Nodes.Add(new MenuTemplateNode("ObjectsDiff", "Сравнить атрибуты", -1, 10, 60));
      MenuTemplateNode node3 = new MenuTemplateNode("Relation attributes", "Атрибуты связей", -1, 30, 71);
      contextMenuTemplate.Nodes.Add(node3);
      node3.Nodes.Add(new MenuTemplateNode("EditRelationAttributeValue", LocalizationHolder.rm.GetString("Client.Core_465"), -1, 11, 10));
      node3.Nodes.Add(new MenuTemplateNode("AddRelationAttribute", LocalizationHolder.rm.GetString("Client.Core_466"), -1, 11, 20));
      node3.Nodes.Add(new MenuTemplateNode("AddRelationAttributeGroup", LocalizationHolder.rm.GetString("Client.Core_467"), -1, 11, 30));
      node3.Nodes.Add(new MenuTemplateNode("DeleteRelationAttribute", LocalizationHolder.rm.GetString("Client.Core_1212"), -1, 11, 40));
      node3.Nodes.Add(new MenuTemplateNode("DeleteRelationAttributeGroup", LocalizationHolder.rm.GetString("Client.Core_468"), -1, 11, 50));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("OpenDocument", LocalizationHolder.rm.GetString("Client.Core_469"), -1, 15, 15));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("EditDocument", LocalizationHolder.rm.GetString("Client.Core_470"), -1, 15, 25, Keys.F4 | Keys.Shift));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ViewDocument", LocalizationHolder.rm.GetString("Client.Core_471"), Holder.NamedImageList.ImageIndex("imgView"), 15, 30, Keys.F3));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("PrintDocument", LocalizationHolder.rm.GetString("Client.Core_472"), Holder.NamedImageList.ImageIndex("imgPrint"), 15, 35, Keys.P | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("PrintDocumentPDF", LocalizationHolder.rm.GetString("PrintPdfCommand"), -1, 15, 35));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("OpenWith", LocalizationHolder.rm.GetString("Client.Core_473"), -1, 15, 20));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ViewWithOptions", "Смотреть...", Holder.NamedImageList.ImageIndex("imgView"), 15, 32 /*0x20*/));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SaveToSnapshot", LocalizationHolder.rm.GetString("Client.Core_1620"), Holder.NamedImageList.ImageIndex("imgSnapshot"), 16 /*0x10*/, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CheckOut", LocalizationHolder.rm.GetString("Client.Core_474"), Holder.NamedImageList.ImageIndex("imgCheckOut"), 50, 10, Keys.F9));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CheckIn", LocalizationHolder.rm.GetString("Client.Core_475"), Holder.NamedImageList.ImageIndex("imgCheckIn"), 50, 40, Keys.F10 | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SaveChanges", LocalizationHolder.rm.GetString("Client.Core_476"), Holder.NamedImageList.ImageIndex("imgSaveChanges"), 50, 20, Keys.F10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CancelChanges", LocalizationHolder.rm.GetString("Client.Core_477"), Holder.NamedImageList.ImageIndex("imgCancelChanges"), 50, 30, Keys.F12));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AdminCancelChanges", LocalizationHolder.rm.GetString("Client.Core_478"), Holder.NamedImageList.ImageIndex("imgAdminCancelChanges"), 50, 50, Keys.C | Keys.Shift | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Synchronize", LocalizationHolder.rm.GetString("Client.Core_479"), -1, 50, 8));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ConsistsFrom", LocalizationHolder.rm.GetString("Client.Core_480"), -1, 58, 20, Keys.F2 | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("EntersIn", LocalizationHolder.rm.GetString("Client.Core_276"), -1, 58, 30, Keys.F2 | Keys.Shift));
      MenuTemplateNode node4 = new MenuTemplateNode("Lifecycle", LocalizationHolder.rm.GetString("Client.Core_481"), -1, 30, 60);
      contextMenuTemplate.Nodes.Add(node4);
      node4.Nodes.Add(new MenuTemplateNode("SetLifecycleStep", LocalizationHolder.rm.GetString("Client.Core_482"), Holder.NamedImageList.ImageIndex("imgLCStepDocument"), 10, 10));
      node4.Nodes.Add(new MenuTemplateNode("SetLifecycleStepChilds", LocalizationHolder.rm.GetString("Client.Core_483"), Holder.NamedImageList.ImageIndex("imgLCStepDocument"), 10, 15));
      node4.Nodes.Add(new MenuTemplateNode("VersionHistory", LocalizationHolder.rm.GetString("Client.Core_484"), -1, 10, 20));
      node4.Nodes.Add(new MenuTemplateNode("ObjectHistory", LocalizationHolder.rm.GetString("Client.Core_485"), -1, 10, 30));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ListVersions", LocalizationHolder.rm.GetString("Client.Core_324"), Holder.NamedImageList.ImageIndex("imgVersionsTree"), 30, 10, Keys.F5 | Keys.Control));
      MenuTemplateNode node5 = new MenuTemplateNode("ObjectComposition", LocalizationHolder.rm.GetString("Client.Core_1180"), -1, 30, 30);
      contextMenuTemplate.Nodes.Add(node5);
      node5.Nodes.Add(new MenuTemplateNode("Add", LocalizationHolder.rm.GetString("Client.Core_460"), -1, 10, 20));
      node5.Nodes.Add(new MenuTemplateNode("Exclude", LocalizationHolder.rm.GetString("Client.Core_95"), Holder.NamedImageList.ImageIndex("imgExclude"), 10, 30, Keys.Delete));
      node5.Nodes.Add(new MenuTemplateNode("ReplaceObjectInComposition", LocalizationHolder.rm.GetString("Client.Core_ReplaceObjectInComposition"), -1, 10, 40));
      node5.Nodes.Add(new MenuTemplateNode("ReplaceObjectVersionInComposition", LocalizationHolder.rm.GetString("Client.Core_ReplaceObjectVersionInComposition"), -1, 10, 50));
      MenuTemplateNode menuTemplateNode = new MenuTemplateNode("CreateInComposition", LocalizationHolder.rm.GetString("Client.Core_1181"), -1, 10, 10);
      node5.Nodes.Add(menuTemplateNode);
      Services.FillCreateInCompositionNode(menuTemplateNode);
      node5.Nodes.Add(new MenuTemplateNode("BasedOnTemplate", LocalizationHolder.rm.GetString("Client.Core_1381"), -1, 5, 10));
      MenuTemplateNode node6 = new MenuTemplateNode("AuthFiles", LocalizationHolder.rm.GetString("Client.Core_AuthFiles"), -1, 30, 80 /*0x50*/);
      contextMenuTemplate.Nodes.Add(node6);
      node6.Nodes.Add(new MenuTemplateNode("AuthFilesCreate", LocalizationHolder.rm.GetString("Client.Core_AuthFilesCreate"), -1, 10, 10));
      node6.Nodes.Add(new MenuTemplateNode("AuthFilesView", LocalizationHolder.rm.GetString("Client.Core_AuthFilesView"), -1, 10, 20, Keys.F3 | Keys.Control));
      node6.Nodes.Add(new MenuTemplateNode("AuthFilesSave", "Сохранить на диск", -1, 10, 30));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ChangeDocumentsStamp", LocalizationHolder.rm.GetString("Client.Core_ChangeDocsStamp"), -1, 30, 90));
      MenuTemplateNode node7 = new MenuTemplateNode("Markers", LocalizationHolder.rm.GetString("Client.Core_486"), Holder.NamedImageList.ImageIndex("imgBookmark"), 70, 10);
      contextMenuTemplate.Nodes.Add(node7);
      node7.Nodes.Add(new MenuTemplateNode("InvertMarkers", LocalizationHolder.rm.GetString("Client.Core_487"), Holder.NamedImageList.ImageIndex("imgSelectionReplace"), 10, 10));
      MenuTemplateNode node8 = new MenuTemplateNode("MarkGroup", LocalizationHolder.rm.GetString("Client.Core_488"), -1, 10, 20);
      node7.Nodes.Add(node8);
      node8.Nodes.Add(new MenuTemplateNode("MarkGroupUp", LocalizationHolder.rm.GetString("Client.Core_489"), -1, 10, 10));
      node8.Nodes.Add(new MenuTemplateNode("MarkGroupDown", LocalizationHolder.rm.GetString("Client.Core_490"), -1, 10, 20));
      node8.Nodes.Add(new MenuTemplateNode("MarkGroupAll", LocalizationHolder.rm.GetString("Client.Core_116"), -1, 10, 30, Keys.A | Keys.Control));
      MenuTemplateNode node9 = new MenuTemplateNode("UnMarkGroup", LocalizationHolder.rm.GetString("Client.Core_491"), -1, 10, 30);
      node7.Nodes.Add(node9);
      node9.Nodes.Add(new MenuTemplateNode("UnMarkGroupUp", LocalizationHolder.rm.GetString("Client.Core_489"), -1, 10, 10));
      node9.Nodes.Add(new MenuTemplateNode("UnMarkGroupDown", LocalizationHolder.rm.GetString("Client.Core_490"), -1, 10, 20));
      node9.Nodes.Add(new MenuTemplateNode("UnMarkGroupAll", LocalizationHolder.rm.GetString("Client.Core_116"), -1, 10, 30));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Cut", LocalizationHolder.rm.GetString("Client.Core_129"), Holder.NamedImageList.ImageIndex("imgCut"), 60, 10, Keys.X | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Copy", LocalizationHolder.rm.GetString("Client.Core_98"), Holder.NamedImageList.ImageIndex("imgCopy"), 60, 20, Keys.C | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CopyText", LocalizationHolder.rm.GetString("Client.Core_1615"), Holder.NamedImageList.ImageIndex("imgCopy"), 60, 30));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Paste", LocalizationHolder.rm.GetString("Client.Core_99"), Holder.NamedImageList.ImageIndex("imgPaste"), 60, 40, Keys.V | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("MakeBaseVersion", LocalizationHolder.rm.GetString("Client.Core_1382"), -1, 61, 30, Keys.B | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Delete", LocalizationHolder.rm.GetString("Client.Core_96"), Holder.NamedImageList.ImageIndex("imgDelete"), 62, 40, Keys.Delete | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RestoreObject", LocalizationHolder.rm.GetString("Client.Core_1674"), -1, 62, 41));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CombineObjects", LocalizationHolder.rm.GetString("Client.Core_1626"), -1, 62, 45));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SearchSimilarObjects", LocalizationHolder.rm.GetString("Client.Core_1627"), -1, 100, 10));
      MenuTemplateNode node10 = new MenuTemplateNode("CompareObjects", LocalizationHolder.rm.GetString("Client.Core_1696"), -1, 65, 10);
      contextMenuTemplate.Nodes.Add(node10);
      node10.Nodes.Add(new MenuTemplateNode("ObjectsDiffForCompareObjectsMenu", LocalizationHolder.rm.GetString("Client.Core_54"), -1, 10, 30));
      node10.Nodes.Add(new MenuTemplateNode("CompareFilesForCompareObjectsMenu", LocalizationHolder.rm.GetString("Client.Core_297"), -1, 10, 40));
      node10.Nodes.Add(new MenuTemplateNode("CompareAuthFilesForCompareObjectsMenu", LocalizationHolder.rm.GetString("Client.Core_AuthFiles"), -1, 10, 50));
      MenuTemplateNode node11 = new MenuTemplateNode("CompareVersionObjects", LocalizationHolder.rm.GetString("Client.Core_1697"), -1, 65, 20);
      contextMenuTemplate.Nodes.Add(node11);
      node11.Nodes.Add(new MenuTemplateNode("ObjectsDiffForCompareVersionObjectsMenu", LocalizationHolder.rm.GetString("Client.Core_54"), -1, 10, 30));
      node11.Nodes.Add(new MenuTemplateNode("CompareFilesForCompareVersionObjectsMenu", LocalizationHolder.rm.GetString("Client.Core_297"), -1, 10, 40));
      node11.Nodes.Add(new MenuTemplateNode("CompareAuthFilesForCompareVersionObjectsMenu", LocalizationHolder.rm.GetString("Client.Core_AuthFiles"), -1, 10, 50));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CreateFilter", LocalizationHolder.rm.GetString("Client.Core_492"), Holder.NamedImageList.ImageIndex("imgEventLogCreateFilterIcon"), 200, 5));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("DeleteEventLogRecord", LocalizationHolder.rm.GetString("Client.Core_493"), Holder.NamedImageList.ImageIndex("imgDelete"), 200, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ClearEventLog", LocalizationHolder.rm.GetString("Client.Core_494"), -1, 200, 20));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("EventLogExcelReport", LocalizationHolder.rm.GetString("Client.Core_494_Report"), -1, 200, 20));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Permissions", LocalizationHolder.rm.GetString("Client.Core_495"), Holder.NamedImageList.ImageIndex("imgAccess"), 10000, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Properties", LocalizationHolder.rm.GetString("Client.Core_146"), Holder.NamedImageList.ImageIndex("imgProp"), 10000, 20));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RemoveRecentObjects", LocalizationHolder.rm.GetString("Client.Core_496"), -1, 19990, 5));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ClearRecentObjects", LocalizationHolder.rm.GetString("Client.Core_497"), -1, 19990, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("NavigatorContextSearch", LocalizationHolder.rm.GetString("Client.Core_1383"), Holder.NamedImageList.ImageIndex("imgFindText"), 19991, 1, Keys.F | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("NavigatorContextSearchNext", LocalizationHolder.rm.GetString("Client.Core_1384"), Holder.NamedImageList.ImageIndex("imgFindTextNext"), 19991, 2, Keys.F3 | Keys.Control));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ManualSortingSetup", LocalizationHolder.rm.GetString("Client.Core_498"), Holder.NamedImageList.ImageIndex("imgManualSortingSetup"), 19992, 9));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SetupColumns", LocalizationHolder.rm.GetString("Client.Core_499"), Holder.NamedImageList.ImageIndex("imgViewSettings"), 20000, 15));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ResetColumns", LocalizationHolder.rm.GetString("Client.Core_1385"), -1, 20000, 20));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("SetSystemGuid", LocalizationHolder.rm.GetString("Client.Core_1219"), -1, 80 /*0x50*/, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("GetFileHistory", LocalizationHolder.rm.GetString("Client.Core_1386"), -1, 1000, 0));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("GetHTML", LocalizationHolder.rm.GetString("HtmlReport"), -1, 10, 500));
      MenuTemplateNode node12 = new MenuTemplateNode("CopyHyperlink", LocalizationHolder.rm.GetString("CopyHyperlink"), -1, 7000, 10);
      contextMenuTemplate.Nodes.Add(node12);
      node12.Nodes.Add(new MenuTemplateNode("CopyHyperlinkForObjects", LocalizationHolder.rm.GetString("CopyHyperlinkForObjects"), -1, 7000, 20));
      node12.Nodes.Add(new MenuTemplateNode("CopyHyperlinkForObjectCart", LocalizationHolder.rm.GetString("CopyHyperlinkForObjectCart"), -1, 7000, 30));
      node12.Nodes.Add(new MenuTemplateNode("CopyHyperlinkForObjectView", LocalizationHolder.rm.GetString("CopyHyperlinkForObjectView"), -1, 7000, 40));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AddToFavoritesNavigator", LocalizationHolder.rm.GetString("AddToFavoritesNavigator"), Holder.NamedImageList.ImageIndex("imgAddToFavoritesNavigator"), 1450, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RemoveFromFavoritesNavigator", LocalizationHolder.rm.GetString("RemoveFromFavoritesNavigator"), Holder.NamedImageList.ImageIndex("imgRemoveFromFavoritesNavigator"), 1450, 2));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ActivateProject", LocalizationHolder.rm.GetString("ActivateProjectCommand"), -1, 8000, 0));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
      (ServicesManager.GetService(typeof (AdjustableMenuCommands)) as AdjustableMenuCommands).Assign(AdjustableMenusHelper.BuildFromMenuTemplate(contextMenuTemplate));
    }
  }

  /// <summary>
  /// Заполняет узел шаблона "Состав объекта/ Создать в составе"
  /// </summary>
  /// <param name="createInComposition">Узел "Состав объекта/ Создать в составе"</param>
  private static void FillCreateInCompositionNode(MenuTemplateNode createInComposition)
  {
    createInComposition.Nodes.Add(new MenuTemplateNode("CreateNewInComposition", LocalizationHolder.rm.GetString("Client.Core_461"), Holder.NamedImageList.ImageIndex("imgNewItem"), 10, 10, Keys.N | Keys.Alt));
  }

  private static void CheckServices(System.IServiceProvider viewServices)
  {
  }

  internal static void Start()
  {
    Services.InitCollectors();
    Services.InitConverters();
    Services.InitTemplate();
    ObjectExtensions.Start();
  }

  internal static void Stop() => ObjectExtensions.Stop();

  internal static void Check(string commandName)
  {
    if (commandName == null)
      throw new ArgumentNullException(nameof (commandName), LocalizationHolder.rm.GetString("Client.Core_501"));
    if (commandName == string.Empty)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Client.Core_502"), nameof (commandName));
  }

  internal static void Check(CommandInfo commandInfo)
  {
    if (commandInfo == null)
      throw new ArgumentNullException(nameof (commandInfo), LocalizationHolder.rm.GetString("Client.Core_503"));
  }

  internal static void Check(CommandsInfo info)
  {
    if (info == null)
      throw new ArgumentNullException(nameof (info), LocalizationHolder.rm.GetString("Client.Core_504"));
  }

  internal static void Check(ISelectedItems items)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items), LocalizationHolder.rm.GetString("Client.Core_505"));
  }

  internal static void Check(CommandsTable table)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table), LocalizationHolder.rm.GetString("Client.Core_506"));
  }

  internal static void Check(CommandLink commandLink)
  {
    if (commandLink == null)
      throw new ArgumentNullException(nameof (commandLink), LocalizationHolder.rm.GetString("Client.Core_507"));
  }

  internal static void Check(System.IServiceProvider viewServices)
  {
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices), LocalizationHolder.rm.GetString("Client.Core_508"));
  }

  internal static void Check(ISourceData sourceData)
  {
    if (sourceData == null)
      throw new ArgumentNullException(nameof (sourceData), LocalizationHolder.rm.GetString("Client.Core_509"));
  }

  internal static void Check(CommandsTableBuilder builder)
  {
    if (builder == null)
      throw new ArgumentNullException(nameof (builder), LocalizationHolder.rm.GetString("Client.Core_510"));
  }
}
