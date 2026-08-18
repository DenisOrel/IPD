// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeTypeCreator
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Класс создает клиентские обработчики типов атрибутов</summary>
internal static class CAttributeTypeCreator
{
  public static CAttributeType CreateCAttributeType(ClientSession uSession, int aAttributeID)
  {
    DataRow dataRow = uSession.ClientCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) aAttributeID);
    if (dataRow == null)
      return new CAttributeType(uSession, aAttributeID);
    CAttributeType cattributeType;
    switch (Convert.ToInt32(dataRow["F_ATTRIBUTE_TYPE"]))
    {
      case 8:
        cattributeType = (CAttributeType) new CObjectLinkAttributeType(uSession, aAttributeID);
        break;
      case 13:
        cattributeType = (CAttributeType) new CMeasuredAttributeType(uSession, aAttributeID);
        break;
      default:
        cattributeType = new CAttributeType(uSession, aAttributeID);
        break;
    }
    return cattributeType;
  }
}
