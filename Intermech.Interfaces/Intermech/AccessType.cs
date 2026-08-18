
// Type: Intermech.AccessType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech
{
    /// <summary>
    /// Виды доступа (по умолчанию, не разрешено, разрешено, запрещено)
    /// </summary>
    public enum AccessType
    {
      [CustomDescription("AccessTypeDefault")] Default,
      [CustomDescription("AccessTypeNoGrant")] NoGrant,
      [CustomDescription("AccessTypeGrant")] Grant,
      [CustomDescription("AccessTypeDeny")] Deny,
      [CustomDescription("AccessTypeGrantAlways")] GrantAlways,
    }
}
