# IO.Swagger.Api.ProductsApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ApiProductsGet**](ProductsApi.md#apiproductsget) | **GET** /api/Products | 
[**ApiProductsIdDelete**](ProductsApi.md#apiproductsiddelete) | **DELETE** /api/Products/{id} | 
[**ApiProductsIdGet**](ProductsApi.md#apiproductsidget) | **GET** /api/Products/{id} | 
[**ApiProductsIdPut**](ProductsApi.md#apiproductsidput) | **PUT** /api/Products/{id} | 
[**ApiProductsPost**](ProductsApi.md#apiproductspost) | **POST** /api/Products | 

<a name="apiproductsget"></a>
# **ApiProductsGet**
> List<ProductDTO> ApiProductsGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ApiProductsGetExample
    {
        public void main()
        {
            var apiInstance = new ProductsApi();

            try
            {
                List&lt;ProductDTO&gt; result = apiInstance.ApiProductsGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductsApi.ApiProductsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<ProductDTO>**](ProductDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apiproductsiddelete"></a>
# **ApiProductsIdDelete**
> ProductDTO ApiProductsIdDelete (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ApiProductsIdDeleteExample
    {
        public void main()
        {
            var apiInstance = new ProductsApi();
            var id = 56;  // int? | 

            try
            {
                ProductDTO result = apiInstance.ApiProductsIdDelete(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductsApi.ApiProductsIdDelete: " + e.Message );
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

[**ProductDTO**](ProductDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apiproductsidget"></a>
# **ApiProductsIdGet**
> ProductDTO ApiProductsIdGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ApiProductsIdGetExample
    {
        public void main()
        {
            var apiInstance = new ProductsApi();
            var id = 56;  // int? | 

            try
            {
                ProductDTO result = apiInstance.ApiProductsIdGet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductsApi.ApiProductsIdGet: " + e.Message );
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

[**ProductDTO**](ProductDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apiproductsidput"></a>
# **ApiProductsIdPut**
> ProductDTO ApiProductsIdPut (int? id, ProductDTO body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ApiProductsIdPutExample
    {
        public void main()
        {
            var apiInstance = new ProductsApi();
            var id = 56;  // int? | 
            var body = new ProductDTO(); // ProductDTO |  (optional) 

            try
            {
                ProductDTO result = apiInstance.ApiProductsIdPut(id, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductsApi.ApiProductsIdPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **body** | [**ProductDTO**](ProductDTO.md)|  | [optional] 

### Return type

[**ProductDTO**](ProductDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apiproductspost"></a>
# **ApiProductsPost**
> ProductDTO ApiProductsPost (ProductDTO body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ApiProductsPostExample
    {
        public void main()
        {
            var apiInstance = new ProductsApi();
            var body = new ProductDTO(); // ProductDTO |  (optional) 

            try
            {
                ProductDTO result = apiInstance.ApiProductsPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductsApi.ApiProductsPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ProductDTO**](ProductDTO.md)|  | [optional] 

### Return type

[**ProductDTO**](ProductDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
