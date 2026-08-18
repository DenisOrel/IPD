
// Type: Intermech.Controls.ExceptionToInlinesConverter
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.UI.ExceptionHandling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;


namespace Intermech.Controls;

/// <summary>
/// Выполняет преобразование обычных строк в форматированный WPF-текст с использованием объектов типа <see cref="T:System.Windows.Documents.Inline" />.
/// При обработке в тексте строки ищутся упоминания объектов IPS, которые заменяются на гиперссылки для быстрого перехода к ним.
/// Класс используется для отображения сообщений объектов <see cref="T:System.Exception" /> средствами WPF.
/// </summary>
[ValueConversion(typeof (ExceptionVM), typeof (FlowDocument))]
[ValueConversion(typeof (ExceptionVM), typeof (IEnumerable<Inline>))]
internal sealed class ExceptionToInlinesConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is ExceptionVM exceptionVM && this.IsTargetTypeSupported(targetType))
    {
      List<Inline> inlines = this.ConvertExceptionToInlines(exceptionVM);
      if (targetType == typeof (IEnumerable<Inline>))
        return (object) inlines;
      if (targetType == typeof (FlowDocument))
      {
        Paragraph paragraph = new Paragraph();
        paragraph.Inlines.AddRange((IEnumerable) inlines);
        FlowDocument flowDocument = new FlowDocument();
        flowDocument.Blocks.Add((Block) paragraph);
        return (object) flowDocument;
      }
    }
    return DependencyProperty.UnsetValue;
  }

  private bool IsTargetTypeSupported(Type targetType)
  {
    return targetType == typeof (IEnumerable<Inline>) || targetType == typeof (FlowDocument);
  }

  private List<Inline> ConvertExceptionToInlines(ExceptionVM exceptionVM)
  {
    if (exceptionVM.Exception != null && exceptionVM.RecoveryHandler != null)
    {
      string message = exceptionVM.Message;
      IEnumerable<ErrorRecoveryAction> recoveryActions = exceptionVM.Exception != null ? exceptionVM.Exception.EnumerateRecoveryActions() : (IEnumerable<ErrorRecoveryAction>) new ErrorRecoveryAction[0];
      List<InTextActionPlacementRecord> actionPlacementRecordList = exceptionVM.RecoveryHandler.PlaceRecoveryActions(message, recoveryActions);
      if (actionPlacementRecordList.Count != 0)
      {
        List<Inline> inlines = new List<Inline>(actionPlacementRecordList.Count * 2 + 1);
        int startIndex = 0;
        foreach (InTextActionPlacementRecord actionPlacementRecord in actionPlacementRecordList)
        {
          if (startIndex < actionPlacementRecord.Index)
            inlines.Add((Inline) new Run(message.Substring(startIndex, actionPlacementRecord.Index - startIndex)));
          Hyperlink hyperlink = new Hyperlink((Inline) new Run(actionPlacementRecord.AnchorText));
          hyperlink.NavigateUri = actionPlacementRecord.ActionUri;
          hyperlink.Tag = (object) exceptionVM;
          hyperlink.RequestNavigate += new RequestNavigateEventHandler(this.OnNavigateInternal);
          inlines.Add((Inline) hyperlink);
          startIndex = actionPlacementRecord.Index + actionPlacementRecord.AnchorText.Length;
        }
        if (startIndex < message.Length)
          inlines.Add((Inline) new Run(message.Substring(startIndex, message.Length - startIndex)));
        return inlines;
      }
    }
    return new List<Inline>(1)
    {
      (Inline) new Run(exceptionVM.Message)
    };
  }

  private void OnNavigateInternal(object sender, RequestNavigateEventArgs e)
  {
    if (!(((FrameworkContentElement) e.Source).Tag is ExceptionVM tag) || tag.RecoveryHandler == null)
      return;
    tag.RecoveryHandler.TryInvokeRecoveryAction(e.Uri);
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
