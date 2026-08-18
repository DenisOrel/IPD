
// Type: Intermech.Interfaces.Client.RuleNotAvailableException
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Interfaces.Client;

/// <summary>
/// Используется в тех случаях, когда источник правил подбора версий не может предоставить правило
/// по запросу.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="message">Текст сообщения об ошибке</param>
public sealed class RuleNotAvailableException(string message) : FaultException(message)
{
}
