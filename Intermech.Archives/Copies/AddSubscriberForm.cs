// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.AddSubscriberForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Copies.Subscribers;
using Intermech.Client.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Форма для добавления абонентов</summary>
public class AddSubscriberForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private Button btnCancel;
  private AddSubscriberControl addSubcribers;

  private AddSubscriberForm() => this.InitializeComponent();

  /// <summary>Конструктор</summary>
  /// <param name="deliveryList">список листов рассылки, в которые будут добавлены абоненты</param>
  /// <param name="isCallFromEco">вызвана ли форма по команде Лист рассылки из извещения</param>
  public AddSubscriberForm(List<long> deliveryList, bool isCallFromEco)
    : this()
  {
    this.addSubcribers.LoadSubscribers(deliveryList, OwnerType.Form, isCallFromEco);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void AddSubscriberForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this.addSubcribers.LoadLayout();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void AddSubscriberForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
    this.addSubcribers.SaveLayout();
  }

  /// <summary>Сохранить сделанные изменения</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    this.addSubcribers.Save();
    this.DialogResult = DialogResult.OK;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddSubscriberForm));
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.addSubcribers = new AddSubscriberControl();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.addSubcribers, "addSubcribers");
    this.addSubcribers.ID = 0L;
    this.addSubcribers.IsChanged = false;
    this.addSubcribers.Name = "addSubcribers";
    this.addSubcribers.ObjectID = 0L;
    this.addSubcribers.ReadOnly = false;
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.addSubcribers);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AddSubscriberForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.AddSubscriberForm_FormClosing);
    this.Load += new EventHandler(this.AddSubscriberForm_Load);
    this.ResumeLayout(false);
  }
}
