// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ComponentSelectionHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

public static class ComponentSelectionHelper
{
  /// <summary>
  /// Является ли связь связью с основным компонентом для подбора
  /// </summary>
  public static bool IsMainComponent(IDBRelation relation, out string posDesignation)
  {
    IDBAttribute attributeByGuid1 = relation.GetAttributeByGuid(ComponentSelectionConsts.attributeReplace);
    if (attributeByGuid1 == null || !attributeByGuid1.AsBoolean)
    {
      posDesignation = string.Empty;
      return false;
    }
    IDBAttribute attributeByGuid2 = relation.GetAttributeByGuid(new Guid("cad01478-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid2 != null && !string.IsNullOrEmpty(attributeByGuid2.AsString))
    {
      posDesignation = attributeByGuid2.AsString;
      return true;
    }
    posDesignation = string.Empty;
    return false;
  }
}
