// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDBRelationCollectionExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IDBRelationCollectionExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DBRelation CreateRelationCopy(
    [NotNull] this IDBRelationCollection relationCollection,
    [NotNull] IDBRelation relationPrototype,
    [NotEmpty] long projectObjectID,
    [NotEmpty] long partID,
    [NotEmpty] long partObjectID)
  {
    return relationCollection.CreateRelationCopy<DBRelation>(relationPrototype, projectObjectID, partID, partObjectID);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBRelation CreateRelationCopy<TDBRelation>(
    [NotNull] this IDBRelationCollection relationCollection,
    [NotNull] IDBRelation relationPrototype,
    [NotEmpty] long projectObjectID,
    [NotEmpty] long partID,
    [NotEmpty] long partObjectID)
    where TDBRelation : DBRelation
  {
    if (relationCollection is IServerDBRelationCollection relationCollection1)
      relationCollection1.AssignMode = 8192 /*0x2000*/;
    return relationCollection.Create(new NewRelationProperties()
    {
      BeginDate = relationPrototype.CreateDate,
      PrototypeRelation = relationPrototype,
      PrototypeRelationID = relationPrototype.RelationID,
      ProjectObjectID = projectObjectID,
      PartID = partID,
      PartObjectID = partObjectID
    }).CastInterfaceToClass<IDBRelation, TDBRelation>();
  }
}
