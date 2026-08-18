// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ArchiveParameters
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Settings;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

/// <summary>
/// Позволяет получить путь к папке исходного архива предприятия, а также различные параметры архива.
/// </summary>
public static class ArchiveParameters
{
  private static CommonArchiveParameters common;

  /// <summary>
  /// Возвращает общие настройки исходного архива предприятия.
  /// </summary>
  public static CommonArchiveParameters Common
  {
    [MethodImpl(MethodImplOptions.Synchronized)] get
    {
      if (ArchiveParameters.common == null)
      {
        ArchiveParameters.common = new CommonArchiveParameters();
        ArchiveParameters.common.GetErrorText += new EventHandler<ErrorTextArgs>(ArchiveParameters.CommonParametersErrorHandler);
        ArchiveParameters.common.Load();
        ArchiveParameters.common.Changed += new EventHandler(ArchiveParameters.SaveChangesHandler);
      }
      return ArchiveParameters.common;
    }
  }

  private static void CommonParametersErrorHandler(object sender, ErrorTextArgs e)
  {
    e.Text = $"{e.Text} {LocalizationHolder.rm.GetString("SR_522")}";
  }

  private static void SaveChangesHandler(object sender, EventArgs e)
  {
    ((PersistentSettingsObject) sender).SaveInBackground();
  }
}
