// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CreateByAnalog.CreateByAnalogObjectOptionsControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.CreateByAnalog;

/// <summary>
/// Контрол для опций команды создания объектов по аналогу
/// </summary>
public class CreateByAnalogObjectOptionsControl : UserControl
{
  /// <summary>Событие на изменение настроек</summary>
  public EventHandler OptionsChanged;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox chbCopyExistingProcessRoutes;
  private ToolTip toolTip;

  /// <summary>
  /// 
  /// </summary>
  public CreateByAnalogObjectOptionsControl() => this.InitializeComponent();

  /// <summary>Опции команды</summary>
  internal CreateByAnalogObjectOptions Options
  {
    get
    {
      return new CreateByAnalogObjectOptions()
      {
        IgnoreExistingProcessRoutes = !this.chbCopyExistingProcessRoutes.Checked
      };
    }
    set
    {
      if (value == null)
        return;
      this.chbCopyExistingProcessRoutes.Checked = !value.IgnoreExistingProcessRoutes;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chbCopyExistingProcessRoutes_CheckedChanged(object sender, EventArgs e)
  {
    EventHandler optionsChanged = this.OptionsChanged;
    if (optionsChanged == null)
      return;
    optionsChanged((object) this, EventArgs.Empty);
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.chbCopyExistingProcessRoutes = new CheckBox();
    this.toolTip = new ToolTip(this.components);
    this.SuspendLayout();
    this.chbCopyExistingProcessRoutes.AutoSize = true;
    this.chbCopyExistingProcessRoutes.Location = new Point(13, 8);
    this.chbCopyExistingProcessRoutes.Name = "chbCopyExistingProcessRoutes";
    this.chbCopyExistingProcessRoutes.Size = new Size(253, 17);
    this.chbCopyExistingProcessRoutes.TabIndex = 0;
    this.chbCopyExistingProcessRoutes.Text = "Копировать объекты для существующих МО";
    this.toolTip.SetToolTip((Control) this.chbCopyExistingProcessRoutes, "Для создания по аналогу объектов в ПК, имеющие в составе МО по другим входимостям, необходимо установить данный флаг");
    this.chbCopyExistingProcessRoutes.UseVisualStyleBackColor = true;
    this.chbCopyExistingProcessRoutes.CheckedChanged += new EventHandler(this.chbCopyExistingProcessRoutes_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.chbCopyExistingProcessRoutes);
    this.Name = nameof (CreateByAnalogObjectOptionsControl);
    this.Size = new Size(352, 32 /*0x20*/);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
