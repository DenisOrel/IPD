
// Type: Intermech.Navigator.Nodes.ObjectTypesNode
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
using System.Linq;


namespace Intermech.Navigator.Nodes;

public sealed class ObjectTypesNode : CompositeNode, IContextAware, INodeNotifications
{
  private int[] _objectTypeIds;

  public ObjectTypesNode(int[] objectTypeIds)
  {
    if (objectTypeIds == null)
      throw new ArgumentNullException(nameof (objectTypeIds));
    this._objectTypeIds = !ObjectTypeHelper.IsAnyUnknownObjectTypeID((IEnumerable<int>) objectTypeIds) ? ((IEnumerable<int>) objectTypeIds).Distinct<int>().OrderBy<int, string>((Func<int, string>) (o => MetaDataHelper.GetObjectTypeName(o))).ToArray<int>() : throw new ArgumentException();
    this.Options |= NodeOptions.CanContainsObjectsList;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (int objectTypeId in this._objectTypeIds)
    {
      Intermech.Navigator.DBObjectTypes.Descriptor descriptor = new Intermech.Navigator.DBObjectTypes.Descriptor(objectTypeId);
      descriptors.Add(MetaDataHelper.GetObjectTypeGuid(objectTypeId), (IDescriptor) descriptor);
    }
    return new List<PartSlot>()
    {
      new PartSlot(new Guid("ADFF3A9F-6FAD-41C7-8A34-D2C8D04C185C"), (INodePart) new DescriptorsPart(descriptors))
    };
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    List<PartSlot> nonFolderSlots = new List<PartSlot>();
    int[] notLocalDescendants = this.GetNotLocalDescendants(this._objectTypeIds);
    foreach (int objTypeID in ((IEnumerable<int>) this._objectTypeIds).Where<int>((Func<int, bool>) (o => !((IEnumerable<int>) notLocalDescendants).Contains<int>(o))))
    {
      ObjectsPart part = new ObjectsPart(objTypeID, this.Services);
      PartSlot partSlot = new PartSlot(MetaDataHelper.GetObjectTypeGuid(objTypeID), (INodePart) part);
      nonFolderSlots.Add(partSlot);
    }
    return nonFolderSlots;
  }

  public IServiceProvider Services { get; set; }

  public ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    if (!(e.EventName == "ApplicabilityAdded") && !(e.EventName == "ApplicabilityChanged") && !(e.EventName == "ApplicabilityRemoved"))
      return ProcessResult.None;
    this.folderSlots = (List<PartSlot>) null;
    this.nonFolderSlots = (List<PartSlot>) null;
    return ProcessResult.RefreshNode;
  }

  private int[] GetNotLocalDescendants(int[] objectTypeIds)
  {
    List<int> intList = new List<int>();
    foreach (int objectTypeId in objectTypeIds)
    {
      foreach (int notLocalDescendant in this.GetNotLocalDescendants(objectTypeId))
      {
        if (!intList.Contains(notLocalDescendant))
          intList.Add(notLocalDescendant);
      }
    }
    return intList.ToArray();
  }

  private IEnumerable<int> GetNotLocalDescendants(int objectTypeID)
  {
    foreach (int childObjectTypeID in MetaDataHelper.GetObjectTypeChildrenID(objectTypeID))
    {
      if (!MetaDataHelper.GetObjectType(childObjectTypeID).IsLocalType)
      {
        yield return childObjectTypeID;
        foreach (int notLocalDescendant in this.GetNotLocalDescendants(childObjectTypeID))
          yield return notLocalDescendant;
      }
    }
  }
}
