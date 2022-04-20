using AutoMapper;
using BookAMech.Contracts.V1.Requests;
using BookAMech.Contracts.V1.Responses;
using BookAMech.Domain;

namespace BookAMech.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            //The application layer and presentation layer operates with data transfer objects.
            //You're showing EF Core there (DbSet) which is something that is part of the infrastructure layer (in this case, the data persistence concern of that layer).

            //Finds naming properties and matches them. Then maps the domain object to responseobject
            CreateMap<Reservation, ReservationResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.StreetAddress))
                .ForMember(dest => dest.StreetNumber, opt => opt.MapFrom(src => src.StreetNumber))
                .ForMember(dest => dest.Phonenumber, opt => opt.MapFrom(src => src.Phonenumber))
                .ForMember(dest => dest.startDate, opt => opt.MapFrom(src => src.startDate))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Reservation, CreateReservationRequest>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.StreetAddress))
                .ForMember(dest => dest.StreetNumber, opt => opt.MapFrom(src => src.StreetNumber))
                .ForMember(dest => dest.Phonenumber, opt => opt.MapFrom(src => src.Phonenumber))
                .ForMember(dest => dest.startDate, opt => opt.MapFrom(src => src.startDate));

            CreateMap<Reservation, UpdateReservationRequest>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.StreetAddress))
                .ForMember(dest => dest.StreetNumber, opt => opt.MapFrom(src => src.StreetNumber))
                .ForMember(dest => dest.Phonenumber, opt => opt.MapFrom(src => src.Phonenumber))
                .ForMember(dest => dest.startDate, opt => opt.MapFrom(src => src.startDate));

        }
    }
}
