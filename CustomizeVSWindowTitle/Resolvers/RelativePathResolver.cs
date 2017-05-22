using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ErwinMayerLabs.Lib;

namespace ErwinMayerLabs.RenameVSWindowTitle.Resolvers {
    public class RelativePathResolver : TagResolver {
        public string TagName { get; } = "relativePath";

        public RelativePathResolver() : base(tagNames: new[] { "relativePath" }) { }
        
        public override bool TryResolve(string tag, AvailableInfo info, out string s) {
            s = null;
            //var relativePath = Globals.GetActiveDocumentNameOrEmpty(activeDocument, activeWindow) ?? string.Empty;

            if (info.ActiveDocument != null)
            {
                if (!string.IsNullOrEmpty(info.Solution?.FullName))
                {
                    Uri.TryCreate(info.ActiveDocument.FullName, UriKind.Absolute, out var docUri);
                    Uri.TryCreate(info.Solution.FullName, UriKind.Absolute, out var solutionUri);

                    if (docUri != null && solutionUri != null)
                        s = solutionUri.MakeRelativeUri(docUri).ToString().Replace('/', Path.DirectorySeparatorChar);
                }
                else
                {
                    s = info.ActiveDocument.FullName;
                }

                return true;
            }
            else
            {
                s = info.ActiveWindow?.Caption;
            }

            return s != null;
        }
    }
}