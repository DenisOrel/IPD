// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseUsageHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Server;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Localization;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseUsageHelper
{
  public static bool CanUseImbaseObject(ImbaseObjectCaptionItem imbaseObject, bool showMessage = true)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ApplicabilityStatusEnum applicabilityStatus = ImbaseUsageHelper.GetApplicabilityStatus(sessionKeeper.Session, imbaseObject);
      switch (applicabilityStatus)
      {
        case ApplicabilityStatusEnum.None:
        case ApplicabilityStatusEnum.NoLimit:
          return true;
        case ApplicabilityStatusEnum.ForbiddenUse:
        case ApplicabilityStatusEnum.TotalForbiddenUse:
          if (showMessage)
          {
            int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("PositionForbiddenToUse"), (object) EnumTypeHelper.GetCaption((Enum) applicabilityStatus)), LocalizationHolder.rm.GetString("Imbase.Client_1133"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          return false;
        case ApplicabilityStatusEnum.LimitedUse:
          IImbaseRestrictiveCache service = ServiceUtils.GetService<IImbaseRestrictiveCache>((object) sessionKeeper.Session, true);
          string imbaseInternalKey = ImbaseHelper.MakeInternalImbaseKey(imbaseObject.ObjectInfo.ItemID, imbaseObject.RecordId);
          if (service.Check(sessionKeeper.Session.UserID, imbaseInternalKey))
            return true;
          if (MessageBox.Show(LocalizationHolder.rm.GetString("PositionNotInRestrictionList"), LocalizationHolder.rm.GetString("Imbase.Client_1133"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return false;
          service.Add(sessionKeeper.Session.UserID, imbaseInternalKey);
          return true;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }

  private static ApplicabilityStatusEnum GetApplicabilityStatus(
    IUserSession session,
    ImbaseObjectCaptionItem imbaseObject)
  {
    long itemId = imbaseObject.ObjectInfo.ItemID;
    long recordId = imbaseObject.RecordId;
    string asString;
    if (recordId >= 0L)
    {
      IImbaseServer service = ServiceUtils.GetService<IImbaseServer>((object) session, true);
      string str = $"[-2]={recordId}";
      Guid sessionGuid = session.SessionGUID;
      long objectId = itemId;
      string filter = str;
      string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
      DataTable dataTable;
      ref DataTable local1 = ref dataTable;
      AttributeTypeProperties[] attributeTypePropertiesArray;
      ref AttributeTypeProperties[] local2 = ref attributeTypePropertiesArray;
      ImbaseKeyInfo imbaseKeyInfo;
      ref ImbaseKeyInfo local3 = ref imbaseKeyInfo;
      service.LoadRecords(sessionGuid, objectId, filter, decimalSeparator, out local1, out local2, out local3);
      if (dataTable.Rows.Count == 0)
        return ApplicabilityStatusEnum.None;
      int columnIndex = dataTable.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttID.ToString());
      if (columnIndex == -1)
        return ApplicabilityStatusEnum.None;
      asString = Convert.ToString(dataTable.Rows[0][columnIndex]);
    }
    else
    {
      IDBAttribute attributeById = session.GetObject(itemId, false)?.GetAttributeByID(Intermech.Imbase.Consts.ImbaseUsingAttID);
      if (attributeById == null)
        return ApplicabilityStatusEnum.None;
      asString = attributeById.AsString;
    }
    return !string.IsNullOrEmpty(asString) ? ApplicabilityStatusHelper.GetStatus(asString) : ApplicabilityStatusEnum.None;
  }
}
