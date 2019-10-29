using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Features.Models;
using Features.ViewModels;

namespace Features.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<User, UserViewModel>().ReverseMap();
                    cfg.CreateMap<File, FileViewModel>().ReverseMap();
                    /*-- AddMap --*/                    
                });


        }
    }
}