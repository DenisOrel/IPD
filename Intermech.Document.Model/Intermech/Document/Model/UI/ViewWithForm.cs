// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.ViewWithForm
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Extensions;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>Форма настроек просмотра</summary>
public class ViewWithForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btOK;
  private CheckBox cbInjectSignNamesOnly;
  private CheckBox cbInjectAttributes;
  private CheckBox cbInjectFileChecksum;
  private CheckBox cbInjectSigns;

  public ViewWithForm() => this.InitializeComponent();

  /// <summary>Вызов формы для настроек</summary>
  /// <param name="mode">Оригинальные настройки</param>
  /// <returns>Настройки после вызова диалога</returns>
  public static DocumentViewMode Execute(DocumentViewMode mode)
  {
    ViewWithForm viewWithForm = new ViewWithForm();
    viewWithForm.cbInjectAttributes.Checked = mode.HasFlag((Enum) DocumentViewMode.ShowDocumentReferences);
    viewWithForm.cbInjectSigns.Checked = mode.HasFlag((Enum) DocumentViewMode.ShowSigns);
    viewWithForm.cbInjectFileChecksum.Checked = mode.HasFlag((Enum) DocumentViewMode.ShowCRC);
    viewWithForm.cbInjectSignNamesOnly.Checked = mode.HasFlag((Enum) DocumentViewMode.ShowOnlySignName);
    int num = (int) viewWithForm.ShowDialog();
    DocumentViewMode documentViewMode = DocumentViewMode.Empty;
    if (viewWithForm.cbInjectAttributes.Checked)
      documentViewMode = documentViewMode.AddFlags<DocumentViewMode>(DocumentViewMode.ShowDocumentReferences);
    if (viewWithForm.cbInjectSigns.Checked)
      documentViewMode = documentViewMode.AddFlags<DocumentViewMode>(DocumentViewMode.ShowSigns);
    if (viewWithForm.cbInjectSignNamesOnly.Checked)
      documentViewMode = documentViewMode.AddFlags<DocumentViewMode>(DocumentViewMode.ShowOnlySignName);
    if (viewWithForm.cbInjectFileChecksum.Checked)
      documentViewMode = documentViewMode.AddFlags<DocumentViewMode>(DocumentViewMode.ShowCRC);
    return documentViewMode;
  }

  private void checkBoxShowDocumentReferences_CheckedChanged(object sender, EventArgs e)
  {
  }

  private void checkBoxShowSigns_CheckedChanged(object sender, EventArgs e)
  {
    this.cbInjectSignNamesOnly.Enabled = this.cbInjectSigns.Checked;
  }

  private void checkboxShowOnlySignName_CheckedChanged(object sender, EventArgs e)
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
    this.btOK = new Button();
    this.cbInjectSignNamesOnly = new CheckBox();
    this.cbInjectAttributes = new CheckBox();
    this.cbInjectFileChecksum = new CheckBox();
    this.cbInjectSigns = new CheckBox();
    this.SuspendLayout();
    this.btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(347, 145);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(75, 25);
    this.btOK.TabIndex = 5;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.cbInjectSignNamesOnly.AutoSize = true;
    this.cbInjectSignNamesOnly.Enabled = false;
    this.cbInjectSignNamesOnly.Location = new Point(32 /*0x20*/, 49);
    this.cbInjectSignNamesOnly.Margin = new Padding(3, 3, 3, 8);
    this.cbInjectSignNamesOnly.Name = "cbInjectSignNamesOnly";
    this.cbInjectSignNamesOnly.Size = new Size(253, 17);
    this.cbInjectSignNamesOnly.TabIndex = 7;
    this.cbInjectSignNamesOnly.Text = "Записывать только фамилию подписавшего";
    this.cbInjectSignNamesOnly.UseVisualStyleBackColor = true;
    this.cbInjectAttributes.AutoSize = true;
    this.cbInjectAttributes.Location = new Point(12, 105);
    this.cbInjectAttributes.Margin = new Padding(3, 3, 3, 8);
    this.cbInjectAttributes.Name = "cbInjectAttributes";
    this.cbInjectAttributes.Size = new Size(258, 17);
    this.cbInjectAttributes.TabIndex = 9;
    this.cbInjectAttributes.Text = "Разрешить запись атрибутов объекта в файл";
    this.cbInjectAttributes.UseVisualStyleBackColor = true;
    this.cbInjectFileChecksum.AutoSize = true;
    this.cbInjectFileChecksum.Location = new Point(12, 77);
    this.cbInjectFileChecksum.Margin = new Padding(3, 3, 3, 8);
    this.cbInjectFileChecksum.Name = "cbInjectFileChecksum";
    this.cbInjectFileChecksum.Size = new Size(265, 17);
    this.cbInjectFileChecksum.TabIndex = 8;
    this.cbInjectFileChecksum.Text = "Разрешить запись контрольной суммы в файл";
    this.cbInjectFileChecksum.UseVisualStyleBackColor = true;
    this.cbInjectSigns.AutoSize = true;
    this.cbInjectSigns.Location = new Point(12, 21);
    this.cbInjectSigns.Margin = new Padding(3, 3, 3, 8);
    this.cbInjectSigns.Name = "cbInjectSigns";
    this.cbInjectSigns.Size = new Size(249, 17);
    this.cbInjectSigns.TabIndex = 6;
    this.cbInjectSigns.Text = "Разрешить запись подписи объекта в файл";
    this.cbInjectSigns.UseVisualStyleBackColor = true;
    this.cbInjectSigns.CheckedChanged += new EventHandler(this.checkBoxShowSigns_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(434, 182);
    this.Controls.Add((Control) this.cbInjectSignNamesOnly);
    this.Controls.Add((Control) this.cbInjectAttributes);
    this.Controls.Add((Control) this.cbInjectFileChecksum);
    this.Controls.Add((Control) this.cbInjectSigns);
    this.Controls.Add((Control) this.btOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (ViewWithForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Опции просмотра";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
