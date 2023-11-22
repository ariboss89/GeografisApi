using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeografisApi.Models
{
	public class KategoriUpdateDTO
	{
        [Required]
        public int Id { get; set; }
        public string kategori { get; set; }
    }
}

