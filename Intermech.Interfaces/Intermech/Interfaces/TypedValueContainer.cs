
// Type: Intermech.Interfaces.TypedValueContainer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Универсальный класс для хранения данных параметра системы
    /// </summary>
    [Serializable]
    public class TypedValueContainer
    {
      protected object value;

      public object Value
      {
        get => this.value;
        set => this.AssignValue(value, this.Type);
      }

      public Type Type { get; set; } = typeof (object);

      public TypedValueContainer()
      {
      }

      public TypedValueContainer(object value, Type type)
      {
        this.AssignValue(value, type);
        this.Type = type;
      }

      protected virtual void AssignValue(object newvalue, Type type)
      {
        switch (newvalue)
        {
          case TypedValueContainer typedValueContainer:
            this.AssignValue(typedValueContainer.Value, typedValueContainer.Type);
            break;
          case string inValue when type != typeof (string) && type != typeof (object):
            this.value = TypedValueContainer.ParseStringValue(type, inValue);
            break;
          case Enum _:
            this.value = (object) (int) newvalue;
            break;
          default:
            this.value = newvalue;
            break;
        }
      }

      protected static object ParseStringValue(Type t, string inValue)
      {
        try
        {
          return TypeDescriptor.GetConverter(t).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) inValue);
        }
        catch
        {
          throw new ParseException($"Невозможно привести '{inValue}' к типу '{t}'");
        }
      }
    }
}
