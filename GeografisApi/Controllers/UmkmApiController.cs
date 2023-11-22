using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeografisApi.Data;
using GeografisApi.Models;
using GeografisApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GeografisApi.Controllers
{
    [Route("api/UmkmApi")]
    [ApiController]
    public class UmkmApiController : ControllerBase
    {
        private readonly GeografisContext _db;

        public UmkmApiController(GeografisContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Umkm> GetAllUmkm()
        {
            var kategoriData = _db.Kategoris.ToList();

            var srlJson = JsonConvert.SerializeObject(kategoriData);

            return JsonConvert.DeserializeObject<List<Umkm>>(srlJson);
        }

        [HttpGet("{id:int}", Name = "GetUmkm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200)]
        public ActionResult<Kategori> GetUmkm(int id)
        {
            if (id == 0)
            {
                //  _logger.Log("Get villa error with Id " + id, "error");
                return BadRequest();
            }

            var ktg = _db.Umkms.FirstOrDefault(x => x.Id == id);

            if (ktg == null)
            {
                return NotFound();
            }

            return Ok(ktg);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Kategori> CreateUmkm([FromBody] UmkmCreateDTO umkmDTO)
        {
            if (_db.Umkms.FirstOrDefault(x => x.longitude == umkmDTO.longitude && x.lattitude == umkmDTO.lattitude) != null)
            {

                ModelState.AddModelError("CustomError", "UMKM Already Exist!");

                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (umkmDTO == null)
            {
                return BadRequest();
            }

            Umkm model = new()
            {
                nama = umkmDTO.nama,
                alamat = umkmDTO.alamat,
                deskripsi = umkmDTO.deskripsi,
                gambar1 = umkmDTO.gambar1,
                gambar2 = umkmDTO.gambar2,
                gambar3 = umkmDTO.gambar3,
                kategori = umkmDTO.kategori,
                lattitude = umkmDTO.lattitude,
                longitude = umkmDTO.longitude
            };

            _db.Umkms.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetUmkm", new { id = model.Id }, model);

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteUmkm")]
        public async Task<IActionResult> DeleteUmkm(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var data = await _db.Umkms.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            _db.Umkms.Remove(data);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateUmkm")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUmkm(int id, [FromBody] UmkmUpdateDTO umkmDTO)
        {
            if (umkmDTO == null || id != umkmDTO.Id)
            {
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Sqft = villaDTO.Sqft;
            //villa.Occupancy = villaDTO.Occupancy;

            Umkm model = new()
            {
                 Id = umkmDTO.Id,
                 alamat =umkmDTO.alamat,
                 deskripsi = umkmDTO.deskripsi,
                 kategori = umkmDTO.kategori,
                 lattitude = umkmDTO.lattitude,
                 longitude = umkmDTO.longitude,
                 nama = umkmDTO.nama
            };

            _db.Umkms.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
