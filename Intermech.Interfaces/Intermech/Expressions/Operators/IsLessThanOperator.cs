
// Type: Intermech.Expressions.Operators.IsLessThanOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>IsLessThan operator.</summary>
    internal class IsLessThanOperator : Operator
    {
      public override object Evaluate(object[] values)
      {
        return values[0].GetType().Equals(typeof (DateTime)) ? (object) (DateTime.Compare(Convert.ToDateTime(values[0]), Convert.ToDateTime(values[1])) < 0) : (object) (Convert.ToDouble(values[0]) < Convert.ToDouble(values[1]));
      }

      internal override OperatorType GetOperatorType() => OperatorType.isLessThanOperator;

      public override Type GetReturnType(Type[] types) => typeof (bool);

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag1 = true;
        if (ExpTypeConverter.CanConvert(types[0], typeof (double)))
        {
          if (!ExpTypeConverter.CanConvert(types[1], typeof (double)))
          {
            flag1 = false;
            invalidArgument = 1;
          }
          return flag1;
        }
        if (types[0].Equals(typeof (DateTime)))
        {
          if (!types[1].Equals(typeof (DateTime)))
          {
            flag1 = false;
            invalidArgument = 1;
          }
          return flag1;
        }
        bool flag2 = false;
        invalidArgument = 0;
        return flag2;
      }

      public override string Name => "<";
    }
}
