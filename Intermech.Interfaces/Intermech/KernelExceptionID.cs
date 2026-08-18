
// Type: Intermech.KernelExceptionID
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Нумерованные ошибки, генерируемые серверной частью ядра системы
    /// </summary>
    [Serializable]
    public class KernelExceptionID : KernelException
    {
      private int _ErrorID;
      private object _Arg1;
      private object _Arg2;
      private object _Arg3;
      private object _Arg4;
      private object _Arg5;

      public KernelExceptionID(int errorID) => this._ErrorID = errorID;

      public KernelExceptionID(int errorID, Exception innerException)
        : base(string.Empty, innerException)
      {
        this._ErrorID = errorID;
      }

      public KernelExceptionID(int errorID, object arg1)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
      }

      public KernelExceptionID(int errorID, object arg1, Exception innerException)
        : base(string.Empty, innerException)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
      }

      public KernelExceptionID(int errorID, object arg1, object arg2)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
        this._Arg2 = arg2;
      }

      public KernelExceptionID(int errorID, object arg1, object arg2, Exception innerException)
        : base(string.Empty, innerException)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
        this._Arg2 = arg2;
      }

      public KernelExceptionID(int errorID, object arg1, object arg2, object arg3)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
        this._Arg2 = arg2;
        this._Arg3 = arg3;
      }

      public KernelExceptionID(int errorID, object arg1, object arg2, object arg3, object arg4)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
        this._Arg2 = arg2;
        this._Arg3 = arg3;
        this._Arg4 = arg4;
      }

      public KernelExceptionID(
        int errorID,
        object arg1,
        object arg2,
        object arg3,
        object arg4,
        object arg5)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
        this._Arg2 = arg2;
        this._Arg3 = arg3;
        this._Arg4 = arg4;
        this._Arg5 = arg5;
      }

      public KernelExceptionID(
        int errorID,
        object arg1,
        object arg2,
        object arg3,
        Exception innerException)
        : base(string.Empty, innerException)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
        this._Arg2 = arg2;
        this._Arg3 = arg3;
      }

      public KernelExceptionID(
        int errorID,
        object arg1,
        object arg2,
        object arg3,
        object arg4,
        Exception innerException)
        : base(string.Empty, innerException)
      {
        this._ErrorID = errorID;
        this._Arg1 = arg1;
        this._Arg2 = arg2;
        this._Arg3 = arg3;
        this._Arg4 = arg4;
      }

      protected KernelExceptionID(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._ErrorID = info.GetInt32(nameof (_ErrorID));
        this._Arg1 = info.GetValue(nameof (_Arg1), typeof (object));
        this._Arg2 = info.GetValue(nameof (_Arg2), typeof (object));
        this._Arg3 = info.GetValue(nameof (_Arg3), typeof (object));
        this._Arg4 = info.GetValue(nameof (_Arg4), typeof (object));
        this._Arg5 = info.GetValue(nameof (_Arg5), typeof (object));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_ErrorID", this._ErrorID);
        info.AddValue("_Arg1", this._Arg1);
        info.AddValue("_Arg2", this._Arg2);
        info.AddValue("_Arg3", this._Arg3);
        info.AddValue("_Arg4", this._Arg4);
        info.AddValue("_Arg5", this._Arg5);
      }

      public override string Message
      {
        get
        {
          string format = KernelErrorMessages.GetErrorMessage(Convert.ToInt32(this._ErrorID));
          if (this._Arg5 != null)
            format = string.Format(format, this._Arg1, this._Arg2, this._Arg3, this._Arg4, this._Arg5);
          else if (this._Arg4 != null)
            format = string.Format(format, this._Arg1, this._Arg2, this._Arg3, this._Arg4);
          else if (this._Arg3 != null)
            format = string.Format(format, this._Arg1, this._Arg2, this._Arg3);
          else if (this._Arg2 != null)
            format = string.Format(format, this._Arg1, this._Arg2);
          else if (this._Arg1 != null)
            format = string.Format(format, this._Arg1);
          return format;
        }
      }

      /// <summary>Номер ошибки</summary>
      public int ErrorID => this._ErrorID;

      public object Argument1 => this._Arg1;

      public object Argument2 => this._Arg2;

      public object Argument3 => this._Arg3;

      public object Argument4 => this._Arg4;

      public object Argument5 => this._Arg5;
    }
}
