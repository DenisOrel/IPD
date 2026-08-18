// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Navigator.Commands.DocumentTechCardCommands
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Intermech.DataFormats;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Expert.User;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Generate.Interfaces;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Navigator.Commands;

internal class DocumentTechCardCommands
{
  private readonly DocumentTechCardCommandsEnum _commands;

  private long SelectDocConfig()
  {
    DescriptorCollection descriptors = new DescriptorCollection()
    {
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(BlankConsts.ObjectType.BlankSetupId)
    };
    IDescriptor rootDescriptor = descriptors.Count != 1 ? (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("TechCard.Document_011"), descriptors) : descriptors[0];
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("TechCard.Document_010"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default);
    return objArray != null && objArray.Length != 0 ? ((IDBTypedObjectID) objArray[0]).ObjectID : 0L;
  }

  public static ImDocument UnpackImDocument(byte[] zipScr, bool updateDoc)
  {
    if (zipScr == null)
      return (ImDocument) null;
    using (Stream baseInputStream = (Stream) new MemoryStream(zipScr))
    {
      using (InflaterInputStream inflaterInputStream = new InflaterInputStream(baseInputStream))
        return ImDocument.LoadFromXml((Stream) inflaterInputStream, updateDoc, true, false);
    }
  }

  public DocumentTechCardCommands(DocumentTechCardCommandsEnum commands)
  {
    this._commands = commands;
  }

  public void Execute(ISelectedItems items, System.IServiceProvider viewServices, object additionalInfo)
  {
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    long configId = this.SelectDocConfig();
    if (configId == 0L)
      return;
    IExpertUser service1 = ServiceUtils.GetService<IExpertUser>((object) ApplicationServices.Container, false);
    if (service1 == null)
      return;
    ImDocument document = (ImDocument) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechCardDocumentService service2 = ServiceUtils.GetService<ITechCardDocumentService>((object) sessionKeeper.Session, true);
      using (IExpertTask expertTask = service1.GetExpertTask())
      {
        TechCardDocumentGenerateParameter parameter = new TechCardDocumentGenerateParameter(configId, itemData.ObjectID)
        {
          ExpertTaskId = expertTask.TaskId
        };
        try
        {
          long docId;
          if (service2.GenerateDocument(sessionKeeper.Session.SessionGUID, parameter, out docId))
            document = DocumentEditorPluginBase.LoadDocumentFromDBObject(sessionKeeper.Session, docId) as ImDocument;
          if (service1.ShowTraceWindow)
          {
            ExpertUser.rur.Clear();
            ExpertUser.rur.Execute(expertTask.GetTraceInfo(), true);
          }
        }
        finally
        {
          parameter.ExpertTaskId = 0;
        }
      }
    }
    if (document == null)
      return;
    DocumentEditorPlugin.Instance.OpenImDocument(document, true);
    int num = (int) MessageBox.Show($"Выводим документ {configId.ToString()} для техпроцесса {itemData.ObjectID.ToString()}");
  }
}
