using Aplicacao.Dtos;
using AutoMapper;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<NovoUsuarioDto, Usuario>();
            CreateMap<UsuarioDto, Usuario>()
                .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password));
            CreateMap<AlteraSenhaUsuarioDto, Usuario>();
            CreateMap<MudarSenhaUsuarioDto, Usuario>();
        }
    }
}
