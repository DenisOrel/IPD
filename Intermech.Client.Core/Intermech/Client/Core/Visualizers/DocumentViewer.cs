
// Type: Intermech.Client.Core.Visualizers.DocumentViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace Intermech.Client.Core.Visualizers;

internal class DocumentViewer
{
  private Guid _classID = Guid.Empty;
  private string _progID = string.Empty;
  private DocumentViewer.TViewerFlag _flags;
  private string _extension = string.Empty;

  /// <summary>Эта функция поставляет идентификатор класса, связанным с точно установленным именем файла.</summary>
  /// <param name="fileName">[in] имя файла с нулевым символом в конце</param>
  /// <param name="clsid">идентификатор класса</param>
  /// <returns></returns>
  [DllImport("ole32.dll")]
  private static extern int GetClassFile([MarshalAs(UnmanagedType.LPWStr)] string fileName, out Guid clsid);

  [DllImport("ole32.dll")]
  private static extern int ProgIDFromCLSID(Guid clsid, [MarshalAs(UnmanagedType.LPWStr)] out string progId);

  internal string Extension => this._extension;

  internal Guid ClassID => this._classID;

  internal string ProgID => this._progID;

  internal DocumentViewer.TViewerFlag Flags => this._flags;

  internal DocumentViewer(string @extension)
  {
    this._extension = @extension != null ? @extension : throw new ArgumentNullException();
    this._progID = this.FindRegistryProgID(this._extension);
    Type typeFromProgId = Type.GetTypeFromProgID(this._progID);
    this._classID = typeFromProgId != (Type) null ? typeFromProgId.GUID : Guid.Empty;
    this._flags = this.GetViewerFlags(this._classID, this._extension);
  }

  /// <summary> в реестре найти по расширению  ProgID </summary>
  /// <param name="extension"> расширение файла</param>
  /// <returns>ProgID , иначе string.Empty</returns>
  private string FindRegistryProgID(string @extension)
  {
    if (@extension.Length == 0 || @extension.Substring(0, 1) != ".")
      @extension = "." + @extension;
    using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(@extension))
    {
      if (registryKey != null)
        return (string) registryKey.GetValue((string) null);
    }
    foreach (string subKeyName in Registry.ClassesRoot.GetSubKeyNames())
    {
      if (subKeyName[0] == '.' && Regex.IsMatch(subKeyName, @extension, RegexOptions.IgnoreCase | RegexOptions.Singleline))
      {
        using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(subKeyName))
        {
          if (registryKey != null)
            return (string) registryKey.GetValue((string) null);
        }
      }
    }
    return string.Empty;
  }

  private Guid FindMimeCLSID(string progID)
  {
    using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey($"MIME\\Database\\Content Type\\{progID}\\CLSID"))
      return registryKey == null ? Guid.Empty : new Guid((string) registryKey.GetValue((string) null));
  }

  private DocumentViewer.TViewerFlag GetViewerFlags(Guid classID, string @extension)
  {
    if (classID == Guid.Empty)
      return DocumentViewer.TViewerFlag.vfNone;
    if (@extension.Length == 0 || @extension.Substring(0, 1) != ".")
      @extension = "." + @extension;
    DocumentViewer.TViewerFlag viewerFlags = DocumentViewer.TViewerFlag.vfNone;
    string[] strArray = (string[]) null;
    string name = $"CLSID\\{{{classID.ToString()}}}";
    using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name))
    {
      if (registryKey == null)
        return viewerFlags;
      strArray = registryKey.GetSubKeyNames();
    }
    foreach (string str in strArray)
    {
      switch (str)
      {
        case "DocObject":
          viewerFlags |= DocumentViewer.TViewerFlag.vfDocObject;
          break;
        case "Insertable":
          viewerFlags |= DocumentViewer.TViewerFlag.vfInsertable;
          break;
        case "Control":
          viewerFlags |= DocumentViewer.TViewerFlag.vfControl;
          break;
        case "EnableFullPage":
          using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name + "\\EnableFullPage"))
          {
            if (registryKey != null)
            {
              foreach (string subKeyName in registryKey.GetSubKeyNames())
              {
                if (subKeyName.ToLower() == @extension.ToLower())
                  viewerFlags |= DocumentViewer.TViewerFlag.vfEnableFullPage;
              }
              break;
            }
            break;
          }
      }
    }
    return viewerFlags;
  }

  [System.Flags]
  internal enum TViewerFlag
  {
    vfNone = 0,
    vfControl = 1,
    vfEnableFullPage = 2,
    vfDocObject = 4,
    vfInsertable = 8,
  }
}
