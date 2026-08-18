// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.StandardPartTypeCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.Settings;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует валидатор для свойства в настройках интегратора, хранящего тип объектов IPS для моделей стандартных изделий.
/// </summary>
public sealed class StandardPartTypeCheck : CADSettingsCheck
{
  /// <summary>
  /// Выполняет проверку свойства интегратора с типом объекта IPS для моделей стандартных изделий.
  /// </summary>
  /// <param name="settings">Объект с настройками интегратора</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>null, если проверка успешно пройдена, иначе - текст с детальным описанием проблемы</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект настроек не может быть null</exception>
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    if (settings.StandardPartType == null)
      return LocalizationHolder.rm.GetString("Tools.Components_362");
    return !DBHelper.IsBasedOnType(settings.StandardPartType.Id, IDCache.Default.StandardPartDocuments.Id) ? $"Тип документа '{settings.StandardPartType.Name}', располагаемый в группе 'Модели стандартных изделий', должен быть унаследован от типа '{IDCache.Default.StandardPartDocuments.Text}'." : (string) null;
  }
}
