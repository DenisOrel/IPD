// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.SelectionDataSource
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Источник данных для выборки</summary>
public enum SelectionDataSource
{
  /// <summary>Неизвестно</summary>
  Unknown,
  /// <summary>База данных IPS</summary>
  DataBase,
  /// <summary>Портал</summary>
  Portal,
}
