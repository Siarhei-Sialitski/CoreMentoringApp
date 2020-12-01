/* 
 * Core Mentoring App Api
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using NUnit.Framework;

using IO.Swagger.Client;
using IO.Swagger.Api;
using IO.Swagger.Model;

namespace IO.Swagger.Test
{
    /// <summary>
    ///  Class for testing CategoriesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class CategoriesApiTests
    {
        private CategoriesApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new CategoriesApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of CategoriesApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' CategoriesApi
            //Assert.IsInstanceOfType(typeof(CategoriesApi), instance, "instance is a CategoriesApi");
        }

        /// <summary>
        /// Test ApiCategoriesGet
        /// </summary>
        [Test]
        public void ApiCategoriesGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ApiCategoriesGet();
            //Assert.IsInstanceOf<List<CategoryDTO>> (response, "response is List<CategoryDTO>");
        }
        /// <summary>
        /// Test ApiCategoriesIdGet
        /// </summary>
        [Test]
        public void ApiCategoriesIdGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? id = null;
            //var response = instance.ApiCategoriesIdGet(id);
            //Assert.IsInstanceOf<CategoryDTO> (response, "response is CategoryDTO");
        }
        /// <summary>
        /// Test ApiCategoriesIdImagePut
        /// </summary>
        [Test]
        public void ApiCategoriesIdImagePutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? id = null;
            //ImageDTO body = null;
            //var response = instance.ApiCategoriesIdImagePut(id, body);
            //Assert.IsInstanceOf<ProductDTO> (response, "response is ProductDTO");
        }
    }

}