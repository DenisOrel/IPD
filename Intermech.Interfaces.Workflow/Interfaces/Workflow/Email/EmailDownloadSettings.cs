// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.Email.EmailDownloadSettings
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Data;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Workflow.Email;

[Serializable]
public class EmailDownloadSettings
{
  /// <summary>Разрешить автоматический прием почты</summary>
  public bool EnableDownload;
  /// <summary>Имя компьютера</summary>
  public string ComputerName = string.Empty;
  /// <summary>Удалять письма с почтового сервера после получения</summary>
  public bool RemoveMessages;
  /// <summary>Периодичность приема</summary>
  public int Period = 15;
  /// <summary>Принимать почту только в рабочее время</summary>
  public bool WorkTimeOnly = true;
  /// <summary>Идентификатор календаря</summary>
  public Guid CalendarGuid = new Guid("cad01582-306c-11d8-b4e9-00304f19f545");
  private string _moduleName = "Workflow";
  private string _sectionName = "EmailDownload";
  private const string _paramNameComp = "COMPUTER_NAME";
  private const string _paramRemoveMessages = "REMOVE_MESSAGES";
  private const string _paramEnableDownload = "ENABLE";
  private const string _paramPeriod = "PERIOD";
  private const string _paramWorkTimeOnly = "WORKTIME_ONLY";
  private const string _paramCalendarGuid = "CALENDAR";

  public bool Load(IUserSession session)
  {
    DataTable dataTable = session.Configurations.ReadSection(this._moduleName, this._sectionName, 0L);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (dataTable.Rows[index]["F_VALUE"] != DBNull.Value)
      {
        switch (Convert.ToString(dataTable.Rows[index]["F_PARAM_NAME"]))
        {
          case "COMPUTER_NAME":
            this.ComputerName = Convert.ToString(dataTable.Rows[index]["F_VALUE"]);
            continue;
          case "REMOVE_MESSAGES":
            this.RemoveMessages = Convert.ToBoolean(Convert.ToString(dataTable.Rows[index]["F_VALUE"]), (IFormatProvider) CultureInfo.InvariantCulture);
            continue;
          case "ENABLE":
            this.EnableDownload = Convert.ToBoolean(Convert.ToString(dataTable.Rows[index]["F_VALUE"]), (IFormatProvider) CultureInfo.InvariantCulture);
            continue;
          case "WORKTIME_ONLY":
            this.WorkTimeOnly = Convert.ToBoolean(Convert.ToString(dataTable.Rows[index]["F_VALUE"]), (IFormatProvider) CultureInfo.InvariantCulture);
            continue;
          case "CALENDAR":
            this.CalendarGuid = new Guid(Convert.ToString(dataTable.Rows[index]["F_VALUE"]));
            continue;
          case "PERIOD":
            this.Period = Convert.ToInt32(dataTable.Rows[index]["F_VALUE"]);
            continue;
          default:
            continue;
        }
      }
    }
    return true;
  }

  public bool Save(IUserSession session)
  {
    DataTable table = new DataTable();
    table.Columns.Add("F_PARAM_NAME", typeof (string));
    table.Columns.Add("F_VALUE", typeof (string));
    DataRow row1 = table.NewRow();
    row1["F_PARAM_NAME"] = (object) "ENABLE";
    row1["F_VALUE"] = (object) Convert.ToString(this.EnableDownload, (IFormatProvider) CultureInfo.InvariantCulture);
    table.Rows.Add(row1);
    DataRow row2 = table.NewRow();
    row2["F_PARAM_NAME"] = (object) "COMPUTER_NAME";
    row2["F_VALUE"] = (object) this.ComputerName.ToUpper();
    table.Rows.Add(row2);
    DataRow row3 = table.NewRow();
    row3["F_PARAM_NAME"] = (object) "REMOVE_MESSAGES";
    row3["F_VALUE"] = (object) Convert.ToString(this.RemoveMessages, (IFormatProvider) CultureInfo.InvariantCulture);
    table.Rows.Add(row3);
    DataRow row4 = table.NewRow();
    row4["F_PARAM_NAME"] = (object) "PERIOD";
    row4["F_VALUE"] = (object) Convert.ToString(this.Period);
    table.Rows.Add(row4);
    DataRow row5 = table.NewRow();
    row5["F_PARAM_NAME"] = (object) "WORKTIME_ONLY";
    row5["F_VALUE"] = (object) Convert.ToString(this.WorkTimeOnly, (IFormatProvider) CultureInfo.InvariantCulture);
    table.Rows.Add(row5);
    DataRow row6 = table.NewRow();
    row6["F_PARAM_NAME"] = (object) "CALENDAR";
    row6["F_VALUE"] = (object) Convert.ToString((object) this.CalendarGuid);
    table.Rows.Add(row6);
    table.AcceptChanges();
    session.Configurations.WriteSection(this._moduleName, this._sectionName, table, 0L);
    return true;
  }
}
