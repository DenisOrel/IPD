// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectLinkBaseAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel;

internal abstract class DBObjectLinkBaseAttributeType : DBAttributeType
{
  public DBObjectLinkBaseAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
  }

  public override void ValidateAssign(IDBAttributeType source)
  {
    base.ValidateAssign(source);
    if ((source.AttributeType == FieldTypes.ftObjectLink || source.AttributeType == FieldTypes.ftObjectLinkByID) && this.SizeType > 0L && source.SizeType != this.SizeType && !(this.UserSession.GetObjectType((int) this.SizeType) as DBObjectType).IsChildType((int) source.SizeType))
      throw new KernelExceptionID(sc_12706.ssp_appserver_12707(50257900), (object) this.Name, (object) source.Name);
  }

  protected override string GetNullOperator()
  {
    return string.Format("(({0} <= 0) OR ({0} IS NULL))", (object) this._ValueFieldName);
  }

  internal override string[] IndexFieldNames
  {
    get
    {
      return new string[2]
      {
        "F" + this.AttributeID.ToString(),
        $"F{this.AttributeID.ToString()}ID"
      };
    }
  }

  internal override string ColumnSQL
  {
    get
    {
      return string.Format("{0} {1}, {0}ID {2}", (object) base.ColumnSQL, (object) this.UserSession.DataManager.DataProvider.NVARCHARType(Consts.MaxStringSize), (object) this.UserSession.DataManager.DataProvider.INTEGERType);
    }
  }

  public override object DefaultValue
  {
    get
    {
      object defaultValue = base.DefaultValue;
      if (defaultValue == DBNull.Value || defaultValue == null || defaultValue.ToString() == string.Empty)
        return (object) null;
      return defaultValue.ToString() == Consts.CurrentUserFunction ? defaultValue : (object) Convert.ToInt64(defaultValue);
    }
  }

  public override void ValidateSizeType(long newValue)
  {
    if (newValue <= 0L)
      return;
    this.UserSession.GetObjectType(Convert.ToInt32(newValue), true);
  }

  public override string SizeTypeDescription
  {
    get
    {
      return this.SizeType <= 0L ? LocalizationHolder.rm.GetString("Kernel_119") : this.UserSession.GetObjectType(Convert.ToInt32(this.SizeType)).ObjectTypeName;
    }
  }

  internal override bool CompareValues(object value1, object value2)
  {
    if (value1 is string && value1.Equals((object) Consts.CurrentUserFunction))
      return value1.Equals(value2);
    return value2 is string && value2.Equals((object) Consts.CurrentUserFunction) ? value2.Equals(value1) : CompareValuesHelper.CompareIntValues(value1, value2);
  }

  protected override void DoGetPropertiesStructure(ref AttributeTypeProperties atProperties)
  {
    int[] mdValuesInt = this.GetMDValuesInt("OBJ_LINKS_ID");
    if (mdValuesInt.Length == 0)
      return;
    atProperties.MetadataExtensions[(object) "OBJ_LINKS_ID"] = (object) mdValuesInt;
  }

  protected override void DoSetPropertiesStructure(AttributeTypeProperties value)
  {
    object metadataExtension = value.MetadataExtensions[(object) "OBJ_LINKS_ID"];
    if (metadataExtension == null)
      return;
    int[] valuesList = (int[]) metadataExtension;
    if (valuesList.Length != 0)
      this.SizeType = 0L;
    for (int index = 0; index < valuesList.Length; ++index)
      this.UserSession.GetObjectType(valuesList[index], true);
    this.SetMDValues("OBJ_LINKS_ID", 4, valuesList);
  }

  public void ValidateObjectType(int objectTypeID)
  {
    if (objectTypeID == this.UserSession.IdentHelper.objtypeIncompleteObject)
      return;
    int[] validObjectTypes = this.GetValidObjectTypes();
    if (validObjectTypes.Length == 0)
      return;
    bool flag = false;
    for (int index = 0; index < validObjectTypes.Length; ++index)
    {
      if (this.UserSession.DBCache.IsInhertitedFrom(objectTypeID, validObjectTypes[index]))
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new KernelExceptionID(sc_12706.ssp_appserver_12708(26437623), (object) this.Name, (object) this.UserSession.GetObjectType(objectTypeID, true).ObjectTypeName);
  }

  public int[] GetValidObjectTypes()
  {
    if (this.SizeType <= 0L)
      return this.GetMDValuesInt("OBJ_LINKS_ID");
    return new int[1]{ Convert.ToInt32(this.SizeType) };
  }
}
