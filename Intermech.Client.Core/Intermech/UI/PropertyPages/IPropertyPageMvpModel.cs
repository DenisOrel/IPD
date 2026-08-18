
// Type: Intermech.UI.PropertyPages.IPropertyPageMvpModel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.UI.PropertyPages;

/// <summary>
/// Интерфейс MVP-модели для страниц окна настройки параметров.
/// </summary>
public interface IPropertyPageMvpModel
{
  /// <summary>
  /// Возвращает MVP-модель к исходному состоянию, отбрасывая все сделанные изменения.
  /// </summary>
  void Reset();

  /// <summary>Сохраняет все сделанные изменения, если они есть.</summary>
  void SaveChanges();
}
