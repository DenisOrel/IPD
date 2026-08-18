// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBSelection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBSelection : DBObject
{
  private int _attributeFindInLocalTypes;
  private int _attributeSelectionType;

  public DBSelection(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._attributeFindInLocalTypes = MetaDataHelper.GetAttributeTypeID("cadd9971-306c-11d8-b4e9-00304f19f545");
    this._attributeSelectionType = MetaDataHelper.GetAttributeTypeID("cad00158-306c-11d8-b4e9-00304f19f545");
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.IncludeInComposition, this.GetDefaultAccess(ActionType.IncludeInComposition));
    this.AccessActions.Add(ActionType.ExcludeFromComposition, this.GetDefaultAccess(ActionType.ExcludeFromComposition));
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
    if (attribute.AttributeID == this._attributeFindInLocalTypes && Convert.ToBoolean(newValue))
    {
      IDBAttribute byGuid = this.Attributes.FindByGUID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
      if ((int) this.Attributes.FindByGUID(new Guid("cad00158-306c-11d8-b4e9-00304f19f545")).AsInteger == 3 && byGuid != null && !byGuid.IsNull)
        throw new Exception("Нельзя включать поиск среди локальных типов для выборки с принадлежностью \"Тип объектов\".");
    }
    else
    {
      if (attribute.AttributeID != this._attributeSelectionType || Convert.ToInt32(newValue) != 3)
        return;
      IDBAttribute byId = this.Attributes.FindByID(this._attributeFindInLocalTypes);
      if (byId != null && byId.AsBoolean)
        throw new Exception("Нельзя установить принадлежность выборки \"Тип объектов\" при включенном поиске среди локальных типов.");
    }
  }
}
