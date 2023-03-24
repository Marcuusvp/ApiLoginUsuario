using Aplicacao.Dtos;
using AutoMapper;
using Dominio.Models;

namespace Aplicacao.Profiles
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<NovaPessoaDto, Pessoas>();
            //.ForMember(dominio => dominio.NomeCompleto,
            //dto => dto.MapFrom(dto => dto.Nome));
            CreateMap<Pessoas, NovaPessoaDto>();

            CreateMap<PessoaDto, Pessoas>();
            CreateMap<Pessoas, PessoaDto>();
        }
    }
}
