using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.MVC.Services.Contract;
using NSubstitute;
using BookStore.MVC.Models.ApiResponse.Book;
using System.Collections.Generic;
using BookStore.MVC.Controllers;
using System.Web.Mvc;
using BookStore.MVC.Models.ViewModel.Book;
using BookStore.MVC.Models.ApiResponse.Category;
using System.Linq;
namespace BookStore.MVC.Test
{
    [TestClass]
    public class BookControllerTest
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private BookController _bookController;
        [TestInitialize]
        public void Setup()
        {
            _bookService = Substitute.For<IBookService>();
            _categoryService = Substitute.For<ICategoryService>();
            _bookController = new BookController(_bookService, _categoryService);
            
        }
        [TestCleanup]
        public void Cleanup()
        {
            _bookService = null;
            _categoryService = null;
            _bookController = null;
        }

        [TestMethod]
        public void When_Index_Return_Valid_Data()
        {
            //arrange
            var getAllBookResponse = new GetAllBookResponse();
            var category = new CategoryResponse
            {
                 Id = 1,
                 Name = "Programming"
            };
            getAllBookResponse.Data = new List<BookResponse>
            {
                new BookResponse
                {
                    Id = 1,
                    Title = "Book1",
                    Category = category
                },
                new BookResponse
                {
                    Id = 2,
                    Title = "Book2",
                    Category = category
                },
            };
            getAllBookResponse.Success = true;
            _bookService.GetAllBook().Returns(getAllBookResponse);

            //act
            var result = _bookController.Index() as ViewResult;
            var resultModel = result.Model as BookGetAllViewModel;

            //assert
            Assert.AreEqual(resultModel.Books.Count, getAllBookResponse.Data.Count);


        }

        [TestMethod]
        public void When_Create_Return_Valid_Data()
        {
            //arrange
            var getAllCategoryResponse = new GetAllCategoryResponse();
            var categories = new List<CategoryResponse>
            {
                new CategoryResponse
                {
                    Id = 1,
                    Name = "Programming"
                },
                new CategoryResponse
                {
                    Id = 1,
                    Name = "Programming"
                },
            };
            getAllCategoryResponse.Success = true;
            getAllCategoryResponse.Data = categories;
            _categoryService.GetAllCategory().Returns(getAllCategoryResponse);
            var result = _bookController.Create() as ViewResult;
            var resultView = result.Model as FormBookViewModel;

            //assert
            Assert.AreEqual(getAllCategoryResponse.Data.First().Id.ToString(), resultView.CategorySelectList.First().Value);
            Assert.AreEqual(getAllCategoryResponse.Data.First().Name, resultView.CategorySelectList.First().Text);
        }

        [TestMethod]
        public void When_Create_Return_No_Category()
        {
            //arrange
            var getAllCategoryResponse = new GetAllCategoryResponse();
            getAllCategoryResponse.Success = false;
            _categoryService.GetAllCategory().Returns(getAllCategoryResponse);
            var result = _bookController.Create() as ViewResult;
            var resultView = result.Model as FormBookViewModel;

            //assert
            Assert.AreEqual(0, resultView.CategorySelectList.Count());
           
        }
    }
}
