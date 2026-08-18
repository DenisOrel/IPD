// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.FavouriteData
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using Intermech.Imbase;
using System;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>
/// Внутренний класс для хранения данных виртуального узла "Материал".
/// </summary>
[Serializable]
public class FavouriteData
{
  private long _folderID;
  private long _tableRefID;
  private long _recID = -1;
  private string _caption = string.Empty;
  private string _imbaseKey = string.Empty;

  /// <summary>Наименование материала.</summary>
  public string Caption => this._caption;

  /// <summary>
  /// 
  /// </summary>
  public long FolderID => this._folderID;

  /// <summary>
  /// 
  /// </summary>
  public string ImbaseKey
  {
    get => this._imbaseKey;
    set => this._imbaseKey = value;
  }

  /// <summary>Номер записи в таблице IMBASE.</summary>
  public long RecordID => this._recID;

  /// <summary>Ссылка на таблицу IMBASE.</summary>
  public long TableRefID => this._tableRefID;

  /// <summary>Конструктор.</summary>
  /// <param name="tableRefID">Ссылка на таблицу IMBASE</param>
  /// <param name="recID">Номер записи в таблице IMBASE</param>
  /// <param name="caption">Наименование материала</param>
  public FavouriteData(long tableRefID, long recID, string caption)
  {
    this._tableRefID = tableRefID;
    this._recID = recID;
    this._caption = caption;
    this._imbaseKey = ImbaseHelper.MakeInternalImbaseKey(tableRefID, recID);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="tableRefID">Ссылка на таблицу IMBASE</param>
  /// <param name="recID">Номер записи в таблице IMBASE</param>
  /// <param name="caption">Наименование материала</param>
  /// <param name="imbaseKey"></param>
  public FavouriteData(long tableRefID, long recID, string caption, string imbaseKey)
  {
    this._tableRefID = tableRefID;
    this._recID = recID;
    this._caption = caption;
    this._imbaseKey = imbaseKey;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="folderID"></param>
  /// <param name="tableRefID">Ссылка на таблицу IMBASE</param>
  /// <param name="recID">Номер записи в таблице IMBASE</param>
  /// <param name="caption">Наименование материала</param>
  public FavouriteData(long folderID, long tableRefID, long recID, string caption)
    : this(tableRefID, recID, caption)
  {
    this._folderID = folderID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    bool flag;
    if (obj is FavouriteData)
    {
      FavouriteData favouriteData = obj as FavouriteData;
      flag = this._tableRefID == favouriteData.TableRefID && this._recID == favouriteData.RecordID;
    }
    else
      flag = base.Equals(obj);
    return flag;
  }

  /// <summary>Наименование материала.</summary>
  /// <returns></returns>
  public override string ToString() => this._caption;
}
