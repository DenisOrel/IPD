
// Type: Intermech.Expressions.Functions.CaptionFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Len function.</summary>
    public class CaptionFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        string g = Convert.ToString(values[0]);
        if (string.IsNullOrWhiteSpace(g))
          return (object) string.Empty;
        string empty = string.Empty;
        string str;
        try
        {
          str = !(ApplicationServices.Container.GetService(typeof (IObjectsInfoCache)) is IObjectsInfoCache service) ? "Service IObjectsInfoCache not found" : service.GetObjectCaption(new Guid(g));
        }
        catch (Exception ex)
        {
          str = ex.Message;
        }
        return (object) str;
      }

      public override Type GetReturnType(Type[] types) => typeof (string);

      protected override bool InputTypeSupported(Type type, int index)
      {
        return index == 0 && type.Equals(typeof (string));
      }

      public override bool IsNullable(object[] values) => false;

      public override string Name => "CAPTION";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description
      {
        get
        {
          return "caption(guid объекта)\nВозвращает заголовок объекта, GUID версии которого передается в качестве аргумента или текст сообщения об ошибке";
        }
      }
    }
}
