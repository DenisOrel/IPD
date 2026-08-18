// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBModelErrors
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Kernel.Entities;

public class DBModelErrors
{
  public const int InvalidModelConfiguration = 1;
  public const int InvalidEntityType = 2;
  public const int InvalidEntityProperty = 3;
  public const int InvalidEntityTypeDefinition = 4;
  public const int MissingDBObjectTypeMapping = 5;
  public const int DuplicateDBObjectTypeMapping = 6;
  public const int MissingEntityDefaultCtor = 7;
  public const int MissingDataPropertyMapping = 8;
  public const int DuplicateDataPropertyMapping = 9;
  public const int InvalidDataPropertyType = 10;
  public const int MissingKeyProperty = 11;
  public const int DuplicateKeyProperty = 12;
  public const int MissingDBRelationMapping = 13;
  public const int MissingChildOccurenceDefaultCtor = 14;
  public const int InvalidNavigationPropertyType = 15;
}
