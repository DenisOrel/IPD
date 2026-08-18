
// Type: Intermech.PropertyEditors.RelationKindPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for RelationKindEditor.</summary>
public class RelationKindPropertyClass
{
  private RelationKinds relationKind;

  public RelationKinds RelationKind => this.relationKind;

  public RelationKindPropertyClass(RelationKinds aRelationKind)
  {
    this.relationKind = aRelationKind;
  }

  public override string ToString() => RelationKindsHelper.GetCaption(this.relationKind);
}
