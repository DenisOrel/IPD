using Intermech.ComparisonPlugins.PDFComparison.Common;


namespace Intermech.ComparisonPlugins.PDFComparison
{
    public class CompareAuthenticFilesProvider : ComparisonProvider
    {
      public override FileDescription SelectFirstComparedFile()
      {
        return ClientUtils.FindAuthenticObjectFile(this._firstComparedVersion);
      }

      public override FileDescription SelectSecondComparedFile()
      {
        return ClientUtils.FindAuthenticObjectFile(this._secondComparedVersion);
      }
    }
}
