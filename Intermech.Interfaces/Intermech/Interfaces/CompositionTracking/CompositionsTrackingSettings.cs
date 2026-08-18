
// Type: Intermech.Interfaces.CompositionTracking.CompositionsTrackingSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>Composition tracking settings</summary>
    /// <remarks>Для возможности расширения настроек храним все внутри класса</remarks>
    [Serializable]
    public class CompositionsTrackingSettings : IEquatable<CompositionsTrackingSettings>
    {
      /// <summary>Composition tracking commands</summary>
      private CompositionTrackingCommands _commands;
      /// <summary>Режим обработки дочерних объектов</summary>
      private CompositionTrackingObjMode _objMode = CompositionTrackingObjMode.ctomProceed;

      /// <summary>Конструктор</summary>
      public CompositionsTrackingSettings()
        : this(CompositionTrackingCommands.ctcNone)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="commands"></param>
      public CompositionsTrackingSettings(CompositionTrackingCommands commands)
        : this(commands, CompositionTrackingObjMode.ctomProceed)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="commands"></param>
      /// <param name="objMode"></param>
      public CompositionsTrackingSettings(
        CompositionTrackingCommands commands,
        CompositionTrackingObjMode objMode)
      {
        this._commands = commands;
        this._objMode = objMode;
      }

      /// <summary>Composition tracking commands</summary>
      public CompositionTrackingCommands Commands
      {
        [DebuggerStepThrough] get => this._commands;
        [DebuggerStepThrough] set => this._commands = value;
      }

      /// <summary>Composition obj mode</summary>
      public CompositionTrackingObjMode ObjMode
      {
        [DebuggerStepThrough] get => this._objMode;
        [DebuggerStepThrough] set => this._objMode = value;
      }

      /// <summary>Is empty settings</summary>
      public bool IsEmpty
      {
        get
        {
          return this._commands == CompositionTrackingCommands.ctcNone && this._objMode == CompositionTrackingObjMode.ctcNone;
        }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(CompositionsTrackingSettings other)
      {
        if (other == null)
          return this.IsEmpty;
        return this.ObjMode == other.ObjMode && this.Commands == other.Commands;
      }
    }
}
