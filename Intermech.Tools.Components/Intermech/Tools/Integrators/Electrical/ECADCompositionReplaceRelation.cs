// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADCompositionReplaceRelation
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal static class ECADCompositionReplaceRelation
{
  public static void CreateAdditionalAttributes(CompositionItem relation)
  {
    relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(new Guid("cad00654-306c-11d8-b4e9-00304f19f545")), (object) 1));
  }
}
