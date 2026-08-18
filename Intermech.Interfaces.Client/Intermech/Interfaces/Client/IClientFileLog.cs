// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IClientFileLog
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Потокобезопасный файловый протокол для клиентского приложения
/// </summary>
public interface IClientFileLog
{
  /// <summary>Является ли протокол открытым</summary>
  bool IsLogOpen { get; }

  /// <summary>Закрыть файл с протоколом</summary>
  void CloseLog();

  /// <summary>Открыть файл с протоколом (путь и имя - по умолчанию)</summary>
  void OpenLog();

  /// <summary>Открыть файл с протоколом</summary>
  /// <param name="logFilePath">Путь к файлу протокола</param>
  /// <param name="logFileName">Имя файла с протоколом</param>
  void OpenLog(string logFilePath, string logFileName);

  /// <summary>Записать в протокол указанный текст</summary>
  /// <param name="text">Текст</param>
  /// <param name="withDateTime">true - в начало строки добавить текущие дату и время (UTC)</param>
  void Write(string text, bool withDateTime);

  /// <summary>Записать в протокол указанный текст</summary>
  /// <param name="text">Текст</param>
  /// <param name="withDateTime">true - в начало строки добавить текущие дату и время (UTC)</param>
  void WriteLn(string text, bool withDateTime);
}
