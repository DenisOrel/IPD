
// Type: Intermech.Navigator.ElementStatusesClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;


namespace Intermech.Navigator;

/// <summary>
/// Клиентская служба, которая позволяет считывать статусы элементов
/// </summary>
public sealed class ElementStatusesClientService : IElementStatusesClientService
{
  /// <summary>Guid настроек службы</summary>
  private static readonly Guid SettingsGuid = new Guid("{7DA7EF36-04BC-4FC4-BA8C-97342D34AC53}");
  /// <summary>
  /// Суммарное количество бит в массиве, отожранное всеми плагинами
  /// </summary>
  private int _bitsCount;
  /// <summary>
  /// Коллекция пар значений [(string)Guid плагина] = [(ElementStatusesPluginDescription)Описание плагина]
  /// </summary>
  private Dictionary<string, ElementStatusesPluginDescription> _plugins = new Dictionary<string, ElementStatusesPluginDescription>();
  /// <summary>
  /// Коллекция пар значений [(string)Guid плагина] = [(Int32)Номер первого бита плагина в общем массиве бит]
  /// </summary>
  private Dictionary<string, int> _pluginBits = new Dictionary<string, int>();
  /// <summary>
  /// Коллекция пар значений [(string)Guid плагина] = [(Int32)количество бит плагина в общем массиве бит]
  /// </summary>
  private Dictionary<string, int> _pluginBitsCount = new Dictionary<string, int>();
  /// <summary>
  /// Список Guid плагинов, которым надо запретить добавлять свои статусы в столбец "Статусы элементов"
  /// </summary>
  private List<string> _disabledPlugins = new List<string>(0);
  /// <summary>Список значков для статусов различных плагинов</summary>
  private Dictionary<Guid, Dictionary<int, Image>> _statusesIcons = new Dictionary<Guid, Dictionary<int, Image>>();

