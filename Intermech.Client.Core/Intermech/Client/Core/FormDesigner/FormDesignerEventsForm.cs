
// Type: Intermech.Client.Core.FormDesigner.FormDesignerEventsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>
/// 
/// </summary>
public class FormDesignerEventsForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnOK;
  private Button _btnCancel;
  private CheckedListBox _clbEvents;

  /// <summary>Отмеченные элементы.</summary>
  public FormDesignerAction[] CheckedEvents { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="handlerType"></param>
  /// <param name="events"></param>
  public FormDesignerEventsForm(System.Type handlerType, FormDesignerAction[] events)
  {
    this.InitializeComponent();
    this.LoadItems(handlerType, events);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_clbEvents_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    this._btnOK.Enabled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
    this.CheckedEvents = this._clbEvents.CheckedItems.Count > 0 ? this._clbEvents.CheckedItems.Cast<FormDesignerAction>().ToArray<FormDesignerAction>() : (FormDesignerAction[]) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e) => FormStorage.LoadLayout((Control) this);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="handlerType"></param>
  /// <param name="checkedEvs"></param>
  private void LoadItems(System.Type handlerType, FormDesignerAction[] checkedEvs)
  {
    if (!(ServicesManager.GetService(typeof (IFormDesignerEventsManager)) is IFormDesignerEventsManager service))
      return;
    Dictionary<Guid, FormDesignerAction> events = service.GetEvents(handlerType);
    if (events == null)
      return;
    List<FormDesignerAction> formDesignerActionList = checkedEvs != null ? ((IEnumerable<FormDesignerAction>) checkedEvs).ToList<FormDesignerAction>() : new List<FormDesignerAction>(0);
    foreach (KeyValuePair<Guid, FormDesignerAction> keyValuePair in events)
      this._clbEvents.Items.Add((object) keyValuePair.Value, formDesignerActionList.Contains(keyValuePair.Value));
    this._btnOK.Enabled = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDesignerEventsForm));
    this._pnlBottom = new Panel();
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this._clbEvents = new CheckedListBox();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._clbEvents.CheckOnClick = true;
    componentResourceManager.ApplyResources((object) this._clbEvents, "_clbEvents");
    this._clbEvents.FormattingEnabled = true;
    this._clbEvents.Name = "_clbEvents";
    this._clbEvents.ItemCheck += new ItemCheckEventHandler(this.On_clbEvents_ItemCheck);
    this.AcceptButton = (IButtonControl) this._btnOK;
    this.CancelButton = (IButtonControl) this._btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._clbEvents);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (FormDesignerEventsForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
