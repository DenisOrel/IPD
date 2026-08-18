// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.RevisionComplect
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;

#nullable disable
namespace Intermech.ECO.Server;

internal class RevisionComplect
{
  private static IEventLogHelper eventLogHelper;
  public static Guid RevisionComplect_TypeGuid = new Guid("cadd9522-306c-11d8-b4e9-00304f19f545");
  public static Guid RevisionComplectRelation_TypeGuid = new Guid("cadd9523-306c-11d8-b4e9-00304f19f545");
  public static Guid Revision_TypeGuid = new Guid("cad00348-306c-11d8-b4e9-00304f19f545");
  public static readonly string InventoryNumberGuid = "cadd935b-306c-11d8-b4e9-00304f19f545";

  public static void Load(IServiceProvider serviceProvider)
  {
    IDBObjectCreator creatorInstance1 = (IDBObjectCreator) new RevisionComplectCreator();
    (serviceProvider.GetService(typeof (IDBObjectService)) as ICreatorContainer).AddCreator((object) RevisionComplect.RevisionComplect_TypeGuid, (object) creatorInstance1, true);
    ICreatorContainer service = serviceProvider.GetService(typeof (IDBRelationService)) as ICreatorContainer;
    IDBRelationCreator dbRelationCreator = (IDBRelationCreator) new RevisionComplectRelationCreator();
    // ISSUE: variable of a boxed type
    __Boxed<Guid> relationTypeGuid = (ValueType) RevisionComplect.RevisionComplectRelation_TypeGuid;
    IDBRelationCreator creatorInstance2 = dbRelationCreator;
    service.AddCreator((object) relationTypeGuid, (object) creatorInstance2, true);
    RevisionComplect.eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
  }

  public static int RevisionComplect_TypeId
  {
    get => MetaDataHelper.GetObjectTypeID(RevisionComplect.RevisionComplect_TypeGuid);
  }

  public static int RevisionComplectRelation_TypeId
  {
    get => MetaDataHelper.GetRelationTypeID(RevisionComplect.RevisionComplectRelation_TypeGuid);
  }

  public static int Revision_TypeId
  {
    get => MetaDataHelper.GetObjectTypeID(RevisionComplect.Revision_TypeGuid);
  }

  public static int Attr_Designation
  {
    get => MetaDataHelper.GetAttributeID((object) new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
  }

  public static int Attr_Sort
  {
    get => MetaDataHelper.GetAttributeID((object) new Guid("cad00202-306c-11d8-b4e9-00304f19f545"));
  }

  public static int Attr_TermOfChange
  {
    get => MetaDataHelper.GetAttributeID((object) new Guid("cad007a0-306c-11d8-b4e9-00304f19f545"));
  }

  public static int Attr_InventoryNumber
  {
    get => MetaDataHelper.GetAttributeID((object) new Guid(RevisionComplect.InventoryNumberGuid));
  }
}
