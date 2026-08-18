// Decompiled with JetBrains decompiler
// Type: Intermech.Signs.Interfaces.RankGraphsInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Signs.Interfaces;

/// <summary>
/// Класс для хранения результатов выбора пользователем должности и граф для подписей в данной должности
/// </summary>
[Serializable]
public class RankGraphsInfo
{
  private long _rankID;
  private string _rankCaption;
  private Tuple<string, string>[] _graphs;

  /// <summary>Конструктор</summary>
  /// <param name="rankID">ObjectID должности</param>
  /// <param name="rankCaption">Заголовок должности</param>
  /// <param name="graphs">Список граф для подписи</param>
  public RankGraphsInfo(long rankID, string rankCaption, Tuple<string, string>[] graphs)
  {
    this._graphs = graphs;
    this._rankCaption = rankCaption;
    this._rankID = rankID;
  }

  /// <summary>ObjectID должности</summary>
  public long RankID => this._rankID;

  /// <summary>Заголовок должности</summary>
  public string RankCaption => this._rankCaption;

  /// <summary>
  /// Массив граф для подписи в данной должности (строковый ид. графы, строковая расшифровка графы)
  /// </summary>
  public Tuple<string, string>[] Graphs => this._graphs;
}
