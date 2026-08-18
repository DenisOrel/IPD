
// Type: Intermech.Mvp.Components.ITreeView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp.Components
{
    public interface ITreeView
    {
      void AddRootNode(string key, string text);

      void AddChildNode(string parent, string key, string text);

      bool ContainsNode(string key);

      void ClearNodes();

      bool IsNodeExpanded(string key);

      void ExpandNode(string key, bool expanded);

      string GetSelectedNode();

      void SelectNode(string key);

      string GetTopVisibleNode();

      void SetTopVisibleNode(string key);

      bool IsNodeVisible(string key);

      event EventHandler SelectionChanged;
    }
}
