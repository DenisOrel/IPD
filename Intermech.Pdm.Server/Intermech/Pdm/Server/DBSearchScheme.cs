// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.DBSearchScheme
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

internal class DBSearchScheme(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    if (!(this.UserSession.GetCustomService(typeof (ISearchScheme)) is ISearchScheme customService))
      return;
    customService.AddScheme((IUserSession) this.UserSession, this.ObjectID);
  }

  protected override void DoDelete()
  {
    base.DoDelete();
    if (!(this.UserSession.GetCustomService(typeof (ISearchScheme)) is ISearchScheme customService))
      return;
    customService.DeleteScheme((IUserSession) this.UserSession, this.ObjectID);
  }

  protected override void DoPurge(long DeleteMode) => base.DoPurge(DeleteMode);

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
    string str = attribute.AttributeType.GUID.ToString();
    if (!str.Equals("cad0014a-306c-11d8-b4e9-00304f19f545") && !str.Equals("cad00131-306c-11d8-b4e9-00304f19f545") && !str.Equals("cad00d18-306c-11d8-b4e9-00304f19f545") && !str.Equals("cad00020-306c-11d8-b4e9-00304f19f545") || !(this.UserSession.GetCustomService(typeof (ISearchScheme)) is ISearchScheme customService))
      return;
    customService.ChangeScheme((IUserSession) this.UserSession, this.ObjectID);
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
    if ((attribute as IDBGuid).GUID.ToString() == "cad00621-306c-11d8-b4e9-00304f19f545" && this.ObjectType == MetaDataHelper.GetObjectTypeID("cad0012a-306c-11d8-b4e9-00304f19f545") && newValue != null && newValue != DBNull.Value && this.Session.GetObjectInfo((long) newValue).ObjectTypeID != MetaDataHelper.GetObjectTypeID(new Guid("cad00122-306c-11d8-b4e9-00304f19f545")))
      throw new Exception("Условиями фильтрации в Общей схеме поиске может быть только Общая выборка.");
  }

  protected override void DoAfterDeleteAdditionalAttributeValue(
    IDBAttribute attribute,
    AttributeDataTableValue deletedValue)
  {
    base.DoAfterDeleteAdditionalAttributeValue(attribute, deletedValue);
    if (!(this.UserSession.GetCustomService(typeof (ISearchScheme)) is ISearchScheme customService))
      return;
    customService.ChangeScheme((IUserSession) this.UserSession, this.ObjectID);
  }
}
