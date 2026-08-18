// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportingAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal abstract class ImportingAttribute : ImportBriefcaseBase
{
  protected string messagePattern;

  public ImportingAttribute(
    UserSession session,
    ImportEventLog eventLog,
    SetImportProgressEventHandler setImportProgressEvent,
    string messagePattern)
    : base(session, eventLog, setImportProgressEvent)
  {
    this.messagePattern = messagePattern;
  }

  protected abstract bool CheckAdded(
    DataTable typesTable,
    int attributeID,
    string attributeName,
    long attributableID,
    int typeID);

  public bool AddAtribute(
    IUserSession session,
    ImportingAttributable attributable,
    long attributableID,
    int typeID,
    string briefcasePath,
    Guid briefcase,
    DataTable attributeTable,
    DataTable typesTable,
    AttributeRecord attr,
    BriefcaseImportProgress bip,
    bool throwException)
  {
    int conformityAttribureType = Helper.GetConformityAttribureType(session as UserSession, attributeTable, attr.AttributeId);
    IDBAttributeType attributeType = session.GetAttributeType(conformityAttribureType, false);
    try
    {
      if (attributeType == null)
        throw new Exception($"Импорт {string.Format(this.messagePattern, (object) attributableID)} атрибут {attr.AttributeId}: {BriefcaseConsts.logAttributeNotFound}");
      if ((attributeType.AttributeType == FieldTypes.ftBlob || attributeType.AttributeType == FieldTypes.ftFile || attributeType.AttributeType == FieldTypes.ftMemo || attributeType.AttributeType == FieldTypes.ftShortBlob) && attr.FileSize != null && Convert.ToInt64(attr.FileSize) > 0L)
      {
        if (attr.Path2File == null || attr.Path2File == string.Empty)
          attr.Path2File = ImportBlob.GetImportingBlobPath(attributableID, attr.AttributeId, (long) attr.IntegerValue, briefcasePath, attributeType.AttributeType);
        if (attr.Path2File == null)
          throw new Exception($"Импорт {this.messagePattern} атрибут {attributeType.Name}" + LocalizationHolder.rm.GetString("Kernel_875") + EnumDescConverter.GetEnumDescription((Enum) attributeType.AttributeType) + LocalizationHolder.rm.GetString("Kernel_876"));
      }
      DataRow dataRow = attributeTable.Rows.Find((object) attr.AttributeId);
      if (!attributeType.IsCompatibleType((FieldTypes) Convert.ToInt32(dataRow["F_ATTRIBUTE_TYPE"])))
        throw new Exception($"Импорт {this.messagePattern} атрибут {attributeType.Name}: несовместимые типы данных!");
      if (!this.CheckAdded(typesTable, attributeType.AttributeID, Convert.ToString(dataRow["F_NAME"]), attributableID, typeID))
        return true;
      attr.AttributeId = conformityAttribureType;
      attributable.AddAttribute(attr);
      return true;
    }
    catch (Exception ex)
    {
      this.eventLog.AddToTrace(ex.Message);
      if (!throwException)
        return true;
      bip.ErrorException = ex;
      bip.Operation = OperationType.Error;
      this.SetImportProgress(briefcase, bip);
      return false;
    }
  }
}
