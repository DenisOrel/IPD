
// Type: Intermech.Interfaces.Dictionary.IDictionaryService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Dictionary
{
    /// <summary>Interface for dictionary service</summary>
    public interface IDictionaryService
    {
      /// <summary>Load language info</summary>
      /// <param name="session">User session's guid</param>
      /// <returns></returns>
      LangHelper[] LoadLanguages(Guid session);

      /// <summary>Save language info</summary>
      /// <param name="langHelpers"></param>
      /// <param name="session">User session's guid</param>
      void SaveLanguages(LangHelper[] langHelpers, Guid session);
    }
}
