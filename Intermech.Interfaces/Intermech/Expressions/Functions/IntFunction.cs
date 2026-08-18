
// Type: Intermech.Expressions.Functions.IntFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Expressions.Functions
{
    /// <summary>Int function. ( = Floor function)</summary>
    public class IntFunction : FloorFunction
    {
      public override string Name => "INT";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_659");
    }
}
