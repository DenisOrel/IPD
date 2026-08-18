
// Type: Intermech.Search.UI.ObjectsFoundExceptionDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public sealed class ObjectsFoundExceptionDialog : Form
{
  private ObjectsFoundException _exception;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _closeButton;
  private TextBox _textBox;
  private MultipleObjectsView _multipleObjectsView;

  public ObjectsFoundExceptionDialog() => this.InitializeComponent();

  public ObjectsFoundException Exception
  {
    get => this._exception;
    set
    {
      if (this._exception == value)
        return;
      this._exception = value;
      if (this._exception != null)
        this._textBox.Text = this._exception.Message;
      ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryCustomNode, -1, !string.IsNullOrEmpty(this._exception.ObjectsListCaption) ? this._exception.ObjectsListCaption : "Объекты", (IList) this._exception.ObjectsID);
      ObjectsSelectionOptionsHolder serviceInstance = new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.TrashMode | ObjectsSelectionOptions.LocalTypesMode);
      ServiceContainer services = new ServiceContainer((System.IServiceProvider) ServicesManager.ServiceContainer);
      services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) serviceInstance);
      this._multipleObjectsView.Initialize((IDescriptor) rootDescriptor, (System.IServiceProvider) services);
      this._multipleObjectsView.PageViewsManager.Services = (System.IServiceProvider) ServicesManager.ServiceContainer;
      this._multipleObjectsView.Activate((IView) null);
    }
  }

  private void RemovingAllowableAttributeValueExceptionDialog_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void RemovingAllowableAttributeValueExceptionDialog_FormClosed(
    object sender,
    FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void CloseButton_Click(object sender, EventArgs e) => this.Close();

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
    this._closeButton = new Button();
    this._textBox = new TextBox();
    this._multipleObjectsView = new MultipleObjectsView();
    this.SuspendLayout();
    this._closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._closeButton.Location = new Point(475, 354);
    this._closeButton.Name = "_closeButton";
    this._closeButton.Size = new Size(75, 23);
    this._closeButton.TabIndex = 0;
    this._closeButton.Text = "Закрыть";
    this._closeButton.UseVisualStyleBackColor = true;
    this._closeButton.Click += new EventHandler(this.CloseButton_Click);
    this._textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._textBox.Location = new Point(13, 13);
    this._textBox.Multiline = true;
    this._textBox.Name = "_textBox";
    this._textBox.ReadOnly = true;
    this._textBox.Size = new Size(537, 74);
    this._textBox.TabIndex = 1;
    this._multipleObjectsView.AllowCustomGroupValues = true;
    this._multipleObjectsView.AllowEditing = true;
    this._multipleObjectsView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._multipleObjectsView.Control = (object) this._multipleObjectsView;
    this._multipleObjectsView.DisableKeyDownEvents = false;
    this._multipleObjectsView.EditingMode = false;
    this._multipleObjectsView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._multipleObjectsView.Font = new Font("Tahoma", 8.25f);
    this._multipleObjectsView.Location = new Point(13, 93);
    this._multipleObjectsView.Name = "_objectsChildrenView";
    this._multipleObjectsView.Size = new Size(537, (int) byte.MaxValue);
    this._multipleObjectsView.TabIndex = 2;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(562, 389);
    this.Controls.Add((Control) this._multipleObjectsView);
    this.Controls.Add((Control) this._textBox);
    this.Controls.Add((Control) this._closeButton);
    this.Name = nameof (ObjectsFoundExceptionDialog);
    this.Text = "Внимание";
    this.FormClosed += new FormClosedEventHandler(this.RemovingAllowableAttributeValueExceptionDialog_FormClosed);
    this.Load += new EventHandler(this.RemovingAllowableAttributeValueExceptionDialog_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
