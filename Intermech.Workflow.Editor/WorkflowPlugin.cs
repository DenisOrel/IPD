// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Editor.WorkflowPlugin
// Assembly: Intermech.Workflow.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 48E18BC1-AABA-4AA1-97DA-4BBD788BE326
// Assembly location: D:\IPS\Client\Intermech.Workflow.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Editor.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Workflow;
using Intermech.NavBars;
using Intermech.Navigator.ContextMenu;
using Intermech.Protection;
using Intermech.Search;
using Intermech.Workflow.Base;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Editor;

/// <summary>
/// Плагин "Редактор шаблонов процессов". Добавляет кнопку "Редактор шаблонов процессов" в
/// главное меню и на панель приложений.
/// </summary>
public class WorkflowPlugin : IPackage, ICommandTarget
{
  private MenuBarItem _schemeMI;
  private WorkflowStartForm _startDock;
  private wfEditorForm _activeEditor;

  /// <summary>Возвращает имя плагина.</summary>
  public string Name => LocalizationHolder.rm.GetString("Workflow.Editor_9");

  /// <summary>
  /// Инициализирует плагин при загрузке. При этом происходит
  /// добавление кнопок в главное меню и на панель приложений.
  /// </summary>
  /// <param name="serviceProvider"></param>
  public void Load(System.IServiceProvider serviceProvider)
  {
    if (!(serviceProvider.GetService(typeof (ILicenser)) is ILicenser service1))
      throw new ProtectionException(LocalizationHolder.rm.GetString("Workflow.Editor_10"));
    int appId = 366;
    byte[][] numArray = new byte[32 /*0x20*/][]
    {
      new byte[16 /*0x10*/]
      {
        (byte) 198,
        (byte) 215,
        (byte) 249,
        (byte) 245,
        (byte) 243,
        (byte) 128 /*0x80*/,
        (byte) 222,
        (byte) 228,
        (byte) 200,
        (byte) 238,
        (byte) 6,
        (byte) 159,
        (byte) 16 /*0x10*/,
        (byte) 39,
        (byte) 218,
        (byte) 120
      },
      new byte[16 /*0x10*/]
      {
        (byte) 179,
        (byte) 26,
        (byte) 112 /*0x70*/,
        (byte) 162,
        (byte) 197,
        (byte) 37,
        (byte) 17,
        (byte) 190,
        (byte) 87,
        (byte) 199,
        (byte) 50,
        (byte) 238,
        (byte) 227,
        (byte) 164,
        (byte) 215,
        (byte) 56
      },
      new byte[16 /*0x10*/]
      {
        (byte) 58,
        (byte) 227,
        (byte) 113,
        (byte) 114,
        (byte) 46,
        (byte) 108,
        (byte) 100,
        (byte) 132,
        (byte) 37,
        (byte) 40,
        (byte) 175,
        (byte) 76,
        (byte) 133,
        (byte) 246,
        (byte) 174,
        (byte) 161
      },
      new byte[16 /*0x10*/]
      {
        (byte) 83,
        (byte) 107,
        (byte) 86,
        (byte) 145,
        (byte) 5,
        (byte) 80 /*0x50*/,
        (byte) 238,
        (byte) 218,
        (byte) 60,
        (byte) 92,
        (byte) 29,
        (byte) 150,
        (byte) 83,
        (byte) 34,
        (byte) 186,
        (byte) 182
      },
      new byte[16 /*0x10*/]
      {
        (byte) 161,
        byte.MaxValue,
        (byte) 31 /*0x1F*/,
        (byte) 40,
        (byte) 30,
        (byte) 150,
        (byte) 227,
        (byte) 5,
        (byte) 245,
        (byte) 81,
        (byte) 80 /*0x50*/,
        (byte) 130,
        (byte) 203,
        (byte) 11,
        (byte) 148,
        (byte) 88
      },
      new byte[16 /*0x10*/]
      {
        (byte) 87,
        (byte) 39,
        (byte) 44,
        (byte) 227,
        (byte) 144 /*0x90*/,
        (byte) 26,
        (byte) 5,
        (byte) 52,
        (byte) 63 /*0x3F*/,
        (byte) 45,
        (byte) 212,
        (byte) 106,
        (byte) 231,
        (byte) 80 /*0x50*/,
        (byte) 74,
        (byte) 237
      },
      new byte[16 /*0x10*/]
      {
        (byte) 136,
        (byte) 185,
        (byte) 18,
        (byte) 86,
        (byte) 114,
        (byte) 72,
        (byte) 236,
        (byte) 89,
        (byte) 115,
        (byte) 117,
        (byte) 208 /*0xD0*/,
        (byte) 202,
        (byte) 208 /*0xD0*/,
        (byte) 145,
        (byte) 95,
        (byte) 209
      },
      new byte[16 /*0x10*/]
      {
        (byte) 139,
        (byte) 112 /*0x70*/,
        (byte) 219,
        (byte) 149,
        (byte) 112 /*0x70*/,
        (byte) 145,
        (byte) 12,
        (byte) 21,
        (byte) 201,
        (byte) 245,
        (byte) 79,
        (byte) 184,
        (byte) 3,
        (byte) 181,
        (byte) 202,
        (byte) 160 /*0xA0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 63 /*0x3F*/,
        (byte) 100,
        (byte) 254,
        (byte) 16 /*0x10*/,
        (byte) 8,
        (byte) 178,
        (byte) 90,
        (byte) 163,
        (byte) 0,
        (byte) 107,
        (byte) 125,
        (byte) 221,
        (byte) 28,
        (byte) 226,
        (byte) 184,
        (byte) 25
      },
      new byte[16 /*0x10*/]
      {
        (byte) 181,
        (byte) 165,
        (byte) 247,
        (byte) 99,
        (byte) 44,
        (byte) 32 /*0x20*/,
        (byte) 225,
        (byte) 102,
        (byte) 240 /*0xF0*/,
        (byte) 3,
        (byte) 56,
        (byte) 37,
        (byte) 138,
        (byte) 224 /*0xE0*/,
        (byte) 49,
        (byte) 146
      },
      new byte[16 /*0x10*/]
      {
        (byte) 148,
        (byte) 87,
        (byte) 196,
        (byte) 204,
        (byte) 228,
        (byte) 87,
        (byte) 46,
        (byte) 88,
        (byte) 18,
        (byte) 197,
        (byte) 181,
        (byte) 169,
        (byte) 198,
        (byte) 24,
        (byte) 52,
        (byte) 99
      },
      new byte[16 /*0x10*/]
      {
        (byte) 240 /*0xF0*/,
        (byte) 23,
        (byte) 41,
        (byte) 235,
        (byte) 230,
        (byte) 17,
        (byte) 202,
        (byte) 31 /*0x1F*/,
        (byte) 230,
        (byte) 107,
        (byte) 90,
        (byte) 92,
        (byte) 73,
        (byte) 75,
        (byte) 204,
        (byte) 11
      },
      new byte[16 /*0x10*/]
      {
        (byte) 241,
        (byte) 126,
        (byte) 66,
        (byte) 103,
        (byte) 13,
        (byte) 131,
        (byte) 79,
        (byte) 214,
        (byte) 95,
        (byte) 110,
        (byte) 86,
        (byte) 7,
        (byte) 203,
        (byte) 15,
        (byte) 61,
        (byte) 144 /*0x90*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 90,
        (byte) 189,
        (byte) 105,
        (byte) 0,
        (byte) 177,
        (byte) 113,
        (byte) 175,
        (byte) 208 /*0xD0*/,
        (byte) 47,
        (byte) 77,
        (byte) 164,
        (byte) 237,
        (byte) 211,
        (byte) 73,
        (byte) 8,
        (byte) 210
      },
      new byte[16 /*0x10*/]
      {
        (byte) 188,
        (byte) 182,
        (byte) 53,
        (byte) 121,
        (byte) 205,
        (byte) 147,
        (byte) 237,
        (byte) 231,
        (byte) 54,
        (byte) 165,
        (byte) 253,
        (byte) 67,
        (byte) 252,
        (byte) 176 /*0xB0*/,
        (byte) 221,
        (byte) 231
      },
      new byte[16 /*0x10*/]
      {
        (byte) 58,
        (byte) 177,
        (byte) 123,
        (byte) 111,
        (byte) 78,
        (byte) 53,
        (byte) 227,
        (byte) 37,
        (byte) 64 /*0x40*/,
        (byte) 169,
        (byte) 173,
        (byte) 244,
        (byte) 202,
        (byte) 74,
        (byte) 16 /*0x10*/,
        (byte) 119
      },
      new byte[16 /*0x10*/]
      {
        (byte) 55,
        (byte) 25,
        (byte) 235,
        (byte) 158,
        (byte) 159,
        (byte) 81,
        (byte) 12,
        (byte) 17,
        (byte) 7,
        (byte) 237,
        (byte) 108,
        (byte) 118,
        (byte) 1,
        (byte) 39,
        (byte) 141,
        (byte) 3
      },
      new byte[16 /*0x10*/]
      {
        (byte) 95,
        (byte) 42,
        (byte) 180,
        (byte) 149,
        (byte) 103,
        (byte) 204,
        (byte) 35,
        (byte) 122,
        (byte) 115,
        (byte) 95,
        (byte) 142,
        (byte) 226,
        (byte) 217,
        (byte) 230,
        (byte) 91,
        (byte) 170
      },
      new byte[16 /*0x10*/]
      {
        (byte) 136,
        (byte) 36,
        (byte) 236,
        (byte) 16 /*0x10*/,
        (byte) 226,
        (byte) 186,
        (byte) 150,
        (byte) 94,
        (byte) 145,
        (byte) 90,
        (byte) 48 /*0x30*/,
        (byte) 103,
        (byte) 133,
        (byte) 11,
        (byte) 218,
        (byte) 174
      },
      new byte[16 /*0x10*/]
      {
        (byte) 215,
        (byte) 31 /*0x1F*/,
        (byte) 61,
        (byte) 244,
        (byte) 10,
        (byte) 149,
        (byte) 33,
        (byte) 105,
        (byte) 201,
        (byte) 70,
        (byte) 109,
        (byte) 80 /*0x50*/,
        (byte) 148,
        (byte) 238,
        (byte) 251,
        (byte) 169
      },
      new byte[16 /*0x10*/]
      {
        (byte) 144 /*0x90*/,
        (byte) 110,
        (byte) 208 /*0xD0*/,
        (byte) 177,
        (byte) 36,
        (byte) 158,
        (byte) 241,
        (byte) 107,
        (byte) 169,
        (byte) 36,
        (byte) 114,
        (byte) 224 /*0xE0*/,
        (byte) 38,
        (byte) 118,
        (byte) 230,
        (byte) 14
      },
      new byte[16 /*0x10*/]
      {
        (byte) 249,
        (byte) 12,
        (byte) 13,
        (byte) 52,
        (byte) 30,
        (byte) 227,
        (byte) 190,
        (byte) 19,
        (byte) 254,
        (byte) 109,
        (byte) 95,
        (byte) 181,
        (byte) 231,
        (byte) 33,
        (byte) 54,
        (byte) 211
      },
      new byte[16 /*0x10*/]
      {
        (byte) 24,
        (byte) 250,
        (byte) 187,
        (byte) 216,
        (byte) 196,
        (byte) 204,
        (byte) 5,
        (byte) 124,
        (byte) 131,
        (byte) 140,
        (byte) 13,
        (byte) 69,
        (byte) 32 /*0x20*/,
        (byte) 164,
        (byte) 139,
        (byte) 251
      },
      new byte[16 /*0x10*/]
      {
        (byte) 102,
        (byte) 252,
        (byte) 138,
        (byte) 162,
        (byte) 82,
        (byte) 97,
        (byte) 25,
        (byte) 157,
        (byte) 200,
        (byte) 218,
        (byte) 22,
        (byte) 254,
        (byte) 254,
        (byte) 248,
        (byte) 73,
        (byte) 122
      },
      new byte[16 /*0x10*/]
      {
        (byte) 18,
        (byte) 190,
        (byte) 229,
        (byte) 146,
        (byte) 87,
        (byte) 219,
        (byte) 222,
        (byte) 233,
        (byte) 51,
        (byte) 128 /*0x80*/,
        (byte) 102,
        (byte) 190,
        (byte) 72,
        (byte) 181,
        (byte) 56,
        (byte) 26
      },
      new byte[16 /*0x10*/]
      {
        (byte) 78,
        (byte) 71,
        (byte) 193,
        (byte) 229,
        (byte) 180,
        (byte) 99,
        (byte) 34,
        (byte) 44,
        (byte) 117,
        (byte) 69,
        (byte) 237,
        (byte) 210,
        (byte) 17,
        (byte) 73,
        (byte) 233,
        (byte) 195
      },
      new byte[16 /*0x10*/]
      {
        (byte) 116,
        (byte) 34,
        (byte) 79,
        (byte) 138,
        (byte) 80 /*0x50*/,
        (byte) 2,
        (byte) 233,
        (byte) 196,
        (byte) 105,
        (byte) 83,
        (byte) 46,
        (byte) 152,
        (byte) 145,
        (byte) 171,
        (byte) 75,
        (byte) 5
      },
      new byte[16 /*0x10*/]
      {
        (byte) 178,
        (byte) 225,
        (byte) 83,
        (byte) 33,
        (byte) 8,
        (byte) 148,
        (byte) 236,
        (byte) 112 /*0x70*/,
        (byte) 140,
        (byte) 63 /*0x3F*/,
        (byte) 220,
        (byte) 3,
        (byte) 25,
        (byte) 116,
        (byte) 163,
        (byte) 222
      },
      new byte[16 /*0x10*/]
      {
        (byte) 152,
        (byte) 118,
        (byte) 205,
        (byte) 104,
        (byte) 145,
        (byte) 84,
        (byte) 183,
        (byte) 60,
        (byte) 28,
        (byte) 209,
        (byte) 164,
        (byte) 67,
        (byte) 101,
        (byte) 70,
        (byte) 51,
        (byte) 205
      },
      new byte[16 /*0x10*/]
      {
        (byte) 49,
        (byte) 22,
        (byte) 182,
        (byte) 248,
        (byte) 50,
        (byte) 11,
        (byte) 169,
        (byte) 172,
        (byte) 224 /*0xE0*/,
        (byte) 213,
        (byte) 179,
        (byte) 214,
        (byte) 200,
        (byte) 169,
        (byte) 143,
        (byte) 62
      },
      new byte[16 /*0x10*/]
      {
        (byte) 83,
        (byte) 243,
        (byte) 209,
        (byte) 254,
        (byte) 84,
        (byte) 1,
        (byte) 171,
        (byte) 16 /*0x10*/,
        (byte) 212,
        (byte) 33,
        (byte) 40,
        (byte) 3,
        (byte) 173,
        (byte) 70,
        (byte) 88,
        (byte) 135
      },
      new byte[16 /*0x10*/]
      {
        (byte) 123,
        (byte) 249,
        (byte) 6,
        (byte) 207,
        (byte) 229,
        (byte) 112 /*0x70*/,
        (byte) 251,
        (byte) 211,
        (byte) 109,
        (byte) 70,
        (byte) 180,
        (byte) 38,
        (byte) 17,
        (byte) 42,
        (byte) 26,
        (byte) 211
      }
    };
    service1.AllocateLicense(appId);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      object obj = (object) null;
      try
      {
        obj = sessionKeeper.Session.GetCustomService(typeof (IRouterService));
      }
      catch
      {
      }
      if (obj == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Editor_11"), (object) this.Name));
      GlobalMailSettings.Init(sessionKeeper.Session);
      ClearOldProcessSettings.Init();
    }
    Holder.Init((IPackage) this, serviceProvider);
    int schemeNamedImageIndex = Holder.SchemeNamedImageIndex;
    if (ApplicationServices.Container.GetService(typeof (IMainMenuService)) is IMainMenuService service2)
    {
      MenuButtonItem menuButtonItem = new MenuButtonItem(this.Name);
      menuButtonItem.CommandName = "ShowWfEditor";
      menuButtonItem.Click += new EventHandler(this.ShowWorkflowStartForm);
      menuButtonItem.ImageIndex = schemeNamedImageIndex;
      service2.RegisterMenuItems(MainMenuItemSite.TuningTop, MainMenuItemPosition.Default, menuButtonItem);
    }
    MenuBar menuBar1 = ((BarManager) serviceProvider.GetService(typeof (BarManager))).MenuBar;
    BaseHolder.CommandManager.AddTarget((ICommandTarget) this);
    MenuBarItem menuBar2 = menuBar1.FindMenuBar(BaseHolder.KeyMenuBarName);
    MenuBarItem menuBar3 = menuBar1.FindMenuBar("File");
    if (menuBar3 != null)
    {
      MenuItemBase menuItemBase = menuBar3.FindItem("New");
      if (menuItemBase != null)
      {
        MenuButtonItem menuButtonItem = new MenuButtonItem(LocalizationHolder.rm.GetString("Workflow.Editor_12"));
        menuButtonItem.CommandName = "New.wfScheme";
        menuButtonItem.Icon = BaseHolder.IconService.GetIcon(4, wfConsts.SchemesTypeID);
        BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem);
        menuItemBase.Items.Add((ToolbarItemBase) menuButtonItem);
      }
      this._schemeMI = new MenuBarItem(LocalizationHolder.rm.GetString(sc_22034.ssp_workflow_22035()));
      this._schemeMI.Visible = false;
      MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("Workflow.Editor_14"));
      menuButtonItem1.CommandName = "wfSchemeCheck";
      menuButtonItem1.Shortcut = Shortcut.CtrlF9;
      menuButtonItem1.ShortcutActive = true;
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem1);
      this._schemeMI.Items.Add((ToolbarItemBase) menuButtonItem1);
      MenuButtonItem menuButtonItem2 = new MenuButtonItem(LocalizationHolder.rm.GetString("Workflow.Editor_15"));
      menuButtonItem2.CommandName = "wfSnapToGrid";
      menuButtonItem2.Shortcut = Shortcut.F4;
      menuButtonItem2.ShortcutActive = true;
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem2);
      this._schemeMI.Items.Add((ToolbarItemBase) menuButtonItem2);
      MenuButtonItem menuButtonItem3 = new MenuButtonItem(LocalizationHolder.rm.GetString("Variables_Cmd"));
      menuButtonItem3.CommandName = "wfVariables";
      menuButtonItem3.BeginGroup = true;
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem3);
      this._schemeMI.Items.Add((ToolbarItemBase) menuButtonItem3);
      MenuButtonItem menuButtonItem4 = new MenuButtonItem("Закончить отладку шаблона");
      menuButtonItem4.CommandName = "wfSchemeRelease";
      menuButtonItem4.BeginGroup = true;
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem4);
      this._schemeMI.Items.Add((ToolbarItemBase) menuButtonItem4);
      MenuButtonItem menuButtonItem5 = new MenuButtonItem("Закончить отладку всех связанных шаблонов");
      menuButtonItem5.CommandName = "wfSchemeReleaseAll";
      menuButtonItem5.BeginGroup = false;
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem5);
      this._schemeMI.Items.Add((ToolbarItemBase) menuButtonItem5);
      MenuButtonItem menuButtonItem6 = new MenuButtonItem(LocalizationHolder.rm.GetString("Workflow.Editor_17"));
      menuButtonItem6.CommandName = "wfEditorSettings";
      menuButtonItem6.BeginGroup = true;
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem6);
      this._schemeMI.Items.Add((ToolbarItemBase) menuButtonItem6);
      menuBar1.Items.Insert(menuBar2.Index, (ToolbarItemBase) this._schemeMI);
    }
    if (service2 != null)
    {
      List<MenuButtonItem> menuButtonItemList = new List<MenuButtonItem>();
      MenuButtonItem menuButtonItem7 = new MenuButtonItem(LocalizationHolder.GetString("ImportCmd"));
      menuButtonItem7.BeginGroup = true;
      menuButtonItem7.Click += new EventHandler(this.ImportScheme);
      menuButtonItemList.Add(menuButtonItem7);
      MenuButtonItem menuButtonItem8 = new MenuButtonItem(LocalizationHolder.GetString("ExportCmd"));
      menuButtonItem8.CommandName = "wfExport";
      BaseHolder.CommandManager.Add((ButtonItemBase) menuButtonItem8);
      menuButtonItemList.Add(menuButtonItem8);
      service2.RegisterMenuItemsGroup(MainMenuItemSite.ExportImport, MainMenuItemPosition.Last, false, menuButtonItemList.ToArray());
    }
    DockManager service3 = (DockManager) ApplicationServices.Container.GetService(typeof (DockManager));
    if (service3 != null)
      service3.DocumentContainer.ActiveDocumentChanged += new ActiveDocumentEventHandler(this.DocumentContainer_ActiveDocumentChanged);
    INavigationBar service4 = (INavigationBar) serviceProvider.GetService(typeof (INavigationBar));
    if (service4 != null && service4.FindPane("adminPane") is IAppPane pane)
      pane.Add(this.Name, new EventHandler(this.ShowWorkflowStartForm), schemeNamedImageIndex);
    ((IContentProvider) serviceProvider.GetService(typeof (IContentProvider))).ContentCallback += new GetContentCallback(this.contProvider_ContentCallback);
    try
    {
      wfCommandProvider provider = new wfCommandProvider(this);
      BaseHolder.Factory.AddCommandsProvider(1, wfConsts.SchemesTypeID, (ICommandsProvider) provider);
    }
    catch
    {
    }
    MenuTemplate contextMenuTemplate = BaseHolder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("wfExport", LocalizationHolder.GetString("ExportCmd"), -1, 19999, 0));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    BriefcaseCommandProvider provider1 = new BriefcaseCommandProvider();
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.SchemesTypeID, (ICommandsProvider) provider1);
    BaseHolder.Factory.AddCommandsProvider(1, wfConsts.ProcessesTypeID, (ICommandsProvider) provider1);
    ((IDefaultCommands4ObjTypes) serviceProvider.GetService(typeof (IDefaultCommands4ObjTypes)))?.AddDefaultCommand(wfConsts.SchemesTypeID, "EditDocument", DefaultCommandHandler.ContectMenu);
    ((IObjectCreatorService) ApplicationServices.Container.GetService(typeof (IObjectCreatorService))).RegisterCreatorCustomService(wfConsts.SchemesTypeID, typeof (NewSchemeCreator));
  }

  private void DocumentContainer_ActiveDocumentChanged(object sender, ActiveDocumentEventArgs e)
  {
    this._activeEditor = e.NewActiveDocument == null || !(e.NewActiveDocument.Tag is wfEditorForm) ? (wfEditorForm) null : (wfEditorForm) e.NewActiveDocument.Tag;
    this._schemeMI.Visible = this._activeEditor != null;
    BriefcaseAccessor.GlobalBriefcase = this._activeEditor?.Briefcase;
    BaseHolder.CommandManager.QueryStatus();
    this._startDock?.UpdateFloatingDocks(e.NewActiveDocument != null && e.NewActiveDocument.Guid == Holder.wfEditorDockGuid);
  }

  private DockControl contProvider_ContentCallback(Guid guid, string persistString)
  {
    if (!(guid == Holder.wfEditorDockGuid))
      return (DockControl) null;
    if (this._startDock == null)
      this.CreateDockControl();
    return (DockControl) this._startDock;
  }

  /// <summary>Завершает работу плагина перед выгрузкой.</summary>
  public void Unload()
  {
  }

  private void ShowWorkflowStartForm(object sender, EventArgs e)
  {
    DockManager service = (DockManager) ApplicationServices.Container.GetService(typeof (DockManager));
    if (service != null && this._startDock == null)
    {
      DockControl dockControl = service.FindDockControl(Holder.wfEditorDockGuid);
      if (dockControl != null)
      {
        dockControl.Activate();
        this._startDock = service.FindDockControl(Holder.wfEditorDockGuid) as WorkflowStartForm;
      }
    }
    if (this._startDock == null)
      this.CreateDockControl();
    if (sender == null)
      return;
    this._startDock.Activate();
  }

  private void CreateDockControl()
  {
    this._startDock = new WorkflowStartForm();
    this._startDock.Guid = Holder.wfEditorDockGuid;
    this._startDock.TabImageIndex = Holder.SchemeNamedImageIndex;
    this._startDock.ShowImageInDocumentTab = true;
    this._startDock.Closing += new CancelEventHandler(this.WorkflowStartForm_FormClosing);
    this._startDock.Show((DockManager) ApplicationServices.Container.GetService(typeof (DockManager)));
    this._startDock.LoadState();
  }

  private void WorkflowStartForm_FormClosing(object sender, CancelEventArgs e)
  {
    if (this._startDock != null)
    {
      this._startDock.SaveState();
      this._startDock.OnClosed();
    }
    this._startDock = (WorkflowStartForm) null;
  }

  private void ImportScheme(object sender, EventArgs e) => WorkflowBriefcase.Import();

  public bool Execute(ICommandState commandState)
  {
    if (commandState.CommandName == "New.wfScheme")
    {
      wfFunx.EditProcess(0L);
      return true;
    }
    return this._activeEditor != null && this._activeEditor.Execute(commandState);
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (commandState.CommandName == "New.wfScheme")
      return true;
    return this._activeEditor != null && this._activeEditor.QueryStatus(commandState);
  }
}
