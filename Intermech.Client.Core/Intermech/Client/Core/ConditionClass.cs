
// Type: Intermech.Client.Core.ConditionClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core;

/// <summary>Summary description for ConditionComboBoxClass.</summary>
public class ConditionClass
{
  private FlagsConditions fc;

  public FlagsConditions FlagCondition => this.fc;

  public ConditionClass(FlagsConditions afc) => this.fc = afc;

  public override string ToString() => FlagsConditionsHelper.GetCaption(this.fc);
}
