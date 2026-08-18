
// Type: Intermech.Search.ButtonBars.ButtonBarsEditorView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.ButtonBars;

[ViewDescriptionProvider(typeof (ButtonBarsEditorView.ButtonBarsEditorViewDescriptionProvider))]
public sealed class ButtonBarsEditorView : UserControl, IView
{
  private LazyService<IButtonBarClientService> _buttonBarClientService = new LazyService<IButtonBarClientService>();
  private IDBTypedObjectID _typedObjectID;
  private bool _readOnly;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ButtonBarsEditorControl _buttonBarsEditorControl;
  private Panel panel1;
  private Button _cancelButton;
  private Button _applyButton;
  private Panel panel2;
  private MessageControl _messageControl;

  public static bool CheckParams(
    ISelectedItems selectedItems,
    System.IServiceProvider serviceProvider,
    out IDBTypedObjectID typedObjectID)
  {
    if (!SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(selectedItems, out typedObjectID))
      return false;
    return typedObjectID.ObjectType == Constants.UserConfigurationObjectTypeID || typedObjectID.ObjectType == Constants.RoleConfigurationObjectTypeID || typedObjectID.ObjectType == Constants.RoleObjectTypeID;
  }

  public ButtonBarsEditorView() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    private set
    {
      if (this._readOnly == value)
        return;
      this._readOnly = value;
      this.UpdateView();
    }
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (provider == null)
      throw new ArgumentNullException(nameof (provider));
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (!ButtonBarsEditorView.CheckParams(items, provider, out typedObjectID))
      throw new ArgumentException();
    this._typedObjectID = typedObjectID;
  }

  public void Activate(IView previousView)
  {
    if (this._typedObjectID.ObjectType == Constants.UserConfigurationObjectTypeID)
      this._buttonBarsEditorControl.ButtonBars = this._buttonBarClientService.Value.FindButtonBarsForCurrentUser();
    else if (this._typedObjectID.ObjectType == Constants.RoleConfigurationObjectTypeID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IButtonBarServerService customService = sessionKeeper.Session.GetCustomService(typeof (IButtonBarServerService)) as IButtonBarServerService;
        this._buttonBarsEditorControl.ButtonBars = customService.GetButtonBarsFromRoleConfiguration(sessionKeeper.Session.SessionGUID, this._typedObjectID.ObjectID);
        this.ReadOnly = !customService.CheckButtonBarsEditRightsForRoleConfiguration(sessionKeeper.Session.SessionGUID, this._typedObjectID.ObjectID);
      }
    }
    else if (this._typedObjectID.ObjectType == Constants.RoleObjectTypeID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IButtonBarServerService customService = sessionKeeper.Session.GetCustomService(typeof (IButtonBarServerService)) as IButtonBarServerService;
        this._buttonBarsEditorControl.ButtonBars = customService.FindButtonBarsForRole(sessionKeeper.Session.SessionGUID, this._typedObjectID.ObjectID);
        this.ReadOnly = !customService.CheckButtonBarsEditRightsForRole(sessionKeeper.Session.SessionGUID, this._typedObjectID.ObjectID);
      }
    }
    else
      this._buttonBarsEditorControl.ButtonBars = (ButtonBar[]) null;
    this.UpdateView();
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => "Кнопочные панели";

  public int ImageIndex => -1;

  public int OrderID => 100;

  private void ButtonBarsEditorControl_Changed(object sender, EventArgs e) => this.UpdateView();

  private void ApplyButton_Click(object sender, EventArgs e)
  {
    if (this._typedObjectID.ObjectType == Constants.UserConfigurationObjectTypeID)
      this._buttonBarClientService.Value.SaveButtonBarsForCurrentUser(this._buttonBarsEditorControl.ButtonBars);
    else if (this._typedObjectID.ObjectType == Constants.RoleConfigurationObjectTypeID)
      this._buttonBarClientService.Value.SaveButtonBarsToRoleConfiguration(this._typedObjectID.ObjectID, this._buttonBarsEditorControl.ButtonBars);
    else if (this._typedObjectID.ObjectType == Constants.RoleObjectTypeID)
      this._buttonBarClientService.Value.SaveButtonBarsForRole(this._typedObjectID.ObjectID, this._buttonBarsEditorControl.ButtonBars);
    this._buttonBarsEditorControl.ApplyChanges();
  }

  private void CancelButton_Click(object sender, EventArgs e)
  {
    this._buttonBarsEditorControl.CancelChanges();
  }

  private void UpdateView()
  {
    this._applyButton.Enabled = this.CanApply();
    this._cancelButton.Enabled = this.CanCancel();
    this._messageControl.Visible = this.ReadOnly;
  }

  private bool CanApply() => !this.ReadOnly && this._buttonBarsEditorControl.HasChanges;

  private bool CanCancel() => !this.ReadOnly && this._buttonBarsEditorControl.HasChanges;

  private void panel2_Paint(object sender, PaintEventArgs e)
  {
  }

  private void panel1_Paint(object sender, PaintEventArgs e)
  {
  }

  private void ButtonBarsEditorView_Load(object sender, EventArgs e)
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
    this.panel1 = new Panel();
    this._cancelButton = new Button();
    this._applyButton = new Button();
    this.panel2 = new Panel();
    this._buttonBarsEditorControl = new ButtonBarsEditorControl();
    this._messageControl = new MessageControl();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this._cancelButton);
    this.panel1.Controls.Add((Control) this._applyButton);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 322);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(837, 43);
    this.panel1.TabIndex = 1;
    this._cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._cancelButton.Location = new Point(754, 6);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 0;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    this._applyButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._applyButton.Location = new Point(639, 6);
    this._applyButton.Name = "_applyButton";
    this._applyButton.Size = new Size(109, 23);
    this._applyButton.TabIndex = 0;
    this._applyButton.Text = "Применить";
    this._applyButton.UseVisualStyleBackColor = true;
    this._applyButton.Click += new EventHandler(this.ApplyButton_Click);
    this.panel2.Controls.Add((Control) this._buttonBarsEditorControl);
    this.panel2.Dock = DockStyle.Fill;
    this.panel2.Location = new Point(0, 58);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(837, 264);
    this.panel2.TabIndex = 2;
    this._buttonBarsEditorControl.ButtonBars = new ButtonBar[0];
    this._buttonBarsEditorControl.Dock = DockStyle.Fill;
    this._buttonBarsEditorControl.Location = new Point(0, 0);
    this._buttonBarsEditorControl.Name = "_buttonBarsEditorControl";
    this._buttonBarsEditorControl.ReadOnly = false;
    this._buttonBarsEditorControl.Size = new Size(837, 264);
    this._buttonBarsEditorControl.TabIndex = 0;
    this._buttonBarsEditorControl.Changed += new EventHandler(this.ButtonBarsEditorControl_Changed);
    this._messageControl.BackColor = Color.LightYellow;
    this._messageControl.BorderStyle = BorderStyle.FixedSingle;
    this._messageControl.Dock = DockStyle.Top;
    this._messageControl.Location = new Point(0, 0);
    this._messageControl.Name = "_messageControl";
    this._messageControl.Size = new Size(837, 58);
    this._messageControl.TabIndex = 1;
    this._messageControl.Text = "Редактироваине запрещено текущими настройками безопасти или объект находится на шаге жизненного цикла запрещающем его модификацию.";
    this._messageControl.Type = _MessageType.Warning;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this._messageControl);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (ButtonBarsEditorView);
    this.Size = new Size(837, 365);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class ButtonBarsEditorViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = "Кнопочные панели",
        ImageIndex = -1,
        OrderID = 100
      };
    }
  }
}
