
// Type: Intermech.Client.Core.Forms.ChecksProgressForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core.Forms;

/// <summary>Форма "Идёт загрузка структуры объекта/объектов"</summary>
internal class ChecksProgressForm : Form
{
  [CanBeNull]
  private static ChecksProgressForm _instance;
  private int _objectsChecked;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label _labelPleaseWaitText;
  private Label _labelProgressText;
  private Panel _panelProgress;
  private ProgressBar _progressBar;
  private Panel panel1;
  private Button _btnCancel;

  protected ChecksProgressForm() => this.InitializeComponent();

  [NotNull]
  public static ChecksProgressForm Init([CanBeNull] Form parentForm, int nodesWaiting)
  {
    LazyInitializer.EnsureInitialized<ChecksProgressForm>(ref ChecksProgressForm._instance, (Func<ChecksProgressForm>) (() =>
    {
      ChecksProgressForm checksProgressForm1 = new ChecksProgressForm();
      if (parentForm != null)
      {
        checksProgressForm1.Owner = parentForm;
        checksProgressForm1.StartPosition = FormStartPosition.Manual;
        ChecksProgressForm checksProgressForm2 = checksProgressForm1;
        Point location = parentForm.Location;
        int x = location.X + (parentForm.Width - checksProgressForm1.Width) / 2;
        location = parentForm.Location;
        int y = location.Y + (parentForm.Height - checksProgressForm1.Height) / 2;
        Point point = new Point(x, y);
        checksProgressForm2.Location = point;
      }
      else
        checksProgressForm1.StartPosition = FormStartPosition.CenterScreen;
      return checksProgressForm1;
    }));
    ChecksProgressForm._instance._progressBar.Maximum = nodesWaiting;
    ChecksProgressForm._instance.FormClosed += new FormClosedEventHandler(ChecksProgressForm._instance.FormClosedHandler);
    ChecksProgressForm._instance.Size = new Size(396, 155);
    ChecksProgressForm._instance.Show();
    Application.DoEvents();
    return ChecksProgressForm._instance;
  }

  protected void FormClosedHandler([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
    ChecksProgressForm._instance = (ChecksProgressForm) null;
  }

  private void _btnCancel_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ChecksProgressForm._instance?.Close();
  }

  /// <summary>Варианты склонения числа для получения человекочитабельной строки</summary>
  public int Sklon(int count)
  {
    count = count <= 10 || count >= 20 ? count % 10 : 7;
    if (count == 1)
      return 1;
    return count > 1 && count < 5 ? 2 : 3;
  }

  public int ObjectsChecked
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._objectsChecked;
    }
    set
    {
      this._objectsChecked = value;
      if (this._objectsChecked > this._progressBar.Maximum)
      {
        this.Close();
      }
      else
      {
        if (this._objectsChecked == 0)
          this._labelProgressText.Text = string.Empty;
        else
          this._labelProgressText.Text = new StringBuilder().AppendFormat(LocalizationHolder.rm.GetString("Client.Core_1669"), (object) this._objectsChecked, this.Sklon(this._objectsChecked) == 1 ? (object) LocalizationHolder.rm.GetString("Client.Core_1670") : (object) LocalizationHolder.rm.GetString("Client.Core_1671"), (object) this._progressBar.Maximum).ToString();
        if (this._objectsChecked % 5 != 0)
          return;
        this._progressBar.Invalidate();
        Application.DoEvents();
      }
    }
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
    this._labelPleaseWaitText = new Label();
    this._labelProgressText = new Label();
    this._panelProgress = new Panel();
    this._progressBar = new ProgressBar();
    this.panel1 = new Panel();
    this._btnCancel = new Button();
    this._panelProgress.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this._labelPleaseWaitText.Dock = DockStyle.Top;
    this._labelPleaseWaitText.Location = new Point(0, 0);
    this._labelPleaseWaitText.Name = "_labelPleaseWaitText";
    this._labelPleaseWaitText.Size = new Size(380, 33);
    this._labelPleaseWaitText.TabIndex = 0;
    this._labelPleaseWaitText.Text = "Идёт обработка отметок, пожалуйста подождите";
    this._labelPleaseWaitText.TextAlign = ContentAlignment.MiddleCenter;
    this._labelProgressText.Dock = DockStyle.Top;
    this._labelProgressText.Location = new Point(0, 55);
    this._labelProgressText.Name = "_labelProgressText";
    this._labelProgressText.Size = new Size(380, 28);
    this._labelProgressText.TabIndex = 1;
    this._labelProgressText.TextAlign = ContentAlignment.MiddleCenter;
    this._panelProgress.Controls.Add((Control) this._progressBar);
    this._panelProgress.Dock = DockStyle.Top;
    this._panelProgress.Location = new Point(0, 33);
    this._panelProgress.Name = "_panelProgress";
    this._panelProgress.Padding = new Padding(20, 0, 20, 0);
    this._panelProgress.Size = new Size(380, 22);
    this._panelProgress.TabIndex = 2;
    this._progressBar.Dock = DockStyle.Fill;
    this._progressBar.Location = new Point(20, 0);
    this._progressBar.MarqueeAnimationSpeed = 30;
    this._progressBar.Name = "_progressBar";
    this._progressBar.Size = new Size(340, 22);
    this._progressBar.Step = 1;
    this._progressBar.Style = ProgressBarStyle.Marquee;
    this._progressBar.TabIndex = 0;
    this.panel1.Controls.Add((Control) this._btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 104);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(380, 35);
    this.panel1.TabIndex = 4;
    this._btnCancel.Anchor = AnchorStyles.Top;
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Location = new Point(143, 6);
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Size = new Size(95, 23);
    this._btnCancel.TabIndex = 0;
    this._btnCancel.Text = "Прервать";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this._btnCancel_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(380, 139);
    this.ControlBox = false;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this._labelProgressText);
    this.Controls.Add((Control) this._panelProgress);
    this.Controls.Add((Control) this._labelPleaseWaitText);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (ChecksProgressForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Обработка отметок";
    this._panelProgress.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
