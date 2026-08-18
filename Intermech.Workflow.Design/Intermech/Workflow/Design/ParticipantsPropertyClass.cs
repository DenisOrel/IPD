// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ParticipantsPropertyClass
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Workflow.Design;

internal class ParticipantsPropertyClass
{
  private string _value;

  public string Value
  {
    get => this._value;
    set => this._value = value;
  }

  public ParticipantsPropertyClass(string val) => this._value = val;

  public override string ToString()
  {
    if (this._value == "")
      return LocalizationHolder.rm.GetString("EmptyMsg");
    try
    {
      return new ParticipantList()
      {
        AsString = this._value
      }.ToUserString();
    }
    catch
    {
      return this._value;
    }
  }
}
