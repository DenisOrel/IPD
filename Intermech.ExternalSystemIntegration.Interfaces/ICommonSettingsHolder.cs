// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.ICommonSettingsHolder
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

using System;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

/// <summary>Сервис хранения общих настроек</summary>
public interface ICommonSettingsHolder
{
  /// <summary>Директория для входящих файлов</summary>
  string InputFiles { get; set; }

  /// <summary>Директория для сохранения файлов</summary>
  string OutputFiles { get; set; }

  /// <summary>Директория для обработанных файлов</summary>
  string DoneFiles { get; set; }

  /// <summary>Директория для некорректных файлов</summary>
  string ErrorFiles { get; set; }

  /// <summary>Прочитать настройки</summary>
  /// <param name="sessionGuid"></param>
  void ReadSettings(Guid sessionGuid);

  /// <summary>Записать настройки</summary>
  /// <param name="sessionGuid"></param>
  void WriteSettings(Guid sessionGuid);
}
