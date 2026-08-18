
// Type: Intermech.Interfaces.ObjectChangingAction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Действие, которое выполняется над указанным объектом</summary>
    [Serializable]
    public enum ObjectChangingAction
    {
      /// <summary>Объект берётся на редактирование</summary>
      [Description("CheckOut")] CheckOut,
      /// <summary>Изменения завершаются</summary>
      [Description("CheckIn")] CheckIn,
      /// <summary>
      /// Изменения сохраняются, объект остаётся взят на редактирование
      /// </summary>
      [Description("SaveChanges")] SaveChanges,
      /// <summary>Изменения отменяются</summary>
      [Description("CancelChanges")] CancelChanges,
    }
}
