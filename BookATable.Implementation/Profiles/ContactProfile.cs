using AutoMapper;
using BookATable.Application.DTO;
using BookATable.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ResponseContactDTO>()
               .ForMember(x => x.Id, y => y.MapFrom(u => u.Id))
               .ForMember(x => x.Email, y => y.MapFrom(u => u.Email))
               .ForMember(x => x.FirstName, y => y.MapFrom(u => u.FirstName))
               .ForMember(x => x.LastName, y => y.MapFrom(u => u.LastName))
               .ForMember(x => x.Subject, y => y.MapFrom(u => u.Subject))
               .ForMember(x => x.Message, y => y.MapFrom(u => u.Message));
        }
    }
}
