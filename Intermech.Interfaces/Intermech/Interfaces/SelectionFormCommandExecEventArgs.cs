
// Type: Intermech.Interfaces.SelectionFormCommandExecEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Аргументы на событие, возникающие  при вызове команды в диалоге редактирования условий выборки
    /// </summary>
    public class SelectionFormCommandExecEventArgs : EventArgs
    {
      public SelectionFormCommandExecEventArgs(
        ISelectionForm selectionForm,
        ConditionStructure condition)
      {
        this.SelectionForm = selectionForm;
        this.Condition = condition;
      }

      /// <summary>
      /// Ссылка на форму редактирования условий выборки в которой произошло событие
      /// </summary>
      public ISelectionForm SelectionForm { get; private set; }

      /// <summary>Текущее условие</summary>
      public ConditionStructure Condition { get; private set; }
    }
}
