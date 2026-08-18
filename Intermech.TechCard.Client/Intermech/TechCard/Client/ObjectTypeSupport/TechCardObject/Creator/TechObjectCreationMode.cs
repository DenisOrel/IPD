// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechObjectCreationMode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>Creation mode of techcard object</summary>
public enum TechObjectCreationMode
{
  /// <summary>Creation by Imbase</summary>
  Imbase,
  /// <summary>Creation By Custom forms</summary>
  CustomForm,
  /// <summary>Default object creator</summary>
  Default,
}
