// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.MenuItemForm
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// 
/// </summary>
public class MenuItemForm
{
  private MenuBarItem _mbiForm;
  /// <summary>Link Form to</summary>
  private MenuButtonItem _LinkTo;
  /// <summary>Условие</summary>
  private MenuButtonItem _Condition;
  /// <summary>Autos</summary>
  private MenuButtonItem _Auto;
  private Action<object, EventArgs> _onMenuClick;
  private List<MenuButtonItem> _buttons = new List<MenuButtonItem>();
  private List<MenuButtonItem> _rootButtons = new List<MenuButtonItem>();
  private bool _isWorkFlow;

  /// <summary>
  /// 
  /// </summary>
  public IMenuCommandService MenuCommandSrv { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public bool IsWorkFlow
  {
    get => this._isWorkFlow;
    set
    {
      this._isWorkFlow = value;
      this._LinkTo.Enabled = this._Auto.Enabled = this._Condition.Enabled = !value;
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="onMenuClick"></param>
  public MenuItemForm(Action<object, EventArgs> onMenuClick)
  {
    this._onMenuClick = onMenuClick;
    this.InitMenu();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_Form_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (this.MenuCommandSrv != null)
    {
      foreach (MenuButtonItem button in this._buttons)
      {
        if (button.Tag is CommandID tag)
          button.Enabled = this.CheckCommandEnabled(tag);
      }
    }
    else
      this._rootButtons.ForEach((Action<MenuButtonItem>) (x => x.Enabled = false));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_MenuClick(object sender, EventArgs e)
  {
    if (!(sender is ButtonItemBase buttonItemBase))
      return;
    CommandID tag = buttonItemBase.Tag as CommandID;
    switch (buttonItemBase.CommandName)
    {
      case "Bottoms":
      case "BringToFront":
      case "Centers":
      case "Control":
      case "ControlHeight":
      case "ControlWidth":
      case "HS_Decrease":
      case "HS_Equal":
      case "HS_Increase":
      case "HS_Remove":
      case "Horizontally":
      case "Lefts":
      case "Middles":
      case "Rights":
      case "SendToBack":
      case "TabOrder":
      case "Tops":
      case "VS_Decrease":
      case "VS_Equal":
      case "VS_Increase":
      case "VS_Remove":
      case "Vertically":
        this.MenuCommandSrv.GlobalInvoke(tag);
        break;
      default:
        if (this._onMenuClick == null)
          break;
        this._onMenuClick(sender, e);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="readOnly"></param>
  public void SetMenuReadOnly(bool readOnly)
  {
    bool visible = !readOnly;
    this._LinkTo.Visible = this._Condition.Visible = this._Auto.Visible = visible;
    this._rootButtons.ForEach((Action<MenuButtonItem>) (x => x.Visible = visible));
    if (!this._Condition.Visible)
      return;
    this._Condition.Enabled = ServiceUtils.GetService<IExpertEditor>((object) ApplicationServices.Container, false) != null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="isVisible"></param>
  public void SetMenuVisible(bool isVisible) => this._mbiForm.Visible = isVisible;

  /// <summary>
  /// 
  /// </summary>
  private void InitMenu()
  {
    this._mbiForm = ProviderHolder.BarManager.MenuBar.AddMenuBar(LocalizationHolder.rm.GetString("FormDesigner_119"));
    this._mbiForm.Visible = false;
    this._mbiForm.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.On_Form_BeforePopup);
    MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_MenuItem_ShowToolBox"), new EventHandler(this.On_MenuClick));
    menuButtonItem1.CommandName = "ShowToolBox";
    MenuButtonItem menuButtonItem2 = menuButtonItem1;
    MenuButtonItem menuButtonItem3 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_MenuItem_ShowProperties"), new EventHandler(this.On_MenuClick));
    menuButtonItem3.CommandName = "ShowProperties";
    MenuButtonItem menuButtonItem4 = menuButtonItem3;
    MenuButtonItem menuButtonItem5 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_120"), new EventHandler(this.On_MenuClick));
    menuButtonItem5.CommandName = "LinkTo";
    menuButtonItem5.BeginGroup = true;
    this._LinkTo = menuButtonItem5;
    MenuButtonItem menuButtonItem6 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_121"), new EventHandler(this.On_MenuClick));
    menuButtonItem6.CommandName = "Condition";
    this._Condition = menuButtonItem6;
    MenuButtonItem menuButtonItem7 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_122"), new EventHandler(this.On_MenuClick));
    menuButtonItem7.CommandName = "Auto";
    menuButtonItem7.BeginGroup = true;
    this._Auto = menuButtonItem7;
    MenuButtonItem menuButtonItem8 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_123"));
    menuButtonItem8.BeginGroup = true;
    MenuButtonItem menuButtonItem9 = menuButtonItem8;
    MenuButtonItem[] menuButtonItemArray1 = new MenuButtonItem[6];
    MenuButtonItem menuButtonItem10 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_66"), new EventHandler(this.On_MenuClick));
    menuButtonItem10.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 2];
    menuButtonItem10.CommandName = "Lefts";
    menuButtonItem10.Tag = (object) StandardCommands.AlignLeft;
    menuButtonItemArray1[0] = menuButtonItem10;
    MenuButtonItem menuButtonItem11 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_67"), new EventHandler(this.On_MenuClick));
    menuButtonItem11.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 4];
    menuButtonItem11.CommandName = "Rights";
    menuButtonItem11.Tag = (object) StandardCommands.AlignRight;
    menuButtonItemArray1[1] = menuButtonItem11;
    MenuButtonItem menuButtonItem12 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_68"), new EventHandler(this.On_MenuClick));
    menuButtonItem12.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 6];
    menuButtonItem12.CommandName = "Tops";
    menuButtonItem12.Tag = (object) StandardCommands.AlignTop;
    menuButtonItemArray1[2] = menuButtonItem12;
    MenuButtonItem menuButtonItem13 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_64"), new EventHandler(this.On_MenuClick));
    menuButtonItem13.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 0];
    menuButtonItem13.CommandName = "Bottoms";
    menuButtonItem13.Tag = (object) StandardCommands.AlignBottom;
    menuButtonItemArray1[3] = menuButtonItem13;
    MenuButtonItem menuButtonItem14 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_124"), new EventHandler(this.On_MenuClick));
    menuButtonItem14.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 3];
    menuButtonItem14.CommandName = "Middles";
    menuButtonItem14.Tag = (object) StandardCommands.AlignHorizontalCenters;
    menuButtonItemArray1[4] = menuButtonItem14;
    MenuButtonItem menuButtonItem15 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_125"), new EventHandler(this.On_MenuClick));
    menuButtonItem15.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 1];
    menuButtonItem15.CommandName = "Centers";
    menuButtonItem15.Tag = (object) StandardCommands.AlignVerticalCenters;
    menuButtonItemArray1[5] = menuButtonItem15;
    MenuButtonItem[] menuButtonItemArray2 = menuButtonItemArray1;
    menuButtonItem9.Items.AddRange((ToolbarItemBase[]) menuButtonItemArray2);
    this._buttons.AddRange((IEnumerable<MenuButtonItem>) menuButtonItemArray2);
    MenuButtonItem menuButtonItem16 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_126"));
    MenuButtonItem[] menuButtonItemArray3 = new MenuButtonItem[3];
    MenuButtonItem menuButtonItem17 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_127"), new EventHandler(this.On_MenuClick));
    menuButtonItem17.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 13];
    menuButtonItem17.CommandName = "Control";
    menuButtonItem17.Tag = (object) StandardCommands.SizeToControl;
    menuButtonItemArray3[0] = menuButtonItem17;
    MenuButtonItem menuButtonItem18 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_128"), new EventHandler(this.On_MenuClick));
    menuButtonItem18.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 16 /*0x10*/];
    menuButtonItem18.CommandName = "ControlWidth";
    menuButtonItem18.Tag = (object) StandardCommands.SizeToControlWidth;
    menuButtonItemArray3[1] = menuButtonItem18;
    MenuButtonItem menuButtonItem19 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_129"), new EventHandler(this.On_MenuClick));
    menuButtonItem19.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 14];
    menuButtonItem19.CommandName = "ControlHeight";
    menuButtonItem19.Tag = (object) StandardCommands.SizeToControlHeight;
    menuButtonItemArray3[2] = menuButtonItem19;
    MenuButtonItem[] menuButtonItemArray4 = menuButtonItemArray3;
    menuButtonItem16.Items.AddRange((ToolbarItemBase[]) menuButtonItemArray4);
    this._buttons.AddRange((IEnumerable<MenuButtonItem>) menuButtonItemArray4);
    MenuButtonItem menuButtonItem20 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_130"));
    menuButtonItem20.BeginGroup = true;
    MenuButtonItem menuButtonItem21 = menuButtonItem20;
    MenuButtonItem[] menuButtonItemArray5 = new MenuButtonItem[4];
    MenuButtonItem menuButtonItem22 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_131"), new EventHandler(this.On_MenuClick));
    menuButtonItem22.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 11];
    menuButtonItem22.CommandName = "HS_Equal";
    menuButtonItem22.Tag = (object) StandardCommands.HorizSpaceMakeEqual;
    menuButtonItemArray5[0] = menuButtonItem22;
    MenuButtonItem menuButtonItem23 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_132"), new EventHandler(this.On_MenuClick));
    menuButtonItem23.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 10];
    menuButtonItem23.CommandName = "HS_Increase";
    menuButtonItem23.Tag = (object) StandardCommands.HorizSpaceIncrease;
    menuButtonItemArray5[1] = menuButtonItem23;
    MenuButtonItem menuButtonItem24 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_133"), new EventHandler(this.On_MenuClick));
    menuButtonItem24.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 9];
    menuButtonItem24.CommandName = "HS_Decrease";
    menuButtonItem24.Tag = (object) StandardCommands.HorizSpaceDecrease;
    menuButtonItemArray5[2] = menuButtonItem24;
    MenuButtonItem menuButtonItem25 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_134"), new EventHandler(this.On_MenuClick));
    menuButtonItem25.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 12];
    menuButtonItem25.CommandName = "HS_Remove";
    menuButtonItem25.Tag = (object) StandardCommands.HorizSpaceConcatenate;
    menuButtonItemArray5[3] = menuButtonItem25;
    MenuButtonItem[] menuButtonItemArray6 = menuButtonItemArray5;
    menuButtonItem21.Items.AddRange((ToolbarItemBase[]) menuButtonItemArray6);
    this._buttons.AddRange((IEnumerable<MenuButtonItem>) menuButtonItemArray6);
    MenuButtonItem menuButtonItem26 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_135"));
    MenuButtonItem[] menuButtonItemArray7 = new MenuButtonItem[4];
    MenuButtonItem menuButtonItem27 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_131"), new EventHandler(this.On_MenuClick));
    menuButtonItem27.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 22];
    menuButtonItem27.CommandName = "VS_Equal";
    menuButtonItem27.Tag = (object) StandardCommands.VertSpaceMakeEqual;
    menuButtonItemArray7[0] = menuButtonItem27;
    MenuButtonItem menuButtonItem28 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_132"), new EventHandler(this.On_MenuClick));
    menuButtonItem28.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 21];
    menuButtonItem28.CommandName = "VS_Increase";
    menuButtonItem28.Tag = (object) StandardCommands.VertSpaceIncrease;
    menuButtonItemArray7[1] = menuButtonItem28;
    MenuButtonItem menuButtonItem29 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_133"), new EventHandler(this.On_MenuClick));
    menuButtonItem29.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 20];
    menuButtonItem29.CommandName = "VS_Decrease";
    menuButtonItem29.Tag = (object) StandardCommands.VertSpaceDecrease;
    menuButtonItemArray7[2] = menuButtonItem29;
    MenuButtonItem menuButtonItem30 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_134"), new EventHandler(this.On_MenuClick));
    menuButtonItem30.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 23];
    menuButtonItem30.CommandName = "VS_Remove";
    menuButtonItem30.Tag = (object) StandardCommands.VertSpaceConcatenate;
    menuButtonItemArray7[3] = menuButtonItem30;
    MenuButtonItem[] menuButtonItemArray8 = menuButtonItemArray7;
    menuButtonItem26.Items.AddRange((ToolbarItemBase[]) menuButtonItemArray8);
    this._buttons.AddRange((IEnumerable<MenuButtonItem>) menuButtonItemArray8);
    MenuButtonItem menuButtonItem31 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_136"));
    menuButtonItem31.BeginGroup = true;
    MenuButtonItem menuButtonItem32 = menuButtonItem31;
    MenuButtonItem[] menuButtonItemArray9 = new MenuButtonItem[2];
    MenuButtonItem menuButtonItem33 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_137"), new EventHandler(this.On_MenuClick));
    menuButtonItem33.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 7];
    menuButtonItem33.CommandName = "Horizontally";
    menuButtonItem33.Tag = (object) StandardCommands.CenterHorizontally;
    menuButtonItemArray9[0] = menuButtonItem33;
    MenuButtonItem menuButtonItem34 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_138"), new EventHandler(this.On_MenuClick));
    menuButtonItem34.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 8];
    menuButtonItem34.CommandName = "Vertically";
    menuButtonItem34.Tag = (object) StandardCommands.CenterVertically;
    menuButtonItemArray9[1] = menuButtonItem34;
    MenuButtonItem[] menuButtonItemArray10 = menuButtonItemArray9;
    menuButtonItem32.Items.AddRange((ToolbarItemBase[]) menuButtonItemArray10);
    this._buttons.AddRange((IEnumerable<MenuButtonItem>) menuButtonItemArray10);
    MenuButtonItem menuButtonItem35 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_139"));
    menuButtonItem35.BeginGroup = true;
    MenuButtonItem menuButtonItem36 = menuButtonItem35;
    MenuButtonItem[] menuButtonItemArray11 = new MenuButtonItem[3];
    MenuButtonItem menuButtonItem37 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_19"), new EventHandler(this.On_MenuClick));
    menuButtonItem37.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 17];
    menuButtonItem37.CommandName = "BringToFront";
    menuButtonItem37.Tag = (object) StandardCommands.BringToFront;
    menuButtonItemArray11[0] = menuButtonItem37;
    MenuButtonItem menuButtonItem38 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_18"), new EventHandler(this.On_MenuClick));
    menuButtonItem38.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 18];
    menuButtonItem38.CommandName = "SendToBack";
    menuButtonItem38.Tag = (object) StandardCommands.SendToBack;
    menuButtonItemArray11[1] = menuButtonItem38;
    MenuButtonItem menuButtonItem39 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_140"), new EventHandler(this.On_MenuClick));
    menuButtonItem39.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 19];
    menuButtonItem39.CommandName = "TabOrder";
    menuButtonItem39.Tag = (object) StandardCommands.TabOrder;
    menuButtonItemArray11[2] = menuButtonItem39;
    MenuButtonItem[] menuButtonItemArray12 = menuButtonItemArray11;
    menuButtonItem36.Items.AddRange((ToolbarItemBase[]) menuButtonItemArray12);
    this._buttons.AddRange((IEnumerable<MenuButtonItem>) menuButtonItemArray12);
    MenuButtonItem menuButtonItem40 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_141"), new EventHandler(this.On_MenuClick));
    menuButtonItem40.BeginGroup = true;
    menuButtonItem40.CommandName = "Reset";
    MenuButtonItem menuButtonItem41 = menuButtonItem40;
    this._mbiForm.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[12]
    {
      menuButtonItem2,
      menuButtonItem4,
      this._LinkTo,
      this._Condition,
      this._Auto,
      menuButtonItem9,
      menuButtonItem16,
      menuButtonItem21,
      menuButtonItem26,
      menuButtonItem32,
      menuButtonItem36,
      menuButtonItem41
    });
    this._buttons.AddRange((IEnumerable<MenuButtonItem>) new MenuButtonItem[6]
    {
      menuButtonItem2,
      menuButtonItem4,
      this._LinkTo,
      this._Condition,
      this._Auto,
      menuButtonItem41
    });
    this._rootButtons.AddRange((IEnumerable<MenuButtonItem>) new MenuButtonItem[6]
    {
      menuButtonItem9,
      menuButtonItem16,
      menuButtonItem21,
      menuButtonItem26,
      menuButtonItem32,
      menuButtonItem36
    });
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="commandID"></param>
  /// <returns></returns>
  private bool CheckCommandEnabled(CommandID commandID)
  {
    bool flag = false;
    if (this.MenuCommandSrv != null)
    {
      MenuCommand command = this.MenuCommandSrv.FindCommand(commandID);
      flag = command != null && command.Enabled;
    }
    return flag;
  }
}
