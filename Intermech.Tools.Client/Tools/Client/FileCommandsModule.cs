// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.FileCommandsModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class FileCommandsModule : InitializerModule
{
  private MenuButtonItem mbtFileImport;
  private MenuButtonItem mbtFileExport;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (!(ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service))
      return;
    this.mbtFileImport = new MenuButtonItem(LocalizationHolder.rm.GetString("Tools.Client_91"));
    this.mbtFileImport.ImageIndex = -1;
    this.mbtFileImport.BeginGroup = true;
    this.mbtFileImport.CommandName = "ImportFiles";
    this.mbtFileImport.Click += new EventHandler(this.ImportFilesClick);
    this.mbtFileExport = new MenuButtonItem(LocalizationHolder.rm.GetString("Tools.Client_222"));
    this.mbtFileExport.ImageIndex = -1;
    this.mbtFileExport.BeginGroup = false;
    this.mbtFileExport.CommandName = "ExportFiles";
    this.mbtFileExport.Click += new EventHandler(this.ExportFilesClick);
    MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[2]
    {
      this.mbtFileImport,
      this.mbtFileExport
    };
    service.RegisterMenuItemsGroup(MainMenuItemSite.ExportImport, MainMenuItemPosition.Second, true, menuButtonItemArray);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.mbtFileImport != null)
    {
      this.mbtFileImport.Dispose();
      this.mbtFileImport = (MenuButtonItem) null;
    }
    if (this.mbtFileExport == null)
      return;
    this.mbtFileExport.Dispose();
    this.mbtFileExport = (MenuButtonItem) null;
  }

  private void ImportFilesClick(object sender, EventArgs e)
  {
    using (new DynamicScope())
    {
      UIVars.UICommand.Declare(new UICommandInfo(LocalizationHolder.rm.GetString("Tools.Client_91")));
      ClientContext.FileImporter.BatchImport(LocalizationHolder.rm.GetString("Tools.Client_120"), ClientContext.FileVault.WorkArea.AreaPath, (Action<long>) null);
    }
  }

  private void ExportFilesClick(object sender, EventArgs e)
  {
    using (new DynamicScope())
    {
      string str = LocalizationHolder.rm.GetString("Tools.Client_222");
      UIVars.UICommand.Declare(new UICommandInfo(str));
      IList<IDBTypedObjectID> objectsToExport = this.GetObjectsToExport();
      if (objectsToExport.Count > 0)
      {
        IFileAttributeEditorService fileAttributeEditorService = ServiceUtils.GetService<IFileAttributeEditorService>((object) ApplicationServices.Container, true);
        CollectionUtils.RemoveAll<IDBTypedObjectID>(objectsToExport, (Predicate<IDBTypedObjectID>) (rootObject =>
        {
          FileAttributeEditMode? attributeEditMode = fileAttributeEditorService.GetFileAttributeEditMode(rootObject.ObjectType);
          return !attributeEditMode.HasValue || attributeEditMode.Value != 0;
        }));
        if (objectsToExport.Count == 0)
        {
          MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter(LocalizationHolder.rm.GetString("SR_295"), str, MessageIcon.Information));
          return;
        }
        VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
        this.RemoveExtraVersions(objectsToExport, editorRule);
        if (objectsToExport.Count > 0)
        {
          IFileVault fileVault = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
          IReplaceFilePolicy replacePolicy = (IReplaceFilePolicy) new PreserveAnyChanges();
          ProgressSinks.DialogService.Invoke(str, ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink =>
          {
            IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink, objectsToExport.Count);
            foreach (IDBTypedObjectID dbTypedObjectId in (IEnumerable<IDBTypedObjectID>) objectsToExport)
            {
              if (progressSink.IsCancelled)
                break;
              progressSink.SetState(dbTypedObjectId.Caption);
              fileVault.WorkArea.Publish((IList<DBObjectState>) fileVault.DBObjectsInfo.CreateStateListForObjectTree(dbTypedObjectId.ObjectID, editorRule), replacePolicy);
              progressUpdater.AddCompletedTasks(1);
            }
          }));
        }
      }
      MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter(LocalizationHolder.rm.GetString("SR_296"), str, MessageIcon.Information));
    }
  }

  private IList<IDBTypedObjectID> GetObjectsToExport()
  {
    ISimpleSelectedItems navigatorSelection = this.GetNavigatorSelection();
    if (navigatorSelection != null && navigatorSelection.Count > 0)
    {
      OrderedList<IDBTypedObjectID> objectsToExport = new OrderedList<IDBTypedObjectID>(navigatorSelection.Count, (IComparer<IDBTypedObjectID>) new FileCommandsModule.DBTypedObjectIDComparer());
      for (int index = 0; index < navigatorSelection.Count; ++index)
      {
        if (navigatorSelection.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
          objectsToExport.Add(itemData);
      }
      if (objectsToExport.Count > 0)
        return (IList<IDBTypedObjectID>) objectsToExport;
    }
    return this.GetUserSelectedObjectsToExport();
  }

  private ISimpleSelectedItems GetNavigatorSelection()
  {
    ICurrentNavWindow service = ServiceUtils.GetService<ICurrentNavWindow>((object) ServicesManager.ServiceContainer, false);
    if (service != null)
    {
      ISelectedItemsHost selectedItemsHost = (ISelectedItemsHost) null;
      if (service.TreeView != null && ((Control) service.TreeView).Focused)
        selectedItemsHost = service.TreeView as ISelectedItemsHost;
      else if (service.ViewsManagers is IViewsManager viewsManagers && viewsManagers.ActiveViewPage != null)
        selectedItemsHost = viewsManagers.ActiveViewPage.Control as ISelectedItemsHost;
      if (selectedItemsHost != null)
        return (ISimpleSelectedItems) selectedItemsHost.SelectedItems;
    }
    return (ISimpleSelectedItems) null;
  }

  private IList<IDBTypedObjectID> GetUserSelectedObjectsToExport()
  {
    object[] objArray = Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Tools.Client_222"), LocalizationHolder.rm.GetString("SR_297"), (IDescriptor) new AllObjectTypesDescriptor(), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableSelectAbstractTypes) ?? new object[0];
    OrderedList<IDBTypedObjectID> selectedObjectsToExport = new OrderedList<IDBTypedObjectID>(objArray.Length, (IComparer<IDBTypedObjectID>) new FileCommandsModule.DBTypedObjectIDComparer());
    foreach (IDBTypedObjectID dbTypedObjectId in objArray)
      selectedObjectsToExport.Add(dbTypedObjectId);
    return (IList<IDBTypedObjectID>) selectedObjectsToExport;
  }

  private void RemoveExtraVersions(
    IList<IDBTypedObjectID> sortedCollection,
    VersionsRulePackage versionsRule)
  {
    for (int index1 = 0; index1 < sortedCollection.Count; ++index1)
    {
      long id = sortedCollection[index1].ID;
      int index2 = index1 + 1;
      while (index2 < sortedCollection.Count && sortedCollection[index2].ID == id)
        ++index2;
      int num1 = index2 - index1;
      if (num1 > 1)
      {
        long num2;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject objectByVersionsRule = sessionKeeper.Session.GetObjectByVersionsRule(id, versionsRule.OwnerId, false);
          num2 = objectByVersionsRule != null ? objectByVersionsRule.ObjectID : sortedCollection[index1].ObjectID;
        }
        int index3 = index1;
        for (; num1 > 0; --num1)
        {
          if (sortedCollection[index3].ObjectID == num2)
            ++index3;
          else
            sortedCollection.RemoveAt(index3);
        }
      }
    }
  }

  private sealed class DBTypedObjectIDComparer : IComparer<IDBTypedObjectID>
  {
    public int Compare(IDBTypedObjectID x, IDBTypedObjectID y)
    {
      if (x == null)
        throw new ArgumentNullException(nameof (x));
      if (y == null)
        throw new ArgumentNullException(nameof (y));
      int num = x.ID.CompareTo(y.ID);
      if (num == 0)
        num = x.ObjectID.CompareTo(y.ObjectID);
      return num;
    }
  }
}
