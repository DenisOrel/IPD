// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IndexerState
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Текущее состояние индексатора</summary>
public enum IndexerState
{
  /// <summary>Индексатор простаивает</summary>
  Idle,
  /// <summary>Обрабатывает объекты</summary>
  Objects,
  /// <summary>Обрабатывает связи</summary>
  Relations,
  /// <summary>Обрабатывает атрибуты</summary>
  Attributes,
  /// <summary>Работа индексатора прервана</summary>
  Cancelled,
  /// <summary>Работа индексатора завершена</summary>
  Completed,
}
