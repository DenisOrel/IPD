// Decompiled with JetBrains decompiler
// Type: Intermech.Signs.Interfaces.ISignsClientService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Signs.Interfaces;

/// <summary>Интерфейс для доступа к клиентской службе подписей</summary>
public interface ISignsClientService
{
  /// <summary>
  /// Возвращает массив граф, в которых текущий пользователь может подписать объект objectID. Если objectID == 0, то возвращает весь список граф для данного юзера от всех его должностей.
  /// </summary>
  /// <param name="objectID">Ид. версии объекта.</param>
  /// <returns>Массив граф в виде строкового идентификатора графы и его расшифровки.</returns>
  Tuple<string, string>[] GetUserGraphs(long objectID);

  /// <summary>
  /// Предлагает юзеру выбрать в каких графах и должностях нужно подписывать или создавать замечания для объекта objectID.
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта.</param>
  /// <returns>Массив с информацией о выбранных должностях и графах. Массив пустой, если юзер ничего не выбрал или отменил выбор.</returns>
  RankGraphsInfo[] ShowUserGraphsDialog(long objectID);
}
