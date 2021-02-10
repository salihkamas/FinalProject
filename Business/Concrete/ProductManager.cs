using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            //Business Codes
            if (product.ProductName.Length<2)
            {
                //Magic Strings
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //Business Code
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult();
            }
            return new DataResult<List<Product>>(_productDal.GetAll(),true,"Products listed");
        }

        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new DataResult(_productDal.GetAll(p=> p.CategoryId==id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return _productDal.Get(p=>p.ProductId==productId);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<=max);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
