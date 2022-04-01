using LitAnime.Domain;

namespace LitAnime.Services.SearchStringToTags
{
    public class StringTransformer
    {
        public static List<string> TransformToTags(string searchString)
        {
            

            return searchString.Trim().Split(" ").ToList();
        }

        public static string PathToURL(string path)
        {
            
            return string.Format(path.ToLower().Replace(path, "").Replace(@"\", "/"));
        }

    }
}
