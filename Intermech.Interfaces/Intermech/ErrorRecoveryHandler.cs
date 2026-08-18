
// Type: Intermech.ErrorRecoveryHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech
{
    /// <summary>
    /// Базовый класс для обработчиков восстановления после ошибок.
    /// Реализация не является thread safe.
    /// </summary>
    public abstract class ErrorRecoveryHandler
    {
      /// <summary>
      /// Размечает текст сообщения об ошибке, определяя элементы текста, которые могуть быть заменены на гиперссылки, запускающие действия по восстановлению после ошибки.
      /// </summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="recoveryActions">Действия по восстановлению</param>
      /// <returns>Коллекция найденных элементов текста, которые могут быть заменены на гиперссылки</returns>
      public List<InTextActionPlacementRecord> PlaceRecoveryActions(
        string message,
        IEnumerable<ErrorRecoveryAction> recoveryActions)
      {
        if (message == null)
          throw new ArgumentNullException(nameof (message));
        if (recoveryActions == null)
          throw new ArgumentNullException(nameof (recoveryActions));
        List<InTextActionPlacementRecord> collection = new List<InTextActionPlacementRecord>();
        foreach (ErrorRecoveryAction recoveryAction in recoveryActions)
        {
          if (recoveryAction != null)
          {
            if (!(recoveryAction is OpenFileRecoveryAction fileRecoveryAction))
            {
              if (recoveryAction is OpenIPSObjectRecoveryAction objectRecoveryAction)
              {
                string anchorText = objectRecoveryAction.ObjectId.ToString();
                for (int index = message.IndexOf(anchorText); index >= 0; index = message.IndexOf(anchorText, index + anchorText.Length))
                  new InTextActionPlacementRecord(index, anchorText, new Uri($"ips://object/{objectRecoveryAction.ObjectId}")).TryPutIntoCollectionIfNotOverlapped((ICollection<InTextActionPlacementRecord>) collection);
              }
            }
            else
            {
              string filePath = fileRecoveryAction.FilePath;
              for (int index = message.IndexOf(filePath); index >= 0; index = message.IndexOf(filePath, index + filePath.Length))
                new InTextActionPlacementRecord(index, filePath, new Uri($"file:///{fileRecoveryAction.FilePath}")).TryPutIntoCollectionIfNotOverlapped((ICollection<InTextActionPlacementRecord>) collection);
            }
          }
        }
        if (collection.Count != 0)
          collection.Sort(new Comparison<InTextActionPlacementRecord>(this.CompareByIndex));
        return collection;
      }

      private int CompareByIndex(InTextActionPlacementRecord x, InTextActionPlacementRecord y)
      {
        return x.Index.CompareTo(y.Index);
      }

      /// <summary>Выполняет действие по восстановлению после ошибки.</summary>
      /// <param name="recoveryUri">Данные для восстановления после ошибки</param>
      /// <returns>Признак успешного/неуспешного выполнения действия</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="recoveryUri" /> содержит null</exception>
      public bool TryInvokeRecoveryAction(Uri recoveryUri)
      {
        return !(recoveryUri == (Uri) null) ? this.DoInvokeRecoveryAction(recoveryUri) : throw new ArgumentNullException(nameof (recoveryUri));
      }

      /// <summary>Выполняет действие по восстановлению после ошибки.</summary>
      /// <param name="recoveryUri">Данные для восстановления после ошибки</param>
      /// <returns>Признак успешного/неуспешного выполнения действия</returns>
      protected virtual bool DoInvokeRecoveryAction(Uri recoveryUri) => false;
    }
}
