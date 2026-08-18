// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.CounterValue
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Office.Interfaces;
using System;
using System.Globalization;

#nullable disable
namespace Intermech.Office.Server;

internal sealed class CounterValue
{
  public int DocTypeID;
  public long UnitID;
  public DateTime LastWrite;
  public OfficeDocumentTypes OfficeType;
  public int Value;
  public int StartValue;
  public int IncrementValue;

  [NotNull]
  public static CounterValue GetValue([NotNull] string value)
  {
    try
    {
      string[] strArray = value.Split('|');
      return new CounterValue(Convert.ToInt32(strArray[0]), Convert.ToDateTime(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture), (OfficeDocumentTypes) Convert.ToInt32(strArray[2]), Convert.ToInt32(strArray[3]), strArray.Length >= 5 ? Convert.ToInt64(strArray[4]) : 0L, strArray.Length == 7 ? Convert.ToInt32(strArray[5]) : 1, strArray.Length == 7 ? Convert.ToInt32(strArray[6]) : 1);
    }
    catch
    {
      throw new Exception(Localization.GetString("Office.Server_8"));
    }
  }

  public CounterValue(
    int docTypeID,
    DateTime lastWrite,
    OfficeDocumentTypes officeType,
    int value,
    long unitID)
    : this(docTypeID, lastWrite, officeType, value, unitID, 1, 1)
  {
  }

  public CounterValue(
    int docTypeID,
    DateTime lastWrite,
    OfficeDocumentTypes officeType,
    int value,
    long unitID,
    int startValue,
    int incrementValue)
  {
    this.DocTypeID = docTypeID;
    this.LastWrite = lastWrite;
    this.OfficeType = officeType;
    this.Value = value;
    this.UnitID = unitID;
    this.StartValue = startValue;
    this.IncrementValue = incrementValue;
  }

  public override string ToString()
  {
    return $"{this.DocTypeID}|{this.LastWrite.ToString((IFormatProvider) CultureInfo.InvariantCulture)}|{(int) this.OfficeType}|{this.Value}|{this.UnitID}|{this.StartValue}|{this.IncrementValue}";
  }
}
