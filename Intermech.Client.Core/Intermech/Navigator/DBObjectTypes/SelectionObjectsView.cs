
// Type: Intermech.Navigator.DBObjectTypes.SelectionObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjectTypes;

public sealed class SelectionObjectsView : ObjectsViewBase
{
  private readonly int _objectTypeID = -1;
  private readonly long _selectionVersionID;
  private readonly string _selectionCaption;
  private readonly int _order;

  public SelectionObjectsView(
    int objectTypeID,
    long selectionVersionID,
    string selectionCaption,
    int order)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(selectionVersionID))
      throw new ArgumentNullException(nameof (selectionVersionID));
    this._objectTypeID = objectTypeID;
    this._selectionVersionID = selectionVersionID;
    this._selectionCaption = selectionCaption;
    this._order = order;
  }

  public override string Caption => this._selectionCaption;

  protected override INode GetNode()
  {
    return (INode) new SelectionObjectsView.SelectionObjectTypeNode(this._objectTypeID, this._selectionVersionID, (IServiceProvider) this.Services);
  }

  public override int ImageIndex
  {
    get
    {
      INamedImageList service;
      return (service = ServicesManager.GetService<INamedImageList>()) != null ? service.ImageIndex("imgFind") : -1;
    }
  }

  public override int OrderID => this._order;

  private sealed class SelectionObjectTypeNode : ObjectTypeNode
  {
    private readonly long _selectionVersionID;

    public SelectionObjectTypeNode(
      int objectTypeID,
      long selectionVersionID,
      IServiceProvider serviceProvider)
      : base(objectTypeID, AccessRights.NotDefined)
    {
      this._selectionVersionID = !ObjectHelper.IsUnknownObjectVersionID(selectionVersionID) ? selectionVersionID : throw new ArgumentException();
      this.Services = serviceProvider ?? throw new ArgumentNullException(nameof (serviceProvider));
    }

    protected override List<PartSlot> CreateFolderSlots() => new List<PartSlot>(0);

    protected override List<PartSlot> CreateNonFolderSlots()
    {
      IObjectTypeNodeOptionsHolder service;
      if (this.Services != null && (service = this.Services.GetService<IObjectTypeNodeOptionsHolder>(false)) != null)
        service.Options = ObjectTypeNodeOptions.None;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return this.SlotsFromSinglePart((INodePart) new ObjectsPart(this.objTypeID, (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).GetConditionStructures((object) sessionKeeper.Session.SessionGUID, this._selectionVersionID), this.Services));
    }
  }
}
