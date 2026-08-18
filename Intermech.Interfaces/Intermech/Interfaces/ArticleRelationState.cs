
// Type: Intermech.Interfaces.ArticleRelationState
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Вид связи в составе исполнения</summary>
    [Serializable]
    public enum ArticleRelationState
    {
      /// <summary>Неизвестный тип</summary>
      Unknown,
      /// <summary>Связь принадлежит общей части исполнения</summary>
      CommonPart,
      /// <summary>Связь принадлежит переменной части исполнения</summary>
      VariablePart,
    }
}
