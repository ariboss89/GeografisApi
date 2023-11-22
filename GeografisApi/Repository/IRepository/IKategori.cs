using System;
using System.Linq.Expressions;
using GeografisApi.Models;

namespace GeografisApi.Repository.IRepository
{
	public interface IKategori
	{
        Task<List<Kategori>> GetAll(Expression<Func<Kategori, bool>> filter = null);
        Task<Kategori> Get(Expression<Func<Kategori,bool>> filter = null, bool tracked=true);

        Task Create(Kategori entity);
        Task Remove(Kategori entity);
        Task Save();
    }
}

