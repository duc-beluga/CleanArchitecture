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
        Task<List<Blog>> GetAllSync();
        Task<Blog> GetByIdSync(int id);
        Task<Blog> CreateAsync(Blog blog);
        Task<int> UpdateAsync(int id, Blog blog);

        Task<int> DeleteAsync(int id);
    }
}
