
// Type: Intermech.Tools.LaunchActions.LaunchActionInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Tools.LaunchActions
{
    [Serializable]
    public sealed class LaunchActionInfo
    {
      private Guid actionId;
      private Guid handlerId;
      private string displayName;

      public LaunchActionInfo(Guid actionId, Guid handlerId, string displayName)
      {
        if (actionId == Guid.Empty)
          throw new ArgumentException();
        if (handlerId == Guid.Empty)
          throw new ArgumentException();
        if (string.IsNullOrEmpty(displayName))
          throw new ArgumentException();
        this.actionId = actionId;
        this.handlerId = handlerId;
        this.displayName = displayName;
      }

      public Guid ActionId => this.actionId;

      public Guid HandlerId => this.handlerId;

      public string DisplayName => this.displayName;

      public override string ToString() => this.displayName;
    }
}
