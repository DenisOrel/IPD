
// Type: Intermech.Search.EditingContexts.AddObjectsToEditingContextResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Contexts;
using System;
using System.Collections.Generic;


namespace Intermech.Search.EditingContexts
{
    [Serializable]
    public sealed class AddObjectsToEditingContextResult
    {
      public AddObjectsToEditingContextResult()
      {
        this.EditingContextLogEnties = new List<EditingContextsLogEntry>();
      }

      public List<EditingContextsLogEntry> EditingContextLogEnties { get; private set; }

      public int AddedObjectsCount { get; set; }

      public int SkippedObjectsCount { get; set; }
    }
}
