// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.ImsRelationTypeExtension
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard;

public static class ImsRelationTypeExtension
{
  /// <summary>
  /// Фильтрация атрибутов, у которых установлен режим копирования (точнее отсутствует запрет)
  /// </summary>
  /// <param name="imsRelationType"></param>
  /// <param name="attributeIds"></param>
  public static void FilterCopyAttributes(
    this IMSRelationType imsRelationType,
    [NotNull] IList<int> attributeIds)
  {
    for (int index = attributeIds.Count - 1; index >= 0; --index)
    {
      int attributeId = attributeIds[index];
      IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(imsRelationType.RelationTypeID, attributeId);
      if (attribute4RelationType != null)
      {
        if (attribute4RelationType.Options.HasFlag((Enum) AttributeOptions.DontCopyPrototypeValue))
          attributeIds.RemoveAt(index);
      }
      else
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeId);
        if (attributeType != null && attributeType.Options.HasFlag((Enum) AttributeOptions.DontCopyPrototypeValue))
          attributeIds.RemoveAt(index);
      }
    }
  }
}
