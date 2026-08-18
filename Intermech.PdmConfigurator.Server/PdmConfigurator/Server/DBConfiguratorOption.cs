// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.DBConfiguratorOption
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal class DBConfiguratorOption(UserSession uSession, DataTable objectParams) : 
  DBObject(uSession, objectParams),
  IDBConfiguratorOption,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public OptionFlags OptionFlags
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionFlagsID);
      return attributeById != null ? (OptionFlags) DataSetProcessor.GetInt64Value(attributeById.Value, 0L) : OptionFlags.None;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionFlagsID);
      if (attributeById == null)
        return;
      attributeById.Value = (object) (long) value;
    }
  }

  public long OptionCategory
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeCategoryLinkID);
      if (attributeById != null)
      {
        long int64Value = DataSetProcessor.GetInt64Value(attributeById.Value, 0L);
        if (int64Value != 0L)
          return int64Value;
      }
      Intermech.Interfaces.PdmConfigurator.Consts.Initialize(this.Session);
      return Intermech.Interfaces.PdmConfigurator.Consts.objectNoCategoryID;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeCategoryLinkID);
      if (attributeById == null)
        return;
      DataSetProcessor.GetInt64Value(attributeById.Value, 0L);
      attributeById.Value = (object) value;
    }
  }

  public string OptionCode
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionCodeID);
      return attributeById != null ? DataSetProcessor.GetStringValue(attributeById.Value, string.Empty) : string.Empty;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionCodeID);
      if (attributeById == null)
        return;
      if (value == null)
        attributeById.Value = (object) DBNull.Value;
      else
        attributeById.Value = (object) value;
    }
  }

  public string OptionDescription
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad00021-306c-11d8-b4e9-00304f19f545"), false);
      return attributeByGuid != null ? DataSetProcessor.GetStringValue(attributeByGuid.Value, string.Empty) : string.Empty;
    }
    set
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad00021-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid == null)
        return;
      if (value == null)
        attributeByGuid.Value = (object) DBNull.Value;
      else
        attributeByGuid.Value = (object) value;
    }
  }

  public FieldTypes OptionDataType
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionDataTypeID);
      if (attributeById == null)
        return FieldTypes.ftString;
      FieldTypes int64Value = (FieldTypes) DataSetProcessor.GetInt64Value(attributeById.Value, 1L);
      return Helper.ValidDataTypes.IndexOf(int64Value) < 0 ? FieldTypes.ftString : int64Value;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionDataTypeID);
      if (attributeById == null)
        return;
      attributeById.Value = Helper.ValidDataTypes.IndexOf(value) >= 0 ? (object) (long) value : throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("PdmConfigurator.Server_7"), (object) value));
    }
  }

  public OptionValuesCollection OptionValues
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionValuesID);
      if (attributeById == null)
        return new OptionValuesCollection();
      StringBuilder stringBuilder = new StringBuilder();
      if (attributeById.ValuesCount == 1)
      {
        stringBuilder.Append(DataSetProcessor.GetStringValue(attributeById.Value, string.Empty));
      }
      else
      {
        object[] values = attributeById.Values;
        if (values != null)
        {
          for (int index = 0; index < values.Length; ++index)
            stringBuilder.Append(DataSetProcessor.GetStringValue(values[index], string.Empty));
        }
      }
      return new OptionValuesCollection(stringBuilder.ToString());
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionValuesID);
      if (attributeById == null)
        return;
      attributeById.ClearValues();
      if (value == null || value.Count == 0)
        return;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionValuesID);
      List<string> stringList = StringsHelper.SplitString(value.ToString(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionValuesID), (int) attributeType.SizeType);
      attributeById.Values = (object[]) stringList.ToArray();
    }
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    if (attribute.AttributeID == Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionDataTypeID)
    {
      if (this.OptionValues.Count > 0)
        throw new PdmConfiguratorTypeCastExeption(LocalizationHolder.rm.GetString("PdmConfigurator.Server_8"));
      if ((this.OptionFlags & OptionFlags.Obsolete) == OptionFlags.Obsolete)
        throw new PdmConfiguratorTypeCastExeption(LocalizationHolder.rm.GetString("PdmConfigurator.Server_9"));
    }
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
  }
}
