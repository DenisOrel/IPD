
// Type: Intermech.FreezableObject
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Базовый класс для объектов, которые позволяют замораживать свое внутреннее состояние. Эту возможность удобно использоваться в тех случаях,
    /// когда требуется предоставить доступ к объекту на чтение нескольким потокам.
    /// </summary>
    [Serializable]
    public abstract class FreezableObject
    {
      [OptionalField]
      private bool isFrozen;

      /// <summary>Возвращает признак, что состояние объекта заморожено.</summary>
      public bool IsFrozen => this.isFrozen;

      /// <summary>
      /// Проверяет корректность состояния объекта перед заморозкой. Если состояние объекта уже заморожено, то метод завершает выполнение без ошибок.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта не корректно и не может быть заморожено</exception>
      public void ValidateBeforeFreeze()
      {
        if (this.isFrozen)
          return;
        this.DoValidate();
      }

      /// <summary>Замораживает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта не корректно и не может быть заморожено</exception>
      public void Freeze()
      {
        if (this.isFrozen)
          return;
        this.DoValidate();
        this.DoFreeze();
        this.isFrozen = true;
      }

      /// <summary>Реализует заморозку состояния объекта.</summary>
      protected virtual void DoFreeze()
      {
      }

      /// <summary>
      /// Позволяет проверить корректность состояния объекта перед заморозкой.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта не корректно и не может быть заморожено</exception>
      protected virtual void DoValidate()
      {
      }

      /// <summary>
      /// Позволяет убедиться, что состояние объекта заморожено.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта не было заморожено</exception>
      public void RequireFrozen()
      {
        if (!this.IsFrozen)
          throw new InvalidOperationException($"Состояние объекта '{this.GetType()}' должно быть заморожено.");
      }

      /// <summary>
      /// Позволяет убедиться, что состояние объекта доступно для изменения.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не доступно для изменения</exception>
      protected void RequireNotFrozen()
      {
        if (this.isFrozen)
          throw new InvalidOperationException($"Невозможно изменить состояние объекта '{this.GetType()}', так как оно заморожено.");
      }

      /// <summary>
      /// Позволяет убедиться, что состояние объекта доступно для изменения.
      /// </summary>
      /// <param name="propertyName">Имя свойства, которое должно быть изменено</param>
      /// <exception cref="T:System.ArgumentNullException">Не указано имя изменяемого свойства</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не доступно для изменения</exception>
      protected void RequireNotFrozenBeforePropertyChange([CallerMemberName] string propertyName = null)
      {
        if (propertyName == null)
          throw new ArgumentNullException(nameof (propertyName));
        if (this.isFrozen)
          throw new InvalidOperationException($"Невозможно изменить свойство '{propertyName}' у объекта '{this.GetType()}', так как состояние объекта заморожено.");
      }
    }
}
