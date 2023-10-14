using AutoMapper;
using Delivery.Domains.BaseEntitys;
using Delivery.Domains.Dto.OrderServicesDto.Lingau_OptLogDto;
using Delivery.Domains.Dto.OrderServicesDto.LingauDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto;
using Delivery.Domains.Dto.SystemServicesDto.SystemDict;
using Delivery.Domains.Dto.UserServicesDto.MenuDto;
using Delivery.Domains.OrderEntitys;
using Delivery.Domains.SystemEntitys;
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.CommonInitializers.CommonInitializer.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Menu, MenuResponse>();

            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();
            CreateMap<OrderTagOrReportResponse,Order_TagOrReport>().ReverseMap();

            CreateMap<Lingau, LingauResponse>().ReverseMap();
            CreateMap<LingauRequest, Lingau>().ReverseMap();
            CreateMap<Lingau_OptLog, LingauOptLogResponse>().ReverseMap();
            
            CreateMap<SystemDictRequest, SystemDict>().ReverseMap();
            //CreateMap<List<SystemDictResponse>, List<SystemDict>>().ReverseMap();
            CreateMap<SystemDict, SystemDictResponse>().ReverseMap();
        }
    }
}
