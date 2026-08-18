
// Type: Intermech.Search.Utilities.LifecycleStepHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.Utilities
{
    public static class LifecycleStepHelper
    {
      public static string GetLifecycleStepName(int lifecycleStepID)
      {
        return lifecycleStepID != -1 ? MetaDataHelper.GetLCStepName(lifecycleStepID) : throw new ArgumentException();
      }
    }
}
