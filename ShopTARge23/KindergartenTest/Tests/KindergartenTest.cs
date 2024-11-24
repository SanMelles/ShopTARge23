using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopTARge23.Controllers;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Models.Kindergarten;
using Microsoft.EntityFrameworkCore.Diagnostics;



namespace ShopTARge23.KindergartenTest.Tests
{
    public class KindergartenTest : TestBase
    {
        private readonly Mock<IKindergartenServices> _mockKindergartenService;
        private readonly Mock<IFileServices> _mockFileService;
        private readonly KindergartenController _controller;

        public KindergartenTest()
        {
            _mockKindergartenService = new Mock<IKindergartenServices>();
            _mockFileService = new Mock<IFileServices>();

            _controller = new KindergartenController(_context,
                _mockKindergartenService.Object, _mockFileService.Object);
        }
    }
}