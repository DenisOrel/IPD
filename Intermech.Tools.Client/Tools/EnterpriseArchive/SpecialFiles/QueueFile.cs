// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.SpecialFiles.QueueFile
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.IO;
using Intermech.Tools.Integrators.FileTrees;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.SpecialFiles;

internal sealed class QueueFile
{
  private const int FileListCapacity = 2048 /*0x0800*/;
  private const int StageCapacity = 32 /*0x20*/;
  private readonly PathDictionary<int> pathToStageIndex;
  private readonly PathDictionary<ICollection<string>> pathToStageBucket;
  private readonly List<IImportStage> importStages;
  private readonly ReadOnlyCollection<IImportStage> importStagesWrapper;
  private readonly PathDictionary<ReadOnlyFileTreeNode> pathToDocumentNode;
  private readonly LinkedList<ReadOnlyFileTreeNode> documentNodes;

  public QueueFile()
  {
    this.pathToStageIndex = new PathDictionary<int>(2048 /*0x0800*/);
    this.pathToStageBucket = new PathDictionary<ICollection<string>>(2048 /*0x0800*/);
    this.importStages = new List<IImportStage>(32 /*0x20*/);
    this.importStagesWrapper = new ReadOnlyCollection<IImportStage>((IList<IImportStage>) this.importStages);
    this.pathToDocumentNode = new PathDictionary<ReadOnlyFileTreeNode>(2048 /*0x0800*/);
    this.documentNodes = new LinkedList<ReadOnlyFileTreeNode>();
  }

  public void Clear()
  {
    this.pathToStageIndex.Clear();
    this.pathToStageBucket.Clear();
    this.importStages.Clear();
    this.pathToDocumentNode.Clear();
    this.documentNodes.Clear();
  }

  public ICollection<IImportStage> ImportStages
  {
    get => (ICollection<IImportStage>) this.importStagesWrapper;
  }

  public int DocumentNodesCount => this.documentNodes.Count;

