#region Using

using System.IO;
using LightPaper.Core.Services;
using PuppyFramework.Helpers;

#endregion

namespace LightPaper.Plugins.CommunityThemeManager
{
    internal class LocalThemeDecorator : DefaultPreviewDecorator
    {
        #region Fields 

        private readonly string _name;
        private string _cssContents;
        private string _cssPath;

        #endregion

        #region Properties 

        public override string CssContents
        {
            get { return _cssContents; }
        }

        public string CssPath
        {
            get { return _cssPath; }
            private set
            {
                _cssPath = value;
                _cssContents = File.Exists(CssPath) ? File.ReadAllText(CssPath) : string.Empty;
            }
        }

        public override string Name
        {
            get { return _name; }
        }

        public override string Guid
        {
            get { return CssPath; }
        }

        #endregion

        #region Constructors 

        public LocalThemeDecorator(string cssPath)
        {
            cssPath.EnsureStringNotNullOrWhitespace("CssPath", "CSS path cannot be empty or null");
            CssPath = cssPath;
            _name = Path.GetFileNameWithoutExtension(CssPath);
        }

        #endregion

        #region Methods 

        protected bool Equals(LocalThemeDecorator other)
        {
            return string.Equals(CssPath, other.CssPath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((LocalThemeDecorator) obj);
        }

        public override int GetHashCode()
        {
            return CssPath.GetHashCode();
        }

        #endregion
    }
}