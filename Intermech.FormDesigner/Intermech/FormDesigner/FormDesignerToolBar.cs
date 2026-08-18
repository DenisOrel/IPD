// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.FormDesignerToolBar
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Bars;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// 
/// </summary>
public class FormDesignerToolBar
{
  private Intermech.Bars.ToolBar _tbBar;
  private Action<object, EventArgs> _onMenuClick;
  private List<ButtonItem> _singleButtons = new List<ButtonItem>();
  private List<ButtonItem> _multiButtons = new List<ButtonItem>();

  /// <summary>
  /// 
  /// </summary>
  public IMenuCommandService MenuCommandSrv { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="onMenuClick"></param>
  public FormDesignerToolBar(Action<object, EventArgs> onMenuClick)
  {
    this._onMenuClick = onMenuClick;
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Name = "FormDesignerBar";
    toolBar.Text = LocalizationHolder.rm.GetString("FormDesigner_142");
    toolBar.ImageList = ProviderHolder.iList;
    this._tbBar = toolBar;
    this.InitToolBar();
    this._tbBar.Layout += new LayoutEventHandler(this.On_tbBar_Layout);
    this.CheckToolBar((ISelectionService) null);
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
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_tbBar_Layout(object sender, LayoutEventArgs e) => this.SaveToolBarState();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="service"></param>
  public void CheckToolBar(ISelectionService service)
  {
    bool isEnabled = service != null && service.SelectionCount > 0 && !(service.PrimarySelection is DesForm);
    this._singleButtons.ForEach((Action<ButtonItem>) (x => x.Enabled = isEnabled));
    isEnabled = service != null && service.SelectionCount > 1;
    this._multiButtons.ForEach((Action<ButtonItem>) (x => x.Enabled = isEnabled));
  }

  /// <summary>
  /// 
  /// </summary>
  public void LoadToolBarState()
  {
    Guid container = ProviderHolder.BarManager.FindSuitableContainer(DockStyle.Right).Guid;
    int num1 = 0;
    int num2 = 0;
    bool flag = true;
    if (ProviderHolder.ServiceProvider.GetService(typeof (IConfigurationManager)) is IConfigurationManager service)
    {
      IConfiguration configuration = service.Open("FormDesigner.Editor.ToolBar");
      if (configuration != null)
      {
        if (configuration.HasProperty("Container"))
        {
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (Guid));
          if (converter.CanConvertFrom(typeof (string)))
          {
            string property = configuration.GetProperty("Container");
            container = (Guid) converter.ConvertFrom((object) property);
          }
        }
        if (configuration.HasProperty("DockLine"))
        {
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
          if (converter.CanConvertFrom(typeof (string)))
          {
            string property = configuration.GetProperty("DockLine");
            num1 = (int) converter.ConvertFrom((object) property);
          }
        }
        if (configuration.HasProperty("DockOffset"))
        {
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
          if (converter.CanConvertFrom(typeof (string)))
          {
            string property = configuration.GetProperty("DockOffset");
            num2 = (int) converter.ConvertFrom((object) property);
          }
        }
        if (configuration.HasProperty("Visible"))
        {
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (bool));
          if (converter.CanConvertFrom(typeof (string)))
          {
            string property = configuration.GetProperty("Visible");
            flag = (bool) converter.ConvertFrom((object) property);
          }
        }
        foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this._tbBar.Items)
        {
          if (configuration.HasProperty(toolbarItemBase.CommandName))
          {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof (bool));
            if (converter.CanConvertFrom(typeof (string)))
            {
              string property = configuration.GetProperty(toolbarItemBase.CommandName);
              toolbarItemBase.Visible = (bool) converter.ConvertFrom((object) property);
            }
          }
        }
      }
    }
    this.AddToolBar(container);
    this._tbBar.Visible = flag;
    this._tbBar.DockLine = num1;
    this._tbBar.DockOffset = num2;
  }

  /// <summary>
  /// 
  /// </summary>
  public void SaveToolBarState()
  {
    if (!(ProviderHolder.ServiceProvider.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration configuration = service.Open("FormDesigner.Editor.ToolBar") ?? service.Create("FormDesigner.Editor.ToolBar");
    if (this._tbBar.Parent is ToolBarContainer parent)
      configuration.SetProperty("Container", Convert.ToString((object) parent.Guid));
    configuration.SetProperty("DockLine", Convert.ToString(this._tbBar.DockLine));
    configuration.SetProperty("DockOffset", Convert.ToString(this._tbBar.DockOffset));
    configuration.SetProperty("Visible", Convert.ToString(this._tbBar.Visible));
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this._tbBar.Items)
      configuration.SetProperty(toolbarItemBase.CommandName, Convert.ToString(toolbarItemBase.Visible));
  }

  /// <summary>
  /// 
  /// </summary>
  public void RemoveToolBar()
  {
    this._tbBar.Parent = (Control) null;
    ProviderHolder.BarManager.RemoveToolbar(this._tbBar);
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitToolBar()
  {
    ButtonItem buttonItem1 = new ButtonItem();
    buttonItem1.BeginGroup = true;
    buttonItem1.CommandName = "Lefts";
    buttonItem1.Text = LocalizationHolder.rm.GetString("FormDesigner_144");
    buttonItem1.ImageIndex = 2;
    buttonItem1.Tag = (object) StandardCommands.AlignLeft;
    ButtonItem buttonItem2 = buttonItem1;
    ButtonItem buttonItem3 = new ButtonItem();
    buttonItem3.CommandName = "Centers";
    buttonItem3.Text = LocalizationHolder.rm.GetString("FormDesigner_145");
    buttonItem3.ImageIndex = 1;
    buttonItem3.Tag = (object) StandardCommands.AlignVerticalCenters;
    ButtonItem buttonItem4 = buttonItem3;
    ButtonItem buttonItem5 = new ButtonItem();
    buttonItem5.CommandName = "Rights";
    buttonItem5.Text = LocalizationHolder.rm.GetString("FormDesigner_146");
    buttonItem5.ImageIndex = 4;
    buttonItem5.Tag = (object) StandardCommands.AlignRight;
    ButtonItem buttonItem6 = buttonItem5;
    ButtonItem buttonItem7 = new ButtonItem();
    buttonItem7.CommandName = "Tops";
    buttonItem7.Text = LocalizationHolder.rm.GetString("FormDesigner_147");
    buttonItem7.ImageIndex = 6;
    buttonItem7.Tag = (object) StandardCommands.AlignTop;
    ButtonItem buttonItem8 = buttonItem7;
    ButtonItem buttonItem9 = new ButtonItem();
    buttonItem9.CommandName = "Middles";
    buttonItem9.Text = LocalizationHolder.rm.GetString("FormDesigner_148");
    buttonItem9.ImageIndex = 3;
    buttonItem9.Tag = (object) StandardCommands.AlignHorizontalCenters;
    ButtonItem buttonItem10 = buttonItem9;
    ButtonItem buttonItem11 = new ButtonItem();
    buttonItem11.CommandName = "Bottoms";
    buttonItem11.Text = LocalizationHolder.rm.GetString("FormDesigner_149");
    buttonItem11.ImageIndex = 0;
    buttonItem11.Tag = (object) StandardCommands.AlignBottom;
    ButtonItem buttonItem12 = buttonItem11;
    ButtonItem buttonItem13 = new ButtonItem();
    buttonItem13.BeginGroup = true;
    buttonItem13.CommandName = "ControlWidth";
    buttonItem13.Text = LocalizationHolder.rm.GetString("FormDesigner_150");
    buttonItem13.ImageIndex = 16 /*0x10*/;
    buttonItem13.Tag = (object) StandardCommands.SizeToControlWidth;
    ButtonItem buttonItem14 = buttonItem13;
    ButtonItem buttonItem15 = new ButtonItem();
    buttonItem15.CommandName = "ControlHeight";
    buttonItem15.Text = LocalizationHolder.rm.GetString("FormDesigner_152");
    buttonItem15.ImageIndex = 14;
    buttonItem15.Tag = (object) StandardCommands.SizeToControlHeight;
    ButtonItem buttonItem16 = buttonItem15;
    ButtonItem buttonItem17 = new ButtonItem();
    buttonItem17.CommandName = "Control";
    buttonItem17.Text = LocalizationHolder.rm.GetString("FormDesigner_153");
    buttonItem17.ImageIndex = 13;
    buttonItem17.Tag = (object) StandardCommands.SizeToControl;
    ButtonItem buttonItem18 = buttonItem17;
    ButtonItem buttonItem19 = new ButtonItem();
    buttonItem19.BeginGroup = true;
    buttonItem19.CommandName = "HS_Equal";
    buttonItem19.Text = LocalizationHolder.rm.GetString("FormDesigner_154");
    buttonItem19.ImageIndex = 11;
    buttonItem19.Tag = (object) StandardCommands.HorizSpaceMakeEqual;
    ButtonItem buttonItem20 = buttonItem19;
    ButtonItem buttonItem21 = new ButtonItem();
    buttonItem21.CommandName = "HS_Increase";
    buttonItem21.Text = LocalizationHolder.rm.GetString("FormDesigner_155");
    buttonItem21.ImageIndex = 10;
    buttonItem21.Tag = (object) StandardCommands.HorizSpaceIncrease;
    ButtonItem buttonItem22 = buttonItem21;
    ButtonItem buttonItem23 = new ButtonItem();
    buttonItem23.CommandName = "HS_Decrease";
    buttonItem23.Text = LocalizationHolder.rm.GetString("FormDesigner_156");
    buttonItem23.ImageIndex = 9;
    buttonItem23.Tag = (object) StandardCommands.HorizSpaceDecrease;
    ButtonItem buttonItem24 = buttonItem23;
    ButtonItem buttonItem25 = new ButtonItem();
    buttonItem25.CommandName = "HS_Remove";
    buttonItem25.Text = LocalizationHolder.rm.GetString("FormDesigner_157");
    buttonItem25.ImageIndex = 12;
    buttonItem25.Tag = (object) StandardCommands.HorizSpaceConcatenate;
    ButtonItem buttonItem26 = buttonItem25;
    ButtonItem buttonItem27 = new ButtonItem();
    buttonItem27.BeginGroup = true;
    buttonItem27.CommandName = "VS_Equal";
    buttonItem27.Text = LocalizationHolder.rm.GetString("FormDesigner_158");
    buttonItem27.ImageIndex = 22;
    buttonItem27.Tag = (object) StandardCommands.VertSpaceMakeEqual;
    ButtonItem buttonItem28 = buttonItem27;
    ButtonItem buttonItem29 = new ButtonItem();
    buttonItem29.CommandName = "VS_Increase";
    buttonItem29.Text = LocalizationHolder.rm.GetString("FormDesigner_159");
    buttonItem29.ImageIndex = 21;
    buttonItem29.Tag = (object) StandardCommands.VertSpaceIncrease;
    ButtonItem buttonItem30 = buttonItem29;
    ButtonItem buttonItem31 = new ButtonItem();
    buttonItem31.CommandName = "VS_Decrease";
    buttonItem31.Text = LocalizationHolder.rm.GetString("FormDesigner_160");
    buttonItem31.ImageIndex = 20;
    buttonItem31.Tag = (object) StandardCommands.VertSpaceDecrease;
    ButtonItem buttonItem32 = buttonItem31;
    ButtonItem buttonItem33 = new ButtonItem();
    buttonItem33.CommandName = "VS_Remove";
    buttonItem33.Text = LocalizationHolder.rm.GetString("FormDesigner_161");
    buttonItem33.ImageIndex = 23;
    buttonItem33.Tag = (object) StandardCommands.VertSpaceConcatenate;
    ButtonItem buttonItem34 = buttonItem33;
    ButtonItem buttonItem35 = new ButtonItem();
    buttonItem35.BeginGroup = true;
    buttonItem35.CommandName = "Horizontally";
    buttonItem35.Text = LocalizationHolder.rm.GetString("FormDesigner_162");
    buttonItem35.ImageIndex = 7;
    buttonItem35.Tag = (object) StandardCommands.CenterHorizontally;
    ButtonItem buttonItem36 = buttonItem35;
    ButtonItem buttonItem37 = new ButtonItem();
    buttonItem37.CommandName = "Vertically";
    buttonItem37.Text = LocalizationHolder.rm.GetString("FormDesigner_163");
    buttonItem37.ImageIndex = 8;
    buttonItem37.Tag = (object) StandardCommands.CenterVertically;
    ButtonItem buttonItem38 = buttonItem37;
    ButtonItem buttonItem39 = new ButtonItem();
    buttonItem39.BeginGroup = true;
    buttonItem39.CommandName = "BringToFront";
    buttonItem39.Text = LocalizationHolder.rm.GetString("FormDesigner_19");
    buttonItem39.ImageIndex = 17;
    buttonItem39.Tag = (object) StandardCommands.BringToFront;
    ButtonItem buttonItem40 = buttonItem39;
    ButtonItem buttonItem41 = new ButtonItem();
    buttonItem41.CommandName = "SendToBack";
    buttonItem41.Text = LocalizationHolder.rm.GetString("FormDesigner_18");
    buttonItem41.ImageIndex = 18;
    buttonItem41.Tag = (object) StandardCommands.SendToBack;
    ButtonItem buttonItem42 = buttonItem41;
    ButtonItem buttonItem43 = new ButtonItem();
    buttonItem43.CommandName = "TabOrder";
    buttonItem43.Text = LocalizationHolder.rm.GetString("FormDesigner_140");
    buttonItem43.ImageIndex = 19;
    buttonItem43.Tag = (object) StandardCommands.TabOrder;
    ButtonItem buttonItem44 = buttonItem43;
    this._singleButtons = new List<ButtonItem>()
    {
      buttonItem36,
      buttonItem38,
      buttonItem40,
      buttonItem42
    };
    this._multiButtons = new List<ButtonItem>()
    {
      buttonItem2,
      buttonItem4,
      buttonItem6,
      buttonItem8,
      buttonItem10,
      buttonItem12,
      buttonItem14,
      buttonItem16,
      buttonItem18,
      buttonItem20,
      buttonItem22,
      buttonItem24,
      buttonItem26,
      buttonItem28,
      buttonItem30,
      buttonItem32,
      buttonItem34
    };
    this._singleButtons.ForEach((Action<ButtonItem>) (x => x.Click += new EventHandler(this.On_MenuClick)));
    this._multiButtons.ForEach((Action<ButtonItem>) (x => x.Click += new EventHandler(this.On_MenuClick)));
    this._tbBar.SuspendLayout();
    try
    {
      this._tbBar.Items.AddRange((ToolbarItemBase[]) this._multiButtons.ToArray());
      this._tbBar.Items.AddRange((ToolbarItemBase[]) this._singleButtons.ToArray());
      this._tbBar.Items.Add((ToolbarItemBase) buttonItem44);
    }
    finally
    {
      this._tbBar.ResumeLayout(false);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="container"></param>
  private void AddToolBar(Guid container)
  {
    ProviderHolder.BarManager.AddToolbar(this._tbBar);
    this._tbBar.Parent = (Control) ProviderHolder.BarManager.FindContainer(container);
  }
}
