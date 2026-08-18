
// Type: Intermech.Search.ObjectGroups.ObjectAndObjectGroupNodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectAndObjectGroupNodePart : INodePart, INodeItems
{
  private INodePart _nodePart;
  private ObjectGroupNodePart[] _objectGroupNodeParts;

  public ObjectAndObjectGroupNodePart(
    ObjectGroupNodePart[] objectGroupsNodeParts,
    INodePart nodePart)
  {
    if (objectGroupsNodeParts == null)
      throw new ArgumentNullException(nameof (objectGroupsNodeParts));
    if (nodePart == null)
      throw new ArgumentNullException(nameof (nodePart));
    this._objectGroupNodeParts = objectGroupsNodeParts;
    this._nodePart = nodePart;
  }

  public ContentAttributes GetAttributesOf(INodeID nodeID)
  {
    return nodeID is ObjectGroupNodeID ? this.GetObjectGroupNodePartForNodeID((ObjectGroupNodeID) nodeID).GetAttributesOf(nodeID) : this._nodePart.GetAttributesOf(nodeID);
  }

  public INode GetChild(INodeID nodeID)
  {
    return nodeID is ObjectGroupNodeID ? this.GetObjectGroupNodePartForNodeID((ObjectGroupNodeID) nodeID).GetChild(nodeID) : this._nodePart.GetChild(nodeID);
  }

  public string GetAddress(INodeID nodeID)
  {
    return nodeID is ObjectGroupNodeID ? this.GetObjectGroupNodePartForNodeID((ObjectGroupNodeID) nodeID).GetAddress(nodeID) : this._nodePart.GetAddress(nodeID);
  }

  public INodeID ParseAddress(string address) => this._nodePart.ParseAddress(address);

  public PersistentState Serialize(INodeID nodeID)
  {
    return !(nodeID is ObjectGroupNodeID) ? this._nodePart.Serialize(nodeID) : (PersistentState) null;
  }

  public INodeID Deserialize(PersistentState persistNodeID)
  {
    return this._nodePart.Deserialize(persistNodeID);
  }

  public object GetData(INodeID nodeID, Type dataFormat)
  {
    return nodeID is ObjectGroupNodeID ? this.GetObjectGroupNodePartForNodeID((ObjectGroupNodeID) nodeID).GetData(nodeID, dataFormat) : this._nodePart.GetData(nodeID, dataFormat);
  }

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    return this._nodePart.GetData(nodeIDs, dataFormat);
  }

  public IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    if ((e.EventName == "RelationsCreated" || e.EventName == "ManagedRelationsInsert" || e.EventName == "ManagedRelationsCreated") && e is DBRelationsEventArgs && this._nodePart is RelatedObjectsPart)
    {
      DBRelationsEventArgs relationsEventArgs = (DBRelationsEventArgs) e;
      List<ObjectGroupNodeID> objectGroupNodeIdList = new List<ObjectGroupNodeID>();
      List<long> relationIDs = new List<long>();
      List<int> intList = new List<int>();
      List<long> longList = new List<long>();
      if (relationsEventArgs.KnownRelationTypes != null)
        intList = relationsEventArgs.KnownRelationTypes.ToList<int>();
      if (relationsEventArgs.ProjIDs != null)
        longList = relationsEventArgs.ProjIDs;
      if (intList.Count == 0 || longList.Count == 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (long relationId in (IEnumerable<long>) relationsEventArgs.RelationIDs)
          {
            IDBRelation relation = sessionKeeper.Session.GetRelation(relationId, false);
            if (relation != null)
            {
              intList.Add(relation.TypeID);
              longList.Add(relation.ProjID);
            }
          }
        }
      }
      if (intList.Contains(((RelatedObjectsPart) this._nodePart).RelationTypeID))
      {
        int num = -1;
        foreach (long relationId in (IEnumerable<long>) relationsEventArgs.RelationIDs)
        {
          if (longList.Contains(((RelatedPartBase) this._nodePart).ProjectVersionID))
          {
            if (relationsEventArgs.PartTypeDictionaryByRelationID == null)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBRelation relation = sessionKeeper.Session.GetRelation(relationId, false);
                if (relation != null)
                  num = sessionKeeper.Session.GetObjectByID(relation.PartID, false).ObjectType;
              }
            }
            else
              relationsEventArgs.PartTypeDictionaryByRelationID.TryGetValue(relationId, out num);
            if (!ObjectTypeHelper.IsUnknownObjectTypeID(num))
            {
              List<int> partAndAncestorsTypeIds = new List<int>();
              partAndAncestorsTypeIds.Add(num);
              partAndAncestorsTypeIds.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeParentsID(num));
              ObjectGroupNodePart[] array = ((IEnumerable<ObjectGroupNodePart>) this._objectGroupNodeParts).Where<ObjectGroupNodePart>((Func<ObjectGroupNodePart, bool>) (o => partAndAncestorsTypeIds.Contains(o.PartTypeID))).ToArray<ObjectGroupNodePart>();
              if (array.Length == 0)
              {
                relationIDs.Add(relationId);
              }
              else
              {
                foreach (ObjectGroupNodePart objectGroupNodePart in array)
                {
                  ObjectGroupNodeID objectGroupNodeId = new ObjectGroupNodeID(objectGroupNodePart.ProjectTypeID, objectGroupNodePart.RelationTypeID, objectGroupNodePart.PartTypeID, objectGroupNodePart.ProjectVersionID);
                  objectGroupNodeIdList.Add(objectGroupNodeId);
                }
              }
            }
          }
        }
      }
      if (relationIDs.Count > 0 && objectGroupNodeIdList.Count > 0)
        return (IUpdateAnalyser) new ObjectAndObjectGroupNodePart.ObjectAndObjectGroupNavigatorTreeViewUpdateAnalyzer(new IUpdateAnalyser[2]
        {
          (IUpdateAnalyser) new ObjectGroupCreatedAnalyser(objectGroupNodeIdList.ToArray()),
          (IUpdateAnalyser) new RelationsCreatedAnalyser((IList<long>) relationIDs)
        });
      if (relationIDs.Count > 0)
        return (IUpdateAnalyser) new RelationsCreatedAnalyser((IList<long>) relationIDs);
      return objectGroupNodeIdList.Count > 0 ? (IUpdateAnalyser) new ObjectGroupCreatedAnalyser(objectGroupNodeIdList.ToArray()) : (IUpdateAnalyser) null;
    }
    if (!(e.EventName == "RelationsRemoved") || !(this._nodePart is RelatedObjectsPart))
      return this._nodePart.GetAnalyser(capabilities, sender, e);
    DBRelationsEventArgs relationsEventArgs1 = (DBRelationsEventArgs) e;
    return (IUpdateAnalyser) new ObjectAndObjectGroupNodePart.ObjectAndObjectGroupNavigatorTreeViewUpdateAnalyzer(new IUpdateAnalyser[2]
    {
      (IUpdateAnalyser) new RelationsRemovedAnalyser(relationsEventArgs1.RelationIDs),
      (IUpdateAnalyser) new ObjectGroupRemovedAnalyser(relationsEventArgs1.RelationIDs.ToArray<long>())
    });
  }

  public object GetService(Type service) => this._nodePart.GetService(service);

  public object Owner
  {
    get => this._nodePart.Owner;
    set => this._nodePart.Owner = value;
  }

  public INodeQuery GetQuery()
  {
    return (INodeQuery) new ObjectAndObjectGroupNodeQuery(this._nodePart.GetQuery(), ((IEnumerable<ObjectGroupNodePart>) this._objectGroupNodeParts).Select<ObjectGroupNodePart, ObjectGroupNodeQuery>((Func<ObjectGroupNodePart, ObjectGroupNodeQuery>) (o => (ObjectGroupNodeQuery) o.GetQuery())).ToArray<ObjectGroupNodeQuery>());
  }

  public NodeColumnCollection GetDefaultColumns() => this._nodePart.GetDefaultColumns();

  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    return this._nodePart.GetSupportedColumns(ColumnSetName);
  }

  public List<string> GetSupportedColumnSetNames() => this._nodePart.GetSupportedColumnSetNames();

  private ObjectGroupNodePart GetObjectGroupNodePartForNodeID(ObjectGroupNodeID objectGroupNodeID)
  {
    return ((IEnumerable<ObjectGroupNodePart>) this._objectGroupNodeParts).FirstOrDefault<ObjectGroupNodePart>((Func<ObjectGroupNodePart, bool>) (o => o.ProjectTypeID == objectGroupNodeID.ProjectTypeID && o.RelationTypeID == objectGroupNodeID.RelationTypeID && o.PartTypeID == objectGroupNodeID.PartTypeID && Math.Abs(o.ProjectVersionID) == Math.Abs(objectGroupNodeID.ProjectVersionID)));
  }

  public sealed class ObjectAndObjectGroupNavigatorTreeViewUpdateAnalyzer : 
    IUpdateAnalyser,
    INavigatorTreeViewUpdateAnalyzer
  {
    private IUpdateAnalyser[] _updateAnalyzers;

    public ObjectAndObjectGroupNavigatorTreeViewUpdateAnalyzer(IUpdateAnalyser[] updateAnalysers)
    {
      this._updateAnalyzers = updateAnalysers != null ? updateAnalysers : throw new ArgumentNullException("updateAnalyzers");
    }

    public void Preprocess(IUpdatePlan plan)
    {
      foreach (IUpdateAnalyser updateAnalyzer in this._updateAnalyzers)
        updateAnalyzer.Preprocess(plan);
    }

    public void Process(INodeID nodeID, IUpdatePlan plan)
    {
      foreach (IUpdateAnalyser updateAnalyzer in this._updateAnalyzers)
        updateAnalyzer.Process(nodeID, plan);
    }

    public void Postprocess(IUpdatePlan plan)
    {
      foreach (IUpdateAnalyser updateAnalyzer in this._updateAnalyzers)
        updateAnalyzer.Postprocess(plan);
    }

    public void Process(NavigatorTreeNode node, IUpdatePlan updatePlan)
    {
      foreach (IUpdateAnalyser updateAnalyzer in this._updateAnalyzers)
      {
        if (updateAnalyzer is INavigatorTreeViewUpdateAnalyzer)
          ((INavigatorTreeViewUpdateAnalyzer) updateAnalyzer).Process(node, updatePlan);
      }
    }
  }
}
