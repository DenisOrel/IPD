// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ColorizerEvents
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Server;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Params;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

#nullable disable
namespace Intermech.Imbase;

internal class ColorizerEvents
{
  public static Dictionary<long, Color> TableViewColorizer_ColorizeRows(
    IUserSession session,
    AttributeTypeProperties[] properties,
    DataTable dataTable)
  {
    IImbaseParamsService service = ServiceUtils.GetService<IImbaseParamsService>((object) session, true);
    if (service == null)
      return (Dictionary<long, Color>) null;
    if (!service.CommonParams.CheckApplicabilityBeforeCreateComposition)
      return (Dictionary<long, Color>) null;
    if (((IEnumerable<AttributeTypeProperties>) properties).Select<AttributeTypeProperties, Guid>((System.Func<AttributeTypeProperties, Guid>) (x => x.AttributeGuid)).ToList<Guid>().IndexOf(Consts.ImbaseUsingAttGUID) == -1)
      return (Dictionary<long, Color>) null;
    DataColumn column = dataTable.Columns[Consts.ImbaseUsingAttID.ToString()];
    if (column == null)
      return (Dictionary<long, Color>) null;
    Dictionary<long, Color> dictionary = new Dictionary<long, Color>();
    ImbaseUserParams userParams = service.GetUserParams(session.SessionGUID);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[-2.ToString()]);
      ApplicabilityStatusEnum status = ApplicabilityStatusHelper.GetStatus(Convert.ToString(row[column]));
      Color color = Color.Empty;
      switch (status)
      {
        case ApplicabilityStatusEnum.NoLimit:
          color = userParams.TableRecordsApplicabilityColors.NoResrictionsRecColor;
          break;
        case ApplicabilityStatusEnum.ForbiddenUse:
          color = userParams.TableRecordsApplicabilityColors.DenyAddRecordRecColor;
          break;
        case ApplicabilityStatusEnum.LimitedUse:
          color = userParams.TableRecordsApplicabilityColors.DenyAddObjectRecColor;
          break;
        case ApplicabilityStatusEnum.TotalForbiddenUse:
          color = userParams.TableRecordsApplicabilityColors.DenyAllRecColor;
          break;
      }
      if (!Color.Empty.Equals((object) color))
        dictionary[int64] = color;
    }
    return dictionary;
  }
}
