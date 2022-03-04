using LitAnime.Services;

namespace LitAnime.Domain
{
    public class Picture
    {
        public Picture()
        {

        }
        public Picture(IFormFile file, string name, User user)
        {
            Name = name;
            Up_date = DateTime.Now.ToUniversalTime();
            Plus = 1;
            Minus = 1;
            U_id = user.Id;
            string fileName = name + "(" + DateTime.Now.ToUniversalTime().ToString("yyyymmddMMss") + ")"+Path.GetExtension(file.FileName);
            Link = Config.ImagePath + fileName;
            using (var fileStream = new FileStream(Link,FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string U_id { get; set; }
        public DateTime Up_date { get; set; }
        public string? Link { get; set; }
        public int Plus { get; set; } = 1;
        public int Minus { get; set; } = 1;
    }
}
