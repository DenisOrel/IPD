using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.ComparisonPlugins.PDFComparison.Common
{
    internal class HelperConsts
    {
      public static int ObjtypeDocument { get; private set; }

      public static List<int> ComparedObjectTypes { get; private set; }

      public static void Initialize()
      {
        HelperConsts.ObjtypeDocument = MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
        HelperConsts.ComparedObjectTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(HelperConsts.ObjtypeDocument);
      }
    }
}
