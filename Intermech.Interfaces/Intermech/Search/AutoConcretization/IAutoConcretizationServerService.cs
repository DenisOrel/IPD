
// Type: Intermech.Search.AutoConcretization.IAutoConcretizationServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.AutoConcretization
{
    public interface IAutoConcretizationServerService
    {
      bool CanModifyCompositionAutoConcretizationAttribute(Guid userSessionGuid, long objectVersionID);

      void EnableAutoConcretization(Guid userSessionGuid, long objectVersionID);

      void DisableAutoConcretization(Guid userSessionGuid, long objectVersionID);

      bool IsAutoConcretizationEnabled(Guid userSessionGuid, IDBObject projObject);
    }
}
