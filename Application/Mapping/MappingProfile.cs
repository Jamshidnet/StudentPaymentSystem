using Application.Abstraction;
using Application.DTO.AUserDtos;
using Domein.AccessEntities;
using AutoMapper;
using Domein.Models;
using Application.DTO.CourseDtos;
using Application.DTO.InvoiceDtos;
using Application.DTO.PaymentDtos;
using Application.DTO.PermissionDtos;
using Application.DTO.RoleDtos;
using Application.DTO.StudentDtos;
using Application.DTO.TransactionDtos;

namespace Application.Mapping;

public  class MappingProfile :Profile
{
    private readonly IApplicationDbContext? _context;
    public MappingProfile()
    {
        StudentMapping();
        CreateMap<AUser, CreateAUserDto>().ReverseMap()
            .ForMember(x => x.Roles, t => t.Ignore());
        CreateMap<AUser, UpdateAUserDto>().ReverseMap();
        CreateMap<AUser, GetAUserDto>().ReverseMap();
        
        CreateMap<Course,CreateCourseDto>().ReverseMap();
       CreateMap<Course,UpdateCourseDto>().ReverseMap();
        CreateMap<Course, GetCourseDto>();
       
        CreateMap<Invoice, CreateInvoiceDto>().ReverseMap();
       CreateMap<Invoice, UpdateInvoiceDto>().ReverseMap();
        CreateMap<Invoice,GetInvoiceDto>();
       
        CreateMap<Payment, CreatePaymentDTO>().ReverseMap();
       CreateMap<Payment, UpdatePaymentDto>().ReverseMap();
        CreateMap <Payment,  GetPaymentDto>();

        CreateMap<Invoice, CreateInvoiceDto>().ReverseMap();
        CreateMap<Invoice, UpdateInvoiceDto>().ReverseMap();
        CreateMap<Invoice, GetInvoiceDto>();

        CreateMap<Permission, GetPermissionDto>().ReverseMap();
        CreateMap<Permission, CreatePermissionDto>().ReverseMap();

          CreateMap<Role, CreateRoleDto>().ReverseMap()
            .ForMember(x => x.Permissions, t => t.Ignore())
            .ForMember(x => x.Users, t => t.Ignore());

        CreateMap<Role, UpdateRoleDto>().ReverseMap();
        CreateMap<Role, GetRoleDto>().ReverseMap()
            .ForMember(x => x.Users, t => t.Ignore());

        CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
        CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
        CreateMap<Transaction, GetTransactionDto>();

        
            //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID));
        

        CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
       CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();

    }

    public void StudentMapping()
    {
        CreateMap< Student, CreateStudentDto>().ReverseMap();
        CreateMap<Student, GetStudentDto>();
        CreateMap<Student, UpdateStudentDto>().ReverseMap();

    }

    public MappingProfile(IApplicationDbContext context)
    {
        _context = context;
    }
}
