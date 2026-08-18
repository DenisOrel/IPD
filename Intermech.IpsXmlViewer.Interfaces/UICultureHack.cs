// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.UICultureHack
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// По мнению Microsoft интерфейс любого приложения Windows должен соответствовать интерфейсу операционной системы. То есть
/// на английской версии Windows не удастся получить русский интерфейс у локализованного приложения. Этот класс позволяет
/// обойти данное ограничение.
/// </summary>
public static class UICultureHack
{
  /// <summary>
  /// Меняет язык интерфейса приложения. Имя локали берется из .config-файла из ключа с именем UICulture. Этот метод
  /// должен быть вызван при старте приложения.
  /// </summary>
  public static void Apply()
  {
    string name = ConfigurationManager.AppSettings["UICulture"];
    if (name != null)
      name = name.Trim();
    if (string.IsNullOrEmpty(name))
      return;
    CultureInfo cultureInfo = UICultureHack.CreateCultureInfo(name);
    if (cultureInfo == null)
      return;
    FieldInfo field = typeof (CultureInfo).GetField("s_userDefaultUICulture", BindingFlags.Static | BindingFlags.NonPublic);
    if (!(field != (FieldInfo) null))
      return;
    field.SetValue((object) null, (object) cultureInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  private static CultureInfo CreateCultureInfo(string name)
  {
    try
    {
      return CultureInfo.GetCultureInfo(name);
    }
    catch (ArgumentException ex)
    {
      return (CultureInfo) null;
    }
  }
}
