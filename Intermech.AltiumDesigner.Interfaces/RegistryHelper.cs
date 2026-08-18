// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.RegistryHelper
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using Microsoft.Win32;
using System;
using System.Security.AccessControl;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Статические методы по работе с реестром</summary>
public class RegistryHelper
{
  /// <summary>
  /// Наименование раздела реестра где пишем путь к исполняемому файлу Altium Designer
  /// </summary>
  public const string RegistryKeyApplicationPath = "SOFTWARE\\Intermech\\CAD\\AltiumDesigner";

  /// <summary>
  /// Возвращает ключ реестра в котором хранится путь к dxp.exe
  /// </summary>
  /// <param name="create">Если ключ отсутствует - создать его</param>
  /// <returns></returns>
  public static RegistryKey GetAltiumDesignerExePathRegistryKey(bool create)
  {
    RegistryKey registryKey = Environment.Is64BitOperatingSystem ? RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64) : Registry.CurrentUser;
    RegistryKey exePathRegistryKey = registryKey.OpenSubKey("SOFTWARE\\Intermech\\CAD\\AltiumDesigner", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
    if (exePathRegistryKey == null & create)
      exePathRegistryKey = registryKey.CreateSubKey("SOFTWARE\\Intermech\\CAD\\AltiumDesigner");
    return exePathRegistryKey;
  }
}
