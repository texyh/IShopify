using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Framework {
    public interface ICryptoService {

        string CreateUniqueKey(int length = 32);

        string Hash (string text, string salt = null, int iterations = 1);

        string GenerateSalt (int maxLenght);
    }
}