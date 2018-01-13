// <copyright file="Post.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Title = string.Empty;
            this.Teaser = string.Empty;
            this.Subtitle = string.Empty;
            this.Content = string.Empty;
            this.Created = DateTime.Now;
            this.LastUpdated = DateTime.Now;
            this.IsPublished = false;
        }

        [Key]
        public int Id { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Teaser { get; set; }

        public string Content { get; set; }

        public int Thumbnail { get; set; }

        public bool IsPublished { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
