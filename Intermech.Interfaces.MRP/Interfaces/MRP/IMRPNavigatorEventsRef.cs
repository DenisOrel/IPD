// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPNavigatorEventsRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Интерфейс, ссылающийся на коллекции, позволяющие сформировать уведомления для Навигатора
/// </summary>
public interface IMRPNavigatorEventsRef : IMRPContext
{
  /// <summary>Добавить в контейнер информацию о созданном объекте</summary>
  /// <param name="objID">Идентификтор версии объекта</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  void AddCreatedObject(long objID, int objTypeID);

  /// <summary>Добавить в контейнер информацию об изменённом объекте</summary>
  /// <param name="objID">Идентификтор версии объекта</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  void AddChangedObject(long objID, int objTypeID);

  /// <summary>Добавить в контейнер информацию о созданной связи</summary>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="projTypeID">Идентификатор типа родительского объекта</param>
  void AddCreatedRelation(long relID, int relTypeID, long projID, int projTypeID);

  /// <summary>Добавить в контейнер информацию об удалённой связи</summary>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  void AddDeletedRelation(long relID, int relTypeID);
}
