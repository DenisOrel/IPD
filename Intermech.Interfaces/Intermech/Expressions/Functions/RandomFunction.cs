
// Type: Intermech.Expressions.Functions.RandomFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Random function.</summary>
    public class RandomFunction : Function
    {
      private static Random _random = new Random();

      public override object Evaluate(object[] values)
      {
        double num = 1.0;
        if (values.Length != 0)
          num = Convert.ToDouble(values[0]);
        return (object) (num * RandomFunction._random.NextDouble());
      }

      public override bool MultArgsSupported(int count) => count == 0 || count == 1;

      public override string Name => "RAND";

      public override FunctionCategory Category => FunctionCategory.Other;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_679");
    }
}
