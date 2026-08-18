
// Type: Intermech.Interfaces.Copies.Subscriber
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Copies
{
    /// <summary>Информация о подписчике листа рассылки</summary>
    [Serializable]
    public class Subscriber
    {
      /// <summary>Id подписчика</summary>
      public long ID { get; set; }

      /// <summary>Наименование подписчика</summary>
      public string Caption { get; set; }

      /// <summary>Количество копий, которые надо выслать</summary>
      public int CopyNumber { get; set; }

      /// <summary>Дата подписания подписчика</summary>
      public DateTime SignDate { get; set; }

      /// <summary>ИД подписавшего юзера</summary>
      public long OwnerId { get; set; }

      /// <summary>Наименование подписавшего</summary>
      public string OwnerName { get; set; }

      /// <summary>ИД актуальной копии для подписчика</summary>
      public long ActualCopyId { get; set; }

      /// <summary>Наименование аткуальной копии</summary>
      public string ActualCopyName { get; set; }

      /// <summary>
      /// Тип объекта подписчика (м.б. юзер, подразделение и т.п.)
      /// </summary>
      public int ObjectType { get; set; }

      /// <summary>Примечание. Обычно причина добавления абонента</summary>
      public string Note { get; set; }

      public Subscriber() => this.Note = string.Empty;
    }
}
