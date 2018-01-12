// <copyright file="Media.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Media
    {
        public Media()
        {
            this.Created = DateTime.Now;
            this.LastUpdated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public byte[] Stream { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileType { get; set; }

        public string MetaData { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
