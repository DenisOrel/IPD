
// Type: Intermech.Interfaces.ExceptionEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для передачи информации о исключительной ситуации и о том,
    /// обработано ли это исключение подписчиком
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {
      /// <summary>
      /// 
      /// </summary>
      private readonly Exception _exception;
      /// <summary>
      /// 
      /// </summary>
      private bool _handled;

      public ExceptionEventArgs(Exception e)
      {
        this._exception = e;
        this._handled = false;
      }

      /// <summary>Исключение</summary>
      public Exception Exception => this._exception;

      /// <summary>Исключение обработано подписчиком или нет.</summary>
      public bool Handled
      {
        get => this._handled;
        set => this._handled = value;
      }
    }
}
