using Application.Abstraction;
using Application.Interfaces;
using Domein.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrustructure.Services
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(IApplicationDbContext Db) : base(Db)
        {
            Console.WriteLine( " a new instance is taken. ");
        }

    }
}
