
// Type: Intermech.PropertyEditors.RelationTypePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for RelationTypeEditor.</summary>
public class RelationTypePropertyClass
{
  private int relationType;

  public int RelationType => this.relationType;

  public RelationTypePropertyClass(int aRelationType) => this.relationType = aRelationType;

  public override string ToString()
  {
    return DataHolders.RelationTypesHolder.GetNamebyID(this.RelationType);
  }
}
