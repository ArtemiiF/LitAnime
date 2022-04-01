using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using LitAnime.Domain;

namespace LitAnime.Models
{
    public class SearchViewModel
    {
        [Required]
        [Display(Name = "SearchString")]
        public string q { get; set; } = string.Empty;
        public List<Picture> Pictures { get; set; }
    }
}
