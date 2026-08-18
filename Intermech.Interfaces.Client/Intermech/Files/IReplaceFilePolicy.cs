// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IReplaceFilePolicy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Позволяет реализовать политику перезаписи файлов объекта содержимым из базы IPS. Политика применяется к
/// локальным файлам, которые являются более свежими по сравнению с файлами объекта в базе IPS. Как правило,
/// такая ситуация имеет место, если пользователь редактирует файлы в обход IPS.
/// </summary>
public interface IReplaceFilePolicy
{
  /// <summary>Применяет политику.</summary>
  /// <param name="workArea">Ссылка на объект рабочей области файлового хранилища</param>
  /// <param name="dbObject">Состояние объекта в базе IPS</param>
  /// <param name="workObject">Состояние объекта в рабочей области</param>
  /// <param name="askUserPairs">Пары состояний файлов, к которым применяется политика</param>
  /// <returns>Список пар состояний файлов после применения политики</returns>
  List<FileDifferencePair> Apply(
    IWorkArea workArea,
    DBObjectState dbObject,
    DBObjectState workObject,
    List<FileDifferencePair> askUserPairs);
}
