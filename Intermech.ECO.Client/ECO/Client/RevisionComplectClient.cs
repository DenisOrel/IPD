// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevisionComplectClient
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.ECO.Client;

internal class RevisionComplectClient : ICommandTarget, ICommandsProvider
{
  private static RevisionComplectClient inst;
  public static Guid RevisionComplect_TypeGuid = new Guid("cadd9522-306c-11d8-b4e9-00304f19f545");
  public static Guid RevisionComplectRelation_TypeGuid = new Guid("cadd9523-306c-11d8-b4e9-00304f19f545");
  public static Guid Revision_TypeGuid = new Guid("cad00348-306c-11d8-b4e9-00304f19f545");
  public static readonly string InventoryNumberGuid = "cadd935b-306c-11d8-b4e9-00304f19f545";

  public static RevisionComplectClient Instance
  {
    get
    {
      if (RevisionComplectClient.inst == null)
        RevisionComplectClient.inst = new RevisionComplectClient();
      return RevisionComplectClient.inst;
    }
  }

  public static void Load(IServiceProvider serviceProvider)
  {
    if (serviceProvider.GetService(typeof (ICommandManager)) is ICommandManager service1)
      service1.AddTarget((ICommandTarget) RevisionComplectClient.Instance);
    IFactory service2 = (IFactory) serviceProvider.GetService(typeof (IFactory));
    service2.AddCommandsProvider(1, RevisionComplectClient.RevisionComplect_TypeId, (ICommandsProvider) RevisionComplectClient.Instance);
    service2.AddCommandsProvider(1, RevisionComplectClient.Revision_TypeId, (ICommandsProvider) new RevisionContextProvider());
  }

  bool ICommandTarget.Execute(ICommandState commandState) => false;

  bool ICommandTarget.QueryStatus(ICommandState commandState) => false;

  CommandsInfo ICommandsProvider.GetMergedCommands(
    ISelectedItems items,
    IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  CommandsInfo ICommandsProvider.GetGroupCommands(
    ISelectedItems items,
    IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public static int RevisionComplect_TypeId
  {
    get => MetaDataHelper.GetObjectTypeID(RevisionComplectClient.RevisionComplect_TypeGuid);
  }

  public static int RevisionComplectRelation_TypeId
  {
    get
    {
      return MetaDataHelper.GetRelationTypeID(RevisionComplectClient.RevisionComplectRelation_TypeGuid);
    }
  }

  public static int Revision_TypeId
  {
    get => MetaDataHelper.GetObjectTypeID(RevisionComplectClient.Revision_TypeGuid);
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
    get
    {
      return MetaDataHelper.GetAttributeID((object) new Guid(RevisionComplectClient.InventoryNumberGuid));
    }
  }
}
