// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.AutoSelectionMode
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.AutoSelection;

/// <summary>Attribute copy mode</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum AutoSelectionMode
{
  /// <summary>Manual selection mode</summary>
  [CustomDescription("Attribute.Interfaces.AutoSelection_1")] Manual,
  /// <summary>Auto selection mode for object</summary>
  [CustomDescription("Attribute.Interfaces.AutoSelection_2")] AutoObject,
  /// <summary>Auto selection mode for relation</summary>
  [CustomDescription("Attribute.Interfaces.AutoSelection_3")] AutoRelation,
  /// <summary>Proceed for all modes</summary>
  [Browsable(false)] All,
}
