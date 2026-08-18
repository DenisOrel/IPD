
// Type: Intermech.Interfaces.CustomServices.ScheduledScriptInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.CustomServices
{
    /// <summary>
    /// Класс для хранения информации о сценарии планировщика задач
    /// </summary>
    [Serializable]
    public class ScheduledScriptInfo : IComparable, IComparable<ScheduledScriptInfo>
    {
      /// <summary>Конструктор</summary>
      /// <param name="scriptGuid"></param>
      /// <param name="scriptName"></param>
      public ScheduledScriptInfo(Guid scriptGuid, string scriptName)
      {
        this.ScriptGuid = !(scriptGuid == Guid.Empty) ? scriptGuid : throw new ArgumentException(nameof (scriptGuid));
        this.ScriptName = scriptName;
      }

      /// <summary>Конструктор</summary>
      /// <param name="dbObject"></param>
      public ScheduledScriptInfo(IDBObject dbObject)
      {
        this.ScriptGuid = dbObject != null ? dbObject.ObjectGUID : throw new ArgumentNullException(nameof (dbObject));
        this.ScriptName = dbObject.Caption;
      }

      /// <summary>Гл. ид. скрипта</summary>
      public Guid ScriptGuid { [DebuggerStepThrough] get; [DebuggerStepThrough] private set; }

      /// <summary>Наименование скрипта</summary>
      public string ScriptName { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode() => this.ScriptGuid.GetHashCode();

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj) => this.CompareTo(obj) == 0;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public int CompareTo(object obj) => this.CompareTo(obj as ScheduledScriptInfo);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public int CompareTo(ScheduledScriptInfo other)
      {
        return other == null ? 1 : this.ScriptGuid.CompareTo(other.ScriptGuid);
      }
    }
}
