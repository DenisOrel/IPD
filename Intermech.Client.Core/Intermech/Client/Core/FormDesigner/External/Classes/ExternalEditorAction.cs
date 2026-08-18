
// Type: Intermech.Client.Core.FormDesigner.External.Classes.ExternalEditorAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Client.Core.FormDesigner.External.Classes;

/// <summary>
/// 
/// </summary>
internal class ExternalEditorAction : IFormDesignerActionHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  public bool ButtonEnabled(object button, object form)
  {
    bool flag = false;
    DesForm desForm = form as DesForm;
    AttrButton attrButton = button as AttrButton;
    if (desForm != null && attrButton != null)
    {
      if (attrButton.Tag is bool && Convert.ToBoolean(attrButton.Tag))
        flag = attrButton.Enabled;
      else if (attrButton.FormDesignerActionParams is ExternalEditorActionParams designerActionParams)
      {
        if (designerActionParams.AttributeEditor == null)
        {
          Dictionary<int, List<IAttributeEditor>> editors1 = desForm.GetEditors(desForm.Info.ElementIdentifier);
          int attributeId = MetaDataHelper.GetAttributeID((object) designerActionParams.AttributeInfo.AttributeGuid);
          IAttributeEditor attributeEditor = (IAttributeEditor) null;
          if (editors1.ContainsKey(attributeId))
          {
            attributeEditor = editors1[attributeId][0];
          }
          else
          {
            IElementInfo relationInfo = desForm.RelationInfo;
            Dictionary<int, List<IAttributeEditor>> editors2 = relationInfo != null ? desForm.GetEditors(relationInfo.ElementIdentifier) : (Dictionary<int, List<IAttributeEditor>>) null;
            if (editors2 != null && editors2.ContainsKey(attributeId))
              attributeEditor = editors2[attributeId][0];
          }
          if (attributeEditor != null && (attributeEditor as Control).Enabled)
          {
            designerActionParams.AttributeEditor = attributeEditor;
            attrButton.Tag = (object) true;
            ExternalEditorParams externalEditorParams = new ExternalEditorParams();
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(designerActionParams.Editor, false);
              if (dbObject != null)
              {
                IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(ExternalEditorConsts.ExternalEditorParamsAttributeType);
                if (attributeByGuid != null)
                {
                  using (MemoryStream memoryStream = new MemoryStream())
                  {
                    BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
                    blobProcReader.ReadData();
                    if (blobProcReader.Result)
                    {
                      if (memoryStream.Length > 0L)
                      {
                        memoryStream.Position = 0L;
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load((Stream) memoryStream);
                        if (externalEditorParams.Load((XmlNode) xmlDocument.DocumentElement))
                        {
                          designerActionParams.ExternalEditorParams = externalEditorParams;
                          (attributeEditor as Control).Enabled = !externalEditorParams.LockControl;
                          flag = designerActionParams.CurrentButtonState = true;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        else
          flag = designerActionParams.CurrentButtonState;
      }
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  public void ButtonPressed(object button, object form)
  {
    DesForm desForm = form as DesForm;
    AttrButton attrButton = button as AttrButton;
    if (desForm == null || attrButton == null || !(attrButton.FormDesignerActionParams is ExternalEditorActionParams designerActionParams))
      return;
    ExternalEditorParams externalEditorParams = designerActionParams.ExternalEditorParams;
    IDataObject data1 = (IDataObject) new DataObject();
    IAttributeEditor attributeEditor = designerActionParams.AttributeEditor;
    AttributeValues values = attributeEditor.Values;
    object data2 = values.Values[0];
    System.Type format = data2 != null ? data2.GetType() : throw new ArgumentException(LocalizationHolder.rm.GetString("Client.Core_177"));
    System.Type type;
    switch (values.AttributeType)
    {
      case FieldTypes.ftInteger:
        type = typeof (long);
        break;
      case FieldTypes.ftDouble:
        type = typeof (double);
        break;
      default:
        type = typeof (string);
        break;
    }
    TypeConverter converter = TypeDescriptor.GetConverter(type);
    string name = externalEditorParams.Command;
    switch (externalEditorParams.Send)
    {
      case SendMethod.CommandString:
        name = name + (name.Length > 0 ? " " : string.Empty) + $"\"{Convert.ToString(data2)}\"";
        break;
      case SendMethod.File:
        using (StreamWriter sw = new StreamWriter((Stream) new FileStream(Environment.ExpandEnvironmentVariables(externalEditorParams.SwapFile), FileMode.Create), Encoding.UTF8))
        {
          if (externalEditorParams.SendAllAttributes)
          {
            IElementInfo elementInfo = attributeEditor is IParent4Control parent4Control ? parent4Control.ParentInfo : desForm.Info;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBAttributable dbAttributable = (IDBAttributable) null;
              if (elementInfo.ElementKind == AttributableElements.Object)
                dbAttributable = (IDBAttributable) sessionKeeper.Session.GetObject(elementInfo.ElementIdentifier);
              else if (elementInfo.ElementKind == AttributableElements.Relation)
                dbAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(elementInfo.ElementIdentifier);
              if (dbAttributable != null)
              {
                AttributeValues[] attributesValues = dbAttributable.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
                List<string> stringList = new List<string>((IEnumerable<string>) new string[1]
                {
                  $"{MetaDataHelper.GetAttributeTypeName(values.AttributeID)} = {Convert.ToString(data2)}"
                });
                List<Guid> guidList = new List<Guid>((IEnumerable<Guid>) new Guid[1]
                {
                  values.AttributeGuid
                });
                foreach (AttributeValues attributeValues in attributesValues)
                {
                  if (!guidList.Contains(attributeValues.AttributeGuid))
                  {
                    stringList.Add($"{attributeValues.AttributeName} = {Convert.ToString(attributeValues.Values[0])}");
                    guidList.Add(attributeValues.AttributeGuid);
                  }
                }
                stringList.ForEach((Action<string>) (x => sw.WriteLine(x)));
                break;
              }
              break;
            }
          }
          sw.Write(Convert.ToString(data2));
          break;
        }
      case SendMethod.Clipboard:
        data1.SetData(data2);
        Clipboard.SetDataObject((object) data1);
        break;
    }
    if (!File.Exists(Environment.ExpandEnvironmentVariables(externalEditorParams.SwapFile)))
    {
      using (FileStream fileStream = new FileStream(Environment.ExpandEnvironmentVariables(externalEditorParams.SwapFile), FileMode.Create))
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream, Encoding.UTF8))
          streamWriter.Close();
        fileStream.Close();
      }
    }
    Process.Start(new ProcessStartInfo(Environment.ExpandEnvironmentVariables(externalEditorParams.Path), Environment.ExpandEnvironmentVariables(name))
    {
      UseShellExecute = false
    }).WaitForExit();
    switch (externalEditorParams.Receive)
    {
      case ReceiveMethod.NotReturn:
        return;
      case ReceiveMethod.File:
        if (!File.Exists(Environment.ExpandEnvironmentVariables(externalEditorParams.SwapFile)))
        {
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_175"), (object) Environment.ExpandEnvironmentVariables(externalEditorParams.SwapFile)), LocalizationHolder.rm.GetString("Client.Core_176"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          break;
        }
        string str = string.Empty;
        using (StreamReader streamReader = new StreamReader((Stream) new FileStream(Environment.ExpandEnvironmentVariables(externalEditorParams.SwapFile), FileMode.Open)))
        {
          if (attributeEditor is AttrMemoEdit)
          {
            str = streamReader.ReadToEnd();
          }
          else
          {
            object obj;
            if ((string) (obj = (object) streamReader.ReadLine()) != null)
              str = Convert.ToString(obj).TrimEnd('\r', '\n');
          }
        }
        AttributeValues attributeValues1 = values.Clone() as AttributeValues;
        if (!string.IsNullOrEmpty(str))
          attributeValues1.Values = new object[1]
          {
            converter.ConvertFrom((object) str)
          };
        else
          attributeValues1.Values = new object[1]
          {
            (object) DBNull.Value
          };
        attributeEditor.Values = values;
        break;
      case ReceiveMethod.Clipboard:
        IDataObject dataObject = Clipboard.GetDataObject();
        if (dataObject.GetDataPresent(format))
        {
          attributeEditor.Values = new AttributeValues(values.AttributeID, dataObject.GetData(format));
          break;
        }
        break;
    }
    File.Delete(Environment.ExpandEnvironmentVariables(externalEditorParams.SwapFile));
  }
}
