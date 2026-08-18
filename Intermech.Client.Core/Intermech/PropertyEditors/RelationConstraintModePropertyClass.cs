
// Type: Intermech.PropertyEditors.RelationConstraintModePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>Summary description for RelationConstraintEditor.</summary>
public class RelationConstraintModePropertyClass
{
  private RelationConstraintModes relationConstraintMode;

  public RelationConstraintModes RelationConstraintMode => this.relationConstraintMode;

  public RelationConstraintModePropertyClass(RelationConstraintModes aRelationConstraintMode)
  {
    this.relationConstraintMode = aRelationConstraintMode;
  }

  public override string ToString()
  {
    return RelationConstraintModesHelper.GetCaption(this.relationConstraintMode);
  }
}
