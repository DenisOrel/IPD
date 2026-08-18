
// Type: Intermech.Expressions.Functions.OrFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Operators;
using Intermech.Localization;


namespace Intermech.Expressions.Functions
{
    /// <summary>Or function.</summary>
    public class OrFunction : AndFunction
    {
      public override object Evaluate(object[] values)
      {
        object obj = values[0];
        for (int index = 1; index <= values.GetUpperBound(0); ++index)
          obj = OrOperator.staticEvaluate(new object[2]
          {
            obj,
            values[index]
          });
        return obj;
      }

      public override string Name => "OR";

      public override FunctionCategory Category => FunctionCategory.Logical;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_676");
    }
}
