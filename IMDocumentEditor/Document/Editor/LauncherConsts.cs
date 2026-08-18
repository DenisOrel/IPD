// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.LauncherConsts
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Localization;
using System;
using System.IO;

#nullable disable
namespace Intermech.Document.Editor;

public class LauncherConsts
{
  public static readonly string SEARCH_KEY = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Intermech\\Search";
  public static readonly string SEARCH_EXENAME = "ExeName";
  public static readonly string IM_KEY = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Intermech";
  public static readonly string IM_DIRECTORY = "IM_Dir";
  public static readonly string IM_ANCI_PATH = "\\IM-BASE\\IMIMBase.dll";
  public static readonly string IPS_KEY = "SOFTWARE\\Intermech";
  public static readonly string IPS_NAME = "IPS ";
  public static readonly string SEARCH_NAME = "Search";
  public static readonly string CADMECH_NAME = "Cadmech";
  public static readonly string CADMECH_T_NAME = "Cadmech-T";
  public static readonly string CADMECH_IPS_NAME = "Cadmech IPS";
  public static readonly string CADMECH_IPS_T_NAME = "Cadmech_T IPS";
  public static readonly string PATTREN_IPS = "^IPSHomeClient\\d+$";
  public static readonly string PATTREN_IMCLIENT = "IMClient.exe.config$";
  public static readonly string PATTREN_IMIMBASE = "IMIMBase_net.dll$";
  public static string FULL_PATH_TO_AUTO_CAD_VERSION = "HKEY_CURRENT_USER\\Software\\Autodesk\\AutoCAD";
  public static string PATH_TO_AUTO_CAD_VERSION = "Software\\Autodesk\\AutoCAD";
  public static readonly string ACAD_VERSION = "CurVer";
  public static readonly string ACAD_PROFILES = "\\Profiles\\";
  public static readonly string ACAD_GENERAL = "\\General";
  public static readonly string ACAD_EXE = "\\acad.exe";
  public static readonly string PROFILE_CADMECH = "<<Cadmech>>";
  public static readonly string PROFILE_CADMECH_T = "<<Cadmech-T>>";
  public static readonly string PROFILE_CADM_IPS = "<<CADM_IPS>>";
  public static readonly string PROFILE_CADM_T_IPS = "<<CADMT_IPS>>";
  public static string FULL_PATH_TO_AUTO_CAD_EXE = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Autodesk\\AutoCAD";
  public static readonly string ACAD_LOCATION = "AcadLocation";
  public static readonly string ACAD_TEMPLATE = "TemplatePath";
  public static readonly string CAD_IM_BASE_LIBRARY = "cadImBaseLibrary";
  public static readonly string COM_SERVER_KEY = "ComServer";
  public static readonly string ENABLE_COM_BAT = "EnableCom.bat";
  public static readonly string IM_CLIENT_EXE = "IMClient.exe";
  public static readonly string COPY_FILE_NODE = "copyfile";
  public static readonly string VER_ATTRIBUTE = "ver";
  public static readonly string NAME_ATTRIBUTE = "name";
  public static readonly string XML_CONFIG = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IntermechLauncher\\Config.xml");
  public static readonly string CONFIG_EXAMPLE = "<Launcher> <pack name=\"Notepad\" > <programm  path=\"C:\\Windows\\system32\\notepad.exe\" /> </pack></Launcher>";
  public static readonly string CONFIG_VERSION = "1.0";
  public static readonly string CONFIG_ENCODING = "utf-8";
  public static readonly string CONFIG_ATTRIBUTE_PATH = "path";
  public static readonly string xmlEmptyDoc = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Launcher/>";
  public static readonly string CONFIG_ATTRIBUTE_IMAGE = "image";
  public static readonly string ERROR_TEXT = LocalizationHolder.rm.GetString("Document.Editor_42");
  public static readonly string ERROR_CAPTION = LocalizationHolder.rm.GetString("Document.Editor_43");
}
