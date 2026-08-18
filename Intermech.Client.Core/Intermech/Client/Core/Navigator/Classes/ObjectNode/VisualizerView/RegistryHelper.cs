
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.RegistryHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Microsoft.Win32;
using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

/// <summary>Класс для получения информации из реестра</summary>
internal class RegistryHelper
{
  /// <summary>Preview handler Guid</summary>
  private const string PreviewHandlerGuid = "{8895b1c6-b41f-4c1c-a562-0d564250836f}";
  /// <summary>Thumbnail image handler IThumbnailProvider</summary>
  private const string ThumbnailProviderGuid = "{E357FCCD-A995-4576-B01F-234630154E96}";
  /// <summary>Image handler IExtractImage</summary>
  private const string ExtractImageGuid = "{BB2E617C-0920-11d1-9A0B-00C04FC2D6C1}";

  /// <summary>
  /// 
  /// </summary>
  /// <param name="registryKey"></param>
  /// <param name="stringGuid"></param>
  /// <returns></returns>
  private static Guid GetShellExGUID(RegistryKey registryKey, string stringGuid)
  {
    if (registryKey == null)
      return Guid.Empty;
    string name = $"ShellEx\\{stringGuid}";
    using (RegistryKey registryKey1 = registryKey.OpenSubKey(name, false))
    {
      Guid result;
      return registryKey1 == null || !Guid.TryParse(Convert.ToString(registryKey1.GetValue((string) null)), out result) ? Guid.Empty : result;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="extension"></param>
  /// <returns></returns>
  public static Guid GetThumbnailProviderGUID(string @extension)
  {
    string lower = @extension.ToLower();
    try
    {
      using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(lower, false))
      {
        if (registryKey != null)
          return RegistryHelper.GetShellExGUID(registryKey, "{E357FCCD-A995-4576-B01F-234630154E96}");
      }
    }
    catch (Exception ex)
    {
    }
    return Guid.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="extension"></param>
  /// <returns></returns>
  public static Guid GetExtractImageGUID(string @extension)
  {
    string lower = @extension.ToLower();
    try
    {
      using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(lower, false))
      {
        if (registryKey != null)
          return RegistryHelper.GetShellExGUID(registryKey, "{BB2E617C-0920-11d1-9A0B-00C04FC2D6C1}");
      }
    }
    catch (Exception ex)
    {
    }
    return Guid.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="extension"></param>
  /// <returns></returns>
  public static Guid GetPreviewHandlerGUID(string @extension)
  {
    string lower = @extension.ToLower();
    try
    {
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey(lower, false))
      {
        if (registryKey1 == null)
          return Guid.Empty;
        Guid shellExGuid1 = RegistryHelper.GetShellExGUID(registryKey1, "{8895b1c6-b41f-4c1c-a562-0d564250836f}");
        if (shellExGuid1 != Guid.Empty)
          return shellExGuid1;
        string name1 = Convert.ToString(registryKey1.GetValue((string) null));
        if (!string.IsNullOrEmpty(name1))
        {
          using (RegistryKey registryKey2 = Registry.ClassesRoot.OpenSubKey(name1, false))
          {
            if (registryKey2 != null)
            {
              Guid shellExGuid2 = RegistryHelper.GetShellExGUID(registryKey2, "{8895b1c6-b41f-4c1c-a562-0d564250836f}");
              if (shellExGuid2 != Guid.Empty)
                return shellExGuid2;
            }
          }
        }
        else
        {
          string str = Convert.ToString(registryKey1.GetValue("PerceivedType"));
          if (!string.IsNullOrEmpty(str))
          {
            string name2 = $"SystemFileAssociations\\{str}";
            using (RegistryKey registryKey3 = Registry.ClassesRoot.OpenSubKey(name2, false))
            {
              Guid shellExGuid3 = RegistryHelper.GetShellExGUID(registryKey3, "{8895b1c6-b41f-4c1c-a562-0d564250836f}");
              if (shellExGuid3 != Guid.Empty)
                return shellExGuid3;
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      return Guid.Empty;
    }
    return Guid.Empty;
  }
}
