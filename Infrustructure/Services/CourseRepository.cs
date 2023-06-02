using Application.Abstraction;
using Application.Interfaces;
using Domein.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrustructure.Services
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(IApplicationDbContext Db) : base(Db)
        {
        }
    }
}
