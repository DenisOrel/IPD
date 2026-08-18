// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DynamicHeaderCaptionSettingsForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Output;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

public class DynamicHeaderCaptionSettingsForm : ExtForm
{
  private const string CellNodeCaption = "Заголовок группы записей";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _BtnCancel;
  private Button _BtnOK;
  private UserControlSetupOutput userControlSetupOutput;

  public DynamicHeaderCaptionSettingsForm()
  {
    this.InitializeComponent();
    this.userControlSetupOutput.OwnerForm = (Form) this;
    this.userControlSetupOutput.buttonReset.Visible = false;
    this.userControlSetupOutput.HideDocumentStructurePane();
  }

  public DynamicHeaderCaptionSettingsForm(DynamicHeaderCaptionSettings captionSettings)
  {
    this.InitializeComponent();
    this.userControlSetupOutput.OwnerForm = (Form) this;
    this.userControlSetupOutput.OutputAttributeMappingScheme = (OutputAttributeMappingScheme) captionSettings;
    this.userControlSetupOutput.buttonReset.Visible = false;
    this.userControlSetupOutput.HideDocumentStructurePane();
    this.FillOutputMappingTree();
  }

  private void FillOutputMappingTree()
  {
    List<CellNode> cellNodeList1 = new List<CellNode>();
    foreach (CellOutputMapping cellOutputMapping in this.userControlSetupOutput.OutputAttributeMappingScheme.CellMaping)
    {
      CellOutputMapping cellMap = cellOutputMapping;
      if (!cellNodeList1.Contains<CellNode>((Predicate<CellNode>) (n => n.Id == cellMap.CellId)))
      {
        List<CellNode> cellNodeList2 = cellNodeList1;
        CellNode cellNode = new CellNode(cellMap.CellId, "Заголовок группы записей");
        cellNode.Text = "Заголовок группы записей";
        cellNodeList2.Add(cellNode);
      }
    }
    this.userControlSetupOutput.LoadMappingTreeContent((IEnumerable<TreeNode>) cellNodeList1);
    this.userControlSetupOutput.ExpandMappingTreeFirstNode();
  }

  protected override void UpdateControls()
  {
    base.UpdateControls();
    this._BtnOK.Enabled = this.userControlSetupOutput.Changed;
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    if (this.DialogResult != DialogResult.OK)
      return;
    this.userControlSetupOutput.UpdateScheme();
  }

  private void DynamicHeaderCaptionSettingsForm_Load(object sender, EventArgs e)
  {
    this.userControlSetupOutput.BuildTrees();
    this.userControlSetupOutput.ExpandMappingTreeFirstNode();
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      return this.userControlSetupOutput.CancelButtonRightEdge > 0 ? this.userControlSetupOutput.CancelButtonRightEdge + 1 : this.Size.Width - (this._BtnCancel.Location.X + this._BtnCancel.Size.Width);
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
    this._BtnCancel = new Button();
    this._BtnOK = new Button();
    this.userControlSetupOutput = new UserControlSetupOutput();
    this.SuspendLayout();
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(466, 552);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 4;
    this._BtnCancel.Text = "Отмена";
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(339, 552);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 3;
    this._BtnOK.Text = "ОК";
    this.userControlSetupOutput.Dock = DockStyle.Fill;
    this.userControlSetupOutput.Location = new Point(0, 0);
    this.userControlSetupOutput.MinimumSize = new Size(560, 365);
    this.userControlSetupOutput.Name = "userControlSetupOutput";
    this.userControlSetupOutput.ShowActionButtons = false;
    this.userControlSetupOutput.Size = new Size(599, 591);
    this.userControlSetupOutput.TabIndex = 0;
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(599, 591);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.Controls.Add((Control) this.userControlSetupOutput);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(610, 630);
    this.Name = nameof (DynamicHeaderCaptionSettingsForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Настройки заголовка групп записей";
    this.Load += new EventHandler(this.DynamicHeaderCaptionSettingsForm_Load);
    this.ResumeLayout(false);
  }
}
