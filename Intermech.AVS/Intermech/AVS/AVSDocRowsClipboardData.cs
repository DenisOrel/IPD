// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSDocRowsClipboardData
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Класс для копирования и вставки записей конструкторского документа через буфер Windows</summary>
[Serializable]
public class AVSDocRowsClipboardData
{
  /// <summary>Имя формата в буфере</summary>
  public static string ClipboardFormat = "AVSDocRow";
  /// <summary>примечания которые копируются в буфер</summary>
  public TableData[] DocRows;

  /// <summary>Конструктор</summary>
  /// <param name="docRows">Строки документа, которые копируются в буфер</param>
  public AVSDocRowsClipboardData(TableData[] docRows)
  {
    this.DocRows = docRows != null ? docRows : throw new ArgumentNullException(nameof (docRows));
  }

  /// <summary>Можно вставить узлы из буфера Windows</summary>
  /// <param name="destination">Приемник узлов</param>
  /// <returns>Можно вставить узлы из буфера Windows</returns>
  public static bool CanPasteDocRowsFromClipboard()
  {
    try
    {
      IDataObject dataObject = Clipboard.GetDataObject();
      if (dataObject != null)
      {
        foreach (string format in dataObject.GetFormats())
        {
          if (format == AVSDocRowsClipboardData.ClipboardFormat)
            return true;
        }
      }
      return false;
    }
    catch
    {
      return false;
    }
  }

  /// <summary>Получить записи из буфера</summary>
  /// <returns></returns>
  public static AVSDocRowsClipboardData GetDocRowsFromClipboard()
  {
    AVSDocRowsClipboardData rowsFromClipboard = (AVSDocRowsClipboardData) null;
    IDataObject dataObject = Clipboard.GetDataObject();
    if (dataObject != null)
    {
      rowsFromClipboard = (AVSDocRowsClipboardData) dataObject.GetData(AVSDocRowsClipboardData.ClipboardFormat);
      if (rowsFromClipboard != null && rowsFromClipboard.DocRows != null)
      {
        int index = 0;
        for (int length = rowsFromClipboard.DocRows.Length; index < length; ++index)
          rowsFromClipboard.DocRows[index].AssignClonedByTemplateWithParent(false);
      }
    }
    return rowsFromClipboard;
  }
}
