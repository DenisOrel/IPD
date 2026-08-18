// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.View.CehRouteEntryListView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.View;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.View;

internal class CehRouteEntryListView : ProcessRouteEntryListView
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  public override void Initialize(ISelectedItems items, IServiceProvider services)
  {
    List<long> source1 = new List<long>();
    List<NavigatorTreeNode> source2 = new List<NavigatorTreeNode>();
    if (items != null)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID parentData1 = items.GetParentData<IDBTypedObjectID>(index, false);
        if (parentData1 != null && MetaDataHelper.IsObjectTypeChildOf(parentData1.ObjectType, TechCardConsts.ObjectTypes.ProcRoutingID))
        {
          NavigatorTreeNode parentData2 = items.GetParentData<NavigatorTreeNode>(index, false);
          if (parentData2 != null)
          {
            if (!source2.Contains(parentData2))
              source2.Add(parentData2);
          }
          else if (!source1.Contains(parentData1.ObjectID))
            source1.Add(parentData1.ObjectID);
        }
      }
    }
    if (!source1.Any<long>() && !source2.Any<NavigatorTreeNode>())
    {
      DataTable compositionInfoTable;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
        CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) items.AsItems<IDBTypedObjectID>().Select<IDBTypedObjectID, ObjInfoItem>((System.Func<IDBTypedObjectID, ObjInfoItem>) (item => new ObjInfoItem(item.ObjectID, item.ObjectType))).ToArray<ObjInfoItem>(), (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingID), (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, (IEnumerable<ColumnDescriptor>) RelObjInfoDbScheme<ObjInfoItem>.GetSourceTableColumns().ToArray<ColumnDescriptor>(), (IEnumerable<ConditionStructure>) null, false, false, 1, (VersionsRule) null, "cad005aa-306c-11d8-b4e9-00304f19f545");
        compositionInfoTable = service.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams);
      }
      if (compositionInfoTable != null)
      {
        TechRelObjInfoItemsFromDataTableProvider<RelObjInfoItem> dataTableProvider = new TechRelObjInfoItemsFromDataTableProvider<RelObjInfoItem>(compositionInfoTable, false);
        source1.AddRange(dataTableProvider.Execute().Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)));
      }
      else
        source1.Add(0L);
    }
    base.Initialize(source2.Any<NavigatorTreeNode>() ? (ISelectedItems) new NavigatorTreeViewSelectedItems(source2.FirstOrDefault<NavigatorTreeNode>()?.Tree, source2.ToArray()) : ObjectExtensions.GetItems(source1.ToArray(), services), services);
  }
}
