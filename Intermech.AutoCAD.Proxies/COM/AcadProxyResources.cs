// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.AcadProxyResources
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class AcadProxyResources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal AcadProxyResources()
  {
  }

  /// <summary>
  ///   Returns the cached ResourceManager instance used by this class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (AcadProxyResources.resourceMan == null)
        AcadProxyResources.resourceMan = new ResourceManager("Intermech.AutoCAD.Proxies.COM.AcadProxyResources", typeof (AcadProxyResources).Assembly);
      return AcadProxyResources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => AcadProxyResources.resourceCulture;
    set => AcadProxyResources.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized string similar to При переключении на окно документа '{0}' произошла внутренняя ошибка. {1}.
  /// </summary>
  internal static string SR_ActivateDocumentFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_ActivateDocumentFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Не удалось дождаться ответа от приложения..
  /// </summary>
  internal static string SR_CallRejected
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_CallRejected), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Не удалось найти документ dwg '{0}'. Возможно, что он был закрыт..
  /// </summary>
  internal static string SR_CantFindDocument
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_CantFindDocument), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При закрытии документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_CloseDocumentFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_CloseDocumentFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При создании нового документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_CreateDocumentFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_CreateDocumentFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При переборе блоков документа '{0}' произошла внутренняя ошибка приложения. {1}.
  /// </summary>
  internal static string SR_DocumentBlocksWalkFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_DocumentBlocksWalkFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При переборе файлов документа '{0}' произошла внутренняя ошибка приложения. {1}.
  /// </summary>
  internal static string SR_DocumentFilesWalkFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_DocumentFilesWalkFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Не задано полное имя файла документа dwg..
  /// </summary>
  internal static string SR_DocumentFullNameNotSpecified
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_DocumentFullNameNotSpecified), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении свойства 'Active' у документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_DocumentIsActiveFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_DocumentIsActiveFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении признака модификации документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_DocumentIsModifiedFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_DocumentIsModifiedFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении абсолютного пути документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_DocumentIsNewFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_DocumentIsNewFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении свойства read-only документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_DocumentIsReadOnlyFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_DocumentIsReadOnlyFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При переборе открытых документов произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_DocumentsWalkFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_DocumentsWalkFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Ошибка.</summary>
  internal static string SR_Error
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_Error), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При поиске документа '{0}' произошла внутренняя ошибка приложения. {1}.
  /// </summary>
  internal static string SR_FindDocumentFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_FindDocumentFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Путь к файлу документа dwg должен быть абсолютным..
  /// </summary>
  internal static string SR_FullPathRequired
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_FullPathRequired), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При обращении к API приложения произошла ошибка. {0}.
  /// </summary>
  internal static string SR_GeneralAPIError
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_GeneralAPIError), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении названия документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_GetDocumentNameFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_GetDocumentNameFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении мастер-файла документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_GetMasterFileFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_GetMasterFileFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении списка открытых в приложении документов произошла внутренняя ошибка. {0}.
  /// </summary>
  internal static string SR_GetOpenDocumentsFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_GetOpenDocumentsFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении списка файлов документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_GetSatelliteFilesFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_GetSatelliteFilesFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При открытии документа '{0}' произошла внутренняя ошибка приложения. {1}.
  /// </summary>
  internal static string SR_OpenDocumentFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_OpenDocumentFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При восстановлении состояния приложения произошла внутренняя ошибка. {0}.
  /// </summary>
  internal static string SR_RestoreVisualStateFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_RestoreVisualStateFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При сохранении документа произошла внутренняя ошибка приложения. {0}.
  /// </summary>
  internal static string SR_SaveDocumentFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_SaveDocumentFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При сохранении состояния приложения произошла внутренняя ошибка. {0}.
  /// </summary>
  internal static string SR_SaveVisualStateFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_SaveVisualStateFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При переключении на {0} произошла внутренняя ошибка. {1}.
  /// </summary>
  internal static string SR_SetForeWindowFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_SetForeWindowFailed), AcadProxyResources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to При получении активного документа приложения произошла внутренняя ошибка. {0}.
  /// </summary>
  internal static string SR_TryGetActiveDocumentFailed
  {
    get
    {
      return AcadProxyResources.ResourceManager.GetString(nameof (SR_TryGetActiveDocumentFailed), AcadProxyResources.resourceCulture);
    }
  }
}
