// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DSettingsControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Imbase.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Tools.Controls;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// 
/// </summary>
internal class Cadmech3DSettingsControl : UserControl
{
  /// <summary>TechCard Navigator контрол</summary>
  private TechNavigatorControl _techNavControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Инициализация кастом контролов</summary>
  private void InitializeCustomControls()
  {
    this._techNavControl = new TechNavigatorControl();
    this.Controls.Add((Control) this._techNavControl);
    this._techNavControl.Dock = DockStyle.Fill;
    this._techNavControl.BringToFront();
    this._techNavControl.Location = new Point(8, 8);
    this._techNavControl.Name = "techNavControl";
    this._techNavControl.ViewsManager.AllowedViews = new string[5]
    {
      "Cadmech3DSettingsParamView",
      "PropertiesView",
      "ChildrenView",
      "SelectionViewObject",
      "ObjectProperties"
    };
    this._techNavControl.TabIndex = 0;
    this._techNavControl.Services.AddService(typeof (IIMCadSettingsService), (object) new IMCadSettingsService());
    this.HandleDestroyed += new EventHandler(this.Cadmech3DSettingsControl_HandleDestroyed);
  }

  /// <summary>
  /// 
  /// </summary>
  internal void LoadControlSettings()
  {
    HybridDictionary config = new HybridDictionary(1);
    TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.Position, (IDictionary) config);
    if (!config.Contains((object) "techNavControl_TreeView_Width"))
      return;
    this._techNavControl.TreeView.Width = (int) config[(object) "techNavControl_TreeView_Width"];
  }

  /// <summary>
  /// 
  /// </summary>
  internal void SaveControlSettings()
  {
    HybridDictionary config = new HybridDictionary(1);
    if (this._techNavControl != null)
      config.Add((object) "techNavControl_TreeView_Width", (object) this._techNavControl.TreeView.Width);
    TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.Position, (IDictionary) config);
  }

  /// <summary>
  /// 
  /// </summary>
  public Cadmech3DSettingsControl()
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    if (this.DesignMode)
      return;
    this.LoadControlSettings();
    this.LoadSettings();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Cadmech3DSettingsControl_HandleDestroyed(object sender, EventArgs e)
  {
    this.SaveControlSettings();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Cadmech3DSettingsControl_VisibleChanged(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  internal void LoadSettings()
  {
    List<long> catalogIdForObjType;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      catalogIdForObjType = ImbaseUtils.GetCatalogIDForObjType(new int[1]
      {
        TechCardConsts.ObjectTypes.SurfaceParamID
      }, sessionKeeper.Session);
    this._techNavControl.RootDescriptor = catalogIdForObjType.Count == 1 ? (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(catalogIdForObjType[0]) : (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, Intermech.Imbase.Consts.ImbaseCatalogTypeID, "", (IList) catalogIdForObjType);
  }

  /// <summary>
  /// 
  /// </summary>
  internal void SaveSettings()
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (Cadmech3DSettingsControl);
    this.VisibleChanged += new EventHandler(this.Cadmech3DSettingsControl_VisibleChanged);
    this.ResumeLayout(false);
  }
}
