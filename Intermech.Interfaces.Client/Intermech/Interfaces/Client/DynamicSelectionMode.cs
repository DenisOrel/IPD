// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DynamicSelectionMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Режим выбора объекта</summary>
public enum DynamicSelectionMode
{
  /// <summary>Производится проверка выбранного объекта</summary>
  PreSelect,
  /// <summary>Объект выбран и обработан ядром</summary>
  Select,
}
