// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileVars
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.ControlFlow;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Содержит динамические переменные, влияющие на работу сервисов IPS по работе с файлами.
/// </summary>
public static class FileVars
{
  /// <summary>
  /// Переключатель, позволяющий активировать режим мягкого импорта файлов в IPS.
  /// </summary>
  public static readonly DynamicVariable<bool> SoftMode = new DynamicVariable<bool>("FileVars.SoftMode", false);
  /// <summary>
  /// Переключатель, позволяющий активировать режим расширенного импорта файлов в IPS.
  /// При включении этого режима помимо импорта документов в IPS также будут созданы
  /// объекты, выпускаемые по этим документам (изделия и др).
  /// </summary>
  public static readonly DynamicVariable<bool> ExtendedMode = new DynamicVariable<bool>("FileVars.ExtendedMode", false);
}
