// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IndexerEventArgs
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Аргументы события от индексатора</summary>
public sealed class IndexerEventArgs : EventArgs
{
  /// <summary>Состояние индексатора</summary>
  public readonly IndexerState IndexerState;
  /// <summary>
  /// Флаг можно установить в true, чтобы прервать процесс работы индексатора
  /// </summary>
  public bool Cancelled;

  /// <summary>Создать аргументы события от индексатора</summary>
  /// <param name="indexerState">Состояние индексатора</param>
  public IndexerEventArgs(IndexerState indexerState) => this.IndexerState = indexerState;
}
