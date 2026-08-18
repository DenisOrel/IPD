
// Type: Intermech.Interfaces.MyObjectElementEx
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, хранящий некоторую информацию о версии объекта.
    /// Поле Value - (Int64) - идентификатор версии объекта
    /// </summary>
    public class MyObjectElementEx : MyObjectElement, IAssignable, ICloneable
    {
      /// <summary>Какой-либо флажок для элемента</summary>
      public bool ElementBool;
      /// <summary>Какой-либо флажок для элемента</summary>
      public bool ElementBool2;
      /// <summary>Какой-либо флажок для элемента</summary>
      public bool ElementBool3;
      /// <summary>Int64-идентификатор элемента</summary>
      public long ElementID64;
      /// <summary>Int32-идентификатор элемента</summary>
      public int ElementID32;
      /// <summary>Guid элемента</summary>
      public Guid ElementGuid = Guid.Empty;
      /// <summary>Дополнительные пользовательские данные</summary>
      public ArrayList Tags;

      /// <summary>Создать пустой экземпляр класса</summary>
      public MyObjectElementEx()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="objectID">Идентификатор версии объекта (будет записан в поле Value)</param>
      /// <param name="caption">Заголовок объекта</param>
      /// <param name="tag">Дополнительное значение</param>
      /// <param name="objectType">Тип объекта</param>
      /// <param name="value">Значение элемента</param>
      /// <param name="elementBool">Какой-либо флажок для элемента</param>
      /// <param name="elementBool2">Какой-либо флажок для элемента</param>
      /// <param name="elementBool3">Какой-либо флажок для элемента</param>
      /// <param name="element64">Int64-идентификатор элемента</param>
      /// <param name="element32">Int32-идентификатор элемента</param>
      /// <param name="elementGuid">Guid элемента</param>
      /// <param name="tags">Пользовательские данные</param>
      public MyObjectElementEx(
        long objectID,
        string caption,
        object tag,
        int objectType,
        object value,
        bool elementBool,
        bool elementBool2,
        bool elementBool3,
        long element64,
        int element32,
        Guid elementGuid,
        params object[] tags)
        : base(objectID, caption, tag, objectType)
      {
        this.Value = value;
        this.ElementBool = elementBool;
        this.ElementBool2 = elementBool2;
        this.ElementBool3 = elementBool3;
        this.ElementID64 = element64;
        this.ElementID32 = element32;
        this.ElementGuid = elementGuid;
        this.Tags = new ArrayList();
        if (tags == null || tags.Length == 0)
          return;
        for (int index = 0; index < tags.Length; ++index)
          this.Tags.Add(tags[index]);
      }

      /// <summary>Очистить поля класса</summary>
      public override void Clear()
      {
        base.Clear();
        this.Value = (object) null;
        this.ElementBool = false;
        this.ElementBool2 = false;
        this.ElementBool3 = false;
        this.ElementID64 = 0L;
        this.ElementID32 = 0;
        this.ElementGuid = Guid.Empty;
        this.Tags = new ArrayList();
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public override void Assign(object source)
      {
        if (this == source)
          return;
        base.Assign(source);
        if (!(source is MyObjectElementEx myObjectElementEx))
          return;
        this.Value = myObjectElementEx.Value;
        this.ElementBool = myObjectElementEx.ElementBool;
        this.ElementBool2 = myObjectElementEx.ElementBool2;
        this.ElementBool3 = myObjectElementEx.ElementBool3;
        this.ElementID64 = myObjectElementEx.ElementID64;
        this.ElementID32 = myObjectElementEx.ElementID32;
        this.ElementGuid = myObjectElementEx.ElementGuid;
        this.Tags = new ArrayList();
        if (myObjectElementEx.Tags == null || myObjectElementEx.Tags.Count <= 0)
          return;
        for (int index = 0; index < myObjectElementEx.Tags.Count; ++index)
          this.Tags.Add(myObjectElementEx.Tags[index]);
      }
    }
}
