
// Type: Intermech.Navigator.LCLevel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Drawing;
using System.IO;


namespace Intermech.Navigator;

/// <summary>Уровень продвижения</summary>
[Serializable]
public class LCLevel : ICloneable, IComparable, IComparable<LCLevel>
{
  /// <summary>Идентификатор уровня продвижения</summary>
  protected int _levelID;
  /// <summary>Guid уровня продвижения</summary>
  protected Guid _guid;
  /// <summary>Название уровня продвижения</summary>
  protected string _levelName;
  /// <summary>Значок уровня продвижения</summary>
  protected Icon _icon;

  /// <summary>Создать пустой экземпляр класса</summary>
  public LCLevel()
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="levelID">Идентификатор уровня продвижения</param>
  /// <param name="guid">Guid уровня продвижения</param>
  /// <param name="levelName">Название уровня продвижения</param>
  /// <param name="icon">Значок уровня продвижения</param>
  public LCLevel(int levelID, Guid guid, string levelName, Icon icon)
  {
    this._levelID = levelID;
    this._guid = guid;
    this._levelName = levelName;
    this._icon = icon;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="level">Описание уровня продвижения из кэша метаданных</param>
  /// <param name="session">Сессия, в рамках которой происходит работа с кэшем метаданных</param>
  public LCLevel(IDBLifecycleLevelType level, IUserSession session)
  {
    if (level == null)
      return;
    this._levelID = level.LevelID;
    this._guid = level.GUID;
    this._levelName = level.LevelName;
    using (MemoryStream memoryStream = new MemoryStream(level.LevelIcon))
      this._icon = new Icon((Stream) memoryStream);
  }

  /// <summary>Идентификатор уровня продвижения</summary>
  public int LevelID => this._levelID;

  /// <summary>Guid уровня продвижения</summary>
  public Guid Guid => this._guid;

  /// <summary>Название уровня продвижения</summary>
  public string LevelName => this._levelName;

  /// <summary>Значок уровня продвижения</summary>
  public Icon Icon => this._icon;

  /// <summary>Выполнить синхронизацию с кэшем метаданных</summary>
  /// <param name="levelID">Идентификатор уровня продвижения (может быть новым)</param>
  /// <param name="session">Сессия, в рамках которой происходит работа с кэшем метаданных</param>
  public virtual void SyncMetadata(int levelID)
  {
    IDBLifecycleLevelInfo lifecycleLevel = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetLifecycleLevel(levelID);
    this._levelID = lifecycleLevel.LevelID;
    this._guid = lifecycleLevel.GUID;
    this._levelName = lifecycleLevel.LevelName;
    byte[] levelIcon = lifecycleLevel.LevelIcon;
    try
    {
      if (levelIcon != null && levelIcon.Length != 0)
      {
        using (MemoryStream memoryStream = new MemoryStream(lifecycleLevel.LevelIcon))
          this._icon = new Icon((Stream) memoryStream);
      }
      else
        this._icon = (Icon) null;
    }
    catch
    {
      this._icon = (Icon) null;
    }
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone()
  {
    return (object) new LCLevel(this.LevelID, this.Guid, this.LevelName, new Icon(this.Icon, new Size(this.Icon.Width, this.Icon.Height)));
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>0, если объекты равны</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as LCLevel);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0 или 1</returns>
  public int CompareTo(LCLevel other) => other == null ? 1 : this.LevelID.CompareTo(other.LevelID);

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj) == 0;

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.LevelID.GetHashCode();

  /// <summary>Вернуть строковое представление экземпляра объекта</summary>
  /// <returns>Строковое представление экземпляра объекта</returns>
  public override string ToString() => $"[{this._levelID}] {this._levelName}";
}
