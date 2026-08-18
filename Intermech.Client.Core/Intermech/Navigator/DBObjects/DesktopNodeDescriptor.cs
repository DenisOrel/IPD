
// Type: Intermech.Navigator.DBObjects.DesktopNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Persistence;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор рабочего стола</summary>
public sealed class DesktopNodeDescriptor : Descriptor
{
  /// <summary>Создать незаполненный экземпляр класса</summary>
  public DesktopNodeDescriptor()
  {
  }

  /// <summary>
  /// Создает дескриптор (есть вся информация об объекте, обращение к СУБД не требуется)
  /// </summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="objGuid">Guid версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  public DesktopNodeDescriptor(long objID, Guid objGuid, ObjectFiltrationState state)
    : base(objID, objGuid, state)
  {
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  public DesktopNodeDescriptor(long objID)
    : base(objID)
  {
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  public DesktopNodeDescriptor(long objID, ObjectFiltrationState state)
    : base(objID, state)
  {
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  /// <param name="notCheckObject">Не выполнять обращение к серверу приложений, дескриптор получается частично заполненным</param>
  public DesktopNodeDescriptor(long objID, ObjectFiltrationState state, bool notCheckObject)
    : base(objID, state, notCheckObject)
  {
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objGuid">Guid объекта</param>
  public DesktopNodeDescriptor(Guid objGuid)
    : base(objGuid)
  {
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objGuid">Guid объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  public DesktopNodeDescriptor(Guid objGuid, ObjectFiltrationState state)
    : base(objGuid, state)
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected DesktopNodeDescriptor(PersistentState state)
    : this()
  {
    this._objID = DesktopObjectNode.DesktopObjectID;
    this._realObjID = DesktopObjectNode.DesktopObjectID;
    this._objGuid = DesktopObjectNode.DesktopObjectGuid;
    this.CorrectState();
  }

  public override void GetObjectData(PersistentState state)
  {
  }
}
