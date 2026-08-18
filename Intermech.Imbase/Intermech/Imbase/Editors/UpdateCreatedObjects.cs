// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.UpdateCreatedObjects
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class UpdateCreatedObjects : Form
{
  private long _linkId;
  private Dictionary<long, long> _recordsList;
  private IContainer components;
  private Button okButton;
  private Button cancelButton;
  private ProgressBar progressBar1;
  private TextBox textBox1;
  private Timer timer1;

  internal static void Show(long linkId, Dictionary<long, long> recordsList)
  {
    if (recordsList == null || recordsList.Count == 0)
      return;
    using (UpdateCreatedObjects updateCreatedObjects = new UpdateCreatedObjects(linkId, recordsList))
    {
      int num = (int) updateCreatedObjects.ShowDialog();
    }
  }

  public UpdateCreatedObjects(long linkId, Dictionary<long, long> recordsList)
    : this()
  {
    this._linkId = linkId;
    this._recordsList = recordsList;
  }

  public UpdateCreatedObjects() => this.InitializeComponent();

  private void UpdateCreatedObjects_Shown(object sender, EventArgs e) => this.timer1.Enabled = true;

  private void timer1_Tick(object sender, EventArgs e)
  {
    this.timer1.Enabled = false;
    this.progressBar1.Maximum = this._recordsList.Count;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IImbaseServer customService = session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
      foreach (long key in this._recordsList.Keys)
      {
        if (this.DialogResult != DialogResult.Cancel)
        {
          ++this.progressBar1.Value;
          long records = this._recordsList[key];
          this.textBox1.AppendText($"{Environment.NewLine}{session.GetObjectInfo(records).Caption} :");
          Application.DoEvents();
          try
          {
            customService.FillObjectAttributes(session.SessionGUID, records, this._linkId, key, false);
            this.textBox1.AppendText(" выполнено успешно.");
            Application.DoEvents();
          }
          catch (Exception ex)
          {
            this.textBox1.AppendText($" Ошибка:.{Environment.NewLine}{ex.Message}");
            Application.DoEvents();
          }
        }
        else
          break;
      }
    }
    this.okButton.Enabled = true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.okButton = new Button();
    this.cancelButton = new Button();
    this.progressBar1 = new ProgressBar();
    this.textBox1 = new TextBox();
    this.timer1 = new Timer(this.components);
    this.SuspendLayout();
    this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.okButton.DialogResult = DialogResult.OK;
    this.okButton.Location = new Point(383, 238);
    this.okButton.Name = "okButton";
    this.okButton.Size = new Size(75, 23);
    this.okButton.TabIndex = 3;
    this.okButton.Text = "OK";
    this.okButton.UseVisualStyleBackColor = true;
    this.cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.cancelButton.DialogResult = DialogResult.Cancel;
    this.cancelButton.Location = new Point(302, 238);
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.Size = new Size(75, 23);
    this.cancelButton.TabIndex = 2;
    this.cancelButton.Text = "Отмена";
    this.cancelButton.UseVisualStyleBackColor = true;
    this.progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.progressBar1.Location = new Point(12, 209);
    this.progressBar1.Maximum = 0;
    this.progressBar1.Name = "progressBar1";
    this.progressBar1.Size = new Size(446, 23);
    this.progressBar1.Step = 1;
    this.progressBar1.Style = ProgressBarStyle.Marquee;
    this.progressBar1.TabIndex = 2;
    this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.textBox1.Location = new Point(12, 12);
    this.textBox1.Multiline = true;
    this.textBox1.Name = "textBox1";
    this.textBox1.ScrollBars = ScrollBars.Vertical;
    this.textBox1.Size = new Size(446, 187);
    this.textBox1.TabIndex = 1;
    this.timer1.Interval = 500;
    this.timer1.Tick += new EventHandler(this.timer1_Tick);
    this.AcceptButton = (IButtonControl) this.okButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.cancelButton;
    this.ClientSize = new Size(470, 273);
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this.progressBar1);
    this.Controls.Add((Control) this.cancelButton);
    this.Controls.Add((Control) this.okButton);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (UpdateCreatedObjects);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Обновление созданных объектов";
    this.Shown += new EventHandler(this.UpdateCreatedObjects_Shown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
