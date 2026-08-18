
// Type: Intermech.Diagnostics.ObjectExistAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>Признак того, что идентификатор, к которому относится данный атрибут, должен описывать реально существующий объект (не версию)</summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ObjectExistAttribute : Attribute
    {
    }
}
