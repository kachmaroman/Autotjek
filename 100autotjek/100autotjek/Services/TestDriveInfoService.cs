using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Syncfusion.DataSource.Extensions;
using _100autotjek.Helpers;
using _100autotjek.Models;

namespace _100autotjek.Services
{
    public class TestDriveInfoService
    {
        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient { Timeout = new TimeSpan(0, 0, 30) };
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<IEnumerable<TestNumberPlate>> GetNumberPlatesAsync(long dealerId)
        {
            const string url = "http://test-drive-car-management.golden.preview.com.ua/MobileTestNumberPlates/GetTestNumberPlatesByDealer/";

            var httpClient = GetHttpClient();
            var response = new HttpResponseMessage();
            var content = string.Empty;

            using (httpClient)
            {
                try
                {
                    response = await httpClient.GetAsync($"{url}{dealerId}");
                    response.EnsureSuccessStatusCode();
                    content = await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<TestNumberPlate>>(content));
        }

        public async Task<IEnumerable<Car>> GetListOfCarsAsync(long dealerId)
        {
            const string url = "http://test-drive-car-management.golden.preview.com.ua/MobileCars/GetCarsByDealer/";

            var httpClient = GetHttpClient();
            var response = new HttpResponseMessage();
            var content = string.Empty;

            using (httpClient)
            {
                try
                {
                    response = await httpClient.GetAsync($"{url}{dealerId}");
                    response.EnsureSuccessStatusCode();
                    content = await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Car>>(content));
        }

        public async Task<TestDriveInfo> GetDriverInfoAsync(string ssn)
        {
            const string url = "http://test-drive-car-management.golden.preview.com.ua/MobileTestDrive/GetDriverInfoBySSN/";

            var httpClient = GetHttpClient();
            var response = new HttpResponseMessage();
            var content = string.Empty;

            using (httpClient)
            {
                try
                {
                    response = await httpClient.GetAsync($"{url}{ssn}");
                    response.EnsureSuccessStatusCode();
                    content = await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TestDriveInfo>(content));
        }


        public async Task<HttpStatusCode> AddTestDriveAsync(TestDriveInfo testDriveInfo)
        {
            const string url = "http://test-drive-car-management.golden.preview.com.ua/MobileTestDrive/AddTestDrive/";

            var httpClient = GetHttpClient();

            var jsonTestDriveInfo = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(testDriveInfo));

            var response = new HttpResponseMessage();

            using (httpClient)
            {
                try
                {
                    response = await httpClient.PostAsync(url, new StringContent(jsonTestDriveInfo, Encoding.UTF8, "application/json"));
                }
                catch
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }

            return await Task.FromResult(response.StatusCode);
        }

        public async Task<HttpStatusCode> RemoveCarAsync(long carId)
        {
            const string url = "http://192.168.1.9:3000//MobileCars/DeleteCar/";

            var httpClient = GetHttpClient();
            var response = new HttpResponseMessage();

            using (httpClient)
            {
                try
                {
                    response = await httpClient.DeleteAsync($"{url}{carId}");
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }

            return await Task.FromResult(response.StatusCode);
        }

        public async Task<HttpStatusCode> UpdateCarAsync(Car car)
        {
            const string url = "http://test-drive-car-management.golden.preview.com.ua/MobileCars/UpdateCar/";

            var httpClient = GetHttpClient();

            var jsonCarInfo = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(car));

            var response = new HttpResponseMessage();
            
            using (httpClient)
            {
                try
                {
                    response = await httpClient.PostAsync(url, new StringContent(jsonCarInfo, Encoding.UTF8, "application/json"));
                }
                catch
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }

            return await Task.FromResult(response.StatusCode);
        }

        public async Task<HttpStatusCode> AddCarAsync(Car car)
        {
            const string url = "http://test-drive-car-management.golden.preview.com.ua/MobileCars/AddCar/";

            var httpClient = GetHttpClient();

            var jsonCarInfo = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(car));

            var response = new HttpResponseMessage();

            using (httpClient)
            {
                try
                {
                    response = await httpClient.PostAsync(url, new StringContent(jsonCarInfo, Encoding.UTF8, "application/json"));
                }
                catch
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }

            return await Task.FromResult(response.StatusCode);
        }

        public async Task<Car> GetCarByNumberAsync(string number)
        {
            const string url = "http://test-drive-car-management.golden.preview.com.ua/MobileCars/GetCarByNumber/?number=";

            var httpClient = GetHttpClient();
            var response = new HttpResponseMessage();
            var content = string.Empty;

            using (httpClient)
            {
                try
                {
                    response = await httpClient.GetAsync($"{url}{number}");
                    response.EnsureSuccessStatusCode();
                    content = await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Car>(content));
        }
    }
}
