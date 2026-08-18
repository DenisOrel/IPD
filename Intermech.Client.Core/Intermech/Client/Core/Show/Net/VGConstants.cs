
// Type: Intermech.Client.Core.Show.Net.VGConstants
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Show.Net;

/// <summary> математические константы </summary>
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct VGConstants
{
  /// <summary> приблизительная точность для чисел с плавающей точкой (1E-15)</summary>
  public const double FuzzReal = 1E-15;
  /// <summary> основная точность (1E-09)</summary>
  public const double FuzzGeneral = 1E-09;
  /// <summary> Допуск геометрического совпадения (1E-06)</summary>
  public const double FuzzDistance = 1E-06;
  /// <summary> одна секунда дуги, в градусах </summary>
  public const double FuzzDegrees = 0.00027777777777777778;
  /// <summary> одна секунда дуги, в радианах ()</summary>
  public const double FuzzRadians = 4.84813681109536E-06;
  /// <summary>Геометрическая бесконечность (1E+30)</summary>
  public const double Infinity = 1E+30;
  /// <summary> 1E+15 </summary>
  public const double LargeReal = 1E+15;
  /// <summary> 1E+9 </summary>
  public const double LargeGeneral = 1000000000.0;
  /// <summary> 1E+6 </summary>
  public const double LargeDistance = 1000000.0;

  /// <summary>числа совпадают с погрешностью </summary>
  /// <param name="first">первое число</param>
  /// <param name="second">второе число</param>
  /// <param name="fuzz">погрешность</param>
  /// <returns>true если |first - second| меньше fuzz </returns>
  [return: MarshalAs(UnmanagedType.U1)]
  public static bool FuzzEqual(double first, double second, double fuzz)
  {
    double num = first - second;
    return (num >= 0.0 ? num : num * -1.0) <= fuzz;
  }

  /// <summary>число около нуля с погрешностью  </summary>
  /// <param name="value"> число</param>
  /// <param name="fuzz">погрешность</param>
  /// <returns>true если |value| меньше fuzz </returns>
  [return: MarshalAs(UnmanagedType.U1)]
  public static bool FuzzZero(double value, double fuzz)
  {
    return (value >= 0.0 ? value : value * -1.0) <= fuzz;
  }
}
