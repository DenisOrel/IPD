// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.StandaloneView.StandaloneViewVars
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.ControlFlow;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.StandaloneView;

/// <summary>
/// Класс содержит динамические переменные, позволяющие изменять поведение команд пользовательского интерфейса,
/// предназначенных для автономного просмотра документов.
/// </summary>
/// <remarks>
/// <para>
/// В режиме автономного просмотра документов в содержимое или файл просматриваемого документа внедряются
/// дополнительные сведения - актуальные подписи документа, контрольная сумма файла, атрибуты документа,
/// заполняемые после согласования документа, и др.</para>
/// <para>
/// Динамические переменные, описанные в этом классе, изменяют поведение некоторых специализированных команд автономного просмотра.</para>
/// </remarks>
public static class StandaloneViewVars
{
  private static readonly DynamicVariable<bool> isActive = new DynamicVariable<bool>("StandaloneViewVars.IsActive", false);
  private static readonly DynamicVariable<bool> adjustSettingsInDialogMode = new DynamicVariable<bool>("StandaloneViewVars.AdjustSettingsInDialogMode", false);

  /// <summary>
  /// Возвращает или задает признак активации автономного просмотра в контексте
  /// текущего действия в интерфейсе пользователя. По умолчанию не активировано.
  /// </summary>
  public static DynamicVariable<bool> IsActive
  {
    [DebuggerStepThrough] get => StandaloneViewVars.isActive;
  }

  /// <summary>
  /// Включает и выключает диалоговый режим предварительной коррекции настроек автономного просмотра,
  /// выполняемой перед просмотром каждого документа по команде "Смотреть...". По умолчанию режим выключен.
  /// </summary>
  public static DynamicVariable<bool> AdjustSettingsInDialogMode
  {
    [DebuggerStepThrough] get => StandaloneViewVars.adjustSettingsInDialogMode;
  }
}
