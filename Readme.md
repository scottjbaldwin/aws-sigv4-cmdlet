# AwsSigV4 Powershell Module

Protecting your API's is always a good idea. You need look no further than the recent Optus breach where PII
of around 10 million customers was leaked to an attacker due to an API that had no authentication. For AWS
API Gateway there are many different options for providing authentication, but of course many of the templates
used to get started don't come with any authentication at all. This makes it easy to get started and test that your
infrastructure is being provisiopned correctly, but before you hook up your API to any real data, you should
ensure that it is protected by some form of authentication.

One of the best ways of doing this is t use AWS IAM. Bsically, this means that a user has to have either long term
user credentials (AccessKey and SecretKey), or temporary credentials (mfa or role based). There's one catch... now
in order to call your API you need to sign every request with
[AWS Signature Version 4](https://docs.aws.amazon.com/general/latest/gr/signature-version-4.html).

Getting this right is non-trivial, and AWS do very little beyond providing some
[sample code](https://docs.aws.amazon.com/general/latest/gr/sigv4-signed-request-examples.html). As such a number of
Open Source projects have sprung up to fill in this glaring gap for various frameworks:

* [NodeJs](https://www.npmjs.com/package/aws4)
* [Python](https://pypi.org/project/aws-requests-auth/)
* [.Net](https://www.nuget.org/packages/AwsSignatureVersion4/)

And there are probably others. This project is a powershell module that implements the AWS Signature Version 4
signing process and hopefully gives a fairly familiar feeling to powershell users.

## Implementation Details

Sir Isaac Newton once said, "I see so far because I have stood on the shoulders of giants". I'd like to thanks the
maintainers of the [.Net AwsSignatureVersion4](https://github.com/FantasticFiasco/aws-signature-version-4) repository
for giving me shoulders to stand on. This module is a C# powershell module that directly uses this library and passess
all of the truly hard work off to it.

I have attempted to give the module a familiar feel for those who are familiar with powershell. My intention is to,
for the most part, be as similar to the `Invoke-WebRequest` commandlet as possible. The idea is that `Invoke-AwsSigV4WebReauest`
should be a fairly simple, drop-in replacement for code that is already using and working with `Invoke-WebRequest`
before you chose to enable IAM authentication. That said, there will be some differences (especially at this early stage),
but hopefully as this project progresses, it will feel more and more like a natural replacement.

## Early Days

So far I have very quickly (and roughly) implemented only the simplest possible use cases that I needed. This is
very much a work in progress.

### Road Map

* Documentation for `Get-Help` commandlet
* Automation Tests
* Add to Package Manager
* Build process
* Add other Verbs
* Add non-text based content types
* Better alignment with `Invoke-WebRequest`
* Add ability to use AWS profiles
