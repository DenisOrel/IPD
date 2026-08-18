// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.TableRecordsApplicabilityColors
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params;

/// <summary>
/// Цвет записи таблицы в зависимости от атрибута Применяемость
/// </summary>
/// 
///             При добавлении новых свойств, которые должны сохраняться
///             применять к ним атрибут Optional, чтоб не поломалась десериализация
[Serializable]
public class TableRecordsApplicabilityColors
{
  public override string ToString() => string.Empty;

  /// <summary>Цвет строки, где применяемость без ограничений</summary>
  public Color NoResrictionsRecColor { get; set; }

  public Color DenyAddRecordRecColor { get; set; }

  public Color DenyAddObjectRecColor { get; set; }

  public Color DenyAllRecColor { get; set; }

  public byte[] SavedData
  {
    get => this.GetData();
    set => this.SetData(value);
  }

  private void SetData(byte[] data)
  {
    try
    {
      if (data == null || data.Length == 0)
        return;
      using (MemoryStream serializationStream = new MemoryStream(data))
      {
        if (!(new BinaryFormatter().Deserialize((Stream) serializationStream) is TableRecordsApplicabilityColors applicabilityColors))
          return;
        this.NoResrictionsRecColor = applicabilityColors.NoResrictionsRecColor;
        this.DenyAddRecordRecColor = applicabilityColors.DenyAddRecordRecColor;
        this.DenyAddObjectRecColor = applicabilityColors.DenyAddObjectRecColor;
        this.DenyAllRecColor = applicabilityColors.DenyAllRecColor;
      }
    }
    catch (Exception ex)
    {
    }
  }

  private byte[] GetData()
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this);
      return serializationStream.ToArray();
    }
  }
}
