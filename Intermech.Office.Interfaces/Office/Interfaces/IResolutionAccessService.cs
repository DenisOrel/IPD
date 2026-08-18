// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.IResolutionAccessService
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Служба прав доступа для поручений.</summary>
public interface IResolutionAccessService
{
  /// <summary>Установить права доступа на поручение.</summary>
  /// <param name="resolutionID">Идентификатор версии поручения.</param>
  bool SetAccess(long resolutionID);

  /// <summary>Возврат поручения на первый шаг ЖЦ.</summary>
  void ReturnResolution(long resolutionID);
}