  public int FindStageIndex(string path)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    int stageIndex;
    if (!this.pathToStageIndex.TryGetValue(path, out stageIndex))
      stageIndex = -1;
    return stageIndex;
  }

  public ICollection<string> FindStageBucket(string path)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    ICollection<string> stageBucket;
    this.pathToStageBucket.TryGetValue(path, out stageBucket);
    return stageBucket;
  }

  public ReadOnlyFileTreeNode FindDocument(string path)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    ReadOnlyFileTreeNode document;
    this.pathToDocumentNode.TryGetValue(path, out document);
    return document;
  }

  public LinkedList<FileBucket> GroupFilesByQueue(ICollection<string> pathList)
  {
    Dictionary<object, LinkedList<string>> dictionary = pathList != null ? new Dictionary<object, LinkedList<string>>(pathList.Count) : throw new ArgumentNullException(nameof (pathList));
    LinkedList<string> collection = new LinkedList<string>();
    foreach (string path in (IEnumerable<string>) pathList)
    {
      if (!string.IsNullOrEmpty(path) && !Path.IsPathRooted(path))
      {
        object stageBucket = (object) this.FindStageBucket(path);
        if (stageBucket != null)
        {
          LinkedList<string> linkedList;
          if (!dictionary.TryGetValue(stageBucket, out linkedList))
          {
            linkedList = new LinkedList<string>();
            dictionary.Add(stageBucket, linkedList);
          }
          linkedList.AddLast(path);
        }
        else
          collection.AddLast(path);
      }
    }
    LinkedList<FileBucket> linkedList1 = new LinkedList<FileBucket>();
    foreach (KeyValuePair<object, LinkedList<string>> keyValuePair in dictionary)
      linkedList1.AddLast(new FileBucket((IEnumerable<string>) keyValuePair.Value));
    if (collection.Count > 0)
      linkedList1.AddLast(new FileBucket((IEnumerable<string>) collection));
    return linkedList1;
  }

  public List<string> AsList()
  {
    List<string> stringList = new List<string>(this.GetFileCount((IEnumerable<ReadOnlyFileTreeNode>) this.documentNodes));
    foreach (ReadOnlyFileTreeNode documentNode in this.documentNodes)
    {
      stringList.Add(documentNode.Path);
      stringList.AddRange((IEnumerable<string>) documentNode.Satellites);
    }
    return stringList;
  }

  private int GetFileCount(IEnumerable<ReadOnlyFileTreeNode> documentNodes)
  {
    int fileCount = 0;
    foreach (ReadOnlyFileTreeNode documentNode in documentNodes)
      fileCount += 1 + documentNode.Satellites.Count;
    return fileCount;
  }

  public PathCollection Append(LinkedList<ReadOnlyFileTreeNode> documentNodes)
  {
    PathCollection pathCollection = documentNodes != null ? this.ValidateAppend(documentNodes) : throw new ArgumentNullException(nameof (documentNodes));
    QueueFile.FileGraph fileGraph = this.CreateFileGraph((ICollection<ReadOnlyFileTreeNode>) documentNodes);
    this.RemoveFileCycles(fileGraph);
    this.ComputeImportStages(fileGraph);
    this.Append(fileGraph);
    return pathCollection;
  }

  private PathCollection ValidateAppend(LinkedList<ReadOnlyFileTreeNode> documentNodes)
  {
    PathCollection newFilesTable = new PathCollection(this.GetFileCount((IEnumerable<ReadOnlyFileTreeNode>) documentNodes));
    foreach (ReadOnlyFileTreeNode documentNode in documentNodes)
    {
      this.ValidateAppend(documentNode.Path, newFilesTable);
      foreach (string satellite in (IEnumerable<string>) documentNode.Satellites)
        this.ValidateAppend(satellite, newFilesTable);
    }
    return newFilesTable;
  }

  private void ValidateAppend(string path, PathCollection newFilesTable)
  {
    if (this.FindStageIndex(path) >= 0)
      throw new ArgumentException($"File '{path}' is already in import queue.");
    if (newFilesTable.AddOrGetIndex(path) >= 0)
      throw new ArgumentException($"File '{path}' is duplicated.");
  }

  private QueueFile.FileGraph CreateFileGraph(ICollection<ReadOnlyFileTreeNode> documentNodes)
  {
    QueueFile.FileGraph graph = new QueueFile.FileGraph(documentNodes.Count);
    PathDictionary<QueueFile.FileGraphVertex> pathDictionary = new PathDictionary<QueueFile.FileGraphVertex>(documentNodes.Count);
    foreach (ReadOnlyFileTreeNode documentNode in (IEnumerable<ReadOnlyFileTreeNode>) documentNodes)
    {
      QueueFile.FileGraphVertex v = new QueueFile.FileGraphVertex(documentNode, true);
      pathDictionary.Add(documentNode.Path, v);
      graph.AddVertex(v);
    }
    foreach (ReadOnlyFileTreeNode documentNode in (IEnumerable<ReadOnlyFileTreeNode>) documentNodes)
    {
      QueueFile.FileGraphVertex source = pathDictionary[documentNode.Path];
      foreach (string dependency in (IEnumerable<string>) documentNode.Dependencies)
      {
        QueueFile.FileGraphVertex fileGraphVertex;
        if (!pathDictionary.TryGetValue(dependency, out fileGraphVertex))
        {
          ReadOnlyFileTreeNode document = this.FindDocument(dependency);
          if (document != null)
          {
            fileGraphVertex = new QueueFile.FileGraphVertex(document, false);
            pathDictionary.Add(dependency, fileGraphVertex);
            graph.AddVertex(fileGraphVertex);
          }
        }
        if (fileGraphVertex != null)
          graph.AddEdge(new Edge<QueueFile.FileGraphVertex>(source, fileGraphVertex));
      }
    }
    this.LinkSameNamedFiles(graph);
    return graph;
  }

  private void LinkSameNamedFiles(QueueFile.FileGraph graph)
  {
    PathDictionary<List<QueueFile.FileGraphVertex>> table = new PathDictionary<List<QueueFile.FileGraphVertex>>(graph.VertexCount);
    foreach (QueueFile.FileGraphVertex vertex in graph.Vertices)
    {
      if (vertex.IsNewDocument)
      {
        foreach (ReadOnlyFileTreeNode documentNode in vertex.DocumentNodes)
        {
          this.AddToSameNameTable(vertex, Path.GetFileNameWithoutExtension(documentNode.Path), (Dictionary<string, List<QueueFile.FileGraphVertex>>) table);
          foreach (string satellite in (IEnumerable<string>) documentNode.Satellites)
            this.AddToSameNameTable(vertex, Path.GetFileNameWithoutExtension(satellite), (Dictionary<string, List<QueueFile.FileGraphVertex>>) table);
        }
      }
    }
    foreach (KeyValuePair<string, List<QueueFile.FileGraphVertex>> keyValuePair in (Dictionary<string, List<QueueFile.FileGraphVertex>>) table)
    {
      List<QueueFile.FileGraphVertex> fileGraphVertexList = keyValuePair.Value;
      for (int index1 = 0; index1 < fileGraphVertexList.Count; ++index1)
      {
        for (int index2 = index1 + 1; index2 < fileGraphVertexList.Count; ++index2)
        {
          Edge<QueueFile.FileGraphVertex> edge;
          if (!graph.TryGetEdge(fileGraphVertexList[index1], fileGraphVertexList[index2], out edge))
            graph.AddEdge(new Edge<QueueFile.FileGraphVertex>(fileGraphVertexList[index1], fileGraphVertexList[index2]));
          if (!graph.TryGetEdge(fileGraphVertexList[index2], fileGraphVertexList[index1], out edge))
            graph.AddEdge(new Edge<QueueFile.FileGraphVertex>(fileGraphVertexList[index2], fileGraphVertexList[index1]));
        }
      }
    }
  }

  private void AddToSameNameTable(
    QueueFile.FileGraphVertex vertex,
    string name,
    Dictionary<string, List<QueueFile.FileGraphVertex>> table)
  {
    List<QueueFile.FileGraphVertex> fileGraphVertexList;
    if (!table.TryGetValue(name, out fileGraphVertexList))
    {
      fileGraphVertexList = new List<QueueFile.FileGraphVertex>();
      table.Add(name, fileGraphVertexList);
    }
    if (fileGraphVertexList.Contains(vertex))
      return;
    fileGraphVertexList.Add(vertex);
  }

  private void RemoveFileCycles(QueueFile.FileGraph graph)
  {
    StronglyConnectedComponentsAlgorithm<QueueFile.FileGraphVertex, Edge<QueueFile.FileGraphVertex>> componentsAlgorithm = new StronglyConnectedComponentsAlgorithm<QueueFile.FileGraphVertex, Edge<QueueFile.FileGraphVertex>>((IVertexListGraph<QueueFile.FileGraphVertex, Edge<QueueFile.FileGraphVertex>>) graph);
    componentsAlgorithm.Compute();
    LinkedList<QueueFile.FileGraphVertex> linkedList = new LinkedList<QueueFile.FileGraphVertex>();
    foreach (QueueFile.FileGraphVertex vertex in graph.Vertices)
    {
      QueueFile.FileGraphVertex root = componentsAlgorithm.Roots[vertex];
      if (vertex != root)
      {
        foreach (Edge<QueueFile.FileGraphVertex> inEdge in graph.InEdges(vertex))
        {
          if (componentsAlgorithm.Roots[inEdge.Source] != root)
            graph.AddEdge(new Edge<QueueFile.FileGraphVertex>(inEdge.Source, root));
        }
        foreach (Edge<QueueFile.FileGraphVertex> outEdge in graph.OutEdges(vertex))
        {
          if (componentsAlgorithm.Roots[outEdge.Target] != root)
            graph.AddEdge(new Edge<QueueFile.FileGraphVertex>(root, outEdge.Target));
        }
        root.DocumentNodes.AddRange<ReadOnlyFileTreeNode>((IEnumerable<ReadOnlyFileTreeNode>) vertex.DocumentNodes);
        linkedList.AddLast(vertex);
      }
    }
    foreach (QueueFile.FileGraphVertex v in linkedList)
      graph.RemoveVertex(v);
  }

  private void ComputeImportStages(QueueFile.FileGraph graph)
  {
    DepthFirstSearchAlgorithm<QueueFile.FileGraphVertex, Edge<QueueFile.FileGraphVertex>> firstSearchAlgorithm = new DepthFirstSearchAlgorithm<QueueFile.FileGraphVertex, Edge<QueueFile.FileGraphVertex>>((IVertexListGraph<QueueFile.FileGraphVertex, Edge<QueueFile.FileGraphVertex>>) graph);
    firstSearchAlgorithm.InitializeVertex += (VertexAction<QueueFile.FileGraphVertex>) (vertex => vertex.ImportStageIndex = 0);
    firstSearchAlgorithm.FinishVertex += (VertexAction<QueueFile.FileGraphVertex>) (vertex =>
    {
      if (vertex.IsNewDocument)
      {
        if (graph.IsOutEdgesEmpty(vertex))
        {
          vertex.ImportStageIndex = 0;
        }
        else
        {
          int num = 0;
          foreach (Edge<QueueFile.FileGraphVertex> outEdge in graph.OutEdges(vertex))
          {
            if (outEdge.Target.ImportStageIndex > num)
              num = outEdge.Target.ImportStageIndex;
          }
          vertex.ImportStageIndex = num + 1;
        }
      }
      else
        vertex.ImportStageIndex = this.FindStageIndex(vertex.DocumentNodes.First.Value.Path);
    });
    firstSearchAlgorithm.Compute();
  }

  private void Append(QueueFile.FileGraph graph)
  {
    foreach (QueueFile.FileGraphVertex vertex in graph.Vertices)
    {
      if (vertex.IsNewDocument)
      {
        List<string> stringList = new List<string>(this.GetFileCount((IEnumerable<ReadOnlyFileTreeNode>) vertex.DocumentNodes));
        foreach (ReadOnlyFileTreeNode documentNode in vertex.DocumentNodes)
        {
          stringList.Add(documentNode.Path);
          stringList.AddRange<string>((IEnumerable<string>) documentNode.Satellites);
        }
        while (vertex.ImportStageIndex >= this.importStages.Count)
          this.importStages.Add((IImportStage) new QueueFile.ImportStage());
        this.AppendBucket(vertex.ImportStageIndex, (IList<string>) stringList);
        foreach (ReadOnlyFileTreeNode documentNode in vertex.DocumentNodes)
          this.AppendDocument(documentNode);
      }
    }
  }

  public void FromXml(XmlDocument document)
  {
    if (document == null)
      throw new ArgumentNullException("doc");
    this.Clear();
    this.DecodeImportStates(document);
    this.DecodeDocumentNodes(document);
    this.RemoveOrphanedImportBuckets();
  }

  private void DecodeImportStates(XmlDocument doc)
  {
    XmlNodeList xmlNodeList1 = doc.DocumentElement.SelectNodes("ImportStages/Stage");
    while (this.importStages.Count < xmlNodeList1.Count)
      this.importStages.Add((IImportStage) new QueueFile.ImportStage());
    for (int index = 0; index < xmlNodeList1.Count; ++index)
    {
      XmlNodeList xmlNodeList2 = xmlNodeList1[index].SelectNodes("Bucket");
      LinkedList<List<string>> linkedList = new LinkedList<List<string>>();
      foreach (XmlNode rootXmlNode in xmlNodeList2)
      {
        List<string> bucket = QueueFile.DecodeFiles(rootXmlNode);
        foreach (string key in bucket)
        {
          if (this.pathToStageIndex.ContainsKey(key))
            throw new ArgumentException($"Bad xml document. Import stages contain a duplicated file '{key}'.");
        }
        this.AppendBucket(index, (IList<string>) bucket);
      }
    }
  }

  private void DecodeDocumentNodes(XmlDocument doc)
  {
    foreach (XmlNode selectNode in doc.DocumentElement.SelectNodes("FileGraph/Node[File/@path]"))
    {
      string str = QueueFile.ReadXmlAttribute((XmlNode) selectNode["File"], "path");
      if (this.pathToDocumentNode.ContainsKey(str))
        throw new ArgumentException($"Bad xml document. A file graph contains a duplicated file '{str}'.");
      ICollection<string> strings1;
      if (!this.pathToStageBucket.TryGetValue(str, out strings1))
        throw new ArgumentException($"Bad xml document. No import bucket found for a file '{str}'.");
      List<string> satellites = QueueFile.DecodeFiles((XmlNode) selectNode["Satellites"]);
      List<string> dependencies = QueueFile.DecodeFiles((XmlNode) selectNode["Dependencies"]);
      foreach (string key in satellites)
      {
        ICollection<string> strings2;
        if (!this.pathToStageBucket.TryGetValue(key, out strings2) || strings1 != strings2)
          throw new ArgumentException($"Files '{str}' and '{key}' must be in same import bucket.");
      }
      this.AppendDocument(new ReadOnlyFileTreeNode(str, satellites, dependencies));
    }
  }

  private static List<string> DecodeFiles(XmlNode rootXmlNode)
  {
    if (rootXmlNode == null)
      return new List<string>();
    XmlNodeList xmlNodeList = rootXmlNode.SelectNodes("File[@path]");
    List<string> stringList = new List<string>(xmlNodeList.Count);
    foreach (XmlNode node in xmlNodeList)
      stringList.Add(QueueFile.ReadXmlAttribute(node, "path"));
    return stringList;
  }

  private static string ReadXmlAttribute(XmlNode node, string attrName)
  {
    return node.Attributes[attrName].Value.Trim();
  }

  private void RemoveOrphanedImportBuckets()
  {
    foreach (IImportStage importStage in this.importStages)
    {
      foreach (ICollectionWrapper<string> bucket in (IEnumerable<ICollection<string>>) importStage.Buckets)
      {
        ICollection<string> collection = bucket.Unwrap();
        foreach (string allAsLinked in CollectionUtils.FindAllAsLinkedList<string>((IEnumerable<string>) collection, (Predicate<string>) (bucketFile => !this.pathToDocumentNode.ContainsKey(bucketFile))))
          collection.Remove(allAsLinked);
      }
    }
  }

  public XmlDocument ToXml()
  {
    XmlDocument xml = new XmlDocument();
    xml.AppendChild((XmlNode) xml.CreateXmlDeclaration("1.0", "utf-8", (string) null));
    xml.AppendChild((XmlNode) xml.CreateElement("Document"));
    XmlNode rootNode1 = xml.DocumentElement.AppendChild((XmlNode) xml.CreateElement("ImportStages"));
    foreach (IImportStage importStage in this.importStages)
      QueueFile.ToXml(rootNode1, importStage);
    XmlNode rootNode2 = xml.DocumentElement.AppendChild((XmlNode) xml.CreateElement("FileGraph"));
    foreach (ReadOnlyFileTreeNode documentNode in this.documentNodes)
      QueueFile.ToXml(rootNode2, documentNode);
    return xml;
  }

  private static void ToXml(XmlNode rootNode, IImportStage stage)
  {
    XmlDocument ownerDocument = rootNode.OwnerDocument;
    XmlElement element1 = ownerDocument.CreateElement("Stage");
    foreach (ICollection<string> bucket in (IEnumerable<ICollection<string>>) stage.Buckets)
    {
      if (bucket.Count > 0)
      {
        XmlElement element2 = ownerDocument.CreateElement("Bucket");
        foreach (string path in (IEnumerable<string>) bucket)
          element2.AppendChild(QueueFile.CreateFileElement(ownerDocument, path));
        element1.AppendChild((XmlNode) element2);
      }
    }
    rootNode.AppendChild((XmlNode) element1);
  }

  private static void ToXml(XmlNode rootNode, ReadOnlyFileTreeNode documentNode)
  {
    XmlDocument ownerDocument = rootNode.OwnerDocument;
    XmlElement element1 = ownerDocument.CreateElement("Node");
    element1.AppendChild(QueueFile.CreateFileElement(ownerDocument, documentNode.Path));
    if (documentNode.Satellites.Count > 0)
    {
      XmlElement element2 = ownerDocument.CreateElement("Satellites");
      foreach (string satellite in (IEnumerable<string>) documentNode.Satellites)
        element2.AppendChild(QueueFile.CreateFileElement(ownerDocument, satellite));
      element1.AppendChild((XmlNode) element2);
    }
    if (documentNode.Dependencies.Count > 0)
    {
      XmlElement element3 = ownerDocument.CreateElement("Dependencies");
      foreach (string dependency in (IEnumerable<string>) documentNode.Dependencies)
        element3.AppendChild(QueueFile.CreateFileElement(ownerDocument, dependency));
      element1.AppendChild((XmlNode) element3);
    }
    rootNode.AppendChild((XmlNode) element1);
  }

  private static XmlNode CreateFileElement(XmlDocument doc, string path)
  {
    XmlElement element = doc.CreateElement("File");
    element.Attributes.Append(QueueFile.CreateXmlAttribute(doc, nameof (path), path));
    return (XmlNode) element;
  }

  private static XmlAttribute CreateXmlAttribute(XmlDocument doc, string name, string value)
  {
    XmlAttribute attribute = doc.CreateAttribute(name);
    attribute.Value = value;
    return attribute;
  }

  private void AppendDocument(ReadOnlyFileTreeNode documentNode)
  {
    this.pathToDocumentNode.Add(documentNode.Path, documentNode);
    foreach (string satellite in (IEnumerable<string>) documentNode.Satellites)
      this.pathToDocumentNode.Add(satellite, documentNode);
    this.documentNodes.AddLast(documentNode);
  }

  private void AppendBucket(int stageIndex, IList<string> bucket)
  {
    foreach (string key in (IEnumerable<string>) bucket)
    {
      this.pathToStageIndex.Add(key, stageIndex);
      this.pathToStageBucket.Add(key, (ICollection<string>) bucket);
    }
    ((QueueFile.ImportStage) this.importStages[stageIndex]).Buckets.Add((ICollection<string>) new ReadOnlyListWrapper<string>(bucket));
  }

  private sealed class FileGraph(int capacity) : 
    BidirectionalGraph<QueueFile.FileGraphVertex, Edge<QueueFile.FileGraphVertex>>(true, capacity)
  {
  }

  private sealed class FileGraphVertex
  {
    private readonly LinkedList<ReadOnlyFileTreeNode> documentNodes;
    private int importStageIndex;
    private readonly bool isNewDocument;

    public FileGraphVertex(ReadOnlyFileTreeNode documentNode, bool isNewDocument)
    {
      this.documentNodes = new LinkedList<ReadOnlyFileTreeNode>();
      this.documentNodes.AddLast(documentNode);
      this.isNewDocument = isNewDocument;
    }

    public LinkedList<ReadOnlyFileTreeNode> DocumentNodes => this.documentNodes;

    public int ImportStageIndex
    {
      get => this.importStageIndex;
      set => this.importStageIndex = value;
    }

    public bool IsNewDocument => this.isNewDocument;
  }

  private sealed class ImportStage : IImportStage
  {
    private readonly List<ICollection<string>> buckets;
    private readonly ReadOnlyCollection<ICollection<string>> bucketsWrapper;

    public ImportStage()
    {
      this.buckets = new List<ICollection<string>>(2048 /*0x0800*/);
      this.bucketsWrapper = new ReadOnlyCollection<ICollection<string>>((IList<ICollection<string>>) this.buckets);
    }

    public List<ICollection<string>> Buckets => this.buckets;

    ICollection<ICollection<string>> IImportStage.Buckets
    {
      get => (ICollection<ICollection<string>>) this.bucketsWrapper;
    }
  }
}
