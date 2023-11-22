using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeografisApi.Models.Dto
{
	public class UmkmUpdateDTO
	{
        public int Id { get; set; }
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

