// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.DBBindedObjectType
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.DataFormats;
using System.Diagnostics;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Формат для передачи данных о типах объектов, с которыми связаны выборки
/// базы данных через clipboard, а также между различными частями универсального клиента
/// </summary>
public class DBBindedObjectType : IDBObjectTypeSelectionID
{
  /// <summary>Тип объекта</summary>
  private int bindedObjectTypeID = -1;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeID"></param>
  public DBBindedObjectType(int objectTypeID) => this.bindedObjectTypeID = objectTypeID;

  /// <summary>Идентификатор типа объекта, с которым связана выборка</summary>
  public int BindedObjectTypeID
  {
    [DebuggerStepThrough] get => this.bindedObjectTypeID;
  }
}
