// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.IFolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Bars;
using Intermech.DatabaseConfigurator;
using Intermech.Interfaces.Briefcase;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>Интерфейс папок в DatabaseConfigurator</summary>
public interface IFolder
{
  Guid InstGuid { get; }

  TreeNode Node { get; }

  TreeNode NodeParent { get; }

  bool DelEnabled { get; }

  bool AddChildEnabled { get; }

  bool NeedApply { get; }

  bool NeedSave { get; }

  bool NeedPageSave { get; }

  int FolderType { get; }

  object Id { get; }

  void SetId(object aId);

  int Category { get; }

  string Text { get; }

  bool CanAddChild { get; }

  IFolder AddChild(MenuButtonItem mi);

  IFolder AddChildDubbed(IFolder ifolder);

  bool CanDelete { get; }

  bool Exclude();

  ActionResult Delete(EventHandler postDeleteHandler);

  void Update();

  void UpdateData();

  void FormLostFocus();

  void Populate(bool reload);

  void Populate(bool reload, bool populateFirstSublevel);

  bool LoadData(Panel panel, bool reload);

  bool CanSave { get; }

  bool ApplyData();

  void Copy();

  void Cut();

  void Paste();

  void ExportImage();

  void LocalizationConfig();

  void SetSystemGuid();

  void Cancel();

  void Cancel(bool withRefresh);

  void GetContextMenu(ContextMenuBarItem contextMenu, IEventsDispatcher iEventsDispatcher);

  void SetContextMenuItemStatus(ContextMenuBarItem contextMenu);

  bool IsVirtualFolder { get; }

  bool InChange { get; set; }

  UserControl PropertiesForm { get; }

  void ChangeEventProcessing(object s, EventArgs e);

  int ObjectTypeProcessing { get; }

  ExportAttribute GetExportAttributes(object[] objects);

  IDatabaseConfiguratorControl IDatabaseConfiguratorControl { get; }

  IFolder Clone();
}
