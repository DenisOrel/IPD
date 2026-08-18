// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSColumnSchemeExtensions
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Attributes;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.AVS;

public static class AVSColumnSchemeExtensions
{
  /// <summary>
  /// Получить информацию об атрибуте-источнике для колонки схемы
  /// </summary>
  public static AttributeInfo FindColumnAttributeInfo(
    this INodeColumnScheme scheme,
    NodeColumn column)
  {
    AttributeInfo columnAttributeInfo = (AttributeInfo) null;
    if (scheme == null)
      return (AttributeInfo) null;
    if (column == null)
      return (AttributeInfo) null;
    switch (scheme)
    {
      case AVSColumnScheme avsColumnScheme:
        return avsColumnScheme.FindAttributeInfo(column);
      case ObjectColumnScheme _:
        return AVSColumnScheme.MakeSourceAttributeInfoForColumn(FieldSource.Object, column);
      case RelationColumnScheme _:
        return AVSColumnScheme.MakeSourceAttributeInfoForColumn(FieldSource.Relation, column);
      default:
        return columnAttributeInfo;
    }
  }
}
