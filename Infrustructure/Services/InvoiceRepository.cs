using Application.Abstraction;
using Application.Interfaces;
using Domein.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrustructure.Services
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IApplicationDbContext Db) : base(Db)
        {
        }
    }
}
