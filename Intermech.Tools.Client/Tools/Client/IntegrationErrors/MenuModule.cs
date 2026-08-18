// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IntegrationErrors.MenuModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.Data;
using System;
using System.Data;

#nullable disable
namespace Intermech.Tools.Client.IntegrationErrors;

internal sealed class MenuModule : InitializerModule
{
  private IFactory navigatorFactory;
  private Func<MenuCommandsProvider> commandsProviderFactory;
  private ISelectionsService selectionsService;
  private MenuTemplateNode showIntegrationErrorsCommandNode;

  public MenuModule(
    IFactory navigatorFactory,
    Func<MenuCommandsProvider> commandsProviderFactory,
    ISelectionsService selectionsService)
  {
    this.navigatorFactory = navigatorFactory;
    this.commandsProviderFactory = commandsProviderFactory;
    this.selectionsService = selectionsService;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.CreateBadObjectsSelectionIfNotExists("Объекты с ошибками интеграции");
    this.AddCommandItemsToContextMenuTemplate();
    this.AddCommandsProviderToNavigator();
  }

  private void CreateBadObjectsSelectionIfNotExists(string selectionName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.FindSelectionByName(selectionName) != 0L)
        return;
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(new Guid("cad00123-306c-11d8-b4e9-00304f19f545")).Create();
      IDBAttribute attributeById = dbObject.GetAttributeByID(IDCache.Default.Name.Id);
      if (attributeById != null)
        attributeById.Value = (object) selectionName;
      ConditionStructure[] conditionStructures = new ConditionStructure[1]
      {
        new ConditionStructure(IDCache.Default.IntegrationErrors.Id, RelationalOperators.AttributeExists, (object) null, LogicalOperators.NONE, 0, true)
      };
      this.selectionsService.SetConditionStructures((object) sessionKeeper.Session, dbObject.ObjectID, conditionStructures);
      this.AddToWorkspace(dbObject.ObjectID);
      dbObject.CommitCreation(true);
    }
  }

  private long FindSelectionByName(string selectionName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(new Guid("cad00123-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams()
      {
        RecordCount = 1,
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        },
        Conditions = new ConditionStructure[1]
        {
          new ConditionStructure(IDCache.Default.Name.Id, RelationalOperators.Equal, (object) selectionName, LogicalOperators.NONE, 0, true)
        }
      });
      return dataTable.Rows.Count != 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
    }
  }

  private void AddToWorkspace(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationType relationType = sessionKeeper.Session.GetRelationType(new Guid("cad0005e-306c-11d8-b4e9-00304f19f545"));
      sessionKeeper.Session.GetRelationCollection(relationType.RelationType).Create(this.GetWorkspaceId(), objectId);
    }
  }

  private long GetWorkspaceId()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return Convert.ToInt64(sessionKeeper.Session.GetObjectCollection(sessionKeeper.Session.IdentHelper.WorkspaceTypeID).Select(new DBRecordSetParams()
      {
        RecordCount = 1,
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        },
        Conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-8, RelationalOperators.Equal, (object) sessionKeeper.Session.UserID, LogicalOperators.NONE, 0, true)
        }
      }).Rows[0][0]);
  }

  private void AddCommandItemsToContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode menuTemplateNode = contextMenuTemplate[Intermech.Tools.Client.IntegratorsContextMenu.MenuConsts.IntegratorsMenuName];
      if (menuTemplateNode == null)
        return;
      this.showIntegrationErrorsCommandNode = new MenuTemplateNode(MenuConsts.ShowIntegrationErrorsCommandName, MenuConsts.ShowIntegrationErrorsDisplayName, -1, 25, 30);
      menuTemplateNode.Nodes.Add(this.showIntegrationErrorsCommandNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private void AddCommandsProviderToNavigator()
  {
    this.navigatorFactory.AddCommandsProvider(1, (ICommandsProvider) this.commandsProviderFactory());
  }

  protected override void DoShutdown()
  {
    this.RemoveCommandItemsFromContextMenuTemplate();
    base.DoShutdown();
  }

  private void RemoveCommandItemsFromContextMenuTemplate()
  {
    if (this.showIntegrationErrorsCommandNode == null)
      return;
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate[Intermech.Tools.Client.IntegratorsContextMenu.MenuConsts.IntegratorsMenuName]?.Nodes.Remove(this.showIntegrationErrorsCommandNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    this.showIntegrationErrorsCommandNode = (MenuTemplateNode) null;
  }
}
