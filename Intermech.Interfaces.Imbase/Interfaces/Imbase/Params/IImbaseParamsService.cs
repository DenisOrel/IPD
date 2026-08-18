// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.IImbaseParamsService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params;

/// <summary>Сервис для работы со всеми параметрами Imbase</summary>
public interface IImbaseParamsService
{
  /// <summary>Общие настройки</summary>
  ImbaseCommonParams CommonParams { get; }

  /// <summary>Пользовательские настройки</summary>
  /// <param name="sessionGuid"></param>
  /// <returns></returns>
  ImbaseUserParams GetUserParams(Guid sessionGuid);

  /// <summary>Сохранить пользовательские настройки</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="userParams"></param>
  void SetUserParams(Guid sessionGuid, ImbaseUserParams userParams);

  /// <summary>Сохранить общие настройки</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="commonParams"></param>
  void SetCommonParams(Guid sessionGuid, ImbaseCommonParams commonParams);

  /// <summary>Перечитать настройки</summary>
  /// <param name="session"></param>
  /// <param name="info"></param>
  void ResetSettings(IUserSession session, string info);
}
