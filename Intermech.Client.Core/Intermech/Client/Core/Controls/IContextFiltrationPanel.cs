
// Type: Intermech.Client.Core.Controls.IContextFiltrationPanel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;


namespace Intermech.Client.Core.Controls;

public interface IContextFiltrationPanel
{
  ButtonItem ButtonEditingContextsEdit { get; }

  DropDownMenuItem MenuEditingContextMode { get; }

  ButtonItem ButtonEditingContextsRefresh { get; }

  ButtonItem ButtonEditingContextsCreate { get; }

  ButtonItem ButtonEditingContextsBrowse { get; }

  DropDownMenuItem ButtonProjectFilterMode { get; }
}
