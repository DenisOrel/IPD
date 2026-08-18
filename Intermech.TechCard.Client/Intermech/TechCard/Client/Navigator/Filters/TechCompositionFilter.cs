// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Filters.TechCompositionFilter
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Parts;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Filters;

public abstract class TechCompositionFilter : ITechCompositionFilter
{
  public abstract void UpdateRelatedObjectsRole(RelatedObjectsRole role);

  public abstract bool CallBaseMethod(
    TechCompositionPart nodePart,
    ref ConditionStructure[] conditions);

  public abstract INodeQuery GetCustomQuery(
    TechCompositionPart nodePart,
    ConditionStructure[] conditions);

  public IRelatedObjectQueryFilterMode QueryFilter { get; set; }
}
