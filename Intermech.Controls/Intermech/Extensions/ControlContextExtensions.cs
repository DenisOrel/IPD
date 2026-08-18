
// Type: Intermech.Extensions.ControlContextExtensions
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Extensions;

/// <summary>Методы-расширения для <see cref="T:System.Windows.Forms.Control" /> в контексте клиента IPS</summary>
public static class ControlContextExtensions
{
  /// <summary>
  /// Сформировать строковое описание контекста, включая вышестояшие именованные контексты, разделённые знаком разделителя.
  /// Например "Создание объекта/Форма создания объекта/Контрол выбора типа"
  /// </summary>
  /// <param name="control">Контрол, контекст которого собирается</param>
  /// <param name="delimiter">Разделитель в формируемом пути</param>
  /// <param name="includeTypeInfo">Если true, то к имени контекста будет прибавлена информация о типе объекта</param>
  /// <param name="includeControlName">Если true, то к имени контекста будет прибавлено имя контрола</param>
  /// <returns>полное имя операции</returns>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetControlContextName(
    [NotNull] this Control control,
    char delimiter = '/',
    bool includeTypeInfo = false,
    bool includeControlName = false)
  {
    string controlContextName = control.GetObjectContextName(delimiter, includeTypeInfo);
    if (includeControlName)
    {
      string name = control.Name;
      if (!string.IsNullOrEmpty(name) && !string.Equals(name, control.GetType().Name))
        controlContextName = !string.IsNullOrEmpty(controlContextName) ? controlContextName + delimiter.ToString() + name : name;
    }
    return controlContextName;
  }

  /// <summary>
  /// Сформировать строковое описание контекста, включая вышестояшие именованные контексты, разделённые знаком разделителя.
  /// Например "Создание объекта/Форма создания объекта/Контрол выбора типа"
  /// </summary>
  /// <param name="control">Контрол, контекст которого собирается</param>
  /// <param name="includeTypeInfo">Если true, то к имени операции будет прибавлена информация о типе объекта</param>
  /// <param name="includeControlName">Если true, то к имени операции будет прибавлено имя контрола</param>
  /// <returns>полное имя операции</returns>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetControlContextName(
    [NotNull] this Control control,
    bool includeTypeInfo,
    bool includeControlName)
  {
    return control.GetControlContextName('/', includeTypeInfo, includeControlName);
  }
}
