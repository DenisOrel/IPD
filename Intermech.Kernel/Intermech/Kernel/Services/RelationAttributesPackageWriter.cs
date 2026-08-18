// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.RelationAttributesPackageWriter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services;

public class RelationAttributesPackageWriter : LongLifeObject, IRelationAttributesPackageWriter
{
  protected virtual IDBAttribute TryToAddAttribute(
    IUserSession session,
    IDBRelation relation,
    int attrID,
    object newValue)
  {
    if (session == null || relation == null || attrID < 0)
      return (IDBAttribute) null;
    IDBAttribute attributeById1 = relation.GetAttributeByID(attrID);
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
    try
    {
      bool flag = false;
      if (attributeType != null && (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList))
        flag = !(newValue is object[] objArray) || objArray.Length == 0;
      if (((newValue == null ? 1 : (newValue == DBNull.Value ? 1 : 0)) | (flag ? 1 : 0)) != 0)
      {
        if (attributeById1 == null || !((session.GetRelationType(relation.TypeID).Attributes as IDBAttribute4RelationTypeCollection).GetAttributeByID(attrID) is IDBAttributeType4Relation attributeById2))
          return (IDBAttribute) null;
        if (attributeById2.Required == RequiredModes.Manual)
        {
          attributeById1.Delete(0L);
          return (IDBAttribute) null;
        }
      }
      return attributeById1 != null || newValue == null ? attributeById1 : relation.Attributes.AddAttribute(attrID, false);
    }
    catch
    {
      return (IDBAttribute) null;
    }
  }

  public virtual bool WriteRelationAttributesPackage(
    Guid sessionID,
    RelationAttributesPackage package,
    out List<long> chRels)
  {
    bool flag1 = false;
    chRels = new List<long>();
    if (package == null || package.Values.Count == 0 || package.Attributes.Count == 0)
      return flag1;
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    bool flag2 = package.WriteableAttributes != null && package.WriteableAttributes.Count > 0;
    foreach (KeyValuePair<long, object[]> keyValuePair in package.Values)
    {
      IDBRelation relation = sessionById.GetRelation(keyValuePair.Key, false);
      if (!chRels.Contains(keyValuePair.Key))
        chRels.Add(keyValuePair.Key);
      for (int index = 0; index < keyValuePair.Value.Length; ++index)
      {
        if (!flag2 || package.WriteableAttributes.Contains(package.Attributes[index]))
        {
          IDBAttribute addAttribute = this.TryToAddAttribute(sessionById, relation, package.Attributes[index], keyValuePair.Value[index]);
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(package.Attributes[index]);
          if (addAttribute != null && attributeType != null)
          {
            if (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList)
            {
              if (!(keyValuePair.Value[index] is object[] objArray) || objArray.Length == 0)
                addAttribute.ClearValues();
              else
                addAttribute.Values = objArray;
            }
            else
              addAttribute.Value = keyValuePair.Value[index];
          }
        }
      }
    }
    return flag1;
  }

  public bool WriteRelationAttributesPackages(
    Guid sessionID,
    Dictionary<long, RelationAttributesPackage> packages,
    out List<long> chRels)
  {
    chRels = new List<long>();
    if (packages == null || packages.Count == 0)
      return false;
    UserSession.GetSessionByID(sessionID);
    foreach (KeyValuePair<long, RelationAttributesPackage> package in packages)
    {
      List<long> chRels1;
      this.WriteRelationAttributesPackage(sessionID, package.Value, out chRels1);
      if (chRels1 != null)
      {
        for (int index = 0; index < chRels1.Count; ++index)
        {
          if (!chRels.Contains(chRels1[index]))
            chRels.Add(chRels1[index]);
        }
      }
    }
    chRels.Sort();
    return chRels.Count > 0;
  }
}
