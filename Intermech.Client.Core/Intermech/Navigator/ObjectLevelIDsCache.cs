
// Type: Intermech.Navigator.ObjectLevelIDsCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Кэш уровней продвижения</summary>
public class ObjectLevelIDsCache : ICache, IObjectLevelIDsCache
{
  /// <summary>Кэш уровней продвижения</summary>
  protected static Dictionary<int, LCLevel> _levels = new Dictionary<int, LCLevel>();
  /// <summary>Кэш имён уровней продвижения</summary>
  protected static Dictionary<string, int> _names = new Dictionary<string, int>();
  /// <summary>Коллекция изображений</summary>
  protected static ImageList _imageList = new ImageList();

  /// <summary>Создать экземпляр кэша</summary>
  public ObjectLevelIDsCache() => this.ReloadIcons();

  /// <summary>Очистить кэш</summary>
  public void Reset()
  {
    lock (ObjectLevelIDsCache._names)
      ObjectLevelIDsCache._names.Clear();
    lock (ObjectLevelIDsCache._levels)
      ObjectLevelIDsCache._levels.Clear();
    lock (ObjectLevelIDsCache._imageList)
      ObjectLevelIDsCache._imageList.Images.Clear();
    this.ReloadIcons();
  }

  /// <summary>Перечитать значки</summary>
  public void ReloadIcons()
  {
    DataTable dataTable = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetLifecycleLevelCollection().Select(string.Empty);
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row["F_LEVEL_ID"]);
      LCLevel lcLevel = new LCLevel();
      lcLevel.SyncMetadata(int32);
      lock (ObjectLevelIDsCache._levels)
      {
        if (ObjectLevelIDsCache._levels.ContainsKey(int32))
          ObjectLevelIDsCache._levels.Remove(int32);
        ObjectLevelIDsCache._levels.Add(int32, lcLevel);
      }
      lock (ObjectLevelIDsCache._names)
      {
        if (ObjectLevelIDsCache._names.ContainsKey(lcLevel.LevelName))
          ObjectLevelIDsCache._names.Remove(lcLevel.LevelName);
        ObjectLevelIDsCache._names.Add(lcLevel.LevelName, int32);
      }
      if (lcLevel.Icon != null)
      {
        lock (ObjectLevelIDsCache._imageList)
        {
          if (ObjectLevelIDsCache._imageList.Images.ContainsKey(lcLevel.LevelName))
            ObjectLevelIDsCache._imageList.Images.RemoveByKey(lcLevel.LevelName);
          ObjectLevelIDsCache._imageList.Images.Add(lcLevel.LevelName, lcLevel.Icon);
        }
      }
    }
    dataTable.Dispose();
  }

  /// <summary>Получить название уровня продвижения</summary>
  /// <param name="levelID">Идентификатор уровня продвижения</param>
  /// <returns>Название уровня продвижения</returns>
  public virtual string GetName(int levelID)
  {
    if (levelID == 0)
      return string.Empty;
    lock (ObjectLevelIDsCache._levels)
    {
      if (ObjectLevelIDsCache._levels.ContainsKey(levelID))
        return ObjectLevelIDsCache._levels[levelID].LevelName;
    }
    this.ReloadIcons();
    lock (ObjectLevelIDsCache._levels)
    {
      if (ObjectLevelIDsCache._levels.ContainsKey(levelID))
        return ObjectLevelIDsCache._levels[levelID].LevelName;
    }
    return string.Empty;
  }

  /// <summary>Получить значок уровня продвижения</summary>
  /// <param name="name">Название уровня продвижения</param>
  /// <returns>Значок уровня продвижения</returns>
  public virtual Icon GetIcon(string name)
  {
    lock (ObjectLevelIDsCache._levels)
    {
      lock (ObjectLevelIDsCache._names)
        return name == string.Empty || ObjectLevelIDsCache._names.ContainsKey(name) ? (Icon) null : ObjectLevelIDsCache._levels[ObjectLevelIDsCache._names[name]].Icon;
    }
  }

  /// <summary>
  /// Получить список изображений со значками уровней продвижения
  /// </summary>
  public ImageList ImageList => ObjectLevelIDsCache._imageList;

  /// <summary>Получить описание уровня продвижения по его ID</summary>
  /// <param name="levelID">Идентификатор уровня продвижения</param>
  /// <returns>Описание уровня продвижения по его ID</returns>
  public virtual LCLevel GetLCLevel(int levelID)
  {
    if (levelID == 0)
      return (LCLevel) null;
    lock (ObjectLevelIDsCache._levels)
    {
      if (ObjectLevelIDsCache._levels.ContainsKey(levelID))
        return ObjectLevelIDsCache._levels[levelID];
    }
    this.ReloadIcons();
    lock (ObjectLevelIDsCache._levels)
    {
      if (ObjectLevelIDsCache._levels.ContainsKey(levelID))
        return ObjectLevelIDsCache._levels[levelID];
    }
    return (LCLevel) null;
  }
}
