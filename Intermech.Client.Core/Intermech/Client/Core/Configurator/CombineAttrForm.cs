
// Type: Intermech.Client.Core.Configurator.CombineAttrForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Configurator;

/// <summary>
/// Форма для выбора атрибута, остающегося в БД и режима объединения атрибутов
/// </summary>
public class CombineAttrForm : Form
{
  /// <summary>Выделенные в списке конфигуратора атрибуты</summary>
  private List<DBAttributeID> _attrIDs;
  /// <summary>Атрибуты для удаления</summary>
  private List<DBAttributeID> _deleteAttr;
  /// <summary>Атрибут, который нужно оставить</summary>
  private DBAttributeID _remainAttr;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnOK;
  private Button _btnCancel;
  private Label _lblAttrChoice;
  private ListBox _lbAttributes;
  private GroupBox _gbCombineMode;
  private RadioButton _rbCancel;
  private RadioButton _rbLeaveData;
  private RadioButton _rbChangeData;
  private Panel _pnlButtons;

  /// <summary>Атрибуты для удаления</summary>
  public int[] DeleteAttrIDs
  {
    get
    {
      return this._deleteAttr.Select<DBAttributeID, int>((Func<DBAttributeID, int>) (attr => attr.AttribyteID)).ToArray<int>();
    }
  }

  /// <summary>Атрибут, который нужно оставить.</summary>
  public int RemainAttrID => this._remainAttr.AttribyteID;

  /// <summary>Режим объединения атрибутов.</summary>
  public CombineAttributeMode CombineAttributeMode
  {
    get
    {
      if (this._rbCancel.Checked)
        return CombineAttributeMode.CancelOperation;
      return this._rbChangeData.Checked ? CombineAttributeMode.ReplaceData : CombineAttributeMode.LeaveData;
    }
  }

  public CombineAttrForm(List<DBAttributeID> attrIDs)
  {
    this.InitializeComponent();
    this._attrIDs = new List<DBAttributeID>((IEnumerable<DBAttributeID>) attrIDs);
    foreach (object attrId in this._attrIDs)
      this._lbAttributes.Items.Add(attrId);
    this._lbAttributes.SelectedItem = this._lbAttributes.Items[0];
    this._rbCancel.Checked = true;
  }

  /// <summary>Применить</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnOK_Click(object sender, EventArgs e)
  {
    this._remainAttr = this._lbAttributes.SelectedItems[0] as DBAttributeID;
    this._deleteAttr = new List<DBAttributeID>((IEnumerable<DBAttributeID>) this._attrIDs);
    this._deleteAttr.Remove(this._remainAttr);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  /// <summary>Отмена</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void _btnCancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
    this.Close();
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
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this._lblAttrChoice = new Label();
    this._lbAttributes = new ListBox();
    this._gbCombineMode = new GroupBox();
    this._rbLeaveData = new RadioButton();
    this._rbChangeData = new RadioButton();
    this._rbCancel = new RadioButton();
    this._pnlButtons = new Panel();
    this._gbCombineMode.SuspendLayout();
    this._pnlButtons.SuspendLayout();
    this.SuspendLayout();
    this._btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._btnOK.Location = new Point(124, 20);
    this._btnOK.Name = "_btnOK";
    this._btnOK.Size = new Size(121, 27);
    this._btnOK.TabIndex = 0;
    this._btnOK.Text = "Применить";
    this._btnOK.UseVisualStyleBackColor = true;
    this._btnOK.Click += new EventHandler(this.btnOK_Click);
    this._btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Location = new Point(251, 20);
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Size = new Size(121, 27);
    this._btnCancel.TabIndex = 1;
    this._btnCancel.Text = "Отмена";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this._btnCancel_Click);
    this._lblAttrChoice.AutoSize = true;
    this._lblAttrChoice.Location = new Point(12, 5);
    this._lblAttrChoice.Name = "_lblAttrChoice";
    this._lblAttrChoice.Size = new Size(316, 13);
    this._lblAttrChoice.TabIndex = 2;
    this._lblAttrChoice.Text = "Выберите атрибут, который следует оставить в базе данных";
    this._lbAttributes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._lbAttributes.FormattingEnabled = true;
    this._lbAttributes.Location = new Point(12, 22);
    this._lbAttributes.Name = "_lbAttributes";
    this._lbAttributes.Size = new Size(372, 95);
    this._lbAttributes.TabIndex = 3;
    this._gbCombineMode.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._gbCombineMode.Controls.Add((Control) this._rbLeaveData);
    this._gbCombineMode.Controls.Add((Control) this._rbChangeData);
    this._gbCombineMode.Controls.Add((Control) this._rbCancel);
    this._gbCombineMode.Location = new Point(13, 133);
    this._gbCombineMode.Name = "_gbCombineMode";
    this._gbCombineMode.Size = new Size(371, 105);
    this._gbCombineMode.TabIndex = 4;
    this._gbCombineMode.TabStop = false;
    this._gbCombineMode.Text = "При наличии у объекта или связи обоих  объединяемых атрибутов:";
    this._rbLeaveData.AutoSize = true;
    this._rbLeaveData.Location = new Point(6, 71);
    this._rbLeaveData.Name = "_rbLeaveData";
    this._rbLeaveData.Size = new Size(226, 17);
    this._rbLeaveData.TabIndex = 2;
    this._rbLeaveData.TabStop = true;
    this._rbLeaveData.Text = "Оставить данные выбранного атрибута";
    this._rbLeaveData.UseVisualStyleBackColor = true;
    this._rbChangeData.AutoSize = true;
    this._rbChangeData.Location = new Point(6, 48 /*0x30*/);
    this._rbChangeData.Name = "_rbChangeData";
    this._rbChangeData.Size = new Size(250, 17);
    this._rbChangeData.TabIndex = 1;
    this._rbChangeData.TabStop = true;
    this._rbChangeData.Text = "Заменять данными из удаляемого атрибута";
    this._rbChangeData.UseVisualStyleBackColor = true;
    this._rbCancel.AutoSize = true;
    this._rbCancel.Location = new Point(6, 24);
    this._rbCancel.Name = "_rbCancel";
    this._rbCancel.Size = new Size(128 /*0x80*/, 17);
    this._rbCancel.TabIndex = 0;
    this._rbCancel.TabStop = true;
    this._rbCancel.Text = "Отменить операцию";
    this._rbCancel.UseVisualStyleBackColor = true;
    this._pnlButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._pnlButtons.Controls.Add((Control) this._btnOK);
    this._pnlButtons.Controls.Add((Control) this._btnCancel);
    this._pnlButtons.Location = new Point(12, 265);
    this._pnlButtons.Name = "_pnlButtons";
    this._pnlButtons.Size = new Size(381, 50);
    this._pnlButtons.TabIndex = 5;
    this.AcceptButton = (IButtonControl) this._btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.ClientSize = new Size(396, 327);
    this.Controls.Add((Control) this._pnlButtons);
    this.Controls.Add((Control) this._gbCombineMode);
    this.Controls.Add((Control) this._lbAttributes);
    this.Controls.Add((Control) this._lblAttrChoice);
    this.MinimumSize = new Size(412, 365);
    this.Name = nameof (CombineAttrForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Объединение атрибутов";
    this._gbCombineMode.ResumeLayout(false);
    this._gbCombineMode.PerformLayout();
    this._pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
