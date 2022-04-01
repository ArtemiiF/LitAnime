using System.ComponentModel.DataAnnotations;

namespace LitAnime.Models
{
    public class HomeViewModel
    {
        
        [Display(Name = "SearchString")]
        public string SearchString { get; set; } = string.Empty;
    }
}
