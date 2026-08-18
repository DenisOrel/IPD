
// Type: Intermech.Interfaces.WIN32Wrapper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Runtime.InteropServices;


namespace Intermech.Interfaces
{
    /// <summary>WIN32 API Wrapper class</summary>
    public class WIN32Wrapper
    {
      /// <summary>Get all the section names from an INI file</summary>
      [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesA")]
      public static extern int GetPrivateProfileSectionNames(
        [MarshalAs(UnmanagedType.LPArray)] byte[] lpReturnedString,
        int nSize,
        string lpFileName);

      /// <summary>Get all the settings from a section in a INI file</summary>
      [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionA")]
      public static extern int GetPrivateProfileSection(
        string lpAppName,
        [MarshalAs(UnmanagedType.LPArray)] byte[] lpReturnedString,
        int nSize,
        string lpFileName);
    }
}
