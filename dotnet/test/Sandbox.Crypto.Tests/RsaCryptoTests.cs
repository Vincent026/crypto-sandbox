using FluentAssertions;
using Xunit;

namespace Sandbox.Crypto.Tests
{
    public class RsaCryptoTests
    {
        private readonly string _plainText = "Here is some data to encrypt!";

        [Fact]
        public void GenerateKeyPair_should_generate_key_pair()
        {
            var rsaCrypto = new RsaCrypto();

            var (privateKeyJson, publicKeyJson) = rsaCrypto.GenerateKeyPair(2048);

            privateKeyJson.Should().NotBeNullOrWhiteSpace();
            publicKeyJson.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void Crypto_should_encrypt_and_decrypt_with_new_key()
        {
            var rsaCrypto = new RsaCrypto();
            var (privateKeyJson, publicKeyJson) = rsaCrypto.GenerateKeyPair(2048);

            var encrypted = rsaCrypto.Encrypt(_plainText, publicKeyJson);

            encrypted.Should().NotBeNullOrWhiteSpace();
            encrypted.Should().NotBe(_plainText);

            var decrypted = rsaCrypto.Decrypt(encrypted, privateKeyJson);

            decrypted.Should().NotBeNullOrWhiteSpace();
            decrypted.Should().Be(_plainText);
        }

        [Theory]
        [InlineData("{\"D\":null,\"DP\":null,\"DQ\":null,\"Exponent\":\"AQAB\",\"InverseQ\":null,\"Modulus\":\"vSPpZZ4r5zhM3rlaS75i33l6wgQjk9YR1y/QV6+r2NoJS8I17dLZrqk6ivSCgOkRT08/wNUvAUO6vuvaSKo5mG9AalzwVA1EM5FOaaWxYqs3mSnIqft1leL8Q/DHx1A4RFW/SVq+3n8WXV0w+NW4p33yH3eLKD6O3aeCBj9kgkOammeu5uYqcPpu/UQd4tcTEuwE6d1Mcyf6VeBK4J9DMDAcrY43mWc4s/3Vh4AJQXs/iEtqxxlpk8Zv0BNoucomkbw8ZsvdGW+FcnCmcCvBsWBZhI/PZflQGr7FNcsL2LHGQxeIZ1epNzCeTcyVcjZjnNuBcGBL1kymOwOyGN5NPQ==\",\"P\":null,\"Q\":null}")]
        public void Encrypt_should_encrypt_plainText(string publicKeyJson)
        {
            var rsaCrypto = new RsaCrypto();

            var encrypted = rsaCrypto.Encrypt(_plainText, publicKeyJson);

            encrypted.Should().NotBeNullOrWhiteSpace();
            encrypted.Should().NotBe(_plainText);
        }

        [Theory]
        [InlineData("Y8aPaBrzc/YcFNTGQpdzGbDymSoacQP7cykax9hDaT8SS2E9f6kOKafiQLQOB/TvjPttkALkdYulZdVBc9ZZCmXVlUYn9ArtYV1k4xKvUOSNGJQS+2N1AuhoTUfX4BjrLGs+IuqWR2pIFYqUGKSZWveEHxsDJ3v532GNH5m+gKLCwecgBCpTismqS/XbDyEHS5edgJ5kSTEUIKU0Hn+hJbm8SiQ85nqRmxAlaBngjY4TgFF4d1Rg0e1M740NkadtT51W7RX4Xt9tq1EYymB9Epye6Ly1uN2ioikfI+qzVVyy5VkmeVcF3RB0OfUOERuJw+Ijap9xGC88nP+zQpCLuQ==",
            "{\"D\":\"iyAyW3QomTEZoi83U0XdFsMV4EcJIIKzptTd7NIklyNy+Q4He0PAqbCDhpjqsgY+nFcP7zkhFU4LvMpiS1cjfJRBDrwEb98+TqxQwHeH7qtA/2Hz380/JhzMMPGZEmxYpzNnvH6KrcFP4ydckX0sEdAb3LejXdv+XN8NTqkstn/bqDmgRTkfjCaslVd/HcVO3jYHPBx6bryjU+4SK2XnBZj5nnkDxTynF+T9C4Wi3np2/xUBOfynIV8O6GrhRgKlhecpMLfXiD8nz0HJ7ahWLltMD75RWgjE+YxQsqFQiRe5OJtMcYDqPBe4vi65+QJ2eddi7Kw3iZr/C37tSLxpBQ==\",\"DP\":\"qthpbiC8IDTGfQ5eym2mRPPrzsRrog3Gp4/pakhBL4H++pQN1ZJV2emLK/vMJEdPbLfhOOT9UDczcGZJr6C9grUp0eaVCxMFEvwau155JENjsCuwTiU9hzch0T46uIXcE1Wse3mRsAA0AiLX0epi9lSSrb+5jWK2GoFcyKkL8E0=\",\"DQ\":\"XXh0vJP0HQ8yc6qFZIw21ATUnzRvJJc+MCmklhkCtZOnlmxj4+tShNzskpbj0mrZTE63db0vfpj2+x1DsTz0pO+vnwcn7KGhndqYyClao9B9D9rORkFfYir9vHbK14kRr8hiN7lGdq419N7h9J7e49vZnYeQYm4/lT636+3YrqE=\",\"Exponent\":\"AQAB\",\"InverseQ\":\"tICa95haC4FO7JFr/XF9VXemso/Rq3yDUGb1tn0wfooht6OmsN+QZNHzPzW6/dsaerCT+czeOB9GhCLHl5iMTlELb8r8Ri1pmp6SSanl0h6TarxobEYGfdYhigpNKsXAVKVuJIDzFMVzdkmjrSyepEgZWYVyrlc2F8esSGQyUFQ=\",\"Modulus\":\"vSPpZZ4r5zhM3rlaS75i33l6wgQjk9YR1y/QV6+r2NoJS8I17dLZrqk6ivSCgOkRT08/wNUvAUO6vuvaSKo5mG9AalzwVA1EM5FOaaWxYqs3mSnIqft1leL8Q/DHx1A4RFW/SVq+3n8WXV0w+NW4p33yH3eLKD6O3aeCBj9kgkOammeu5uYqcPpu/UQd4tcTEuwE6d1Mcyf6VeBK4J9DMDAcrY43mWc4s/3Vh4AJQXs/iEtqxxlpk8Zv0BNoucomkbw8ZsvdGW+FcnCmcCvBsWBZhI/PZflQGr7FNcsL2LHGQxeIZ1epNzCeTcyVcjZjnNuBcGBL1kymOwOyGN5NPQ==\",\"P\":\"+UUFEGRdnDjk5K69Ju21lRJA34EIqmS+kY0hiR9mZE10toflVi911AVdDhgY5RW8P0fhABenNlEn28MwcX9uTPSX0sXZmKk/GjZEUeTf1mX4nHnI6O0/XfwbioEIB2O6tCE8tC1ZnOlrZiic3wOG85WWwlA4EHvajSI/o2LFlxM=\",\"Q\":\"wj9FZgdosbUNqTaf4PiPIh/C/NORBeWGv8V2D9OFgjHaJ+gOKOgnWyxuhtjTyQxvG7ruwv4YH8KcSGEknWyAFI6E3DUMf+H7lG0GpX0ZsaQMuznIayafAYNPrdIUo0uJfQeMqhxMwruncdEq49JwgEgx0kpn/N8n5wxyH70EhG8=\"}")]
        public void Decrypt_should_decrypt_cipherText(string cipherText, string privateKeyJson)
        {
            var rsaCrypto = new RsaCrypto();

            var decrypted = rsaCrypto.Decrypt(cipherText, privateKeyJson);

            decrypted.Should().NotBeNullOrWhiteSpace();
            decrypted.Should().Be(_plainText);
        }
    }
}
