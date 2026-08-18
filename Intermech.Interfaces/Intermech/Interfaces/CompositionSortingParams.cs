
// Type: Intermech.Interfaces.CompositionSortingParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Параметры назначения сортировки</summary>
    [Serializable]
    public class CompositionSortingParams
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="compositionSortingInfo">Описание параметров связи</param>
      /// <param name="targetRelationId"></param>
      public CompositionSortingParams(
        [NotNull] IEnumerable<CompositionSortingProjInfo> compositionSortingInfo,
        long targetRelationId = 0)
      {
        this.CompositionSortingInfo = compositionSortingInfo;
        this.TargetRelationId = targetRelationId;
      }

      /// <summary>Описание параметров связи</summary>
      public IEnumerable<CompositionSortingProjInfo> CompositionSortingInfo { get; private set; }

      /// <summary>Связь - target</summary>
      public long TargetRelationId { get; private set; }
    }
}
