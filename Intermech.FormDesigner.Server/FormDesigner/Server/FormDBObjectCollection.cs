// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.FormDBObjectCollection
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.FormDesigner.Server;

public class FormDBObjectCollection(UserSession uSession, int objectType) : DBObjectCollection(uSession, objectType)
{
  public DataTable GetAllAttributeValues(int attrID) => this.GetAllValues(attrID);

  public DataTable GetValues(Guid attrGuid)
  {
    return this.GetAllAttributeValues(MetaDataHelper.GetAttributeID((object) attrGuid));
  }
}
