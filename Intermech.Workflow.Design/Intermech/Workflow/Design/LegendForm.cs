// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.LegendForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class LegendForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public LegendForm()
  {
    this.InitializeComponent();
    this.FillLegend();
  }

  public void FillLegend()
  {
    MapView mapView = new MapView();
    mapView.Parent = (Control) this;
    mapView.Dock = DockStyle.Fill;
    MapDocument document = mapView.Document;
    document.StartTransaction();
    int num1 = 10;
    float num2 = 0.0f;
    Intermech.Workflow.ActivityInfo byGuid = ActivityInfos.FindByGuid(wfConsts.TaskGuid);
    float a1 = 0.0f;
    float a2 = 0.0f;
    foreach (ActivityStatus status in Enum.GetValues(typeof (ActivityStatus)))
    {
      switch (status)
      {
        case ActivityStatus.DefineWaiting:
        case ActivityStatus.AutoCompleted:
          continue;
        default:
          string enumDescription = SimpleFuncs.GetEnumDescription((Enum) status);
          WorkflowNode workflowNode = new WorkflowNode(-1L, byGuid.Type);
          workflowNode.Initialize(ClientActivityInfos.ImageList, byGuid.ImageIndex, "", status);
          workflowNode.Remove((MapObject) workflowNode.Label);
          workflowNode.Top = (float) num1 + num2;
          workflowNode.Left = (float) num1;
          num2 = workflowNode.Top + workflowNode.Height;
          document.Add((MapObject) workflowNode);
          MapText mapText = new MapText();
          mapText.Text = enumDescription;
          mapText.Left = workflowNode.Left + workflowNode.Width + (float) num1;
          mapText.Top = (float) ((double) workflowNode.Top + (double) workflowNode.Height / 2.0 - (double) mapText.Height / 2.0);
          document.Add((MapObject) mapText);
          float num3 = mapText.Left + mapText.Width + (float) num1;
          if ((double) a1 < (double) num3)
            a1 = num3;
          a2 = workflowNode.Top + workflowNode.Height + (float) num1;
          continue;
      }
    }
    document.FinishTransaction("pallette fill");
    this.ClientSize = new Size(Convert.ToInt32(Math.Round((double) a1)), Convert.ToInt32(Math.Round((double) a2)) + num1);
    mapView.Document.SetModifiable(false);
  }

  private void LegendForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Escape)
      return;
    this.Close();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LegendForm));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (LegendForm);
    this.ShowInTaskbar = false;
    this.KeyDown += new KeyEventHandler(this.LegendForm_KeyDown);
    this.ResumeLayout(false);
  }
}
