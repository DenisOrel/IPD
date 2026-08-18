
// Type: Intermech.Navigator.ComputerNamesCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator;

/// <summary>Кэш списка компьютеров текущего домена</summary>
public class ComputerNamesCache : IComputerNamesCache, ICache
{
  /// <summary>
  /// Коллекция пар значений [(Int64)Идентификатор компьютера] = [(string)Имя компьютера]
  /// </summary>
  private Dictionary<Guid, string> _names;

  public List<string> GetComputerNames()
  {
    this.CheckLoaded();
    return this._names.Select<KeyValuePair<Guid, string>, string>((Func<KeyValuePair<Guid, string>, string>) (s => s.Value)).ToList<string>();
  }

  /// <summary>Сбросить содержимое кэша</summary>
  public void Reset() => this._names = (Dictionary<Guid, string>) null;

  /// <summary>Метод зачитывает с DC список компьютеров</summary>
  private void LoadComputersCache()
  {
    try
    {
      this._names = LdapProcs.GetNetworkHostsForCurrentDomain();
    }
    catch (Exception ex)
    {
      this._names = new Dictionary<Guid, string>();
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Проверяет зачитан ли кэш и если нет - зачитывает его</summary>
  private void CheckLoaded()
  {
    if (this._names != null)
      return;
    this.LoadComputersCache();
  }
}
