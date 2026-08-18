// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.IExpertUser
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Интерфейс пользовательской части ЭС</summary>
public interface IExpertUser
{
  /// <summary>Получить интерфейс задачи</summary>
  /// <returns>Интерфейс IExpertTask</returns>
  IExpertTask GetExpertTask();

  /// <summary>
  /// Узнать, будет ли показываться трассировка (установка в настройках системы)
  /// </summary>
  bool ShowTraceWindow { get; }

  /// <summary>Узнать, будет ли вестись лог на сервере</summary>
  bool ReportLog { get; }
}
