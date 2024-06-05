﻿using LMS.Application.Payment.Dto;
using LMS.Application.Payment.Interfaces;
using LMS.Domain.Payment.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Web.Controllers.Payment
{
    [Route("api/purchase/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PurchaseControllers : ControllerBase
    {
        private IPurchaseService _purchaseService;

        public PurchaseControllers(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ICollection<PurchaseEntity>>> GetMyPurchases([FromQuery] GetPurchasesListDto model)
        {
            return Ok(await _purchaseService.GetPurchasesList().Execute(model));
        }

        [HttpPost("product/{productId}/buy")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PurchaseEntity>> CreatePurchase([FromBody] CreatePurchaseDto model, Guid productId)
        {
            return Ok(await _purchaseService.CreatePurchase().Execute(model));
        }

        [HttpPost("{purchaseId}/confirm")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PurchaseEntity>> ConfirmPurchase([FromBody] ConfirmPurchaseDto model, Guid purchaseId)
        {
            return Ok(await _purchaseService.ConfirmPurchase().Execute(model));
        }

        [HttpGet("user/{tempUserId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ICollection<PurchaseEntity>>> GetUserPurchases(
            [FromQuery] GetPurchasesListDto model,
            Guid tempUserId)
        {
            return Ok(await _purchaseService.GetPurchasesList().Execute(model));
        }

        [HttpPatch("{purchaseId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<PurchaseEntity>> UpdatePurchase([FromBody] UpdatePurchaseDto model, Guid purchaseId)
        {
            return Ok(await _purchaseService.UpdatePurchase().Execute(model));
        }
    }
}
