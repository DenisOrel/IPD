// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadObjectProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Intermech.Runtime.ComInterop.Proxies;
using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Базовый класс для всех proxy-объектов CAD-системы, а также вспомогательных объектов.
/// </summary>
public abstract class CadObjectProxy
{
  /// <summary>
  /// Создает обертку для исключения, полученного в результате вызова метода внешнего приложения.
  /// Обертка будет содержат имя вызванного метода и код ошибки.
  /// </summary>
  /// <param name="externalException">Исключение, полученное в результате вызова</param>
  /// <param name="applicationName">Имя внешнего приложения</param>
  /// <param name="methodName">Имя вызванного метода внешнего приложения</param>
  /// <param name="additionalInfo">Дополнительная информация об исключении</param>
  protected ApplicationProxyException WrapExternalMethodCOMException(
    COMException externalException,
    string applicationName,
    string methodName,
    string additionalInfo = null)
  {
    if (externalException == null)
      throw new ArgumentNullException(nameof (externalException));
    if (applicationName == null)
      throw new ArgumentNullException(nameof (applicationName));
    if (methodName == null)
      throw new ArgumentNullException(nameof (methodName));
    string exceptionDetails = this.GetExceptionDetails(externalException);
    return new ApplicationProxyException(this.GetExceptionMessage($"При обращении к методу {methodName} произошла внутренняя ошибка приложения {applicationName}.", exceptionDetails, additionalInfo), (Exception) externalException);
  }

  /// <summary>
  /// Создает обертку для исключения, полученного в результате вызова свойства внешнего приложения.
  /// Обертка будет содержат имя вызванного метода и код ошибки.
  /// </summary>
  /// <param name="externalException">Исключение, полученное в результате вызова</param>
  /// <param name="applicationName">Имя внешнего приложения</param>
  /// <param name="methodName">Имя вызванного свойства внешнего приложения</param>
  /// <param name="additionalInfo">Дополнительная информация об исключении</param>
  protected ApplicationProxyException WrapExternalPropertyCOMException(
    COMException externalException,
    string applicationName,
    string propertyName,
    string additionalInfo = null)
  {
    if (externalException == null)
      throw new ArgumentNullException(nameof (externalException));
    if (applicationName == null)
      throw new ArgumentNullException(nameof (applicationName));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    string exceptionDetails = this.GetExceptionDetails(externalException);
    return new ApplicationProxyException(this.GetExceptionMessage($"При обращении к свойству {propertyName} произошла внутренняя ошибка приложения {applicationName}.", exceptionDetails, additionalInfo), (Exception) externalException);
  }

  /// <summary>Получает детали из передаваемого исключения</summary>
  /// <returns>Сообщение исключения либо его код, если сообщение отсутствует</returns>
  private string GetExceptionDetails(COMException externalException)
  {
    string exceptionDetails = externalException.Message;
    if (string.IsNullOrEmpty(exceptionDetails))
      exceptionDetails = $"Код ошибки HRESULT = 0x{externalException.ErrorCode:X8}.";
    return exceptionDetails;
  }

  /// <summary>Строит сообщение обернутого исключения</summary>
  /// <param name="mainPart">
  /// Основная часть текста исключения,
  /// содержащая информацию о вызываемом методе/свойстве и имени внешнего приложения
  /// </param>
  /// <param name="exceptionDetails">Информация о COM-исключении</param>
  /// <param name="additionalInfo">Опциональная дополнительная информация об исключении</param>
  private string GetExceptionMessage(
    string mainPart,
    string exceptionDetails,
    string additionalInfo = null)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(mainPart);
    stringBuilder.Append(" ");
    stringBuilder.Append(exceptionDetails);
    if (additionalInfo != null)
    {
      stringBuilder.Append(" ");
      stringBuilder.Append($"Дополнительные сведения: {additionalInfo}.");
    }
    return stringBuilder.ToString();
  }
}
