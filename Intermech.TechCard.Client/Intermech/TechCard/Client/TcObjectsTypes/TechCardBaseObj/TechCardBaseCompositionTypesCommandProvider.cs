// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardBaseCompositionTypesCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>
/// Базовый провайдер для добавления команд контекстного меню соотв. допустимым дочерним типам объектов
/// </summary>
internal abstract class TechCardBaseCompositionTypesCommandProvider : ICommandsProvider
{
  /// <summary>
  /// Получение допустимых типов объектов для связей / родительских типов
  /// </summary>
  /// <param name="relTypeIds"></param>
  /// <param name="rootObjTypeIds"></param>
  /// <returns></returns>
  protected List<IMSObjectType> GetAllPossibleTypes4Command(int[] relTypeIds, int[] rootObjTypeIds = null)
  {
    List<int> allObjectTypes = new List<int>();
    if (rootObjTypeIds != null)
    {
      foreach (int rootObjTypeId in rootObjTypeIds)
      {
        if (rootObjTypeId == -1)
        {
          allObjectTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypesList().Select<IMSObjectType, int>((System.Func<IMSObjectType, int>) (item => item.ObjectTypeID)).ToList<int>());
        }
        else
        {
          allObjectTypes.Add(rootObjTypeId);
          allObjectTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(rootObjTypeId));
        }
      }
    }
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable[] cacheTables = sessionKeeper.Session.GetCacheTables("IMS_TYPES_APPLICABILITY");
      if (cacheTables != null)
      {
        if (cacheTables.Length != 0)
        {
          DataTable dataTable = cacheTables[0];
          int columnIndex1 = dataTable.Columns.IndexOf("F_RELATION_TYPE");
          int columnIndex2 = dataTable.Columns.IndexOf("F_OBJECT_TYPE");
          if (columnIndex1 != -1)
          {
            for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
            {
              int int32 = Convert.ToInt32(dataTable.Rows[index][columnIndex1]);
              if (Array.IndexOf<int>(relTypeIds, int32) != -1)
                intList.Add(Convert.ToInt32(dataTable.Rows[index][columnIndex2]));
            }
          }
        }
      }
    }
    allObjectTypes.AddRange((IEnumerable<int>) intList);
    allObjectTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) intList));
    List<IMSObjectType> imsObjectTypeList = TechCardBaseCompositionTypesCommandProvider.BuildAlphabeticList(allObjectTypes);
    List<IMSObjectType> possibleTypes4Command = new List<IMSObjectType>();
    if (imsObjectTypeList != null)
    {
      List<int> visibleObjTypes = TechcardClientUtils.ObjectTypes.GetVisibleObjTypes();
      foreach (IMSObjectType imsObjectType in imsObjectTypeList)
      {
        if (imsObjectType != null && visibleObjTypes.BinarySearch(imsObjectType.ObjectTypeID) >= 0)
          possibleTypes4Command.Add(imsObjectType);
      }
    }
    return possibleTypes4Command;
  }

  /// <summary>Сортировка типов объектов по их наименованию</summary>
  /// <param name="allObjectTypes"></param>
  /// <returns></returns>
  private static List<IMSObjectType> BuildAlphabeticList(List<int> allObjectTypes)
  {
    if (allObjectTypes == null)
      return (List<IMSObjectType>) null;
    List<IMSObjectType> imsObjectTypeList = new List<IMSObjectType>(allObjectTypes.Count);
    allObjectTypes.Sort();
    for (int index = allObjectTypes.Count - 1; index > 0; --index)
    {
      if (allObjectTypes[index] == allObjectTypes[index - 1])
        allObjectTypes.RemoveAt(index);
    }
    foreach (int allObjectType in allObjectTypes)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(allObjectType);
      if (objectType != null && objectType.VersionsMode != ObjectVersionModes.Abstract)
        imsObjectTypeList.Add(objectType);
    }
    imsObjectTypeList.Sort();
    return imsObjectTypeList;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public abstract CommandsInfo GetMergedCommands(
    ISelectedItems items,
    IServiceProvider viewServices);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public abstract CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices);
}
