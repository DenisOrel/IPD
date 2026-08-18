
// Type: Intermech.Interfaces.IDLinkEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public class IDLinkEventArgs
    {
      public Guid AttributeGUID;
      /// <summary>
      /// Флаг того, что подписчик на событьие определил, является ли атрибут целочисленной ссылкой на объект
      /// </summary>
      public bool Handled;
      /// <summary>
      /// Флаг того, что атрибут является целочисленной ссылкой на объект
      /// </summary>
      public bool IsIDLink;

      public IDLinkEventArgs(Guid guid)
      {
        this.AttributeGUID = guid;
        this.Handled = false;
        this.IsIDLink = false;
      }
    }
}
