// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ObjectInfoForExteption
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Структура хранит информацию об объектах, которые не удалось синхронизировать.
/// </summary>
[Serializable]
/// <summary>Конструктор.</summary>
/// <param name="id">Идентификатор версии объектов</param>
/// <param name="caption">Наименование объекта</param>
/// <param name="errMsg">Текст сообщения</param>
public struct ObjectInfoForExteption(long id, string caption, string msg)
{
  private long _id = id;
  private string _caption = caption;
  private string _msg = msg;

  /// <summary>Идентификатор версии объектов.</summary>
  public long ID => this._id;

  /// <summary>Наименование объекта.</summary>
  public string Caption => this._caption;

  /// <summary>Текст сообщения.</summary>
  public string Message => this._msg;
}
