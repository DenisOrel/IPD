
// Type: Intermech.Search.GroupAttributesChanging.SpecialCharacters
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Search.GroupAttributesChanging
{
    public static class SpecialCharacters
    {
      public static readonly SpecialCharacter AnyNumber = new SpecialCharacter("*", "Произвольное количество символов");
      public static readonly SpecialCharacter Any = new SpecialCharacter("?", "Любой знак");
      public static readonly SpecialCharacter AnyDigit = new SpecialCharacter("#", "Любая цифра");
      public static readonly SpecialCharacter AnyLetter = new SpecialCharacter("$", "Любая буква");
      public static readonly SpecialCharacter CurrentAttributeValue = new SpecialCharacter("[N]", "Текущее значение атрибута");
      public static readonly SpecialCharacter Counter = new SpecialCharacter("[9999:0:1:1]", "Счетчик");
    }
}
