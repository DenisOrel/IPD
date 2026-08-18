// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.WindowSettingsBase
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Navigator.Windows;

/// <summary>Базовый класс для настроек окон навигатора</summary>
[Serializable]
public abstract class WindowSettingsBase : ICloneable
{
  /// <summary>Ширина дерева</summary>
  public int TreeWidth { get; set; }

  /// <summary>Колонки дерева</summary>
  public NodeColumnCollection TreeColumns { get; set; }

  public abstract object Clone();
}
