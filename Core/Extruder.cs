using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Core
{
    public class Extruder<TSource, TDest>
    {
        private readonly TSource _source;

        public Extruder(TSource source)
        {
            try
            {
                Mapper.Configuration.AssertConfigurationIsValid();
            }
            catch (Exception)
            {
                Mapper.Initialize(cfg => cfg.AddProfile(new MappingProfile()));
            }

            _source = source;
        }

        public TDest Generate()
        {
            return Mapper.Map<TDest>(_source);
        }
    }
}
