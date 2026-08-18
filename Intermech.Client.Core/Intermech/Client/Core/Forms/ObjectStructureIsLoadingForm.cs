
// Type: Intermech.Client.Core.Forms.ObjectStructureIsLoadingForm
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
public class ObjectStructureIsLoadingForm : Form
{
  [CanBeNull]
  private static ObjectStructureIsLoadingForm _instance;
  private int _objectsLoaded;
  [CanBeNull]
  private Action _onCancel;
  [CanBeNull]
  private Graphics _labelLoadedTextGraphics;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label _labelPleaseWaitText;
  private Button _btnCancel;
  private Label _labelLoadedText;
  private Panel _panelProgress;
  private ProgressBar _progressBar;
  private Panel panel1;

  protected ObjectStructureIsLoadingForm()
  {
    this.InitializeComponent();
    this.FormClosed += new FormClosedEventHandler(this._instance_FormClosed);
  }

  [NotNull]
  public static ObjectStructureIsLoadingForm Init([CanBeNull] Form parentForm, [NotNull] Action onCancel)
  {
    LazyInitializer.EnsureInitialized<ObjectStructureIsLoadingForm>(ref ObjectStructureIsLoadingForm._instance, (Func<ObjectStructureIsLoadingForm>) (() =>
    {
      ObjectStructureIsLoadingForm structureIsLoadingForm1 = new ObjectStructureIsLoadingForm();
      if (parentForm != null)
      {
        structureIsLoadingForm1.Owner = parentForm;
        structureIsLoadingForm1.StartPosition = FormStartPosition.Manual;
        ObjectStructureIsLoadingForm structureIsLoadingForm2 = structureIsLoadingForm1;
        Point location = parentForm.Location;
        int x = location.X + (parentForm.Width - structureIsLoadingForm1.Width) / 2;
        location = parentForm.Location;
        int y = location.Y + (parentForm.Height - structureIsLoadingForm1.Height) / 2;
        Point point = new Point(x, y);
        structureIsLoadingForm2.Location = point;
      }
      else
        structureIsLoadingForm1.StartPosition = FormStartPosition.CenterScreen;
      return structureIsLoadingForm1;
    }));
    ObjectStructureIsLoadingForm._instance._onCancel = onCancel;
    return ObjectStructureIsLoadingForm._instance;
  }

  public new void Close()
  {
    base.Close();
    if (this.Visible)
      return;
    this._instance_FormClosed((object) this, new FormClosedEventArgs(CloseReason.None));
  }

  protected void _instance_FormClosed([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
    if (this._onCancel != null)
    {
      this._onCancel();
      this._onCancel = (Action) null;
    }
    ObjectStructureIsLoadingForm._instance = (ObjectStructureIsLoadingForm) null;
  }

  private void _btnCancel_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ((Form) ObjectStructureIsLoadingForm._instance)?.Close();
  }

  /// <summary>Варианты склонение числа для получения человекочитабельной строки</summary>
  public int Sklon(int count)
  {
    count = count <= 10 || count >= 20 ? count % 10 : 7;
    if (count == 1)
      return 1;
    return count > 1 && count < 5 ? 2 : 3;
  }

  [NotNull]
  public Graphics LabelLoadedTextGraphics
  {
    get
    {
      return this._labelLoadedTextGraphics ?? (this._labelLoadedTextGraphics = this._labelLoadedText.CreateGraphics());
    }
  }

  public int ObjectsLoaded
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._objectsLoaded;
    }
    set
    {
      if (ObjectStructureIsLoadingForm._instance == null || this.Disposing || this.IsDisposed)
        return;
      this._objectsLoaded = value;
      if (this._objectsLoaded == 0)
        this._labelLoadedText.Text = string.Empty;
      else
        this._labelLoadedText.Text = new StringBuilder().AppendFormat(LocalizationHolder.rm.GetString("Client.Core_1672"), (object) this._objectsLoaded, this.Sklon(this._objectsLoaded) == 1 ? (object) LocalizationHolder.rm.GetString("Client.Core_1670") : (object) LocalizationHolder.rm.GetString("Client.Core_1671")).ToString();
      SizeF sizeF = this.LabelLoadedTextGraphics.MeasureString(this._labelLoadedText.Text, this._labelLoadedText.Font, new SizeF((float) this._labelLoadedText.Width, (float) this._labelLoadedText.Height));
      ref SizeF local = ref sizeF;
      double width1 = (double) sizeF.Width;
      Padding padding1 = this._labelLoadedText.Padding;
      double left = (double) padding1.Left;
      double num1 = width1 + left;
      padding1 = this._labelLoadedText.Padding;
      double right = (double) padding1.Right;
      double width2 = num1 + right;
      double height1 = (double) sizeF.Height;
      Padding padding2 = this._labelLoadedText.Padding;
      double bottom = (double) padding2.Bottom;
      double num2 = height1 + bottom;
      padding2 = this._labelLoadedText.Padding;
      double top = (double) padding2.Top;
      double height2 = num2 + top;
      local = new SizeF((float) width2, (float) height2);
      this._labelLoadedText.Height = (int) sizeF.Height;
      this.ClientSize = new Size(this.ClientSize.Width, this._labelPleaseWaitText.Height + this._panelProgress.Height + this._labelLoadedText.Height + this.panel1.Height);
      if (this.Visible || this._objectsLoaded <= 20)
        return;
      this.Size = new Size(396, 155);
      this.Show();
      Application.DoEvents();
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
    this._btnCancel = new Button();
    this._labelLoadedText = new Label();
    this._panelProgress = new Panel();
    this._progressBar = new ProgressBar();
    this.panel1 = new Panel();
    this._panelProgress.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this._labelPleaseWaitText.Dock = DockStyle.Top;
    this._labelPleaseWaitText.Location = new Point(0, 0);
    this._labelPleaseWaitText.Name = "_labelPleaseWaitText";
    this._labelPleaseWaitText.Size = new Size(380, 33);
    this._labelPleaseWaitText.TabIndex = 0;
    this._labelPleaseWaitText.Text = "Идёт загрузка содержимого объектов, пожалуйста подождите";
    this._labelPleaseWaitText.TextAlign = ContentAlignment.MiddleCenter;
    this._btnCancel.Anchor = AnchorStyles.Top;
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Location = new Point(143, 6);
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Size = new Size(95, 23);
    this._btnCancel.TabIndex = 0;
    this._btnCancel.Text = "Прервать";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this._btnCancel_Click);
    this._labelLoadedText.Dock = DockStyle.Top;
    this._labelLoadedText.Location = new Point(0, 55);
    this._labelLoadedText.Name = "_labelLoadedText";
    this._labelLoadedText.Padding = new Padding(5);
    this._labelLoadedText.Size = new Size(380, 28);
    this._labelLoadedText.TabIndex = 1;
    this._labelLoadedText.TextAlign = ContentAlignment.MiddleCenter;
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
    this._progressBar.Style = ProgressBarStyle.Marquee;
    this._progressBar.TabIndex = 0;
    this.panel1.Controls.Add((Control) this._btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 106);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(380, 35);
    this.panel1.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.ClientSize = new Size(380, 141);
    this.ControlBox = false;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this._labelLoadedText);
    this.Controls.Add((Control) this._panelProgress);
    this.Controls.Add((Control) this._labelPleaseWaitText);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (ObjectStructureIsLoadingForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Загрузка структуры";
    this._panelProgress.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
