
// Type: Intermech.Data.ICommitableObjectThreadState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Data
{
    public interface ICommitableObjectThreadState
    {
      ICommitableObject CommitableObject { get; }

      /// <summary>
      /// Возвращает или задает признак, что фиксация транзакции разрешена.
      /// </summary>
      /// <remarks>
      /// Свойство используется вложенными областями видимости транзакции для запрета фиксации всей транзакции в случае,
      /// если вложенная область видимости не смогла подтвердить свое успешное завершение.
      /// </remarks>
      bool CanCommit { get; set; }
    }
}
