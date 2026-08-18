// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.ClassifyObject.TechCardClassifyTechProcessDesignationStrategy
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Services.ClassifyObject;

/// <summary>
/// Стратегия для классификации атрибута "Обозначение" для техпроцесса
/// </summary>
public class TechCardClassifyTechProcessDesignationStrategy : ITechCardClassifyObjectStrategy
{
  /// <summary>Получение шаблона классификации атрибута</summary>
  /// <param name="session"></param>
  /// <param name="classifyParams"></param>
  /// <returns></returns>
  public string GetClassifyTemplate(
    [NotNull] IUserSession session,
    [NotNull] TechCardClassifyObjectParams classifyParams)
  {
    IDBObject dbObject = session.GetObject(classifyParams.ContextObjectItem.ObjectID, false);
    if (dbObject == null)
      return string.Empty;
    string str1 = dbObject.GetAttributeByID(TechCardConsts.AttributeTypes.DesignationAttrTypeID)?.AsString ?? string.Empty;
    if (string.IsNullOrEmpty(str1))
      str1 = dbObject.Caption;
    if (str1.Equals(string.Empty))
    {
      UniqueValueModes uniqueValueModes = UniqueValueModes.NotUnique;
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(classifyParams.ClassifyObjectItem.ObjTypeID, TechCardConsts.AttributeTypes.DesignationAttrTypeID);
      if (attribute4ObjectType != null)
      {
        uniqueValueModes = attribute4ObjectType.Unique;
      }
      else
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.DesignationAttrTypeID);
        if (attributeType != null)
          uniqueValueModes = attributeType.Unique;
      }
      switch (uniqueValueModes)
      {
        case UniqueValueModes.TypeOnly:
        case UniqueValueModes.VerTypeOnly:
        case UniqueValueModes.AllVerTypes:
          str1 = string.Format(LocalizationHolder.rm.GetString("TechCard.Client_371"), (object) dbObject.ObjectID);
          break;
      }
    }
    if (string.IsNullOrEmpty(str1))
      return string.Empty;
    string str2 = string.Empty;
    IEnumerable<AttributeValues> attributeValues1 = classifyParams.AttributeValues;
    AttributeValues attributeValues2 = attributeValues1 != null ? attributeValues1.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.ProductionAttrID)) : (AttributeValues) null;
    if (attributeValues2 != null)
    {
      IDBAttribute objectAttributeById = session.GetObjectAttributeByID(attributeValues2.AsInteger, MetaDataHelper.GetAttributeID((object) "cad00005-306c-11d8-b4e9-00304f19f545"));
      if (objectAttributeById != null)
        str2 = objectAttributeById.AsString;
    }
    if (string.IsNullOrEmpty(str1))
      return string.Empty;
    return $"{str1} {str2}{"<%obj_no%>"} {TechCardClassifyObjectService.GetObjectTypePostfix(classifyParams.ClassifyObjectItem.ObjTypeID)}";
  }
}
