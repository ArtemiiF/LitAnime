using LitAnime.Domain;
using System.ComponentModel.DataAnnotations;

namespace LitAnime.Models
{
    public class UploadPictureViewModel
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile? FormFile { get; set; }

        [Required]
        [Display(Name = "PicName")]
        public string PicName { get; set; }

        [Required]
        [Display(Name = "Tags"+"(nya, kawaii, desu)")]
        public string Tags { get; set; }
    }
}
