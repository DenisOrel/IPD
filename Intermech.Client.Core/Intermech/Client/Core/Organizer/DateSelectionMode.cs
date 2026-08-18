
// Type: Intermech.Client.Core.Organizer.DateSelectionMode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Organizer;

/// <summary>Способ выделения дней месяца.</summary>
public enum DateSelectionMode
{
  /// <summary>Выделение ограниченное одним днем.</summary>
  Days,
  /// <summary>Выделение ограниченное полным месяцем.</summary>
  Month,
  /// <summary>Выделение ограниченное полной неделей.</summary>
  Week,
  /// <summary>Выделение ограниченное рабочей неделей.</summary>
  WorkWeek,
}
