// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertTableFoundationClass
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces.Expert;

#nullable disable
namespace Intermech.Expert.Server;

internal class ExpertTableFoundationClass
{
  private CalcAttrPair _calcAttrPair;
  private ExpertTableFoundationEnum _foundation;
  private object _value;

  public ExpertTableFoundationClass(
    CalcAttrPair calcAttrPair,
    ExpertTableFoundationEnum foundation,
    object value)
  {
    this._calcAttrPair = calcAttrPair;
    this._foundation = foundation;
    this._value = value;
  }

  public CalcAttrPair CalcAttrPair => this._calcAttrPair;

  public ExpertTableFoundationEnum Foundation
  {
    get => this._foundation;
    set => this._foundation = value;
  }

  public object Value
  {
    get => this._value;
    set => this._value = value;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is ExpertTableFoundationClass))
      return base.Equals(obj);
    ExpertTableFoundationClass tableFoundationClass = obj as ExpertTableFoundationClass;
    return tableFoundationClass._calcAttrPair.Equals((object) this._calcAttrPair) && tableFoundationClass._foundation.Equals((object) this._foundation);
  }

  public override int GetHashCode() => base.GetHashCode();
}
