// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.TableRefsObjectNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Views;

internal class TableRefsObjectNode(int objTypeID, long objID) : ObjectNode(objTypeID, objID)
{
  public const string CategoryObjectsNodeGuid = "{4B484904-EF4B-4771-8F25-DF183DDF13DA}";
  public static readonly string NodeName = LocalizationHolder.rm.GetString("Imbase.Client_1154");

  public override IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    IViewState service = this.Services != null ? this.Services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    if (service == null || (service.ViewState & ViewStateFlags.NodeInViews) == ViewStateFlags.None && (service.ViewState & ViewStateFlags.InParametersCard) == ViewStateFlags.None)
      return (List<PartSlot>) null;
    return new List<PartSlot>()
    {
      new PartSlot(new Guid("{4B484904-EF4B-4771-8F25-DF183DDF13DA}"), (INodePart) new TableRefsObjectPart(this._objID, this.Services))
    };
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (IDBTypedObjectID) ? (object) new DBTypedObjectID(nodeID.TypeID, (nodeID as NodeID).ObjectID, 0L, string.Empty, 0L, 0L, 0L, string.Empty, 0L) : base.GetData(nodeID, dataFormat);
  }

  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    IViewState service1 = this.Services != null ? this.Services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    ViewStateFlags viewStateFlags = service1 != null ? service1.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.NodeInViews) != ViewStateFlags.NodeInViews && (viewStateFlags & ViewStateFlags.InParametersCard) != ViewStateFlags.InParametersCard)
      return Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending);
    NodeColumnCollection defaultColumns = new NodeColumnCollection();
    Guid columnSchemeGuid1 = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    Guid columnSchemeGuid2 = Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid;
    IColumnSchemes service2 = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    defaultColumns.Add(service2.CreateColumn(columnSchemeGuid1, (object) ObligatoryObjectAttributes.F_OBJECT_ID), 90);
    defaultColumns.Add(service2.CreateColumn(columnSchemeGuid1, (object) ObligatoryObjectAttributes.CAPTION, NodeColumnSortOrder.Ascending, 0), 400);
    return defaultColumns;
  }
}
