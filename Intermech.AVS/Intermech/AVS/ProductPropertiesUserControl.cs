// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ProductPropertiesUserControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.LookAndFeel;
using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Bars;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Панель свойств выбранных объектов или документов в редакторе спецификаций </summary>
public class ProductPropertiesUserControl : UserControl
{
  private AVSWindow _avsWindow;
  private bool _servicesspecified;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label _labelSelectProduct;
  public PageViewsManager PageViewsManager;
  private ButtonEdit lbViewName;

  /// <summary> Конструктор по-умолчанию </summary>
  public ProductPropertiesUserControl(AVSWindow avsWindow)
  {
    this._avsWindow = avsWindow;
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1509);
  }

  public AVSWindow AVSWindow => this._avsWindow;

  public override string Text
  {
    get => this.lbViewName.Text;
    set => this.lbViewName.Text = value;
  }

  /// <summary> Попытка закрытия панели свойств </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lbViewName_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    this.PageViewsManager.SaveChanges();
    if (this.OnClose == null)
      return;
    this.OnClose((object) this, new EventArgs());
  }

  /// <summary> Событие вызываемое при попытке закрытия панели свойств </summary>
  public event EventHandler OnClose;

  /// <summary> Обновление содержимого </summary>
  /// <param name="items"></param>
  public void UpdateViews(ISelectedItems items)
  {
    if (items == null)
    {
      if (this._labelSelectProduct.Visible)
        return;
      this.PageViewsManager.Visible = false;
      this.PageViewsManager.Dock = DockStyle.Bottom;
      this._labelSelectProduct.Dock = DockStyle.Fill;
      this._labelSelectProduct.Visible = true;
      this._labelSelectProduct.BringToFront();
    }
    else
    {
      if (!this._servicesspecified)
      {
        ServiceContainer serviceContainer = new ServiceContainer();
        ViewStateService serviceInstance = new ViewStateService(ViewStateFlags.InParametersCard);
        serviceContainer.AddService(typeof (IViewState), (object) serviceInstance);
        serviceContainer.AddService(typeof (IAVSViewsService), (object) new AVSViewsService(this._avsWindow));
        serviceContainer.AddService(typeof (ICommandManager), (object) (ICommandManager) ServicesManager.GetService(typeof (ICommandManager)));
        serviceContainer.AddService(typeof (INotificationService), (object) (INotificationService) ServicesManager.GetService(typeof (INotificationService)));
        this.PageViewsManager.Services = (System.IServiceProvider) serviceContainer;
        this._servicesspecified = true;
      }
      NodeID nodeId = (NodeID) null;
      if (items.Count == 1)
        nodeId = items.GetItemID(0) as NodeID;
      if (nodeId != null && nodeId.ObjectID == this.AVSWindow.DocumentID && (this.AVSWindow.GetSelectedNoteRows().Count > 0 || this.AVSWindow.GetSelectedProducts().Count > 0))
      {
        ViewsInfo views = AVSPartViewsProvider.Instance.GetViews((ISelectedItems) null, this.PageViewsManager.Services);
        if (views.ViewNames != null && views.ViewNames.Length != 0)
          this.PageViewsManager.AllowedViews = views.ViewNames;
      }
      else
        this.PageViewsManager.AllowedViews = (string[]) null;
      if (!this.PageViewsManager.Visible)
      {
        this._labelSelectProduct.Visible = false;
        this._labelSelectProduct.Dock = DockStyle.Top;
        this.PageViewsManager.Dock = DockStyle.Fill;
        this.PageViewsManager.Visible = true;
        this.PageViewsManager.BringToFront();
      }
      this.PageViewsManager.UpdateViews(items, true);
    }
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
    this.PageViewsManager = new PageViewsManager();
    this._labelSelectProduct = new Label();
    this.lbViewName = new ButtonEdit();
    this.lbViewName.Properties.BeginInit();
    this.SuspendLayout();
    this.PageViewsManager.ActiveViewPage = (IViewPage) null;
    this.PageViewsManager.CausesValidation = false;
    this.PageViewsManager.Dock = DockStyle.Bottom;
    this.PageViewsManager.Font = new Font("Tahoma", 8.25f);
    this.PageViewsManager.Location = new Point(0, 169);
    this.PageViewsManager.Name = "PageViewsManager";
    this.PageViewsManager.Padding = new Padding(10, 0, 0, 0);
    this.PageViewsManager.Size = new Size(426, 212);
    this.PageViewsManager.TabIndex = 0;
    this.PageViewsManager.Visible = false;
    this._labelSelectProduct.Dock = DockStyle.Fill;
    this._labelSelectProduct.Font = new Font("Verdana", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this._labelSelectProduct.Location = new Point(0, 17);
    this._labelSelectProduct.Name = "_labelSelectProduct";
    this._labelSelectProduct.Size = new Size(426, 152);
    this._labelSelectProduct.TabIndex = 1;
    this._labelSelectProduct.Text = "Выберите изделие или документ";
    this._labelSelectProduct.TextAlign = ContentAlignment.MiddleCenter;
    this.lbViewName.Dock = DockStyle.Top;
    this.lbViewName.EditValue = (object) "";
    this.lbViewName.Location = new Point(0, 0);
    this.lbViewName.Name = "lbViewName";
    this.lbViewName.Properties.AllowFocused = false;
    this.lbViewName.Properties.AutoHeight = false;
    this.lbViewName.Properties.BorderStyle = BorderStyles.NoBorder;
    this.lbViewName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Delete, "", -1, true, true, false, HorzAlignment.Default, (Image) null, new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8f), "", StyleOptions.StyleEnabled | StyleOptions.UseWordWrap, false, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ActiveCaption, SystemColors.ActiveCaptionText))
    });
    this.lbViewName.Properties.ButtonsStyle = BorderStyles.UltraFlat;
    this.lbViewName.Properties.LookAndFeel.Style = LookAndFeelStyle.Office2003;
    this.lbViewName.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
    this.lbViewName.Properties.ReadOnly = true;
    this.lbViewName.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8f), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ActiveCaption, SystemColors.ActiveCaptionText);
    this.lbViewName.Properties.StyleBorder = new ViewStyle("ControlStyleBorder", (string) null, new Font("Microsoft Sans Serif", 8f), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, false, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ActiveCaption, SystemColors.ActiveCaptionText);
    this.lbViewName.Properties.StyleDisabled = new ViewStyle("ControlStyleDisabled", (string) null, new Font("Microsoft Sans Serif", 8f), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseForeColor, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ActiveCaption, SystemColors.ActiveCaptionText);
    this.lbViewName.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.lbViewName.Size = new Size(426, 17);
    this.lbViewName.TabIndex = 19;
    this.lbViewName.TabStop = false;
    this.lbViewName.ButtonClick += new ButtonPressedEventHandler(this.lbViewName_ButtonClick);
    this.Controls.Add((Control) this._labelSelectProduct);
    this.Controls.Add((Control) this.lbViewName);
    this.Controls.Add((Control) this.PageViewsManager);
    this.Name = nameof (ProductPropertiesUserControl);
    this.Size = new Size(426, 381);
    this.lbViewName.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
