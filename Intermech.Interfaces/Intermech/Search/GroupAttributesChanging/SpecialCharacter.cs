
// Type: Intermech.Search.GroupAttributesChanging.SpecialCharacter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.GroupAttributesChanging
{
    public sealed class SpecialCharacter
    {
      public SpecialCharacter(string character, string description)
      {
        this.Character = !string.IsNullOrEmpty(character) ? character : throw new ArgumentException();
        this.Description = description;
      }

      public string Character { get; private set; }

      public string Description { get; private set; }

      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is SpecialCharacter && this.Character == ((SpecialCharacter) obj).Character;
      }

      public override int GetHashCode() => this.Character.GetHashCode();
    }
}
