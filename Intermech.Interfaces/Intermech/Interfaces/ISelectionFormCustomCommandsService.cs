
// Type: Intermech.Interfaces.ISelectionFormCustomCommandsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сервис для добавления команды в форму редактирования условия выборок SelectionForm
    /// </summary>
    public interface ISelectionFormCustomCommandsService
    {
      /// <summary>Регистрация подписчика</summary>
      /// <param name="subscriber"></param>
      void Register(ISelectionFormCustomCommandsSubscriber subscriber);

      /// <summary>Дерегистрация подписчика</summary>
      /// <param name="subscriber"></param>
      void UnRegister(ISelectionFormCustomCommandsSubscriber subscriber);

      /// <summary>Список всех подписчиков</summary>
      List<ISelectionFormCustomCommandsSubscriber> Subscribers { get; }

      /// <summary>
      /// Проверка дступности элемента управления для команды в текущий момент.
      /// </summary>
      /// <param name="allConditions">Все условия выборки</param>
      /// <param name="current">Условие текущего нода в treeList</param>
      /// <param name="name">Название кнопки</param>
      /// <returns></returns>
      bool EnableButton(ConditionStructure[] allConditions, ConditionStructure current, string name);
    }
}
