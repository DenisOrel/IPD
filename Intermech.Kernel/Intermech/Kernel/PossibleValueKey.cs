// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.PossibleValueKey
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel;

public class PossibleValueKey
{
  public object Value { get; private set; }

  public int AttributeID { get; private set; }

  public PossibleValueKey(int attrID, object val)
  {
    this.AttributeID = attrID;
    this.Value = val;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is PossibleValueKey))
      return false;
    PossibleValueKey possibleValueKey = obj as PossibleValueKey;
    return this.AttributeID == possibleValueKey.AttributeID && this.Value.Equals(possibleValueKey.Value);
  }

  public override int GetHashCode() => this.Value.GetHashCode() ^ this.AttributeID.GetHashCode();
}
