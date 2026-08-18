
// Type: Intermech.PropertyEditors.PropertyForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Форма "Свойства"</summary>
public class PropertyForm : TabPageForm
{
  private PropertyGrid propertyGrid;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public PropertyForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PropertyForm));
    this.propertyGrid = new PropertyGrid();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.PropertySort = PropertySort.Alphabetical;
    this.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
    this.Controls.Add((Control) this.propertyGrid);
    this.Name = nameof (PropertyForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "  ";
    this.Load += new EventHandler(this.PropertyForm_Load);
    this.ResumeLayout(false);
  }

  private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (EventsHolder.BlockOnChange)
      return;
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, true);
    EventsHolder.FireWasChanged(s, this.instGuid, (EventArgs) e);
  }

  public PropertyGrid PropertyGrid => this.propertyGrid;

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    if (StatesController.GetLoadState((object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage))
      return;
    this._folder.LoadData(this._folder.PlacePanel, true);
    StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, true);
  }

  public override bool SaveForm(IFolder folder)
  {
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage))
      StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, false);
    return true;
  }

  private void PropertyForm_Load(object sender, EventArgs e)
  {
  }

  public override string HelpTopicID
  {
    get
    {
      if (this._folder != null)
      {
        if (this._folder is AttributeFolder)
          return "1008";
        if (this._folder is AttributeGroupFolder)
          return "1390";
        if (this._folder is ObjectTypeFolder)
          return "1021";
        if (this._folder is RelationTypeFolder)
          return "1032";
        if (this._folder is LevelFolder)
          return "1039";
        if (this._folder is LCSchemaFolder)
          return "1044";
        if (this._folder is AreaFolder)
          return "1050";
        if (this._folder is LanguageFolder)
          return "1055";
      }
      return base.HelpTopicID;
    }
  }
}
