
// Type: Intermech.Client.Core.Forms.Warning
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Client.Core.Forms;

/// <summary>Класс для показа предупреждений</summary>
public static class Warning
{
  /// <summary>Отобразить окно с предупреждением</summary>
  public static bool Show(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [NotNull, ItemNotNull] IEnumerable<string> warnings)
  {
    return Warning.Show(centerOnForm, ownerServices, (string) null, warnings);
  }

  /// <summary>Отобразить окно с предупреждением</summary>
  public static bool Show(
    [CanBeNull] Form centerOnForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [CanBeNull, ItemNotNull] IEnumerable<string> warnings)
  {
    if (warnings == null)
      return true;
    IEnumerator<string> enumerator = warnings.GetEnumerator();
    if (!enumerator.MoveNext())
      return true;
    using (SelectObjectCompositionWarningForm compositionWarningForm = new SelectObjectCompositionWarningForm(centerOnForm, ownerServices, contextName, enumerator))
      return compositionWarningForm.ShowDialog() == DialogResult.OK;
  }
}
