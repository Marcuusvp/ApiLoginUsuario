using Aplicacao.Dtos;
using AutoMapper;
using Dominio.Models;

namespace Aplicacao.Profiles
{
    public class CidadeProfile : Profile
    {
        public CidadeProfile()
        {
            CreateMap<NovaCidadeDto, Cidade>();
            CreateMap<Cidade, NovaCidadeDto>();

            CreateMap<CidadeDto, Cidade>();
            CreateMap<Cidade, CidadeDto>();
        }
    }
}
