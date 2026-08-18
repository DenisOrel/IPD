// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.NumberCounter
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Office.Interfaces;
using System;

#nullable disable
namespace Intermech.Office.Server;

internal static class NumberCounter
{
  public static int NextPrivate(
    int docTypeID,
    OfficeDocumentTypes type,
    CountResetTypes resetType,
    bool countWithinType,
    long unitID,
    int startValue,
    int increment)
  {
    using (SystemSessionKeeper systemSessionKeeper = new SystemSessionKeeper("OfficeServer.NextPrivate"))
    {
      IDBObject container = UnitContainer.GetContainer(systemSessionKeeper.Session, unitID);
      IDBAttribute dbAttribute = container.GetAttributeByID(OfficeConsts.AttrCountersID) ?? container.Attributes.AddAttribute(OfficeConsts.AttrCountersID, false);
      for (int index = 0; index < dbAttribute.ValuesCount; ++index)
      {
        dbAttribute.Index = index;
        if (dbAttribute.AsString != string.Empty)
        {
          CounterValue val = CounterValue.GetValue(dbAttribute.AsString);
          if ((countWithinType && val.DocTypeID == docTypeID || !countWithinType) && val.OfficeType == type)
          {
            if (!countWithinType)
              resetType = systemSessionKeeper.Session.GetCustomService<IOfficeDocumentTypeService>().GetOwnResetModes(systemSessionKeeper.Session.SessionGUID)[type];
            NumberCounter.CalculateValue(val, resetType);
            dbAttribute.AsString = val.ToString();
            return val.Value;
          }
        }
      }
      CounterValue counterValue = new CounterValue(countWithinType ? docTypeID : -1, DateTime.Now, type, startValue, 0L, startValue, increment);
      if (dbAttribute.ValuesCount == 0)
        dbAttribute.Value = (object) counterValue.ToString();
      else
        dbAttribute.AddValue((object) counterValue.ToString());
      return startValue;
    }
  }

  private static void CalculateValue([NotNull] CounterValue val, CountResetTypes resetType)
  {
    DateTime now = DateTime.Now;
    if (resetType == CountResetTypes.PerMonth && val.LastWrite.Month != now.Month && val.LastWrite.Year != now.Year)
      val.Value = val.StartValue;
    else if (resetType == CountResetTypes.PerYear && val.LastWrite.Year != now.Year)
      val.Value = val.StartValue;
    else
      val.Value += val.IncrementValue;
    val.LastWrite = now;
  }

  public static int Next(
    int docTypeID,
    OfficeDocumentTypes type,
    CountResetTypes resetType,
    bool countWithinType,
    bool countWithinUnit,
    long unitID,
    int startValue,
    int increment)
  {
    using (SystemSessionKeeper systemSessionKeeper = new SystemSessionKeeper("OfficeServer.Next"))
    {
      IDBObject dbObject = systemSessionKeeper.Session.GetObject(OfficeConsts.ObjectCounterID);
      IDBAttribute dbAttribute = dbObject.GetAttributeByID(OfficeConsts.AttrCountersID) ?? dbObject.Attributes.AddAttribute(OfficeConsts.AttrCountersID, false);
      for (int index = 0; index < dbAttribute.ValuesCount; ++index)
      {
        dbAttribute.Index = index;
        if (dbAttribute.AsString != string.Empty)
        {
          CounterValue val = CounterValue.GetValue(dbAttribute.AsString);
          if ((countWithinType && val.DocTypeID == docTypeID || !countWithinType) && (countWithinUnit && val.UnitID == unitID || !countWithinUnit) && val.OfficeType == type)
          {
            if (!countWithinUnit && !countWithinType)
              resetType = systemSessionKeeper.Session.GetCustomService<IOfficeDocumentTypeService>().GetOwnResetModes(systemSessionKeeper.Session.SessionGUID)[type];
            NumberCounter.CalculateValue(val, resetType);
            val.LastWrite = DateTime.Now;
            dbAttribute.AsString = val.ToString();
            return val.Value;
          }
        }
      }
      CounterValue counterValue = new CounterValue(countWithinType ? docTypeID : -1, DateTime.Now, type, startValue, unitID, startValue, increment);
      if (dbAttribute.ValuesCount == 0)
        dbAttribute.Value = (object) counterValue.ToString();
      else
        dbAttribute.AddValue((object) counterValue.ToString());
      return startValue;
    }
  }
}
