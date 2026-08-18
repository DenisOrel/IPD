
// Type: Intermech.Interfaces.SelectionService.StructureCopierStateInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.SelectionService
{
    /// <summary>
    /// Информация о выполняемом копировании структуры выборки/классификатора
    /// </summary>
    [Serializable]
    public class StructureCopierStateInfo : OperationStateInfo
    {
      /// <summary>Список идентификаторов созданных объектов</summary>
      public List<long> CreatedObjectIDs { get; }

      /// <summary>Список идентификаторов созданных связей</summary>
      public List<long> CreatedRelationIDs { get; }

      /// <summary>Ошибка копирования</summary>
      public Exception Exception { get; set; }

      public StructureCopierStateInfo(string operationName)
        : base(operationName)
      {
        this.CreatedObjectIDs = new List<long>();
        this.CreatedRelationIDs = new List<long>();
      }
    }
}
