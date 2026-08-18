// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.TechCardSelectedItemsCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>Базовый контейнер команд контекстного меню TechCard</summary>
internal abstract class TechCardSelectedItemsCommand : ExtendedSelectedItemsCommand
{
  /// <summary>
  /// Описание тек. объекта, для которого выполняется команда
  /// </summary>
  protected ObjInfoItem _selectedObjInfo;
  /// <summary>Список созданных связей</summary>
  protected readonly List<RelObjInfoItem> _createdRelInfoList = new List<RelObjInfoItem>();

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
  }

  /// <summary>Проверка параметров команды</summary>
  /// <returns></returns>
  protected virtual bool ValidateCommandArgs()
  {
    return this.Items != null && this.ContextServices != null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool LoadSelectedObjInfo()
  {
    if (!(this.Items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return false;
    this._selectedObjInfo = (ObjInfoItem) new ObjInfoIDItem(itemData.ObjectID, itemData.ObjectType, itemData.ID);
    return true;
  }

  /// <summary>Загрузка данных для команды</summary>
  /// <returns></returns>
  protected virtual bool LoadCommandInfo() => this.LoadSelectedObjInfo();

  /// <summary>Реализация команды</summary>
  protected abstract bool ExecuteCommand();

  /// <summary>
  /// 
  /// </summary>
  protected virtual void UpdateNotificationQueue()
  {
    List<RelObjInfoItem> list = this._createdRelInfoList.Where<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.ProjInfo == (TypedInfoItem) this._selectedObjInfo)).ToList<RelObjInfoItem>();
    if (list.Count == 0)
      return;
    this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) list.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) list.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => !((TypedInfoItem) item.ProjInfo != (TypedInfoItem) null) ? 0L : item.ProjInfo.ObjectID)).ToList<long>(), (IList<int>) list.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => !((TypedInfoItem) item.ProjInfo != (TypedInfoItem) null) ? -1 : item.ProjInfo.ObjTypeID)).ToList<int>(), (IList<int>) list.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToList<int>()));
  }

  /// <summary>Конструктор</summary>
  /// <param name="name"></param>
  protected TechCardSelectedItemsCommand(string name)
    : base(name)
  {
    this.InitializeData();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    if (!this.ValidateCommandArgs() || !this.LoadCommandInfo() || !this.ExecuteCommand())
      return;
    this.UpdateNotificationQueue();
  }

  /// <summary>Информация о созданных объектах / связях</summary>
  public IEnumerable<RelObjInfoItem> CreatedRelObjInfoList
  {
    get
    {
      List<RelObjInfoItem> createdRelInfoList = this._createdRelInfoList;
      return createdRelInfoList == null ? (IEnumerable<RelObjInfoItem>) null : (IEnumerable<RelObjInfoItem>) createdRelInfoList.ToArray();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="viewServices"></param>
  public static void ClearCheckedItems(IServiceProvider viewServices)
  {
    NavigatorTreeView service = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service == null || service.CheckedNodes.Length == 0)
      return;
    service.CheckedNodesClear();
  }
}
