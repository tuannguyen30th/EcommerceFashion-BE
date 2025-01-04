using Domain.Entities;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BrandService.Commands
{
    public record BrandAddCommand(
       string Name,
       string VitualPath,
       string CoverImage,
       string Description
   ) : IRequest<ResponseModel>;
    public class BrandAddCommandHandler : IRequestHandler<BrandAddCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandAddCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ResponseModel> Handle(BrandAddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid brand data."
                    };
                }

                // Check if a brand with the same name exists
                var existingBrand = await _unitOfWork.BrandRepository.GetAllAsync(filter: _ => _.Name.Equals(request.Name));
                if (existingBrand.Data.Count != 0)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status409Conflict,
                        Message = "Brand with this name already exists."
                    };
                }

                // Create and save the brand entity
                var brand = new Brand
                {
                    Name = request.Name,
                    VitualPath = request.VitualPath,
                    CoverImage = request.CoverImage,
                    Description = request.Description,
                };

                await _unitOfWork.BrandRepository.AddAsync(brand);
                var result = await _unitOfWork.SaveChangeAsync();

                if (result > 0)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status201Created,
                        Message = "Brand created successfully."
                    };
                }

                return new ResponseModel
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Failed to create brand."
                };
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.Error.WriteLine($"Error in BrandAddCommandHandler: {ex}");

                return new ResponseModel
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while processing your request."
                };
            }
        }

    }
}
