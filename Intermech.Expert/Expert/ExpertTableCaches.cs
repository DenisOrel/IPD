// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertTableCaches
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

public sealed class ExpertTableCaches
{
  private static Dictionary<long, AttributeTypeHolder> _attrCache = new Dictionary<long, AttributeTypeHolder>();
  private static Dictionary<long, ObjectTypeHolder> _objCache = new Dictionary<long, ObjectTypeHolder>();

  public static AttributeTypeHolder GetAttrHolder(long attrId)
  {
    return ExpertTableCaches._attrCache.ContainsKey(attrId) ? ExpertTableCaches._attrCache[attrId] : (AttributeTypeHolder) null;
  }

  public static ObjectTypeHolder GetObjHolder(long objTypeId)
  {
    return ExpertTableCaches._objCache.ContainsKey(objTypeId) ? ExpertTableCaches._objCache[objTypeId] : (ObjectTypeHolder) null;
  }

  public static bool HasAttrHolder(long attrId) => ExpertTableCaches._attrCache.ContainsKey(attrId);

  public static bool HasObjHolder(long objTypeId)
  {
    return ExpertTableCaches._objCache.ContainsKey(objTypeId);
  }

  public static void AddAttrHolder(long attrId, AttributeTypeHolder ath)
  {
    if (ExpertTableCaches._attrCache.ContainsKey(attrId))
      return;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType((int) attrId);
    if (attributeType != null && ath.FieldTypes != attributeType.FieldType)
      ath.FieldTypes = attributeType.FieldType;
    ExpertTableCaches._attrCache.Add(attrId, ath);
  }

  public static void AddObjHolder(long objTypeId, ObjectTypeHolder oth)
  {
    if (ExpertTableCaches._objCache.ContainsKey(objTypeId))
      return;
    ExpertTableCaches._objCache.Add(objTypeId, oth);
  }
}
