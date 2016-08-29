using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
namespace Resmed.Stock.Service
{
    public static class ServiceMapper
    {
        static ServiceMapper()
        {
            ConfigureMapper();             
        }


        private static void ConfigureMapper()
        {   
              
                     
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            TDestination output = Mapper.Map<TSource, TDestination>(source);
            return output;
        }

    }
}