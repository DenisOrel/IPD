
// Type: Intermech.Navigator.SelectionView.AttributeSourceSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

public class AttributeSourceSelector : Form
{
  private List<RadioButton> _presentRBs = new List<RadioButton>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button buttonCancel;
  private Button buttonOK;
  private Panel panelValues;
  private RadioButton radioButton1;

  public AttributeSourceSelector() => this.InitializeComponent();

  public void SetControls(AttributeSourceTypes inType)
  {
    List<AttributeSourceTypes> attributeSourceTypesList = new List<AttributeSourceTypes>();
    attributeSourceTypesList.Add(AttributeSourceTypes.Auto);
    attributeSourceTypesList.Add(AttributeSourceTypes.Object);
    attributeSourceTypesList.Add(AttributeSourceTypes.Relation);
    this.ClientSize = new Size(this.ClientSize.Width, this.panelBottom.Height + (this.radioButton1.Height * attributeSourceTypesList.Count + 30));
    for (int index = 0; index < attributeSourceTypesList.Count; ++index)
    {
      if (index == 0)
      {
        this.radioButton1.Text = EnumDescConverter.GetEnumDescription((Enum) attributeSourceTypesList[index]);
        this.radioButton1.Tag = (object) attributeSourceTypesList[index];
        if (inType == attributeSourceTypesList[index])
          this.radioButton1.Checked = true;
        this._presentRBs.Add(this.radioButton1);
      }
      else
      {
        RadioButton radioButton = new RadioButton();
        radioButton.Tag = (object) attributeSourceTypesList[index];
        this.panelValues.Controls.Add((Control) radioButton);
        radioButton.AutoSize = true;
        radioButton.Location = new Point(this.radioButton1.Location.X, this.radioButton1.Height * (index + 1));
        radioButton.Text = EnumDescConverter.GetEnumDescription((Enum) attributeSourceTypesList[index]);
        radioButton.UseVisualStyleBackColor = true;
        if (inType == attributeSourceTypesList[index])
          radioButton.Checked = true;
        this._presentRBs.Add(radioButton);
      }
    }
  }

  public AttributeSourceTypes SelectedType
  {
    get
    {
      if (this._presentRBs != null)
      {
        foreach (RadioButton presentRb in this._presentRBs)
        {
          if (presentRb.Checked)
            return (AttributeSourceTypes) presentRb.Tag;
        }
      }
      return AttributeSourceTypes.Auto;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttributeSourceSelector));
    this.panelBottom = new Panel();
    this.buttonCancel = new Button();
    this.buttonOK = new Button();
    this.panelValues = new Panel();
    this.radioButton1 = new RadioButton();
    this.panelBottom.SuspendLayout();
    this.panelValues.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Controls.Add((Control) this.buttonCancel);
    this.panelBottom.Controls.Add((Control) this.buttonOK);
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.buttonOK, "buttonOK");
    this.buttonOK.DialogResult = DialogResult.OK;
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.panelValues, "panelValues");
    this.panelValues.Controls.Add((Control) this.radioButton1);
    this.panelValues.Name = "panelValues";
    componentResourceManager.ApplyResources((object) this.radioButton1, "radioButton1");
    this.radioButton1.Name = "radioButton1";
    this.radioButton1.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.buttonOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.Controls.Add((Control) this.panelValues);
    this.Controls.Add((Control) this.panelBottom);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (AttributeSourceSelector);
    this.ShowInTaskbar = false;
    this.panelBottom.ResumeLayout(false);
    this.panelValues.ResumeLayout(false);
    this.panelValues.PerformLayout();
    this.ResumeLayout(false);
  }
}
