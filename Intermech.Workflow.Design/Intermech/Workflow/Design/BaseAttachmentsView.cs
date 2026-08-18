// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.BaseAttachmentsView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for AttachmentsView.</summary>
[ViewDescriptionProvider(typeof (BaseAttachmentsView.BaseAttachmentsViewDescriptionProvider))]
public class BaseAttachmentsView : AttachmentsView, IView
{
  private long _lastObjectID;
  protected long _objectID;
  protected int _initialObjectType;

  protected virtual void AdjustObject(ref IDBObject obj, ref bool readOnly)
  {
    if (this._initialObjectType == 0)
      this._initialObjectType = obj.TypeID;
    if (this._initialObjectType != wfConsts.WorkOfferTypeID && this._initialObjectType != wfConsts.MessageTypeID)
      return;
    long objectID = 0;
    IDBAttribute attributeById = obj.GetAttributeByID(wfConsts.AttrActivityID);
    if (attributeById != null)
      objectID = attributeById.AsInteger;
    if (objectID != 0L)
      obj = obj.Session.GetObject(objectID, false);
    readOnly = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  protected IDBObject GetObject(IUserSession session)
  {
    if (this._objectID == 0L)
      return (IDBObject) null;
    IDBObject dbObject1 = session.GetObject(this._objectID, false);
    bool readOnly = this.ReadOnly;
    if (dbObject1 != null)
    {
      IDBObject dbObject2 = dbObject1;
      this.AdjustObject(ref dbObject1, ref readOnly);
      if (dbObject1 != null && dbObject2 != dbObject1)
        this._objectID = dbObject1.ObjectID;
    }
    if (dbObject1 != null && this._lastObjectID != this._objectID)
    {
      this.Init(dbObject1);
      this._lastObjectID = this._objectID;
    }
    this.ReadOnly = readOnly || dbObject1 == null;
    return dbObject1;
  }

  [Obsolete("Убрать из использования в IMProject")]
  protected IDBObject GetObject(ref SessionKeeper sk)
  {
    if (this._objectID == 0L)
      return (IDBObject) null;
    if (sk == null)
      sk = new SessionKeeper();
    IDBObject dbObject1 = sk.Session.GetObject(this._objectID, false);
    bool readOnly = this.ReadOnly;
    if (dbObject1 != null)
    {
      IDBObject dbObject2 = dbObject1;
      this.AdjustObject(ref dbObject1, ref readOnly);
      if (dbObject1 != null && dbObject2 != dbObject1)
        this._objectID = dbObject1.ObjectID;
    }
    if (dbObject1 != null && this._lastObjectID != this._objectID)
    {
      this.Init(dbObject1);
      this._lastObjectID = this._objectID;
    }
    this.ReadOnly = readOnly || dbObject1 == null;
    return dbObject1;
  }

  public override int ImageIndex => Holder.AttachsImageIndex;

  public override int OrderID => 2;

  public override string Caption => LocalizationHolder.rm.GetString("Workflow.Client_6");

  public override void Initialize(ISelectedItems items, IServiceProvider services)
  {
    base.Initialize(items, services);
    this._objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    this._initialObjectType = 0;
  }

  void IView.Activate(IView previousView)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = this.GetObject(sessionKeeper.Session);
      if (dbObject != null)
        this.Load(dbObject, previousView);
      else
        this.Activate(previousView);
    }
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BaseAttachmentsView));
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("_grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("_grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "_pageViewsManager");
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "_gridHeaderMenuBar");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (BaseAttachmentsView);
    this.Tag = (object) "";
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  protected class BaseAttachmentsViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Workflow.Client_6"),
        ImageIndex = Holder.AttachsImageIndex,
        OrderID = 2
      };
    }
  }
}
