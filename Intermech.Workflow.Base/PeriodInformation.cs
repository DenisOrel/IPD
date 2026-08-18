// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.PeriodInformation
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Project;
using Intermech.Workflow.Briefcase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Workflow
{
    public class PeriodInformation : BriefcaseAccessor, IValidatedItem
    {
      private TimeUnits _units = TimeUnits.Days;
      private int _unitsCount = 2;
      private int _varTypeID;
      private IUserSession _userSession;
      private string _varName;
      private bool _modified;
      private bool _writeGuids;
      private bool _invalid;

      public IUserSession Session => this._userSession;

      public PeriodInformation(IUserSession session) => this._userSession = session;

      public TimeUnits Units
      {
        get => this._units;
        set
        {
          if (this._units == value)
            return;
          this._units = value;
          this._modified = true;
        }
      }

      public int VarTypeID
      {
        get => this._varTypeID;
        set
        {
          if (this._varTypeID == value)
            return;
          this._varTypeID = value;
          this._varName = (string) null;
          this._modified = true;
        }
      }

      public string VarName
      {
        get
        {
          if (this._varName == null)
          {
            if (this.VarTypeID == 0)
            {
              this._varName = "N/A";
            }
            else
            {
              try
              {
                this._varName = MetaDataHelper.GetAttributeTypeName(this.VarTypeID);
              }
              catch
              {
                this._varName = "";
              }
            }
          }
          return this._varName;
        }
      }

      public int UnitsCount
      {
        get => this._unitsCount;
        set
        {
          if (this._unitsCount == value)
            return;
          this._unitsCount = value;
          this._modified = true;
        }
      }

      public bool Modified
      {
        get => this._modified;
        set => this._modified = value;
      }

      /// <summary>
      /// Записывать гуиды объектов, или нет. Полный формат используется при экспорте в портфель
      /// </summary>
      public bool WriteGuids
      {
        get => this._writeGuids || this.Invalid;
        set => this._writeGuids = value;
      }

      public void BaseSave(XmlTextWriter writer)
      {
        writer.WriteStartElement("Units");
        writer.WriteString(((int) this.Units).ToString());
        writer.WriteEndElement();
        writer.WriteStartElement("Count");
        writer.WriteString(this.UnitsCount.ToString());
        writer.WriteEndElement();
        writer.WriteStartElement("Var");
        writer.WriteString(this.VarTypeID.ToString());
        writer.WriteEndElement();
        if (!this.WriteGuids || this.VarTypeID == 0)
          return;
        writer.WriteStartElement("VarGuid");
        writer.WriteString(SimpleFuncs.AttributeIDToGuid(this.VarTypeID).ToString());
        writer.WriteEndElement();
      }

      public void BaseLoad(XmlTextReader reader)
      {
        reader.ReadStartElement("Units");
        this.Units = (TimeUnits) Convert.ToInt32(reader.ReadString());
        reader.ReadEndElement();
        reader.ReadStartElement("Count");
        this.UnitsCount = Convert.ToInt32(reader.ReadString());
        reader.ReadEndElement();
        try
        {
          reader.ReadStartElement("Var");
          this.VarTypeID = Convert.ToInt32(reader.ReadString());
          reader.ReadEndElement();
        }
        catch
        {
        }
        if (!reader.Read() || reader.NodeType == XmlNodeType.EndElement)
          return;
        reader.ReadStartElement("VarGuid");
        int id = SimpleFuncs.AttributeGuidToID(new Guid(reader.ReadString()));
        if (id != 0)
        {
          this.VarTypeID = id;
          this._invalid = false;
        }
        else
        {
          this._invalid = true;
          if (this.Briefcase != null && this.Briefcase.Map.Get(Domain.Variables, (long) this.VarTypeID) is MapperVariable mapperVariable)
            this._varName = mapperVariable.Caption;
        }
        reader.ReadEndElement();
      }

      public string PeriodText
      {
        get
        {
          return this.VarTypeID == 0 ? SimpleFuncs.UnitsToStr(this.Units, this.UnitsCount) : string.Format(LocalizationHolder.rm.GetString("Workflow.Design_79"), (object) this.VarName);
        }
      }

      public void SaveToStream(Stream stream)
      {
        XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartElement("Period");
        this.BaseSave(writer);
        writer.WriteEndElement();
        writer.Flush();
      }

      public void LoadFromStream(Stream stream)
      {
        if (stream.Length == 0L)
          return;
        XmlTextReader reader = new XmlTextReader(stream);
        reader.ReadStartElement("Period");
        this.BaseLoad(reader);
        reader.Close();
        this._modified = false;
      }

      public string AsString
      {
        get
        {
          MemoryStream ms = new MemoryStream();
          try
          {
            this.SaveToStream((Stream) ms);
            return StreamHelper.StreamToString((Stream) ms);
          }
          finally
          {
            ms.Close();
          }
        }
        set
        {
          MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(value));
          try
          {
            this.LoadFromStream((Stream) memoryStream);
            memoryStream.Close();
          }
          finally
          {
            memoryStream.Close();
          }
        }
      }

      /// <summary>
      /// Вычисляет время выполнения, отсчитывая UnitsCount единиц типа Units от времени fromTime. Учитывает календарь, если он указан в настройках Workflow.
      /// </summary>
      /// <returns></returns>
      public DateTime GetExecTime(DateTime fromTime)
      {
        DateTime execTime1 = DateTime.MinValue;
        if (GlobalMailSettings.Cfg.CalendarID != 0L)
        {
          Schedule schedule = ScheduleList.GetSchedule(GlobalMailSettings.Cfg.CalendarID, this.Session);
          double work = 0.0;
          switch (this.Units)
          {
            case TimeUnits.Minutes:
              work = (double) this.UnitsCount / 60.0;
              break;
            case TimeUnits.Hours:
              work = (double) this.UnitsCount;
              break;
            case TimeUnits.Days:
              work = schedule.DayDuration * (double) this.UnitsCount;
              break;
            case TimeUnits.Weeks:
              work = schedule.WeekDuration * (double) this.UnitsCount;
              break;
            case TimeUnits.Months:
              work = schedule.MonthDuration * (double) this.UnitsCount;
              break;
            case TimeUnits.Years:
              work = 12.0 * schedule.MonthDuration * (double) this.UnitsCount;
              break;
          }
          foreach (DateSchedule dateSchedule in (List<DateSchedule>) schedule.GetWorkTime(fromTime, work))
          {
            if (dateSchedule.FinishTime > execTime1)
              execTime1 = dateSchedule.FinishTime;
          }
          return execTime1;
        }
        DateTime execTime2 = fromTime;
        switch (this.Units)
        {
          case TimeUnits.Minutes:
            execTime2 = execTime2.AddMinutes((double) this.UnitsCount);
            break;
          case TimeUnits.Hours:
            execTime2 = execTime2.AddHours((double) this.UnitsCount);
            break;
          case TimeUnits.Days:
            execTime2 = execTime2.AddDays((double) this.UnitsCount);
            break;
          case TimeUnits.Weeks:
            execTime2 = execTime2.AddDays((double) (7 * this.UnitsCount));
            break;
          case TimeUnits.Months:
            execTime2 = execTime2.AddMonths(this.UnitsCount);
            break;
          case TimeUnits.Years:
            execTime2 = execTime2.AddYears(this.UnitsCount);
            break;
        }
        return execTime2;
      }

      /// <summary>
      /// Вычисляет время выполнения в UTC, отсчитывая от текущего времени. При вычислении периода учитывает календарь, если он указан в настройках Workflow.
      /// </summary>
      public DateTime GetExecTime(IDBObject varSource)
      {
        DateTime minValue = DateTime.MinValue;
        DateTime dateTime;
        if (this.VarTypeID != 0)
        {
          try
          {
            dateTime = (varSource.GetAttributeByID(this.VarTypeID) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_80"), (object) this.VarTypeID))).AsDateTime;
          }
          catch
          {
            return DateTime.MinValue;
          }
        }
        else
          dateTime = this.GetExecTime(DateTime.Now);
        return dateTime.ToUniversalTime();
      }

      /// <summary>Exec time calculated from Now (in UTC)</summary>
      /// <param name="varSourceID">Source object ID for variables</param>
      /// <returns></returns>
      public DateTime GetExecTime(long varSourceID)
      {
        try
        {
          return this.GetExecTime(this.Session.GetObject(varSourceID));
        }
        catch
        {
          return DateTime.MinValue;
        }
      }

      public bool Invalid => this._invalid;
    }
}
