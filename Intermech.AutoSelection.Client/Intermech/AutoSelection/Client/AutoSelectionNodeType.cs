// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNodeType
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.AutoSelection.Client;

[TypeConverter(typeof (EnumDescConverter))]
public enum AutoSelectionNodeType
{
  [CustomDescription("Attribute.AutoSelection.Client_70")] None = -1, // 0xFFFFFFFF
  [CustomDescription("Attribute.AutoSelection.Client_71")] ItemImbase = 0,
  [CustomDescription("Attribute.AutoSelection.Client_72")] ItemObject = 1,
  [CustomDescription("Attribute.AutoSelection.Client_73")] Folder = 2,
  [CustomDescription("Attribute.AutoSelection.Client_74")] Question = 3,
  [CustomDescription("Attribute.AutoSelection.Client_75")] ProcCall = 4,
  [CustomDescription("Attribute.AutoSelection.Client_96")] ScriptCall = 5,
  [CustomDescription("Attribute.AutoSelection.Client_98")] FillAttributes = 6,
}
