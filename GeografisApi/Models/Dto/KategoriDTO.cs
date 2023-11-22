using System;
using System.ComponentModel.DataAnnotations;

namespace GeografisApi.Models.Dto
{
	public class KategoriDTO
	{
        [Required]
        public int Id { get; set; }
        [Required]
        public string kategori { get; set; }
    }
}

