
// Type: Intermech.RelationKinds
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Вид связи:
    /// 0 - вертикальная (например, состоит из);
    /// 1 - горизонтальная (взаимозаменяемый).
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_197")]
    [Category("Misc")]
    public enum RelationKinds
    {
      /// <summary>Вертикальная связь</summary>
      [CustomDescription("Attribute.Interfaces_198")] Vertical,
      /// <summary>Горизонтальная связь</summary>
      [CustomDescription("Attribute.Interfaces_199")] Horizontal,
    }
}
