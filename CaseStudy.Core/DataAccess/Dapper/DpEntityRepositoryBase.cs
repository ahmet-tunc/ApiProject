using CaseStudy.Core.Entities;
using CaseStudy.Core.Utilities.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Data.OracleClient;
using System.ComponentModel.DataAnnotations;
using Dommel;
using System.Data;
using CaseStudy.Core.Helpers;
using CaseStudy.Core.Constant;

namespace CaseStudy.Core.DataAccess.Dapper
{
    public class DpEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity:class, IEntity, new()
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionPath;


        public DpEntityRepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionPath = _configuration.GetConnectionString("Oracle");
            //Veritabanı bağlantısı sadece generic repository içerisinde gerçekleşiyor, özel işlemlerde ilgili dal içerisinde de veritabanı bağlantısı yapmak gerekir. Yanlış yapı
        }


        public async Task<IDataResult<TEntity>> Add(TEntity entity)
        {
            using (var connection = new OracleConnection(_connectionPath))
            {
                var table = typeof(TEntity).GetCustomAttribute<TableAttribute>();
                string namesOfProperties = GenericRepositoryHelper<TEntity>.GetPropertiesNames();
                var properties = GenericRepositoryHelper<TEntity>.GetProperties();
                var parametersOfProperties = GenericRepositoryHelper<TEntity>.GetParametersOfProperties();
                var stBuilder = new StringBuilder();
                stBuilder.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2})", new object[] { table.Name, namesOfProperties, string.Join(",", parametersOfProperties)});
                try
                {
                    connection.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    for (int i = 0; i < properties.Count(); i++)
                    {
                        parameters.Add(parametersOfProperties[i], entity.GetType().GetProperty(properties[i].Name).GetValue(entity,null));
                    }
                    var affectedRows = SqlMapper.Execute(connection, stBuilder.ToString(), param: parameters, commandType: CommandType.Text);
                    connection.Close();
                    return new SuccessDataResult<TEntity>(entity);
                }
                catch (Exception)
                {
                    return new SuccessDataResult<TEntity>(entity,Messages.ConnectionError,StatusCodeEnum.GatewayTimeout);
                }
            }
        }


        public IResult Delete(TEntity entity)
        {
            using (var connection = new OracleConnection(_connectionPath))
            {
                var result = connection.Delete<TEntity>(entity);
                if (result)
                    return new SuccessResult();

                return new ErrorResult();
            }
        }

        public IDataResult<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var connection = new OracleConnection(_connectionPath))
            {
                var result = connection.Get<TEntity>(filter);
                if (result != null)
                    return new SuccessDataResult<TEntity>(result,StatusCodeEnum.Success);

                return new ErrorDataResult<TEntity>(result,StatusCodeEnum.InternalServerError);
            }
        }

        public IDataResult<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var connection = new OracleConnection(_connectionPath))
            {
                var result = connection.Get<List<TEntity>>(filter).ToList();
                if (result != null)
                    return new SuccessDataResult<List<TEntity>>(result, StatusCodeEnum.Success);

                return new ErrorDataResult<List<TEntity>>(result, StatusCodeEnum.InternalServerError);
            }
        }

        public IDataResult<TEntity> Update(TEntity entity)
        {
            using (var connection = new OracleConnection(_connectionPath))
            {
                var result = connection.Update<TEntity>(entity);
                if (result)
                    return new SuccessDataResult<TEntity>(entity, StatusCodeEnum.Success);

                return new ErrorDataResult<TEntity>(entity, StatusCodeEnum.InternalServerError);
            }
        }
    }
}
