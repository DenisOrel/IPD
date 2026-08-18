
// Type: Intermech.Client.Core.Forms.SelectObjectCompositionWarningForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Forms;

public class SelectObjectCompositionWarningForm : 
  IpsBaseDialog,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _panelWarning1;
  private Label _labelWarning1;
  private PictureBox _pictureWarning1;
  private Panel _panelWarning2;
  private Label _labelWarning2;
  private PictureBox _pictureWarning2;
  private Bevel _bevelWarnings;
  private Panel _panelWarning3;
  private Label _labelWarning3;
  private PictureBox _pictureWarning3;
  private Bevel _bevelWarnings2;

  public SelectObjectCompositionWarningForm() => this.InitializeComponent();

  public SelectObjectCompositionWarningForm(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [NotNull] IEnumerator<string> warnings)
    : base(centerOnForm, ownerServices, contextName)
  {
    this.InitializeComponent();
    this._labelWarning1.Text = warnings.Current;
    if (warnings.MoveNext())
    {
      this._bevelWarnings.Visible = true;
      this._labelWarning2.Text = warnings.Current;
      this._panelWarning2.Visible = true;
      if (warnings.MoveNext())
      {
        this._bevelWarnings2.Visible = true;
        this._labelWarning3.Text = warnings.Current;
        this._panelWarning3.Visible = true;
      }
      else
      {
        int width = this._panelWarning1.Size.Width;
        Size size = this._panelWarning1.Size;
        int num1 = size.Height * 2;
        size = this._pnlDialogButtons.Size;
        int num2 = size.Height * 2;
        int num3 = num1 + num2 + this._bevelWarnings.Height;
        size = this._bevelDialogButtons.Size;
        int height1 = size.Height;
        int height2 = num3 + height1 + 8;
        this.ClientSize = new Size(width, height2);
      }
    }
    else
    {
      int width = this._panelWarning1.Size.Width;
      Size size = this._panelWarning1.Size;
      int height3 = size.Height;
      size = this._pnlDialogButtons.Size;
      int height4 = size.Height;
      int num = height3 + height4;
      size = this._bevelDialogButtons.Size;
      int height5 = size.Height;
      int height6 = num + height5 + 8;
      this.ClientSize = new Size(width, height6);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectObjectCompositionWarningForm));
    this._panelWarning1 = new Panel();
    this._labelWarning1 = new Label();
    this._pictureWarning1 = new PictureBox();
    this._panelWarning2 = new Panel();
    this._labelWarning2 = new Label();
    this._pictureWarning2 = new PictureBox();
    this._bevelWarnings = new Bevel();
    this._panelWarning3 = new Panel();
    this._labelWarning3 = new Label();
    this._pictureWarning3 = new PictureBox();
    this._bevelWarnings2 = new Bevel();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this._panelWarning1.SuspendLayout();
    ((ISupportInitialize) this._pictureWarning1).BeginInit();
    this._panelWarning2.SuspendLayout();
    ((ISupportInitialize) this._pictureWarning2).BeginInit();
    this._panelWarning3.SuspendLayout();
    ((ISupportInitialize) this._pictureWarning3).BeginInit();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 155);
    this._pnlDialogButtons.Size = new Size(318, 36);
    this._cancelButton.Location = new Point(101, 6);
    this._cancelButton.Size = new Size(90, 23);
    this._okButton.Size = new Size(90, 23);
    this._okButton.Text = "Продолжить";
    this._bevelDialogButtons.Location = new Point(0, 153);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(318, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(120, 0);
    this._panelBtns.Size = new Size(198, 36);
    this._panelWarning1.BackColor = SystemColors.Control;
    this._panelWarning1.Controls.Add((Control) this._labelWarning1);
    this._panelWarning1.Controls.Add((Control) this._pictureWarning1);
    this._panelWarning1.Dock = DockStyle.Top;
    this._panelWarning1.Location = new Point(0, 0);
    this._panelWarning1.Name = "_panelWarning1";
    this._panelWarning1.Size = new Size(318, 50);
    this._panelWarning1.TabIndex = 4;
    this._labelWarning1.Dock = DockStyle.Fill;
    this._labelWarning1.Location = new Point(50, 0);
    this._labelWarning1.Name = "_labelWarning1";
    this._labelWarning1.Padding = new Padding(7);
    this._labelWarning1.Size = new Size(268, 50);
    this._labelWarning1.TabIndex = 1;
    this._labelWarning1.Text = "Состав некоторых выбранных объектов не загружен";
    this._labelWarning1.TextAlign = ContentAlignment.MiddleLeft;
    this._pictureWarning1.Dock = DockStyle.Left;
    this._pictureWarning1.Image = (Image) componentResourceManager.GetObject("_pictureWarning1.Image");
    this._pictureWarning1.Location = new Point(0, 0);
    this._pictureWarning1.Name = "_pictureWarning1";
    this._pictureWarning1.Size = new Size(50, 50);
    this._pictureWarning1.TabIndex = 0;
    this._pictureWarning1.TabStop = false;
    this._panelWarning2.Controls.Add((Control) this._labelWarning2);
    this._panelWarning2.Controls.Add((Control) this._pictureWarning2);
    this._panelWarning2.Dock = DockStyle.Top;
    this._panelWarning2.Location = new Point(0, 52);
    this._panelWarning2.Name = "_panelWarning2";
    this._panelWarning2.Size = new Size(318, 50);
    this._panelWarning2.TabIndex = 5;
    this._panelWarning2.Visible = false;
    this._labelWarning2.Dock = DockStyle.Fill;
    this._labelWarning2.Location = new Point(50, 0);
    this._labelWarning2.Name = "_labelWarning2";
    this._labelWarning2.Padding = new Padding(7);
    this._labelWarning2.Size = new Size(268, 50);
    this._labelWarning2.TabIndex = 1;
    this._labelWarning2.Text = "Выбрано более... объектов, обработка может занять длительное время";
    this._labelWarning2.TextAlign = ContentAlignment.MiddleLeft;
    this._pictureWarning2.Dock = DockStyle.Left;
    this._pictureWarning2.Image = (Image) componentResourceManager.GetObject("_pictureWarning2.Image");
    this._pictureWarning2.Location = new Point(0, 0);
    this._pictureWarning2.Name = "_pictureWarning2";
    this._pictureWarning2.Size = new Size(50, 50);
    this._pictureWarning2.TabIndex = 0;
    this._pictureWarning2.TabStop = false;
    this._bevelWarnings.Dock = DockStyle.Top;
    this._bevelWarnings.Location = new Point(0, 50);
    this._bevelWarnings.Name = "_bevelWarnings";
    this._bevelWarnings.Size = new Size(318, 2);
    this._bevelWarnings.TabIndex = 6;
    this._bevelWarnings.Visible = false;
    this._panelWarning3.Controls.Add((Control) this._labelWarning3);
    this._panelWarning3.Controls.Add((Control) this._pictureWarning3);
    this._panelWarning3.Dock = DockStyle.Top;
    this._panelWarning3.Location = new Point(0, 104);
    this._panelWarning3.Name = "_panelWarning3";
    this._panelWarning3.Size = new Size(318, 50);
    this._panelWarning3.TabIndex = 7;
    this._panelWarning3.Visible = false;
    this._labelWarning3.Dock = DockStyle.Fill;
    this._labelWarning3.Location = new Point(50, 0);
    this._labelWarning3.Name = "_labelWarning3";
    this._labelWarning3.Padding = new Padding(7);
    this._labelWarning3.Size = new Size(268, 50);
    this._labelWarning3.TabIndex = 1;
    this._labelWarning3.Text = "Выбрано более... объектов, обработка может занять длительное время";
    this._labelWarning3.TextAlign = ContentAlignment.MiddleLeft;
    this._pictureWarning3.Dock = DockStyle.Left;
    this._pictureWarning3.Image = (Image) componentResourceManager.GetObject("_pictureWarning3.Image");
    this._pictureWarning3.Location = new Point(0, 0);
    this._pictureWarning3.Name = "_pictureWarning3";
    this._pictureWarning3.Size = new Size(50, 50);
    this._pictureWarning3.TabIndex = 0;
    this._pictureWarning3.TabStop = false;
    this._bevelWarnings2.Dock = DockStyle.Top;
    this._bevelWarnings2.Location = new Point(0, 102);
    this._bevelWarnings2.Name = "_bevelWarnings2";
    this._bevelWarnings2.Size = new Size(318, 2);
    this._bevelWarnings2.TabIndex = 8;
    this._bevelWarnings2.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(318, 191);
    this.Controls.Add((Control) this._panelWarning3);
    this.Controls.Add((Control) this._bevelWarnings2);
    this.Controls.Add((Control) this._panelWarning2);
    this.Controls.Add((Control) this._bevelWarnings);
    this.Controls.Add((Control) this._panelWarning1);
    this.Name = nameof (SelectObjectCompositionWarningForm);
    this.Text = "Выбор объектов";
    this.Controls.SetChildIndex((Control) this._panelWarning1, 0);
    this.Controls.SetChildIndex((Control) this._bevelWarnings, 0);
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._panelWarning2, 0);
    this.Controls.SetChildIndex((Control) this._bevelWarnings2, 0);
    this.Controls.SetChildIndex((Control) this._panelWarning3, 0);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this._panelWarning1.ResumeLayout(false);
    ((ISupportInitialize) this._pictureWarning1).EndInit();
    this._panelWarning2.ResumeLayout(false);
    ((ISupportInitialize) this._pictureWarning2).EndInit();
    this._panelWarning3.ResumeLayout(false);
    ((ISupportInitialize) this._pictureWarning3).EndInit();
    this.ResumeLayout(false);
  }
}
