
// Type: Intermech.Interfaces.ShortAttributeDecriptionList
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Interfaces
{
    /// <summary> Список коротких описаний атрибутов </summary>
    public class ShortAttributeDecriptionList : ArrayList
    {
      public ShortAttributeDecriptionList()
      {
      }

      public ShortAttributeDecriptionList(int capacity)
        : base(capacity)
      {
      }

      public ShortAttributeDecription this[int index]
      {
        get => (ShortAttributeDecription) base[index];
        set => this[index] = (object) value;
      }
    }
}
