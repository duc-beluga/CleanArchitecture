using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interface
{
    public interface IBlogRepository
    {
        Task<List<BlogEntity>> GetAllSync();
        Task<BlogEntity?> GetByIdSync(int id);
        Task<BlogEntity> CreateAsync(BlogEntity blog);
        Task<BlogEntity?> UpdateAsync(int id, BlogEntity blog);

        Task<int> DeleteAsync(int id);
    }
}
