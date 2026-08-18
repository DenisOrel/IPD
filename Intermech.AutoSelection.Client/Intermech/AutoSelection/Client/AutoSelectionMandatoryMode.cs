// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionMandatoryMode
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.AutoSelection.Client;

[TypeConverter(typeof (EnumDescConverter))]
public enum AutoSelectionMandatoryMode
{
  [CustomDescription("Attribute.AutoSelection.Client_65")] Optional,
  [CustomDescription("Attribute.AutoSelection.Client_66")] Mandatory,
}
