// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.GroupInstanceHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// 
/// </summary>
public class GroupInstanceHelper
{
  /// <summary>
  /// Проверяет объект на проведение операций с групповыми изделиями
  /// </summary>
  /// <param name="session"></param>
  /// <param name="createdObject"></param>
  /// <returns></returns>
  public static CreateGroupInstanceType ProcessingEnable(
    IUserSession session,
    IDBObject createdObject)
  {
    CreateGroupInstanceType groupInstanceType = CreateGroupInstanceType.None;
    if (createdObject.VersionID > 0)
    {
      IDBAttribute attributeByGuid = createdObject.GetAttributeByGuid(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"));
      if (ObjectTypesCacheHelper.GetRootType(createdObject.ObjectType) == MetaDataHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545") && attributeByGuid != null && GuidHelper.IsGuid(attributeByGuid.AsString))
        return CreateGroupInstanceType.ArticleVersion;
      IDBObjectType objectType = session.GetObjectType(new Guid("cad00133-306c-11d8-b4e9-00304f19f545"));
      if (session.GetRelationsApplicabilityCollection().GetApplicability(MetaDataHelper.GetRelationType(new Guid("cad00154-306c-11d8-b4e9-00304f19f545")).RelationTypeID, objectType.ObjectType, createdObject.ObjectType) != null)
        groupInstanceType = CreateGroupInstanceType.ArticleVersion;
      else if (createdObject.ObjectType == objectType.ObjectType)
        groupInstanceType = CreateGroupInstanceType.Specification;
    }
    return groupInstanceType;
  }
}
