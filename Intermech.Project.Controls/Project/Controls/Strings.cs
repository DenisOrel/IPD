// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.Strings
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Project.Controls;

internal static class Strings
{
  /// <summary>Не удается найти "{0}"</summary>
  [NotNull]
  public static string CannotFind => Localization.GetString(nameof (CannotFind));

  /// <summary>
  /// Не все выбранные объекты могут применяться в качестве исходных данных. Настройками связи "Вложения ImProject" не допускается вхождение в задачи ImProject объектов следующих типов: {0}.
  /// Продолжить операцию с пропуском объектов данных типов?
  /// </summary>
  [NotNull]
  public static string NotAllObjectsCouldBeSrcData
  {
    get => Localization.GetString(nameof (NotAllObjectsCouldBeSrcData));
  }

  /// <summary>Импортировать как подпроект</summary>
  [NotNull]
  public static string ImportAsSubproject => Localization.GetString(nameof (ImportAsSubproject));

  /// <summary>Статистика</summary>
  [NotNull]
  public static string StatisticsTitle => Localization.GetString(nameof (StatisticsTitle));
}
