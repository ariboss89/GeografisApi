using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeografisApi.Data;
using GeografisApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GeografisApi.Controllers
{
    [Route("api/KategoriApi")]
    [ApiController]
    public class KategoriApiController : Controller
    {
        private readonly GeografisContext _db;

        public KategoriApiController(GeografisContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Kategori> GetAllKategori()
        {
            var kategoriData = _db.Kategoris.ToList();

            var srlJson = JsonConvert.SerializeObject(kategoriData);

            return JsonConvert.DeserializeObject<List<Kategori>>(srlJson);
        }

        [HttpGet("{id:int}", Name = "GetKategori")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200)]
        public ActionResult<Kategori> GetKategori(int id)
        {
            if (id == 0)
            {
                //  _logger.Log("Get villa error with Id " + id, "error");
                return BadRequest();
            }

            var ktg = _db.Kategoris.FirstOrDefault(x => x.Id == id);

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
        public ActionResult<Kategori> CreateKategori([FromBody] KategoriCreateDTO ktgDTO)
        {
            if (_db.Kategoris.FirstOrDefault(x => x.kategori.ToLower() == ktgDTO.kategori.ToLower()) != null)
            {

                ModelState.AddModelError("CustomError", "Kategori Already Exist!");

                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ktgDTO == null)
            {
                return BadRequest();
            }

            Kategori model = new()
            {
                kategori = ktgDTO.kategori
            };

            _db.Kategoris.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetKategori", new { id = model.Id }, model);

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteKategori")]
        public async Task<IActionResult> DeleteKategori(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var data = await _db.Kategoris.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            _db.Kategoris.Remove(data);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateKategori")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateKategori(int id, [FromBody] KategoriUpdateDTO ktgDTO)
        {
            if (ktgDTO == null || id != ktgDTO.Id)
            {
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Sqft = villaDTO.Sqft;
            //villa.Occupancy = villaDTO.Occupancy;

            Kategori model = new()
            {
                kategori = ktgDTO.kategori,
                Id = ktgDTO.Id
            };

            _db.Kategoris.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        //[HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    var villa = _db.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);

        //    VillaDTO villaDTO = new()
        //    {
        //        Amenity = villa.Amenity,
        //        Details = villa.Details,
        //        Id = villa.Id,
        //        ImageUrl = villa.ImageUrl,
        //        Name = villa.Name,
        //        Occupancy = villa.Occupancy,
        //        Rate = villa.Rate,
        //        Sqft = villa.Sqft
        //    };

        //    if (villa == null)
        //    {
        //        return BadRequest();
        //    }

        //    patchDTO.ApplyTo(villaDTO, ModelState);

        //    Villa model = new()
        //    {
        //        Amenity = villaDTO.Amenity,
        //        Details = villaDTO.Details,
        //        Id = villaDTO.Id,
        //        ImageUrl = villaDTO.ImageUrl,
        //        Name = villaDTO.Name,
        //        Occupancy = villaDTO.Occupancy,
        //        Rate = villaDTO.Rate,
        //        Sqft = villaDTO.Sqft
        //    };

        //    _db.Villas.Update(model);
        //    _db.SaveChanges();

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return NoContent();
        //}
    }
}