namespace Penna.Entities.DTOs
{
    public static class Toolbar
    {
        public static string Title { get; set; }
        public static string SubTitle { get; set; }
        public static string[] Breadcrumbs { get; set; }
        public static string[] Urls { get; set; }
        public static void Init()
        {
            Title = "";
            SubTitle = "";
            Breadcrumbs = new[] { "" };
            Urls = new[] { "#" };
        }
        public static void Clear()
        {
            Title = string.Empty;
            SubTitle = string.Empty;
            Breadcrumbs = null;
            Urls = null;
        }
        public static void Set(string title, string subTitle, string[] breadcrumbs, string[] urls)
        {
            Title = title;
            SubTitle = subTitle;
            Breadcrumbs = breadcrumbs;
            Urls = urls;
        }
    }
}
