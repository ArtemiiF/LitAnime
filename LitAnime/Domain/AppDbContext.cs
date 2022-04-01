using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LitAnime.Domain
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
            
        }
        public DbSet<Picture> Pictures { get; set; } = null!;
        public DbSet<Pic_tag> Pic_Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "1",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "2",
                Name = "moderator",
                NormalizedName = "MODERATOR"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "3",
                Name = "user",
                NormalizedName = "USER"
            });

            builder.Entity<User>().HasData(new User
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "my@email.com",
                NormalizedEmail = "MY@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "admin"),
                SecurityStamp = string.Empty,
                Minus = 1,
                Plus = 1
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            });
        }

        public void UploadPicture(IFormFile picture, string picName, User user)
        {
            Picture tempPic = new Picture(picture, picName, user);
            this.Pictures.Add(tempPic);
            this.SaveChanges();
        }

        public void UploadTags(string tags, string path)
        {
            Picture picture = this.Pictures.FirstOrDefault(p => p.Link == path);
            if (picture != null)
            {
                List<string> currTags = tags.ToLower().Replace(" ", "").Split(',').ToList();

                foreach (var item in currTags)
                {
                    Pic_tag pic_Tag = new Pic_tag(item, picture.Id);
                    this.Pic_Tags.Add(pic_Tag);
                }

                this.SaveChanges();
            }
        }

        public List<Picture> GetPicturesByTags(List<string> tags)
        {

            List<Picture> pictures = new List<Picture>();

            if (Pictures.Count() == 0)
            {
                return pictures;
            }


            List<int> pic_id = new List<int>();

            foreach (var item in tags)
            {
                var tempPic_Tags = (from pt in Pic_Tags
                                    where pt.Tag == item
                                    group pt by pt.Pic_id into picId
                                    select picId.Key).ToList();

                pic_id.AddRange(tempPic_Tags);
            }

            foreach (var item in pic_id)
            {
                var tempPictures = from p in Pictures
                                   where p.Id == item
                                   select p;

                pictures.AddRange(tempPictures);
            }

            return pictures;
        }
    }
}
