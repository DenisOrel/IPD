
// Type: Intermech.Interfaces.SelectionInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Информация для выборок</summary>
    public class SelectionInfo : Attribute
    {
      /// <summary>Конструктор</summary>
      /// <param name="type">Типы выборок в которых используется оператор</param>
      /// <param name="options">Опции для оператора</param>
      public SelectionInfo(UsedInSelection type, RelationOperatorOptions options = RelationOperatorOptions.None)
      {
        this.Type = type;
        this.Options = options;
      }

      /// <summary>Типы выборок в которых используется оператор</summary>
      public UsedInSelection Type { get; private set; }

      /// <summary>Является ли оператором входимостей</summary>
      public RelationOperatorOptions Options { get; private set; }
    }
}
