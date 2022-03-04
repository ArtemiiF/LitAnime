using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LitAnime.Domain
{
    public class Pic_tag
    {
        public Pic_tag()
        {

        }

        public Pic_tag(string tag, int pic_id)
        {
            Tag = tag;
            Pic_id = pic_id;
        }

        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Pic_id { get; set; }
        public string? Tag { get; set; }
    }
}
