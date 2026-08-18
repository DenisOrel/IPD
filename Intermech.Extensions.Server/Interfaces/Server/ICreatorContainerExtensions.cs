// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ICreatorContainerExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public static class ICreatorContainerExtensions
{
  public static void RegisterCreator(
    [NotNull] this ICreatorContainer creatorContainer,
    [NotNull] IDBObjectCreator objectCreator,
    [NotNull, NotEmpty, ItemNotEmpty] params int[] objectTypeIDs)
  {
    foreach (int objectTypeId in objectTypeIDs)
    {
      creatorContainer.AddCreator((object) objectTypeId, (object) objectCreator);
      Guid objectTypeGuid = MetaDataHelperService.Instance.GetObjectTypeGuid(objectTypeId);
      creatorContainer.AddCreator((object) objectTypeGuid, (object) objectCreator);
    }
  }

  public static void RegisterCreator(
    [NotNull] this ICreatorContainer creatorContainer,
    [NotNull] IDBObjectCreator objectCreator,
    [NotNull, NotEmpty, ItemNotEmpty] params Guid[] objectTypeGuids)
  {
    foreach (Guid objectTypeGuid in objectTypeGuids)
    {
      creatorContainer.AddCreator((object) objectTypeGuid, (object) objectCreator);
      int objectTypeId = MetaDataHelperService.Instance.GetObjectTypeID(objectTypeGuid);
      creatorContainer.AddCreator((object) objectTypeId, (object) objectCreator);
    }
  }

  public static void RegisterCollectionCreator(
    [NotNull] this ICreatorContainer creatorContainer,
    [NotNull] IDBObjectCollectionCreator objectCollectionCreator,
    [NotNull, NotEmpty, ItemNotEmpty] params int[] objectTypeIDs)
  {
    foreach (int objectTypeId in objectTypeIDs)
    {
      creatorContainer.AddCreator((object) objectTypeId, (object) objectCollectionCreator);
      Guid objectTypeGuid = MetaDataHelperService.Instance.GetObjectTypeGuid(objectTypeId);
      creatorContainer.AddCreator((object) objectTypeGuid, (object) objectCollectionCreator);
    }
  }

  public static void RegisterCollectionCreator(
    [NotNull] this ICreatorContainer creatorContainer,
    [NotNull] IDBObjectCollectionCreator objectCollectionCreator,
    [NotNull, NotEmpty, ItemNotEmpty] params Guid[] objectTypeGuids)
  {
    foreach (Guid objectTypeGuid in objectTypeGuids)
    {
      creatorContainer.AddCreator((object) objectTypeGuid, (object) objectCollectionCreator);
      int objectTypeId = MetaDataHelperService.Instance.GetObjectTypeID(objectTypeGuid);
      creatorContainer.AddCreator((object) objectTypeId, (object) objectCollectionCreator);
    }
  }
}
