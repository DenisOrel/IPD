
// Type: Intermech.Client.Core.FormDesigner.Actions.FormDesignerActionExtentions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Interfaces;


namespace Intermech.Client.Core.FormDesigner.Actions;

/// <summary>
/// Класс для методов расширения действий, назначаемых на кнопку.
/// </summary>
public static class FormDesignerActionExtentions
{
  /// <summary>Применить.</summary>
  /// <param name="action">Действие</param>
  /// <param name="form">Форма редактирования</param>
  public static void Apply(this IFormDesignerActionHandler action, object form)
  {
    if (!(form is DesForm desForm) || !desForm.Modified)
      return;
    desForm.SaveAttributes();
  }

  /// <summary>Отмена.</summary>
  /// <param name="action">Действие</param>
  /// <param name="form">Форма редактирования</param>
  public static void Cancel(this IFormDesignerActionHandler action, object form)
  {
    if (!(form is DesForm desForm))
      return;
    desForm.LoadAttributes(RefreshMode.Forced);
    desForm.IncludedClassificators.Clear();
  }
}
