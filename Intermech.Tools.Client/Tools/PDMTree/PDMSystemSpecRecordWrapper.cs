// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMSystemSpecRecordWrapper
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.Tools.Data;
using Interop.CADInterface;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMSystemSpecRecordWrapper : SingleThreadedObject, IParametersContainer
{
  private readonly PDMSystem pdmSystem;
  private readonly SimpleSpecificationRow specRecord;
  private IPhysicalQuantity specCountCache;
  private IPhysicalQuantity specMassCache;

  public PDMSystemSpecRecordWrapper(PDMSystem pdmSystem, SimpleSpecificationRow specRecord)
  {
    this.pdmSystem = pdmSystem;
    this.specRecord = specRecord;
  }

  public string[] GetParameterNames(bool convertNames)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystemSpecRecordWrapper.GetParameterNames");
    this.pdmSystem.PrepareCall();
    try
    {
      throw new NotSupportedException(LocalizationHolder.rm.GetString("Tools.Client_150"));
    }
    catch (Exception ex)
    {
      this.pdmSystem.ReportException(ex);
      throw;
    }
  }

  public void GetParameters(
    string[] paramNames,
    bool convertNames,
    out object[] paramValues,
    out short[] readOnlyFlags)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystemSpecRecordWrapper.GetParameters");
    this.pdmSystem.PrepareCall();
    try
    {
      paramValues = paramNames != null ? new object[paramNames.Length] : throw new ArgumentNullException(LocalizationHolder.rm.GetString("Tools.Client_151"));
      readOnlyFlags = new short[paramNames.Length];
      for (int index = 0; index < paramNames.Length; ++index)
      {
        paramValues[index] = this.LookupParameterValue(paramNames[index]);
        readOnlyFlags[index] = (short) 1;
      }
    }
    catch (Exception ex)
    {
      this.pdmSystem.ReportException(ex);
      throw;
    }
  }

  private object LookupParameterValue(string name)
  {
    EAttributeID? attributeIdByName = this.pdmSystem.IPSAttributeLocalizer.GetAttributeIDByName(name);
    if (attributeIdByName.HasValue)
    {
      switch (attributeIdByName.Value)
      {
        case EAttributeID.ATTR_Name:
          return (object) this.specRecord.Name;
        case EAttributeID.ATTR_Designation:
          return (object) this.specRecord.Designation;
        case EAttributeID.ATTR_OKPCode:
          return (object) this.specRecord.OKPCode;
        case EAttributeID.ATTR_SPSection:
          return (object) this.specRecord.SectionName;
        case EAttributeID.ATTR_IMBaseKey:
          return (object) this.specRecord.ImbaseKey;
        case EAttributeID.ATTR_Material:
          return (object) this.specRecord.Material;
        case EAttributeID.ATTR_Mass:
          if (this.specRecord.Mass == null)
            return (object) null;
          if (this.specMassCache == null)
            this.specMassCache = (IPhysicalQuantity) this.pdmSystem.PhysicalValues.ToPhysicalQuantity(this.specRecord.Mass);
          return (object) this.specMassCache;
        case EAttributeID.ATTR_Position:
          int result;
          return (object) (int.TryParse(this.specRecord.Position, out result) ? result : 0);
        case EAttributeID.ATTR_Comment:
          return (object) this.specRecord.Note;
        case EAttributeID.ATTR_Quantity:
          if (this.specRecord.Count.MeasureID == IDCache.Default.ItemsMeasure.Id)
            return MathUtils.AlmostEqual(this.specRecord.Count.Value, Math.Truncate(this.specRecord.Count.Value)) ? (object) Convert.ToInt32(this.specRecord.Count.Value) : (object) this.specRecord.Count.Value;
          if (this.specCountCache == null)
            this.specCountCache = (IPhysicalQuantity) this.pdmSystem.PhysicalValues.ToPhysicalQuantity(this.specRecord.Count);
          return (object) this.specCountCache;
        case EAttributeID.ATTR_Modification:
          return (object) this.specRecord.GetProjectDesignationsList();
        case EAttributeID.ATTR_Zone:
          return (object) this.specRecord.Zone;
        case EAttributeID.ATTR_GUID:
          return (object) this.specRecord.OccurenceGuid.ToString("D");
      }
    }
    return (object) null;
  }

  public void SetParameters(string[] paramNames, object[] paramValues, bool convertNames)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystemSpecRecordWrapper.SetParameters");
    this.pdmSystem.PrepareCall();
    try
    {
      throw new NotSupportedException(LocalizationHolder.rm.GetString("Tools.Client_150"));
    }
    catch (Exception ex)
    {
      this.pdmSystem.ReportException(ex);
      throw;
    }
  }

  public void DeleteParameters(string[] paramNames, bool convertValues)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystemSpecRecordWrapper.DeleteParameters");
    this.pdmSystem.PrepareCall();
    try
    {
      throw new NotSupportedException(LocalizationHolder.rm.GetString("Tools.Client_150"));
    }
    catch (Exception ex)
    {
      this.pdmSystem.ReportException(ex);
      throw;
    }
  }
}
