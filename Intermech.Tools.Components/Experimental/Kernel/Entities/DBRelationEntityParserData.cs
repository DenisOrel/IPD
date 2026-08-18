// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBRelationEntityParserData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBRelationEntityParserData(Type childOccurenceType) : DBEntityParserData(DBEntityKind.Relation, childOccurenceType)
{
  public DataPropertyParserData KeyProperty { get; set; }

  public DataPropertyParserData GuidProperty { get; set; }

  public Dictionary<string, DBRelationNavigationPropertyParserData> NavigationPropertiesParserData { get; set; }

  public DBRelationNavigationPropertyParserData RelationStartProperty { get; set; }

  public DBRelationNavigationPropertyParserData RelationEndProperty { get; set; }
}
