
// Type: Intermech.Interfaces.WebPortal.PublicationReceipt
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>квитанция публикации</summary>
    [Serializable]
    public class PublicationReceipt
    {
      public long ReceiptID { get; set; }

      /// <summary>Тип</summary>
      public ReceiptTypes ReceiptType { get; set; }

      /// <summary>Дата и время создания</summary>
      public DateTime CreateDate { get; set; }

      /// <summary>Создатель</summary>
      public string CreatorUser { get; set; }

      /// <summary>Узел, на котором сформирована квитанция</summary>
      public char CreatorSite { get; set; }

      /// <summary>
      /// Идентификатор действия, в рамках которого сформирована квитанция или Consts.UnknownObjectId
      /// </summary>
      public long ActionID { get; set; }

      /// <summary>
      /// Идентификатор процесса, в рамках которого сформирована квитанция или Consts.UnknownObjectId
      /// </summary>
      public long ProcessID { get; set; }

      /// <summary>Содержимое</summary>
      public byte[] Content { get; set; }

      public PublicationReceipt()
      {
      }

      public PublicationReceipt(
        long receiptID,
        ReceiptTypes receiptType,
        DateTime createDate,
        string creatorUser,
        char creatorSite,
        byte[] content)
        : this(receiptID, receiptType, createDate, creatorUser, creatorSite, 0L, 0L, content)
      {
      }

      public PublicationReceipt(
        long receiptID,
        ReceiptTypes receiptType,
        DateTime createDate,
        string creatorUser,
        char creatorSite,
        long processID,
        long actionID,
        byte[] content)
      {
        this.ReceiptID = receiptID;
        this.ReceiptType = receiptType;
        this.CreateDate = createDate;
        this.CreatorUser = creatorUser;
        this.CreatorSite = creatorSite;
        this.ProcessID = processID;
        this.ActionID = actionID;
        this.Content = content;
      }
    }
}
