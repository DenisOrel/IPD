// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionFolderType
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.AutoSelection.Client;

[TypeConverter(typeof (EnumDescConverter))]
public enum AutoSelectionFolderType
{
  [CustomDescription("Attribute.AutoSelection.Client_76")] SimpleFolder,
  [CustomDescription("Attribute.AutoSelection.Client_77")] SelectFolder,
  [CustomDescription("Attribute.AutoSelection.Client_78")] DialogFolder,
  [CustomDescription("Attribute.AutoSelection.Client_79")] MultiSelectFolder,
  [CustomDescription("Attribute.AutoSelection.Client_86")] SlideFolder,
}
