using Api_project_core.DTOs;
using Api_project_core.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.Mapping_Profiles
{
    public class Mapping_Profile
    {
        private static readonly TypeAdapterConfig _config = new TypeAdapterConfig();
        static Mapping_Profile()
        {
            _config.NewConfig<Items, ItemsDTO>().Map(dest => dest.ItemUnits, src => src.ItemsUnits.Select(x => x.Units.Name).ToList())
                .Map(dest => dest.Stores, src => src.InvItemStores.Select(x => x.Store.Name).ToList());
        }
        public static TypeAdapterConfig Config()
        {
            return _config;
        }

    }
}
