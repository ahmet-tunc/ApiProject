using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.Helpers
{
    public static class GenericRepositoryHelper<TEntity>
    {
        public static List<PropertyInfo> GetProperties()
        {
            List<PropertyInfo> propertiesList = new List<PropertyInfo>();

            foreach (PropertyInfo propertyInfo in typeof(TEntity).GetProperties())
            {
                if (propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false).Length == 0)
                {
                    propertiesList.Add(propertyInfo);
                }
            }
            return propertiesList;
        }


        public static string GetPropertiesNames()
        {
            List<string> propertiesList = new List<string>();

            foreach (PropertyInfo propertyInfo in typeof(TEntity).GetProperties())
            {
                if (propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false).Length == 0)
                {
                    propertiesList.Add(propertyInfo.GetCustomAttributes<ColumnAttribute>().First().Name);
                }
            }
            return string.Join(",", propertiesList);
        }


        public static List<string> GetParametersOfProperties()
        {
            List<string> propertiesList = new List<string>();

            foreach (PropertyInfo propertyInfo in typeof(TEntity).GetProperties())
            {
                if (propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false).Length == 0)
                {
                    propertiesList.Add(":" + propertyInfo.Name);
                }
            }
            return propertiesList;
        }
    }
}
