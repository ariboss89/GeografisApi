using System;
using System.Linq;
using System.Linq.Expressions;
using GeografisApi.Data;
using GeografisApi.Models;
using GeografisApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GeografisApi.Repository
{
    public class KategoriRepository : IKategori
    {
        private readonly GeografisContext _db;

        public KategoriRepository(GeografisContext db)
        {
            _db = db;
        }

        public async Task Create(Kategori entity)
        {
           await _db.Kategoris.AddAsync(entity);
            await Save();
        }

        public async Task<Kategori> Get(Expression<Func<Kategori,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Kategori> query = _db.Kategoris;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Kategori>> GetAll(Expression<Func<Kategori,bool>> filter = null)
        {
            IQueryable<Kategori> query = _db.Kategoris;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task Remove(Kategori entity)
        {
            _db.Kategoris.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}

