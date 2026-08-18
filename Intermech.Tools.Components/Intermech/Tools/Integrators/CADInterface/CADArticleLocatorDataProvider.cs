// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADArticleLocatorDataProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует провайдер исходных данных для поиска изделия в базе IPS по конфигурации 3D-модели.
/// </summary>
public sealed class CADArticleLocatorDataProvider : ArticleLocatorDataProvider
{
  private readonly ValueBag cfgAttributes;
  private readonly string cfgName;
  private readonly long documentId;

  /// <summary>Создает объект.</summary>
  /// <param name="configurationAttrs">Атрибуты конфигурации, прочитанные из файла 3D-модели</param>
  /// <param name="configurationName">Имя конфигурации 3D-модели</param>
  /// <param name="documentId">Идентификатор документа для 3D-модели, если он известен</param>
  public CADArticleLocatorDataProvider(ValueBag cfgAttributes, string cfgName, long documentId)
  {
    if (cfgAttributes == null)
      throw new ArgumentNullException(nameof (cfgAttributes));
    if (string.IsNullOrEmpty(cfgName))
      throw new ArgumentException();
    this.cfgAttributes = cfgAttributes;
    this.cfgName = cfgName;
    this.documentId = documentId;
  }

  /// <summary>
  /// Создает объект. При использовании этого конструктора поиск по внешнему ключу изделия будет недоступен.
  /// </summary>
  /// <param name="configurationAttrs">Атрибуты конфигурации, прочитанные из файла 3D-модели</param>
  public CADArticleLocatorDataProvider(ValueBag cfgAttributes)
  {
    this.cfgAttributes = cfgAttributes != null ? cfgAttributes : throw new ArgumentNullException(nameof (cfgAttributes));
    this.cfgName = string.Empty;
    this.documentId = 0L;
  }

  /// <summary>
  /// Создает декодер для поиска изделия по внешнему ключу изделия.
  /// </summary>
  /// <returns>Ссылка на объект декодера</returns>
  public override IExternalKeyLocatorData TryCreateExternalKeyDecoder()
  {
    return (IExternalKeyLocatorData) new CADArticleLocatorDataProvider.ExternalKeyDecoder(this);
  }

  /// <summary>Создает декодер для поиска изделия по ключу Imbase.</summary>
  /// <returns>Ссылка на объект декодера</returns>
  public override IImbaseKeyLocatorData TryCreateImbaseKeyDecoder()
  {
    return (IImbaseKeyLocatorData) new CADArticleLocatorDataProvider.ImbaseKeyDecoder(this);
  }

  /// <summary>
  /// Создает декодер для поиска изделия по обозначению, коду ОКП или наименованию.
  /// </summary>
  /// <returns>Ссылка на объект декодера</returns>
  public override IIdentityArticleLocatorData TryCreateIdentityDecoder()
  {
    return (IIdentityArticleLocatorData) new CADArticleLocatorDataProvider.IdentityDecoder(this);
  }

  private sealed class ExternalKeyDecoder : IExternalKeyLocatorData
  {
    private readonly CADArticleLocatorDataProvider data;

    public ExternalKeyDecoder(CADArticleLocatorDataProvider data)
    {
      this.data = data != null ? data : throw new ArgumentNullException(nameof (data));
    }

    public string GetExternalKey()
    {
      return !string.IsNullOrEmpty(this.data.cfgName) ? CADArticleExternalKeys.GetExternalKey(this.data.cfgAttributes, this.data.cfgName) : (string) null;
    }

    public long GetDocumentId() => this.data.documentId;
  }

  private sealed class ImbaseKeyDecoder : IImbaseKeyLocatorData
  {
    private readonly CADArticleLocatorDataProvider data;

    public ImbaseKeyDecoder(CADArticleLocatorDataProvider data)
    {
      this.data = data != null ? data : throw new ArgumentNullException(nameof (data));
    }

    public string GetImbaseKey()
    {
      return this.data.cfgAttributes.Read<string>((StringKey) IDCache.Default.ImbaseKey.Text, (string) null);
    }
  }

  private sealed class IdentityDecoder : IIdentityArticleLocatorData
  {
    private readonly CADArticleLocatorDataProvider data;

    public IdentityDecoder(CADArticleLocatorDataProvider data)
    {
      this.data = data != null ? data : throw new ArgumentNullException(nameof (data));
    }

    public string GetDesignation()
    {
      return this.data.cfgAttributes.Read<string>((StringKey) IDCache.Default.Designation.Text, string.Empty);
    }

    public string GetOKPCode()
    {
      return this.data.cfgAttributes.Read<string>((StringKey) IDCache.Default.OKPCode.Text, string.Empty);
    }

    public string GetName()
    {
      return this.data.cfgAttributes.Read<string>((StringKey) IDCache.Default.Name.Text, string.Empty);
    }
  }
}
