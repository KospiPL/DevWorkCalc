using AutoMapper;
using D.W.C.Lib.D.W.C.Models;
using DevWorkCalc.D.W.C.Models;

namespace D.W.C.API.D.W.C.Service
{
    public class WorkItemProfile : Profile
    {
        public WorkItemProfile()
        {
            CreateMap<WorkItemDetailsDto, WorkItemDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApiId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Rev, opt => opt.MapFrom(src => src.Rev))
                .ForMember(dest => dest.AreaPath, opt => opt.MapFrom(src => src.Fields.AreaPath))
                .ForMember(dest => dest.TeamProject, opt => opt.MapFrom(src => src.Fields.TeamProject))
                .ForMember(dest => dest.IterationPath, opt => opt.MapFrom(src => src.Fields.IterationPath))
                .ForMember(dest => dest.WorkItemType, opt => opt.MapFrom(src => src.Fields.WorkItemType))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Fields.State))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Fields.CreatedDate))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Fields.Title))
                .ForMember(dest => dest.BoardColumn, opt => opt.MapFrom(src => src.Fields.BoardColumn))
                .ForMember(dest => dest.ActivatedDate, opt => opt.MapFrom(src => src.Fields.ActivatedDate))
                .ForMember(dest => dest.ResolvedDate, opt => opt.MapFrom(src => src.Fields.ResolvedDate))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Fields.AssignedTo.DisplayName))
                .ReverseMap();

            CreateMap<WorkItemHistorydto, WorkItemHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApiId, opt => opt.MapFrom(src => src.workItemId))
                .ForMember(dest => dest.Rev, opt => opt.MapFrom(src => src.Rev))
                .ForMember(dest => dest.OldValueDate, opt => opt.MapFrom(src => src.Fields.System_ChangedDate.OldValue))
                .ForMember(dest => dest.NewValueDate, opt => opt.MapFrom(src => src.Fields.System_ChangedDate.NewValue))
                .ForMember(dest => dest.OldValueColumn, opt => opt.MapFrom(src => src.Fields.System_BoardColumn.OldValue))
                .ForMember(dest => dest.NewValueColumn, opt => opt.MapFrom(src => src.Fields.System_BoardColumn.NewValue))
                .ReverseMap();

            CreateMap<Iterationdto, Iteration>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApiId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Attributes.StartDate))
                .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => src.Attributes.FinishDate))
                .ReverseMap();

            CreateMap<WorkItemRelationDto, WorkItemsList>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ApiId, opt => opt.MapFrom(src => src.Target.Id))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Target.Url))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src => src.SprintId))
                .ReverseMap();
        }
    }

}
