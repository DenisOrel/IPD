// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.DBOfficeDocument
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Office.Server;

internal class DBOfficeDocument([NotNull] UserSession session, [NotNull] DataTable objectParams) : 
  DBObject(session, objectParams)
{
  protected override List<long> GetExtendedUserID()
  {
    bool flag = false;
    IDBAttribute attributeById = this.GetAttributeByID(OfficeConsts.AttrAddresseesID);
    if (attributeById != null && attributeById.ValuesCount > 0)
    {
      for (int index = 0; index < attributeById.ValuesCount; ++index)
      {
        if (index > 0)
          attributeById.Index = index;
        if (attributeById.AsInteger == this.UserSession.UserID)
          flag = true;
      }
    }
    if (!flag)
      return base.GetExtendedUserID();
    return ListFactory.Create<long>(new long[1]
    {
      (this.UserSession.GetObject(OfficeConsts.ObjectAddresseeGroupID, false) ?? throw new Exception("Группа пользователей АДРЕСАТ не найдена!")).ObjectID
    });
  }
}
