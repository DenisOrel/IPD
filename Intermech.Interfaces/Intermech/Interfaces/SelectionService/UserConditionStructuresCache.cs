
// Type: Intermech.Interfaces.SelectionService.UserConditionStructuresCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Interfaces.SelectionService
{
    /// <summary>
    /// Кэш временных значений для выборок текущего пользователя
    /// </summary>
    internal sealed class UserConditionStructuresCache
    {
      /// <summary>Кэш временных значений</summary>
      private Dictionary<long, TemporaryInfo> _cache;
      /// <summary>Имя файла для записи в базу</summary>
      private readonly string _configFileName = nameof (UserConditionStructuresCache);

      /// <summary>Конструктор</summary>
      public UserConditionStructuresCache() => this._cache = new Dictionary<long, TemporaryInfo>();

      /// <summary>
      /// Перечитать кэш. Если включен режим сохранения временных значений для выборок между сеансами,
      /// то кэш зачитывается из базы
      /// </summary>
      public void Reload()
      {
        this._cache.Clear();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBConfigurations configurations = sessionKeeper.Session.Configurations;
          if (!this.IsSaveToBase(configurations))
            return;
          BlobInformation config_info;
          byte[] config_file;
          configurations.LoadConfigData(this._configFileName, out config_info, out config_file, sessionKeeper.Session.UserID);
          if (config_info.RealFileSize == 0L || config_file == null || config_file.Length == 0)
            return;
          using (Stream serializationStream = (Stream) new MemoryStream(config_file))
          {
            serializationStream.Position = 0L;
            this._cache = (Dictionary<long, TemporaryInfo>) new BinaryFormatter().Deserialize(serializationStream);
          }
        }
      }

      /// <summary>Сохранение кэша в базе</summary>
      public void Save()
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBConfigurations configurations = sessionKeeper.Session.Configurations;
          if (!this.IsSaveToBase(configurations))
            return;
          using (ImChunkedStream serializationStream = new ImChunkedStream())
          {
            new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._cache);
            configurations.WriteConfigData(new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.UtcNow, this._configFileName, ArcMethods.NotPacked, string.Empty), serializationStream.ToArray());
          }
        }
      }

      /// <summary>Установить временные значения для выборки</summary>
      /// <param name="selectionID">Идентификатор версии выборки</param>
      /// <param name="info">Временные значения</param>
      public void SetValue(long selectionID, TemporaryInfo info)
      {
        if (this._cache.ContainsKey(selectionID))
          this._cache[selectionID] = info;
        else
          this._cache.Add(selectionID, info);
        this.Save();
      }

      /// <summary>Получить временные значения для выборки</summary>
      /// <param name="selectionID">Идентификатор версии выборки</param>
      /// <returns></returns>
      public TemporaryInfo GetValue(long selectionID)
      {
        TemporaryInfo temporaryInfo = (TemporaryInfo) null;
        this._cache.TryGetValue(selectionID, out temporaryInfo);
        return temporaryInfo;
      }

      /// <summary>Признак записи кэша в базу</summary>
      /// <param name="configs"></param>
      /// <returns></returns>
      private bool IsSaveToBase(IDBConfigurations configs)
      {
        return configs.ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.SaveSelectionConditionStateParamName, false, DBConfigMode.GlobalOnly);
      }
    }
}
