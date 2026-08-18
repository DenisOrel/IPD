
// Type: Intermech.PropertyEditors.PrototypeList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.PropertyEditors;

/// <summary>
/// Хранит список объектов-прототипов для соотв типа объекта.
/// Загружает список привязанных к типу объектов прототипов,
/// при сохранении удаляет привязки к прототипам, которые удалили из списка,
/// добавляет и привязывает новые (добавленные) объекты-прототипы.
/// </summary>
public class PrototypeList : List<PrototypeClass>
{
  private bool loaded;
  private int objectType = -1;

  public bool Loaded
  {
    get => this.loaded;
    set => this.loaded = value;
  }

  public int ObjectType
  {
    get => this.objectType;
    set => this.objectType = value;
  }

  public PrototypeList()
  {
  }

  public PrototypeList(int objectType) => this.objectType = objectType;

  public void Load()
  {
    this.ClearList();
    this.loaded = true;
  }

  public void Load(int objectType)
  {
    this.ClearList();
    this.objectType = objectType;
    if (objectType > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long filePrototype in sessionKeeper.Session.ServerCache.GetFilePrototypes(this.objectType))
          this.Add(new PrototypeClass(filePrototype));
      }
    }
    this.loaded = true;
  }

  public void ClearList()
  {
    this.Clear();
    this.loaded = false;
  }

  public PrototypeList Clone()
  {
    PrototypeList prototypeList = new PrototypeList();
    for (int index = 0; index < prototypeList.Count; ++index)
      prototypeList.Add(this[index].Clone());
    prototypeList.loaded = this.Loaded;
    return prototypeList;
  }

  public override string ToString()
  {
    string str1 = string.Empty;
    for (int index = 0; index < this.Count; ++index)
    {
      string str2 = this[index].ToString();
      string str3 = index < this.Count - 1 ? ";" : string.Empty;
      str1 = str1 + str2 + str3;
    }
    return str1;
  }
}
