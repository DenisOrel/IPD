
// Type: Intermech.Client.Core.FormDesigner.Controls.FormLinks
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Список провайдеров ссылок.</summary>
[Editor(typeof (FormLinksEditor), typeof (UITypeEditor))]
[TypeConverter(typeof (FormLinksConverter))]
public class FormLinks : List<IFormDesignerFormLinksProvider>
{
  /// <summary>Идентификатор формы.</summary>
  public long FormID { get; private set; }

  /// <summary>Контруктор.</summary>
  /// <param name="formID">Идентификатор формы</param>
  public FormLinks(long formID)
  {
    this.FormID = formID;
    if (!(ServicesManager.GetService(typeof (IFormDesignerFormLinksManager)) is IFormDesignerFormLinksManager service))
      return;
    foreach (FormDesignerFormLinksProviderType linksProviderType in (IEnumerable<FormDesignerFormLinksProviderType>) service)
    {
      if (linksProviderType != null && Activator.CreateInstance(linksProviderType.ProviderType) is IFormDesignerFormLinksProvider instance)
        this.Add(instance);
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="formID">Идентификатор формы</param>
  /// <param name="source"></param>
  public FormLinks(long formID, IEnumerable<IFormDesignerFormLinksProvider> source)
  {
    this.FormID = formID;
    foreach (ICloneable cloneable in source)
      this.Add(cloneable.Clone() as IFormDesignerFormLinksProvider);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="providerType"></param>
  /// <returns></returns>
  public IFormDesignerFormLinksProvider GetProvider(Guid providerType)
  {
    return this.FirstOrDefault<IFormDesignerFormLinksProvider>((Func<IFormDesignerFormLinksProvider, bool>) (x => x.ProviderGuid == providerType));
  }

  /// <summary>Сохранение данных из провайдеров.</summary>
  public void Save() => this.ForEach((Action<IFormDesignerFormLinksProvider>) (x => x.Commit()));

  /// <summary>Загрузка данных в провайдеры.</summary>
  public void Load()
  {
    this.ForEach((Action<IFormDesignerFormLinksProvider>) (x => x.Load(this.FormID)));
  }
}
