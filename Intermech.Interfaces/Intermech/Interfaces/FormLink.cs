
// Type: Intermech.Interfaces.FormLink
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс ссылки на форму.
    /// Для каждого различного типа ссылки нужно создавать новый класс, который наследуется от этого класса.
    /// </summary>
    public abstract class FormLink : ICloneable
    {
      /// <summary>Идентификатор провайдера.</summary>
      public Guid ProviderGuid = Guid.Empty;

      /// <summary>Атрибуты.</summary>
      public virtual List<int> Attributes => (List<int>) null;

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public abstract object Clone();
    }
}
