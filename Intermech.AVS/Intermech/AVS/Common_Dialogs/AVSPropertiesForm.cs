// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.AVSPropertiesForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Bars;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class AVSPropertiesForm : Form
{
  protected long _SchemaObjectID = -1;
  protected long _TemplateObjectID = -1;
  private int _settingsHolderObjType = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelMain;
  private Panel panel2;
  private Button buttonClose;
  private PageViewsManager pageViewsManager;
  private Button button1;

  private AVSPropertiesForm() => this.InitializeComponent();

  public AVSPropertiesForm(long ObjectId)
  {
    this.InitializeComponent();
    this.SchemaObjectID = ObjectId;
  }

  public AVSPropertiesForm(long specificationID, long templateID)
  {
    this.InitializeComponent();
    this._settingsHolderObjType = AvsIDCache.ObjType_Specification;
    this._TemplateObjectID = templateID;
    this.SchemaObjectID = specificationID;
  }

  protected override void OnLoad(EventArgs e)
  {
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2909);
    base.OnLoad(e);
  }

  public static void Execute(long objectId, long templateId)
  {
    using (AVSPropertiesForm avsPropertiesForm = new AVSPropertiesForm(objectId, templateId))
    {
      int num = (int) avsPropertiesForm.ShowDialog();
    }
  }

  protected long SchemaObjectID
  {
    get => this._SchemaObjectID;
    set
    {
      this._SchemaObjectID = value;
      this.UpdateView(this._SchemaObjectID);
    }
  }

  private void buttonClose_Click(object sender, EventArgs e) => this.Close();

  private void UpdateView(long id)
  {
    ISelectedItems items = ObjectExtensions.GetItems(id);
    if (this.pageViewsManager.Services == null)
    {
      ServiceContainer serviceContainer = new ServiceContainer();
      ViewStateService serviceInstance1 = new ViewStateService(ViewStateFlags.NoCompositionView | ViewStateFlags.NoContainsInView | ViewStateFlags.NoEventsView | ViewStateFlags.NoGroupingObjectsViews | ViewStateFlags.InParametersCard);
      serviceContainer.AddService(typeof (IViewState), (object) serviceInstance1);
      AVSTemplatesViewsService serviceInstance2 = new AVSTemplatesViewsService()
      {
        ShowAll = true,
        DocumetnTemplateId = this._TemplateObjectID
      };
      serviceContainer.AddService(typeof (IAVSTemplatesViewsService), (object) serviceInstance2);
      serviceContainer.AddService(typeof (ICommandManager), (object) (ICommandManager) ServicesManager.GetService(typeof (ICommandManager)));
      serviceContainer.AddService(typeof (INotificationService), (object) (INotificationService) ServicesManager.GetService(typeof (INotificationService)));
      this.pageViewsManager.Services = (System.IServiceProvider) serviceContainer;
      this.pageViewsManager.AllowedViews = new string[5]
      {
        "SetupKeyWordsView",
        "SetupNumberingView",
        "SetupSkipLinesView",
        "SetupSortingView",
        "SetupAVSPropertiesView"
      };
    }
    this.pageViewsManager.UpdateViews(items, true);
  }

  private void AVSPropertiesForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.pageViewsManager.ActiveViewPage == null)
      return;
    this.pageViewsManager.ActiveViewPage.View.Deactivate((IView) null);
  }

  private void button1_Click(object sender, EventArgs e)
  {
    int num = (int) new KeywordReplacementDictForm(AVSDocument.ObjID_CommonSpecificationTemplate).ShowDialog();
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
    this.panelMain = new Panel();
    this.pageViewsManager = new PageViewsManager();
    this.panel2 = new Panel();
    this.buttonClose = new Button();
    this.button1 = new Button();
    this.panelMain.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panelMain.Controls.Add((Control) this.pageViewsManager);
    this.panelMain.Dock = DockStyle.Fill;
    this.panelMain.Location = new Point(0, 0);
    this.panelMain.Name = "panelMain";
    this.panelMain.Size = new Size(852, 615);
    this.panelMain.TabIndex = 0;
    this.pageViewsManager.ActiveViewPage = (IViewPage) null;
    this.pageViewsManager.CausesValidation = false;
    this.pageViewsManager.Dock = DockStyle.Fill;
    this.pageViewsManager.AutoScaleMode = AutoScaleMode.Inherit;
    this.pageViewsManager.Location = new Point(0, 0);
    this.pageViewsManager.Name = "pageViewsManager";
    this.pageViewsManager.Padding = new Padding(10, 0, 0, 0);
    this.pageViewsManager.Size = new Size(852, 615);
    this.pageViewsManager.TabIndex = 0;
    this.panel2.Controls.Add((Control) this.button1);
    this.panel2.Controls.Add((Control) this.buttonClose);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(0, 615);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(852, 38);
    this.panel2.TabIndex = 1;
    this.buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonClose.DialogResult = DialogResult.OK;
    this.buttonClose.Location = new Point(720, -1);
    this.buttonClose.Name = "buttonClose";
    this.buttonClose.Size = new Size(121, 27);
    this.buttonClose.TabIndex = 0;
    this.buttonClose.Text = "Закрыть";
    this.buttonClose.UseVisualStyleBackColor = true;
    this.buttonClose.Click += new EventHandler(this.buttonClose_Click);
    this.button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.button1.Location = new Point(12, 1);
    this.button1.Name = "button1";
    this.button1.Size = new Size(110, 23);
    this.button1.TabIndex = 1;
    this.button1.Text = "Словарь замен";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.buttonClose;
    this.ClientSize = new Size(852, 653);
    this.Controls.Add((Control) this.panelMain);
    this.Controls.Add((Control) this.panel2);
    this.MinimumSize = new Size(760, 615);
    this.Name = nameof (AVSPropertiesForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Настройки конструкторского документа";
    this.FormClosing += new FormClosingEventHandler(this.AVSPropertiesForm_FormClosing);
    this.panelMain.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
