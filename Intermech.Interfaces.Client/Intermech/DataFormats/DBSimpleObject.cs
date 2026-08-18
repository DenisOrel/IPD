// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBSimpleObject
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Класс для хранения минимальной информации об объекте, которая часто нужна для работы c данными на интерфейсе клиента
/// </summary>
public class DBSimpleObject
{
  public long ObjectID;
  public int ObjectTypeID;
  public string Caption;

  public DBSimpleObject(long objectID, int objectTypeId, string caption = "")
  {
    this.ObjectID = objectID;
    this.ObjectTypeID = objectTypeId;
    this.Caption = caption;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj != null && obj is DBSimpleObject dbSimpleObject && this.ObjectID == dbSimpleObject.ObjectID && this.ObjectTypeID == dbSimpleObject.ObjectTypeID;
  }

  /// <summary>Получить 32-битный хэш-код объекта</summary>
  /// <returns>32-битный хэш-код объекта</returns>
  public override int GetHashCode() => this.ObjectID.GetHashCode();
}
