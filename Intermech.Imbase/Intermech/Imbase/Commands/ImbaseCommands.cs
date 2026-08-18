// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Commands.ImbaseCommands
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Imbase.Commands;

internal class ImbaseCommands
{
  internal static void Register()
  {
    int groupID = 6;
    IFactory service1 = ServicesManager.GetService(typeof (IFactory)) as IFactory;
    INamedImageList service2 = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    ImbaseCommandsProvider provider1 = new ImbaseCommandsProvider();
    ImbaseContextCommandProvider provider2 = new ImbaseContextCommandProvider();
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseCatalogTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseFavoritesTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseFolderTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseTableRefTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseTableTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseTableMixTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseTemplateTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseItemTypeID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(1, Intermech.Imbase.Consts.ImbaseRootObjectTypeID, (ICommandsProvider) new ImbaseCopyPasteProvider());
    service1.AddCommandsProvider(1, (ICommandsProvider) provider2);
    service1.AddCommandsProvider(4, (ICommandsProvider) provider2);
    service1.AddCommandsProvider(Intermech.Imbase.Consts.RootNodeCategoryID, (ICommandsProvider) provider1);
    service1.AddCommandsProvider(Intermech.Imbase.Consts.CatalogsNodeCategoryID, (ICommandsProvider) provider1);
    MenuTemplate contextMenuTemplate = service1.ContextMenuTemplate;
    MenuTemplateNode menuTemplateNode1 = contextMenuTemplate["ObjectComposition"];
    MenuTemplateNode menuTemplateNode2 = contextMenuTemplate["Create"];
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("FindByImages", LocalizationHolder.rm.GetString("Imbase_ContextMenuCommand_FindByImages"), service2.ImageIndex("imgFindByImages"), groupID, 2));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("FindInTables", LocalizationHolder.rm.GetString("Imbase_ContextMenuCommand_FindInTables"), service2.ImageIndex("imgFindInTables"), groupID, 3));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("FindByName", LocalizationHolder.rm.GetString("Imbase_ContextMenuCommand_FindByName"), service2.ImageIndex("imgSearchTree"), groupID, 4));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("FindByIndex", LocalizationHolder.rm.GetString("Imbase_ContextMenuCommand_FindByIndex"), service2.ImageIndex("imgFindByIndex"), groupID, 5));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Imbase.Capitalize", LocalizationHolder.rm.GetString("Imbase_ContextMenuCommand_Capitalize"), -1, groupID, 6));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("CreatedObjectsFromImbase", LocalizationHolder.rm.GetString("Imbase_CreatedObjectsFromImbase"), -1, groupID, 7));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ImportedTableConflictEditor", "Редактировать конфликт импорта", -1, groupID, 8));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("Imbase.TableFieldsRights", LocalizationHolder.rm.GetString("Imbase_ContextMenuCommand_AssignRights"), -1, groupID, 9));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("UnloadToExcel", LocalizationHolder.rm.GetString("Imbase_ExportToExcel"), -1, groupID, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AddToFavorites", LocalizationHolder.rm.GetString("Imbase_Add_To_Favorites"), service2.ImageIndex("addFavorites"), groupID, 11));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("RemoveFromFavorites", LocalizationHolder.rm.GetString("Imbase_Remove_From_Favorites"), service2.ImageIndex("delFavorites"), groupID, 11));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("FindInImbaseTree", LocalizationHolder.rm.GetString("Imbase_Find_In_Tree"), service2.ImageIndex("show"), groupID, 11));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("FindByRecordKey", LocalizationHolder.rm.GetString("FindRecordByCode"), service2.ImageIndex("imgFindInTables"), groupID, 5));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ReplaceAttribute", LocalizationHolder.rm.GetString("Imbase_ContextMenuCommand_ReplaceAttribute"), -1, groupID, 12));
      MenuTemplateNode node = new MenuTemplateNode("IMBASE", "IMBASE", -1, 3, 1);
      contextMenuTemplate.Nodes.Add(node);
      node.Nodes.Add(new MenuTemplateNode("GoToIMBASE", LocalizationHolder.rm.GetString("Imbase_GoToImbase_MenuCaption"), -1, 1, 0));
      node.Nodes.Add(new MenuTemplateNode("ViewInTreeIMBASE", "Показать в иерархии IMBASE", -1, 1, 0));
      node.Nodes.Add(new MenuTemplateNode("LinkToImbase", LocalizationHolder.rm.GetString("Imbase_LinkTo_MenuCaption"), -1, 1, 1));
      node.Nodes.Add(new MenuTemplateNode("RegistryInImbase", LocalizationHolder.rm.GetString("Imbase_RegistryInImbase_MenuCaption"), -1, 1, 2));
      node.Nodes.Add(new MenuTemplateNode("SynchObjects", LocalizationHolder.rm.GetString("Imbase_SynchObjects_MenuCaption"), -1, 1, 3));
      node.Nodes.Add(new MenuTemplateNode("InverseSynchObjects", LocalizationHolder.rm.GetString("Imbase_InverseSynchObjects_MenuCaption"), -1, 1, 4));
      if (ImbaseHelper.IsAdmin)
        node.Nodes.Add(new MenuTemplateNode("UpdateObjectsFromImbase", LocalizationHolder.rm.GetString("Imbase_UpdateObjectsFromImbase_Caption"), -1, 1, 4));
      MenuTemplateNode menuTemplateNode3 = contextMenuTemplate["Paste"];
      menuTemplateNode3.Nodes.Add(new MenuTemplateNode("CreateCopy", LocalizationHolder.rm.GetString("Imbase_CopyTableRef"), -1, 1, 0));
      menuTemplateNode3.Nodes.Add(new MenuTemplateNode("CreatePrototype", LocalizationHolder.rm.GetString("Imbase_CopyTable"), -1, 1, 1));
      menuTemplateNode3.Nodes.Add(new MenuTemplateNode("CreateFolderCopy", LocalizationHolder.rm.GetString("Imbase_CopyTableRefs"), -1, 1, 2));
      menuTemplateNode3.Nodes.Add(new MenuTemplateNode("CreateFolderPrototype", LocalizationHolder.rm.GetString("Imbase_CopyTables"), -1, 1, 3));
      menuTemplateNode1?.Nodes.Add(new MenuTemplateNode("AddFromImbase", LocalizationHolder.rm.GetString("Imbase_ContextMenuCommand_AddFromImbase"), -1, 10, 20));
      if (menuTemplateNode2 == null)
        return;
      menuTemplateNode2.Nodes.Add(new MenuTemplateNode("CreateCatalogsNode", LocalizationHolder.rm.GetString("Imbase_ContextMenu_CreateCatalog"), -1, 10, 0));
      int imageIndex1 = service2.Add(TreeBuilder.ImageList.Images[TreeBuilder.GetIconIndex(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)], "imgImbaseCatalogRecordType");
      menuTemplateNode2.Nodes.Add(new MenuTemplateNode("CreateCatalogRecordsNode", LocalizationHolder.rm.GetString("Imbase_ContextMenu_CreateCatalogRecord"), imageIndex1, 10, 1));
      int imageIndex2 = service2.Add(TreeBuilder.ImageList.Images[TreeBuilder.GetIconIndex(Intermech.Imbase.Consts.ImbaseFavoritesTypeID)], "imgImbaseFavoritesType");
      menuTemplateNode2.Nodes.Add(new MenuTemplateNode("CreateFavoritesNode", LocalizationHolder.rm.GetString("Imbase_Favorites_Folder"), imageIndex2, 10, 2));
      int imageIndex3 = service2.Add(TreeBuilder.ImageList.Images[TreeBuilder.GetIconIndex(Intermech.Imbase.Consts.ImbaseFolderTypeID)], "imgImbaseFolderType");
      menuTemplateNode2.Nodes.Add(new MenuTemplateNode("CreateFoldersNode", LocalizationHolder.rm.GetString("Imbase_ContextMenu_CreateFolder"), imageIndex3, 10, 3));
      int imageIndex4 = service2.Add(TreeBuilder.ImageList.Images[TreeBuilder.GetIconIndex(Intermech.Imbase.Consts.ImbaseTableRefTypeID)], "imgImbaseTablesRefType");
      menuTemplateNode2.Nodes.Add(new MenuTemplateNode("CreateTablesRefNode", LocalizationHolder.rm.GetString("Imbase_ContextMenu_CreateTeblesRef"), imageIndex4, 10, 4));
      int imageIndex5 = service2.Add(TreeBuilder.ImageList.Images[TreeBuilder.GetIconIndex(Intermech.Imbase.Consts.ImbaseTableMixTypeID)], "imgImbaseTablesMixType");
      menuTemplateNode2.Nodes.Add(new MenuTemplateNode("CreateTablesMixNode", LocalizationHolder.rm.GetString("Imbase_ContextMenu_CreateTeblesMix"), imageIndex5, 10, 5));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }
}
