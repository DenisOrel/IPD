// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.AutoSelectionCache.AutoSelectionLinkMode
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using System;

#nullable disable
namespace Intermech.Interfaces.AutoSelection.AutoSelectionCache;

/// <summary>Autoselection rule's link mode</summary>
[Serializable]
public enum AutoSelectionLinkMode
{
  /// <summary>Link with object type</summary>
  asotObjectType,
  /// <summary>Link with imbase object</summary>
  asotImbaseObject,
}
