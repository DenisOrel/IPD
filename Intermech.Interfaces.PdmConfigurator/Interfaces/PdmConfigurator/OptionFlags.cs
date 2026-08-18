// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OptionFlags
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Набор флажков, назначенных опции</summary>
[Flags]
[Serializable]
public enum OptionFlags : long
{
  /// <summary>Никаких флажков у опции нет</summary>
  None = 0,
  /// <summary>
  /// Опция устарела, её нельзя использовать в новых объектах
  /// </summary>
  Obsolete = 1,
}
