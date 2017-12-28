# ObjectiveTracker
A prototype web application for setting employee objectives and tasks.

This application was created in around 3 hours. It uses .NET Core 2.0 MVC Razor pages with Entity Framework as a quick solution to reduce front-end work, but my typical approach would be to use a WebAPI controller and Angular JS to link the view to the controller. This would have a far greater UI experience.

I have also written tests using xUnit that make sure my controllers are carrying out the required functionality and returning the expected results.

The application is using a local SQL database via Code First Migrations, and it automatically seeds test data the first time it is run.
