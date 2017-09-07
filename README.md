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

Support for the AdHoc & Subscription endpoints have now been added.
Testing has been done to ensure that when a request is succesfull that all the variables
are correct and match the expected values. Work still needs to be done to handle all failures.
The status and code variables can be used to check this.

I am in the process of adding xml documentation for all types and methods. This is a process,
and more will get added in future releases.

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

# Supported API Endpoints

* AdHoc
* Subscription

# Support Post Type

* ITN Callback post from PayFast

# Validation

> Note this is still under development and it is not recommended to rely on for production use cases yet.

There is a new class called PayFastValidator, it is used to validate the PayFastNotify object created from a 
itn post from PayFast. All efforts have been taken to ensure that the validator class works as expected and it is now
considered stable. However it is still recommended to keep any validation you currently use in place, while this 
class is being used. It is new code and should be treated as such, as always feedback is greatly appreciated.

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

The latest version is 1.0.3.

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
