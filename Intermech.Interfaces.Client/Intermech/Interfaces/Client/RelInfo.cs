// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.RelInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Краткая информация по связи</summary>
[Serializable]
public class RelInfo
{
  /// <summary>Идентификатор версии родительского объекта</summary>
  public long ProjID;
  /// <summary>Идентификатор типа родительского объекта</summary>
  public int ProjTypeID;
  /// <summary>Идентификатор типа связи</summary>
  public int RelType;

  /// <summary>Создать краткую информацию по связи</summary>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="projTypeID">Идентификатор типа родительского объекта</param>
  /// <param name="relType">Идентификатор типа связи</param>
  public RelInfo(long projID, int projTypeID, int relType)
  {
    this.ProjID = projID;
    this.ProjTypeID = projTypeID;
    this.RelType = relType;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.RelType.GetHashCode() << 24 ^ this.ProjID.GetHashCode();

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is RelInfo relInfo && this.RelType == relInfo.RelType && this.ProjID == relInfo.ProjID;
  }

  /// <summary>Получить строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return $"F_PROJ_ID: {this.ProjID}, F_RELATION_TYPE: {this.RelType} ({MetaDataHelper.GetRelationTypeName(this.RelType)}), F_PROJ_TYPE: {this.ProjTypeID} ({MetaDataHelper.GetObjectTypeName(this.ProjTypeID)})";
  }
}
