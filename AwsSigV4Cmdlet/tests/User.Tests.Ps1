BeforeAll {
    $accessKeyMaskLength = $env:AccessKey.Length - 4
    $secretKeyMaskLength = $env:SecretKey.Length - 4

    Write-Verbose "Using access key $("*" * $accessKeyMaskLength)$($env:AccessKey.Substring($accessKeyMaskLength)), and secret key $("*" * $secretKeyMaskLength)$($env:SecretKey.Substring($secretKeyMaskLength)). BaseUri: $env:BaseUri" 
}

Describe "Basig GET Operation should return 200" -Tag "GET" {
    $result = Invoke-AwsSigV4WebRequest -Uri "$BaseUri/api/values"

    $result.StatusCode | Should -Be 200
}