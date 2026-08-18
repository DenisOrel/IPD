// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Document.Editor.Resources.DocumentEditorResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Document.Editor.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