  /// <summary>Синхронизироваться с серверной стороной</summary>
  /// <param name="serverSide">Серверная служба</param>
  /// <param name="statuses">Серверная служба</param>
  public void SyncWithServerSide(IElementStatusesService serverSide, IPluginStatusesTable statuses)
  {
    this._bitsCount = 0;
    this._plugins.Clear();
    this._pluginBits.Clear();
    this._pluginBitsCount.Clear();
    this._statusesIcons.Clear();
    if (serverSide == null || statuses == null || serverSide.PluginBits.Count <= 0)
      return;
    this._bitsCount = serverSide.CapacityInBits;
    IDictionaryEnumerator enumerator = (IDictionaryEnumerator) serverSide.PluginBits.GetEnumerator();
    enumerator.Reset();
    while (enumerator.MoveNext())
    {
      string key = (string) enumerator.Key;
      int num1 = (int) enumerator.Value;
      int num2 = serverSide.PluginBitsCount[key];
      this._pluginBits[key] = num1;
      this._pluginBitsCount[key] = num2;
      this._plugins[key] = serverSide.Plugins[key];
      DataTable pluginStatusesTable = statuses.GetPluginStatusesTable(key, true, (int[]) null);
      Dictionary<int, Image> dictionary = new Dictionary<int, Image>();
      this._statusesIcons.Add(new Guid(key), dictionary);
      foreach (DataRow row in (InternalDataCollectionBase) pluginStatusesTable.Rows)
      {
        byte[] buffer = Convert.IsDBNull(row[2]) ? (byte[]) null : (byte[]) row[2];
        if (buffer != null && buffer.Length != 0)
        {
          Image image = (Image) null;
          if (buffer[0] == (byte) 0)
          {
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
              using (Icon icon = new Icon((Stream) memoryStream))
                image = (Image) icon.ToBitmap();
            }
          }
          else
          {
            using (MemoryStream memoryStream = new MemoryStream(buffer))
              image = Image.FromStream((Stream) memoryStream);
          }
          if (image != null)
          {
            int int32 = Convert.ToInt32(row[0]);
            dictionary[int32] = image;
          }
        }
      }
    }
  }

  /// <summary>
  /// Загрузить настройки пользователя (например, список отключенных статусов)
  /// </summary>
  /// <param name="session">Сессия</param>
  public void LoadUserSettings(IUserSession session)
  {
    if (session == null || !(session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    this._disabledPlugins.Clear();
    if (!(customService[session.UserID, (object) ElementStatusesClientService.SettingsGuid] is List<Guid> guidList) || guidList.Count == 0)
      return;
    for (int index = 0; index < guidList.Count; ++index)
      this._disabledPlugins.Add(guidList[index].ToString());
  }

  /// <summary>
  /// Сохранить настройки пользователя (например, список отключенных статусов)
  /// </summary>
  /// <param name="session">Сессия</param>
  public void SaveUserSettings(IUserSession session)
  {
    if (session == null || !(session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    List<Guid> guidList = new List<Guid>(this._disabledPlugins.Count);
    for (int index = 0; index < this._disabledPlugins.Count; ++index)
      guidList.Add(new Guid(this._disabledPlugins[index]));
    customService[session.UserID, (object) ElementStatusesClientService.SettingsGuid] = (object) guidList;
  }

  /// <summary>
  /// Текущая емкость массива бит, который требуется для всех зарегистрированных плагинов (в байтах)
  /// </summary>
  public int Capacity => (this._bitsCount + 7) / 8;

  /// <summary>
  /// Текущая емкость массива бит, который требуется для всех зарегистрированных плагинов (в битах)
  /// </summary>
  public int CapacityInBits => this._bitsCount;

  /// <summary>
  /// Коллекция пар значений [(string)Guid плагина] = [(ElementStatusesPluginDescription)Описание плагина]
  /// </summary>
  public Dictionary<string, ElementStatusesPluginDescription> Plugins => this._plugins;

  /// <summary>
  /// Считать статусы указанного элемента из подмножества бит указанного плагина
  /// с учётом того, что суммарная длина статусов не превышает 16 бит
  /// </summary>
  /// <param name="pluginGuid">Guid плагина, который в данный момент выполняет чтение статусов указанного элемента</param>
  /// <param name="elementStatuses">Битовый массив всех статусов обрабатываемого элемента в виде массива байт.
  /// Часть битов принадлежит указанному плагину и должна быть считана в виде 16-битного числа</param>
  /// <returns>Статусы текущего элемента, принадлежащие указанному плагину (не больше 16 бит)</returns>
  public short GetElementStatuses16(string pluginGuid, byte[] elementStatuses)
  {
    if (!this._pluginBitsCount.ContainsKey(pluginGuid))
      return 0;
    int pluginBit = this._pluginBits[pluginGuid];
    int count = this._pluginBitsCount[pluginGuid];
    return BitsArray.ExtractInt16(elementStatuses, pluginBit, count);
  }

  /// <summary>
  /// Список Guid плагинов, которым надо запретить добавлять свои статусы в столбец "Статусы элементов"
  /// </summary>
  public List<string> DisabledPlugins => this._disabledPlugins;

  /// <summary>
  /// Считать статусы указанного элемента из подмножества бит указанного плагина
  /// с учётом того, что суммарная длина статусов не превышает 32 бита
  /// </summary>
  /// <param name="pluginGuid">Guid плагина, который в данный момент выполняет чтение статусов указанного элемента</param>
  /// <param name="elementStatuses">Битовый массив всех статусов обрабатываемого элемента в виде массива байт.
  /// Часть битов принадлежит указанному плагину и должна быть считана в виде 32-битного числа</param>
  /// <returns>Статусы текущего элемента, принадлежащие указанному плагину (не больше 32 бит)</returns>
  public int GetElementStatuses32(string pluginGuid, byte[] elementStatuses)
  {
    if (!this._pluginBitsCount.ContainsKey(pluginGuid))
      return 0;
    int pluginBit = this._pluginBits[pluginGuid];
    int count = this._pluginBitsCount[pluginGuid];
    return BitsArray.ExtractInt32(elementStatuses, pluginBit, count);
  }

  /// <summary>
  /// Записать статусы указанного элемента в подмножество бит указанного плагина
  /// с учётом того, что суммарная длина статусов не превышает 16 бит
  /// </summary>
  /// <param name="pluginGuid">Guid плагина, который в данный момент выполняет запись статусов указанного элемента</param>
  /// <param name="elementStatuses">Битовый массив всех статусов обрабатываемого элемента в виде массива байт.
  /// Часть битов принадлежит указанному плагину и должна быть записана из 16-битного числа</param>
  /// <param name="value">Статусы текущего элемента, принадлежащие указанному плагину (не больше 16 бит)</param>
  public void SetElementStatuses16(string pluginGuid, byte[] elementStatuses, short value)
  {
    if (!this._pluginBits.ContainsKey(pluginGuid))
      return;
    int pluginBit = this._pluginBits[pluginGuid];
    BitsArray.PasteInt16(elementStatuses, value, pluginBit, this._pluginBitsCount[pluginGuid]);
  }

  /// <summary>
  /// Записать статусы указанного элемента в подмножество бит указанного плагина
  /// с учётом того, что суммарная длина статусов не превышает 32 бита
  /// </summary>
  /// <param name="pluginGuid">Guid плагина, который в данный момент выполняет запись статусов указанного элемента</param>
  /// <param name="elementStatuses">Битовый массив всех статусов обрабатываемого элемента в виде массива байт.
  /// Часть битов принадлежит указанному плагину и должна быть записана из 32-битного числа</param>
  /// <param name="value">Статусы текущего элемента, принадлежащие указанному плагину (не больше 32 бит)</param>
  public void SetElementStatuses32(string pluginGuid, byte[] elementStatuses, int value)
  {
    if (!this._pluginBits.ContainsKey(pluginGuid))
      return;
    int pluginBit = this._pluginBits[pluginGuid];
    BitsArray.PasteInt32(elementStatuses, value, pluginBit, this._pluginBitsCount[pluginGuid]);
  }

  /// <summary>Получить значок для статуса указанного плагина</summary>
  /// <param name="pluginGuid">Guid плагина</param>
  /// <param name="status">Статус</param>
  /// <returns>Значок или null</returns>
  public Image GetStatusIcon(Guid pluginGuid, int status)
  {
    if (!this._statusesIcons.ContainsKey(pluginGuid))
      return (Image) null;
    Image statusIcon = (Image) null;
    this._statusesIcons[pluginGuid].TryGetValue(status, out statusIcon);
    return statusIcon;
  }

  public int[] GetStatuses(string moduleKey, byte[] bytes)
  {
    if (string.IsNullOrEmpty(moduleKey))
      throw new ArgumentException();
    if (bytes == null)
      throw new ArgumentNullException(nameof (bytes));
    List<int> intList = new List<int>();
    if (this._plugins.ContainsKey(moduleKey))
    {
      int elementStatuses32 = this.GetElementStatuses32(moduleKey, bytes);
      if (this._plugins[moduleKey].IsFlags)
      {
        for (int y = 0; y < 32 /*0x20*/; ++y)
        {
          int num = (int) Math.Pow(2.0, (double) y);
          if ((elementStatuses32 & num) > 0 && !intList.Contains(num))
            intList.Add(num);
        }
      }
      else
        intList.Add(elementStatuses32);
    }
    return intList.ToArray();
  }
}
