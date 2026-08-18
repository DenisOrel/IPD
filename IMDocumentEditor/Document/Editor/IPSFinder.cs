// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.IPSFinder
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace Intermech.Document.Editor;

internal class IPSFinder
{
  private static List<string> ReadRegistryValueForIPS()
  {
    List<string> stringList = new List<string>();
    try
    {
      RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(LauncherConsts.IPS_KEY);
      if (registryKey != null)
      {
        string[] valueNames = registryKey.GetValueNames();
        Regex regex = new Regex(LauncherConsts.PATTREN_IPS);
        foreach (string str in valueNames)
        {
          if (regex.IsMatch(str) && registryKey.GetValue(str).ToString() != string.Empty)
            stringList.Add(registryKey.GetValue(str).ToString());
        }
      }
    }
    catch (SecurityException ex)
    {
      return stringList;
    }
    return stringList;
  }

  public static string PathToIPSConfig()
  {
    List<string> stringList = new List<string>();
    foreach (string str in IPSFinder.ReadRegistryValueForIPS())
    {
      if (File.Exists(str))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(str);
        xmlDocument.DocumentElement.ChildNodes[0].Attributes[LauncherConsts.VER_ATTRIBUTE].Value.Remove(2, 1).Insert(1, ".");
        string empty = string.Empty;
        Regex regex1 = new Regex(LauncherConsts.PATTREN_IMCLIENT);
        Regex regex2 = new Regex(LauncherConsts.PATTREN_IMIMBASE);
        foreach (XmlNode childNode in xmlDocument.ChildNodes[0].ChildNodes[0].ChildNodes)
        {
          if (childNode.Name == LauncherConsts.COPY_FILE_NODE)
          {
            if (empty == string.Empty && regex1.IsMatch(childNode.Attributes[LauncherConsts.NAME_ATTRIBUTE].Value))
              empty = childNode.Attributes[LauncherConsts.NAME_ATTRIBUTE].Value;
            if (empty != string.Empty)
              break;
          }
        }
        stringList.Add(empty);
        break;
      }
    }
    string ipsConfig = "";
    if (stringList.Count > 0)
      ipsConfig = stringList[0];
    return ipsConfig;
  }
}
