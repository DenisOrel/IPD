
// Type: Intermech.Interfaces.Briefcase.ExportAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Briefcase
{
    [Serializable]
    public struct ExportAttribute(int aCategory, object[] aIdentifiers)
    {
      private int category = aCategory;
      private object[] identifiers = aIdentifiers;

      public int Category
      {
        get => this.category;
        set => this.category = value;
      }

      public object[] Identifiers
      {
        get => this.identifiers;
        set => this.identifiers = value;
      }
    }
}
