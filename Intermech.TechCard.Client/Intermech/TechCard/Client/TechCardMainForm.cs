// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TechCardMainForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.TechAcad.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>Summary description for TechCardMainForm.</summary>
public class TechCardMainForm : Form
{
  private ImageList imageList1;
  private ComboBoxItem comboBoxItem1;
  internal Intermech.Bars.ToolBar toolBarTechCard;
  internal MenuBar menuBarTechCard;
  private MenuBarItem menuBarItemTechCard0;
  private MenuButtonItem mbiTest;
  private MenuButtonItem AutosSetup;
  private IContainer components;
  internal MenuButtonItem mbiAcadService;
  private MenuButtonItem menuButtonItem2;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechCardMainForm));
    this.imageList1 = new ImageList(this.components);
    this.toolBarTechCard = new Intermech.Bars.ToolBar();
    this.comboBoxItem1 = new ComboBoxItem();
    this.menuBarTechCard = new MenuBar();
    this.menuBarItemTechCard0 = new MenuBarItem();
    this.AutosSetup = new MenuButtonItem();
    this.mbiAcadService = new MenuButtonItem();
    this.mbiTest = new MenuButtonItem();
    this.menuButtonItem2 = new MenuButtonItem();
    this.SuspendLayout();
    this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.imageList1, "imageList1");
    this.imageList1.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.toolBarTechCard, "toolBarTechCard");
    this.toolBarTechCard.FullMenus = true;
    this.toolBarTechCard.Guid = new Guid("183331d5-c995-4ecc-9588-7ab45bdb689b");
    this.toolBarTechCard.Hidden = false;
    this.toolBarTechCard.ImageList = this.imageList1;
    this.toolBarTechCard.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.comboBoxItem1
    });
    this.toolBarTechCard.Name = "toolBarTechCard";
    componentResourceManager.ApplyResources((object) this.comboBoxItem1, "comboBoxItem1");
    this.comboBoxItem1.MinimumControlWidth = 200;
    this.comboBoxItem1.Padding.Bottom = 0;
    this.comboBoxItem1.Padding.Left = 1;
    this.comboBoxItem1.Padding.Right = 1;
    this.comboBoxItem1.Padding.Top = 0;
    this.comboBoxItem1.Visible = false;
    this.menuBarTechCard.Guid = new Guid("0229352f-2800-4f5c-9701-c574264a2a29");
    this.menuBarTechCard.Hidden = false;
    this.menuBarTechCard.ImageList = this.imageList1;
    this.menuBarTechCard.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.menuBarItemTechCard0
    });
    componentResourceManager.ApplyResources((object) this.menuBarTechCard, "menuBarTechCard");
    this.menuBarTechCard.Name = "menuBarTechCard";
    this.menuBarTechCard.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.menuBarItemTechCard0, "menuBarItemTechCard0");
    this.menuBarItemTechCard0.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.AutosSetup,
      (ToolbarItemBase) this.mbiAcadService,
      (ToolbarItemBase) this.mbiTest
    });
    this.menuBarItemTechCard0.ShowText = true;
    this.menuBarItemTechCard0.Visible = false;
    componentResourceManager.ApplyResources((object) this.AutosSetup, "AutosSetup");
    this.AutosSetup.ShowText = true;
    this.AutosSetup.Visible = false;
    this.AutosSetup.Click += new EventHandler(this.AutosSetup_Click);
    componentResourceManager.ApplyResources((object) this.mbiAcadService, "mbiAcadService");
    this.mbiAcadService.ShowText = true;
    this.mbiAcadService.Click += new EventHandler(this.mbiAcadService_Click);
    componentResourceManager.ApplyResources((object) this.mbiTest, "mbiTest");
    this.mbiTest.ImageIndex = 2;
    this.mbiTest.ShowText = true;
    this.mbiTest.Visible = false;
    this.mbiTest.Click += new EventHandler(this.mbiTest_Click);
    componentResourceManager.ApplyResources((object) this.menuButtonItem2, "menuButtonItem2");
    this.menuButtonItem2.ShowText = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.toolBarTechCard);
    this.Controls.Add((Control) this.menuBarTechCard);
    this.Name = nameof (TechCardMainForm);
    this.Tag = (object) " ";
    this.ResumeLayout(false);
  }

  /// <summary>Initialize class data</summary>
  private void InitializeData()
  {
  }

  /// <summary>Конструктор</summary>
  public TechCardMainForm()
  {
    this.InitializeComponent();
    this.InitializeData();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.components?.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void AutosSetup_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiTest_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  internal void mbiAcadService_Click(object sender, EventArgs e)
  {
    ITechAcadService service = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      service.LoadAcad(TechAcadLoadMode.Normal);
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_479"), LocalizationHolder.rm.GetString("TechCard.Client_138"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }
}
