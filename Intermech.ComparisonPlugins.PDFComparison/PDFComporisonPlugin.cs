using Intermech.Client.Core;
using Intermech.ComparisonPlugins.PDFComparison.Common;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Intermech.ComparisonPlugins.PDFComparison
{
    public class PDFComporisonPlugin : IPackage, ICanCompareObjectsFiles, ICanComparePDFFiles
    {
      internal static IServiceProvider _serviceProvider;

      public string Name => "Сравнение PDF";

      public void Load(IServiceProvider serviceProvider)
      {
        PDFComporisonPlugin._serviceProvider = serviceProvider;
        HelperConsts.Initialize();
        this.RegisterPluginCompareFilesService();
      }

      private void RegisterPluginCompareFilesService()
      {
        (PDFComporisonPlugin._serviceProvider.GetService(typeof (ICompareFilesService)) as ICompareFilesService).AddPluginToCompareFilesService((ICanCompareObjectsFiles) this);
      }

      public void Unload()
      {
      }

      public string UniqueName => this.Name;

      public string NameInMessages => this.Name;

      public ReadOnlyCollection<int> TypeIds => HelperConsts.ComparedObjectTypes.AsReadOnly();

      public void CompareFilesFor(
        DBObjectToCompare object1,
        DBObjectToCompare object2,
        FileTypes fileType)
      {
        if (fileType == FileTypes.ftAuthentical)
          this.CompareAuthenticalFiles(object1, object2);
        else
          new OpenForComparisonProvider().ShowСomparisonWindow((IEnumerable<long>) new long[2]
          {
            object1.ObjectID,
            object2.ObjectID
          });
      }

      private void CompareAuthenticalFiles(DBObjectToCompare object1, DBObjectToCompare object2)
      {
        new CompareAuthenticFilesProvider().ShowСomparisonWindow((IEnumerable<long>) new long[2]
        {
          object1.ObjectID,
          object2.ObjectID
        });
      }

      public void RemoveTypeId(int typeId)
      {
      }

      internal void CreateContextMenuCommand()
      {
        IFactory service = PDFComporisonPlugin._serviceProvider.GetService(typeof (IFactory)) as IFactory;
        MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
        MenuTemplateNode node = service.ContextMenuTemplate["PDFComparison"];
        if (node == null)
        {
          node = new MenuTemplateNode("PDFComparison", "Сравнение PDF", -1, 20, int.MaxValue);
          contextMenuTemplate.Nodes.Add(node);
        }
        node.Nodes.Add(new MenuTemplateNode("OpenForComparison", "Открыть для сравнения", -1, 20, int.MaxValue));
        node.Nodes.Add(new MenuTemplateNode("СompareWithBaseVersion", "Сравнить с базовой версией", -1, 20, int.MaxValue));
        service.AddCommandsProvider(1, (ICommandsProvider) new CommandProvider());
      }
    }
}
