using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartUrl.Entities.Domain;
using SmartUrl.Repository;

namespace SmartUrl.Services.HashKey
{
    public class HashKeyService : IShortUrlService
    {
        private Random _randomGenerator;
        private readonly ManagedConfig _managedConfig;
        private readonly IDataProvider _dataProvider;

        public HashKeyService(IOptions<ManagedConfig> managedConfig, IDataProvider dataProvider)
        {
            _randomGenerator = new Random();
            _managedConfig = managedConfig.Value;
            _dataProvider = dataProvider;
        }

        public async Task<SmartUrlEntity> CreateSmartUrl(string url)
        {
            return await GenerateSmartUrl(url, _managedConfig.ApplicationPath);
        }
        public async Task<SmartUrlEntity> GetSmartUrl(string urlKey)
        {
            return await _dataProvider.GetSmartUrlByKey(urlKey);
        }

        private string GenerateRandomString(int length)
        {
            byte[] randomBytes = new byte[_randomGenerator.Next(length)];
            _randomGenerator.NextBytes(randomBytes);
            var data = Convert.ToBase64String(randomBytes);

            data = data.Replace("+", string.Empty);
            data = data.Replace("=", string.Empty);
            data = data.Replace("/", string.Empty);

            if (data.Length > length)
            {
                data = data.Substring(0, length);
            }

            if (data.Length != length)
            {
                data = data + GenerateRandomString(length);
            }

            if (data.Length > length)
            {
                data = data.Substring(0, length);
            }

            return data;
        }
        private string GenerateUniqueUrlKey()
        {
            string uniqueKey = string.Empty;

            var randomLength = _managedConfig.KeyLength;
            var mainInterations = _managedConfig.UniqueUrlKeyIterations;
            var uniqueKeyTryCount = 0;

            while (uniqueKeyTryCount++ < mainInterations && string.IsNullOrEmpty(uniqueKey))
            {
                var data = GenerateRandomString(randomLength);

                if (!string.IsNullOrEmpty(data) && _dataProvider.GetSmartUrlByKey(data) == null)
                {
                    uniqueKey = data;
                    break;
                }
            }

            return uniqueKey;
        }
        private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        private async Task<SmartUrlEntity> GenerateSmartUrl(string url,string path)
        {
            SmartUrlEntity objSmartUrlEntity = null;
            string urlHash = CalculateMD5Hash(url);

            objSmartUrlEntity = await _dataProvider.GetSmartUrlByHashAsync(urlHash);

            if (objSmartUrlEntity != null)
            {
                objSmartUrlEntity.IsSuccess = true;
                return objSmartUrlEntity;
            }

            objSmartUrlEntity = await _dataProvider.GetSmartUrlByShortAsync(url);

            if (objSmartUrlEntity != null)
            {
                objSmartUrlEntity.IsSuccess = true;
                objSmartUrlEntity.IsShortUrl = true;
                return objSmartUrlEntity;
            }

            var uniqueUrlKey = GenerateUniqueUrlKey();

            if (!string.IsNullOrEmpty(uniqueUrlKey))
            {
                objSmartUrlEntity = new SmartUrlEntity();
                objSmartUrlEntity.UrlKey = uniqueUrlKey;
                objSmartUrlEntity.Url = url;
                objSmartUrlEntity.UrlHash = urlHash;
                objSmartUrlEntity.ShortUrl = new Uri(string.Format(path, uniqueUrlKey)).ToString();

                await _dataProvider.Add(objSmartUrlEntity);
                objSmartUrlEntity.IsSuccess = true;
                return objSmartUrlEntity;
            }

            objSmartUrlEntity = new SmartUrlEntity();
            objSmartUrlEntity.IsSuccess = false;
            return objSmartUrlEntity;
        }
    }
}
