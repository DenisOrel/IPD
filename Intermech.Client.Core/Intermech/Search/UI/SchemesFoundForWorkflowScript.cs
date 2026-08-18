
// Type: Intermech.Search.UI.SchemesFoundForWorkflowScript
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public class SchemesFoundForWorkflowScript : Form
{
  private long[] _shemesID;
  private string _messageText = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private MultipleObjectsView _multipleObjectsView;
  private TextBox _textBox;
  private Button _closeButton;
  private Button saveBtn;

  public SchemesFoundForWorkflowScript(List<long> shemesID, string messageText)
  {
    this.InitializeComponent();
    this._shemesID = shemesID.ToArray();
    this._messageText = messageText;
  }

  private void SchemesFoundForWorkflowScript_Load(object sender, EventArgs e)
  {
    this._textBox.Text = this._messageText;
    ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryCustomNode, -1, "Шаблоны процессов", (IList) this._shemesID);
    ObjectsSelectionOptionsHolder serviceInstance = new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.TrashMode | ObjectsSelectionOptions.LocalTypesMode);
    ServiceContainer services = new ServiceContainer((System.IServiceProvider) ServicesManager.ServiceContainer);
    services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) serviceInstance);
    this._multipleObjectsView.Initialize((IDescriptor) rootDescriptor, (System.IServiceProvider) services);
    this._multipleObjectsView.PageViewsManager.Services = (System.IServiceProvider) ServicesManager.ServiceContainer;
    this._multipleObjectsView.Activate((IView) null);
  }

  private void saveBtn_Click(object sender, EventArgs e) => this.Close();

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
    this._multipleObjectsView = new MultipleObjectsView();
    this._textBox = new TextBox();
    this._closeButton = new Button();
    this.saveBtn = new Button();
    this.SuspendLayout();
    this._multipleObjectsView.AllowCustomGroupValues = true;
    this._multipleObjectsView.AllowEditing = true;
    this._multipleObjectsView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._multipleObjectsView.Control = (object) this._multipleObjectsView;
    this._multipleObjectsView.DisableKeyDownEvents = false;
    this._multipleObjectsView.EditingMode = false;
    this._multipleObjectsView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._multipleObjectsView.Font = new Font("Tahoma", 8.25f);
    this._multipleObjectsView.Location = new Point(13, 92);
    this._multipleObjectsView.Name = "_multipleObjectsView";
    this._multipleObjectsView.Size = new Size(537, (int) byte.MaxValue);
    this._multipleObjectsView.TabIndex = 5;
    this._multipleObjectsView.ViewContentType = ContentType.NonFolders;
    this._textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._textBox.Location = new Point(13, 12);
    this._textBox.Multiline = true;
    this._textBox.Name = "_textBox";
    this._textBox.ReadOnly = true;
    this._textBox.Size = new Size(537, 74);
    this._textBox.TabIndex = 4;
    this._closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._closeButton.DialogResult = DialogResult.Cancel;
    this._closeButton.Location = new Point(412, 353);
    this._closeButton.Name = "_closeButton";
    this._closeButton.Size = new Size(138, 23);
    this._closeButton.TabIndex = 3;
    this._closeButton.Text = "Отменить сохранение";
    this._closeButton.UseVisualStyleBackColor = true;
    this._closeButton.Click += new EventHandler(this.saveBtn_Click);
    this.saveBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.saveBtn.DialogResult = DialogResult.OK;
    this.saveBtn.Location = new Point(331, 353);
    this.saveBtn.Name = "saveBtn";
    this.saveBtn.Size = new Size(75, 23);
    this.saveBtn.TabIndex = 6;
    this.saveBtn.Text = "Сохранить";
    this.saveBtn.UseVisualStyleBackColor = true;
    this.saveBtn.Click += new EventHandler(this.saveBtn_Click);
    this.AcceptButton = (IButtonControl) this.saveBtn;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._closeButton;
    this.ClientSize = new Size(562, 389);
    this.Controls.Add((Control) this.saveBtn);
    this.Controls.Add((Control) this._multipleObjectsView);
    this.Controls.Add((Control) this._textBox);
    this.Controls.Add((Control) this._closeButton);
    this.MinimumSize = new Size(578, 428);
    this.Name = nameof (SchemesFoundForWorkflowScript);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Внимание";
    this.Load += new EventHandler(this.SchemesFoundForWorkflowScript_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
