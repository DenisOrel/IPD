// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.LocalizationHolder
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Interfaces.AutoSelection;

/// <summary>
/// Класс позволяет получать строки из ресурсов текущей сборки
/// </summary>
internal class LocalizationHolder
{
  /// <summary>Статическое поле для обращения к ресурсам</summary>
  public static ResourceManager rm = new ResourceManager("Intermech.Interfaces.AutoSelection.Resources.Interfaces.AutoSelectionResources", Assembly.GetExecutingAssembly());
  /// <summary>Статическое свойство для обращения к ресурсам</summary>
  public static ResourceManager rma = new ResourceManager("Intermech.Interfaces.AutoSelection.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
