// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Links.FormDesignerFormLinksManager
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.FormDesigner.Links;

/// <summary>
/// 
/// </summary>
internal class FormDesignerFormLinksManager : 
  IFormDesignerFormLinksManager,
  IEnumerable<FormDesignerFormLinksProviderType>,
  IEnumerable
{
  private Dictionary<Guid, FormDesignerFormLinksProviderType> _providers = new Dictionary<Guid, FormDesignerFormLinksProviderType>();

  /// <summary>Регистрация данных о провайдере.</summary>
  /// <param name="providerType">Данные о провайдере</param>
  public void RegisterProvider(FormDesignerFormLinksProviderType providerType)
  {
    if (providerType == null)
      return;
    this._providers[providerType.ProviderGuid] = providerType;
  }

  /// <summary>Удаление данных о провайдере.</summary>
  /// <param name="providerGuid">Глобальный идентификатор провайдера</param>
  public void UnregisterProvider(Guid providerGuid) => this._providers.Remove(providerGuid);

  /// <summary>
  /// Получение данных о провайдере по его глобальному идентификатору.
  /// </summary>
  /// <param name="providerGuid">Глобальный идентификатор провайдера</param>
  /// <returns>Данные о провайдере</returns>
  public FormDesignerFormLinksProviderType GetProvider(Guid providerGuid)
  {
    return !this._providers.ContainsKey(providerGuid) ? (FormDesignerFormLinksProviderType) null : this._providers[providerGuid];
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerator<FormDesignerFormLinksProviderType> GetEnumerator()
  {
    return (IEnumerator<FormDesignerFormLinksProviderType>) this._providers.Values.GetEnumerator();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  /// <summary>Регистрация данных о провайдерах объектов и связей.</summary>
  public static void Register()
  {
    IFormDesignerFormLinksManager serviceInstance = (IFormDesignerFormLinksManager) new FormDesignerFormLinksManager();
    string name1 = LocalizationHolder.rm.GetString("FormDesigner_8");
    serviceInstance.RegisterProvider(new FormDesignerFormLinksProviderType(name1, ObjectTypeFormLinkProvider.stProviderGuid, typeof (ObjectTypeFormLinkProvider)));
    string name2 = LocalizationHolder.rm.GetString("FormDesigner_9");
    serviceInstance.RegisterProvider(new FormDesignerFormLinksProviderType(name2, RelationTypeFormLinkProvider.stProviderGuid, typeof (RelationTypeFormLinkProvider)));
    ServicesManager.ServiceContainer.AddService(typeof (IFormDesignerFormLinksManager), (object) serviceInstance);
  }
}
