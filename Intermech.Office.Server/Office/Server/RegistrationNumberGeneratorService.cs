// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.RegistrationNumberGeneratorService
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Office.Interfaces;
using System;
using System.Data;

#nullable disable
namespace Intermech.Office.Server;

public class RegistrationNumberGeneratorService : LongLifeObject, IRegistrationNumberGenerator
{
  public string PrivateGenerate(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type,
    long classifierID,
    long unitID)
  {
    return new PrivateRegistrationNumberGenerator(sessionGuid, documentID, docTypeID, type, classifierID, unitID).Generate();
  }

  public string PrivateGenerate(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type,
    long unitID)
  {
    return this.PrivateGenerate(sessionGuid, documentID, docTypeID, type, 0L, unitID);
  }

  public string Generate(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type,
    long classifierID)
  {
    return new GeneralRegistrationNumberGenerator(sessionGuid, documentID, docTypeID, type, classifierID).Generate();
  }

  public bool IsAutoGenerate(
    Guid sessionGuid,
    int docTypeID,
    OfficeDocumentTypes type,
    long unitID)
  {
    RegNumberSettings template = RegistrationNumberHelper.GetTemplate(UserSession.GetSessionByID(sessionGuid), docTypeID, type, unitID);
    return template != null && template.AutoGenerateRegNumber;
  }

  public bool IsEmptyRegNumbersEnabled(
    Guid sessionGuid,
    int docTypeID,
    OfficeDocumentTypes type,
    long unitID)
  {
    RegNumberSettings template = RegistrationNumberHelper.GetTemplate(UserSession.GetSessionByID(sessionGuid), docTypeID, type, unitID);
    return template != null && template.EnableEmptyRegNumbers;
  }

  public bool IsAutoGenerate(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type)
  {
    return this.IsAutoGenerate(sessionGuid, docTypeID, type, 0L);
  }

  public string Generate(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type)
  {
    return this.Generate(sessionGuid, documentID, docTypeID, type, 0L);
  }

  public bool IsClassifierPresent(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type)
  {
    return this.IsClassifierPresent(sessionGuid, docTypeID, type, 0L);
  }

  public bool IsClassifierPresent(
    Guid sessionGuid,
    int docTypeID,
    OfficeDocumentTypes type,
    long unitID)
  {
    RegNumberSettings template = RegistrationNumberHelper.GetTemplate(UserSession.GetSessionByID(sessionGuid), docTypeID, type, unitID);
    return template != null && template.Template.ToUpper().IndexOf("{C}", StringComparison.Ordinal) >= 0;
  }

  public bool ResetCounter(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type)
  {
    IDBAttribute attributeById = UserSession.GetSessionByID(sessionGuid).GetObject(OfficeConsts.ObjectCounterID).GetAttributeByID(OfficeConsts.AttrCountersID);
    if (attributeById == null)
      return false;
    for (int index = 0; index < attributeById.ValuesCount; ++index)
    {
      attributeById.Index = index;
      if (attributeById.AsString != string.Empty)
      {
        CounterValue counterValue = CounterValue.GetValue(attributeById.AsString);
        if (counterValue.DocTypeID == docTypeID && counterValue.OfficeType == type)
        {
          attributeById.DeleteValue();
          return true;
        }
      }
    }
    return false;
  }

  public bool ResetCounter(Guid sessionGuid, int docTypeID, OfficeDocumentTypes type, long unitID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    DataTable dataTable = sessionById.GetObjectCollection(OfficeConsts.ObjtypeContainerID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(OfficeConsts.AttrUnitLinkID, RelationalOperators.Equal, (object) unitID, LogicalOperators.NONE, 0, true)
    }, new object[1]{ (object) -2 }));
    if (dataTable.Rows.Count == 0)
      return false;
    IDBAttribute attributeById = sessionById.GetObject(Convert.ToInt64(dataTable.Rows[0][0])).GetAttributeByID(OfficeConsts.AttrCountersID);
    if (attributeById == null)
      return false;
    for (int index = 0; index < attributeById.ValuesCount; ++index)
    {
      attributeById.Index = index;
      if (attributeById.AsString != string.Empty)
      {
        CounterValue counterValue = CounterValue.GetValue(attributeById.AsString);
        if (counterValue.DocTypeID == docTypeID && counterValue.OfficeType == type)
        {
          attributeById.DeleteValue();
          return true;
        }
      }
    }
    return false;
  }
}
