
// Type: Intermech.Search.CompositionContexts.CompositionContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.CompositionContexts
{
    /// <summary>
    /// Класс, содержащий числовое значение контекста и его описание
    /// </summary>
    [Serializable]
    public class CompositionContext
    {
      public long Value { get; private set; }

      public string Description { get; private set; }

      public CompositionContext(long value, string description)
      {
        this.Value = value;
        this.Description = description;
      }
    }
}
