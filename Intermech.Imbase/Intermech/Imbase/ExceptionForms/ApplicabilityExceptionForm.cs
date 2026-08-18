// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ExceptionForms.ApplicabilityExceptionForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Server;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.ExceptionForms;

public class ApplicabilityExceptionForm : Form
{
  private ServiceContainer _serviceContainer;
  private AppicabilityExceptionTempCommandProvider _tempCommandProvider;
  private MenuTemplateNode _locateInParentNode;
  private ImbaseApplicablityException _exception;
  private IContainer components;
  private TextBox _textBox;
  private Panel bottomPnl;
  private Button btOk;
  private ObjectsViewBase _objectsViewBase;

  public ApplicabilityExceptionForm() => this.InitializeComponent();

  public ApplicabilityExceptionForm(ImbaseApplicablityException exception)
    : this()
  {
    this._exception = exception;
    this._serviceContainer = new ServiceContainer((System.IServiceProvider) ApplicationServices.Container);
    this._locateInParentNode = new MenuTemplateNode("LocateInParent", "Показать в составе", -1, 0, 0);
  }

  public void InitializeData()
  {
    this._textBox.Text = $"Не возможно завершить редактирование объекта {this._exception.ParentObjectName}. В составе имеются объекты, у которых: {string.Join(",", ((IEnumerable<ApplicabilityStatusEnum>) this._exception.Applicabilities).Select<ApplicabilityStatusEnum, string>((Func<ApplicabilityStatusEnum, string>) (x => EnumTypeHelper.GetCaption((Enum) x))).ToArray<string>())}";
    if (!(this._serviceContainer.GetService(typeof (IFactory)) is IFactory service))
      return;
    this._tempCommandProvider = new AppicabilityExceptionTempCommandProvider();
    service.AddCommandsProvider(1, (ICommandsProvider) this._tempCommandProvider);
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(this._locateInParentNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private void ViewLoad(object sender, EventArgs e)
  {
    ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryCustomNode, -1, "Объекты в составе", (IList) ((IEnumerable<Tuple<long, int>>) this._exception.ChildObjectInfo).Select<Tuple<long, int>, long>((Func<Tuple<long, int>, long>) (x => x.Item1)).ToArray<long>());
    this._serviceContainer.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.LocalTypesMode));
    this._serviceContainer.AddService(typeof (ImbaseApplicablityException), (object) this._exception);
    this._objectsViewBase.Initialize((IDescriptor) rootDescriptor, (System.IServiceProvider) this._serviceContainer);
    this._objectsViewBase.PageViewsManager.Services = (System.IServiceProvider) ApplicationServices.Container;
    this._objectsViewBase.Activate((IView) null);
  }

  private void btOk_Click(object sender, EventArgs e) => this.Close();

  private void ApplicabilityExceptionForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this._tempCommandProvider == null || this._serviceContainer == null || !(this._serviceContainer.GetService(typeof (IFactory)) is IFactory service))
      return;
    service.RemoveCommandsProvider(1, (ICommandsProvider) this._tempCommandProvider);
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Remove(this._locateInParentNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this._textBox = new TextBox();
    this.bottomPnl = new Panel();
    this.btOk = new Button();
    this._objectsViewBase = new ObjectsViewBase();
    this.bottomPnl.SuspendLayout();
    this.SuspendLayout();
    this._textBox.Dock = DockStyle.Top;
    this._textBox.Location = new Point(3, 3);
    this._textBox.Multiline = true;
    this._textBox.Name = "_textBox";
    this._textBox.ReadOnly = true;
    this._textBox.Size = new Size(951, 55);
    this._textBox.TabIndex = 4;
    this.bottomPnl.Controls.Add((Control) this.btOk);
    this.bottomPnl.Dock = DockStyle.Bottom;
    this.bottomPnl.Location = new Point(3, 400);
    this.bottomPnl.Name = "bottomPnl";
    this.bottomPnl.Size = new Size(951, 47);
    this.bottomPnl.TabIndex = 5;
    this.btOk.AccessibleDescription = "";
    this.btOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOk.Location = new Point(864, 12);
    this.btOk.Name = "btOk";
    this.btOk.Size = new Size(75, 23);
    this.btOk.TabIndex = 0;
    this.btOk.Text = "Закрыть";
    this.btOk.UseVisualStyleBackColor = true;
    this.btOk.Click += new EventHandler(this.btOk_Click);
    this._objectsViewBase.AllowCustomGroupValues = true;
    this._objectsViewBase.Control = (object) this._objectsViewBase;
    this._objectsViewBase.DisableKeyDownEvents = false;
    this._objectsViewBase.Dock = DockStyle.Fill;
    this._objectsViewBase.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._objectsViewBase.Font = new Font("Tahoma", 8.25f);
    this._objectsViewBase.Location = new Point(3, 58);
    this._objectsViewBase.Name = "_objectsViewBase";
    this._objectsViewBase.Size = new Size(951, 342);
    this._objectsViewBase.TabIndex = 0;
    this._objectsViewBase.ViewContentType = ContentType.NonFolders;
    this._objectsViewBase.Load += new EventHandler(this.ViewLoad);
    this.AcceptButton = (IButtonControl) this.btOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(957, 450);
    this.Controls.Add((Control) this._objectsViewBase);
    this.Controls.Add((Control) this.bottomPnl);
    this.Controls.Add((Control) this._textBox);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (ApplicabilityExceptionForm);
    this.Padding = new Padding(3);
    this.Text = "Ошибка";
    this.FormClosing += new FormClosingEventHandler(this.ApplicabilityExceptionForm_FormClosing);
    this.bottomPnl.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
