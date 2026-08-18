// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.WizardPageOperationErrorsVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.UI;
using Intermech.UI.Wpf.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Resources;
using System.Xml;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.FormatProviders.Rtf;
using Telerik.Windows.Documents.Model;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class WizardPageOperationErrorsVM : WizardPageErrorsVM<OperationError>
{
  private PluggableCommand<object> _checkedCommand;
  private PluggableCommand _openObjectCardCommand;
  private PluggableCommand _saveToHtmlReportCommand;
  private PluggableCommand _saveToRtfReportCommand;
  private PluggableCommand _saveRtfReportToBufferCommand;
  private PluggableCommand _errorDoubleClickCommand;
  private bool _isErrorVisible = true;
  private bool _isWarningVisible = true;
  private OperationError _selectedErrorItem;
  private CollectionViewSource _cvsItems;
  private CopyingSession _session;

  public event WizardPageOperationErrorsVM.ErrorDoubleClickHandler ErrorDoubleClick;

  public WizardPageOperationErrorsVM()
  {
    this._checkedCommand = new PluggableCommand<object>(new Action<object>(this.CheckedCommand));
    this._openObjectCardCommand = new PluggableCommand(new Action(this.OpenObjectCard));
    this._saveToHtmlReportCommand = new PluggableCommand(new Action(this.SaveToHtml));
    this._saveToRtfReportCommand = new PluggableCommand(new Action(this.SaveToRtf));
    this._saveRtfReportToBufferCommand = new PluggableCommand(new Action(this.SaveToBuffer));
    this._errorDoubleClickCommand = new PluggableCommand(new Action(this.ErrorVertexDoubleClick));
    this.Items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Items_CollectionChanged);
    this._cvsItems = new CollectionViewSource()
    {
      Source = (object) this.Items
    };
    this._cvsItems.Filter += new FilterEventHandler(this.ApplyFilter);
  }

  public void SetCopyingSession(CopyingSession session)
  {
    this._session = session ?? throw new ArgumentNullException(nameof (session));
  }

  public ICollectionView AllItems => this._cvsItems.View;

  public string ErrorCount
  {
    get
    {
      int messageCount = this.Items.Count<OperationError>((Func<OperationError, bool>) (x => !x.IsWarning));
      return $"{messageCount} {this.GetNumberName(messageCount, "Ошибка", "Ошибок", "Ошибки")}";
    }
  }

  public string WarningCount
  {
    get
    {
      int messageCount = this.Items.Count<OperationError>((Func<OperationError, bool>) (x => x.IsWarning));
      return $"{messageCount} {this.GetNumberName(messageCount, "Предупреждение", "Предупреждений", "Предупреждения")}";
    }
  }

  public PluggableCommand<object> CheckedMessagesCommand => this._checkedCommand;

  public PluggableCommand OpenObjectCardCommand => this._openObjectCardCommand;

  public PluggableCommand SaveToHtmlReportCommand => this._saveToHtmlReportCommand;

  public PluggableCommand SaveToRtfReportCommand => this._saveToRtfReportCommand;

  public PluggableCommand SaveRtfReportToBufferCommand => this._saveRtfReportToBufferCommand;

  public PluggableCommand ErrorDoubleClickCommand => this._errorDoubleClickCommand;

  public OperationError SelectedErrorItem
  {
    get => this._selectedErrorItem;
    set
    {
      this._selectedErrorItem = value;
      this.RaisePropertyChanged(nameof (SelectedErrorItem));
    }
  }

  private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
  {
    this.RaisePropertyChanged("ErrorCount");
    this.RaisePropertyChanged("WarningCount");
  }

  private string GetNumberName(int messageCount, string one, string zero, string many)
  {
    int num = messageCount % 10;
    if (messageCount >= 5 && messageCount < 21 || num == 0)
      return zero;
    if (num == 1)
      return one;
    return num > 1 && num < 5 ? many : zero;
  }

  private void CheckedCommand(object parameter)
  {
    object[] objArray = (object[]) parameter;
    if (objArray == null || objArray.Length != 2)
      return;
    this._isErrorVisible = Convert.ToBoolean(objArray[0]);
    this._isWarningVisible = Convert.ToBoolean(objArray[1]);
    this.OnFilterChanged();
  }

  private void OnFilterChanged() => this._cvsItems.View.Refresh();

  private void ApplyFilter(object sender, FilterEventArgs e)
  {
    OperationError operationError = (OperationError) e.Item;
    e.Accepted = operationError.IsWarning ? this._isWarningVisible : this._isErrorVisible;
  }

  private void OpenObjectCard()
  {
    if (this.SelectedErrorItem?.Vertex == null)
      return;
    ObjectCommands.ParametersCardCommand(Services.GetItems(this.SelectedErrorItem.Vertex.ObjectId), (IServiceProvider) null, (object) null);
  }

  private void SaveToRtf()
  {
    HtmlFormatProvider htmlFormatProvider = new HtmlFormatProvider();
    RadDocument radDocument = new RadDocument();
    string outerXml = this.CreateHtmlDocument().OuterXml;
    RadDocument document = htmlFormatProvider.Import(outerXml);
    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
    saveFileDialog1.DefaultExt = "rtf";
    saveFileDialog1.FileName = "errorReport";
    saveFileDialog1.AddExtension = true;
    saveFileDialog1.Filter = "RTF файл|*.rtf";
    SaveFileDialog saveFileDialog2 = saveFileDialog1;
    bool? nullable = saveFileDialog2.ShowDialog();
    bool flag = true;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    using (Stream output = (Stream) File.Create(saveFileDialog2.FileName))
      new RtfFormatProvider().Export(document, output);
  }

  private void SaveToHtml()
  {
    XmlDocument htmlDocument = this.CreateHtmlDocument();
    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
    saveFileDialog1.DefaultExt = "html";
    saveFileDialog1.FileName = "errorReport";
    saveFileDialog1.AddExtension = true;
    saveFileDialog1.Filter = "Html файл|*.html";
    SaveFileDialog saveFileDialog2 = saveFileDialog1;
    bool? nullable = saveFileDialog2.ShowDialog();
    bool flag = true;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    File.WriteAllText(saveFileDialog2.FileName, htmlDocument.OuterXml, Encoding.UTF8);
  }

  private void SaveToBuffer()
  {
    HtmlFormatProvider htmlFormatProvider = new HtmlFormatProvider();
    RadDocument radDocument = new RadDocument();
    string outerXml = this.CreateHtmlDocument().OuterXml;
    using (MemoryStream output = new MemoryStream())
    {
      new RtfFormatProvider().Export(htmlFormatProvider.Import(outerXml), (Stream) output);
      output.Position = 0L;
      using (StreamReader streamReader = new StreamReader((Stream) output))
      {
        string end = streamReader.ReadToEnd();
        DataObject data = new DataObject();
        data.SetData(DataFormats.Rtf, (object) end);
        data.SetData(DataFormats.Text, (object) end);
        Clipboard.SetDataObject((object) data);
      }
    }
  }

  private XmlDocument CreateHtmlDocument()
  {
    XmlDocument htmlDocument = new XmlDocument();
    string text = "Список ошибок и предупреждений возникший в процессе работы мастера копирования.";
    htmlDocument.AppendChild((XmlNode) htmlDocument.CreateElement("html"));
    XmlNode xmlNode1 = htmlDocument.DocumentElement.AppendChild((XmlNode) htmlDocument.CreateElement("head"));
    XmlNode xmlNode2 = htmlDocument.DocumentElement.AppendChild((XmlNode) htmlDocument.CreateElement("body"));
    XmlElement element1 = htmlDocument.CreateElement("title");
    element1.AppendChild((XmlNode) htmlDocument.CreateTextNode(text));
    xmlNode1.AppendChild((XmlNode) element1);
    XmlText textNode = htmlDocument.CreateTextNode("table, th, td { border: 1px solid black; }");
    XmlElement element2 = htmlDocument.CreateElement("style");
    element2.AppendChild((XmlNode) textNode);
    xmlNode1.AppendChild((XmlNode) element2);
    XmlElement element3 = htmlDocument.CreateElement("h1");
    element3.AppendChild((XmlNode) htmlDocument.CreateTextNode(text));
    xmlNode2.AppendChild((XmlNode) element3);
    XmlElement element4 = htmlDocument.CreateElement("p");
    element4.AppendChild((XmlNode) htmlDocument.CreateTextNode($"Дата создания: {DateTime.Now:f}"));
    xmlNode2.AppendChild((XmlNode) element4);
    if (this._session != null)
    {
      XmlElement element5 = htmlDocument.CreateElement("p");
      element5.AppendChild((XmlNode) htmlDocument.CreateTextNode($"Идентификатор сессии копирования: {this._session.UniqueId}"));
      xmlNode2.AppendChild((XmlNode) element5);
    }
    XmlElement element6 = htmlDocument.CreateElement("h2");
    element6.AppendChild((XmlNode) htmlDocument.CreateTextNode("Список ошибок и предупреждений: "));
    xmlNode2.AppendChild((XmlNode) element6);
    XmlElement element7 = htmlDocument.CreateElement("table");
    XmlElement element8 = htmlDocument.CreateElement("tr");
    XmlElement element9 = htmlDocument.CreateElement("th");
    element9.AppendChild((XmlNode) htmlDocument.CreateTextNode("Текст ошибки/предупреждения"));
    element8.AppendChild((XmlNode) element9);
    XmlElement element10 = htmlDocument.CreateElement("th");
    element10.AppendChild((XmlNode) htmlDocument.CreateTextNode("Предложение с исправлением"));
    element8.AppendChild((XmlNode) element10);
    element7.AppendChild((XmlNode) element8);
    foreach (OperationError operationError in (Collection<OperationError>) this.Items)
    {
      XmlElement element11 = htmlDocument.CreateElement("tr");
      XmlElement element12 = htmlDocument.CreateElement("td");
      StreamResourceInfo streamResourceInfo = operationError.IsWarning ? Application.GetResourceStream(new Uri("pack://application:,,,/Intermech.Tools.Client;component/Resources/warningIcon.png", UriKind.RelativeOrAbsolute)) : Application.GetResourceStream(new Uri("pack://application:,,,/Intermech.Tools.Client;component/Resources/errorIcon.png", UriKind.RelativeOrAbsolute));
      if (streamResourceInfo != null)
      {
        XmlElement element13 = htmlDocument.CreateElement("img");
        using (MemoryStream destination = new MemoryStream())
        {
          streamResourceInfo.Stream.CopyTo((Stream) destination);
          string base64String = Convert.ToBase64String(destination.ToArray());
          element13.SetAttribute("src", "data:image/png;base64," + base64String);
          element13.SetAttribute("width", "20px");
          element13.SetAttribute("height", "20px");
          element13.SetAttribute("style", "vertical-align: middle;");
        }
        element12.AppendChild((XmlNode) element13);
      }
      element12.AppendChild((XmlNode) htmlDocument.CreateTextNode(operationError.Message));
      element11.AppendChild((XmlNode) element12);
      XmlElement element14 = htmlDocument.CreateElement("td");
      element14.AppendChild((XmlNode) htmlDocument.CreateTextNode(operationError.Solution));
      element11.AppendChild((XmlNode) element14);
      element7.AppendChild((XmlNode) element11);
    }
    xmlNode2.AppendChild((XmlNode) element7);
    return htmlDocument;
  }

  private void ErrorVertexDoubleClick()
  {
    WizardPageOperationErrorsVM.ErrorDoubleClickHandler errorDoubleClick = this.ErrorDoubleClick;
    if (errorDoubleClick == null)
      return;
    errorDoubleClick(this.SelectedErrorItem?.Vertex);
  }

  internal delegate void ErrorDoubleClickHandler(DBObjectGraphVertex vertex);
}
