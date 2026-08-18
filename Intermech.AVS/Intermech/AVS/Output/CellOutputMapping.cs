// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.CellOutputMapping
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Extensions;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

#nullable disable
namespace Intermech.AVS.Output;

/// <summary>
/// Базовый класс для модели данных схемы вывода атрибутов
/// </summary>
public class CellOutputMapping
{
  public string SectionGuid { get; set; } = string.Empty;

  public string ObjTypeGuid { get; set; } = string.Empty;

  public string CellId { get; set; } = string.Empty;

  public List<OutputMappingBase> Items { get; } = new List<OutputMappingBase>();

  public int Length => this.Items.Count;

  public bool IsHidden => this.Items.Count == 0;

  public bool HasBlankOutput
  {
    get
    {
      return this.Items.Count == 1 && this.Items[0] is DelimiterMapping delimiterMapping && delimiterMapping.IsEmptyStub;
    }
  }

  public bool IsEmpty => this.Items.IsNullOrEmpty<OutputMappingBase>();

  public CellOutputMapping()
  {
  }

  public CellOutputMapping(string cellId, OutputMappingBase item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    this.CellId = !string.IsNullOrEmpty(cellId) ? cellId : throw new ArgumentNullException(nameof (cellId));
    this.Add(item);
  }

  internal void Add(AttributeMapping attribute, DelimiterMapping delimiter)
  {
    this.Add((OutputMappingBase) attribute);
    this.Add((OutputMappingBase) delimiter);
  }

  internal void Add(OutputMappingBase item)
  {
    item.Order = this.Items.Count;
    item.Owner = this;
    this.Items.Add(item);
  }

  internal CellOutputMapping Hide()
  {
    this.Items.Clear();
    return this;
  }

  internal static CellOutputMapping FromNode(CellNode cellNode)
  {
    if (cellNode == null)
      return (CellOutputMapping) null;
    string str1 = Guid.Empty.ToString();
    string str2 = str1;
    if (cellNode.Parent is SectionNode parent2)
      str1 = parent2.SectionGuid;
    else if (cellNode.Parent is ObjTypeNode parent1)
    {
      str2 = parent1.ObjectTypeGuid;
      str1 = parent1.Parent is SectionNode parent ? parent.SectionGuid : str1;
    }
    CellOutputMapping cellOutputMapping = new CellOutputMapping()
    {
      CellId = cellNode.Id,
      SectionGuid = str1,
      ObjTypeGuid = str2
    };
    foreach (TreeNode node in cellNode.Nodes)
    {
      if (node is AttributeNode attributeNode)
        cellOutputMapping.Add((OutputMappingBase) new AttributeMapping(attributeNode.AttributeInfo));
      if (node is DelimiterNode delimiterNode)
        cellOutputMapping.Add((OutputMappingBase) DelimiterMapping.Create(delimiterNode.Delimiter));
    }
    if (cellNode.Nodes.Count == 0)
      cellOutputMapping.Add((OutputMappingBase) DelimiterMapping.EmptyStub);
    return cellOutputMapping;
  }

  internal virtual XElement ToXML()
  {
    XElement xml = new XElement((XName) "CellOutput", new object[3]
    {
      (object) new XAttribute((XName) "Sid", (object) this.SectionGuid),
      (object) new XAttribute((XName) "Oid", (object) this.ObjTypeGuid),
      (object) new XAttribute((XName) "Cid", (object) this.CellId)
    });
    foreach (OutputMappingBase outputMappingBase in this.Items)
      xml.Add((object) outputMappingBase.ToXML());
    return xml;
  }

  /// <summary>Объединить по формуле значения атрибутов с разделителями в один текст</summary>
  public string ConcatenateAttributesValues(
    GetFieldValueByCellOutputMapping GetFieldStringValue)
  {
    return GetFieldStringValue != null ? this.ConcatenateAttributesValues((IList<string>) this.CollectValuesForConcatenate(GetFieldStringValue), out IList<string> _) : throw new ArgumentNullException(nameof (GetFieldStringValue));
  }

