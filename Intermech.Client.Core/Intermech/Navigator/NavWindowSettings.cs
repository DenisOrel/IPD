
// Type: Intermech.Navigator.NavWindowSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Служба позволяет хранить различные настройки для элементов управления,
/// находящихся внутри окон "Навигатора"
/// </summary>
internal class NavWindowSettings : INavWindowSettings
{
  /// <summary>Объект для потокобезопасного доступа</summary>
  private object _syncRoot = new object();
  /// <summary>Словарик с настройками</summary>
  private Dictionary<object, object> _settings = new Dictionary<object, object>();

  /// <summary>Считать или установить настройки с указанным ключом</summary>
  /// <param name="key">Ключ настроек</param>
  /// <returns>Настройки с указанным ключом или null, если настроек нет</returns>
  public object this[object key]
  {
    get
    {
      lock (this._syncRoot)
        return key == null || !this._settings.ContainsKey(key) ? (object) null : this._settings[key];
    }
    set
    {
      lock (this._syncRoot)
      {
        if (key == null)
          return;
        this._settings[key] = value;
      }
    }
  }

  /// <summary>Удалить все настройки</summary>
  public void Reset()
  {
    lock (this._syncRoot)
      this._settings.Clear();
  }
}
