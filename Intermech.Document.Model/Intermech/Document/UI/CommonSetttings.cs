// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.CommonSetttings
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Класс общих настроек</summary>
public class CommonSetttings
{
  private static Dictionary<string, CommonSetttings> settings = new Dictionary<string, CommonSetttings>();
  private string name;
  private Dictionary<string, object> properties = new Dictionary<string, object>();

  /// <summary>Получить настройки по имени</summary>
  /// <param name="name">Наименование настроек</param>
  /// <returns>Настройки</returns>
  public static CommonSetttings GetSetttings(string name)
  {
    if (CommonSetttings.settings.ContainsKey(name))
      return CommonSetttings.settings[name];
    CommonSetttings setttings = new CommonSetttings(name);
    CommonSetttings.settings[name] = setttings;
    return setttings;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name">Имя настроек</param>
  public CommonSetttings(string name) => this.Name = name;

  /// <summary>Имя настроек</summary>
  public string Name
  {
    get => this.name;
    set => this.name = value;
  }

  /// <summary>Получить параметр</summary>
  /// <param name="name">Имя параметра</param>
  /// <returns>Значение, null если отсутствует</returns>
  public object GetProperty(string name)
  {
    return this.properties.ContainsKey(name) ? this.properties[name] : (object) null;
  }

  /// <summary>Установить значение</summary>
  /// <param name="name">Имя параметра</param>
  /// <param name="value">Значение</param>
  public void SetProperty(string name, object value) => this.properties[name] = value;
}
