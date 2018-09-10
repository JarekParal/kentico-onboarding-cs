# kentico-onboarding-cs

#### Summary
Create a Web API that will provide persistence to your JavaScript application (from task 5) using the latest .NET Framework (not .NET Core).

##### Application design
 * Web API will (eventually) provide CRUD operations for items in your JS front-end
 * Database provider might change at any time – has to be isolated in a single assembly
 * Controllers have only and single responsibility – they react to user calls
 * Dependency injection is a must (since you favor TDD, right)
 * It is a simple application, so some simplifications are allowed (some are not):
(OK) having a single assembly for all contracts and (single) DTO is perfectly OK
(OK) dependency injection framework is referenced in all assemblies
(BAD) having a public implementation of a contract is NOT OK → their implementation must be internal
(BAD) having single assembly is NOT OK → (Web) API, (intra-assembly) contracts and database assemblies should emerge by now

##### Technology stack
 * be [as async as possible](https://msdn.microsoft.com/en-us/magazine/dn802603.aspx) in controllers and services and repositories
 * use [Mongo DB as NoSQL](http://docs.mlab.com/languages/) storage provider. [mLab](https://mlab.com/) is at your service (and for free) – `single assembly reference expected`
 * use [Unity as Dependency Injection container](https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/dependency-injection) – `all contract-implementing assemblies and API assembly reference expected`
 * use [NUnit as test automation framework](https://kentico.atlassian.net/wiki/display/TEST/NUnit) and [NSubstitute](http://nsubstitute.github.io/help/getting-started/) as mocking framework – `only test assemblies reference expected`
 * do not use [AutoMapper](http://docs.automapper.org/en/stable/Getting-started.html) as this is not necessary for API of this size, however, get to know it at least theoretically

##### Tooling
 * Use newest (RC is permissible) version of [Visual Studio](https://kentico.atlassian.net/wiki/spaces/KA/pages/120324187/Visual+Studio) – check out the link to ensure you are set-up correctly and leverage your [R#](https://kentico.atlassian.net/wiki/display/KA/ReSharper) license.
 * Use [Postman](https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop) to tune (and debug) API calls

##### Way of work
 * Preferably work in pairs (strong preference)

#### TASK 0 - Prepare environment 

##### Create a new GitHub repository 
 * *kentico-onboarding-cs* sounds like a good name for the repository of yours (it is a terrible name for a solution, though ;-))
 * Use this file as README of your repository
 * Give write access to your peer and possible reviewers
 * Commit correct [.gitignore](https://github.com/github/gitignore)
 * Use [GitFlow workflow](https://kentico.atlassian.net/wiki/spaces/KA/pages/201097293/03+-+Git)
 * Commit to a feature branch (<sub> features/task-0 in this case)
 * There will be no upstream repository in this case

##### Set up continuous integration for your GitHub repository
 * Set-up continuous integration in the same way as in your JS repository (look at the CI part of [JS task 0](https://github.com/KenticoAcademy/kentico-onboarding-js#task-0))

##### Use your Visual Studio to create an ASP.NET Web Application
 * Be careful about difference between Name and Solution name (the name should have .Api suffix)
 * Use your default Git repository folder as Location
 * Do not forget to Create directory for solution

##### Select Empty ASP.NET 4.6 template
 * Check Add folders and core references for Web API
 * Add unit tests, but do NOT check Host in the cloud

##### Final push
 * Remove any empty classes, clean up the solution to contain only as much *.cs files as necessary
 * Push your solution to your origin repository
 * Create a pull-request to your develop branch

#### TASK 1 - Create dummy controller

##### Learn what is [REST](https://kentico.atlassian.net/wiki/x/zlyoD)
##### Learn what is [NUnit](https://kentico.atlassian.net/wiki/display/TEST/NUnit)
 * motivation on [constraint asserts](http://tddaddict.blogspot.cz/2015/11/nunit-assertions-constraint-model-and.html) (already familiart *jest* tests)

##### Learn why [async](https://msdn.microsoft.com/en-us/magazine/dn802603.aspx)
##### Create a dummy controller that will be *RESTful* and that will:
 * All following actions can be "implemented" in the controller (for now)
 * All controller actions are covered by tests (preferably use TDD)
 * Provide list of (random static) items
 * Add a new item
 * Display item with given ID (whatever ID one provides, a predefined static item will be served)
 * Delete item with given ID (whether it exists or not, it will act like it did)
 * Change item with given ID (whether it exists or not, it will act like it did)

##### Version your API
 * Read a bit about [versioning, its motivation, and differences of individual approaches](https://www.troyhunt.com/your-api-versioning-is-wrong-which-is/)
 * Apply at least *URL* versioning in your dummy controller

(!) **Important**
 * When you finish this task and your peer is not available, work on task *Connect JS to CS* from now on.
 * Everybody needs to develop the functionality of *Connect JS to CS* on their own (and to their own [JS onboarding](https://kentico.atlassian.net/wiki/display/KA/05</ins>-<ins>JS</ins>Onboarding<ins>task) repository)
{panel}

#### TASK 2 - Introduce DI and dummy DB layer

##### Learn what is [repository pattern](http://blog.sapiensworks.com/post/2014/06/02/The-Repository-Pattern-For-Dummies.aspx)
##### Learn what [dependency injection or inversion of control or Holywood principle](http://www.javaworld.com/article/2071914/excellent-explanation-of-dependency-injection--inversion-of-control-.html) principles refer to and what [Unity](https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/dependency-injection) is
##### Learn what is [NSubstitute](http://nsubstitute.github.io/help/getting-started/) 
##### Extend your API with a dummy repository (database layer) that will be injected into controller (along with any other current dependencies)
 * One might encounter problems with [obtaining an instance of *HttpRequestMessage* outside API controller](https://stackoverflow.com/questions/16670329/how-to-access-the-current-httprequestmessage-object-globally) 
 * Repository itself and routing does not have to be covered by tests

#### TASK 3 - Connect mongoDB and finish GET</ins>POST+DELETE

##### Sanitize secrets (a.k.a private connection string)
 * you will provide yourself with a database URL and username and password later in this task, let's looks on web application secrets topic and prepare our ground for them properly.
 * Understand why a connection string must not be in code directly and should not be a part of *database layer*
 * Make sure your connection string (or any other sensitive/instance-specific/secret information) is not checked-in in your public GitHub repository 
 * Store your secrets in [web.config](https://msdn.microsoft.com/en-us/library/ms733932.aspx) file of your *presentation layer*
 * Sanitize your *web.config* based on [this article](http://johnatten.com/2014/04/06/asp-net-mvc-keep-private-settings-out-of-source-control/)
 * Do not forget your [connection string is a dependency](http://www.devtrends.co.uk/blog/configuration-settings-are-a-dependency-that-should-be-injected)

##### Connect your API to a mongo DB
 * Use [mLab](https://mlab.com/) to create a sandbox NoSQL database
 * Select the Azure cloud provider with the location in North Europe
 * While replacing dummy repository's logic completely, implement only unconditional *obtainment_ and _addition* in API controller

##### API clean-up
 * Make sure your controller's actions have a single responsibility and act on a single (highest) level of abstraction.
 * Extend your model with two properties each representing date and time of model's *creation_ and its _last update/change* (these values will be sent through API to clients, however, it will not be consumed by your JS front-end in any way)
 * Make sure (API) user input is sanitized (at least somewhat) properly
 * If you have no [service layer](https://martinfowler.com/eaaCatalog/serviceLayer.html) so far, there is something wrong – do not forget you write a simplified version of (somewhat large) [multitier application](https://en.wikipedia.org/wiki/Multitier_architecture) with [separated presentation](https://martinfowler.com/eaaDev/SeparatedPresentation.html)

#### (optional) TASK 4 - Finish remaining HTTP verbs

##### Finish remaining [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) operations (update) and eventually (extra optional) patch
##### Finish interconnecting with your JS application

#### (optional) TASK 5 - Publish CS apps online

##### Publish your Web API application to Azure Web App
 * ask <</sub>petrs2> to share Academy subscription with you
 * prefix all your resources with your login (including resource group)
 * be careful not create a resource that consumes money - let's start low and watch our expenses before first customers pay
 * add [Application Insights Telemetry](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-asp-net)
   * do not use Visual Studio to create any resources, always use Azure portal
 * be extra careful not create a resource that consumes money
 * configure resource group and create/use (free) service plan in North Europe

##### Implement Continuous Deployment
 * extend CI set-up of your CS repository so each push to the *master* branch triggers release
 * alternatively set-up deployment options of the service plan for your CS repository on the Azure portal so each push to the *master* branch triggers release
