// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBMeasureAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class DBMeasureAttributeType : DBAttributeType, IDBMeasureAttributeType
{
  public DBMeasureAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftMeasured, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this.CompatibleTypes = new FieldTypes[1]
    {
      FieldTypes.ftMeasured
    };
  }

  public override void ValidateAssign(IDBAttributeType source)
  {
    base.ValidateAssign(source);
    if (this.SizeType > 0L && this.SizeType != source.SizeType)
      throw new KernelExceptionID(sc_12693.ssp_appserver_12694(1775930177), (object) this.Name, (object) source.Name);
  }

  internal override string[] IndexFieldNames
  {
    get
    {
      return new string[1]
      {
        $"F{this.AttributeID.ToString()}ID2"
      };
    }
  }

  internal override string ColumnSQL
  {
    get
    {
      return string.Format("{0} {1}, {0}ID {2}, {0}ID2 {3}", (object) base.ColumnSQL, (object) this.UserSession.DataManager.DataProvider.NVARCHARType(Consts.CastStringSize), (object) this.UserSession.DataManager.DataProvider.INTEGERType, (object) this.UserSession.DataManager.DataProvider.FLOATType);
    }
  }

  public override void ValidateSizeType(long newValue)
  {
    if (newValue <= 0L)
      return;
    IDBObject dbObject = this.UserSession.GetObject(newValue, false);
    if (dbObject == null)
      throw new KernelExceptionID(sc_12693.ssp_appserver_12695(222025368));
    if (dbObject.ObjectType != this.UserSession.IdentHelper.PhysicValueTypeID)
      throw new KernelException(LocalizationHolder.rm.GetString(sc_12693.ssp_appserver_12696()));
  }

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null || !(newValue.ToString() != string.Empty))
      return;
    MeasureHelper.ConvertToMeasuredValue(newValue.ToString());
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    IDbManager dataManager = this.UserSession.DataManager;
    switch (newType)
    {
      case FieldTypes.ftString:
        this.ClearValues("F_DOUBLE_VALUE");
        this.ClearValues("F_INTEGER_VALUE");
        break;
      case FieldTypes.ftInteger:
        List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables.Add("IMS_RELATION_ATTRS");
        objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables.Add("IMS_REL_SNAPATTRS");
        for (int index = 0; index < objectAttrsTables.Count; ++index)
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables[index]} SET F_INTEGER_VALUE = CAST(F_DOUBLE_VALUE AS INTEGER) WHERE F_ATTRIBUTE_ID = {this.AttributeID}");
        this.ClearValues("F_DOUBLE_VALUE");
        break;
      case FieldTypes.ftDouble:
        this.ClearValues("F_INTEGER_VALUE");
        break;
    }
    this.ClearValidatingRule();
  }

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareMeasuredValues(value1, value2);
  }

  public string RuleFormula
  {
    get => string.Empty;
    set => throw new OperationNotApplicableException();
  }

  public long DefaultMeasureID
  {
    get => 0;
    set => throw new OperationNotApplicableException();
  }

  public bool ShortNameInString
  {
    get => true;
    set => throw new OperationNotApplicableException();
  }

  public bool ConvertToDefaultMeasure
  {
    get => false;
    set => throw new OperationNotApplicableException();
  }

  public void ValidateMuID(long muID)
  {
    long[] validPhysicalValues = this.GetValidPhysicalValues();
    if (validPhysicalValues.Length == 0)
      return;
    bool flag = false;
    long physicalQuantityId = MeasureHelper.FindDescriptor(muID).PhysicalQuantityID;
    for (int index = 0; index < validPhysicalValues.Length; ++index)
    {
      if (physicalQuantityId == validPhysicalValues[index])
      {
        flag = true;
        break;
      }
    }
    if (!flag)
    {
      string physicalValuesCaption = this.GetPhysicalValuesCaption(validPhysicalValues);
      throw new KernelExceptionID(sc_12693.ssp_appserver_12697(1444118976), (object) this.Name, (object) physicalValuesCaption);
    }
  }

  public long[] GetValidPhysicalValues()
  {
    if (this.SizeType <= 0L)
      return this.GetMDValuesInt64("OBJ_LINKS_ID");
    return new long[1]{ this.SizeType };
  }

  private string GetPhysicalValuesCaption(long[] guids)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < guids.Length; ++index)
    {
      QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(guids[index]);
      if (!objectInfo.Empty)
        stringBuilder.Append(objectInfo.Caption + ", ");
      else
        stringBuilder.AppendFormat("Object N{0} not found", (object) guids[index].ToString());
    }
    if (stringBuilder.Length > 0)
      stringBuilder.Length -= 2;
    return stringBuilder.ToString();
  }

  public bool IsCompatible(long aMeasureID)
  {
    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(aMeasureID);
    if (descriptor.Empty)
      return false;
    long[] validPhysicalValues = this.GetValidPhysicalValues();
    bool flag = validPhysicalValues.Length == 0;
    for (int index = 0; index < validPhysicalValues.Length; ++index)
    {
      if (descriptor.PhysicalQuantityID == validPhysicalValues[index])
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  protected override void DoGetPropertiesStructure(ref AttributeTypeProperties atProperties)
  {
    long[] mdValuesInt64 = this.GetMDValuesInt64("MU_PHYSICAL_ID");
    if (mdValuesInt64.Length == 0)
      return;
    atProperties.MetadataExtensions[(object) "MU_PHYSICAL_ID"] = (object) mdValuesInt64;
  }

  protected override void DoSetPropertiesStructure(AttributeTypeProperties value)
  {
    object metadataExtension = value.MetadataExtensions[(object) "MU_PHYSICAL_ID"];
    if (metadataExtension == null)
      return;
    long[] valuesList = (long[]) metadataExtension;
    if (valuesList.Length != 0)
      this.SizeType = 0L;
    for (int index = 0; index < valuesList.Length; ++index)
      this.UserSession.GetObject(valuesList[index], true);
    this.SetMDValues("MU_PHYSICAL_ID", 1, valuesList);
  }
}
