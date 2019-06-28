using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoRental.Models;
using VideoRental.ViewModels;

namespace VideoRental.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerIndexViewModel>();
            Mapper.CreateMap<CustomerIndexViewModel, Customer>();

            Mapper.CreateMap<Customer, CustomerFormViewModel>();
            Mapper.CreateMap<CustomerFormViewModel, Customer>();

            Mapper.CreateMap<MembershipType, MembershipTypeViewModel>();
            Mapper.CreateMap<MembershipTypeViewModel, MembershipType>();

            Mapper.CreateMap<Movie, MovieIndexViewModel>();
            Mapper.CreateMap<MovieIndexViewModel, Movie>();

            Mapper.CreateMap<Movie, MovieFormViewModel>();
            Mapper.CreateMap<MovieFormViewModel, Movie>();

            Mapper.CreateMap<Genre, GenreViewModel>();
            Mapper.CreateMap<GenreViewModel, Genre>();

        }
    }
}