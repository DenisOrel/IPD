
// Type: Intermech.Client.Core.FormDesigner.ValidationForDeleteAttributes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>Форма запроса удаления атрибутов.</summary>
public class ValidationForDeleteAttributes : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView _lv;
  private ColumnHeader _header;
  private Button _btnYes;
  private Button _btnNo;
  private Label _lb;

  /// <summary>Конструктор.</summary>
  /// <param name="attrIDs">Список идентификаторов выбранных атрибутов</param>
  public ValidationForDeleteAttributes(List<int> attrIDs)
  {
    this.InitializeComponent();
    if (this._lv.Columns.Count > 0)
      this._lv.Columns[0].Width = -2;
    this._lv.SmallImageList = Statics.IconSrv.ImageList;
    if (attrIDs == null)
      return;
    foreach (int attrId in attrIDs)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
      if (attributeType != null)
      {
        int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.FieldType);
        this._lv.Items.Add(new ListViewItem(attributeType.Name, imageIndex));
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lv_SizeChanged(object sender, EventArgs e)
  {
    if (this._lv == null || this._lv.Columns.Count == 0 || this._lv.Columns[0] == null)
      return;
    this._lv.Columns[0].Width = -2;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ValidationForDeleteAttributes));
    this._lv = new ListView();
    this._header = new ColumnHeader();
    this._btnYes = new Button();
    this._btnNo = new Button();
    this._lb = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._lv, "_lv");
    this._lv.Columns.AddRange(new ColumnHeader[1]
    {
      this._header
    });
    this._lv.FullRowSelect = true;
    this._lv.HeaderStyle = ColumnHeaderStyle.None;
    this._lv.HideSelection = false;
    this._lv.Name = "_lv";
    this._lv.Sorting = SortOrder.Ascending;
    this._lv.UseCompatibleStateImageBehavior = false;
    this._lv.View = View.Details;
    this._lv.SizeChanged += new EventHandler(this.On_lv_SizeChanged);
    componentResourceManager.ApplyResources((object) this._header, "_header");
    componentResourceManager.ApplyResources((object) this._btnYes, "_btnYes");
    this._btnYes.DialogResult = DialogResult.Yes;
    this._btnYes.Name = "_btnYes";
    this._btnYes.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnNo, "_btnNo");
    this._btnNo.DialogResult = DialogResult.No;
    this._btnNo.Name = "_btnNo";
    this._btnNo.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._lb, "_lb");
    this._lb.Name = "_lb";
    this.AcceptButton = (IButtonControl) this._btnYes;
    this.CancelButton = (IButtonControl) this._btnNo;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._lb);
    this.Controls.Add((Control) this._btnNo);
    this.Controls.Add((Control) this._btnYes);
    this.Controls.Add((Control) this._lv);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (ValidationForDeleteAttributes);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
