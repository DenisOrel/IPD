
// Type: Intermech.Tools.Integrators.DBObjectIntegrationStatus
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Tools.Integrators
{
    public sealed class DBObjectIntegrationStatus(string value) : BitString(value)
    {
      private const int partialObjectStructureBitIndex = 0;
      private const string partialObjectStructureErrorCategory = "ObjectStructure";

      public bool IsEmpty => !this.PartialObjectStructure;

      public bool PartialObjectStructure
      {
        get => this.Read(0);
        set => this.Write(0, value);
      }

      public static string PartialObjectStructureErrorCategory
      {
        [DebuggerStepThrough] get => "ObjectStructure";
      }

      public static int GetBitIndexByErrorCategory(string category)
      {
        switch (category)
        {
          case null:
            throw new ArgumentNullException(nameof (category));
          case "ObjectStructure":
          case "DocumentStructure":
          case "ArticleStructure":
            return 0;
          default:
            throw new NotSupportedException();
        }
      }

      public static IList<int> GetBitIndexesForErrorCategories() => (IList<int>) new int[1];
    }
}
