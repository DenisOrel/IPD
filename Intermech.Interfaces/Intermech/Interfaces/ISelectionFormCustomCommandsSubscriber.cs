
// Type: Intermech.Interfaces.ISelectionFormCustomCommandsSubscriber
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейc, который должен реализовать подписчик формы редактировая условий выборки
    /// </summary>
    public interface ISelectionFormCustomCommandsSubscriber
    {
      /// <summary>Дополнительные кнопки в тулбар и контекстное меню</summary>
      List<SelectionFormCommand> Buttons { get; }

      /// <summary>
      /// Доступность элемента управления для команды, дергается при перерисовке доступности элементов управления на форме.
      /// </summary>
      /// <param name="allConditions">Все условия выборки</param>
      /// <param name="current">Условие текущего нода в treeList</param>
      /// <param name="name">Название команды</param>
      /// <param name="handled">Флаг того, что подписчик обработал доступность элемента управления</param>
      /// <returns></returns>
      bool EnableButton(
        ConditionStructure[] allConditions,
        ConditionStructure current,
        string name,
        ref bool handled);

      /// <summary>
      /// Метод вызывается у подписчиков при нажатии кнопки Редактировать
      /// </summary>
      /// <param name="current">Условие текущего нода в treeList</param>
      /// <param name="handled">Флаг того, что подписчик обработал команду Редактировать</param>
      /// <returns></returns>
      ConditionStructure Edit(ConditionStructure current, ref bool handled);
    }
}
