using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoRental.Dtos;
using VideoRental.Models;
using VideoRental.ViewModels;

namespace VideoRental.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Customer, CustomerIndexViewModel>();
            CreateMap<CustomerIndexViewModel, Customer>();

            CreateMap<Customer, CustomerFormViewModel>();
            CreateMap<CustomerFormViewModel, Customer>();

            CreateMap<MembershipType, MembershipTypeViewModel>();
            CreateMap<MembershipTypeViewModel, MembershipType>();

            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<MembershipTypeDto, MembershipType>();

            CreateMap<Movie, MovieIndexViewModel>();
            CreateMap<MovieIndexViewModel, Movie>();

            CreateMap<Movie, MovieFormViewModel>();
            CreateMap<MovieFormViewModel, Movie>();

            CreateMap<Genre, GenreViewModel>();
            CreateMap<GenreViewModel, Genre>();
        }
    }
}