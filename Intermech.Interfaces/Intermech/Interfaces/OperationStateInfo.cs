
// Type: Intermech.Interfaces.OperationStateInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Информация о выполняемой операции</summary>
    [Serializable]
    public class OperationStateInfo
    {
      public int CurrentUnit;
      public int MaxUnits;
      public string OperationName = string.Empty;
      public OperationStates State;
      public DateTime StartTime;
      public Guid SessionGuid = Guid.Empty;

      public OperationStateInfo(string operationName)
      {
        this.OperationName = operationName;
        this.MaxUnits = 100;
        this.CurrentUnit = 0;
        this.State = OperationStates.Processing;
        this.StartTime = DateTime.UtcNow;
      }

      public void Start(int maxUnits)
      {
        this.MaxUnits = maxUnits;
        this.CurrentUnit = 0;
        this.State = OperationStates.Processing;
        this.StartTime = DateTime.UtcNow;
      }

      public void SetProperties(OperationStateInfo operationState)
      {
        this.CurrentUnit = operationState.CurrentUnit;
        this.MaxUnits = operationState.MaxUnits;
        this.OperationName = operationState.OperationName;
        this.State = operationState.State;
        this.StartTime = operationState.StartTime;
      }
    }
}
