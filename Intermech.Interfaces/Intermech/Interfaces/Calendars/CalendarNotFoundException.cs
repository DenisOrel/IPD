
// Type: Intermech.Interfaces.Calendars.CalendarNotFoundException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Не найден календарь</summary>
    [Serializable]
    public class CalendarNotFoundException : 
      KernelException,
      IEquatable<CalendarNotFoundException>,
      ISerializable
    {
      public long CalendarID { get; }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public CalendarNotFoundException([CanBeNull] string customMessage = null)
        : this(0L, customMessage)
      {
      }

      public CalendarNotFoundException([NotEmpty] long calendarID, [CanBeNull] string customMessage = null)
        : base(customMessage ?? CalendarNotFoundException.CreateMessage(calendarID))
      {
        this.CalendarID = calendarID;
      }

      [NotNull]
      private static string CreateMessage(long calendarID)
      {
        return calendarID == 0L ? "Календарь не найден!" : $"Календарь с ID={calendarID} не найден!";
      }

      [SecuritySafeCritical]
      protected CalendarNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.CalendarID = info.GetInt64(nameof (CalendarID));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("CalendarID", this.CalendarID);
      }

      public bool Equals(CalendarNotFoundException other)
      {
        if (other == null)
          return false;
        return this == other || this.CalendarID == other.CalendarID;
      }

      public override bool Equals(object obj)
      {
        if (obj == null)
          return false;
        if (this == obj)
          return true;
        return !(obj.GetType() != this.GetType()) && this.Equals((CalendarNotFoundException) obj);
      }

      public override int GetHashCode() => this.CalendarID.GetHashCode();
    }
}
