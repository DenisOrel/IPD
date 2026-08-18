
// Type: Intermech.Search.GroupAttributesChanging.CharacterCaseTransformation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.ComponentModel;


namespace Intermech.Search.GroupAttributesChanging
{
    public enum CharacterCaseTransformation
    {
      [Description("Без изменения")] None,
      [Description("все строчные")] LowerCase,
      [Description("ВСЕ ПРОПИСНЫЕ")] UpperCase,
      [Description("Начинать с прописных")] StartWithCapital,
    }
}
