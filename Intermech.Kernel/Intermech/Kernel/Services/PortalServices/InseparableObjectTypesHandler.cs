// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.InseparableObjectTypesHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class InseparableObjectTypesHandler : LinkedObjectsHandler, ILinkedObjectsHandler
{
  private List<InseparableObjectTypes> _types;
  private List<int> _allTypes;
  private readonly LinkedObjectComparer _comparer = new LinkedObjectComparer();

  public List<int> HandleTypes
  {
    get
    {
      if (this._allTypes == null)
        this.ReadAllTypes();
      return this._allTypes;
    }
  }

  public List<int> OutputTypes
  {
    get
    {
      if (this._allTypes == null)
        this.ReadAllTypes();
      return this._allTypes;
    }
  }

  public string Name => "Совместно публикуемые объекты";

  public List<LinkedObject> Handle(
    IUserSession session,
    long objectID,
    int objectTypeID,
    string filtrationOwnerID)
  {
    List<LinkedObject> result = new List<LinkedObject>();
    IDBRelationsApplicabilityCollection applicabilityCollection = session.GetRelationsApplicabilityCollection();
    IDBObjectType objectType = session.GetObjectType(objectTypeID);
    MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545");
    EntersInRelationFinder entersInRelationFinder = new EntersInRelationFinder(objectType, applicabilityCollection);
    ConsistFromRelationFinder consistFromRelationFinder = new ConsistFromRelationFinder(objectType, applicabilityCollection);
    foreach (InseparableObjectTypes inseparableObjectTypes in this._types.FindAll((Predicate<InseparableObjectTypes>) (x => x.LeftTypes.Contains(objectTypeID))))
      this.HandleFoundTypes(inseparableObjectTypes.RightTypes, ref result, session, objectID, entersInRelationFinder, consistFromRelationFinder);
    foreach (InseparableObjectTypes inseparableObjectTypes in this._types.FindAll((Predicate<InseparableObjectTypes>) (x => x.RightTypes.Contains(objectTypeID))))
      this.HandleFoundTypes(inseparableObjectTypes.LeftTypes, ref result, session, objectID, entersInRelationFinder, consistFromRelationFinder);
    return result;
  }

  private void HandleFoundTypes(
    List<int> foundTypes,
    ref List<LinkedObject> result,
    IUserSession session,
    long objectID,
    EntersInRelationFinder entersInRelationFinder,
    ConsistFromRelationFinder consistFromRelationFinder)
  {
    foreach (int foundType in foundTypes)
    {
      IList<LinkedObject> second1 = entersInRelationFinder.Find(session, foundType, objectID);
      if (second1.Count > 0)
        result = result.Union<LinkedObject>((IEnumerable<LinkedObject>) second1, (IEqualityComparer<LinkedObject>) this._comparer).ToList<LinkedObject>();
      IList<LinkedObject> second2 = consistFromRelationFinder.Find(session, foundType, objectID);
      if (second2.Count > 0)
        result = result.Union<LinkedObject>((IEnumerable<LinkedObject>) second2, (IEqualityComparer<LinkedObject>) this._comparer).ToList<LinkedObject>();
    }
  }

  protected override void OnReloadTypes()
  {
    this._types = new List<InseparableObjectTypes>();
    this._allTypes = (List<int>) null;
    IPublishRulesService service = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true);
    if (service.InseparableObjectTypes == null || service.InseparableObjectTypes.Count <= 0)
      return;
    foreach (Tuple<int, int> inseparableObjectType in service.InseparableObjectTypes)
      this._types.Add(new InseparableObjectTypes(MetaDataHelper.GetObjectTypeChildrenIDRecursive(inseparableObjectType.Item1), MetaDataHelper.GetObjectTypeChildrenIDRecursive(inseparableObjectType.Item2)));
  }

  private void ReadAllTypes()
  {
    this._allTypes = new List<int>();
    foreach (InseparableObjectTypes type in this._types)
      this._allTypes = this._allTypes.Union<int>((IEnumerable<int>) type.LeftTypes).Union<int>((IEnumerable<int>) type.RightTypes).ToList<int>();
  }

  bool ILinkedObjectsHandler.IsTypesChanged(IUserSession session) => this.IsTypesChanged(session);

  void ILinkedObjectsHandler.UpdateHandleAndOutputTypes(IUserSession session, bool force)
  {
    this.UpdateHandleAndOutputTypes(session, force);
  }
}
