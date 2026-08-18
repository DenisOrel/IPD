
// Type: Intermech.Interfaces.Briefcase.BriefcaseLog
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;
using System.IO;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Summary description for BriefcaseLog.</summary>
    public class BriefcaseLog
    {
      private string logName = string.Empty;
      private StreamWriter log;
      private bool fullLog = true;

      public string LogName => this.logName;

      /// <summary>писать полный лог</summary>
      public bool FullLog
      {
        get => this.fullLog;
        set => this.fullLog = value;
      }

      public bool OpenLog(string fileName, bool fullLog)
      {
        if (this.log != null)
          this.CloseLog();
        this.log = new StreamWriter(fileName);
        this.logName = fileName;
        this.fullLog = fullLog;
        return true;
      }

      public void CloseLog()
      {
        if (this.log == null)
          return;
        this.log.Flush();
        this.log.Close();
        this.log = (StreamWriter) null;
        this.logName = string.Empty;
      }

      /// <summary>записать строку</summary>
      /// <param name="s"></param>
      public void WriteString(string s) => this.WriteString("", s, LogFlags.EMPTY);

      /// <summary>записать строку</summary>
      /// <param name="s"></param>
      public void WriteString(string erCode, string s) => this.WriteString(erCode, s, LogFlags.EMPTY);

      /// <summary>
      /// записать строку
      /// запись произвести учитывая флаги
      /// </summary>
      /// <param name="s"></param>
      /// <param name="flags"></param>
      public void WriteString(string s, LogFlags flags) => this.WriteString("", s, flags);

      /// <summary>
      /// записать строку
      /// запись произвести учитывая флаги
      /// </summary>
      /// <param name="s"></param>
      /// <param name="flags"></param>
      public void WriteString(string erCode, string s, LogFlags flags)
      {
        if (this.log == null || !this.fullLog && (flags & LogFlags.INFO) != LogFlags.EMPTY)
          return;
        string str = (flags & LogFlags.DATE) != 0 ? $"{DateTime.Now.ToString((IFormatProvider) CultureInfo.InvariantCulture)}: {s}" : s;
        if (erCode != "")
          str = $"{erCode}: {str}";
        this.log.WriteLine(str);
        this.log.Flush();
      }
    }
}
