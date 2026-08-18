// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.BaseObjectsInfoView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

[ViewDescriptionProvider(typeof (BaseObjectsInfoView.BaseObjectsInfoViewDescriptionProvider))]
public class BaseObjectsInfoView : PropertiesView
{
  private long _selNodeID = -1;
  private string _viewsCaption = string.Empty;
  public static int imageIndex = -1;
  private IContainer components;
  private Panel _pnlPropsRows;
  private Splitter _splitter;
  private System.Windows.Forms.PropertyGrid _pgRows;

  public BaseObjectsInfoView()
  {
    this.InitializeComponent();
    this._imageIndex = BaseObjectsInfoView.imageIndex;
    this.PropertyGrid.HelpVisible = false;
    this._viewsCaption = LocalizationHolder.rm.GetString("Imbase_BaseObjectsInfoView_Caption");
  }

  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    this._selNodeID = itemData.ObjectID;
    this._parentNode = items.GetItemData(0, typeof (INode)) as INode;
    this._nodeID = items.GetItemID(0);
    this._services = services;
  }

  public override void Activate(IView previousView)
  {
    this._splitter.Visible = this._pnlPropsRows.Visible = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._selNodeID, false);
      if (objectActualCopy == null)
        return;
      IDBAttribute attributeById1 = objectActualCopy.GetAttributeByID(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
      if (attributeById1 == null || attributeById1.Values[0] == null || attributeById1.Values[0] == DBNull.Value || attributeById1.AsInteger == 0L)
        return;
      long asInteger = attributeById1.AsInteger;
      this.Initialize(asInteger, -1, -1L, this._services);
      IDBAttribute attributeById2 = objectActualCopy.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
      if (attributeById2 != null)
      {
        if (attributeById2.Values[0] != null)
        {
          if (attributeById2.Values[0] != DBNull.Value)
          {
            if (attributeById2.AsInteger > -1L)
            {
              ObjsFromRowDescr objsFromRowDescr = new ObjsFromRowDescr(sessionKeeper.Session, asInteger, attributeById2.AsInteger);
              if (!objsFromRowDescr.IsEmpty)
              {
                this._pnlPropsRows.Visible = this._splitter.Visible = true;
                this._pgRows.SelectedObject = (object) objsFromRowDescr;
              }
            }
          }
        }
      }
    }
    base.Activate(previousView);
  }

  public override string Caption => this._viewsCaption;

  public override int OrderID
  {
    [DebuggerStepThrough] get => 11;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BaseObjectsInfoView));
    this._pnlPropsRows = new Panel();
    this._pgRows = new System.Windows.Forms.PropertyGrid();
    this._splitter = new Splitter();
    this.pnButtons.SuspendLayout();
    this._pnlPropsRows.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    componentResourceManager.ApplyResources((object) this.pnButtons, "pnButtons");
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    componentResourceManager.ApplyResources((object) this._pnlPropsRows, "_pnlPropsRows");
    this._pnlPropsRows.Controls.Add((Control) this._pgRows);
    this._pnlPropsRows.Name = "_pnlPropsRows";
    componentResourceManager.ApplyResources((object) this._pgRows, "_pgRows");
    this._pgRows.Name = "_pgRows";
    this._pgRows.PropertySort = PropertySort.Alphabetical;
    this._pgRows.ToolbarVisible = false;
    componentResourceManager.ApplyResources((object) this._splitter, "_splitter");
    this._splitter.BorderStyle = BorderStyle.FixedSingle;
    this._splitter.Name = "_splitter";
    this._splitter.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._splitter);
    this.Controls.Add((Control) this._pnlPropsRows);
    this.DoubleBuffered = true;
    this.Name = nameof (BaseObjectsInfoView);
    this.Controls.SetChildIndex((Control) this.pnButtons, 0);
    this.Controls.SetChildIndex((Control) this._pnlPropsRows, 0);
    this.Controls.SetChildIndex((Control) this._splitter, 0);
    this.Controls.SetChildIndex((Control) this.panel1, 0);
    this.pnButtons.ResumeLayout(false);
    this._pnlPropsRows.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class BaseObjectsInfoViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Imbase_BaseObjectsInfoView_Caption"),
        ImageIndex = -1,
        OrderID = 11
      };
    }
  }
}
