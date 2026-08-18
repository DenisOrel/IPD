// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.ITabPageForm
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>Интерфейс работы с формой входящей в TabPage</summary>
public interface ITabPageForm
{
  void FillForm(IFolder folder);

  bool SaveForm(IFolder folder);

  void FormLostFocus(IFolder folder);

  bool RefreshAfterCanceling();

  string HelpTopicID { get; }
}
