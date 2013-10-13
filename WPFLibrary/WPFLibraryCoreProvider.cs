namespace WPFLibrary
{
    public static class WPFLibraryCoreProvider
    {
        private static IWPFLibraryCore _current;

        public static IWPFLibraryCore Current
        {
            get
            {
                return _current;
            }
        }

        public static void SetWPFLibraryCore(IWPFLibraryCore wpfLibraryCore)
        {
            _current = wpfLibraryCore;
        }
    }
}
