
// Type: Intermech.Redline.RedliningCommonSettingsPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Redline;

/// <summary>
/// Реализует контрол "Общие настройки" для системы красного карандаша. В соответствии с паттерном MVP все взаимодействие с контролом
/// выполняется только через интерфейс IRedliningCommonSettingsView. Вся логика реализована в классе RedliningCommonSettingsPresenter.
/// </summary>
internal class RedliningCommonSettingsPage : MvpUserControl, IRedliningCommonSettingsView, IView
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox cbLaunchScrShooter;

  public RedliningCommonSettingsPage() => this.InitializeComponent();

  private void OnPageItemChanged(object sender, EventArgs e) => this.RaisePageChanged();

  private void RaisePageChanged()
  {
    if (this.EditableStateChanged == null)
      return;
    this.EditableStateChanged((object) this, EventArgs.Empty);
  }

  bool IRedliningCommonSettingsView.LaunchScreenShooter
  {
    get => this.cbLaunchScrShooter.Checked;
    set => this.cbLaunchScrShooter.Checked = value;
  }

  /// <summary>Событие изменения какого-либо элемента управления.</summary>
  public event EventHandler EditableStateChanged;

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
    this.cbLaunchScrShooter = new CheckBox();
    this.SuspendLayout();
    this.cbLaunchScrShooter.AutoSize = true;
    this.cbLaunchScrShooter.Location = new Point(7, 7);
    this.cbLaunchScrShooter.Name = "cbLaunchScrShooter";
    this.cbLaunchScrShooter.Size = new Size(386, 17);
    this.cbLaunchScrShooter.TabIndex = 0;
    this.cbLaunchScrShooter.Text = "По команде 'Смотреть' запускать приложение для снятия скриншотов";
    this.cbLaunchScrShooter.UseVisualStyleBackColor = true;
    this.cbLaunchScrShooter.CheckedChanged += new EventHandler(this.OnPageItemChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.cbLaunchScrShooter);
    this.Name = nameof (RedliningCommonSettingsPage);
    this.Padding = new Padding(4);
    this.Size = new Size(402, 164);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
