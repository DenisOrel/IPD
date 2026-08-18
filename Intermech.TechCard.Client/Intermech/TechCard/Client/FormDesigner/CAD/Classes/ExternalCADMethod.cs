// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.FormDesigner.CAD.Classes.ExternalCADMethod
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Client.FormDesigner.CAD.Classes;

/// <summary>List of all availabled CAD methods</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum ExternalCADMethod
{
  /// <summary>Undefined</summary>
  [CustomDescription("Attribute.TechCard.Client_4")] undefined,
  /// <summary>Common params</summary>
  [CustomDescription("Attribute.TechCard.Client_5")] CommonAttributes,
  /// <summary>Face params</summary>
  [CustomDescription("Attribute.TechCard.Client_6")] FaceAttributes,
}