  /// <summary>Объединить по формуле значения атрибутов с разделителями в один текст</summary>
  /// <param name="itemValues">Список значений атрибутов и разделителей в порядке Items</param>
  /// <param name="itemValuesWithoutUnnecessaryDelimeters">Копия списка значений атрибутов и разделителей, очищенная от лишних разделителей после пустых атрибутов</param>
  /// <returns></returns>
  public string ConcatenateAttributesValues(
    IList<string> itemValues,
    out IList<string> itemValuesWithoutUnnecessaryDelimeters)
  {
    if (itemValues == null)
      throw new ArgumentNullException(nameof (itemValues));
    if (itemValues.Count != this.Items.Count)
      throw new ArgumentException(nameof (itemValues), $"Количество {nameof (itemValues)}: {itemValues.Count} не соответствует количеству {"Items"}: {this.Items.Count}");
    itemValuesWithoutUnnecessaryDelimeters = (IList<string>) new List<string>((IEnumerable<string>) itemValues);
    this.RemoveDelimetersForEmptyAttributes(itemValuesWithoutUnnecessaryDelimeters);
    return string.Concat((IEnumerable<string>) itemValuesWithoutUnnecessaryDelimeters);
  }

  private List<string> CollectValuesForConcatenate(
    GetFieldValueByCellOutputMapping GetFieldStringValue)
  {
    List<string> stringList = new List<string>(this.Items.Count);
    foreach (OutputMappingBase outputMappingBase in this.Items)
    {
      string str = outputMappingBase is AttributeMapping attrMapping ? GetFieldStringValue(attrMapping) : ((DelimiterMapping) outputMappingBase).DelimiterRTF;
      stringList.Add(str);
    }
    return stringList;
  }

  public void RemoveDelimetersForEmptyAttributes(IList<string> itemValues)
  {
    if (itemValues == null)
      throw new ArgumentNullException(nameof (itemValues));
    if (itemValues.Count != this.Items.Count)
      throw new ArgumentException(nameof (itemValues), $"Количество {nameof (itemValues)}: {itemValues.Count} не соответствует количеству {"Items"}: {this.Items.Count}");
    bool flag = false;
    int index1 = -1;
    int index2 = -1;
    for (int index3 = 0; index3 < this.Items.Count; ++index3)
    {
      if (this.Items[index3] is AttributeMapping)
      {
        flag = string.IsNullOrEmpty(itemValues[index3]);
        if (index1 == -1)
          index1 = index3;
        index2 = index3;
      }
      else if (flag)
        itemValues[index3] = "";
    }
    if (index2 != -1 && string.IsNullOrEmpty(itemValues[index2]))
    {
      for (int index4 = index2 - 1; index4 >= 0 && (!(this.Items[index4] is AttributeMapping) || string.IsNullOrEmpty(itemValues[index4])); --index4)
        itemValues[index4] = "";
    }
    if (index1 == -1 || !string.IsNullOrEmpty(itemValues[index1]))
      return;
    for (int index5 = 0; index5 < index1; ++index5)
      itemValues[index5] = "";
  }

  public override string ToString() => this.CellId;

  public bool ContainsAttribute(AttributeInfo attribute)
  {
    return !this.Items.IsNullOrEmpty<OutputMappingBase>() && this.Items.OfType<AttributeMapping>().Any<AttributeMapping>((Func<AttributeMapping, bool>) (a => a.Equals((object) attribute)));
  }

  public bool ContainsAttribute(Func<AttributeInfo, bool> predicate)
  {
    return !this.Items.IsNullOrEmpty<OutputMappingBase>() && this.Items.OfType<AttributeMapping>().Any<AttributeMapping>((Func<AttributeMapping, bool>) (a => predicate(a.AttributeInfo)));
  }

  public IEnumerable<AvsRowAttributeInfo> Attributes
  {
    get
    {
      return this.Items.OfType<AttributeMapping>().Select<AttributeMapping, AvsRowAttributeInfo>((Func<AttributeMapping, AvsRowAttributeInfo>) (am => am.AttributeInfo is AvsRowAttributeInfo attributeInfo ? attributeInfo : new AvsRowAttributeInfo(am.AttributeInfo)));
    }
  }
}
