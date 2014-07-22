#region Using

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using LightPaper.Infrastructure.Contracts;
using Microsoft.Practices.Prism.Logging;
using PuppyFramework.Helpers;
using PuppyFramework.Interfaces;

#endregion

namespace LightPaper.Plugins.CommunityThemeManager
{
    [Export(typeof (IPreviewDecoratorProvider))]
    [Export(typeof (CommunityPreviewDecoratorProvider))]
    public class CommunityPreviewDecoratorProvider : IPreviewDecoratorProvider
    {
        #region Properties

        public IEnumerable<IPreviewDecorator> PreviewDecorators { get; private set; }

        #endregion

        [ImportingConstructor]
        public CommunityPreviewDecoratorProvider(ILogger logger)
        {
            PreviewDecorators = DecoratorsWithExtensions(MagicStrings.CSS_EXTENSION);
            logger.Log("Loaded {type}", Category.Info, MagicStrings.PLUGIN_CATEGORY, GetType().Name);
        }

        private static IEnumerable<IPreviewDecorator> DecoratorsWithExtensions(string extension)
        {
            var themesDir = Path.Combine("Plugins".CombineWithLocalAppDataPath(), MagicStrings.PLUGIN_NAME, MagicStrings.PathNames.THEMES_FOLDER_NAME);
            return Directory.Exists(themesDir)
                ? Directory.EnumerateFiles(themesDir, string.Format("*.{0}", extension), SearchOption.AllDirectories)
                    .Select(path => new LocalThemeDecorator(path) {GroupName = MagicStrings.DECORATORS_GROUP_NAME})
                : new List<LocalThemeDecorator>();
        }
    }
}