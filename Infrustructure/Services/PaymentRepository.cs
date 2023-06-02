using Application.Abstraction;
using Application.Interfaces;
using Domein.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrustructure.Services
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IApplicationDbContext Db) : base(Db)
        {
        }
    }
}
