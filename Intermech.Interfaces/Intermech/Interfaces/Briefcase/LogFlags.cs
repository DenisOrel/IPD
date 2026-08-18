
// Type: Intermech.Interfaces.Briefcase.LogFlags
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Briefcase
{
    [Flags]
    public enum LogFlags
    {
      EMPTY = 0,
      /// <summary>
      /// флаг указывает на информационное сообщение.
      /// такое сообщение пишется в зависимости от BriefcaseLog.FullLog
      /// </summary>
      INFO = 1,
      /// <summary>присоединить дату</summary>
      DATE = 2,
    }
}
