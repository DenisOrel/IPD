// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.EventChosingForm
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class EventChosingForm : Form
{
  private NotificationEventType eventType;
  private IContainer components;
  private Button btnOk;
  private Button btnCancel;
  private Label label1;
  private ListView lvEventTypes;

  public NotificationEventType EventType => this.eventType;

  public EventChosingForm() => this.InitializeComponent();

  private void EventChosingForm_Load(object sender, EventArgs e)
  {
    this.FillListBoxWithEventTypes();
  }

  private void FillListBoxWithEventTypes()
  {
    this.lvEventTypes.BeginUpdate();
    this.lvEventTypes.Items.AddRange(new ListViewItem[12]
    {
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_90"))
      {
        Tag = (object) NotificationEventType.CheckOut
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_92"))
      {
        Tag = (object) NotificationEventType.CheckIn
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_93"))
      {
        Tag = (object) NotificationEventType.Write
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_94"))
      {
        Tag = (object) NotificationEventType.GetAccess
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_95"))
      {
        Tag = (object) NotificationEventType.Cancel
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_96"))
      {
        Tag = (object) NotificationEventType.NextLCLevel
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_97"))
      {
        Tag = (object) NotificationEventType.NextLCStep
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_98"))
      {
        Tag = (object) NotificationEventType.Create
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_109"))
      {
        Tag = (object) NotificationEventType.CreateVersion
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_99"))
      {
        Tag = (object) NotificationEventType.AddLink
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_101"))
      {
        Tag = (object) NotificationEventType.Delete
      },
      new ListViewItem(LocalizationHolder.rm.GetString("Workflow.Client_102"))
      {
        Tag = (object) NotificationEventType.DeleteLink
      }
    });
    this.lvEventTypes.Items[0].Selected = true;
    this.lvEventTypes.Select();
    this.lvEventTypes.EndUpdate();
  }

  private void lvEventTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvEventTypes.SelectedItems.Count != 1)
      return;
    this.eventType = (NotificationEventType) this.lvEventTypes.SelectedItems[0].Tag;
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    this.eventType = (NotificationEventType) this.lvEventTypes.SelectedItems[0].Tag;
    this.Close();
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.eventType = NotificationEventType.None;
    this.Close();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.label1 = new Label();
    this.lvEventTypes = new ListView();
    this.SuspendLayout();
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.Location = new Point(74, 241);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(122, 29);
    this.btnOk.TabIndex = 0;
    this.btnOk.Text = "ОК";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(202, 241);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(123, 29);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(13, 13);
    this.label1.Name = "label1";
    this.label1.Size = new Size(150, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Выберите тип уведомления:";
    this.lvEventTypes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lvEventTypes.Location = new Point(16 /*0x10*/, 30);
    this.lvEventTypes.MultiSelect = false;
    this.lvEventTypes.Name = "lvEventTypes";
    this.lvEventTypes.Size = new Size(309, 205);
    this.lvEventTypes.TabIndex = 3;
    this.lvEventTypes.UseCompatibleStateImageBehavior = false;
    this.lvEventTypes.View = View.List;
    this.lvEventTypes.SelectedIndexChanged += new EventHandler(this.lvEventTypes_SelectedIndexChanged);
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(337, 282);
    this.Controls.Add((Control) this.lvEventTypes);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.MinimumSize = new Size(353, 320);
    this.Name = nameof (EventChosingForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Событие срабатывания уведомления";
    this.Load += new EventHandler(this.EventChosingForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
