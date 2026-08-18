
// Type: Intermech.InTextActionPlacementRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech
{
    /// <summary>
    /// Описатель для найденного фрагмента в тексте сообщения об ошибке, который может быть заменен на гиперссылку с действием по восстановлению после этой ошибки.
    /// Реализация является immutable.
    /// </summary>
    public sealed class InTextActionPlacementRecord
    {
      /// <summary>Создает объект.</summary>
      /// <param name="index">Индекс фрагмента в тексте сообщения об ошибке</param>
      /// <param name="anchorText">Заменяемый фрагмент текста</param>
      /// <param name="actionUri">Гиперссылка</param>
      /// <param name="tag">Дополнительные данные, связанные с фрагментом (параметр может быть не задан)</param>
      public InTextActionPlacementRecord(int index, string anchorText, Uri actionUri, object tag = null)
      {
        if (index < 0)
          throw new ArgumentOutOfRangeException(nameof (index));
        if (string.IsNullOrEmpty(anchorText))
          throw new ArgumentException("The parameter anchorText cannot be null or empty.", nameof (anchorText));
        if (actionUri == (Uri) null)
          throw new ArgumentNullException(nameof (actionUri));
        this.Index = index;
        this.AnchorText = anchorText;
        this.ActionUri = actionUri;
        this.Tag = tag;
      }

      /// <summary>Индекс элемента в тексте сообщения об ошибке</summary>
      public int Index { get; }

      /// <summary>Заменяемый фрагмент текста</summary>
      public string AnchorText { get; }

      /// <summary>Гиперссылка</summary>
      public Uri ActionUri { get; }

      /// <summary>
      /// Дополнительные данные, связанные с фрагментом.
      /// Значение может быть не задано и равно null.
      /// </summary>
      public object Tag { get; }

      /// <summary>
      /// Проверяет, пересекается ли текущий фрагмент с указанным фрагментом.
      /// </summary>
      /// <param name="other">Другой фрагмент</param>
      /// <returns>true - фрагменты пересекаются, false - не пересекаются</returns>
      public bool IsOverlappedWith(InTextActionPlacementRecord other)
      {
        if (other == null)
          throw new ArgumentNullException(nameof (other));
        return this.Index + this.AnchorText.Length - 1 >= other.Index && this.Index <= other.Index + other.AnchorText.Length - 1;
      }

      /// <summary>
      /// Добавляет текущий фрагмент в коллекцию, если он не пересекается ни с каким другим фрагментом.
      /// </summary>
      /// <param name="collection">Коллекция фрагментов</param>
      /// <returns>Признак успешного или неуспешного добавления</returns>
      public bool TryPutIntoCollectionIfNotOverlapped(
        ICollection<InTextActionPlacementRecord> collection)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        foreach (InTextActionPlacementRecord other in (IEnumerable<InTextActionPlacementRecord>) collection)
        {
          if (this.IsOverlappedWith(other))
            return false;
        }
        collection.Add(this);
        return true;
      }
    }
}
