// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.EditorSettingsForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Controls;
using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class EditorSettingsForm : FormEx
{
  private EditorSettings _settings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PropertyGrid propertyGrid;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;

  public EditorSettingsForm()
  {
    this._settings = Holder.EditorSettings;
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1290);
  }

  public void PropagateProperties()
  {
    foreach (EditorInfo editor in (List<EditorInfo>) Holder.Editors)
      Holder.EditorSettings.SetProperties((editor.Form as wfEditorForm).View);
  }

  public void GetProperties(wfEditorForm form)
  {
    GraphView view = form.View;
    if (!EditorSettings.Loaded)
    {
      this._settings.BackColor = view.BackColor;
      this._settings.GridCellSize = view.GridCellSize;
      this._settings.GridColor = view.GridColor;
      this._settings.GridSnapDrag = view.GridSnapDrag;
      this._settings.GridStyle = view.GridStyle;
    }
    this.propertyGrid.SelectedObject = (object) this._settings;
  }

  public static void EditEditorProperties(wfEditorForm form)
  {
    using (EditorSettingsForm editorSettingsForm = new EditorSettingsForm())
    {
      editorSettingsForm.GetProperties(form);
      if (editorSettingsForm.ShowDialog() != DialogResult.OK)
        return;
      editorSettingsForm._settings.Save();
      editorSettingsForm.PropagateProperties();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditorSettingsForm));
    this.propertyGrid = new PropertyGrid();
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.Panel2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.Name = "propertyGrid";
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.propertyGrid);
    this.Controls.Add((Control) this.Panel2);
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditorSettingsForm);
    this.ShowInTaskbar = false;
    this.Panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
