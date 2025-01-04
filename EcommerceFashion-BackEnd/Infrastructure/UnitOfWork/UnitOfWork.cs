using Domain.Common;
using Infrastructure.Context;
using Infrastructure.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(EcommerceFashionDbContext context, 
            IAccountRepository accountRepository, 
            IRoleRepository roleRepository, 
            IBrandRepository brandRepository, 
            IBrandProductCategoryDataRepository brandProductCategoryDataRepository, 
            ICartItemRepository cartItemRepository, 
            ICartRepository cartRepository, 
            ICreditHistoryRepository creditHistoryRepository, 
            ICreditRepository creditRepository, 
            IDeliveryRepository deliveryRepository, 
            IFeedbackRepository feedbackRepository, 
            IOrderDetailAttributeRepository orderDetailAttributeRepository, 
            IOrderDetailRepository orderDetailRepository, 
            IOrderRepository orderRepository, 
            IPaymentRepository paymentRepository, 
            IProductRepository productRepository, 
            IProductCategoryRepository productCategoryRepository, 
            IProductAttributeValueRepository productAttributeValueRepository, 
            IProductAttributeRepository productAttributeRepository, 
            IProductCategoryAttributeDataRepository productCategoryAttributeDataRepository, 
            IProductCategoryDataRepository productCategoryDataRepository, 
            IProductVariantAttributeValueDataRepository productVariantAttributeValueDataRepository, 
            IProductVariantRepository productVariantRepository, 
            IProductVideoRepository productVideoRepository, 
            IWishListAttributeRepository wishListAttributeRepository, 
            IWishListRepository wishListRepository,
            IAccountRoleRepository accountRoleRepository)
        {
            Context = context;
            AccountRepository = accountRepository;
            RoleRepository = roleRepository;
            BrandRepository = brandRepository;
            BrandProductCategoryDataRepository = brandProductCategoryDataRepository;
            CartItemRepository = cartItemRepository;
            CartRepository = cartRepository;
            CreditHistoryRepository = creditHistoryRepository;
            CreditRepository = creditRepository;
            DeliveryRepository = deliveryRepository;
            FeedbackRepository = feedbackRepository;
            OrderDetailAttributeRepository = orderDetailAttributeRepository;
            OrderDetailRepository = orderDetailRepository;
            OrderRepository = orderRepository;
            PaymentRepository = paymentRepository;
            ProductRepository = productRepository;
            ProductCategoryRepository = productCategoryRepository;
            ProductAttributeValueRepository = productAttributeValueRepository;
            ProductAttributeRepository = productAttributeRepository;
            ProductCategoryAttributeDataRepository = productCategoryAttributeDataRepository;
            ProductCategoryDataRepository = productCategoryDataRepository;
            ProductVariantAttributeValueDataRepository = productVariantAttributeValueDataRepository;
            ProductVariantRepository = productVariantRepository;
            ProductVideoRepository = productVideoRepository;
            WishListAttributeRepository = wishListAttributeRepository;
            WishListRepository = wishListRepository;
            AccountRoleRepository = accountRoleRepository;
        }

        public EcommerceFashionDbContext Context { get; }

        public IAccountRepository AccountRepository {  get; }

        public IRoleRepository RoleRepository { get; }

        public IBrandRepository BrandRepository { get; }

        public IBrandProductCategoryDataRepository BrandProductCategoryDataRepository {get;}

        public ICartItemRepository CartItemRepository {get;}

        public ICartRepository CartRepository {get;}

        public ICreditHistoryRepository CreditHistoryRepository {get;}

        public ICreditRepository CreditRepository {get;}

        public IDeliveryRepository DeliveryRepository {get;}

        public IFeedbackRepository FeedbackRepository {get;}

        public IOrderDetailAttributeRepository OrderDetailAttributeRepository {get;}

        public IOrderDetailRepository OrderDetailRepository {get;}

        public IOrderRepository OrderRepository {get;}

        public IPaymentRepository PaymentRepository {get;}

        public IProductRepository ProductRepository {get;}

        public IProductCategoryRepository ProductCategoryRepository {get;}

        public IProductAttributeValueRepository ProductAttributeValueRepository {get;}

        public IProductAttributeRepository ProductAttributeRepository {get;}

        public IProductCategoryAttributeDataRepository ProductCategoryAttributeDataRepository {get;}

        public IProductCategoryDataRepository ProductCategoryDataRepository {get;}

        public IProductVariantAttributeValueDataRepository ProductVariantAttributeValueDataRepository {get;}

        public IProductVariantRepository ProductVariantRepository {get;}

        public IProductVideoRepository ProductVideoRepository {get;}

        public IWishListAttributeRepository WishListAttributeRepository {get;}

        public IWishListRepository WishListRepository {get;}

        public IAccountRoleRepository AccountRoleRepository { get;}

        public async Task<int> SaveChangeAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}