// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Briefcase.MapperVariable
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Workflow.Briefcase;

public class MapperVariable : MapperObject
{
  public VarType Type;
  public StringList ValuesList = new StringList();

  public MapperVariable()
  {
  }

  public MapperVariable(IMSAttributeType t)
    : base(t.AttributeGuid, t.Name)
  {
    this.Type = MiscFunx.DetermineVarType(t);
    if (t.MultiValueMode == MultiValueModes.SingleValue)
      return;
    this.ValuesList.Clear();
    if (t.PossibleValues != null)
    {
      foreach (object possibleValue in t.PossibleValues)
        this.ValuesList.Add(possibleValue.ToString());
    }
    if ((t.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
      return;
    this.ValuesList.Insert(0, "");
  }

  public bool ShouldSerializeValuesList() => this.ValuesList.Count > 0;
}
