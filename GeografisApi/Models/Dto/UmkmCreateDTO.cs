using System;
using System.ComponentModel.DataAnnotations;

namespace GeografisApi.Models.Dto
{
	public class UmkmCreateDTO
	{
        [Required]
        public string nama { get; set; }
        [Required]
        public string alamat { get; set; }
        [Required]
        public double lattitude { get; set; }
        [Required]
        public double longitude { get; set; }
        public string deskripsi { get; set; }
        [Required]
        public string kategori { get; set; }
        public byte[] gambar1 { get; set; }
        public byte[] gambar2 { get; set; }
        public byte[] gambar3 { get; set; }
    }
}

