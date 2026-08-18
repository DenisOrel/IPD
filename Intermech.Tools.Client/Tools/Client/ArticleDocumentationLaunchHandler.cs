// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.ArticleDocumentationLaunchHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class ArticleDocumentationLaunchHandler : ParameterlessLaunchHandler
{
  private ILaunchActionService launchActionService;

  public ArticleDocumentationLaunchHandler(ILaunchActionService launchActionService)
    : base(new Guid("1BF9C0B6-F43E-409A-97F4-D33BAB1AAA8E"), LocalizationHolder.rm.GetString("Tools.Client_108"))
  {
    this.launchActionService = launchActionService;
  }

  public override void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    if (handlerData == null)
      throw new ArgumentNullException(nameof (handlerData));
    List<long> articleDocuments = DBDocumentHelper.FindArticleDocuments(launchParams.ObjectId, false, launchParams.VersionsRule);
    if (articleDocuments.Count == 0)
    {
      string caption = EnumTypeHelper.GetCaption((Enum) launchParams.LaunchType);
      string objectNameInMessages = DBHelper.GetObjectNameInMessages(launchParams.ObjectId);
      int num = (int) MessageBox.Show($"Невозможно выполнить команду запуска '{caption}', так как у изделия '{objectNameInMessages}' отсутствуют связанные с ним документы.", caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      long objectId;
      if (articleDocuments.Count == 1)
      {
        objectId = articleDocuments[0];
      }
      else
      {
        DescriptorCollection descriptors = new DescriptorCollection();
        foreach (long objID in articleDocuments)
          descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objID));
        IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Tools.Client_105"), descriptors);
        long[] numArray = SelectionWindow.SelectObjects(EnumDescConverter.GetEnumDescription((Enum) launchParams.LaunchType), LocalizationHolder.rm.GetString("Tools.Client_106"), rootDescriptor, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect | SelectionOptions.ForceFilterObjectsByRule);
        if (numArray == null || numArray.Length == 0)
          return;
        objectId = numArray[0];
      }
      launchParams.ChangeObject(objectId, DBHelper.GetObjectType(objectId));
      this.MakeArticleLaunchContext(launchParams);
      this.launchActionService.Launch(launchParams);
    }
  }

  private void MakeArticleLaunchContext(LaunchParams documentLaunchParams)
  {
    int objTypeId = DBHelper.GetObjTypeID(documentLaunchParams.ObjectId);
    IntegratorObject integrator = IntegratorServices.Find(objTypeId);
    if (integrator == null)
      return;
    IArticleLaunchActionSupport service = IntegratorServices.GetService<IArticleLaunchActionSupport>(integrator, false);
    if (service == null)
      return;
    long originalObjectId = documentLaunchParams.OriginalObjectId;
    if (!service.IsSupported(originalObjectId, documentLaunchParams, objTypeId))
      return;
    service.MakeLaunchContext(originalObjectId, documentLaunchParams);
  }
}
