// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBObjectEntityParserData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBObjectEntityParserData(Type entityType) : DBEntityParserData(DBEntityKind.Object, entityType)
{
  public Guid DBObjectTypeGuid { get; set; }

  public DBObjectTypeMapping DBObjectType { get; set; }

  public Dictionary<string, DataPropertyMapping> DataPropertiesMappings { get; set; }

  public DataPropertyParserData KeyProperty { get; set; }

  public Dictionary<string, DBObjectNavigationPropertyParserData> NavigationPropertiesParserData { get; set; }

  public Dictionary<string, NavigationPropertyDescriptor> NavigationPropertiesDescriptors { get; set; }

  public Dictionary<string, DBObjectNavigationPropertyMapping> NavigationPropertiesMappings { get; set; }
}
