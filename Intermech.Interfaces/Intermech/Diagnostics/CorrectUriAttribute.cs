
// Type: Intermech.Diagnostics.CorrectUriAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>Признак того, что идентификатор, к которому относится данный атрибут, должен описывать реально существующий Uri,
    /// возможно соответствующей указанной схеме (напр. UriScheme.Http для Http адреса). Если схема не указана - она не проверяется</summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class CorrectUriAttribute : Attribute
    {
      [UsedImplicitly]
      private UriScheme Scheme { get; }

      public CorrectUriAttribute(UriScheme scheme = UriScheme.Any) => this.Scheme = scheme;
    }
}
