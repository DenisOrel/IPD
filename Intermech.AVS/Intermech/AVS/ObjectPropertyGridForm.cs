// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ObjectPropertyGridForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Summary description for ObjectPropertyGridForm.</summary>
public class ObjectPropertyGridForm : DockControl, ISkipTargetActivate
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private PropertiesView propertiesView;
  private System.IServiceProvider serviceProvider;

  public PropertiesView PropertiesView => this.propertiesView;

  public ObjectPropertyGridForm()
  {
    this.InitializeComponent();
    this.AllowedStates &= ~DockLocation.Document;
  }

  public ObjectPropertyGridForm(System.IServiceProvider serviceProvider)
  {
    this.InitializeComponent();
    this.serviceProvider = serviceProvider;
  }

  public void SelectObject(long objID, int objType, string objCaption, long relID)
  {
    this.propertiesView.Initialize(objID, objType, relID, this.serviceProvider);
    this.propertiesView.Activate((IView) null);
    this.UpdateCaption(objCaption);
  }

  /// <summary>Clean up any resources being used.</summary>
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
    this.propertiesView = new PropertiesView();
    this.SuspendLayout();
    this.propertiesView.Dock = DockStyle.Fill;
    this.propertiesView.Location = new Point(0, 0);
    this.propertiesView.Name = "propertiesView";
    this.propertiesView.Padding = new Padding(2);
    this.propertiesView.Size = new Size(288, 594);
    this.propertiesView.TabIndex = 0;
    this.Controls.Add((Control) this.propertiesView);
    this.HideOnClose = true;
    this.Name = nameof (ObjectPropertyGridForm);
    this.Size = new Size(288, 594);
    this.Text = "Свойства";
    this.ResumeLayout(false);
  }

  private void propertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
  {
    if (this.propertiesView.PropertyGrid.SelectedObject != null)
      return;
    this.UpdateCaption("");
  }

  public void UpdateCaption(string objectCaption)
  {
    if (objectCaption != null)
      this.Text = $"Свойства {objectCaption}";
    else
      this.Text = "Свойства";
  }

  public override void OnClosed(EventArgs e)
  {
    this.SelectObject(-1L, -1, (string) null, -1L);
    this.propertiesView.Deactivate((IView) null);
    base.OnClosed(e);
  }

  /// <summary>вернуть раздел справки для контрола</summary>
  public override string HelpID => "1507";
}
