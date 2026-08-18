
// Type: Intermech.PropertyEditors.ActionsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.LifeCycles;
using Intermech.Kernel.Search;
using Intermech.Navigator.EventLog;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.PropertyEditors;

public class ActionsForm : TabPageForm
{
  private IFolder _folder;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private iGCellStyle iGrid1Col0CellStyle;
  private iGColHdrStyle iGrid1Col0ColHdrStyle;
  private iGCellStyle iGrid1Col1CellStyle;
  private iGColHdrStyle iGrid1Col1ColHdrStyle;
  private iGCellStyle iGrid1Col2CellStyle;
  private iGColHdrStyle iGrid1Col2ColHdrStyle;
  private iGCellStyle iGrid1Col3CellStyle;
  private iGColHdrStyle iGrid1Col3ColHdrStyle;
  private iGCellStyle iGrid1Col4CellStyle;
  private iGColHdrStyle iGrid1Col4ColHdrStyle;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private iGCellStyle iGrid1Col5CellStyle;
  private iGColHdrStyle iGrid1Col5ColHdrStyle;
  private iGCellStyle iGrid1Col6CellStyle;
  private iGColHdrStyle iGrid1Col6ColHdrStyle;
  private iGCellStyle iGrid1Col9CellStyle;
  private iGColHdrStyle iGrid1Col9ColHdrStyle;
  private iGCellStyle iGrid1Col10CellStyle;
  private iGColHdrStyle iGrid1Col10ColHdrStyle;
  private iGCellStyle iGrid1Col11CellStyle;
  private iGColHdrStyle iGrid1Col11ColHdrStyle;
  private iGCellStyle iGrid1Col12CellStyle;
  private iGColHdrStyle iGrid1Col12ColHdrStyle;
  private ToolTip _toolTip;
  private EventsView _eventsView;

  public ActionsForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.instGuid = aInstGuid;
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder;
    this._eventsView.Deactivate((IView) null);
    this._eventsView.Initialize((IDescriptor) new ActionsForm.ActionsFormDescriptor(this), (System.IServiceProvider) ServicesManager.ServiceContainer);
    this._eventsView.Activate((IView) null);
    this._eventsView.ReloadItems();
  }

  public override bool SaveForm(IFolder folder)
  {
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).ActionsTabPage))
      StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).ActionsTabPage, false);
    return true;
  }

  public override void FormLostFocus(IFolder folder)
  {
  }

  public override string HelpTopicID => "1595";

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ActionsForm));
    this.iGrid1Col0CellStyle = new iGCellStyle(true);
    this.iGrid1Col0ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col1CellStyle = new iGCellStyle(true);
    this.iGrid1Col1ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col2CellStyle = new iGCellStyle(true);
    this.iGrid1Col2ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col3CellStyle = new iGCellStyle(true);
    this.iGrid1Col3ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col4CellStyle = new iGCellStyle(true);
    this.iGrid1Col4ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col5CellStyle = new iGCellStyle(true);
    this.iGrid1Col5ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col6CellStyle = new iGCellStyle(true);
    this.iGrid1Col6ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col9CellStyle = new iGCellStyle(true);
    this.iGrid1Col9ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col10CellStyle = new iGCellStyle(true);
    this.iGrid1Col10ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col11CellStyle = new iGCellStyle(true);
    this.iGrid1Col11ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col12CellStyle = new iGCellStyle(true);
    this.iGrid1Col12ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    this._toolTip = new ToolTip(this.components);
    this._eventsView = new EventsView();
    this.SuspendLayout();
    this._eventsView.AllowCustomGroupValues = true;
    this._eventsView.AllowEditing = false;
    this._eventsView.Control = (object) this._eventsView;
    this._eventsView.DisableKeyDownEvents = false;
    this._eventsView.DisableParentSelectedItems = true;
    componentResourceManager.ApplyResources((object) this._eventsView, "_eventsView");
    this._eventsView.EditingMode = false;
    this._eventsView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._eventsView.Name = "_eventsView";
    this._eventsView.ViewContentType = ContentType.NonFolders;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this._eventsView);
    this.Name = nameof (ActionsForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "  ";
    this.ResumeLayout(false);
  }

  public sealed class ActionsFormDescriptor : HiveDescriptor
  {
    private ActionsForm _actionsForm;

    public ActionsFormDescriptor(ActionsForm actionsForm)
      : base(10, 0, (string) null)
    {
      this._actionsForm = actionsForm != null ? actionsForm : throw new ArgumentNullException(nameof (actionsForm));
    }

    public override INode GetChild(INodeID nodeID)
    {
      ConditionStructure[] conditions = (ConditionStructure[]) null;
      if (this._actionsForm._folder is ObjectTypeFolder)
        conditions = new ConditionStructure[2]
        {
          new ConditionStructure(-31, RelationalOperators.Equal, (object) 4, LogicalOperators.AND, 0, false),
          new ConditionStructure(-32, RelationalOperators.Equal, (object) (int) (this._actionsForm._folder as ObjectTypeFolder).Id, LogicalOperators.NONE, 0, false)
        };
      else if (this._actionsForm._folder is RelationTypeFolder)
        conditions = new ConditionStructure[2]
        {
          new ConditionStructure(-31, RelationalOperators.Equal, (object) 6, LogicalOperators.AND, 0, false),
          new ConditionStructure(-32, RelationalOperators.Equal, (object) (int) (this._actionsForm._folder as RelationTypeFolder).Id, LogicalOperators.NONE, 0, false)
        };
      else if (this._actionsForm._folder is AttributeGroupFolder)
        conditions = new ConditionStructure[2]
        {
          new ConditionStructure(-31, RelationalOperators.Equal, (object) 12, LogicalOperators.AND, 0, false),
          new ConditionStructure(-32, RelationalOperators.Equal, (object) (int) (this._actionsForm._folder as AttributeGroupFolder).Id, LogicalOperators.NONE, 0, false)
        };
      else if (this._actionsForm._folder is AttributeFolder)
        conditions = new ConditionStructure[2]
        {
          new ConditionStructure(-31, RelationalOperators.Equal, (object) 3, LogicalOperators.AND, 0, false),
          new ConditionStructure(-32, RelationalOperators.Equal, (object) (int) (this._actionsForm._folder as AttributeFolder).Id, LogicalOperators.NONE, 0, false)
        };
      else if (this._actionsForm._folder is LevelFolder)
        conditions = new ConditionStructure[2]
        {
          new ConditionStructure(-31, RelationalOperators.Equal, (object) 8, LogicalOperators.AND, 0, false),
          new ConditionStructure(-32, RelationalOperators.Equal, (object) (int) (this._actionsForm._folder as LevelFolder).Id, LogicalOperators.NONE, 0, false)
        };
      else if (this._actionsForm._folder is CustomFolder)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          conditions = new ConditionStructure[2]
          {
            new ConditionStructure(-31, RelationalOperators.Equal, (object) 16 /*0x10*/, LogicalOperators.AND, 0, false),
            new ConditionStructure(-32, RelationalOperators.Equal, (object) (((CustomFolder) this._actionsForm._folder).GetServerObject(sessionKeeper.Session) as IDBLCSchema).SchemaID, LogicalOperators.NONE, 0, false)
          };
      }
      return (INode) new EventsNode(conditions, (HybridDictionary) null)
      {
        Services = (System.IServiceProvider) this._actionsForm._eventsView.Services
      };
    }
  }
}
