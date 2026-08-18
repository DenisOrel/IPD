// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CMeasuredAttributeType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using ImSSP;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

internal class CMeasuredAttributeType(ClientSession uSession, int aAttributeID) : 
  CAttributeType(uSession, aAttributeID),
  IDBMeasureAttributeType
{
  protected override void DoGetPropertiesStructure(ref AttributeTypeProperties atProperties)
  {
    long[] mdValuesInt64 = this.GetMDValuesInt64("MU_PHYSICAL_ID");
    if (mdValuesInt64.Length == 0)
      return;
    atProperties.MetadataExtensions[(object) "MU_PHYSICAL_ID"] = (object) mdValuesInt64;
  }

  public string RuleFormula
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return string.Empty;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public long DefaultMeasureID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return 0;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public bool ShortNameInString
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return true;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public bool ConvertToDefaultMeasure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return false;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  /// <summary>
  /// Проверяет допустимость присвоения данному атрибуту единицы измерения muID
  /// </summary>
  public void ValidateMuID(long muID)
  {
    this._clientSession.Guard.ValidateCall();
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
      throw new KernelExceptionID(sc_10482.ssp_appserver_10483(1908152628), (object) this.Name, (object) physicalValuesCaption);
    }
  }

  /// <summary>
  /// Возвращает список идентификаторов физических величин, единицы измерения которых можно присваивать данному атрибуту.
  /// Возвращает массив нулевой длины, если атрибуту можно присвоить любую единицу измерения.
  /// </summary>
  public long[] GetValidPhysicalValues()
  {
    this._clientSession.Guard.ValidateCall();
    if (this.SizeType <= 0L)
      return this.GetMDValuesInt64("MU_PHYSICAL_ID");
    return new long[1]{ this.SizeType };
  }

  /// <summary>
  /// Возвращает список наименований допустимых физических величин по их идентификаторам
  /// </summary>
  private string GetPhysicalValuesCaption(long[] guids)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < guids.Length; ++index)
    {
      IDBObject dbObject = this._clientSession.GetObject(guids[index], false);
      if (dbObject != null)
        stringBuilder.Append(dbObject.Caption + ", ");
      else
        stringBuilder.AppendFormat("Object N{0} not found", (object) guids[index].ToString());
    }
    if (stringBuilder.Length > 0)
      stringBuilder.Length -= 2;
    return stringBuilder.ToString();
  }

  /// <summary>Проверяет единицы измерения на совместимость.</summary>
  /// <param name="aMeasureID">Идентификатор объекта-единицы измерения, который
  /// проверяется на совместимость с данным параметром (например, единица массы не
  /// совместима с единицей объема и т.п.). </param>
  public bool IsCompatible(long aMeasureID)
  {
    this._clientSession.Guard.ValidateCall();
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
}
