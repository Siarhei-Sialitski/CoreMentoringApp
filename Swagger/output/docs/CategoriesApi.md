# IO.Swagger.Api.CategoriesApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ApiCategoriesGet**](CategoriesApi.md#apicategoriesget) | **GET** /api/Categories | 
[**ApiCategoriesIdGet**](CategoriesApi.md#apicategoriesidget) | **GET** /api/Categories/{id} | 
[**ApiCategoriesIdImagePut**](CategoriesApi.md#apicategoriesidimageput) | **PUT** /api/Categories/{id}/image | 

<a name="apicategoriesget"></a>
# **ApiCategoriesGet**
> List<CategoryDTO> ApiCategoriesGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ApiCategoriesGetExample
    {
        public void main()
        {
            var apiInstance = new CategoriesApi();

            try
            {
                List&lt;CategoryDTO&gt; result = apiInstance.ApiCategoriesGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoriesApi.ApiCategoriesGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<CategoryDTO>**](CategoryDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apicategoriesidget"></a>
# **ApiCategoriesIdGet**
> CategoryDTO ApiCategoriesIdGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ApiCategoriesIdGetExample
    {
        public void main()
        {
            var apiInstance = new CategoriesApi();
            var id = 56;  // int? | 

            try
            {
                CategoryDTO result = apiInstance.ApiCategoriesIdGet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoriesApi.ApiCategoriesIdGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 

### Return type

[**CategoryDTO**](CategoryDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apicategoriesidimageput"></a>
# **ApiCategoriesIdImagePut**
> ProductDTO ApiCategoriesIdImagePut (int? id, ImageDTO body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ApiCategoriesIdImagePutExample
    {
        public void main()
        {
            var apiInstance = new CategoriesApi();
            var id = 56;  // int? | 
            var body = new ImageDTO(); // ImageDTO |  (optional) 

            try
            {
                ProductDTO result = apiInstance.ApiCategoriesIdImagePut(id, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoriesApi.ApiCategoriesIdImagePut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **body** | [**ImageDTO**](ImageDTO.md)|  | [optional] 

### Return type

[**ProductDTO**](ProductDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
