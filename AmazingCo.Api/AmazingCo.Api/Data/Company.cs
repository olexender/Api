using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmazingCo.Api.Data
{
    public class Company : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //root node if null
        public int? ParentId { get; set; }
        public int Height { get; set; }
    }
}
