
// Type: Intermech.BoolSrv
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech
{
    public class BoolSrv
    {
      public static string YesNoConvert(bool b) => b ? Consts.YesValue : Consts.NoValue;

      public static bool BoolConvert(string s)
      {
        string upper = s.ToUpper();
        return upper == Consts.YesValue.ToUpper() || upper == Consts.TrueValue.ToUpper();
      }

      public static bool CanBoolConvert(string s)
      {
        string upper = s.ToUpper();
        return upper == Consts.YesValue.ToUpper() || upper == Consts.TrueValue.ToUpper() || upper == Consts.NoValue.ToUpper() || upper == Consts.FalseValue.ToUpper();
      }
    }
}
