# PayFast 

This .Net library facilitates working with the PayFast api.
It provides classes for handling both the request and notify urls.
The PayFastRequest class makes it simple to create the url for the confirm page,
also known as the GET string. The PayFastNotify class is for handling return variables for the itn process.
It supports using the passphrase system. It also handles generating the signature for requests.

In order to simplify the use of the PayFastNotify class, which handles the return variables for the itn process,
a package has been created for the different versions of Asp.Net. Each package contains a custom model binder.
The function of these model binders are to ensure that the variables are read in the correct order
from the incoming form on the request and written correctly to the PayFastNotify class instance.
The samples have been updated to showcase how they are used.

Please be sure to use the correct package for the version of Asp.Net you are using.

# Live Samples

There are now two live samples

* [Asp.Net](https://payfast-demo-mvc.azurewebsites.net)
* [Asp.Net Core](https://payfast-demo.azurewebsites.net)

# Supported Runtimes

* .Net 4.5
* .Net 4.5.1
* .Net 4.6
* .Net 4.6.1
* .Net 4.6.2
* .Net Standard 1.6

> .Net Standard 2.0 Comming Soon

# Supported Request Types

* Once Off Payment
* Recurring Billing
* AdHoc Agreements

# Support Post Type

* ITN Callback post from PayFast

# Validation

> Note this is still under development and it is not recommended to rely on for production use cases yet.

There is a new class called PayFastValidator, it is used to validate the PayFastNotify object created from a 
itn post from PayFast. It currently returns inconsistant results for data validation requests, a matter that must be taken up with
PayFast and checked before it can be used in production.

It currently has the following validations available

* Validate Merchant Id
* Validate Source IP Address
* Validate Data

Feel free to give it a try and get your feedback back to me.

# Packages

## Nuget Feed

[ Payfast Nuget Link](https://www.nuget.org/packages/PayFast/)
[ Payfast.AspNet Nuget Link](https://www.nuget.org/packages/PayFast.AspNet/)
[ Payfast.AspNetCore Nuget Link](https://www.nuget.org/packages/PayFast.AspNetCore/)

> Install-Package PayFast

> Install-Package PayFast.AspNet

> Install-Package PayFast.AspNetCore

The latest version is 1.0.2. It is a pre-release, pending testing and feedback.

# Samples

Samples can be found in the samples directory on disk, or in the samples solution folder in VS.
There are now two samples, one for Asp.Net Mvc 5 and the other for Asp.Net Core

## PayFast.Web.Core

This is a ASP.NET Core application. In order to run the sample, use something like ngrok.
With a command like ngrok http 42817. To instruct IISExpress to accept the connection, you will 
need to mody the applicationhost.config file. you are looking for a value that looks like this:

> ```<binding protocol="http" bindingInformation="*:42817:localhost" />```

You would then change it to look like this:

> ```<binding protocol="http" bindingInformation="*:42817:*" />```

Sometimes visual studio has problems starting when the above change is made, try keeping it to the first one,
start the project, then close stop debugging and then make the change and then try again.

Make sure to look at appsettings.json, that is where you will configure all your PayFast related settings,
which will allow the sample to work correctly.
