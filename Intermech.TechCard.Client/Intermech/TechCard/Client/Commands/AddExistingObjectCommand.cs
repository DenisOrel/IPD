// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.AddExistingObjectCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Реализация команды контекстного меню "Добавить" (существующих объектов)
/// </summary>
internal class AddExistingObjectCommand : TechCardSelectedItemsCommand
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeId"></param>
  public AddExistingObjectCommand(int objectTypeId)
    : base("add" + (object) objectTypeId)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="name"></param>
  public AddExistingObjectCommand(string name)
    : base(name)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  protected override bool ExecuteCommand()
  {
    if (!(this.Items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID))
      return false;
    int objTypeID = -1;
    try
    {
      objTypeID = Convert.ToInt32(this.AdditionalInfo);
    }
    catch (Exception ex)
    {
      if (!(ex is FormatException))
        throw;
    }
    List<long> longList = TechCardClientConst.SelectObjectsDlg(MetaDataHelper.GetObjectTypeGuid(objTypeID), LocalizationHolder.rm.GetString("TechCard.Client_253"));
    if (longList == null || longList.Count == 0)
      return false;
    IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, true);
    service.Push();
    try
    {
      ObjectCommands.CopyCommand(Services.GetItems(longList.ToArray()), this.ContextServices, this.AdditionalInfo);
      CommandsTable commandsTable = Services.GetCommandsTable(this.Items, this.ContextServices, false);
      if (!commandsTable.Contains("Paste"))
        throw new Exception(LocalizationHolder.rm.GetString("TechCard.PasteCommandNotFound"));
      Services.InvokeCommand("Paste", commandsTable, this.ContextServices);
    }
    finally
    {
      service.Pop();
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  /// <returns></returns>
  public static bool IsAllowCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (additionalInfo == null)
      return false;
    int anObjectType = -1;
    try
    {
      anObjectType = Convert.ToInt32(additionalInfo);
    }
    catch (Exception ex)
    {
      if (!(ex is FormatException))
        throw;
    }
    if (anObjectType == -1)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType4 attributeByGuid = sessionKeeper.Session.GetObjectType(anObjectType, true).Attributes?.GetAttributeByGUID(Intermech.Imbase.Consts.CreateNewObjectAttGUID, false);
      try
      {
        if (attributeByGuid != null && attributeByGuid.DefaultValue != null && attributeByGuid.DefaultValue != DBNull.Value)
        {
          if (!Convert.ToBoolean(attributeByGuid.DefaultValue))
            goto label_15;
        }
        return false;
      }
      catch (Exception ex)
      {
        if (!(ex is FormatException))
          throw;
      }
label_15:
      List<int> objTypeIds;
      return ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, true).GetCreationTypes(sessionKeeper.Session.SessionGUID, out objTypeIds) && objTypeIds != null && !objTypeIds.Contains(anObjectType);
    }
  }
}
