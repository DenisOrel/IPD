// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBExternalLinkAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBExternalLinkAttributeType : DBAttributeType
{
  public DBExternalLinkAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftExternalLink, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._CanStorePossibleValues = false;
    this.CompatibleTypes = new FieldTypes[1]
    {
      FieldTypes.ftExternalLink
    };
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

  internal override string ColumnSQL => string.Empty;

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    if (newValue != 0L && (this.UserSession.GetObjectType(this.UserSession.GetObject(newValue).ObjectType) as DBObjectType).GUID != new Guid("cad0000a-306c-11d8-b4e9-00304f19f545"))
      throw new KernelException(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12675()));
  }

  public override string SizeTypeDescription
  {
    get
    {
      return this.SizeType == 0L ? string.Empty : this.UserSession.DBCache.GetObjectInfo(this.UserSession.DataManager, this.SizeType).Caption;
    }
  }

  public override string DefaultValueDescription
  {
    get
    {
      if (this.DefaultValue == null || this.DefaultValue != null && (this.DefaultValue == DBNull.Value || this.DefaultValue.ToString() == string.Empty))
        return base.DefaultValueDescription;
      try
      {
        return this.UserSession.DBCache.GetObjectInfo(this.UserSession.DataManager, Convert.ToInt64(this.DefaultValue)).Caption;
      }
      catch
      {
        return base.DefaultValueDescription;
      }
    }
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    if (newType != FieldTypes.ftString)
      return;
    this.ClearValues("F_INTEGER_VALUE");
  }
}
