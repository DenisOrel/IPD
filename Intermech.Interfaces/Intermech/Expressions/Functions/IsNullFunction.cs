
// Type: Intermech.Expressions.Functions.IsNullFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Expressions.Functions
{
    /// <summary>IsNull function</summary>
    public class IsNullFunction : IsDBNullFunction
    {
      public override string Name => "IsNull";

      public override FunctionCategory Category => FunctionCategory.Other;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_661");
    }
}
