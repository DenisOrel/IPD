// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPAddToContextActionAdv
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces.Contexts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее добавить одну или несколько версий объектов
/// в контекст редактирования (оптимизированная версия действия)
/// </summary>
public class MRPAddToContextActionAdv : MRPBaseAction
{
  /// <summary>Идентификатор версии контекста редектирования</summary>
  private long contextID;
  /// <summary>
  /// Список идентификаторов версий объектов, которые требуется добавить в контекст редактирования
  /// </summary>
  private List<long> objectIDs;
  /// <summary>
  /// Список идентификаторов объектов, которые требуется добавить в контекст редактирования
  /// </summary>
  private List<long> objectFIDs;
  /// <summary>
  /// Список типов объектов, которые требуется добавить в контекст редактирования
  /// </summary>
  private List<int> objectTypeIDs;

  /// <summary>
  /// Создать действие, позволяющее добавить одну или несколько версий объектов
  /// в контекст редактирования
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="contextID">Идентификатор версии контекста редектирования</param>
  /// <param name="objectIDs">Список идентификаторов версий объектов, которые требуется добавить в контекст редактирования</param>
  /// <param name="objectFIDs">Список идентификаторов объектов, которые требуется добавить в контекст редактирования</param>
  /// <param name="objectTypeIDs">Список типов объектов, которые требуется добавить в контекст редактирования</param>
  public MRPAddToContextActionAdv(
    IServiceProvider services,
    long contextID,
    IList<long> objectIDs,
    IList<long> objectFIDs,
    IList<int> objectTypeIDs)
    : base(services)
  {
    if (contextID == 0L)
      throw new ArgumentException();
    if (objectIDs == null)
      throw new ArgumentNullException(nameof (objectIDs));
    if (objectFIDs == null)
      throw new ArgumentNullException(nameof (objectFIDs));
    if (objectTypeIDs == null)
      throw new ArgumentNullException(nameof (objectTypeIDs));
    if (objectIDs.Count != objectFIDs.Count || objectIDs.Count != objectTypeIDs.Count)
      throw new ArgumentException();
    this.contextID = contextID;
    this.objectIDs = new List<long>((IEnumerable<long>) objectIDs);
    this.objectFIDs = new List<long>((IEnumerable<long>) objectFIDs);
    this.objectTypeIDs = new List<int>((IEnumerable<int>) objectTypeIDs);
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPAddToContextActionAdv(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.contextID = 0L;
    this.objectIDs = (List<long>) null;
    this.objectFIDs = (List<long>) null;
    this.objectTypeIDs = (List<int>) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPAddToContextActionAdv contextActionAdv))
      return;
    this.contextID = contextActionAdv.contextID;
    this.objectIDs = new List<long>((IEnumerable<long>) contextActionAdv.objectIDs);
    this.objectFIDs = new List<long>((IEnumerable<long>) contextActionAdv.objectFIDs);
    this.objectTypeIDs = new List<int>((IEnumerable<int>) contextActionAdv.objectTypeIDs);
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.objectIDs.Count == 0)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      if (!(contextSession.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService))
        throw new ArgumentNullException("IDBEditingContextsService");
      EditingContextsObjectContainer editingContextsObject = customService.GetEditingContextsObject((object) contextSession.SessionGUID, this.contextID, false, false);
      List<long> longList1 = new List<long>();
      List<long> longList2 = new List<long>();
      List<long> newVersionIDs = new List<long>();
      List<long> fIDs = new List<long>();
      if (this.Services.GetService(typeof (IMRPProgress)) is IMRPProgress service)
      {
        service.MinProgress = 0;
        service.Progress = 0;
        service.MaxProgress = this.objectIDs.Count + 1;
      }
      for (int index = 0; index < this.objectIDs.Count; ++index)
      {
        if (service != null)
          service.Progress = index;
        long objectId = this.objectIDs[index];
        int objectTypeId = this.objectTypeIDs[index];
        long objectFiD = this.objectFIDs[index];
        int num = MetaDataHelper.GetObjectType(objectTypeId) == null ? 0 : (MetaDataHelper.IsObjectTypeChildOf(objectTypeId, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545")) ? 1 : 0);
        if (!editingContextsObject.SimpleContext)
        {
          if (!editingContextsObject.ExistsObject(objectFiD) && !editingContextsObject.ExistsLinkedVersion(objectId) && !editingContextsObject.ExistsVersion(objectId, false) && !MetaDataHelper.IsObjectTypeEditingContext(objectTypeId) && longList1.IndexOf(objectId) < 0)
          {
            longList1.Add(objectId);
            longList2.Add(objectFiD);
          }
        }
        else if (editingContextsObject.ExistsObject(objectFiD))
        {
          if (newVersionIDs.IndexOf(objectId) < 0)
          {
            fIDs.Add(objectFiD);
            newVersionIDs.Add(objectId);
          }
        }
        else if (longList1.IndexOf(objectId) < 0)
        {
          longList1.Add(objectId);
          longList2.Add(objectFiD);
        }
      }
      if (longList1.Count > 0)
      {
        for (int index = 0; index < longList2.Count; ++index)
        {
          try
          {
            customService.AddToContext((object) contextSession.SessionGUID, editingContextsObject.ContextID, editingContextsObject.ModificationID, longList2[index], longList1[index], !editingContextsObject.SimpleContext, true);
          }
          catch
          {
          }
        }
      }
      if (newVersionIDs.Count > 0)
        customService.ReplaceInSimpleContext((object) contextSession.SessionGUID, editingContextsObject.ContextID, editingContextsObject.ModificationID, (IList<long>) fIDs, (IList<long>) newVersionIDs, true);
      if (service == null)
        return;
      service.Progress = service.MaxProgress;
    }
  }
}
