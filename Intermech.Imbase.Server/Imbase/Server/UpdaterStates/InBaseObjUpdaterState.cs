// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.UpdaterStates.InBaseObjUpdaterState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Synchronization;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Imbase.Server.UpdaterStates;

internal class InBaseObjUpdaterState : IObjUpdaterState
{
  public SynchObjectsStatus Handle(SynchronizationAttributesUpdater context)
  {
    context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, "Попытка обновить атрибуты объекта непосредственно в базе.");
    string readOnlyAttrInfo;
    if (!this.HasReadOnlyAttributes(context, out readOnlyAttrInfo))
      return this.WriteAttributeValues(context);
    context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, readOnlyAttrInfo);
    return SynchObjectsStatus.NotSynchronized;
  }

  protected internal bool HasReadOnlyAttributes(
    SynchronizationAttributesUpdater context,
    out string readOnlyAttrInfo)
  {
    readOnlyAttrInfo = string.Empty;
    IDBAttribute[] array = context.NewAttributeValues.Select<AttributeValues, IDBAttribute>((Func<AttributeValues, IDBAttribute>) (x => context.Obj.GetAttributeByID(x.AttributeID))).Where<IDBAttribute>((Func<IDBAttribute, bool>) (x => x != null && x.ReadOnly)).ToArray<IDBAttribute>();
    if (array.Length == 0)
      return false;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("Следующие атрибуты нельзя модифицировать:");
    foreach (IDBAttribute dbAttribute in array)
      stringBuilder.AppendLine($" - '{dbAttribute.Name}' [{dbAttribute.AttributeID}]");
    readOnlyAttrInfo = stringBuilder.ToString();
    return true;
  }

  private SynchObjectsStatus WriteAttributeValues(SynchronizationAttributesUpdater context)
  {
    Dictionary<string, Exception> dictEx = new Dictionary<string, Exception>();
    AttributeValues[] attributesValues = context.Obj.GetAttributesValues(context.AttributeValuesModes);
    AttributeValues[] savedAVs;
    try
    {
      List<AttributeValues> list = context.NewAttributeValues.ToList<AttributeValues>();
      list.ForEach((Action<AttributeValues>) (x =>
      {
        x.ReadOnly = false;
        x.ThrowSetException = false;
      }));
      savedAVs = ((DBAttributable) context.Obj).SetAttributesValues(list.ToArray(), false, true, true, GetAttributeValuesModes.IncludeName, dictEx);
    }
    catch (Exception ex)
    {
      context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, $"{LocalizationHolder.rm.GetString("Imbase_Obj_SaveAttrs_Error")}:{Environment.NewLine}{ex.Message}");
      return SynchObjectsStatus.NotSynchronized;
    }
    if (dictEx.Count > 0)
    {
      context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, LocalizationHolder.rm.GetString("Imbase_Obj_SaveAttrs_ErrorList") + ":");
      string format = LocalizationHolder.rm.GetString("Imbase_Attr_Message");
      foreach (KeyValuePair<string, Exception> keyValuePair in dictEx)
        context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, string.Format(format, (object) keyValuePair.Key, (object) MetaDataHelper.GetAttributeByTypeNameID(keyValuePair.Key).ToString(), (object) keyValuePair.Value.Message));
      return SynchObjectsStatus.NotSynchronized;
    }
    AttributeValues[] array = context.NewAttributeValues.Where<AttributeValues>((Func<AttributeValues, bool>) (x => !dictEx.ContainsKey(x.AttributeName))).ToArray<AttributeValues>();
    if (array.Length != 0)
    {
      context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, LocalizationHolder.rm.GetString("Imbase_Obj_SaveAttrs_Success") + ":");
      context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, this.GetString(attributesValues, array));
    }
    if (savedAVs != null && savedAVs.Length != 0)
    {
      context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, LocalizationHolder.rm.GetString("Imbase_Obj_SaveAttrs_ChangedByServer") + ":");
      context.Log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Normal, this.GetString(attributesValues, savedAVs));
    }
    return SynchObjectsStatus.Synchronized;
  }

  private string GetString(AttributeValues[] oldAVs, AttributeValues[] savedAVs)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    string format1 = LocalizationHolder.rm.GetString("Imbase_Attr_OldValue_NewValue");
    string format2 = LocalizationHolder.rm.GetString("Imbase_Attr_NewValue");
    string format3 = LocalizationHolder.rm.GetString("Imbase_Attr_OldValues_NewValues");
    string format4 = LocalizationHolder.rm.GetString("Imbase_Attr_NewValues");
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    foreach (AttributeValues savedAv in savedAVs)
    {
      AttributeValues av = savedAv;
      AttributeValues attributeValues = ((IEnumerable<AttributeValues>) oldAVs).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeName == av.AttributeName));
      if (av.MultipleValued == MultiValueModes.SingleValue || av.MultipleValued == MultiValueModes.SingleValueFromList)
      {
        StringBuilder stringBuilder2 = stringBuilder1;
        string str;
        if (attributeValues == null)
          str = string.Format(format2, (object) av.AttributeName, (object) av.AttributeID.ToString(), (object) Convert.ToString(av.Values[0]));
        else
          str = string.Format(format1, (object) av.AttributeName, (object) av.AttributeID.ToString(), (object) Convert.ToString(attributeValues.Values[0]), (object) Convert.ToString(av.Values[0]));
        stringBuilder2.AppendLine(str);
      }
      else
      {
        foreach (object obj in av.Values)
          empty2 += $"{Convert.ToString(obj)}; ";
        if (attributeValues != null)
        {
          foreach (object obj in attributeValues.Values)
            empty1 += $"{Convert.ToString(obj)}; ";
          stringBuilder1.AppendLine(string.Format(format3, (object) av.AttributeName, (object) av.AttributeID.ToString(), (object) empty1, (object) empty2));
        }
        else
          stringBuilder1.AppendLine(string.Format(format4, (object) av.AttributeName, (object) av.AttributeID.ToString(), (object) empty2));
      }
    }
    return stringBuilder1.ToString();
  }
}
