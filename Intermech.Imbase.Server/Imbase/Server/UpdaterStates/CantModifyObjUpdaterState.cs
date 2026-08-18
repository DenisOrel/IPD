// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.UpdaterStates.CantModifyObjUpdaterState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Synchronization;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Imbase.Server.UpdaterStates;

internal class CantModifyObjUpdaterState : IObjUpdaterState
{
  public SynchObjectsStatus Handle(SynchronizationAttributesUpdater context)
  {
    context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, "Попытка обновить атрибуты объекта не влияющие на дату модификации.");
    string message;
    if (this.AllAttributesNotContent(context, out message))
    {
      context.State = (IObjUpdaterState) new InBaseObjUpdaterState();
      return context.Update();
    }
    context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, message);
    return SynchObjectsStatus.NotSynchronized;
  }

  private bool AllAttributesNotContent(SynchronizationAttributesUpdater context, out string message)
  {
    message = string.Empty;
    int objType = context.Obj.ObjectType;
    AttributeValues[] array = context.NewAttributeValues.Where<AttributeValues>((Func<AttributeValues, bool>) (x => this.IsContentAttr(objType, x.AttributeID))).ToArray<AttributeValues>();
    if (array.Length == 0)
      return true;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("Следующие атрибуты либо влияют на дату модификации объекта либо нельзя модифицировать без взятия на изменение:");
    foreach (AttributeValues attributeValues in array)
      stringBuilder.AppendLine($" - '{attributeValues.AttributeName}' [{attributeValues.AttributeID}]");
    message = stringBuilder.ToString();
    return false;
  }

  private bool IsContentAttr(int objTypeID, int attrID)
  {
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objTypeID, attrID);
    bool flag;
    if (attribute4ObjectType == null)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
      flag = (attributeType.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase || attributeType.IsContent;
    }
    else
      flag = this.IsContentAttr(attribute4ObjectType);
    return flag;
  }

  private bool IsContentAttr(IMSAttribute4ObjectType imsAttr)
  {
    return (imsAttr.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase || imsAttr.IsContent;
  }
}
