// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DrawingSuffixesCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Tools.Settings;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует валидатор для списка суффиксов имен файлов чертежей.
/// </summary>
public sealed class DrawingSuffixesCheck : CADSettingsCheck
{
  /// <summary>
  /// Выполняет проверку списка суффиксов имен файлов чертежей.
  /// </summary>
  /// <param name="settings">Объект с настройками интегратора</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>null, если проверка успешно пройдена, иначе - текст с детальным описанием проблемы</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект настроек не может быть null</exception>
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    if (settings.DrawingSuffixes.Count > 0)
    {
      char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
      foreach (string drawingSuffix in settings.DrawingSuffixes)
      {
        string str = drawingSuffix?.Trim();
        if (string.IsNullOrEmpty(str))
          return LocalizationHolder.rm.GetString("Tools.Components_518");
        if (str.Length > 15)
          return LocalizationHolder.rm.GetString("Tools.Components_519");
        if (str.IndexOfAny(invalidFileNameChars) >= 0)
          return string.Format(LocalizationHolder.rm.GetString("Tools.Components_520"), (object) str);
      }
    }
    return (string) null;
  }
}
