using Application.Abstraction;
using Application.Interfaces;
using Domein.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrustructure.Services
{
    public class TransactionRepository : Repository<Transaction>,  ITransactionRepository
    {
        public TransactionRepository(IApplicationDbContext Db) : base(Db)
        {
        }
    }
}
