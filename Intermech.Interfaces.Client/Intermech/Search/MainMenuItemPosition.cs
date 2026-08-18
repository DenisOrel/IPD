// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MainMenuItemPosition
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Search;

/// <summary>Возможные позиции пунктов главного меню</summary>
public enum MainMenuItemPosition
{
  /// <summary>По умолчанию - между третими и предпоследними</summary>
  Default,
  /// <summary>Первые</summary>
  First,
  /// <summary>Вторые</summary>
  Second,
  /// <summary>Третьи</summary>
  Third,
  /// <summary>Предпоследние</summary>
  Penultimate,
  /// <summary>Последние</summary>
  Last,
}
