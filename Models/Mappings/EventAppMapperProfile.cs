using AutoMapper;

namespace EventApp.Models.Mappings {
    public class EventAppMapperProfile : Profile {
        public EventAppMapperProfile(){
            CreateMap<EventCreateDto, Event>()
                .ForMember(dst => dst.PlaceIdentity, opt => opt.MapFrom(src => src.PlaceId));
            CreateMap<EventUpdateDto, Event>();
            CreateMap<Event, EventReadDto>()
                .ForMember(dst => dst.PlaceName, opt => opt.MapFrom(src => src.Place.Name))
                .ForMember(dst => dst.PlaceAddress, opt => opt.MapFrom(src => src.Place.Address));

            CreateMap<PlaceCreateDto, Place>();
            CreateMap<PlaceUpdateDto, Place>();
            CreateMap<Place, PlaceReadDto>();

            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
            CreateMap<Person, PersonReadDto>();

            CreateMap<InvitationCreateDto, Invitation>();
            CreateMap<Invitation, InvitationReadDto>()
                .ForMember(dst => dst.PersonName, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(dst => dst.InvitationStatus, opt => opt.MapFrom(src => src.Status));
        }
    }
}