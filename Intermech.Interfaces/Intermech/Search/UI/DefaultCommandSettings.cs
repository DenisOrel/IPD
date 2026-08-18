
// Type: Intermech.Search.UI.DefaultCommandSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.UI
{
    [Serializable]
    public sealed class DefaultCommandSettings
    {
      public DefaultCommandSettings(int objectTypeID, string commandName)
      {
        if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
          throw new ArgumentException();
        if (string.IsNullOrEmpty(commandName))
          throw new ArgumentException();
        this.ObjectTypeID = objectTypeID;
        this.CommandName = commandName;
      }

      public int ObjectTypeID { get; private set; }

      public string CommandName { get; private set; }
    }
}
