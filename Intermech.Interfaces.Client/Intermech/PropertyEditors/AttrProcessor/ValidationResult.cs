// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.ValidationResult
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

public struct ValidationResult(int attributeId, int index, string reason)
{
  public int AttributeId = attributeId;
  public int Index = index;
  public string Reason = reason;

  public ValidationResult(int attributeId, string reason)
    : this(attributeId, 0, reason)
  {
  }
}
