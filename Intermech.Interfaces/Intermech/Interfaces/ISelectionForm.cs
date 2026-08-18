
// Type: Intermech.Interfaces.ISelectionForm
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс на форму редактирования условий выборки</summary>
    public interface ISelectionForm
    {
      /// <summary>Добавить новые условия</summary>
      /// <param name="cs"></param>
      void Add(ConditionStructure cs);

      /// <summary>Изменить текущее условие</summary>
      /// <param name="cs"></param>
      void Replace(ConditionStructure cs);
    }
}
