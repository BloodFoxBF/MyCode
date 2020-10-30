using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя выступающего
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Биография выступающего
        /// </summary>
        [StringLength(4000)]
        public string Bio { get; set; }
        /// <summary>
        /// Web сайт
        /// </summary>
        [StringLength(1000)]
        public virtual string WebSite { get; set; }
    }
}
