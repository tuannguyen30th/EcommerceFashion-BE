using Domain.Common;
using Infrastructure.Context;
using Infrastructure.IRepositories;
using System;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork 
    {
        EcommerceFashionDbContext Context { get; }
        #region Repository
        IBrandRepository BrandRepository { get; }
        IBrandProductCategoryDataRepository BrandProductCategoryDataRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        ICartRepository CartRepository { get; }
        ICreditHistoryRepository CreditHistoryRepository { get; }
        ICreditRepository CreditRepository { get; }
        IDeliveryRepository DeliveryRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        IOrderDetailAttributeRepository OrderDetailAttributeRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderRepository OrderRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IProductAttributeValueRepository ProductAttributeValueRepository { get; }
        IProductAttributeRepository ProductAttributeRepository { get; }
        IProductCategoryAttributeDataRepository ProductCategoryAttributeDataRepository { get; }
        IProductCategoryDataRepository ProductCategoryDataRepository { get; }
        IProductVariantAttributeValueDataRepository ProductVariantAttributeValueDataRepository { get; }
        IProductVariantRepository ProductVariantRepository { get; }
        IProductVideoRepository ProductVideoRepository { get; }
        IWishListAttributeRepository WishListAttributeRepository { get; }
        IWishListRepository WishListRepository { get; }
        IAccountRepository AccountRepository { get; }
        IRoleRepository RoleRepository { get; }
        IAccountRoleRepository AccountRoleRepository { get; }
        #endregion
        public Task<int> SaveChangeAsync();
    }
}
