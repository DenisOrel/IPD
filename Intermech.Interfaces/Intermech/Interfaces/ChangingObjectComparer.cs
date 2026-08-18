
// Type: Intermech.Interfaces.ChangingObjectComparer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Базовый класс для сравнивателей</summary>
    public class ChangingObjectComparer : IComparer<ChangingObject>
    {
      /// <summary>Сортировать по возрастанию</summary>
      protected bool ascending = true;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="ascending">true - сортировать по возрастанию</param>
      public ChangingObjectComparer(bool ascending) => this.ascending = ascending;

      /// <summary>
      /// Вернуть результат сравнения в зависимости от значения флажка ascending
      /// </summary>
      /// <param name="value">Оригинальный результат сравнения после вызова метода Compare</param>
      /// <returns>Результат сравнения в зависимости от значения флажка ascending</returns>
      public virtual int InvertResult(int value)
      {
        switch (value)
        {
          case -1:
            return this.ascending ? value : 1;
          case 0:
            return value;
          default:
            return this.ascending ? value : -1;
        }
      }

      public virtual int Compare(ChangingObject x, ChangingObject y) => 0;
    }
}
