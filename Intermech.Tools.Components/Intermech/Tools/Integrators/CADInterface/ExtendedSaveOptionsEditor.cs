// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ExtendedSaveOptionsEditor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public class ExtendedSaveOptionsEditor : WorkCopyCommandOptionsEditor
{
  private ExtendedSaveOptions options;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox cbUpdateArticles;
  private CheckBox cbRecalculateMass;
  private CheckBox cbCreateArticles;

  public ExtendedSaveOptionsEditor()
  {
    this.InitializeComponent();
    this.Text = LocalizationHolder.rm.GetString("Tools.Components_390");
  }

  public void BindToOptions(ExtendedSaveOptions options)
  {
    this.options = options;
    this.cbCreateArticles.Checked = options.CreateNewArticlesOnly;
    this.cbUpdateArticles.Checked = options.UpdateExistingArticlesOnly;
    this.cbRecalculateMass.Checked = options.RecalculateMass;
    this.ToggleRecalculateMass();
  }

  private void ToggleRecalculateMass()
  {
    this.cbRecalculateMass.Enabled = this.cbCreateArticles.Checked || this.cbUpdateArticles.Checked;
  }

  /// <summary>Применяет изменения, сделанные в редакторе опций.</summary>
  public override void ApplyChanges()
  {
    base.ApplyChanges();
    if (this.options == null)
      return;
    this.options.CreateNewArticlesOnly = this.cbCreateArticles.Checked;
    this.options.UpdateExistingArticlesOnly = this.cbUpdateArticles.Checked;
    this.options.RecalculateMass = this.cbRecalculateMass.Checked;
  }

  private void cbDoExtendedSave_CheckedChanged(object sender, EventArgs e)
  {
    this.ToggleRecalculateMass();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExtendedSaveOptionsEditor));
    this.cbUpdateArticles = new CheckBox();
    this.cbRecalculateMass = new CheckBox();
    this.cbCreateArticles = new CheckBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.cbUpdateArticles, "cbUpdateArticles");
    this.cbUpdateArticles.Name = "cbUpdateArticles";
    this.cbUpdateArticles.UseVisualStyleBackColor = true;
    this.cbUpdateArticles.CheckedChanged += new EventHandler(this.cbDoExtendedSave_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbRecalculateMass, "cbRecalculateMass");
    this.cbRecalculateMass.Name = "cbRecalculateMass";
    this.cbRecalculateMass.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbCreateArticles, "cbCreateArticles");
    this.cbCreateArticles.Name = "cbCreateArticles";
    this.cbCreateArticles.UseVisualStyleBackColor = true;
    this.cbCreateArticles.CheckedChanged += new EventHandler(this.cbDoExtendedSave_CheckedChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.Transparent;
    this.Controls.Add((Control) this.cbCreateArticles);
    this.Controls.Add((Control) this.cbRecalculateMass);
    this.Controls.Add((Control) this.cbUpdateArticles);
    this.Name = nameof (ExtendedSaveOptionsEditor);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
