// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADObjectProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime.ComInterop.Proxies;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Удобный базовый класс для всех proxy-объектов CAD-системы, а также вспомогательных объектов.
/// </summary>
public abstract class CADObjectProxy
{
  /// <summary>
  /// Создает обертку для исключения, полученного в результате вызова метода/свойства внешнего приложения.
  /// Обертка будет содержат имя вызванного метода/свойства и код ошибки.
  /// </summary>
  /// <param name="externalException">Исключение, полученное в результате вызова</param>
  /// <param name="methodName">Имя вызванного метода/свойства внешнего приложения</param>
  /// <returns>Исключение-обертка</returns>
  protected ApplicationProxyException WrapExternalException(
    COMException externalException,
    string methodName)
  {
    if (externalException == null)
      throw new ArgumentNullException(nameof (externalException));
    if (methodName == null)
      throw new ArgumentNullException(nameof (methodName));
    string exceptionDetails = externalException.Message;
    if (string.IsNullOrEmpty(exceptionDetails))
      exceptionDetails = $"Код ошибки HRESULT = 0x{externalException.ErrorCode:X8}.";
    return this.WrapExternalException((Exception) externalException, methodName, exceptionDetails);
  }

  /// <summary>
  /// Создает обертку для исключения, полученного в результате вызова метода/свойства внешнего приложения.
  /// Обертка будет содержат имя вызванного метода/свойства и код ошибки.
  /// </summary>
  /// <param name="externalException">Исключение, полученное в результате вызова</param>
  /// <param name="methodName">Имя вызванного метода/свойства внешнего приложения</param>
  /// <param name="exceptionDetails">Дополнительные сведения об ошибке. Может быть не задано или содержать null</param>
  /// <returns>Исключение-обертка</returns>
  protected ApplicationProxyException WrapExternalException(
    Exception externalException,
    string methodName,
    string exceptionDetails)
  {
    if (externalException == null)
      throw new ArgumentNullException(nameof (externalException));
    if (methodName == null)
      throw new ArgumentNullException(nameof (methodName));
    if (string.IsNullOrEmpty(exceptionDetails))
      exceptionDetails = "отсутствуют";
    return new ApplicationProxyException($"Сбой при обращении к COM-интерфейсу. Вызов метода '{methodName}' завершился с ошибкой. Дополнительные сведения: {exceptionDetails}", externalException);
  }
}
