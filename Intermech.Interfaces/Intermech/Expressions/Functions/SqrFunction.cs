
// Type: Intermech.Expressions.Functions.SqrFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Sqrt function.</summary>
    public class SqrFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double num = Convert.ToDouble(values[0]);
        return (object) (num * num);
      }

      public override string Name => "SQR";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => "sqr(число)\n Возвращает значение квадрта числа";
    }
}
