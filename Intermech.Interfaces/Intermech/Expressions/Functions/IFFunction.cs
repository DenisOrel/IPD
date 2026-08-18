
// Type: Intermech.Expressions.Functions.IFFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Expressions.Functions
{
    /// <summary>If function. ( = IIf function)</summary>
    public class IFFunction : IIFFunction
    {
      public override string Name => "IF";

      public override FunctionCategory Category => FunctionCategory.Logical;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_656");
    }
}
